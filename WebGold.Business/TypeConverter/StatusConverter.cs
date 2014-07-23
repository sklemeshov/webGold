namespace webGold.Business.TypeConverter
{
    internal class StatusConverter<T> : ITypeConverter where T : StatusHelper
    {
        private T _data;
       internal StatusConverter(T data)
       {
           _data = data;
       }
        public string Convert()
        {
            string result = string.Empty;
            switch (_data.State)
            {
                case TransactionState.InProgress:
                    result = "In Progress";
                    break;
                default:
                    result = _data.State.ToString();
                    break;
            }
            return result;
        }
    }
}
