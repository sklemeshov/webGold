using Wr.API.DbLayer;

namespace paiment.core
{
    public class AccountHistoryDto : Mapper
    {
        public string Id
        {
            get
            {
                return this.GetData(AccountHistoryEntity.ID);
            }
            set
            {
                this.SetData(AccountHistoryEntity.ID, value);
            }
        }

        public string UserId
        {
            get
            {
                return this.GetData(AccountHistoryEntity.USER_ID);
            }
            set
            {
                this.SetData(AccountHistoryEntity.USER_ID, value);
            }
        }

        public string AccontId
        {
            get
            {
                return this.GetData(AccountHistoryEntity.ACCOUNT_ID);
            }
            set
            {
                this.SetData(AccountHistoryEntity.ACCOUNT_ID, value);
            }
        }

        public string Date
        {
            get
            {
                return this.GetData(AccountHistoryEntity.DATE);
            }
            set
            {
                this.SetData(AccountHistoryEntity.DATE, value);
            }
        }

        public double Amount
        {
            get
            {
                return this.GetData(AccountHistoryEntity.AMOUNT);
            }
            set
            {
                this.SetData(AccountHistoryEntity.AMOUNT, value);
            }
        }

        
        public string Currency
        {
            get
            {
                return this.GetData(AccountHistoryEntity.CURRENCY);
            }
            set
            {
                this.SetData(AccountHistoryEntity.CURRENCY, value);
            }
        }


        public string PaymentType
        {
            get
            {
                return this.GetData(AccountHistoryEntity.PAYMENT_TYPE);
            }
            set
            {
                this.SetData(AccountHistoryEntity.PAYMENT_TYPE, value);
            }
        }

        public string PaymentMethod
        {
            get
            {
                return this.GetData(AccountHistoryEntity.PAYMENT_METHOD);
            }
            set
            {
                this.SetData(AccountHistoryEntity.PAYMENT_METHOD, value);
            }
        }
    }
}
