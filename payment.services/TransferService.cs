using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Payment.Repository.Entity;
using Wr.Common.Repository.Activator;
//using paiment.core;

namespace payment.services
{
    using Profile.Services;

    using Wr.API.GlobalDb;
    using Wr.API.Utility;
    //using payment.internaltransfer;
    
    public class TransferService// :  BllServiceBase
    {
        private HttpContext _context;
        public TransferService()
        {
            
        }
        public TransferService(HttpContext context)
        {
            _context = context;
        }

        public IActivator TransferRepository
        {
            get
            {
                return RepositoryActivator.Instance["transferrepository"].Invoke();
            }
        }

        //public void CreateTransfer(TransferDto dto)
        //{
        //    TransferRepository["CreateTransfer"].Invoke(dto);
        //}

        public IList<IEntity> GetTransferCollection()
        {
            return TransferRepository["GetTransferCollection"].Invoke();
        } 

        //public decimal ContentDonate(Guid userId, Guid authorId, decimal payInGld, string msg)
        //{
        //    var account = new AccountService().GetAccountByUserId(userId.ToString());

        //    var accountEntity = account as AccountEntity;

        //    IEntity user = new UserService().GetUserById(authorId.ToString());

        //    if (user == null)
        //    {
        //        return -1;
        //    }

        //    if (accountEntity == null || accountEntity.GldAmount <= 0)
        //    {
        //        return -1;
        //    }
        //    double accountBalanceInGld = accountEntity.GldAmount;

        //    if (accountBalanceInGld < (double)payInGld)
        //    {
        //        return -1;
        //    }

        //    DateTime createDate = DateTime.UtcNow;
            
        //    TransferDto dto = new TransferDto();
        //    dto.Id = Guid.NewGuid().ToString();
        //    dto.CreateDate = createDate.ToString();
        //    dto.UpdateDate = createDate.ToString();
        //    dto.Amount = (double)payInGld;
        //    dto.Currency = "GLD";
        //    dto.UserIdFrom = userId.ToString();
        //    dto.UserIdTo = authorId.ToString();
        //    dto.Message = msg;
        //    CreateTransfer(dto);
        //    Withdraw(dto);
        //    Deposit(dto);
        //    return 0;
        //}
       

        //private double fee = 0;

        //public void Withdraw(TransferDto dto)
        //{
        //    string userToWithdraw = dto.UserIdFrom;
        //    var account = AccountService.GetAccountByUserId(userToWithdraw) as AccountEntity;
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
        //        accountDto.UserId = userToWithdraw;

        //    }
        //    var accountHistoryDto = new AccountHistoryDto();
        //    accountHistoryDto.Id = Guid.NewGuid().ToString();
        //    accountHistoryDto.PaymentType = ((int)PaymentType.Transfer).ToString();
        //    accountHistoryDto.PaymentMethod = ((int)PaymentMethod.Debit).ToString();
        //    accountHistoryDto.Date = DateTime.UtcNow.ToString();
        //    switch (dto.Currency)
        //    {
        //       case "GLD":
        //            accountDto.GldAmount = accountDto.GldAmount - dto.Amount;
        //            accountHistoryDto.Currency = "GLD";
        //            break;
        //    }
        //    accountHistoryDto.Amount = dto.Amount;
        //    if (account == null)
        //    {
        //        AccountService.CreateAccount(accountDto);
        //    }
        //    else
        //    {
        //        AccountService.UpdateAccount(accountDto);
        //    }
        //    accountHistoryDto.AccontId = accountDto.Id;
        //    accountHistoryDto.UserId = userToWithdraw;

        //    AccountHistoryService.CreateAccountHistory(accountHistoryDto);
        //}

        //public void Deposit(TransferDto dto)
        //{
        //    string userToDeposit = dto.UserIdTo;
        //    var account = AccountService.GetAccountByUserId(userToDeposit) as AccountEntity;
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
        //        accountDto.UserId = userToDeposit;

        //    }
        //    var accountHistoryDto = new AccountHistoryDto();
        //    accountHistoryDto.Id = Guid.NewGuid().ToString();
        //    accountHistoryDto.PaymentType = ((int)PaymentType.Transfer).ToString();
        //    accountHistoryDto.PaymentMethod = ((int)PaymentMethod.Credit).ToString();
        //    accountHistoryDto.Date = DateTime.UtcNow.ToString();
        //    double amount = dto.Amount - fee;
        //    switch (dto.Currency)
        //    {
        //        case "GLD":
        //            accountDto.GldAmount += amount;
        //            accountHistoryDto.Currency = "GLD";
        //            break;
        //    }
        //    accountHistoryDto.Amount = amount;
        //    if (account == null)
        //    {
        //        AccountService.CreateAccount(accountDto);
        //    }
        //    else
        //    {
        //        AccountService.UpdateAccount(accountDto);
        //    }
        //    accountHistoryDto.AccontId = accountDto.Id;
        //    accountHistoryDto.UserId = userToDeposit;

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

        public IEnumerable<IEntity> GetTransferCollectionTo(string userId)
        {
            return TransferRepository["GetTransferCollectionTo"].Invoke(userId);
        }

        public IEnumerable<IEntity> GetTransferCollectionFrom(string userId)
        {
            return TransferRepository["GetTransferCollectionFrom"].Invoke(userId);
        }
    }
}
