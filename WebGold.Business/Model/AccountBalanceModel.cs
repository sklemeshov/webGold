using System;
using webGold.Business;
using webGold.Repository.Entity;

namespace webGold.Business.Model
{
   public class AccountBalanceModel
    {
       public AccountBalanceModel(){}
       public AccountBalanceModel(Account entity)
       {
           var gsService = new GoldenStandartConverter();
           Currency = gsService.GldPrice10000;
           if (entity != null)
           {
               Usd = entity.UsdAmount;
               Gs = Convert.ToInt64(gsService.ConvertFromUsdToGld(Usd));
           }
       }

      
       public Int64 Gs { get; set; }
      
       public double Usd { get; set; }

       public double Currency { get; set; }
    }
}
