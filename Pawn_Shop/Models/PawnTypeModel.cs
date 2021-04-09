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
    class PawnTypeModel
    {
        private DatabaseConnection dbConnection;
        private MySqlConnection connection;

        public PawnTypeModel()
        {
            this.dbConnection = new DatabaseConnection();
            connection = dbConnection.GetDbConnection();
        }

        public List<PawnType> getPawnTypes(int category_id)
        {
            List<PawnType> pawnTypes = new List<PawnType>();

            string query = "SELECT name FROM types WHERE category_id = " + category_id;

            using (connection)
            {
                connection.Open();

                MySqlCommand con = new MySqlCommand(query, connection);
                MySqlDataReader mysqlread = con.ExecuteReader(CommandBehavior.CloseConnection);

                int pawnTypeNo = 1;
                while (mysqlread.Read())
                {
                    PawnType type = new PawnType(pawnTypeNo, mysqlread.GetString(0));
                    pawnTypes.Add(type);
                    pawnTypeNo++;
                }
            }

            return pawnTypes;
        }
    }
}
