namespace payment.paypal
{
    using System;

    using Wr.API.DbLayer;

    public class PayPalDto : Mapper
    {
        public string Id
        {
            get
            {
                return this.GetData(PayPalEntity.ID);
            }
            set
            {
                this.SetData(PayPalEntity.ID, value);
            }
        }

        public string UserId
        {
            get
            {
                return this.GetData(PayPalEntity.USER_ID);
            }
            set
            {
                this.SetData(PayPalEntity.USER_ID, value);
            }
        }

        public string CreateTime
        {
            get
            {
                return this.GetData(PayPalEntity.CREATE_TIME);
            }
            set
            {
                this.SetData(PayPalEntity.CREATE_TIME, value);
            }
        }

        public string UpdateTime
        {
            get
            {
                return this.GetData(PayPalEntity.UPDATE_TIME);
            }
            set
            {
                this.SetData(PayPalEntity.UPDATE_TIME, value);
            }
        }

        public string PayPalId
        {
            get
            {
                return this.GetData(PayPalEntity.PAYPAL_ID);
            }
            set
            {
                this.SetData(PayPalEntity.PAYPAL_ID, value);
            }
        }

        public string Intent
        {
            get
            {
                return this.GetData(PayPalEntity.INTENT);
            }
            set
            {
                this.SetData(PayPalEntity.INTENT, value);
            }
        }

        public string PayerId
        {
            get
            {
                return this.GetData(PayPalEntity.PAYER_ID);
            }
            set
            {
                this.SetData(PayPalEntity.PAYER_ID, value);
            }
        }

        public string PayerEmail
        {
            get
            {
                return this.GetData(PayPalEntity.PAYER_EMAIL);
            }
            set
            {
                this.SetData(PayPalEntity.PAYER_EMAIL, value);
            }
        }

        public string PayerAddress
        {
            get
            {
                return this.GetData(PayPalEntity.PAYER_ADDRESS);
            }
            set
            {
                this.SetData(PayPalEntity.PAYER_ADDRESS, value);
            }
        }

        public string PayerFirstName
        {
            get
            {
                return this.GetData(PayPalEntity.PAYER_FIRST_NAME);
            }
            set
            {
                this.SetData(PayPalEntity.PAYER_FIRST_NAME, value);
            }
        }

        public string PayerLastName
        {
            get
            {
                return this.GetData(PayPalEntity.PAYER_LAST_NAME);
            }
            set
            {
                this.SetData(PayPalEntity.PAYER_LAST_NAME, value);
            }
        }

        public string State
        {
            get
            {
                return this.GetData(PayPalEntity.STATE);
            }
            set
            {
                this.SetData(PayPalEntity.STATE, value);
            }
        }

        public string PaymentMethod
        {
            get
            {
                return this.GetData(PayPalEntity.PAYMENT_METHOD);
            }
            set
            {
                this.SetData(PayPalEntity.PAYMENT_METHOD, value);
            }
        }

        public double Amount
        {
            get
            {
                return this.GetData(PayPalEntity.AMOUNT);
            }
            set
            {
                this.SetData(PayPalEntity.AMOUNT, value);
            }
        }

        public double Fee
        {
            get
            {
                return this.GetData(PayPalEntity.FEE);
            }
            set
            {
                this.SetData(PayPalEntity.FEE, value);
            }
        }

        public string CurrencyCode
        {
            get
            {
                return this.GetData(PayPalEntity.CURRENCY_CODE);
            }
            set
            {
                this.SetData(PayPalEntity.CURRENCY_CODE, value);
            }
        }

        public string UserIp
        {
            get
            {
                return this.GetData(PayPalEntity.USER_IP);
            }
            set
            {
                this.SetData(PayPalEntity.USER_IP, value);
            }
        }

        public string InternalPaymentId
        {
            get
            {
                return this.GetData(PayPalEntity.INTERNAL_PAYMENT_ID);
            }
            set
            {
                this.SetData(PayPalEntity.INTERNAL_PAYMENT_ID, value);
            }
        }
    }
}