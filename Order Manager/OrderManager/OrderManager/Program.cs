using System;
using System.Collections.Generic;

namespace OrderManager
{
    class Program
    {
        static ClientLogic clientLogic = new ClientLogic();
        static SupplierLogic supplierLogic = new SupplierLogic();
        static HistoryDAO historyDAO = new HistoryDAO();
        static ManageInventoryDAO manageInventoryDAO = new ManageInventoryDAO();
        static void Main(string[] args)
        {
            bool check = true;
            while (check)
            {
                Console.WriteLine("Welcome to SmartCo Database Managing App (SDMA) ");
                Console.WriteLine("Choose a Function: \n" +
                    "1. Login Client \n" +
                    "2. Create New Client \n" +
                    "3. Login Supplier \n" +
                    "4. Create New Supplier \n" +
                    "5. Actions History");
                int choice;
                try
                {
                    choice = int.Parse(Console.ReadLine());
                }
                catch (System.FormatException)
                {
                    Console.WriteLine("Please enter a valid choice (1-5) ");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        clientLogic.loginPrompt();
                        break;

                    case 2:
                        clientLogic.CreateUser();
                        break;

                    case 3:
                        supplierLogic.loginPrompt();
                        break;

                    case 4:
                        supplierLogic.CreateSupplier();
                        break;

                    case 5:
                        HistoryDAO.ViewAll();
                        break;

                    default:
                        Console.WriteLine("Please enter a valid choice (1-5) ");
                        break;
                }
            }
        }
    }
}
