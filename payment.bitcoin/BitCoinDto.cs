using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace payment.bitcoin
{
    using Wr.API.DbLayer;

    public class BitCoinDto : Mapper
    {
        public string Id
        {
            get
            {
                return this.GetData(BitCoinEntity.ID);
            }
            set
            {
                this.SetData(BitCoinEntity.ID, value);
            }
        }

        public string UserId
        {
            get
            {
                return this.GetData(BitCoinEntity.USER_ID);
            }
            set
            {
                this.SetData(BitCoinEntity.USER_ID, value);
            }
        }

        public string Created
        {
            get
            {
                return this.GetData(BitCoinEntity.CREATE_TIME);
            }
            set
            {
                this.SetData(BitCoinEntity.CREATE_TIME, value);
            }
        }

        public string Updated
        {
            get
            {
                return this.GetData(BitCoinEntity.UPDATE_TIME);
            }
            set
            {
                this.SetData(BitCoinEntity.UPDATE_TIME, value);
            }
        }

        public string BitCoinId
        {
            get
            {
                return this.GetData(BitCoinEntity.BITCOIN_ID);
            }
            set
            {
                this.SetData(BitCoinEntity.BITCOIN_ID, value);
            }
        }

        public string PayerId
        {
            get
            {
                return this.GetData(BitCoinEntity.PAYER_ID);
            }
            set
            {
                this.SetData(BitCoinEntity.PAYER_ID, value);
            }
        }

        public string State
        {
            get
            {
                return this.GetData(BitCoinEntity.STATE);
            }
            set
            {
                this.SetData(BitCoinEntity.STATE, value);
            }
        }

        public double Amount
        {
            get
            {
                return this.GetData(BitCoinEntity.AMOUNT);
            }
            set
            {
                this.SetData(BitCoinEntity.AMOUNT, value);
            }
        }

        public string Currency
        {
            get
            {
                return this.GetData(BitCoinEntity.CURRENCY_CODE);
            }
            set
            {
                this.SetData(BitCoinEntity.CURRENCY_CODE, value);
            }
        }

        public string InternalPaymentId
        {
            get
            {
                return this.GetData(BitCoinEntity.INTERNAL_PAYMENT_ID);
            }
            set
            {
                this.SetData(BitCoinEntity.INTERNAL_PAYMENT_ID, value);
            }
        }

        public string UserIp
        {
            get
            {
                return this.GetData(BitCoinEntity.USER_IP);
            }
            set
            {
                this.SetData(BitCoinEntity.USER_IP, value);
            }
        }

        public string CreateTime
        {
            get
            {
                return this.GetData(BitCoinEntity.CREATE_TIME);
            }
            set
            {
                this.SetData(BitCoinEntity.CREATE_TIME, value);
            }
        }

        public string ButtonCode
        {
            get
            {
                return this.GetData(BitCoinEntity.BUTTON_CODE);
            }
            set
            {
                this.SetData(BitCoinEntity.BUTTON_CODE, value);
            }
        }

        public string UpdateTime
        {
            get
            {
                return this.GetData(BitCoinEntity.UPDATE_TIME);
            }
            set
            {
                this.SetData(BitCoinEntity.UPDATE_TIME, value);
            }
        }

        public string CompletedAt
        {
            get
            {
                return this.GetData(BitCoinEntity.COMPLETED_AT);
            }
            set
            {
                this.SetData(BitCoinEntity.COMPLETED_AT, value);
            }
        }

        public string TransactionHash
        {
            get
            {
                return this.GetData(BitCoinEntity.TRANSACTION_HASH);
            }
            set
            {
                this.SetData(BitCoinEntity.TRANSACTION_HASH, value);
            }
        }

        public int Confirmations
        {
            get
            {
                return this.GetData(BitCoinEntity.CONFIRMATIONS);
            }
            set
            {
                this.SetData(BitCoinEntity.CONFIRMATIONS, value);
            }
        }

        public string BtcCurrencyIso
        {
            get
            {
                return this.GetData(BitCoinEntity.BTC_CURRENCY_ISO);
            }
            set
            {
                this.SetData(BitCoinEntity.BTC_CURRENCY_ISO, value);
            }
        }

        public string BtcCents
        {
            get
            {
                return this.GetData(BitCoinEntity.BTC_CENTS);
            }
            set
            {
                this.SetData(BitCoinEntity.BTC_CENTS, value);
            }
        }

        public string ButtonType
        {
            get
            {
                return this.GetData(BitCoinEntity.BUTTON_TYPE);
            }
            set
            {
                this.SetData(BitCoinEntity.BUTTON_TYPE, value);
            }
        }

        public string ButtonName
        {
            get
            {
                return this.GetData(BitCoinEntity.BUTTON_NAME);
            }
            set
            {
                this.SetData(BitCoinEntity.BUTTON_NAME, value);
            }
        }

        public string NativeCurrencyIso
        {
            get
            {
                return this.GetData(BitCoinEntity.NATIVE_CURRENCY_ISO);
            }
            set
            {
                this.SetData(BitCoinEntity.NATIVE_CURRENCY_ISO, value);
            }
        }

        public string NativeCents
        {
            get
            {
                return this.GetData(BitCoinEntity.NATIVE_CENTS);
            }
            set
            {
                this.SetData(BitCoinEntity.NATIVE_CENTS, value);
            }
        }
    }
}
