using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace Payment.Repository.Entity
{
    [TableName("PaymentHistory")]
    public class PaymentHistory
    {
        // Id,UserId,AccountId,Date,Currency,Amount,PaymentType,PaymentMethod,TransactionType,TransactionStatus,ReceivedEmail//
        [PrimaryKey, Identity]
        public string Id { get; set; }

        [NullValue]
        public string UserId { get; set; }

        [NullValue]
        public string AccountId { get; set; }

        [NullValue]
        public DateTime Date { get; set; }

        [NullValue]
        public string Currency { get; set; }

        [NullValue]
        public double Amount { get; set; }

        [NullValue]
        public string PaymentType { get; set; }

        [NullValue]
        public string PaymentMethod { get; set; }

        [NullValue]
        public string TransactionType { get; set; }

        [NullValue]
        public string TransactionStatus { get; set; }

        [NullValue]
        public string ReceivedEmail { get; set; }

        //[Nullable]
        //public string TransactionTime
        //{
        //    get
        //    {
        //        if(Date != 0)
        //        {
        //           return new DateTime(Date).ToString();
        //        }
        //        return string.Empty;
        //    }
        //}
    }
}
