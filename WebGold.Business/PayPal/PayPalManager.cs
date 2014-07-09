using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.InteropServices;
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
           Transaction transactionEntity = repository.GetTransactionBy(payPal.Id);
           if (amount != transactionEntity.Amount)
           {
               model.Errors = "Not enough money in the account.";
               return model;
           }
           transactionEntity.UpdateTime = DateTime.UtcNow;
           transactionEntity.State = (int)TransactionState.Complete;
           transactionEntity.Fee = Converter.ParseToDouble(paymentInfoType.FeeAmount.value);        
          
           payPal.PayerId = payerId;
           payPal.State = (int)TransactionState.Complete;
           repository.UpdatePayPal(payPal);
           if (payPal.State.Equals(TransactionState.InProgress.ToString()))
           {
               Deposit(transactionEntity, repository);
           }
           model.IsSucces = true;
           return model;
       }

       public static void Deposit(Transaction entity, IPaymentRepository repository)
         {
             if (entity.Currency != (int)CurrencyType.USD)
               {
                   throw new ApplicationException(string.Format("Unable to deposit. Unconfigured currency {0}.", entity.Currency));
               }
               var account = repository.GetAccountBy(entity.UserId);
               entity.Amount = entity.Amount - (float)entity.Fee;
               var dgStandartService = new GoldenStandartConverter();
               var convertedWrg = dgStandartService.ConvertFromUsdToGld(entity.Amount);
               if(account == null)
               {
                   account = new Account
                             {
                                 Id = Guid.NewGuid().ToString(),
                                 UserId = entity.UserId
                             };
                   repository.CreateAccount(account);
               }
               if (account.Wrg != 0)
               {
                   account.Wrg = account.Wrg + (float)convertedWrg;
               }
               else
               {
                   account.Wrg = 0;
               }
             
               repository.UpdateAccount(account);
               repository.UpdateTransaction(entity);
         }

       public static IList<PaymentHistoryModel> GetHistoryCollectionBy(string userId)
       {
           var modelList = new List<PaymentHistoryModel>();
           var entityCollection = new PaymentRepository().GetPaymentHistoryBy(userId);
           foreach (var paymentHistory in entityCollection)
           {
               modelList.Add(new PaymentHistoryModel(paymentHistory)); 
           }
           return modelList;
       }
       public static IList<Transaction> HistoryCollectionBy(string userId)
       {
          return new PaymentRepository().GetPaymentHistoryBy(userId);
       }

       public static void PayPalWithdraw(Repository.Entity.PayPal entity, string emailTo)
       {          
           //var repository = new PaymentRepository();
           //double curAmount = entity.Amount;
           //var account = repository.GetAccountBy(entity.UserId);
           //account.UsdAmount = account.UsdAmount - curAmount;
           ////account.UsdAmount
           //repository.UpdateAccount(account);
           //var paymenthistory = new Transaction();
           //paymenthistory.Id = Guid.NewGuid().ToString();
           //paymenthistory.UserId = entity.UserId;
           //paymenthistory.Amount = curAmount;
           //paymenthistory.Date = DateTime.UtcNow;
           //paymenthistory.Currency = "USD";
           //paymenthistory.AccountId = account.Id;
           //paymenthistory.PaymentMethod = entity.PaymentMethod;
           //paymenthistory.PaymentType = ((int)PaymentType.Transfer).ToString();
           //paymenthistory.TransactionType = "Sent";
           //paymenthistory.TransactionStatus = "Pending";
           //paymenthistory.ReceivedEmail = emailTo;
           //repository.CreatePaymentHistory(paymenthistory);
           /**/
       }

       public static double GetAmmountSumInDayBy(string userId)
       {
           var repository = new PaymentRepository();
           return repository.GetAmmountPayPalPorLastDay(userId,(int)PaymentMethod.Debit);
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
           var payPalEntity = new Repository.Entity.PayPal
                        {
                            Id = Guid.NewGuid().ToString(),
                            Intent = "SALE",
                            InternalPaymentId = token,
                            PayerId = _userId,
                            State = (int)TransactionState.InProgress
                        };
           var amoundVal = Convert.ToDouble(amount);
           var wrg = new GoldenStandartConverter().ConvertFromUsdToGld(amoundVal);           
           var transactionEntity = new Transaction()
                                   {
                                       Id = Guid.NewGuid().ToString(),
                                       UserId = _userId,
                                       Amount = amoundVal,
                                       Currency = (int)CurrencyType.USD,
                                       Wrg = (float) wrg,
                                       CreationTime = DateTime.UtcNow,
                                       Fee = 0,
                                       PaymentMethod = (int)PaymentMethod.Debit,
                                       PaymentProviderId = payPalEntity.Id,
                                       PaymentType = (int)PaymentType.PayPal,
                                       ProviderName = PaymentType.PayPal.ToString(),
                                       State = (int)TransactionState.InProgress,
                                       TransactionType = (int)TransactionType.Sent,
                                       UpdateTime = DateTime.UtcNow,
                                   };

           Repository.CreatePayPalTransaction(payPalEntity, transactionEntity);
       }

       private bool IsLimitExceeded(double ammountValue,PayPalResponseResultModel resultmodel)
       {
           try
           {
               var sumAmountInLastDay = Repository.GetAmmountPayPalPorLastDay(_userId,(int)PaymentType.PayPal);
               var limitPorDayVal = ConfigurationManager.AppSettings["addFunds_limitPorDay"];
               var limitPorDay = Convert.ToInt32(limitPorDayVal);
               if ((sumAmountInLastDay + ammountValue) > limitPorDay)
               {
                   resultmodel.IsSucces = false;
                   return true;
               }
               return false;
           }
           catch (Exception e)
           {
               throw new Exception(e.Message);
           }
          
       }
    }
}
