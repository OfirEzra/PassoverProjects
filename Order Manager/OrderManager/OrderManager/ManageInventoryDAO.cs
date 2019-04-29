using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace OrderManager
{
    class ManageInventoryDAO
    {
        private string ConnectionString = "Data Source=.;Initial Catalog=OrderManager;Integrated Security=True";

        public Product FindProduct(string productName)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT * FROM Products WHERE CONVERT(VARCHAR, ProductName)='{productName}'", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Product p = new Product((Int32)reader["Id"], (string)reader["ProductName"], (int)reader["SupplierId"], (float)((double)reader["Price"]), (int)reader["Amount"]);
                        cmd.Connection.Close();
                        return p;
                    }
                    return null;
                }
            }
        }
        public void CreateNewOrder(Product p, int amount, Client c)
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand($"INSERT INTO Orders (ClientId, ItemId, Amount, Price) VALUES ({c.ClientId}, {p.Id}, {amount}, {p.Price * amount})", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    cmd.ExecuteReader();
                    cmd.Connection.Close();
                }
            }
        }
        public void CreateNewProduct(string productName,int supplierId, int amount, float price)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"INSERT INTO Products (ProductName, SupplierId, Price, Amount) VALUES ('{productName}', {supplierId}, {price}, {amount})", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    cmd.ExecuteReader();
                    cmd.Connection.Close();
                }
            }
        }
        public void RemoveAmountFromStock(string productName, int amount)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"UPDATE Products SET Amount = Amount - {amount} WHERE Products.ProductName = '{productName}'", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    cmd.ExecuteReader();
                    cmd.Connection.Close();
                }
            }
        }
        public void AddAmountToStock(string productName, int amount)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"UPDATE Products SET Amount = Amount + {amount} WHERE Products.ProductName = '{productName}'", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    cmd.ExecuteReader();
                    cmd.Connection.Close();
                }
            }
        }
        public string OrderItem(string productName, int amount, Client c)
        {
            Product p = FindProduct(productName);
            if (p != null)
            {
                if(p.Amount >= amount)
                {
                    CreateNewOrder(p, amount, c);
                    RemoveAmountFromStock(productName, amount);
                    return "Order created successfully";
                }
                else
                {
                    return "Not enough in stock";
                }
            }
            else
            {
                return "Item does not exist in stock";
            }

        }
        public void ShoppingHistory(Client c)
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT Products.ProductName, Orders.Amount, Orders.Price FROM Orders JOIN Products on Orders.ItemId = Products.Id WHERE Orders.ClientId = {c.ClientId}", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    double sum = 0;
                    while (reader.Read())
                    {
                        sum += (double)reader["Price"];
                        Console.WriteLine($"Product name: {(string)reader["ProductName"]}, Amount: {(int)reader["Amount"]}, Price: {(double)reader["Price"]}");
                    }
                    Console.WriteLine($"Overall sum spent: {sum}");
                    cmd.Connection.Close();
                }
            }
        }
        public void ShoppingCatalog()
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT ProductName, Price, Amount FROM Products WHERE Amount > 0", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Product name: {(string)reader["ProductName"]}, Price: {(double)reader["Price"]}, Amount in stock: {(int)reader["Amount"]}");
                    }
                    cmd.Connection.Close();
                }
            }
        }
        public void SuppliersProducts(Supplier s)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand($"SELECT ProductName, Price, Amount FROM Products WHERE SupplierId = {s.Id}", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Console.WriteLine($"Product name: {(string)reader["ProductName"]}, Price: {(double)reader["Price"]}, Amount in stock: {(int)reader["Amount"]}");
                    }
                    cmd.Connection.Close();
                }
            }
        }
    }
}
