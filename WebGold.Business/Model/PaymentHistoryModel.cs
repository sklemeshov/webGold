using System;
using System.Globalization;
using System.Threading;
using webGold.Business.TypeConverter;
using webGold.Repository.Entity;

namespace webGold.Business.Model
{
   public class PaymentHistoryModel
    {
       public PaymentHistoryModel() { }
       public PaymentHistoryModel(Transaction entity) {
           Init(entity);
       }

       private void Init(Transaction entity)
       {
           var gsService = new GoldenStandartConverter();
           var usdAmount = gsService.ConvertFromGldToUsd(entity.Wrg);
           var _USD = Math.Round(usdAmount, 2);
           USDstr = AmountConverter.ToUSDAmountStr(_USD);
           var _WRG = Convert.ToInt64(entity.Wrg);
           WRGstr = AmountConverter.ToWRGAmountStr(_WRG);
           var paymentMethod = Convert.ToInt16(entity.PaymentMethod);
           WRGstr = string.Format(paymentMethod == (int)PaymentMethod.Credit ? "+{0}" : "-{0}", WRGstr);
           Date = entity.CreationTime != null ? entity.CreationTime.ToString("ddd, MMMM dd, yyyy H:mm") : string.Empty;
           Status = ConvertValue(new StatusConverter<StatusHelper>(new StatusHelper(entity.State)));
           var tHelperData = new TransactionNameHelper()
                             {
                                 Email = entity.Email,
                                 Type = (TransactionType)entity.TransactionType
                             };
           Name = ConvertValue(new TransactionNameConverter<TransactionNameHelper>(tHelperData));
           Direction = ConvertValue(new DirectionNameConverter<TransactionNameHelper>(tHelperData));
       }

       private string ConvertValue(ITypeConverter converter)
       {
          return converter.Convert();
       }

       public string Direction { get; set; }
       public string Name { get; set; }
       public string Date { get; set; }
       public string Status { get; set; }
       public string WRGstr { get; set; }
       public string USDstr { get; set; }
    }
}
