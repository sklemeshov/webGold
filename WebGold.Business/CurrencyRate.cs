using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;

namespace webGold.Business
{
   public class CurrencyRate
   {
       private string _currencyPageUrl = ConfigurationManager.AppSettings["currencyPageUrl"];

       public double GetRate()
       {
           var request = WebRequest.Create(_currencyPageUrl);
           string text;
           var response = (HttpWebResponse)request.GetResponse();

           using (var sr = new StreamReader(response.GetResponseStream()))
           {
               text = sr.ReadToEnd();
           }
          
           var tmpArr = text.Split(new string[] { "<table" }, StringSplitOptions.None);
           var trText = tmpArr[1].Replace("</tr>", "");
           var tdArr = trText.Split(new string[] { "<tr>" }, StringSplitOptions.None);
           var dataArr = tdArr[2].Replace("</td>", "").Split(new string[] { "<td>" }, StringSplitOptions.None);
           var dataStr = dataArr[2].Replace("$", "").Replace("</a>", "").Split(new string[] {">"},
                                                                              StringSplitOptions.None)[1];
           if (dataStr.IndexOf(',') != -1)
           {
               dataStr = dataStr.Replace(',', '.');
           }
           var provider = new NumberFormatInfo();
           provider.NumberDecimalSeparator = ".";
           //provider.NumberGroupSeparator = ".";
           return Convert.ToDouble(dataStr, provider);

       }
    }
}
