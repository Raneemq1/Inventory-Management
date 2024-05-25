using InventoryManagement.Models;
using System.Collections.Generic;

namespace InventoryManagement.Repositories
{
    public interface IProductRepository
    {
        public void SetupDB();
        public IEnumerable<Product> GetProducts();
        public void InsertProduct(Product product);
        public void UpdateProduct(string productName, Product product);
        public Product? RetrieveProductByName(string productName);
        public bool DeleteProduct(string productName);
    }
}
