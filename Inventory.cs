using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement
{
    public class Inventory
    {

        List<Product> products = new List<Product>();

        //Define Constructor 
        public Inventory() { }

        //Add new Product to the List 

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        //Delete Product from the List 

        public void RemoveProduct(Product product)
        {
            products.Remove(product);
        }

        //Display All Products in the List 
        public void ViewAllProducts()
        {
            foreach (var product in products)
            {
                Console.WriteLine("Name={0}\tQuantity={1}\tPrice={2}$", product.Name, product.Quantity, product.Price);
            }
        }

        //Search for a Product in the List using It's Name
        public Product SearchProduct(String name)
        {
            Product searchedProduct = null;
            foreach (var product in products)
            {
                if (product.Name == name) searchedProduct = product;
            }
            return searchedProduct;
        }


        //Update Product in the List 

        public bool CheckProduct(string name)
        {
            var product = SearchProduct(name);
            //check if its available in the inventory 
            if (product != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


    }
}
