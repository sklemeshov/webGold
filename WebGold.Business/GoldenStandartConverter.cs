using System;

namespace webGold.Business
{
    public class GoldenStandartConverter
    {
        public GoldenStandartConverter()
        {
            GldPrice10000 = new CurrencyRate().GetRate();
        }
        //toDO: to config
        public double GldPrice10000 { get; set; }

        private const string USD = "USD"; 

        public double ConvertFromUsdToGld(double amount)
        {
            return amount * 10000 / GldPrice10000;
        }

        public double ConvertFromGldToUsd(double amount)
        {
            return amount * GldPrice10000 / 10000;
        }

        public double ConvertFromBtcToGld(double amount, double nativeCents, string nativeCurrencyIso)
        {
            if (!nativeCurrencyIso.Equals(USD, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new ApplicationException(string.Format("Unable to convert. Unconfigurated currency {0}", nativeCurrencyIso));
            }
            return nativeCents * 10000 / GldPrice10000;
        }
    }
}