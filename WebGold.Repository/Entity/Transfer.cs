using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace webGold.Repository.Entity
{
   [TableName("Transfer")]
   public class Transfer
    {
       [PrimaryKey, Identity]
       public string Id { get; set; }
       [NullValue]
       public string PayerId { get; set; }
       [NullValue]
       public string RecipientId { get; set; }
       [NullValue]
       public int State { get; set; }
    }
}
