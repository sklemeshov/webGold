using webGold.Business.Model;
using webGold.Repository;
using webGold.Repository.Entity;

namespace webGold.Business
{
   public static class AccountManager
    {
       public static Account GetAccountBy(string userId)
       {
           return RepositoryHelper.Initialize().GetAccountBy(userId);
       }

       public static AccountBalanceModel GetUserBalanceBy(string userId)
       {
           var accModel = GetAccountBy(userId);
           return new AccountBalanceModel(accModel);
       }
    }
}
