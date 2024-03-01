using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class Product
    {
        //Define class properties 


        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }

        //Define class constructors

        Product() { }

        public Product(string name, int quantity, double price)
        {

            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public void UpdateProductName(Product product, string name)
        {
            product.Name = name;
        }

        public void UpdateProductQuantity(Product product, int quantity)
        {
            product.Quantity = quantity;

        }
        public void UpdateProductPrice(Product product, double price)
        {

            product.Price = price;
        }
    }
}
