using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Pawn_Shop.Database
{
    class DatabaseConnection
    {
        private string _connectionString = "server=localhost;user id=root;password=root;database=pawn_shop";

        public MySqlConnection GetDbConnection()
        {
            return new MySqlConnection(_connectionString);
        }
    }
}
