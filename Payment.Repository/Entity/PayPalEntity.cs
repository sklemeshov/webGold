using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payment.Repository.Schema;
using Wr.API.DbLayer;
using Wr.API.DbLayer.Attributes;
using Wr.API.DbLayer.CassandraDb;

namespace Payment.Repository.Entity
{
    [DbObjectName("webrunes", "paypal")]
    public class PayPalEntity : CassandraEntity<PayPalEntity>
    {
        [ColumnTicket(PayPalSchema.ID, CassandraType.VARCHAR, false, IsPrimaryKey = true)]
        public string Id
        {
            get
            {
                return this.GetData(PayPalSchema.ID);
            }
            set
            {
                this.SetData(PayPalSchema.ID, value);
            }
        }

        [ColumnTicket(PayPalSchema.USER_ID, CassandraType.VARCHAR, true)]
        public string UserId
        {
            get
            {
                return this.GetData(PayPalSchema.USER_ID);
            }
            set
            {
                this.SetData(PayPalSchema.USER_ID, value);
            }
        }

        [ColumnTicket(PayPalSchema.USER_IP, CassandraType.VARCHAR, true)]
        public string UserIp
        {
            get
            {
                return this.GetData(PayPalSchema.USER_IP);
            }
            set
            {
                this.SetData(PayPalSchema.USER_IP, value);
            }
        }

        [ColumnTicket(PayPalSchema.INTERNAL_PAYMENT_ID, CassandraType.VARCHAR, true)]
        public string InternalPaymentId
        {
            get
            {
                return this.GetData(PayPalSchema.INTERNAL_PAYMENT_ID);
            }
            set
            {
                this.SetData(PayPalSchema.INTERNAL_PAYMENT_ID, value);
            }
        }

        [ColumnTicket(PayPalSchema.CREATE_TIME, CassandraType.VARCHAR, true)]
        public string CreateTime
        {
            get
            {
                return this.GetData(PayPalSchema.CREATE_TIME);
            }
            set
            {
                this.SetData(PayPalSchema.CREATE_TIME, value);
            }
        }

        [ColumnTicket(PayPalSchema.UPDATE_TIME, CassandraType.VARCHAR, true)]
        public string UpdateTime
        {
            get
            {
                return this.GetData(PayPalSchema.UPDATE_TIME);
            }
            set
            {
                this.SetData(PayPalSchema.UPDATE_TIME, value);
            }
        }

        [ColumnTicket(PayPalSchema.PAYPAL_ID, CassandraType.VARCHAR, true)]
        public string PayPalId
        {
            get
            {
                return this.GetData(PayPalSchema.PAYPAL_ID);
            }
            set
            {
                this.SetData(PayPalSchema.PAYPAL_ID, value);
            }
        }

        [ColumnTicket(PayPalSchema.INTENT, CassandraType.VARCHAR, true)]
        public string Intent
        {
            get
            {
                return this.GetData(PayPalSchema.INTENT);
            }
            set
            {
                this.SetData(PayPalSchema.INTENT, value);
            }
        }

        [ColumnTicket(PayPalSchema.PAYER_ID, CassandraType.VARCHAR, true)]
        public string PayerId
        {
            get
            {
                return this.GetData(PayPalSchema.PAYER_ID);
            }
            set
            {
                this.SetData(PayPalSchema.PAYER_ID, value);
            }
        }

        [ColumnTicket(PayPalSchema.STATE, CassandraType.VARCHAR, true)]
        public string State
        {
            get
            {
                return this.GetData(PayPalSchema.STATE);
            }
            set
            {
                this.SetData(PayPalSchema.STATE, value);
            }
        }

        [ColumnTicket(PayPalSchema.PAYER_EMAIL, CassandraType.VARCHAR, true)]
        public string PayerEmail
        {
            get
            {
                return this.GetData(PayPalSchema.PAYER_EMAIL);
            }
            set
            {
                this.SetData(PayPalSchema.PAYER_EMAIL, value);
            }
        }

        [ColumnTicket(PayPalSchema.PAYER_FIRST_NAME, CassandraType.VARCHAR, true)]
        public string PayerFirstName
        {
            get
            {
                return this.GetData(PayPalSchema.PAYER_FIRST_NAME);
            }
            set
            {
                this.SetData(PayPalSchema.PAYER_FIRST_NAME, value);
            }
        }

        [ColumnTicket(PayPalSchema.PAYER_LAST_NAME, CassandraType.VARCHAR, true)]
        public string PayerLastName
        {
            get
            {
                return this.GetData(PayPalSchema.PAYER_LAST_NAME);
            }
            set
            {
                this.SetData(PayPalSchema.PAYER_LAST_NAME, value);
            }
        }

        [ColumnTicket(PayPalSchema.PAYER_PHONE, CassandraType.VARCHAR, true)]
        public string PayerPhone
        {
            get
            {
                return this.GetData(PayPalSchema.PAYER_PHONE);
            }
            set
            {
                this.SetData(PayPalSchema.PAYER_PHONE, value);
            }
        }

        [ColumnTicket(PayPalSchema.PAYER_ADDRESS, CassandraType.VARCHAR, true)]
        public string PayerAddress
        {
            get
            {
                return this.GetData(PayPalSchema.PAYER_ADDRESS);
            }
            set
            {
                this.SetData(PayPalSchema.PAYER_ADDRESS, value);
            }
        }

        [ColumnTicket(PayPalSchema.PAYMENT_METHOD, CassandraType.VARCHAR, true)]
        public string PaymentMethod
        {
            get
            {
                return this.GetData(PayPalSchema.PAYMENT_METHOD);
            }
            set
            {
                this.SetData(PayPalSchema.PAYMENT_METHOD, value);
            }
        }

        [ColumnTicket(PayPalSchema.AMOUNT, CassandraType.DOUBLE, true)]
        public double Amount
        {
            get
            {
                return this.GetData(PayPalSchema.AMOUNT);
            }
            set
            {
                this.SetData(PayPalSchema.AMOUNT, value);
            }
        }

        [ColumnTicket(PayPalSchema.FEE, CassandraType.DOUBLE, true)]
        public double Fee
        {
            get
            {
                try
                {
                    return this.GetData(PayPalSchema.FEE);
                }
                catch (Exception)
                {
                    return 0;
                    throw;
                }

            }
            set
            {
                this.SetData(PayPalSchema.FEE, value);
            }
        }

        [ColumnTicket(PayPalSchema.CURRENCY_CODE, CassandraType.VARCHAR, true)]
        public string CurrencyCode
        {
            get
            {
                return this.GetData(PayPalSchema.CURRENCY_CODE);
            }
            set
            {
                this.SetData(PayPalSchema.CURRENCY_CODE, value);
            }
        }

        [ColumnTicket(PayPalSchema.RECEIVER_INFO_TYPE, CassandraType.VARCHAR, true)]
        public string ReceiverInfoType { get; set; }
            
    }
}
