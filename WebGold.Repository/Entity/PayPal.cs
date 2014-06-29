using System;
using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace webGold.Repository.Entity
{
    [TableName("PayPal")]
    public class PayPal
    {
        [PrimaryKey, Identity]
        public string Id { get; set; }

        [NullValue]
        public string UserId { get; set; }

        [NullValue]
        public string UserIp { get; set; }

        [NullValue]
        public string InternalPaymentId { get; set; }

        [NullValue]
        public DateTime CreateTime { get; set; }

        [NullValue]
        public DateTime UpdateTime { get; set; }

        [NullValue]
        public string PayPalId { get; set; }

        [NullValue]
        public string Intent { get; set; }

        [NullValue]
        public string PayerId { get; set; }

        [NullValue]
        public string State { get; set; }

        [NullValue]
        public string PayerEmail { get; set; }

        [NullValue]
        public string PayerFirstName { get; set; }

        [NullValue]
        public string PayerLastName { get; set; }

        [NullValue]
        public string PayerPhone { get; set; }

        [NullValue]
        public string PayerAddress { get; set; }

        [NullValue]
        public string PaymentMethod { get; set; }

        [NullValue]
        public double Amount { get; set; }

        [NullValue]
        public double Fee { get; set; }

        [NullValue]
        public string CurrencyCode { get; set; }

        [NullValue]
        public string ReceiverInfoType { get; set; }
    }
}
