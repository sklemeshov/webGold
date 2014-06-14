using Wr.API.DbLayer;
using Wr.API.DbLayer.Attributes;
using Wr.API.DbLayer.CassandraDb;

namespace paiment.core
{
    [DbObjectName("webrunes", "account")]
    public class AccountEntity : CassandraEntity<AccountEntity>
    {
        internal const string ID = "id";

        internal const string USER_ID = "userid";

        internal const string CURRENCY_GLD_AMOUNT = "gld";

        
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

        
        [ColumnTicket(CURRENCY_GLD_AMOUNT, CassandraType.DOUBLE, true)]
        public double GldAmount
        {
            get
            {
                return this.GetData(CURRENCY_GLD_AMOUNT);
            }
            set
            {
                this.SetData(CURRENCY_GLD_AMOUNT, value);
            }
        }

 }
}
