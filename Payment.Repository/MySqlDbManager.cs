namespace Payment.Repository
{
    namespace MySqlDataProvider
    {
        using BLToolkit.Data;
        using System;

        public class MySqlDbManager : DbManager
        {
            public MySqlDbManager(string connectionString)
                : base(new MySqlDataProvider(), connectionString)
            {
            }
        }
    }

}
