using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManager
{
    class SupplierLogic
    {
        private SupplierDAO supplierDAO = new SupplierDAO();
        private ManageInventoryDAO manageInventoryDAO = new ManageInventoryDAO();

        //will keep the client that is currently logged on
        private static Supplier supplier;
        public void CreateSupplier()
        {
            bool retry = true;
            while (retry)
            {
                string username;
                string password;
                string companyName;
                Console.WriteLine("Enter username:");
                username = Console.ReadLine();
                Console.WriteLine("Enter password:");
                password = Console.ReadLine();
                Console.WriteLine("Enter company name:");
                companyName = Console.ReadLine();

                string result = supplierDAO.Create(username, password, companyName);
                if (result == "Username already exists")
                {
                    HistoryDAO.Create("Create Supplier", "Failed " + result);
                    Console.WriteLine(result);
                    Console.WriteLine("Try again? (y/n)");
                    string tmp = Console.ReadLine();
                    if (tmp != "y")
                    {
                        retry = false;
                    }
                }
                else if (result == "Supplier created successfully")
                {
                    HistoryDAO.Create("Create Supplier", result);
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

                Supplier s;
               
                s = supplierDAO.Login(username, password);
                if (s == null)
                {
                    HistoryDAO.Create("Login Supplier", "Failed " + "Wrong username or password");
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
                    HistoryDAO.Create("Login Supplier", "login successful");
                    Console.WriteLine("login successful");
                    supplier = s;
                    supplierMenu();
                    retry = false;
                }
            }
        }
        public void supplierMenu()
        {
            bool check = true;
            while (check)
            {
                Console.WriteLine($"Hello {supplier.CompanyName} what would you like to do?");
                Console.WriteLine("Choose a Function: \n" +
                    "1. Manage my products\n" +
                    "2. View all of my Products in the store \n" +
                    "3. Logout");
                int choice;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Please enter a valid choice (1-2) ");
                    break;
                }

                switch (choice)
                {
                    case 1:
                        AddItemToShop();
                        break;

                    case 2:
                        manageInventoryDAO.SuppliersProducts(supplier);
                        HistoryDAO.Create("Supplier requested to view his products", "");
                        break;
                    case 3:
                        supplier = null;
                        check = false;
                        HistoryDAO.Create("Supplier logged out", "");
                        break;

                }
            }
        }
        public void AddItemToShop()
        {
            bool check = true;
            while (check)
            {
                Console.WriteLine("Choose an option (1-2):\n" +
                "1. Add a product to store inventory\n" +
                "2. Add amount to an existing product");
                int choice;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Please enter a valid choice (1-2) ");
                    continue;
                }
                if (choice > 2)
                {
                    Console.WriteLine("Please enter a valid choice (1-2) ");
                }
                string productName;
                Console.WriteLine("Enter Product Name:");
                productName = Console.ReadLine();
                Product p = manageInventoryDAO.FindProduct(productName);

                if (choice == 1)
                {
                    if (p == null)
                    {
                        float price;
                        int amount;
                        Console.WriteLine("Enter Price:");
                        price = float.Parse(Console.ReadLine());
                        Console.WriteLine("Enter Amount:");
                        amount = int.Parse(Console.ReadLine());
                        manageInventoryDAO.CreateNewProduct(productName, supplier.Id, amount, price);
                        string result = "Product added to the shop";
                        Console.WriteLine(result);
                        HistoryDAO.Create("Add new item to the shop", result);
                        check = false;
                    }
                    else
                    {
                        string result = "Item already exists";
                        HistoryDAO.Create("Add new item to the shop", "Failed, " + result);
                        Console.WriteLine(result + "\n Returning back to menu...");
                    }

                }
                else if (choice == 2)
                {
                    if (p.SupplierId == supplier.Id)
                    {
                        int amount;
                        Console.WriteLine("Enter Amount:");
                        amount = int.Parse(Console.ReadLine());
                        manageInventoryDAO.AddAmountToStock(productName, amount);
                        string result = "Product amount increased";
                        HistoryDAO.Create("Add amount to product", result);
                        check = false;
                    }
                    else
                    {
                        string result = "Item already exists from another supplier";
                        HistoryDAO.Create("Add amount to product", "Failed, " + result);
                        Console.WriteLine(result + "\n Returning back to menu...");
                    }
                }
            }
        }
    }
}
