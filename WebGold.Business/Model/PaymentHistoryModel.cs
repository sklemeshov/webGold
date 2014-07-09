using System;
using System.Globalization;
using System.Threading;
using webGold.Repository.Entity;

namespace webGold.Business.Model
{
   public class PaymentHistoryModel
    {
       public PaymentHistoryModel() { }
       public PaymentHistoryModel(Transaction entity) {
           Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
           var gsService = new GoldenStandartConverter();
           var usdAmount = Math.Round(entity.Amount, 2);
           Usd = usdAmount.ToString("N", CultureInfo.CreateSpecificCulture("en-US"));
           var gsAmount = Convert.ToInt64(gsService.ConvertFromUsdToGld(entity.Amount));
          
           var paymentMethod = Convert.ToInt16(entity.PaymentMethod);
           if (paymentMethod == (int)PaymentMethod.Credit)
           {
               Wrg = string.Format("+{0}", String.Format(CultureInfo.InvariantCulture, "{0:0 000}", gsAmount));
           }
           else
           {
               Wrg = string.Format("-{0}", String.Format(CultureInfo.InvariantCulture, "{0:0 000}", gsAmount));
           }
           Name = entity.TransactionType.ToString();
           Date = entity.CreationTime != null ? entity.CreationTime.ToString("ddd, MMMM dd, yyyy H:mm") : string.Empty;
           Status = entity.State.ToString();
           //Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
       }

       public string Direction { get; set; }
       public string Name { get; set; }
       public string Date { get; set; }
       public string Status { get; set; }
       public string Wrg { get; set; }
       public string Usd { get; set; }
    }
}
