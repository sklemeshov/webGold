using Payment.Business.Model;
using Payment.Repository;
using Payment.Repository.Entity;

namespace Payment.Business
{
   public static class AccountManager
    {
       public static Account GetAccountBy(string userId)
       {
           return new PaymentRepository().GetAccountBy(userId);
       }

       public static AccountBalanceModel GetUserBalanceBy(string userId)
       {
           var accModel = GetAccountBy(userId);
           return new AccountBalanceModel(accModel);
       }
    }
}
