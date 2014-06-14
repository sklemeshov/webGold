using Wr.API.DbLayer;
using Wr.API.DbLayer.Attributes;
using Wr.API.DbLayer.CassandraDb;

namespace paiment.core
{
    [DbObjectName("webrunes", "accounthistory")]
    public class AccountHistoryEntity : CassandraEntity<AccountHistoryEntity>
    {
        internal const string ID = "id";

        internal const string USER_ID = "userid";

        internal const string ACCOUNT_ID = "accountid";

        internal const string DATE = "date";

        internal const string AMOUNT = "amount";

        internal const string CURRENCY = "currency";

        internal const string PAYMENT_TYPE = "paymenttype";

        internal const string PAYMENT_METHOD = "paymentmethod";


        [ColumnTicket(ID, CassandraType.VARCHAR, false, IsPrimaryKey = true)]
        public string Id
        {
            get
            {
                return this.GetData(ID);
            }
            set
            {
                this.SetData(ID, value);
            }
        }

        [ColumnTicket(USER_ID, CassandraType.VARCHAR, true)]
        public string UserId
        {
            get
            {
                return this.GetData(USER_ID);
            }
            set
            {
                this.SetData(USER_ID, value);
            }
        }

        [ColumnTicket(ACCOUNT_ID, CassandraType.VARCHAR, true)]
        public string AccountId
        {
            get
            {
                return this.GetData(ACCOUNT_ID);
            }
            set
            {
                this.SetData(ACCOUNT_ID, value);
            }
        }

        [ColumnTicket(DATE, CassandraType.VARCHAR, true)]
        public string Date
        {
            get
            {
                return this.GetData(DATE);
            }
            set
            {
                this.SetData(DATE, value);
            }
        }

        [ColumnTicket(CURRENCY, CassandraType.VARCHAR, true)]
        public string Currency
        {
            get
            {
                return this.GetData(CURRENCY);
            }
            set
            {
                this.SetData(CURRENCY, value);
            }
        }

        [ColumnTicket(AMOUNT, CassandraType.DOUBLE, true)]
        public double Amount
        {
            get
            {
                return this.GetData(AMOUNT);
            }
            set
            {
                this.SetData(AMOUNT, value);
            }
        }


        [ColumnTicket(PAYMENT_TYPE, CassandraType.VARCHAR, true)]
        public string PaymentType
        {
            get
            {
                return this.GetData(PAYMENT_TYPE);
            }
            set
            {
                this.SetData(PAYMENT_TYPE, value);
            }
        }


        [ColumnTicket(PAYMENT_METHOD, CassandraType.VARCHAR, true)]
        public string PaymentMethod
        {
            get
            {
                return this.GetData(PAYMENT_METHOD);
            }
            set
            {
                this.SetData(PAYMENT_METHOD, value);
            }
        }
    }
}
