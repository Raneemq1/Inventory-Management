using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class Product
    {

        public string Name { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }


        Product() { }

        public Product(string name, int quantity, double price)
        {

            Name = name;
            Quantity = quantity;
            Price = price;
        }

    

        public override string ToString()
        {
            return $"\nName={Name}\nPrice={Price.ToString()}\nQuantity={Quantity.ToString()}";
        }
    }
}
