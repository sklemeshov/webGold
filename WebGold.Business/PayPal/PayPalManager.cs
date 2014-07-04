using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using webGold.Business.Model;
using webGold.Repository;
using webGold.Repository.Entity;

namespace webGold.Business.PayPal
{
   public class PayPalManager
    {
       private IPaymentRepository _repository;
       public IPaymentRepository Repository
        {
            get { return _repository ?? (_repository =  RepositoryHelper.Initialize()); }
        }

       private string _userId;
       private string _userEmail;

       public PayPalManager(string userId, string userEmail)
       {
           _userEmail = userEmail;
           _userId = userId;
       }
       public static PayPalResponseResultModel PayPalResponseResult(string token, string payerId)
       {
           var model = new PayPalResponseResultModel();
           var repository = new PaymentRepository();
           var request = new GetExpressCheckoutDetailsRequestType {Token = token};
           var wrapper = new GetExpressCheckoutDetailsReq {GetExpressCheckoutDetailsRequest = request};
           var service = new PayPalAPIInterfaceServiceService();

           GetExpressCheckoutDetailsResponseType ecResponse = service.GetExpressCheckoutDetails(wrapper);
           var doExpressCheckoutRequest = new DoExpressCheckoutPaymentRequestType();
           var requestDetails = new DoExpressCheckoutPaymentRequestDetailsType();
           doExpressCheckoutRequest.DoExpressCheckoutPaymentRequestDetails = requestDetails;
           requestDetails.PaymentDetails = ecResponse.GetExpressCheckoutDetailsResponseDetails.PaymentDetails;
           requestDetails.Token = token;
           requestDetails.PayerID = payerId;
           requestDetails.PaymentAction = PaymentActionCodeType.SALE;             
           var doCheckoutWrapper = new DoExpressCheckoutPaymentReq
                                   {
                                       DoExpressCheckoutPaymentRequest =
                                           doExpressCheckoutRequest
                                   };
           DoExpressCheckoutPaymentResponseType doECResponse = service.DoExpressCheckoutPayment(doCheckoutWrapper);
           PaymentInfoType paymentInfoType = doECResponse.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0];
           bool isPaid = doECResponse.Ack.Equals(AckCodeType.SUCCESS);
           var payPalStatus = paymentInfoType.PaymentStatus;
           Repository.Entity.PayPal payPal = repository.GetPayPalBy(token);
           if (payPal == null)
           {
               model.Errors = "Current transaction is not found.";
               return model;
           }
           var amount = Converter.ParseToDouble(paymentInfoType.GrossAmount.value);
           var payPalEntity = payPal as Repository.Entity.PayPal;
           if (amount != payPalEntity.Amount)
           {
               model.Errors = "Not enough money in the account.";
               return model;
           }
           var entity = new Repository.Entity.PayPal();
           entity.Id = payPalEntity.Id;
           entity.UpdateTime = DateTime.UtcNow;
           entity.State = doECResponse.Ack.Value.ToString().ToLower();
           entity.InternalPaymentId = payPalEntity.InternalPaymentId;
           entity.PayerId = payerId;
           entity.UserId = payPalEntity.UserId;
           entity.Amount = payPalEntity.Amount;
           entity.Fee = Converter.ParseToDouble(paymentInfoType.FeeAmount.value);
           entity.CurrencyCode = payPalEntity.CurrencyCode;

           Repository.Entity.PayPal paypalEntity = repository.GetPayPalBy(entity.InternalPaymentId);
           var needToDeposit = paypalEntity.State.Equals(StateType.PENDING.ToString()) && isPaid;
           repository.UpdatePayPal(entity);
           if (needToDeposit)
           {
               Deposit(entity, repository);
           }
           model.IsSucces = true;
           return model;
       }

       public static void Deposit(Repository.Entity.PayPal entity, IPaymentRepository repository)
         {
             if (!entity.CurrencyCode.Equals("USD", StringComparison.InvariantCultureIgnoreCase))
             {
                 throw new ApplicationException(string.Format("Unable to deposit. Unconfigured currency {0}.", entity.CurrencyCode));
             }
             var account = repository.GetAccountBy(entity.UserId);
             double amount = entity.Amount - entity.Fee;
             if(account == null)
             {
                 account = new Account
                           {
                               Id = Guid.NewGuid().ToString(),
                               UserId = entity.UserId,
                               GldAmount = 0,
                               UsdAmount = 0
                           };
                 repository.CreateAccount(account);
             }
             if (account.UsdAmount != 0)
             {
                 account.UsdAmount = account.UsdAmount + amount;
             }
             else
             {
                 account.UsdAmount = amount;
             }
            account.GldAmount = new GoldenStandartConverter().ConvertFromUsdToGld(account.UsdAmount);
            repository.UpdateAccount(account);
             var paymentHistoryEntity = new PaymentHistory();
             paymentHistoryEntity.Id = Guid.NewGuid().ToString();
             paymentHistoryEntity.PaymentType = ((int)PaymentType.PayPal).ToString();
             paymentHistoryEntity.PaymentMethod = ((int)PaymentMethod.Credit).ToString();
             paymentHistoryEntity.Date = DateTime.UtcNow;
             paymentHistoryEntity.Amount = amount; 
             paymentHistoryEntity.Currency = "USD";
             paymentHistoryEntity.TransactionStatus = entity.State;
             paymentHistoryEntity.AccountId = account.Id;
             paymentHistoryEntity.UserId = entity.UserId;
             repository.CreatePaymentHistory(paymentHistoryEntity);
         }

       public static IList<PaymentHistory> GetHistoryCollectionBy(string userId)
       {
           return new PaymentRepository().GetPaymentHistoryBy(userId);
       }

       public static void PayPalWithdraw(Repository.Entity.PayPal entity, string emailTo)
       {
           var repository = new PaymentRepository();
           double curAmount = entity.Amount;
           var account = repository.GetAccountBy(entity.UserId);
           account.UsdAmount = account.UsdAmount - curAmount;
           //account.UsdAmount
           repository.UpdateAccount(account);
           var paymenthistory = new PaymentHistory();
           paymenthistory.Id = Guid.NewGuid().ToString();
           paymenthistory.UserId = entity.UserId;
           paymenthistory.Amount = curAmount;
           paymenthistory.Date = DateTime.UtcNow;
           paymenthistory.Currency = "USD";
           paymenthistory.AccountId = account.Id;
           paymenthistory.PaymentMethod = entity.PaymentMethod;
           paymenthistory.PaymentType = ((int)PaymentType.Transfer).ToString();
           paymenthistory.TransactionType = "Sent";
           paymenthistory.TransactionStatus = "Pending";
           paymenthistory.ReceivedEmail = emailTo;
           repository.CreatePaymentHistory(paymenthistory);
       }

       public static double GetAmmountSumInDayBy(string userId)
       {
           var repository = new PaymentRepository();
           return repository.GetAmmountPayPalPorLastDay(userId);
       }

       public static double GetAmmountTransferPorLastDay(string userId)
       {
           var repository = new PaymentRepository();
           return repository.GetAmmountTransferPorLastDay(userId);
       }

       public static AccountBalanceModel GetLastTransaction(string userId)
       {
           var repository = new PaymentRepository();
           var tModel = repository.GetLastTransaction(userId);
           var accountBalanceModel = new AccountBalanceModel(tModel.Amount);
           //accountBalanceModel.Usd = tModel.Amount;
           //accountBalanceModel.Gs = (Int64) new GoldenStandartConverter().ConvertFromUsdToGld(tModel.Amount);
           return accountBalanceModel;
       }

       public PayPalResponseResultModel SetExpressCheckoutOperation(HttpContext context, string amount)
       {
           var ammountValue = Convert.ToDouble(amount);
           var resultmodel = new PayPalResponseResultModel();
           if (!IsLimitExceeded(ammountValue, resultmodel))
           {
               var paymentRequestId = Guid.NewGuid().ToString();
               var hostingEndPoint = ConfigurationManager.AppSettings["HOSTING_ENDPOINT"];
               var webGoldUrlParams = ConfigurationManager.AppSettings["WEBRUNES_WEBGOLD_PARAMS"];
               var cancel = ConfigurationManager.AppSettings["WEBRUNES_WEBGOLD_CANCEL"];
               var redirectUrl = string.Concat(hostingEndPoint, "/PayPal/PayPalResponse");
               try
               {
                   var setExpressCheckoutRequestDetails = new SetExpressCheckoutRequestDetailsType();
                   setExpressCheckoutRequestDetails.ReturnURL = redirectUrl;
                   setExpressCheckoutRequestDetails.CancelURL = string.Concat(hostingEndPoint, webGoldUrlParams, cancel);
                   var paymentDetails = new PaymentDetailsType();
                   paymentDetails.PaymentAction = PaymentActionCodeType.SALE;
                   string orderDescription = "Payment to Webrunes";
                   if (orderDescription != "")
                   {
                       paymentDetails.OrderDescription = orderDescription;
                   }
                   paymentDetails.ItemTotal = new BasicAmountType(CurrencyCodeType.USD, ammountValue.ToString());
                   var orderTotal = new BasicAmountType(CurrencyCodeType.USD, ammountValue.ToString());
                   paymentDetails.OrderTotal = orderTotal;
                   setExpressCheckoutRequestDetails.BuyerEmail = _userEmail;
                   paymentDetails.PaymentRequestID = paymentRequestId;
                   paymentDetails.NotifyURL = redirectUrl;
                   setExpressCheckoutRequestDetails.PaymentDetails.Add(paymentDetails);
                   var setExpressCheckout = new SetExpressCheckoutReq();
                   var setExpressCheckoutRequest = new SetExpressCheckoutRequestType(setExpressCheckoutRequestDetails);
                   setExpressCheckout.SetExpressCheckoutRequest = setExpressCheckoutRequest;
                   var service = new PayPalAPIInterfaceServiceService();
                   SetExpressCheckoutResponseType responseType = service.SetExpressCheckout(setExpressCheckout);
                   if (responseType != null)
                   {
                       if (responseType.Ack.Equals(AckCodeType.SUCCESS))
                       {
                           setExpressCheckoutRequestDetails.Token = responseType.Token;
                           CreateTransaction(amount, responseType.Token);
                           var checkoutUrl = ConfigurationManager.AppSettings["PAYPAL_CHECKOUT_URL"];
                           resultmodel.Url = string.Concat(checkoutUrl,"&token=", responseType.Token);
                           resultmodel.IsSucces = true;
                       }
                       else
                       {
                           resultmodel.IsSucces = false;
                           List<ErrorType> errorMessages = responseType.Errors;
                           foreach (ErrorType error in errorMessages)
                           {
                               resultmodel.Errors += "1." + error.ShortMessage;
                               resultmodel.Errors += "; </br>";
                           }
                       }
                   }
               }   
               catch (Exception e)
               {
                   throw new Exception(e.Message);
               }
           }
           return resultmodel;
       }

       private void CreateTransaction(string amount,string token)
       {
           var entity = new Repository.Entity.PayPal
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = _userId,
                            State = StateType.PENDING.ToString(),
                            UpdateTime = DateTime.UtcNow,
                            CreateTime = DateTime.UtcNow,
                            Amount = Converter.ParseToDouble(amount),
                            CurrencyCode = "USD",
                            InternalPaymentId = token,
                            Intent = "SALE",
                            PayerEmail = _userEmail
                        };

           Repository.CreatePayPalTransaction(entity);
       }

       private bool IsLimitExceeded(double ammountValue,PayPalResponseResultModel resultmodel)
       {
           var sumAmountInLastDay = Repository.GetAmmountPayPalPorLastDay(_userId);
           var limitPorDayVal = ConfigurationManager.AppSettings["addFunds_limitPorDay"];
           var limitPorDay = Convert.ToInt32(limitPorDayVal);
           if ((sumAmountInLastDay + ammountValue) > limitPorDay)
           {
               resultmodel.IsSucces = false;
               return true;
           }
           return false;
       }
    }
}
