using webGold.Business.Model;
using webGold.Repository;
using webGold.Repository.Entity;

namespace webGold.Business
{
   public static class AccountManager
    {
       public static Account GetAccountBy(string userId)
       {
           var account = RepositoryHelper.Initialize().GetAccountBy(userId);
           if (account == null)
           {
               account = new Account();
           }
           return account;
       }

       public static AccountBalanceModel GetUserBalanceBy(string userId)
       {
           var accModel = GetAccountBy(userId);
           return new AccountBalanceModel(accModel);
       }
    }
}
