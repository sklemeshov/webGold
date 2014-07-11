using System;
using System.Linq;

namespace webGold.Business
{
   public static class AmountConverter
    {
        const char dot = '.';
        const char comma = ',';
      
       public static string ToUSDAmountStr(double amount)
       { 
           var str = amount.ToString();
           return AmountFormat(str);
       }
       public static string ToWRGAmountStr(Int64 amount)
       {
           var str = amount.ToString();
           return AmountFormat(str);
       }

       private static string AmountFormat(string str)
       {
           var formatedAmount = string.Empty;
           if (str.IndexOf(dot) != -1 || str.IndexOf(comma) != -1)
           {
               var tmpArr = str.Split(dot, comma);
               var lStr = Format3(tmpArr[0]);
               var rStr = Format3(tmpArr[1]);
               if (rStr.Equals("0"))
               {
                   formatedAmount = lStr;
               }
               else
               {
                   formatedAmount = string.Concat(lStr, dot, rStr);
               }
           }
           else
           {
               formatedAmount = Format3(str);
           }
           return formatedAmount;
       }
       private static string Format3(string numberStr)
       {
           if (numberStr.Equals("0"))
           {
               return numberStr;
           }
           string formatedStr = string.Empty;
           var tmpCounter = 0;
           for (int i = numberStr.Count() - 1; i > -1; i--)
           {
               var symbol = numberStr[i];
               formatedStr += symbol;
               tmpCounter++;
               if (tmpCounter == 3 && i != 0)
               {
                   formatedStr += ' ';
                   tmpCounter = 0;
               }
           }
           return Reverse(formatedStr);
       }

      private static string Reverse(string s)
       {
           char[] charArray = s.ToCharArray();
           Array.Reverse(charArray);
           return new string(charArray);
       }
    }
}
