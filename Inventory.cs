using System;
using System.Collections.Generic;
using System.Linq;


namespace InventoryManagement
{
    public class Inventory
    {

        List<Product> products = new();

        public void AddProduct(Product product)
        {
            products.Add(product);
        }


        public void RemoveProduct(Product product)
        {
            products.Remove(product);
        }

        public List<Product> ViewAllProducts()
        {
            return products;
        }

        public Product SearchProduct(String name)
        {
            Product product = products.FirstOrDefault(p => p.Name.ToLower() == name.ToLower());

            return product;
        }

        public bool CheckProduct(string name)
        {
            var product = SearchProduct(name);
            return product is not null;
        }


    }
}
