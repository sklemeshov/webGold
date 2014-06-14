using Payment.Repository.Schema;
using Wr.API.DbLayer;
using Wr.API.DbLayer.Attributes;
using Wr.API.DbLayer.CassandraDb;

namespace Payment.Repository.Entity
{
    [DbObjectName("webrunes", "account")]
    public class AccountEntity : CassandraEntity<AccountEntity>
    {
        [ColumnTicket(AccountSchema.ID, CassandraType.VARCHAR, false, IsPrimaryKey = true)]
        public string Id
        {
            get
            {
                return this.GetData(AccountSchema.ID);
            }
            set
            {
                this.SetData(AccountSchema.ID, value);
            }
        }

        [ColumnTicket(AccountSchema.USER_ID, CassandraType.VARCHAR, true)]
        public string UserId
        {
            get
            {
                return this.GetData(AccountSchema.USER_ID);
            }
            set
            {
                this.SetData(AccountSchema.USER_ID, value);
            }
        }

        [ColumnTicket(AccountSchema.CURRENCY_GLD_AMOUNT, CassandraType.DOUBLE, true)]
        public double GldAmount
        {
            get
            {
                return this.GetData(AccountSchema.CURRENCY_GLD_AMOUNT);
            }
            set
            {
                this.SetData(AccountSchema.CURRENCY_GLD_AMOUNT, value);
            }
        }

        [ColumnTicket(AccountSchema.CURRENCY_USD_AMOUNT, CassandraType.DOUBLE, true)]
        public double UsdAmount
        {
            get
            {
                return this.GetData(AccountSchema.CURRENCY_USD_AMOUNT);
            }
            set
            {
                this.SetData(AccountSchema.CURRENCY_USD_AMOUNT, value);
            }
        }
    }
}
