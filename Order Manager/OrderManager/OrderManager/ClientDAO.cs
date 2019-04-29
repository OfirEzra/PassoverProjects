using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace OrderManager
{
    class ClientDAO
    {
        private string ConnectionString = "Data Source=.;Initial Catalog=OrderManager;Integrated Security=True";
        public string Create(string username, string password, string firstName, string lastName, string cardNumber)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                //test to see if username exists
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Clients WHERE CONVERT(VARCHAR, Username)='{username}'", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(); 
                    if (reader.Read())
                    {
                        cmd.Connection.Close();
                        return "Username already exists";
                    }
                    cmd.Connection.Close();
                }
                using(SqlCommand cmd = new SqlCommand($"INSERT INTO Clients (Username, Password, FirstName, LastName, CardNumber) VALUES(" +
                        $"'{username}', '{password}', '{firstName}', '{lastName}', '{cardNumber}')", conn))
                {
                    //insert into the table
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    cmd.Connection.Close();
                }
                return "Client created successfully";
            }
        }

        public Client Login(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Clients WHERE CONVERT(VARCHAR, Username)='{username}' AND CONVERT(VARCHAR, Password)='{password}'", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    Client c;
                    if (reader.Read())
                    {
                        c = new Client((string)reader["Username"], (string)reader["Password"], (string)reader["FirstName"], (string)reader["Lastname"], (string)reader["Cardnumber"],(Int32)reader["Id"]);
                        cmd.Connection.Close();
                        return c;
                    }
                    cmd.Connection.Close();  
                    return null;
                }
            }
        }
    }
}
