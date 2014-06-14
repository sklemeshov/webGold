using Payment.Business;

namespace payment.services
{
   public class CurrencyRateService
    {
       public static double GetRateGld()
       {
           return new CurrencyRate().GetRate();
       }
    }
}
