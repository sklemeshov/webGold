using BLToolkit.Data;

namespace webGold.Repository
{
    namespace MySqlDataProvider
    {
        public class MySqlDbManager : DbManager
        {
            public MySqlDbManager(string connectionString)
                : base(new webGold.Repository.MySqlDataProvider.MySqlDataProvider(), connectionString)
            {
            }
        }
    }

}
