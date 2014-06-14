using Wr.API.DbLayer;
using Wr.API.GlobalDb;
using Wr.Common;
using System.Linq;

namespace paiment.core
{
    public class AccountRepository : RepositoryBase
    {
        public bool CreateAccount(IMapper mapper)
        {
            var accountEntity = new AccountEntity();
            accountEntity.MergeData(mapper.PropertyDictionary);

            return accountEntity.Create2();
        }

        public bool UpdateAccount(IMapper mapper)
        {
            var account = new AccountEntity();
            account.MergeData(mapper.PropertyDictionary);

            var accountEntity = this.GetAccountByUserId(account.UserId);
            if (accountEntity != null)
            {
                account.SetData("id", accountEntity.PropertyDictionary["id"]);
            }
            else
            {
                return false;
            }

            return account.Create2();
        }

        public IEntity GetAccountByUserId(string userId)
        {
            var entity = new AccountEntity();
            var entityList = entity.GetBy(AccountEntity.USER_ID, userId);
            if (entityList.Count() > 0)
            {
                return entityList[0];
            }
            return null;
        }

        public IEntity GetAccountById(string id)
        {
            var entity = new AccountEntity();
            var entityList = entity.GetBy(AccountEntity.ID, id);
            if (entityList.Count() > 0)
            {
                return entityList[0];
            }
            return null;
        }

        protected override void InitializeComponent()
        {
            this.ClassMemberDictionary.TryAdd("UpdateAccount", new MethodDelegate_ExecuteBy5(this.UpdateAccount));
            this.ClassMemberDictionary.TryAdd("CreateAccount", new MethodDelegate_ExecuteBy5(this.CreateAccount));
            this.ClassMemberDictionary.TryAdd("GetAccountByUserId", new MethodDelegate_Execute3(this.GetAccountByUserId));
            this.ClassMemberDictionary.TryAdd("GetAccountById", new MethodDelegate_Execute3(this.GetAccountById));
        }
    }
}
