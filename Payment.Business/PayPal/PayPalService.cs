using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using Payment.Business.Model;
using Payment.Repository;
using Payment.Repository.Entity;
using Profile.Repository.Entity;

namespace Payment.Business.PayPal
{
   public class PayPalService
    {
       private PaymentRepository _payPalRepository;
       public PaymentRepository PayPalRepository
        {
            get { return _payPalRepository ?? (_payPalRepository = new PaymentRepository()); }
        }

       private UserAccount _currentUser;

       public PayPalService(UserAccount user)
       {
           _currentUser = user;
       }

       public PayPalResponseResultModel SendRequest(HttpContextBase context, string amount)
       {
           double cAmount = Converter.ParseToDouble(amount);
           var model = new PayPalResponseResultModel();
           var sumAmountInLastDay = PayPalRepository.GetAmmountPayPalPorLastDay(_currentUser.Id);
           if ((sumAmountInLastDay + cAmount) > 1000)
           {
               model.IsSucces = false;
               return model;
           }
           var request = new SetExpressCheckoutRequestType();
           this.PopulateRequest(request, cAmount);
           var wrapper = new SetExpressCheckoutReq();
           wrapper.SetExpressCheckoutRequest = request;
           var service = new PayPalAPIInterfaceServiceService();
           var setECResponse = service.SetExpressCheckout(wrapper);
           context.Items.Add("paymentDetails", request.SetExpressCheckoutRequestDetails.PaymentDetails);
           var keyResponseParameters = new Dictionary<string, string>();
           keyResponseParameters.Add("API Status", setECResponse.Ack.ToString());
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
               context.Items.Add("Response_redirectURL", ConfigurationManager.AppSettings["PAYPAL_REDIRECT_URL"].ToString()
                   + "_express-checkout&token=" + setECResponse.Token);
           }
           context.Items.Add("Response_keyResponseObject", keyResponseParameters);
           context.Items.Add("Response_apiName", "SetExpressCheckout");
           context.Items.Add("Response_requestPayload", service.getLastRequest());
           context.Items.Add("Response_responsePayload", service.getLastResponse());
          
           if (setECResponse.Ack.Equals(AckCodeType.SUCCESS))
           {
               
               var entity = new Payment.Repository.Entity.PayPal
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    UserId = _currentUser.Id,
                                    State = "pending",//StateType.Complete.ToString(),
                                    UpdateTime = DateTime.UtcNow,
                                    CreateTime = DateTime.UtcNow,
                                    Amount = Converter.ParseToDouble(amount),
                                    CurrencyCode = "USD",
                                    InternalPaymentId = setECResponse.Token,
                                    Intent = "SALE",
                                    PayerEmail = _currentUser.Email
                                };

               PayPalRepository.CreatePayPalTransaction(entity);
               model.Url = string.Format("https://www.sandbox.paypal.com/webscr?cmd=_express-checkout&token={0}",
                   setECResponse.Token);
               model.IsSucces = true;
           }
           else
           {
               var errors = (List<ErrorType>)context.Items["Response_error"];
               StringBuilder errorText = new StringBuilder();
               foreach (ErrorType errorType in errors)
               {
                   errorText.AppendFormat("{0} {1};", errorType.ErrorCode, errorType.ShortMessage);
               }
               model.Errors = errorText.ToString();
               model.IsSucces = false;
           }
           //redirect to https://www.sandbox.paypal.com/webscr?cmd=_express-checkout&token=EC-3NX01529PT1747426
           //returned to http://localhost:3089/APICalls/GetExpressCheckoutDetails.aspx?token=EC-3NX01529PT1747426&PayerID=3DUT4Y6QZXA46
           return model;
       }

       private void PopulateRequest(SetExpressCheckoutRequestType request, double itemTotal)
       {
           string rUrl = ConfigurationManager.AppSettings["HOSTING_ENDPOINT"];
           string returnUrl = string.Empty;
           string cancelUrl = string.Empty;
           string ipnNotificationUrl = string.Empty;//"http://localhost:3929";//
           string requestUrl = rUrl;//ConfigurationManager.AppSettings["HOSTING_ENDPOINT"].ToString();
           UriBuilder uriBuilder = new UriBuilder(requestUrl);
           uriBuilder.Path = "/PayPal/PayPalResponse";
           returnUrl = uriBuilder.Uri.ToString();
           uriBuilder = new UriBuilder(requestUrl);
           uriBuilder.Path = "/Payment.html";
           cancelUrl = uriBuilder.Uri.ToString();
           uriBuilder = new UriBuilder(requestUrl);
           //TODO:!!!
           uriBuilder.Path = "/PayPal/PayPalResponse";
           ipnNotificationUrl = uriBuilder.Uri.ToString();
           var setExpressCheckoutRequestDetails = new SetExpressCheckoutRequestDetailsType();
           if (returnUrl != "")
           {
               setExpressCheckoutRequestDetails.ReturnURL = returnUrl;
           }
           if (cancelUrl != "")
           {
               setExpressCheckoutRequestDetails.CancelURL = cancelUrl;
           }
           var email = _currentUser.Email;
           //todo:!!!
           if (!string.IsNullOrEmpty(email))
           {
               setExpressCheckoutRequestDetails.BuyerEmail = email;
           }
           var paymentDetails = new PaymentDetailsType();
           setExpressCheckoutRequestDetails.PaymentDetails.Add(paymentDetails);
           double orderTotal = 0.0;
          
           var currency = (CurrencyCodeType)
               Enum.Parse(typeof(CurrencyCodeType), "USD");
           string orderDescription = "Payment to Webrunes";
           if (orderDescription != "")
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
           var request = new GetExpressCheckoutDetailsRequestType();
           request.Token = token;
           var wrapper = new GetExpressCheckoutDetailsReq();
           wrapper.GetExpressCheckoutDetailsRequest = request;
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
           var doCheckoutWrapper = new DoExpressCheckoutPaymentReq();
           doCheckoutWrapper.DoExpressCheckoutPaymentRequest = doExpressCheckoutRequest;
           DoExpressCheckoutPaymentResponseType doECResponse = service.DoExpressCheckoutPayment(doCheckoutWrapper);
           PaymentInfoType paymentInfoType = doECResponse.DoExpressCheckoutPaymentResponseDetails.PaymentInfo[0];
           bool isPaid = doECResponse.Ack.Equals(AckCodeType.SUCCESS);
           var payPalStatus = paymentInfoType.PaymentStatus;
           Payment.Repository.Entity.PayPal payPal = repository.GetPayPalBy(token);
           if (payPal == null)
           {
               return "PayPal response fail !!!";
           }
           var amount = Converter.ParseToDouble(paymentInfoType.GrossAmount.value);
           //var currencyCode = paymentInfoType.GrossAmount.currencyID;

           var payPalEntity = payPal as Payment.Repository.Entity.PayPal;
           if (amount != payPalEntity.Amount)
           {
               return "<b>Not enough money in the account!</b>";
           }
           var entity = new Payment.Repository.Entity.PayPal();
           entity.Id = payPalEntity.Id;
           entity.UpdateTime = DateTime.UtcNow;
           entity.State = doECResponse.Ack.Value.ToString().ToLower();
           entity.InternalPaymentId = payPalEntity.InternalPaymentId;
           entity.PayerId = payerId;
           entity.UserId = payPalEntity.UserId;
           entity.Amount = payPalEntity.Amount;
           entity.Fee = Converter.ParseToDouble(paymentInfoType.FeeAmount.value);
           entity.CurrencyCode = payPalEntity.CurrencyCode;

           Payment.Repository.Entity.PayPal paypalEntity = repository.GetPayPalBy(entity.InternalPaymentId);
           bool needToDeposit = false;

           needToDeposit = paypalEntity.State.Equals("pending", StringComparison.InvariantCultureIgnoreCase) && isPaid;

          // this.PayPalRepository["UpdatePayPal"].Invoke(dto);
           repository.UpdatePayPal(entity);
           if (needToDeposit)
           {
               Deposit(entity, repository);
           }
           return payPalStatus.ToString().ToLower();
       }

       private static void Deposit(Payment.Repository.Entity.PayPal entity, PaymentRepository repository)
         {
             if (!entity.CurrencyCode.Equals("USD", StringComparison.InvariantCultureIgnoreCase))
             {
                 throw new ApplicationException(string.Format("Unable to deposit. Unconfigured currency {0}.", entity.CurrencyCode));
             }
             var account = repository.GetAccountBy(entity.UserId);
             double amount = entity.Amount - entity.Fee;
             if(account == null)
             {
                 account = new Account();
                 account.Id = Guid.NewGuid().ToString();
                 account.UserId = entity.UserId;
                 account.GldAmount = 0;
                 account.UsdAmount = 0;
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
            account.GldAmount = new GoldenStandartService().ConvertFromUsdToGld(account.UsdAmount);
            repository.UpdateAccount(account);
             //var accountEntity = new Account();
             //if (account == null)
             //{
             //    accountEntity.Id = Guid.NewGuid().ToString();
             //    accountEntity.UserId = entity.UserId;
             //    accountEntity.GldAmount = 0;
             //    accountEntity.UsdAmount = 0;
             //    repository.CreateAccount(accountEntity);
             //}
             //else
             //{
             //    accountEntity.Id = account.Id;
             //}
            // accountEntity.GldAmount = account.GldAmount;
             //if (account.UsdAmount != 0)
             //{
             //    accountEntity.UsdAmount = account.UsdAmount + entity.Amount;
             //}
             //else
             //{
             //    accountEntity.UsdAmount = entity.Amount;
             //}
            // accountEntity.UserId = entity.UserId;
             var paymentHistoryEntity = new PaymentHistory();
             paymentHistoryEntity.Id = Guid.NewGuid().ToString();
             paymentHistoryEntity.PaymentType = ((int)PaymentType.PayPal).ToString();
             paymentHistoryEntity.PaymentMethod = ((int)PaymentMethod.Credit).ToString();
             paymentHistoryEntity.Date = DateTime.UtcNow;
            // double amount = entity.Amount - entity.Fee;
             //accountEntity.GldAmount = accountEntity.GldAmount + GoldenStandartService.ConvertFromUsdToGld(entity.Amount);
             paymentHistoryEntity.Amount = amount; 
             paymentHistoryEntity.Currency = "USD";
             paymentHistoryEntity.TransactionStatus = entity.State;
             //paymentHistoryEntity.ReceivedEmail
             //UpdateAccount(accountEntity.UsdAmount, account.UserId);

             paymentHistoryEntity.AccountId = account.Id;
             paymentHistoryEntity.UserId = entity.UserId;
             repository.CreatePaymentHistory(paymentHistoryEntity);
         }

       public static IList<PaymentHistory> GetHistoryCollectionBy(string userId)
       {
           return new PaymentRepository().GetPaymentHistoryBy(userId);
       }

       public static void PayPalWithdraw(Payment.Repository.Entity.PayPal entity, string emailTo)
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
           accountBalanceModel.Gs = (Int64) new GoldenStandartService().ConvertFromUsdToGld(tModel.Amount);
           return accountBalanceModel;
       }
    }
}
