namespace webGold.Business.TypeConverter
{
    internal class TransactionNameConverter<T> : ITypeConverter where T : TransactionNameHelper
    {
        private T _data;
        internal TransactionNameConverter(T data)
        {
            _data = data;
        }
        public string Convert()
        {
            string result = string.Empty;
            if (!string.IsNullOrEmpty(_data.Email))
            {
                result = string.Format("{0} {1}", _data.Type.ToString(), _data.Email);
            }
            else
            {
                result = _data.Type.ToString();
            }
            return result;
        }
    }
}
