using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace OrderManager
{
    class HistoryDAO
    {
        private static string ConnectionString = "Data Source=.;Initial Catalog=OrderManager;Integrated Security=True";

        public static void Create(string action, string result)
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO History (Time, Action, Result) VALUES(" +
                    $"'{DateTime.Now.ToString()}', '{action}', '{result}')", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    cmd.Connection.Close();
                }
            }
        }
        public static void ViewAll()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM History", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Action ID: {(Int32)reader["Id"]},{(string)reader["Time"]},{(string)reader["Action"]},{(string)reader["Result"]}");
                    }
                    cmd.Connection.Close();
                }
            }
        }
    }
}
