using BLToolkit.DataAccess;
using BLToolkit.Mapping;

namespace Payment.Repository.Entity
{
   [TableName("Account")]
   public class Account
    {
       [PrimaryKey, Identity]
       public string Id { get; set; }
       [NullValue]
       public string UserId { get; set; }
       [NullValue]
       public double GldAmount { get; set; }
       [NullValue]
       public double UsdAmount { get; set; }
    }
}
