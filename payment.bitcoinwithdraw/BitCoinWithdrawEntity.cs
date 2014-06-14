using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace payment.bitcoinwithdraw
{
    using Wr.API.DbLayer;
    using Wr.API.DbLayer.Attributes;
    using Wr.API.DbLayer.CassandraDb;

    [DbObjectName("webrunes", "bitcoinwithdraw")]
    public class BitCoinWithdrawEntity : CassandraEntity<BitCoinWithdrawEntity>
    {
        internal const string ID = "id";

        internal const string USER_ID = "userid";

        internal const string USER_IP = "userip";

        internal const string CREATE_TIME = "createtime";

        internal const string UPDATE_TIME = "updatetime";

        internal const string AMOUNT = "amount";

        internal const string CURRENCY_CODE = "currencycode";

        internal const string INTERNAL_PAYMENT_ID = "internalpaymentid";

        internal const string STATUS = "status";

        internal const string ADDRESS_RECIPIENT = "addressrecipient";

        internal const string COMMENT = "comment";


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

        [ColumnTicket(STATUS, CassandraType.VARCHAR, true)]
        public string Status
        {
            get
            {
                return this.GetData(STATUS);
            }
            set
            {
                this.SetData(STATUS, value);
            }
        }

        [ColumnTicket(ADDRESS_RECIPIENT, CassandraType.VARCHAR, true)]
        public string AddressRecipient
        {
            get
            {
                return this.GetData(ADDRESS_RECIPIENT);
            }
            set
            {
                this.SetData(ADDRESS_RECIPIENT, value);
            }
        }

        [ColumnTicket(COMMENT, CassandraType.VARCHAR, true)]
        public string Comment
        {
            get
            {
                return this.GetData(COMMENT);
            }
            set
            {
                this.SetData(COMMENT, value);
            }
        }

    }
}
