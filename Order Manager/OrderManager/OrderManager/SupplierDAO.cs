using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace OrderManager
{
    class SupplierDAO
    {
        private string ConnectionString = "Data Source=.;Initial Catalog=OrderManager;Integrated Security=True";
        public string Create(string username, string password, string companyName)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                //test to see if username exists
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Suppliers WHERE CONVERT(VARCHAR, Username)='{username}'", conn))
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
                //insert into the table
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO Suppliers (Username, Password, CompanyName) VALUES(" +
                    $"'{username}', '{password}', '{companyName}')", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    cmd.Connection.Close();

                    return "Supplier created successfully";
                }
            }
        }

        public Supplier Login(string username, string password)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Suppliers WHERE CONVERT(VARCHAR, Username)='{username}' AND CONVERT(VARCHAR, Password)='{password}'", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    Supplier s;
                    if (reader.Read())
                    {
                        s = new Supplier((string)reader["Username"], (string)reader["Password"], (string)reader["CompanyName"],(Int32)reader["Id"]);
                        cmd.Connection.Close();
                        return s;
                    }
                    cmd.Connection.Close();  
                    return null;
                }
            }
        }
    }
}
