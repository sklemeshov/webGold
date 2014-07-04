using System;
using System.Globalization;
using System.Threading;
using webGold.Repository.Entity;

namespace webGold.Business.Model
{
   public class AccountBalanceModel
    {
       public AccountBalanceModel(){}
       public AccountBalanceModel(Account entity)
       {
           AmountToStrFormat(entity.UsdAmount);
       }

       public AccountBalanceModel(double usdAmount)
       {
           AmountToStrFormat(usdAmount);
       }

       private void AmountToStrFormat(double usdAmount)
       {
           Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
           var gsService = new GoldenStandartConverter();
           Currency = gsService.GldPrice10000;
           CurrencyStr = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", Currency);

           Usd = Math.Round(usdAmount, 2);
           UsdStr = String.Format(CultureInfo.InvariantCulture, "{0:0 000.00}", Usd);
           Gs = Convert.ToInt64(gsService.ConvertFromUsdToGld(Usd));
           GsStr = String.Format(CultureInfo.InvariantCulture, "{0:0 000}", Convert.ToInt64(gsService.ConvertFromUsdToGld(Usd)));
           Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
       }


       public Int64 Gs { get; set; }

       public string GsStr { get; set; }
      
       public double Usd { get; set; }

       public string UsdStr { get; set; }

       public double Currency { get; set; }

       public string CurrencyStr { get; set; }
    }
}
