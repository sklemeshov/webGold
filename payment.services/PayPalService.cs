using Wr.Common.Repository.Activator;
//using paiment.core;

namespace payment.services
{
    using System;
    using System.Collections.Generic;

    using Wr.API.GlobalDb;
    using Wr.API.Utility;
    
    //using payment.paypal;

    public class PayPalService //:  BllServiceBase
    {
        public IActivator PayPalRepository
        {
            get
            {
                return RepositoryActivator.Instance["paypalrepository"].Invoke();
            }
        }

        //public void CreatePayPal(PayPalDto dto)
        //{
        //    this.PayPalRepository["CreatePayPal"].Invoke(dto);
        //}

        //public IEntity GetPayPalBy(string internalId)
        //{
        //    return this.PayPalRepository["GetPayPalBy"].Invoke(internalId);
        //}

        //public void UpdatePayPal(PayPalDto dto, bool isPaid)
        //{
        //    var paypalEntity = this.PayPalRepository["GetPayPalBy"].Invoke(dto.InternalPaymentId) as PayPalEntity;
        //    bool needToDeposit = false;

        //    needToDeposit = paypalEntity.State.Equals("pending", StringComparison.InvariantCultureIgnoreCase) && isPaid;

        //    this.PayPalRepository["UpdatePayPal"].Invoke(dto);

        //    if (needToDeposit)
        //    {
        //        this.Deposit(dto);
        //    }
        //}

       
        //private void Deposit(PayPalDto dto)
        //{
        //    if (!dto.CurrencyCode.Equals(USD, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        throw new ApplicationException(string.Format("Unable to deposit. Unconfigured currency {0}.", dto.CurrencyCode));
        //    }
        //    var account = this.AccountService.GetAccountByUserId(dto.UserId) as AccountEntity;
        //    var accountDto = new AccountDto();

        //    if (account == null)
        //    {
        //        accountDto.Id = Guid.NewGuid().ToString();
        //        accountDto.UserId = dto.UserId;
        //        accountDto.GldAmount = 0;
        //    }
        //    else
        //    {
        //        accountDto.Id = account.Id;
        //        accountDto.GldAmount = account.GldAmount;
        //    }

        //   var accountHistoryDto = new AccountHistoryDto();
        //    accountHistoryDto.Id = Guid.NewGuid().ToString();
        //    accountHistoryDto.PaymentType = ((int)PaymentType.PayPal).ToString();
        //    accountHistoryDto.PaymentMethod = ((int)PaymentMethod.Credit).ToString();
        //    accountHistoryDto.Date = DateTime.UtcNow.ToString();
        //    double amount = dto.Amount - dto.Fee;
           
        //    accountDto.GldAmount = accountDto.GldAmount + GoldenStandartService.ConvertFromUsdToGld(dto.Amount);
            
        //    accountHistoryDto.Amount = amount; 
        //    accountHistoryDto.Currency = "USD";
 
        //    if (account == null)
        //    {
        //        this.AccountService.CreateAccount(accountDto);
        //    }
        //    else
        //    {
        //        this.AccountService.UpdateAccount(accountDto);
        //    }
        //    accountHistoryDto.AccontId = accountDto.Id;
        //    accountHistoryDto.UserId = dto.UserId;

        //    this.AccountHistoryService.CreateAccountHistory(accountHistoryDto);
        //}

        public List<IEntity> GetPayPalCollection(string userId)
        {
            return this.PayPalRepository["GetPayPalCollection"].Invoke(userId);
        }

        public IEntity GetPayPalById(Guid id)
        {
            return this.PayPalRepository["GetPayPalById"].Invoke(id.ToString());
        }

        private AccountService _accountService;
        public AccountService AccountService
        {
            get
            {
                return this._accountService ?? (this._accountService = new AccountService());
            }
        }

        private AccountHistoryService _accountHistoryService;

        private const string USD = "USD";

        public AccountHistoryService AccountHistoryService
        {
            get
            {
                return this._accountHistoryService ?? (this._accountHistoryService = new AccountHistoryService());
            }
        }
    }
}
