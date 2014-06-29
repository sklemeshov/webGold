﻿using webGold.Business;

namespace WebGold.Services
{
   public static class GoldenStandartService
   {
       public static double GldToUsd(double amount)
       {
           return new GoldenStandartConverter().ConvertFromGldToUsd(amount);
       }

       public static double UsdToGld(double amount)
       {
           return new GoldenStandartConverter().ConvertFromUsdToGld(amount);
       }
    }
}
