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
        public string InternalPaymentId { get; set; }
        [NullValue]
        public string Intent { get; set; }
        [NullValue]
        public string PayerId { get; set; }
        [NullValue]
        public int State { get; set; }
    }
}
