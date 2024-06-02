using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryManagement.Models;

namespace InventoryManagement.Repositories
{
    public class ProductMemoryDBRepository:IProductRepository
    {

        private List<Product>? _products;

        public void SetupDB()=>_products = new();


        public async Task<IEnumerable<Product>> GetProducts() => _products;
        

        public async Task InsertProduct(Product product)=> _products?.Add(product);
        

        public async Task UpdateProduct(string productName, Product product)
        {
            Product? oldProduct = await RetrieveProductByName(productName);
            if (oldProduct is not null)
            {
                oldProduct.Name = product.Name;
                oldProduct.Quantity = product.Quantity;
                oldProduct.Price = product.Price;
            }
            else { throw new Exception("no product found"); }
        }

        public async Task<Product?> RetrieveProductByName(string productName)
        {
        Product? product = _products?.FirstOrDefault(p => p.Name.ToLower() == productName.ToLower());

        return product;
    }

        public async Task<bool> DeleteProduct(string productName)
        {
            Product? product = await RetrieveProductByName(productName);
            if (product is not null)
            {
                _products!.Remove(product);
                return true;
            }
            return false;
               
        }
    }
}
