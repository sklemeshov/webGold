using System.Collections.Generic;
using webGold.Business;
using webGold.Business.Model;
using webGold.Business.PayPal;
using webGold.Repository.Entity;

namespace webGold.Services
{
   public class AccountBalanceService
    {
       public static Account GetAccount(string userId )
       {
           return AccountManager.GetAccountBy(userId);
       }

       public static AccountBalanceModel GetUserBalance(string userId)
       {
           return AccountManager.GetUserBalanceBy(userId);
       }

       public static double SummInDay(string userId)
       {
           return PayPalManager.GetAmmountSumInDayBy(userId);
       }

       public static double TransferPorLastDay(string userId)
       {
           return PayPalManager.GetAmmountTransferPorLastDay(userId);
       }

       public static AccountBalanceModel LastTransaction(string userId)
       {
           return PayPalManager.GetLastTransaction(userId);
       }

       public static IList<PaymentHistoryModel> PaymentHistoryCollection(string userId)
       {
           return PayPalManager.GetHistoryCollectionBy(userId);
       }
    }
}
