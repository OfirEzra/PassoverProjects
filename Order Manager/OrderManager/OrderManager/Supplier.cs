using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManager
{
    class Supplier
    {
        public Supplier(string username, string password, string companyName, Int32 id)
        {
            Username = username;
            Password = password;
            CompanyName = companyName;
            Id = id;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public Int32 Id { get; set; }
    }
}
