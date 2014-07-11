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
           AmountToStrFormat(entity.Wrg);
       }

       public AccountBalanceModel(double wrg)
       {
           AmountToStrFormat(wrg);
       }

       private void AmountToStrFormat(double wrg)
       {
           var gsService = new GoldenStandartConverter();
           Currency = gsService.GldPrice10000;    
           CurrencyStr = String.Format(CultureInfo.InvariantCulture, "{0:0.00}", Currency);
           if (wrg > 0)
           {                                      
               var usdAmount = gsService.ConvertFromGldToUsd(wrg);
               Usd = Math.Round(usdAmount, 2);
               UsdStr = AmountConverter.ToUSDAmountStr(Usd);
               Wrg = Convert.ToInt64(wrg);
               WrgStr = AmountConverter.ToWRGAmountStr(Wrg);
           }
           else
           {
               Usd = 0;
               UsdStr = "0.00";
               Wrg = 0;
               WrgStr = "0";
           }
       }


       public Int64 Wrg { get; set; }

       public string WrgStr { get; set; }
      
       public double Usd { get; set; }

       public string UsdStr { get; set; }

       public double Currency { get; set; }

       public string CurrencyStr { get; set; }
    }
}
