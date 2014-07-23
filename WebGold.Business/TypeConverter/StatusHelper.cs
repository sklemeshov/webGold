namespace webGold.Business.TypeConverter
{
   internal class StatusHelper
    {
       internal TransactionState State { get; set; }

       internal StatusHelper(int value)
       {
           State = (TransactionState) value;
       }
    }
}
