using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WeirdCalculator
{
    class ResultDAO
    {
        public void Cross()
        {
            using(SqlConnection conn = new SqlConnection((@"Data Source=.;Initial Catalog=WeirdCalculator;Integrated Security=True")))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO Results (X, Operation, Y) SELECT X.X, Operations.Operation, Y.Y FROM X CROSS JOIN Operations CROSS JOIN Y", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                cmd.ExecuteReader();
                cmd.Connection.Close();
            }
        }
        public void Calculate()
        {
            Addition();
            Substraction();
            Division();
            Multiplication();
        }
        private void Addition()
        {
            using (SqlConnection conn = new SqlConnection((@"Data Source=.;Initial Catalog=WeirdCalculator;Integrated Security=True")))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE Results SET Result=X+Y WHERE Operation='+'", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                cmd.ExecuteReader();
                cmd.Connection.Close();
            }
        }
        private void Substraction()
        {
            using (SqlConnection conn = new SqlConnection((@"Data Source=.;Initial Catalog=WeirdCalculator;Integrated Security=True")))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE Results SET Result=X-Y WHERE Operation='-'", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                cmd.ExecuteReader();
                cmd.Connection.Close();
            }
        }
        private void Multiplication()
        {
            using (SqlConnection conn = new SqlConnection((@"Data Source=.;Initial Catalog=WeirdCalculator;Integrated Security=True")))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE Results SET Result=X*Y WHERE Operation='*'", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                cmd.ExecuteReader();
                cmd.Connection.Close();
            }
        }
        private void Division()
        {
            using (SqlConnection conn = new SqlConnection((@"Data Source=.;Initial Catalog=WeirdCalculator;Integrated Security=True")))
            {
                SqlCommand cmd = new SqlCommand($"UPDATE Results SET Result=X/Y WHERE Operation='/' AND Y<>0", conn);
                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();
                cmd.ExecuteReader();
                cmd.Connection.Close();
            }
        }
    }
}
