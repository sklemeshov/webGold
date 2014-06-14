using System;
using Payment.Repository.Entity;

namespace Payment.Business.Model
{
   public class AccountBalanceModel
    {
       public AccountBalanceModel(){}
       public AccountBalanceModel(Account entity)
       {
           var gsService = new GoldenStandartService();
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
