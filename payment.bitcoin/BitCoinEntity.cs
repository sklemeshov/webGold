using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace payment.bitcoin
{
    using Wr.API.DbLayer;
    using Wr.API.DbLayer.Attributes;
    using Wr.API.DbLayer.CassandraDb;

    [DbObjectName("webrunes", "bitcoin")]
    public class BitCoinEntity : CassandraEntity<BitCoinEntity>
    {
        internal const string ID = "id";

        internal const string USER_ID = "userid";

        internal const string CREATE_TIME = "createtime";

        internal const string UPDATE_TIME = "updatetime";

        internal const string BITCOIN_ID = "bitcoinid";

        internal const string PAYER_ID = "payer";

        internal const string STATE = "state";

        internal const string AMOUNT = "amount";

        internal const string CURRENCY_CODE = "currencycode";

        internal const string INTERNAL_PAYMENT_ID = "internalpaymentid";

        internal const string USER_IP = "userip";


        internal const string BUTTON_CODE = "buttoncode";
        internal const string COMPLETED_AT = "completedat";
        internal const string TRANSACTION_HASH = "transactionhash";
        internal const string CONFIRMATIONS = "confirmations";
        internal const string BTC_CURRENCY_ISO = "btccurrencyiso";
        internal const string BTC_CENTS = "btccents";
        internal const string BUTTON_TYPE = "buttontype";
        internal const string BUTTON_NAME = "buttonname";
        internal const string NATIVE_CURRENCY_ISO = "nativecurrencyiso";
        internal const string NATIVE_CENTS = "nativecents";


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

        [ColumnTicket(BUTTON_CODE, CassandraType.VARCHAR, true)]
        public string ButtonCode
        {
            get
            {
                return this.GetData(BUTTON_CODE);
            }
            set
            {
                this.SetData(BUTTON_CODE, value);
            }
        }

        [ColumnTicket(COMPLETED_AT, CassandraType.VARCHAR, true)]
        public string CompletedAt
        {
            get
            {
                return this.GetData(COMPLETED_AT);
            }
            set
            {
                this.SetData(COMPLETED_AT, value);
            }
        }

        [ColumnTicket(TRANSACTION_HASH, CassandraType.VARCHAR, true)]
        public string TransactionHash
        {
            get
            {
                return this.GetData(TRANSACTION_HASH);
            }
            set
            {
                this.SetData(TRANSACTION_HASH, value);
            }
        }

        [ColumnTicket(CONFIRMATIONS, CassandraType.INT, true)]
        public int Confirmations
        {
            get
            {
                return this.GetData(CONFIRMATIONS);
            }
            set
            {
                this.SetData(CONFIRMATIONS, value);
            }
        }

        [ColumnTicket(BTC_CURRENCY_ISO, CassandraType.VARCHAR, true)]
        public string BtcCurrencyIso
        {
            get
            {
                return this.GetData(BTC_CURRENCY_ISO);
            }
            set
            {
                this.SetData(BTC_CURRENCY_ISO, value);
            }
        }

        [ColumnTicket(BTC_CENTS, CassandraType.VARCHAR, true)]
        public string BtcCents
        {
            get
            {
                return this.GetData(BTC_CENTS);
            }
            set
            {
                this.SetData(BTC_CENTS, value);
            }
        }

        [ColumnTicket(BUTTON_TYPE, CassandraType.VARCHAR, true)]
        public string ButtonType
        {
            get
            {
                return this.GetData(BUTTON_TYPE);
            }
            set
            {
                this.SetData(BUTTON_TYPE, value);
            }
        }

        [ColumnTicket(BUTTON_NAME, CassandraType.VARCHAR, true)]
        public string ButtonName
        {
            get
            {
                return this.GetData(BUTTON_NAME);
            }
            set
            {
                this.SetData(BUTTON_NAME, value);
            }
        }

        [ColumnTicket(NATIVE_CURRENCY_ISO, CassandraType.VARCHAR, true)]
        public string NativeCurrencyIso
        {
            get
            {
                return this.GetData(NATIVE_CURRENCY_ISO);
            }
            set
            {
                this.SetData(NATIVE_CURRENCY_ISO, value);
            }
        }

        [ColumnTicket(NATIVE_CENTS, CassandraType.VARCHAR, true)]
        public string NativeCents
        {
            get
            {
                return this.GetData(NATIVE_CENTS);
            }
            set
            {
                this.SetData(NATIVE_CENTS, value);
            }
        }
     
    }
}
