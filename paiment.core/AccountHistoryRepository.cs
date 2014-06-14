using System.Collections.Generic;
using Wr.API.DbLayer;
using Wr.API.GlobalDb;
using Wr.Common;

namespace paiment.core
{
    public class AccountHistoryRepository : RepositoryBase
    {
        public bool CreateAccountHistory(IMapper mapper)
        {
            var accountHistoryEntity = new AccountHistoryEntity();
            accountHistoryEntity.MergeData(mapper.PropertyDictionary);

            return accountHistoryEntity.Create();
        }

        public IList<IEntity> GetAccountHistoryCollectionByAccountId(string accountId)
        {
            var accountHistoryEntity = new AccountHistoryEntity();
            return accountHistoryEntity.GetBy(AccountHistoryEntity.ACCOUNT_ID, accountId);
        }

        public IList<IEntity> GetAccountHistoryCollectionByUserId(string userId)
        {
            var payPal = new AccountHistoryEntity();
            return payPal.GetBy(AccountHistoryEntity.USER_ID, userId);
        }

        public IList<IEntity> GetAllAccountHistoryCollection()
        {
            var accHistiry = new AccountHistoryEntity();
            return accHistiry.Get();
        } 

        protected override void InitializeComponent()
        {
            this.ClassMemberDictionary.TryAdd("CreateAccountHistory", new MethodDelegate_ExecuteBy5(this.CreateAccountHistory));
            this.ClassMemberDictionary.TryAdd("GetAccountHistoryCollectionByAccountId", new MethodDelegate_GetListBy(this.GetAccountHistoryCollectionByAccountId));
            this.ClassMemberDictionary.TryAdd("GetAccountHistoryCollectionByUserId", new MethodDelegate_GetListBy(this.GetAccountHistoryCollectionByUserId));
        }
    }
}
