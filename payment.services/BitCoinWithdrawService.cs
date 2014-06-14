using System;
using Wr.Common.Repository.Activator;
//using paiment.core;

namespace payment.services
{
    using Wr.API.Utility;
    
    //using payment.bitcoinwithdraw;
    
    public class BitCoinWithdrawService //: BllServiceBase
    {
        public IActivator BitCoinWithdrawRepository
        {
            get
            {
                return RepositoryActivator.Instance["bitcoinwithdrawrepository"].Invoke();
            }
        }

        //public void CreateBitCoin(BitCoinWithdrawDto dto)
        //{
        //    BitCoinWithdrawRepository["CreateBitCoin"].Invoke(dto);
        //}

        //public void UpdateBitCoin(BitCoinWithdrawDto bitCoinDto, bool isPaid)
        //{
        //    var bitCoinWithdrawEntity = BitCoinWithdrawRepository["GetBitCoinByBy"].Invoke(bitCoinDto.InternalPaymentId) as BitCoinWithdrawEntity;
        //    bool needToWithdraw = false;

        //    needToWithdraw = bitCoinWithdrawEntity.Status.Equals("pending", StringComparison.InvariantCultureIgnoreCase) && isPaid;

        //    BitCoinWithdrawRepository["UpdateBitCoin"].Invoke(bitCoinDto);

        //    if (needToWithdraw)
        //    {
        //        Withdraw(bitCoinDto);
        //    }
        //}

        //private void Withdraw(BitCoinWithdrawDto dto)
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
        //    accountHistoryDto.PaymentType = ((int)PaymentType.BitCoin).ToString();
        //    accountHistoryDto.PaymentMethod = ((int)PaymentMethod.Debit).ToString();
        //    accountHistoryDto.Date = DateTime.UtcNow.ToString();

        //    double convertedAmountFromGld = 0;

        //    switch (dto.Currency)
        //    {
        //        case "BTC":
        //            convertedAmountFromGld = GoldenStandartService.ConvertFromGldToBtc(dto.Amount);
        //            accountDto.GldAmount -= convertedAmountFromGld;
        //            accountHistoryDto.Currency = "BTC";
        //            break;
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
    }
}