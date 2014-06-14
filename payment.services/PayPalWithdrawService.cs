using System;
using System.Collections.Generic;
using Wr.Common.Repository.Activator;
//using paiment.core;

namespace payment.services
{
    using Wr.API.GlobalDb;
    using Wr.API.Utility;
    
   // using payment.paypalwithdraw;

    public class PayPalWithdrawService //: BllServiceBase
    {
        public IActivator PayPalWithdrawRepository
        {
            get
            {
                return RepositoryActivator.Instance["paypalwithdrawrepository"].Invoke();
            }
        }

        //public void CreatePayPal(PayPalWithdrawDto dto)
        //{
        //    PayPalWithdrawRepository["CreatePayPal"].Invoke(dto);
        //}

        public IEntity GetPayPalBy(string internalId)
        {
            return PayPalWithdrawRepository["GetPayPalBy"].Invoke(internalId);
        }

        //public void UpdatePayPal(PayPalWithdrawDto dto, bool isPaid)
        //{
        //    var paypalEntity = PayPalWithdrawRepository["GetPayPalBy"].Invoke(dto.InternalPaymentId) as PayPalWithdrawEntity;
        //    bool needToWithdraw = false;

        //    needToWithdraw = paypalEntity.State.Equals("pending", StringComparison.InvariantCultureIgnoreCase) && isPaid;

        //    PayPalWithdrawRepository["UpdatePayPal"].Invoke(dto);

        //    if (needToWithdraw)
        //    {
        //        Withdraw(dto);
        //    }
        //}

        //private void Withdraw(PayPalWithdrawDto dto)
        //{
        //    var account = AccountService.GetAccountByUserId(dto.UserId) as AccountEntity;
        //    var accountDto = new AccountDto();
        //    if (account != null)
        //    {
        //        accountDto.Id = account.Id;
        //        accountDto.GldAmount = account.GldAmount;
        //        accountDto.UserId = account.UserId;
        //    }
        //    else
        //    {
        //        accountDto.Id = Guid.NewGuid().ToString();
        //        accountDto.GldAmount = 0;
        //        accountDto.UserId = dto.UserId;

        //    }
        //    var accountHistoryDto = new AccountHistoryDto();
        //    accountHistoryDto.Id = Guid.NewGuid().ToString();
        //    accountHistoryDto.PaymentType = ((int)PaymentType.PayPal).ToString();
        //    accountHistoryDto.PaymentMethod = ((int)PaymentMethod.Debit).ToString();
        //    accountHistoryDto.Date = DateTime.UtcNow.ToString();

        //    double convertedAmountFromGld = 0;

        //    switch (dto.CurrencyCode)
        //    {
        //        case "USD":
        //            convertedAmountFromGld = GoldenStandartService.ConvertFromGldToUsd(dto.Amount);
        //            accountDto.GldAmount -= convertedAmountFromGld;
        //            accountHistoryDto.Currency = "USD";
        //            break;
        //        case "GLD":
        //            convertedAmountFromGld = dto.Amount;
        //            accountDto.GldAmount -= dto.Amount;
        //            accountHistoryDto.Currency = "GLD";
        //            break;
        //    }
        //    accountHistoryDto.Amount = convertedAmountFromGld;

        //    if (account == null)
        //    {
        //        AccountService.CreateAccount(accountDto);
        //    }
        //    else
        //    {
        //        AccountService.UpdateAccount(accountDto);
        //    }
        //    accountHistoryDto.AccontId = accountDto.Id;
        //    accountHistoryDto.UserId = dto.UserId;

        //    AccountHistoryService.CreateAccountHistory(accountHistoryDto);
        //}

        public List<IEntity> GetPayPalCollection(string userId)
        {
            return PayPalWithdrawRepository["GetPayPalCollection"].Invoke(userId);
        }

        public List<IEntity> GetPayPalWithdrawCollection()
        {
            return PayPalWithdrawRepository["GetPayPalWithdrawCollection"].Invoke("pending");
        }

        public List<IEntity> GetPayPalPendingCollection()
        {
            return PayPalWithdrawRepository["GetPayPalPendingCollection"].Invoke();
        }

        public IEntity GetPayPalById(Guid id)
        {
            return PayPalWithdrawRepository["GetPayPalById"].Invoke(id.ToString());
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
        public AccountHistoryService AccountHistoryService
        {
            get
            {
                return this._accountHistoryService ?? (this._accountHistoryService = new AccountHistoryService());
            }
        }

        public List<IEntity> GetPayPalProcessedWithdrawCollection()
        {
            return PayPalWithdrawRepository["GetPayPalProcessedWithdrawCollection"].Invoke("pending");
        }
    }
}
