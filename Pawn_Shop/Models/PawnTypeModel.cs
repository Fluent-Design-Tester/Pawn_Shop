using MySql.Data.MySqlClient;
using Pawn_Shop.Database;
using Pawn_Shop.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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

        public List<PawnType> selectAll(int categoryId)
        {
            List<PawnType> pawnTypes = new List<PawnType>();

            string query = "SELECT * FROM types WHERE category_id = " + categoryId;

            using (connection)
            {
                connection.Open();

                MySqlCommand con = new MySqlCommand(query, connection);
                MySqlDataReader mysqlread = con.ExecuteReader(CommandBehavior.CloseConnection);

                int pawnTypeNo = 1;
                while (mysqlread.Read())
                {
                    PawnType type = new PawnType(mysqlread.GetInt32(0), mysqlread.GetString(1), pawnTypeNo);
                    pawnTypes.Add(type);
                    pawnTypeNo++;
                }
            }

            return pawnTypes;
        }

        public Boolean add(int categoryId, string newType)
        {
            string query = "INSERT INTO types (name, category_id) VALUES ('" + newType + "', '" + categoryId + "');";

            using (connection)
            {
                connection.Open();

                MySqlCommand con = new MySqlCommand(query, connection);
                int rowsAffected = con.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool update(int typeId, string updatedText)
        {
            string query = "UPDATE types SET `name` = '" + updatedText + "' WHERE (`type_id` = '" + typeId + "');";

            using (connection)
            {
                connection.Open();

                MySqlCommand con = new MySqlCommand(query, connection);
                int rowsAffected = con.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public bool delete(int typeId)
        {
            string query = "DELETE FROM types WHERE `type_id` = '" + typeId + "';";

            using (connection)
            {
                connection.Open();

                MySqlCommand con = new MySqlCommand(query, connection);
                int rowsAffected = con.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
