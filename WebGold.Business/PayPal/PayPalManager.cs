using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
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

       public PayPalResponseResultModel SendRequest(HttpContext context, string amount)
       {
           double cAmount = amount.ParseToDouble();
           var model = new PayPalResponseResultModel();
           var sumAmountInLastDay = Repository.GetAmmountPayPalPorLastDay(_userId);
           if ((sumAmountInLastDay + cAmount) > 1000)
           {
               model.IsSucces = false;
               return model;
           }
           var request = new SetExpressCheckoutRequestType();
           this.PopulateRequest(request, cAmount);
           var wrapper = new SetExpressCheckoutReq {SetExpressCheckoutRequest = request};
           var service = new PayPalAPIInterfaceServiceService();
           var setECResponse = service.SetExpressCheckout(wrapper);
           context.Items.Add("paymentDetails", request.SetExpressCheckoutRequestDetails.PaymentDetails);
           var keyResponseParameters = new Dictionary<string, string> {{"API Status", setECResponse.Ack.ToString()}};
           if (setECResponse.Ack.Equals(AckCodeType.FAILURE) ||
               (setECResponse.Errors != null && setECResponse.Errors.Count > 0))
           {
               context.Items.Add("Response_error", setECResponse.Errors);
               context.Items.Add("Response_redirectURL", null);
           }
           else
           {
               context.Items.Add("Response_error", null);
               keyResponseParameters.Add("EC token", setECResponse.Token);
               context.Items.Add("Response_redirectURL", string.Concat(
                   ConfigurationManager.AppSettings["PAYPAL_REDIRECT_URL"],
                   "_express-checkout&token=", setECResponse.Token));
           }
           context.Items.Add("Response_keyResponseObject", keyResponseParameters);
           context.Items.Add("Response_apiName", "SetExpressCheckout");
           context.Items.Add("Response_requestPayload", service.getLastRequest());
           context.Items.Add("Response_responsePayload", service.getLastResponse());
          
           if (setECResponse.Ack.Equals(AckCodeType.SUCCESS))
           {
               var entity = new Repository.Entity.PayPal
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    UserId = _userId,
                                    State = "pending",
                                    UpdateTime = DateTime.UtcNow,
                                    CreateTime = DateTime.UtcNow,
                                    Amount = Converter.ParseToDouble(amount),
                                    CurrencyCode = "USD",
                                    InternalPaymentId = setECResponse.Token,
                                    Intent = "SALE",
                                    PayerEmail = _userEmail
                                };

               Repository.CreatePayPalTransaction(entity);
             // https://www.sandbox.paypal.com/webscr?cmd=_express-checkout&token=
               var sandBoxUrl = ConfigurationManager.AppSettings["PAYPAL_SANDBOX_URL"];
               model.Url = string.Format("{0}{1}",sandBoxUrl,setECResponse.Token);
               model.IsSucces = true;
           }
           else
           {
               var errors = (List<ErrorType>)context.Items["Response_error"];
               var errorText = new StringBuilder();
               foreach (ErrorType errorType in errors)
               {
                   errorText.AppendFormat("{0} {1};", errorType.ErrorCode, errorType.ShortMessage);
               }
               model.Errors = errorText.ToString();
               model.IsSucces = false;
           }
           return model;
       }

       private void PopulateRequest(SetExpressCheckoutRequestType request, double itemTotal)
       {
           string rUrl = ConfigurationManager.AppSettings["HOSTING_ENDPOINT"];
           string returnUrl = string.Empty;
           string ipnNotificationUrl = string.Empty;
           string requestUrl = rUrl;
           var uriBuilder = new UriBuilder(requestUrl) {Path = "/PayPal/PayPalResponse"};
           returnUrl = uriBuilder.Uri.ToString();
           ipnNotificationUrl = uriBuilder.Uri.ToString();
           var setExpressCheckoutRequestDetails = new SetExpressCheckoutRequestDetailsType();
           if (!string.IsNullOrEmpty(returnUrl))
           {
               setExpressCheckoutRequestDetails.ReturnURL = returnUrl;
           }
           if (!string.IsNullOrEmpty(_userEmail))
           {
               setExpressCheckoutRequestDetails.BuyerEmail = _userEmail;
           }
           var paymentDetails = new PaymentDetailsType();
           setExpressCheckoutRequestDetails.PaymentDetails.Add(paymentDetails);
           double orderTotal = 0.0;
           var currency = (CurrencyCodeType)
               Enum.Parse(typeof(CurrencyCodeType), "USD");
           const string orderDescription = "Payment to webRunes";
           if (!string.IsNullOrEmpty(orderDescription))
           {
               paymentDetails.OrderDescription = orderDescription;
           }
           paymentDetails.PaymentAction = (PaymentActionCodeType)
               Enum.Parse(typeof(PaymentActionCodeType), "SALE");
           orderTotal += itemTotal;
           paymentDetails.ItemTotal = new BasicAmountType(currency, itemTotal.ToString());
           paymentDetails.OrderTotal = new BasicAmountType(currency, orderTotal.ToString());
           paymentDetails.NotifyURL = ipnNotificationUrl.Trim();
           request.SetExpressCheckoutRequestDetails = setExpressCheckoutRequestDetails;
       }

       public static string PayPalResponseResult(string token, string payerId)
       {
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
           requestDetails.PaymentAction = (PaymentActionCodeType)
               Enum.Parse(typeof(PaymentActionCodeType), "SALE");
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
               return "PayPal response fail !!!";
           }
           var amount = Converter.ParseToDouble(paymentInfoType.GrossAmount.value);
           var payPalEntity = payPal as Repository.Entity.PayPal;
           if (amount != payPalEntity.Amount)
           {
               return "<b>Not enough money in the account!</b>";
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
           bool needToDeposit = false;

           needToDeposit = paypalEntity.State.Equals("pending", StringComparison.InvariantCultureIgnoreCase) && isPaid;
           repository.UpdatePayPal(entity);
           if (needToDeposit)
           {
               Deposit(entity, repository);
           }
           return payPalStatus.ToString().ToLower();
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
           var accountBalanceModel = new AccountBalanceModel();
           accountBalanceModel.Usd = tModel.Amount;
           accountBalanceModel.Gs = (Int64) new GoldenStandartConverter().ConvertFromUsdToGld(tModel.Amount);
           return accountBalanceModel;
       }
    }
}
