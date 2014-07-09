using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace webGold.Repository.Entity
{
   [TableName("Account")]
   public class Account
    {
       [PrimaryKey, Identity]
       public string Id { get; set; }
       [NullValue]
       public string UserId { get; set; }
       [NullValue]
       public float Wrg { get; set; }
    }
}
