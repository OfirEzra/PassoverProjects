using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManager
{
    class ClientLogic
    {
        private ClientDAO clientDAO = new ClientDAO();
        private ManageInventoryDAO manageInventoryDAO = new ManageInventoryDAO();

        //will keep the client that is currently logged on
        private static Client client;
        public void CreateUser()
        {
            bool retry = true;
            while (retry)
            {
                string username;
                string password;
                string firstName;
                string lastName;
                string cardNumber;
                Console.WriteLine("Enter username:");
                username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                password = Console.ReadLine();
                Console.WriteLine("Enter first name:");
                firstName = Console.ReadLine();
                Console.WriteLine("Enter last name:");
                lastName = Console.ReadLine();
                Console.WriteLine("Enter card number:");
                cardNumber = Console.ReadLine();

                string result = clientDAO.Create(username, password, firstName, lastName, cardNumber);
                if (result == "Username already exists")
                {
                    HistoryDAO.Create("Create Client", "Failed " + result);
                    Console.WriteLine(result);
                    Console.WriteLine("Try again? (y/n)");
                    string tmp = Console.ReadLine();
                    if (tmp != "y")
                    {
                        retry = false;
                    }
                }
                else if (result == "Client created successfully")
                {
                    HistoryDAO.Create("Create Client", result);
                    Console.WriteLine(result);
                    retry = false;
                }
            }
        }
        public void loginPrompt()
        {
            bool retry = true;
            while (retry)
            {
                string username;
                string password;
                Console.WriteLine("Enter username:");
                username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                password = Console.ReadLine();

                Client c;

                c = clientDAO.Login(username, password);
                if (c == null)
                {
                    HistoryDAO.Create("Login Client", "Failed " + "Wrong username or password");
                    Console.WriteLine("Wrong username or password");
                    Console.WriteLine("Try again? (y/n)");
                    string tmp = Console.ReadLine();
                    if (tmp != "y")
                    {
                        retry = false;
                    }
                }
                else
                {
                    HistoryDAO.Create("Login Client", "login successful");
                    Console.WriteLine("login successful");
                    client = c;
                    clientMenu();
                    retry = false;
                }
            }
        }
        public void clientMenu()
        {
            bool check = true;
            while (check)
            {
                Console.WriteLine($"Hello {client.Name} what would you like to do?");
                Console.WriteLine("Choose a Function: \n" +
                    "1. View all of my orders \n" +
                    "2. View all Products \n" +
                    "3. Order a product \n" +
                    "4. Logout");
                int choice;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Please enter a valid choice (1-4) \n");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        manageInventoryDAO.ShoppingHistory(client);
                        HistoryDAO.Create("Client requested order history", "");
                        break;

                    case 2:
                        manageInventoryDAO.ShoppingCatalog();
                        HistoryDAO.Create("Client requested Shopping Catalog", "");
                        break;

                    case 3:
                        OrderItem();
                        break;
                    case 4:
                        check = false;
                        HistoryDAO.Create("Client logged out", "");
                        client = null;
                        break;
                    default:
                        Console.WriteLine("Please enter a valid choice (1-4) ");
                        break;
                }
            }
        }
        public void OrderItem()
        {
            string productName;
            int amount;
            Console.WriteLine("Enter product name:");
            productName = Console.ReadLine();
            Console.WriteLine("Enter amount:");
            amount = int.Parse(Console.ReadLine());
            string result = manageInventoryDAO.OrderItem(productName, amount, client);
            if (result == "Order created successfully")
            {
                Console.WriteLine(result);
                HistoryDAO.Create("Order Item", result);
            }
            else if (result == "Not enough in stock")
            {
                Console.WriteLine(result);
                HistoryDAO.Create("Order Item", "Failed " + result);
            }
            else if (result == "Item does not exist in stock")
            {
                Console.WriteLine(result);
                HistoryDAO.Create("Order Item", "Failed " + result);
            }
        }
    }
}
