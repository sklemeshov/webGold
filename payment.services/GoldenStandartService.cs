namespace payment.services
{
    using System;

    public class GoldenStandartService
    {
        //toDO: to config
        private const double GldPrice10000 = 43;

        private const string USD = "USD"; 

        public static double ConvertFromUsdToGld(double amount)
        {
            return amount * 10000 / GldPrice10000;
        }

        public static double ConvertFromGldToUsd(double amount)
        {
            return amount * GldPrice10000 / 10000;
        }

        public static double ConvertFromBtcToGld(double amount, double nativeCents, string nativeCurrencyIso)
        {
            if (!nativeCurrencyIso.Equals(USD, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ApplicationException(string.Format("Unable to convert. Unconfigurated currency {0}", nativeCurrencyIso));
            }
            return nativeCents * 10000 / GldPrice10000;
        }

        public static double ConvertFromGldToBtc(double amount)
        {
            //need to call bitcoin service to get currency rates
            throw new NotImplementedException();
        }
    }
}