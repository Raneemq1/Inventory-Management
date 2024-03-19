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

        //Return All Products in the List 
        public List<Product> ViewAllProducts()
        {
            return products;
        }

        //Search for a Product in the List using It's Name
        public Product SearchProduct(String name)
        {
            Product product=products.FirstOrDefault(p=>p.Name.ToLower()==name.ToLower());
  
            return product;
        }


        //Update Product in the List 
        public bool CheckProduct(string name)
        {
            var product = SearchProduct(name);
            //check if its available in the inventory 
            return product is not null;
        }


    }
}
