using System;
using System.Collections.Generic;
using System.Text;

namespace OrderManager
{
    class Product
    {
        public Product(int id, string productName, int supplierId, float price, int amount)
        {
            Id = id;
            ProductName = productName;
            SupplierId = supplierId;
            Price = price;
            Amount = amount;
        }

        public Int32 Id { get; set; }
        public string ProductName { get; set; }
        public int SupplierId { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }

    }
}
