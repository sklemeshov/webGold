namespace payment.paypalwithdraw
{
    using System;

    using Wr.API.DbLayer;

    public class PayPalWithdrawDto : Mapper
    {
        public string Id
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.ID);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.ID, value);
            }
        }

        public string UserId
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.USER_ID);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.USER_ID, value);
            }
        }

        public string CreateTime
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.CREATE_TIME);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.CREATE_TIME, value);
            }
        }

        public string UpdateTime
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.UPDATE_TIME);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.UPDATE_TIME, value);
            }
        }

        public string PayPalId
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.PAYPAL_ID);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.PAYPAL_ID, value);
            }
        }

        public string ReceiverInfoType
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.RECEIVER_INFO_TYPE);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.RECEIVER_INFO_TYPE, value);
            }
        }

        public string PayerId
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.PAYER_ID);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.PAYER_ID, value);
            }
        }

        public string PayerEmail
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.PAYER_EMAIL);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.PAYER_EMAIL, value);
            }
        }

        public string PayerAddress
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.PAYER_ADDRESS);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.PAYER_ADDRESS, value);
            }
        }

        public string PayerFirstName
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.PAYER_FIRST_NAME);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.PAYER_FIRST_NAME, value);
            }
        }

        public string PayerLastName
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.PAYER_LAST_NAME);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.PAYER_LAST_NAME, value);
            }
        }

        public string State
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.STATE);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.STATE, value);
            }
        }

        public string PaymentMethod
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.PAYMENT_METHOD);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.PAYMENT_METHOD, value);
            }
        }

        public double Amount
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.AMOUNT);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.AMOUNT, value);
            }
        }

        public double Fee
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.FEE);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.FEE, value);
            }
        }

        public string CurrencyCode
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.CURRENCY_CODE);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.CURRENCY_CODE, value);
            }
        }

        public string UserIp
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.USER_IP);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.USER_IP, value);
            }
        }

        public string InternalPaymentId
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.INTERNAL_PAYMENT_ID);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.INTERNAL_PAYMENT_ID, value);
            }
        }

        public string Ack
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.ACK);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.ACK, value);
            }
        }

        public string CorrelationId
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.CORRELATION_ID);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.CORRELATION_ID, value);
            }
        }

        public string ErrorText
        {
            get
            {
                return this.GetData(PayPalWithdrawEntity.ERROR_TEXT);
            }
            set
            {
                this.SetData(PayPalWithdrawEntity.ERROR_TEXT, value);
            }
        }
    }
}