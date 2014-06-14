using Wr.API.DbLayer;

namespace paiment.core
{
    public class AccountDto : Mapper
    {
        public string Id
        {
            get
            {
                return this.GetData(AccountEntity.ID);
            }
            set
            {
                this.SetData(AccountEntity.ID, value);
            }
        }

        public string UserId
        {
            get
            {
                return this.GetData(AccountEntity.USER_ID);
            }
            set
            {
                this.SetData(AccountEntity.USER_ID, value);
            }
        }

      
        public double GldAmount
        {
            get
            {
                return this.GetData(AccountEntity.CURRENCY_GLD_AMOUNT);
            }
            set
            {
                this.SetData(AccountEntity.CURRENCY_GLD_AMOUNT, value);
            }
        }
    }
}