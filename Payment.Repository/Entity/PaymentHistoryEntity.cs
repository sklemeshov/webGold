using Payment.Repository.Schema;
using Wr.API.DbLayer;
using Wr.API.DbLayer.Attributes;
using Wr.API.DbLayer.CassandraDb;

namespace Payment.Repository.Entity
{
   [DbObjectName("webrunes", "paymenthistory")]
    public class PaymentHistoryEntity : CassandraEntity<PaymentHistoryEntity>
    {
        [ColumnTicket(PaymentHistorySchema.ID, CassandraType.VARCHAR, false, IsPrimaryKey = true)]
        public string Id
        {
            get
            {
                return this.GetData(PaymentHistorySchema.ID);
            }
            set
            {
                this.SetData(PaymentHistorySchema.ID, value);
            }
        }

        [ColumnTicket(PaymentHistorySchema.USER_ID, CassandraType.VARCHAR, true)]
        public string UserId
        {
            get
            {
                return this.GetData(PaymentHistorySchema.USER_ID);
            }
            set
            {
                this.SetData(PaymentHistorySchema.USER_ID, value);
            }
        }

        [ColumnTicket(PaymentHistorySchema.ACCOUNT_ID, CassandraType.VARCHAR, true)]
        public string AccountId
        {
            get
            {
                return this.GetData(PaymentHistorySchema.ACCOUNT_ID);
            }
            set
            {
                this.SetData(PaymentHistorySchema.ACCOUNT_ID, value);
            }
        }

        [ColumnTicket(PaymentHistorySchema.DATE, CassandraType.VARCHAR, true)]
        public string Date
        {
            get
            {
                return this.GetData(PaymentHistorySchema.DATE);
            }
            set
            {
                this.SetData(PaymentHistorySchema.DATE, value);
            }
        }

        [ColumnTicket(PaymentHistorySchema.CURRENCY, CassandraType.VARCHAR, true)]
        public string Currency
        {
            get
            {
                return this.GetData(PaymentHistorySchema.CURRENCY);
            }
            set
            {
                this.SetData(PaymentHistorySchema.CURRENCY, value);
            }
        }

        [ColumnTicket(PaymentHistorySchema.AMOUNT, CassandraType.DOUBLE, true)]
        public double Amount
        {
            get
            {
                return this.GetData(PaymentHistorySchema.AMOUNT);
            }
            set
            {
                this.SetData(PaymentHistorySchema.AMOUNT, value);
            }
        }

        [ColumnTicket(PaymentHistorySchema.PAYMENT_TYPE, CassandraType.VARCHAR, true)]
        public string PaymentType
        {
            get
            {
                return this.GetData(PaymentHistorySchema.PAYMENT_TYPE);
            }
            set
            {
                this.SetData(PaymentHistorySchema.PAYMENT_TYPE, value);
            }
        }

        [ColumnTicket(PaymentHistorySchema.PAYMENT_METHOD, CassandraType.VARCHAR, true)]
        public string PaymentMethod
        {
            get
            {
                return this.GetData(PaymentHistorySchema.PAYMENT_METHOD);
            }
            set
            {
                this.SetData(PaymentHistorySchema.PAYMENT_METHOD, value);
            }
        }

        [ColumnTicket(PaymentHistorySchema.TRANSACTION_TYPE, CassandraType.VARCHAR, true)]
        public string TransactionType
        {
            get
            {
                return this.GetData(PaymentHistorySchema.TRANSACTION_TYPE);
            }
            set
            {
                this.SetData(PaymentHistorySchema.TRANSACTION_TYPE, value);
            }
        }

        [ColumnTicket(PaymentHistorySchema.TRANSACTION_STATUS, CassandraType.VARCHAR, true)]
        public string TransactionStatus
        {
            get
            {
                return this.GetData(PaymentHistorySchema.TRANSACTION_STATUS);
            }
            set
            {
                this.SetData(PaymentHistorySchema.TRANSACTION_STATUS, value);
            }
        }

        [ColumnTicket(PaymentHistorySchema.ReceivedEmail, CassandraType.VARCHAR, true)]
        public string ReceivedEmail { get; set; }
    }
}
