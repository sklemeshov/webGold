using System;

namespace webGold.Business.TypeConverter
{
   internal class DirectionNameConverter<T> : ITypeConverter where T : TransactionNameHelper
   {
       private T _data;
       internal DirectionNameConverter(T data)
       {
           _data = data;
       }
       public string Convert()
       {
           var result = string.Empty;
           switch (_data.Type)
           {
               case TransactionType.Purchase:
                   result = "down";
                   break;
               case TransactionType.Sent:
                   result = "right";
                   break;
               case TransactionType.Withdraw:
                   result = "up";
                   break;
               case TransactionType.Received:
                   result = "left";
                   break;
           }
           return result;
       }
    }
}
