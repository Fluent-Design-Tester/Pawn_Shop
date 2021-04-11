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
    class NRCTownshipModel
    {
        private DatabaseConnection dbConnection;
        private MySqlConnection connection;

        public NRCTownshipModel()
        {
            this.dbConnection = new DatabaseConnection();
            connection = dbConnection.GetDbConnection();
        }

        public List<NRCTownship> selectAll(int nrcRegionId)
        {
            List<NRCTownship> nrcTownships = new List<NRCTownship>();

            string query = "SELECT * FROM nrc_townships WHERE nrc_region_id = " + nrcRegionId;

            using (connection)
            {
                connection.Open();

                MySqlCommand con = new MySqlCommand(query, connection);
                MySqlDataReader mysqlread = con.ExecuteReader(CommandBehavior.CloseConnection);

                int nrcTownshipNo = 1;
                while (mysqlread.Read())
                {
                    NRCTownship type = new NRCTownship(mysqlread.GetInt32(0), mysqlread.GetString(1), mysqlread.GetString(2), nrcTownshipNo);
                    nrcTownships.Add(type);
                    nrcTownshipNo++;
                }

                connection.Close();
            }

            return nrcTownships;
        }

        public bool add(int nrcRegionId, string newTownship, string description)
        {
            string query = "INSERT INTO nrc_townships (name, description, nrc_region_id) VALUES ('" + newTownship + "', '" + description + "', '" + nrcRegionId + "');";

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

        public bool update(int nrcTownshipId, string updatedTownship, string updatedDescription)
        {
            string query = "UPDATE nrc_townships SET `name` = '" + updatedTownship + "', `description` = '" + updatedDescription + "' WHERE (`nrc_townships_id` = '" + nrcTownshipId + "');";

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

        public bool delete(int nrcTownshipId)
        {
            string query = "DELETE FROM nrc_townships WHERE `nrc_townships_id` = '" + nrcTownshipId + "';";

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
