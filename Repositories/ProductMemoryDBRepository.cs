using System;
using System.Collections.Generic;
using System.Linq;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
    public class ProductMemoryDBRepository:IProductRepository
    {

        private List<Product> _products;

        public void SetupDB()
        {
            _products = new();
        }

        public IEnumerable<Product> GetProducts()
        {
            return _products;
        }

        public void InsertProduct(Product product)
        {
           _products.Add(product);
        }

        public void UpdateProduct(string productName, Product product)
        {
            Product? oldProduct = RetrieveProductByName(productName);
            if (oldProduct is not null)
            {
                oldProduct.Name = product.Name;
                oldProduct.Quantity = product.Quantity;
                oldProduct.Price = product.Price;
            }
            else { throw new Exception("no product found"); }
        }

        public Product? RetrieveProductByName(string productName)
        {
        Product product = _products.FirstOrDefault(p => p.Name.ToLower() == productName.ToLower());

        return product;
    }

        public bool DeleteProduct(string productName)
        {
            Product product = RetrieveProductByName(productName);
            if (product is not null)
            {
                _products.Remove(product);
                return true;
            }
            return false;
               
        }
    }
}
