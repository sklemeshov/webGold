namespace payment.internaltransfer
{
    using Wr.API.DbLayer;
    using Wr.API.DbLayer.Attributes;
    using Wr.API.DbLayer.CassandraDb;

    [DbObjectName("webrunes", "transfer")]
    public class TransferEntity : CassandraEntity<TransferEntity>
    {
        internal const string ID = "id";

        internal const string USER_ID_FROM = "useridfrom";

        internal const string USER_ID_TO = "useridto";

        internal const string CREATE_TIME = "createtime";

        internal const string UPDATE_TIME = "updatetime";

        internal const string AMOUNT = "amount";

        internal const string CURRENCY = "currency";

        internal const string MESSAGE = "message";


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

        [ColumnTicket(USER_ID_FROM, CassandraType.VARCHAR, true)]
        public string UserIdFrom
        {
            get
            {
                return this.GetData(USER_ID_FROM);
            }
            set
            {
                this.SetData(USER_ID_FROM, value);
            }
        }

        [ColumnTicket(USER_ID_TO, CassandraType.VARCHAR, true)]
        public string UserIdTo
        {
            get
            {
                return this.GetData(USER_ID_TO);
            }
            set
            {
                this.SetData(USER_ID_TO, value);
            }
        }

        [ColumnTicket(CREATE_TIME, CassandraType.VARCHAR, true)]
        public string CreateTime
        {
            get
            {
                return this.GetData(CREATE_TIME);
            }
            set
            {
                this.SetData(CREATE_TIME, value);
            }
        }

        [ColumnTicket(UPDATE_TIME, CassandraType.VARCHAR, true)]
        public string UpdateTime
        {
            get
            {
                return this.GetData(UPDATE_TIME);
            }
            set
            {
                this.SetData(UPDATE_TIME, value);
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

        [ColumnTicket(MESSAGE, CassandraType.VARCHAR, true)]
        public string Message
        {
            get
            {
                return this.GetData(MESSAGE);
            }
            set
            {
                this.SetData(MESSAGE, value);
            }
        }
    }
}
