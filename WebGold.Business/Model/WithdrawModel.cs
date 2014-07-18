using System;

namespace webGold.Business.Model
{
    public class WithdrawModel
    {
        public void AddUserId(string userId, string recipientId)
        {
            UserId = userId;
            RecipientId = recipientId;
        }
        internal string RecipientId { get; set; }
        internal string UserId { get; set; }
        public string USDAmount { get; set; } 
        public string WRGAmount { get; set; } 
        public string Email { get; set; }
        public bool IsTransferCanseled { get; set; }
        public string ErrorType { get; set; }
        public string ErrorMessage { get; set; }
        public double USD { get; set; }
        public double WRG { get; set; } 
    }

    public enum ErrorType
    {
        maximumLimit,
        minimumLimit,
        received,
        errorMessage,
        dayLimit,
        haventMoney,
        incorrectEmail
    }
}