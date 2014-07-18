using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using webGold.Repository.Entity;
using webGold.Repository.MySqlDataProvider;

namespace webGold.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
       private string conn;
       public PaymentRepository()
       {
           conn = ConfigurationManager.ConnectionStrings["MySqlWrioProfile"].ConnectionString;
       }
       [Obsolete("Only for tests")]
       public PaymentRepository(string connectionString)
       {
           conn = connectionString;
       }
       public static IPaymentRepository GetInstance()
       {
           return new PaymentRepository();
       }
       public IList<Transaction> GetPaymentHistoryBy(string userId)
       {
           const string providerName = "PayPal;";
           IList<Transaction> pHistoryCollection;
           using (var context = new MySqlDbManager(conn))
            {
                var queryBuilder = new StringBuilder("SELECT * FROM dev_wrio.Transaction AS tr ");
                queryBuilder.Append("INNER JOIN dev_wrio.PayPal AS pp ON tr.PaymentProviderId = pp.Id ");
                queryBuilder.Append("INNER JOIN dev_wrio.UserAccount AS ua ON tr.UserId = ua.Id ");
                queryBuilder.AppendFormat(" WHERE UserId = '{0}' AND tr.ProviderName = '{1}';", userId, providerName);
                pHistoryCollection = context.SetCommand(queryBuilder.ToString()).ExecuteList<Transaction>();
            }
           return pHistoryCollection;
       }
       public double GetAmmountPayPalPorLastDay(string userId, int paymentMethod)
       {
           double ammountPorLastDay = 0;
           using (var context = new MySqlDbManager(conn))
           {
               var dt =
                   context.SetCommand(
                       string.Format(
                           "SELECT sum(Amount) FROM Transaction WHERE UserId = '{0}' and PaymentMethod = {1} and UpdateTime >= ( CURDATE() - INTERVAL 1 DAY )",
                           userId, paymentMethod)).ExecuteDataTable();
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
               try
               {
                   var dt =
                       context.SetCommand(
                           string.Format(
                               "SELECT sum(Amount) FROM Transaction WHERE UserId = '{0}' and PaymentType = 3 and UpdateTime >= ( CURDATE() - INTERVAL 1 DAY )",
                               userId)).ExecuteDataTable();
                   if (dt != null)
                   {
                       if (dt.Rows.Count != 0)
                       {

                           ammountPorLastDay = dt.Rows[0].ItemArray[0] is double ? (double) dt.Rows[0].ItemArray[0] : 0;
                       }
                   }
               }
               catch (Exception e)
               {
                   throw new Exception(e.Message);
               }
               
           }
           return ammountPorLastDay;
       } 
       public void DeletePaymentHistoryBy(IList<string> paymentHistoryIdCollection)
       {
           var queryBuilder = new StringBuilder();
           queryBuilder.Append(" DELETE FROM Transaction WHERE id IN ( ");
           string separator = string.Empty;
           foreach (var id in paymentHistoryIdCollection)
           {
               queryBuilder.AppendFormat("{0}'{1}' ", separator, id);
           }
           queryBuilder.Append(" )");
          
       }
       public void UpdatePaymenStatus(string userId, int transactionState, int payPalState)
       {
           var queryBuilder = new StringBuilder("START TRANSACTION;");
           queryBuilder.AppendFormat("UPDATE Transaction SET State = {0} WHERE UserId = '{1}';", transactionState, userId);
           queryBuilder.AppendFormat("UPDATE PayPal SET State = {0} WHERE UserId = '{1}';", payPalState, userId);
           queryBuilder.Append("COMMIT;");
           using (var db = new MySqlDbManager(conn))
           {
               db.SetCommand(queryBuilder.ToString()).ExecuteNonQuery();
           }
       }
       public void CreatePaymentHistory(Transaction entity)
       {
           using (var db = new MySqlDbManager(conn))
           {
               db.SetCommand(@"INSERT INTO Transaction 
                              (Id,UserId,CreationTime,UpdateTime,Amount,Currency,Fee,Wrg,PaymentProviderId,ProviderName,State,PaymentMethod,TransactionType,PaymentType)
                    VALUES(@Id,@UserId,@CreationTime,@UpdateTime,@Amount,@Currency,@Fee,@Wrg,@PaymentProviderId,@ProviderName,@State,@PaymentMethod,@TransactionType,@PaymentType)"
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
                    INSERT INTO Account ( Id,  UserId,  Wrg)
                    VALUES( @Id,  @UserId,  @Wrg)", db.CreateParameters(entity)).ExecuteNonQuery();
           }
       }
       public void UpdateAccount(Account entity)
       {
           using (var db = new MySqlDbManager(conn))
           {

               db.SetCommand("UPDATE Account SET Wrg = @Wrg  WHERE UserId = @UserId", db.CreateParameters(entity)).
                   ExecuteNonQuery();
           }
       }
       public void UpdateAccount(double wrgAmount, string userId)
       {
           using (var db = new MySqlDbManager(conn))
           {

               db.SetCommand(
                   string.Format("UPDATE Account SET Wrg = {0}  WHERE UserId = '{1}'", wrgAmount, userId)).
                   ExecuteNonQuery();
           }
       }
       public void CreatePayPalTransaction(PayPal payPalEntity, Transaction transactionEntity)
       {
           using (var db = new MySqlDbManager(conn))
           {
              
               db.SetCommand(@"INSERT INTO `dev_wrio`.`PayPal`(`Id`,`InternalPaymentId`,`Intent`,`PayerId`,`State`)
                               VALUES (@Id,@InternalPaymentId,@Intent,@PayerId,@State);",
                   db.CreateParameters(payPalEntity)).ExecuteNonQuery();
               db.SetCommand(@"INSERT INTO `dev_wrio`.`Transaction` 
                              (`Id`,`UserId`,`CreationTime`,`UpdateTime`,`Amount`,`Currency`,`Fee`,`Wrg`,`PaymentProviderId`,`ProviderName`,`State`,`PaymentMethod`,`TransactionType`,`PaymentType`)
                    VALUES(@Id,@UserId,@CreationTime,@UpdateTime,@Amount,@Currency,@Fee,@Wrg,@PaymentProviderId,@ProviderName,@State,@PaymentMethod,@TransactionType,@PaymentType)"
                   , db.CreateParameters(transactionEntity)).ExecuteNonQuery();              
               
           }
       }
       public void UpdateTransactionAmount(Transaction transaction)
        {
            using (var db = new MySqlDbManager(conn))
            {
                try
                {
                    db.SetCommand(string.Format("UPDATE Transaction SET Amount = @Amount, State=@State, Fee=@Fee  WHERE Id =@Id;"), db.CreateParameters(transaction))
              .ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    
                }
               
            }
        }
       public bool UpdatePayPal(PayPal payPal)
       {
           var payPalEntity = GetPayPalBy(payPal.InternalPaymentId);
           if (payPalEntity != null)
           {
               UpdatePayPalTransaction(payPal);
               return true;
           }
           return false;
       }
       public void UpdatePayPalTransaction(PayPal payPal)
       {
           using (var db = new MySqlDbManager(conn))
           {
               db.SetCommand(@"UPDATE PayPal SET                               
                               InternalPaymentId=@InternalPaymentId,
                               Intent=@Intent,
                               PayerId=@PayerId,
                               State=@State                
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
           var queryBuilder = new StringBuilder("START TRANSACTION;");
           queryBuilder.AppendFormat("DELETE FROM PayPal WHERE Id = '{0}';", id);
           queryBuilder.AppendFormat("DELETE FROM Transaction WHERE PaymentProviderId = '{0}';", id);
           queryBuilder.Append("COMMIT;");
          
           using (var db = new MySqlDbManager(conn))
           {
               db.SetCommand(queryBuilder.ToString()).ExecuteNonQuery();
           }
       }
       public Transaction GetLastTransaction(string userId)
       {
           var queryBuilder = new StringBuilder("SELECT * FROM `dev_wrio`.`Transaction` ");
           queryBuilder.AppendFormat("WHERE UserId = '{0}' ORDER BY `Transaction`.`UpdateTime` DESC LIMIT 1", userId);
           Transaction paymentHistory;
           using (var context = new MySqlDbManager(conn))
           {
               try
               {
                   paymentHistory =
                   context.SetCommand(queryBuilder.ToString()).ExecuteObject<Transaction>();
               }catch(Exception e)
               {
                   throw new Exception(e.Message);
               }
               
           }
           return paymentHistory;
       }
        public Transaction GetTransactionBy(string payPalId)
        {
            Transaction trEntity = null;
            using (var db = new MySqlDbManager(conn))
            {
                try
                {
                    trEntity = db.SetCommand(string.Format("SELECT * FROM Transaction WHERE PaymentProviderId ='{0}'",
                        payPalId)).ExecuteObject<Transaction>();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            return trEntity;
        }
        public void CreateTransfer(Transfer transferEntity,Transaction transactionEntity)
        {
            using (var db = new MySqlDbManager(conn))
            {
                try
                {
                    db.SetCommand(@"INSERT INTO Transfer(Id,PayerId,RecipientId,State)
                               VALUES (@Id,@PayerId,@RecipientId,@State);",
                   db.CreateParameters(transferEntity)).ExecuteNonQuery();
                    db.SetCommand(@"INSERT INTO Transaction 
                              (Id,UserId,CreationTime,UpdateTime,Amount,Currency,Fee,Wrg,PaymentProviderId,ProviderName,State,PaymentMethod,TransactionType,PaymentType)
                    VALUES(@Id,@UserId,@CreationTime,@UpdateTime,@Amount,@Currency,@Fee,@Wrg,@PaymentProviderId,@ProviderName,@State,@PaymentMethod,@TransactionType,@PaymentType)"
                   , db.CreateParameters(transactionEntity)).ExecuteNonQuery();    
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                
            }
        }
        public void UpdateTransferStatus(Transfer entity)
        {
            using (var db = new MySqlDbManager(conn))
            {
                try
                {
                    db.SetCommand("UPDATE Transfer SET State = @State WHERE Id = @Id;", db.CreateParameters(entity))
                    .ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
        public void DeleteTransferBy(string id)
        {
            using (var db = new MySqlDbManager(conn))
            {
                try
                {
                    db.SetCommand(string.Format("DELETE FROM Transfer WHERE Id = '{0}';", id))
                    .ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
