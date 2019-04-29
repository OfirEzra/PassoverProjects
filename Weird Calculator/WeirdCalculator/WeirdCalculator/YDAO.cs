using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace WeirdCalculator
{
    class YDAO : DAO
    {
        public void Insert(int num)
        {
            using (SqlConnection conn = new SqlConnection(@"Data Source=.;Initial Catalog=WeirdCalculator;Integrated Security=True"))
            {
                SqlCommand cmd = new SqlCommand($"INSERT INTO Y VALUES({num})", conn);

                cmd.CommandType = CommandType.Text;
                cmd.Connection.Open();

                SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.Default);

                cmd.Connection.Close();
            }
        }
    }
}
