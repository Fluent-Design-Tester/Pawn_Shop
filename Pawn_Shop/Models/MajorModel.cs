using MySql.Data.MySqlClient;
using Pawn_Shop.Database;
using Pawn_Shop.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pawn_Shop.Models
{
    class MajorModel
    {
        private DatabaseConnection dbConnection;
        private MySqlConnection connection;

        public MajorModel()
        {
            dbConnection = new DatabaseConnection();
            connection = dbConnection.GetDbConnection();
        }

        public List<Major> getMajors()
        {
            List<Major> majors = new List<Major>();

            string query = "SELECT major_id, short_name FROM majors WHERE university_id = 1 LIMIT 5";

            using (connection)
            {
                connection.Open();

                MySqlCommand con = new MySqlCommand(query, connection);
                MySqlDataReader mysqlread = con.ExecuteReader(CommandBehavior.CloseConnection);

                while (mysqlread.Read())
                {
                    Major major = new Major(
                        mysqlread.GetInt32(0),
                        mysqlread.GetString(1)
                        );

                    majors.Add(major);
                }
            }

            return majors;
        }
    }
}
