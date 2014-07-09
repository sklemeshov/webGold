using System;
using System.Web;
using webGold.Business.Model;
using webGold.Business.PayPal;
using webGold.Repository;
using webGold.Repository.Entity;

namespace webGold.Services
{
   public static class PayPalService
    {
       public static PayPalResponseResultModel SendRequest(HttpContextBase context, string amount, string userId,
           string email)
       {
           return SendRequest(context.ApplicationInstance.Context, amount, userId, email);
       }

       public static PayPalResponseResultModel SendRequest(HttpContext context, string amount, string userId,
           string email)
       {
           var manager = new PayPalManager(userId, email);
           return manager.SetExpressCheckoutOperation(context, amount);
       }

       public static PayPalResponseResultModel ResponseResult(string token, string payerId)
       {
           return PayPalManager.PayPalResponseResult(token, payerId);
       }

       //Todo: Email send !!!
       public static void Withdraw(webGold.Repository.Entity.PayPal entity, string emailTo)
       {
           PayPalManager.PayPalWithdraw(entity, emailTo);
       }

       public static AccountBalanceModel GetLastTransaction(string userId)
       {
           return PayPalManager.GetLastTransaction(userId);
       }
    }
}
