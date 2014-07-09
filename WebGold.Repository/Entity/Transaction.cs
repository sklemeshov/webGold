using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace webGold.Repository.Entity
{
   [TableName("Transaction")]
   public class Transaction
    {
        [PrimaryKey, Identity]
        public string Id { get; set; }
        [NullValue]
        public string UserId { get; set; }
        [NullValue]
        public DateTime CreationTime { get; set; }
        [NullValue]
        public DateTime UpdateTime { get; set; }
        [NullValue]
        public double Amount { get; set; }
        [NullValue]
        public int Currency { get; set; }
        [NullValue]
        public double Fee { get; set; }
        [NullValue]
        public float Wrg { get; set; }
        [NullValue]
        public string PaymentProviderId { get; set; }
        [NullValue]
        public string ProviderName { get; set; }
        [NullValue]
        public int State { get; set; }
        [NullValue]
        public int PaymentMethod { get; set; }
        [NullValue]
        public int TransactionType { get; set; }
        [NullValue]
        public int PaymentType { get; set; }

       //---------------------------------------//
        [NullValue]
        public string Email { get; set; }
    }
}
