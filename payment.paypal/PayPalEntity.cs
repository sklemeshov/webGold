namespace payment.paypal
{
    using System;

    using Wr.API.DbLayer;
    using Wr.API.DbLayer.Attributes;
    using Wr.API.DbLayer.CassandraDb;


    [DbObjectName("webrunes", "paypal")]
    public class PayPalEntity : CassandraEntity<PayPalEntity>
    {
        internal const string ID = "id";

        internal const string USER_ID = "userid";


        internal const string CREATE_TIME = "createtime";

        internal const string UPDATE_TIME = "updatetime";

        internal const string PAYPAL_ID = "paypalid";

        internal const string INTENT = "intent";

        internal const string PAYER_ID = "payer";

        internal const string STATE = "state";

        internal const string PAYER_EMAIL = "payeremail";

        internal const string PAYER_FIRST_NAME = "payerfirstname";

        internal const string PAYER_LAST_NAME = "payerlastname";

        internal const string PAYER_PHONE = "payerphone";

        internal const string PAYER_ADDRESS = "payeraddress";

        internal const string PAYMENT_METHOD = "paymentmethod";
        internal const string AMOUNT = "amount";
        internal const string FEE = "fee";
        internal const string CURRENCY_CODE = "currencycode";
        internal const string INTERNAL_PAYMENT_ID = "internalpaymentid";
        internal const string USER_IP = "userip";


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

        [ColumnTicket(USER_IP, CassandraType.VARCHAR, true)]
        public string UserIp
        {
            get
            {
                return this.GetData(USER_IP);
            }
            set
            {
                this.SetData(USER_IP, value);
            }
        }


        [ColumnTicket(INTERNAL_PAYMENT_ID, CassandraType.VARCHAR, true)]
        public string InternalPaymentId
        {
            get
            {
                return this.GetData(INTERNAL_PAYMENT_ID);
            }
            set
            {
                this.SetData(INTERNAL_PAYMENT_ID, value);
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

        [ColumnTicket(PAYPAL_ID, CassandraType.VARCHAR, true)]
        public string PayPalId
        {
            get
            {
                return this.GetData(PAYPAL_ID);
            }
            set
            {
                this.SetData(PAYPAL_ID, value);
            }
        }

        [ColumnTicket(INTENT, CassandraType.VARCHAR, true)]
        public string Intent
        {
            get
            {
                return this.GetData(INTENT);
            }
            set
            {
                this.SetData(INTENT, value);
            }
        }

        [ColumnTicket(PAYER_ID, CassandraType.VARCHAR, true)]
        public string PayerId
        {
            get
            {
                return this.GetData(PAYER_ID);
            }
            set
            {
                this.SetData(PAYER_ID, value);
            }
        }

        [ColumnTicket(STATE, CassandraType.VARCHAR, true)]
        public string State
        {
            get
            {
                return this.GetData(STATE);
            }
            set
            {
                this.SetData(STATE, value);
            }
        }

        [ColumnTicket(PAYER_EMAIL, CassandraType.VARCHAR, true)]
        public string PayerEmail
        {
            get
            {
                return this.GetData(PAYER_EMAIL);
            }
            set
            {
                this.SetData(PAYER_EMAIL, value);
            }
        }

        [ColumnTicket(PAYER_FIRST_NAME, CassandraType.VARCHAR, true)]
        public string PayerFirstName
        {
            get
            {
                return this.GetData(PAYER_FIRST_NAME);
            }
            set
            {
                this.SetData(PAYER_FIRST_NAME, value);
            }
        }

        [ColumnTicket(PAYER_LAST_NAME, CassandraType.VARCHAR, true)]
        public string PayerLastName
        {
            get
            {
                return this.GetData(PAYER_LAST_NAME);
            }
            set
            {
                this.SetData(PAYER_LAST_NAME, value);
            }
        }

        [ColumnTicket(PAYER_PHONE, CassandraType.VARCHAR, true)]
        public string PayerPhone
        {
            get
            {
                return this.GetData(PAYER_PHONE);
            }
            set
            {
                this.SetData(PAYER_PHONE, value);
            }
        }


        [ColumnTicket(PAYER_ADDRESS, CassandraType.VARCHAR, true)]
        public string PayerAddress
        {
            get
            {
                return this.GetData(PAYER_ADDRESS);
            }
            set
            {
                this.SetData(PAYER_ADDRESS, value);
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

        [ColumnTicket(FEE, CassandraType.DOUBLE, true)]
        public double Fee
        {
            get
            {
                try
                {
                    return this.GetData(FEE);
                }
                catch (Exception)
                {
                    return 0;
                    throw;
                }
               
            }
            set
            {
                this.SetData(FEE, value);
            }
        }

        [ColumnTicket(CURRENCY_CODE, CassandraType.VARCHAR, true)]
        public string CurrencyCode
        {
            get
            {
                return this.GetData(CURRENCY_CODE);
            }
            set
            {
                this.SetData(CURRENCY_CODE, value);
            }
        }

    }
}
