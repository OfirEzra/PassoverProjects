using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManager
{
    class Client
    {
        public Client(string username, string password, string name, string lastName, string cardNumber, Int32 clientId)
        {
            Username = username;
            Password = password;
            Name = name;
            LastName = lastName;
            CardNumber = cardNumber;
            ClientId = clientId;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CardNumber { get; set; }
        public Int32 ClientId { get; set; }

    }
}
