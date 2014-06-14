using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wr.Common.Repository.Activator;
//using paiment.core;

namespace payment.services
{
    using System.Globalization;

    using Wr.API.GlobalDb;
    using Wr.API.Utility;
    //using payment.bitcoin;
    
    public class BitCoinService// : BllServiceBase
    {
        public IActivator BitCoinRepository
        {
            get
            {
                return RepositoryActivator.Instance["bitcoinrepository"].Invoke();
            }
        }

        //public void CreateBitCoin(BitCoinDto dto)
        //{
        //    BitCoinRepository["CreateBitCoin"].Invoke(dto);
        //}

        public IEntity GetBitCoinBy(string internalId)
        {
            return BitCoinRepository["GetBitCoinBy"].Invoke(internalId);
        }

        //public void UpdateBitCoin(BitCoinDto dto, bool isPaid)
        //{
        //    var bitCoinEntity = BitCoinRepository["GetBitCoinById"].Invoke(dto.Id) as BitCoinEntity;
        //    bool needToDeposit = false;

        //    if (bitCoinEntity.State != null)
        //    needToDeposit = bitCoinEntity.State.Equals("pending", StringComparison.InvariantCultureIgnoreCase) && isPaid;

            
        //    BitCoinRepository["UpdateBitCoin"].Invoke(dto);

        //    if (needToDeposit)
        //    {
        //        Deposit(bitCoinEntity);
        //    }
        //}

        private const string BTC = "BTC";

        //private void Deposit(BitCoinEntity dto)
        //{
        //    if (!dto.CurrencyCode.Equals(BTC, StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        throw new ApplicationException(string.Format("Unable to deposit. Unconfigured currency {0}.", dto.CurrencyCode));
        //    }
        //    var account = AccountService.GetAccountByUserId(dto.UserId) as AccountEntity;
        //    var accountDto = new AccountDto();
        //    var accountHistoryDto = new AccountHistoryDto();
        //    accountHistoryDto.PaymentType = ((int)PaymentType.BitCoin).ToString();
        //    accountHistoryDto.PaymentMethod = ((int)PaymentMethod.Credit).ToString();
        //    accountHistoryDto.Date = DateTime.UtcNow.ToString();

        //    if (account == null)
        //    {
        //        accountDto.GldAmount = 0;
        //    }
        //    else
        //    {
        //        accountDto.GldAmount = account.GldAmount;
        //    }

        //    accountDto.GldAmount = accountDto.GldAmount + GoldenStandartService.ConvertFromBtcToGld(dto.Amount, ConvertFromNativeCents(dto.NativeCents), dto.NativeCurrencyIso);

        //    accountHistoryDto.Currency = BTC;
        //    accountHistoryDto.Amount = dto.Amount;
            
        //    if (account == null)
        //    {
        //        accountDto.Id = Guid.NewGuid().ToString();
        //        accountDto.UserId = dto.UserId;
        //        AccountService.CreateAccount(accountDto);
        //    }
        //    else
        //    {
        //        accountDto.Id = account.Id;
        //        AccountService.UpdateAccount(accountDto);
        //    }
        //    accountHistoryDto.AccontId = accountDto.Id;
        //    accountHistoryDto.UserId = dto.UserId;

        //    AccountHistoryService.CreateAccountHistory(accountHistoryDto);
        //}

        private double ConvertFromNativeCents(string nativeCents)
        {
            var ci = CultureInfo.InvariantCulture;
            var numFormatInfo = (NumberFormatInfo)ci.NumberFormat.Clone();
            numFormatInfo.NumberDecimalSeparator = ".";
            return double.Parse(Replace(nativeCents.Insert(nativeCents.Length - 2, "."), numFormatInfo), numFormatInfo);

        }
        private static string Replace(string inputString, NumberFormatInfo numFormatInfo)
        {
            foreach (var separator in separators.Where(inputString.Contains))
            {
                return inputString.Replace(separator, numFormatInfo.NumberDecimalSeparator);
            }
            return inputString;
        }

        private static readonly string[] separators = new [] { ".", "," };

        public List<IEntity> GetBitCoinCollection(string userId)
        {
            return BitCoinRepository["GetBitCoinCollection"].Invoke(userId);
        }

        public IEntity GetBitCoinById(Guid id)
        {
            return BitCoinRepository["GetBitCoinById"].Invoke(id.ToString());
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
    }
}
