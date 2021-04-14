using MySql.Data.MySqlClient;
using Pawn_Shop.Database;
using Pawn_Shop.Dto;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

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
                    PawnType type = new PawnType(mysqlread.GetInt32(0), mysqlread.GetString(1), mysqlread.GetString(2), pawnTypeNo);
                    pawnTypes.Add(type);
                    pawnTypeNo++;
                }

                connection.Close();
            }

            return pawnTypes;
        }

        public bool add(int categoryId, string name, string shortName)
        {
            string query = "INSERT INTO types (name, short_name, category_id) VALUES ('" + name + "', '" + shortName + "', '"  + categoryId + "');";

            using (connection)
            {
                MySqlCommand con = new MySqlCommand(query, connection);
                try
                {
                    connection.Open();
                    int rowsAffected = con.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        connection.Close();
                        return true;
                    }
                }
                catch (MySqlException e)
                {
                    connection.Close();
                    Debug.WriteLine(e.Message);
                    return false;
                }
            }
            return false;
        }

        public bool update(int typeId, string name, string shortName)
        {
            string query = "UPDATE types SET `name` = '" + name + "', `short_name` = '" + shortName + "' WHERE (`type_id` = '" + typeId + "');";

            using (connection)
            {
                MySqlCommand con = new MySqlCommand(query, connection);
                try
                {
                    connection.Open();
                    int rowsAffected = con.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        connection.Close();
                        return true;
                    }
                }
                catch (MySqlException e)
                {
                    connection.Close();
                    Debug.WriteLine(e.Message);
                    return false;
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
                    connection.Close();
                    return true;
                }
            }
            return false;
        }
    }
}
