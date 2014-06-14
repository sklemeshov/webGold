using Wr.Common.Repository.Activator;
//using paiment.core;

namespace payment.services
{
    using Wr.API.GlobalDb;
    using Wr.API.Utility;
    

    

    public class AccountService //:  BllServiceBase
    {
        public IActivator AccountRepository
        {
            get
            {
                return RepositoryActivator.Instance["accountrepository"].Invoke();
            }
        }

        //public void CreateAccount(AccountDto dto)
        //{
        //    this.AccountRepository["CreateAccount"].Invoke(dto);
        //}

        //public void UpdateAccount(AccountDto dto)
        //{
        //    this.AccountRepository["UpdateAccount"].Invoke(dto);
        //}

        public IEntity GetAccountByUserId(string userId)
        {
            return this.AccountRepository["GetAccountByUserId"].Invoke(userId);
        }

        public IEntity GetAccountById(string id)
        {
            return this.AccountRepository["GetAccountById"].Invoke(id);
        }
      
    }
}
