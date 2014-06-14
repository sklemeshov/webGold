using System;
using System.Collections.Generic;
using System.Text;
using BLToolkit.Mapping;
using Payment.Repository.Entity;
using Payment.Repository.MySqlDataProvider;
 
namespace Payment.Repository
{
   public class PaymentRepository
    {
       private const string conn = "Data Source=54.235.73.25;Port=3306;Database=dev_wrio;uid=dev;pwd=164103148;";

       public IList<PaymentHistory> GetPaymentHistoryBy(string userId)
       {
           IList<PaymentHistory> pHistoryCollection;
           using (var context = new MySqlDbManager(conn))
            {
                pHistoryCollection = context.SetCommand(
                    string.Format("SELECT * FROM PaymentHistory WHERE UserId = '{0}' ORDER BY Date DESC", userId))
                    .ExecuteList<PaymentHistory>();
            }
           return pHistoryCollection;
       }

       public double GetAmmountPayPalPorLastDay(string userId)
       {
           double ammountPorLastDay = 0;
           using (var context = new MySqlDbManager(conn))
           {
               var dt =
                   context.SetCommand(
                       string.Format(
                           "SELECT sum(Amount) FROM PaymentHistory WHERE UserId = '{0}' and PaymentType = 0 and Date >= ( CURDATE() - INTERVAL 3 DAY )",
                           userId)).ExecuteDataTable();
               if (dt != null)
               {
                   if(dt.Rows.Count != 0)
                   {
                       ammountPorLastDay = dt.Rows[0].ItemArray[0] is double ? (double)dt.Rows[0].ItemArray[0] : 0;
                   }
               }
           }
           return ammountPorLastDay;
       }

       public double GetAmmountTransferPorLastDay(string userId)
       {
           double ammountPorLastDay = 0;
           using (var context = new MySqlDbManager(conn))
           {
               var dt =
                   context.SetCommand(
                       string.Format(
                           "SELECT sum(Amount) FROM PaymentHistory WHERE UserId = '{0}' and PaymentType = 2 and Date >= ( CURDATE() - INTERVAL 3 DAY )",
                           userId)).ExecuteDataTable();
               if (dt != null)
               {
                   if (dt.Rows.Count != 0)
                   {

                       ammountPorLastDay = dt.Rows[0].ItemArray[0] is double ? (double) dt.Rows[0].ItemArray[0] : 0;
                   }
               }
           }
           return ammountPorLastDay;
       } 

       public void DeletePaymentHistoryBy(IList<string> paymentHistoryIdCollection)
       {
           var entity = new PaymentHistoryEntity();
           entity.DeleteBy(paymentHistoryIdCollection);
       }

       public void UpdatePaymenStatus(string userId,string transactionStatus)
       {
           var query = string.Format("UPDATE PaymentHistory SET TransactionStatus = '{0}' WHERE UserId = '{1}'",
               transactionStatus, userId);

           using (var db = new MySqlDbManager(conn))
           {
               db.SetCommand(query).ExecuteNonQuery();
           }
       }

       public void CreatePaymentHistory(PaymentHistory entity)
       {
           using (var db = new MySqlDbManager(conn))
           {
               db.SetCommand(@"INSERT INTO PaymentHistory 
                              (Id,UserId,AccountId,Date,Currency,Amount,PaymentType,PaymentMethod,TransactionType,TransactionStatus,ReceivedEmail)
                    VALUES(@Id,@UserId,@AccountId,@Date,@Currency,@Amount,@PaymentType,@PaymentMethod,@TransactionType,@TransactionStatus,@ReceivedEmail)"
                   , db.CreateParameters(entity)).ExecuteNonQuery();
           }
       }

       public Account GetAccountBy(string userId)
       {
           Account account;
           using (var context = new MySqlDbManager(conn))
           {
               account =
                   context.SetCommand(
                   string.Format("SELECT * FROM Account WHERE UserId = '{0}'", userId)).ExecuteObject<Account>();
           }
           return account;
       }

       public void CreateAccount(Account entity)
       {
           using (var db = new MySqlDbManager(conn))
           {
                 db.SetCommand(@"
                    INSERT INTO Account ( Id,  UserId,  GldAmount, UsdAmount)
                    VALUES( @Id,  @UserId,  @GldAmount, @UsdAmount)", db.CreateParameters(entity)).ExecuteNonQuery();
           }
       }

       public void UpdateAccount(Account entity)
       {
           using (var db = new MySqlDbManager(conn))
           {

               db.SetCommand("UPDATE Account SET UsdAmount = @UsdAmount,GldAmount = @GldAmount  WHERE UserId = @UserId", db.CreateParameters(entity)).
                   ExecuteNonQuery();
           }
       }

       public void UpdateAccount(double usdAmount, string userId)
       {
           using (var db = new MySqlDbManager(conn))
           {

               db.SetCommand(
                   string.Format("UPDATE Account SET UsdAmount = {0}  WHERE UserId = '{1}'", usdAmount, userId)).
                   ExecuteNonQuery();
           }
       }

       public void CreatePayPalTransaction(PayPal entity)
       {
           using (var db = new MySqlDbManager(conn))
           {
              db.SetCommand(@"
                    INSERT INTO PayPal ( 
                    Id, UserId, UserIp,InternalPaymentId,CreateTime,
                    UpdateTime,PayPalId,Intent,PayerId,State,PayerEmail,PayerFirstName,PayerLastName,
                    PayerPhone,PayerAddress,PaymentMethod,Amount,Fee,CurrencyCode,ReceiverInfoType)
                    VALUES (@Id, @UserId, @UserIp,@InternalPaymentId,@CreateTime,
                    @UpdateTime,@PayPalId,@Intent,@PayerId,@State,@PayerEmail,@PayerFirstName,@PayerLastName,
                    @PayerPhone,@PayerAddress,@PaymentMethod,@Amount,@Fee,@CurrencyCode,@ReceiverInfoType)",
                    db.Parameter("@Id", entity.Id),
                    db.Parameter("@UserId", entity.UserId),
                    db.Parameter("@UserIp", entity.UserIp),
                    db.Parameter("@InternalPaymentId", entity.InternalPaymentId),
                    db.Parameter("@CreateTime", entity.CreateTime),
                    db.Parameter("@UpdateTime", entity.UpdateTime),
                    db.Parameter("@PayPalId", entity.PayPalId),
                    db.Parameter("@Intent", entity.Intent),
                    db.Parameter("@PayerId", entity.PayerId),
                    db.Parameter("@State", entity.State),
                    db.Parameter("@PayerEmail", entity.PayerEmail),
                    db.Parameter("@PayerFirstName", entity.PayerFirstName),
                    db.Parameter("@PayerLastName", entity.PayerLastName),
                    db.Parameter("@PayerPhone", entity.PayerPhone),
                    db.Parameter("@PayerAddress", entity.PayerAddress),
                    db.Parameter("@PaymentMethod", entity.PaymentMethod),
                    db.Parameter("@Amount", entity.Amount),
                    db.Parameter("@Fee", entity.Fee),
                    db.Parameter("@CurrencyCode", entity.CurrencyCode),
                    db.Parameter("@ReceiverInfoType", entity.ReceiverInfoType))
                .ExecuteNonQuery();
           }
       }

       public bool UpdatePayPal(PayPal payPal)
       {
           var payPalEntity = GetPayPalBy(payPal.InternalPaymentId);
           if (payPalEntity != null)
           {
               //payPal.Id = payPalEntity.Id;
               UpdatePayPalTransaction(payPal);
               return true;
           }
           return false;
       }

       private void UpdatePayPalTransaction(PayPal payPal)
       {
           using (var db = new MySqlDbManager(conn))
           {
               db.SetCommand(@"UPDATE PayPal SET
                         UserId = @UserId,
                         UserIp = @UserIp,
                         InternalPaymentId = @InternalPaymentId,
                         CreateTime = @CreateTime,
                         UpdateTime = @UpdateTime,
                         PayPalId = @PayPalId,
                         Intent = @Intent,
                         PayerId = @PayerId,
                         State = @State,
                         PayerEmail = @PayerEmail,
                         PayerFirstName = @PayerFirstName,
                         PayerLastName = @PayerLastName,
                         PayerPhone = @PayerPhone,
                         PayerAddress = @PayerAddress,
                         PaymentMethod = @PaymentMethod,
                         Amount = @Amount,
                         Fee = @Fee,
                         CurrencyCode = @CurrencyCode, 
                         ReceiverInfoType = @ReceiverInfoType                        
                         WHERE Id = @Id",
                    db.CreateParameters(payPal))
                .ExecuteNonQuery();
           }
       }

       public PayPal GetPayPalBy(string token)
       {
           PayPal payPal;
           using (var context = new MySqlDbManager(conn))
           {
               payPal =
                   context.SetCommand(
                   string.Format("SELECT * FROM PayPal WHERE InternalPaymentId = '{0}'", token)).ExecuteObject<PayPal>();
           }
           return payPal;
       }

       public void DeletPayPalTransactionBy(string id)
       {
           
       }
      
       public PaymentHistory GetLastTransaction(string userId)
       {
           //SELECT * FROM `dev_wrio`.`PaymentHistory` WHERE Id = 'userId' ORDER BY `PaymentHistory`.`Date` DESC LIMIT 1 
           var queryBuilder = new StringBuilder("SELECT * FROM `dev_wrio`.`PaymentHistory` ");
           queryBuilder.AppendFormat("WHERE UserId = '{0}' ORDER BY `PaymentHistory`.`Date` DESC LIMIT 1", userId);
           PaymentHistory paymentHistory;
           using (var context = new MySqlDbManager(conn))
           {
               try
               {
                   paymentHistory =
                   context.SetCommand(queryBuilder.ToString()).ExecuteObject<PaymentHistory>();
               }catch(Exception e)
               {
                   throw new Exception(e.Message);
               }
               
           }
           return paymentHistory;
       }
    }
}
