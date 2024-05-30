using InventoryManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Repositories
{
    public interface IProductRepository
    {
        public void SetupDB();
        public  Task<IEnumerable<Product>> GetProducts();
        public  Task InsertProduct(Product product);
        public  Task UpdateProduct(string productName, Product product);
        public  Task<Product?> RetrieveProductByName(string productName);
        public  Task<bool> DeleteProduct(string productName);
    }
}
