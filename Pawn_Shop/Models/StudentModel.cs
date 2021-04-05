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
    class StudentModel
    {
        private DatabaseConnection dbConnection;
        private MySqlConnection connection;

        public StudentModel()
        {
            dbConnection = new DatabaseConnection();
            connection = dbConnection.GetDbConnection();
        }

        public List<Student> getStudent()
        {
            List<Student> students = new List<Student>();

            string query = "SELECT * FROM students";

            using (connection)
            {
                connection.Open();

                MySqlCommand con = new MySqlCommand(query, connection);
                MySqlDataReader mysqlread = con.ExecuteReader(CommandBehavior.CloseConnection);

                while (mysqlread.Read())
                {
                    Student student = new Student(
                        mysqlread.GetInt32(0),
                        mysqlread.GetString(1),
                        mysqlread.GetInt32(2),
                        mysqlread.GetString(3),
                        mysqlread.GetString(4),
                        mysqlread.GetString(5),
                        mysqlread.GetString(6),
                        mysqlread.GetString(7)
                        );

                    students.Add(student);
                }
            }

            return students;
        }
    }
}
