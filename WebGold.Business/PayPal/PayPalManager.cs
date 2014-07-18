using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using PayPal.PayPalAPIInterfaceService;
using PayPal.PayPalAPIInterfaceService.Model;
using webGold.Business.Model;
using webGold.Repository;
using webGold.Repository.Entity;
using ErrorType = PayPal.PayPalAPIInterfaceService.Model.ErrorType;

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
               model.IsSucces = false;
               return model;
           }
           if (isPaid)
           {
               transactionEntity.UpdateTime = DateTime.UtcNow;
               transactionEntity.State = (int)TransactionState.Complete;
               transactionEntity.Fee = Converter.ParseToDouble(paymentInfoType.FeeAmount.value);        
               payPal.PayerId = payerId;
               payPal.State = (int)TransactionState.Complete;
               repository.UpdatePayPal(payPal);
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
                   account.Wrg = account.Wrg + (float)convertedWrg;
             
               repository.UpdateAccount(account);
               repository.UpdateTransactionAmount(entity);
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

       public static WithdrawModel PayPalWithdraw(WithdrawModel model)
       {
           var repository = RepositoryHelper.Initialize();
           double amountUSD = model.USD;
          
           if (amountUSD > 100)
           {
               amountUSD = 100;
           }
           else
           {
               if (amountUSD < 5)
               {
                   model.IsTransferCanseled = true;
                   model.ErrorType = Model.ErrorType.minimumLimit.ToString();
                   return model;
               }
           }
           var ammountByLastDay = GetAmmountTransferPorLastDay(model.UserId);
           if (ammountByLastDay >= 100 - 5)
           {
               model.IsTransferCanseled = true;
               //add message!!!
               model.ErrorType = Model.ErrorType.dayLimit.ToString();
               //model.ErrorMessage = "";
               return model;
           }
           if (ammountByLastDay + amountUSD > 100)
           {
               model.IsTransferCanseled = true;
               model.ErrorType = webGold.Business.Model.ErrorType.dayLimit.ToString();
               //add message!!!
               return model;
           }

           var accountEntity = AccountManager.GetAccountBy(model.UserId);
           var gService = new GoldenStandartConverter();
           var withdrawWRGAmount = gService.ConvertFromUsdToGld(amountUSD);
           model.WRG = withdrawWRGAmount;
           model.WRGAmount = AmountConverter.ToWRGAmountStr(Convert.ToInt64(withdrawWRGAmount));
           if (accountEntity.Wrg < withdrawWRGAmount)
           {
               model.IsTransferCanseled = true;
               model.ErrorType = Model.ErrorType.haventMoney.ToString();
               return model;
           }
           if (string.IsNullOrEmpty(model.Email))
           {
               model.IsTransferCanseled = true;
               model.ErrorType = Model.ErrorType.incorrectEmail.ToString();
               return model;
           }
           var transferEntity = new Transfer()
                                {
                                    Id = Guid.NewGuid().ToString(),
                                    PayerId = model.UserId,
                                    RecipientId = model.RecipientId,
                                    State = (int)TransactionState.InProgress
                                };
           var transactionEntity = new Transaction()
           {
               Id = Guid.NewGuid().ToString(),
               UserId = model.UserId,
               Amount = amountUSD,
               Currency = (int)CurrencyType.USD,
               Wrg = (float)withdrawWRGAmount,
               CreationTime = DateTime.UtcNow,
               Fee = 0,
               PaymentMethod = (int)PaymentMethod.Credit,
               PaymentProviderId = transferEntity.Id,
               PaymentType = (int)PaymentType.Transfer,
               ProviderName = PaymentType.Transfer.ToString(),
               State = (int)TransactionState.InProgress,
               TransactionType = (int)TransactionType.Sent,
               UpdateTime = DateTime.UtcNow,
           };
           repository.CreateTransfer(transferEntity, transactionEntity);
           accountEntity.Wrg = accountEntity.Wrg - (float)withdrawWRGAmount;
           repository.UpdateAccount(accountEntity);
           return model;
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
           var accountBalanceModel = new AccountBalanceModel(tModel.Wrg);
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
