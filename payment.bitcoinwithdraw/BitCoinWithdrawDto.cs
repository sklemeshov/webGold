using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace payment.bitcoinwithdraw
{
    using Wr.API.DbLayer;

    public class BitCoinWithdrawDto : Mapper
    {
        public string Id
        {
            get
            {
                return this.GetData(BitCoinWithdrawEntity.ID);
            }
            set
            {
                this.SetData(BitCoinWithdrawEntity.ID, value);
            }
        }

        public string UserId
        {
            get
            {
                return this.GetData(BitCoinWithdrawEntity.USER_ID);
            }
            set
            {
                this.SetData(BitCoinWithdrawEntity.USER_ID, value);
            }
        }

        public string UserIp
        {
            get
            {
                return this.GetData(BitCoinWithdrawEntity.USER_IP);
            }
            set
            {
                this.SetData(BitCoinWithdrawEntity.USER_IP, value);
            }
        }

        public double Amount
        {
            get
            {
                return this.GetData(BitCoinWithdrawEntity.AMOUNT);
            }
            set
            {
                this.SetData(BitCoinWithdrawEntity.AMOUNT, value);
            }
        }

        public string Currency
        {
            get
            {
                return this.GetData(BitCoinWithdrawEntity.CURRENCY_CODE);
            }
            set
            {
                this.SetData(BitCoinWithdrawEntity.CURRENCY_CODE, value);
            }
        }

        public string InternalPaymentId
        {
            get
            {
                return this.GetData(BitCoinWithdrawEntity.INTERNAL_PAYMENT_ID);
            }
            set
            {
                this.SetData(BitCoinWithdrawEntity.INTERNAL_PAYMENT_ID, value);
            }
        }

        public string CreateTime
        {
            get
            {
                return this.GetData(BitCoinWithdrawEntity.CREATE_TIME);
            }
            set
            {
                this.SetData(BitCoinWithdrawEntity.CREATE_TIME, value);
            }
        }

        public string UpdateTime
        {
            get
            {
                return this.GetData(BitCoinWithdrawEntity.UPDATE_TIME);
            }
            set
            {
                this.SetData(BitCoinWithdrawEntity.UPDATE_TIME, value);
            }
        }

        public string Status
        {
            get
            {
                return this.GetData(BitCoinWithdrawEntity.STATUS);
            }
            set
            {
                this.SetData(BitCoinWithdrawEntity.STATUS, value);
            }
        }

        public string AddressRecipient
        {
            get
            {
                return this.GetData(BitCoinWithdrawEntity.ADDRESS_RECIPIENT);
            }
            set
            {
                this.SetData(BitCoinWithdrawEntity.ADDRESS_RECIPIENT, value);
            }
        }

        public string Comment
        {
            get
            {
                return this.GetData(BitCoinWithdrawEntity.COMMENT);
            }
            set
            {
                this.SetData(BitCoinWithdrawEntity.COMMENT, value);
            }
        }
    }
}
