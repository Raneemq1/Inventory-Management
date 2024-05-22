using InventoryManagement.Models;
using InventoryManagement.Repositories;
using System.Collections.Generic;


namespace InventoryManagement.Controllers
{
    public class DataBaseController
    {
        private readonly IDatabaseRepository<Product> _repository;
        public DataBaseController(IDatabaseRepository<Product> repository)
        {
            _repository = repository;
            _repository.SetupDB();
        }
    
        public bool DeleteProduct(string productName)
        {
            return _repository.DeleteProduct(productName);
        }

        public IEnumerable<Product> GetProducts()
        {
            return _repository.GetProducts();
        }

        public void InsertProduct(Product product)
        {
            _repository.InsertProduct(product);
        }

        public Product? RetrieveProductByName(string productName)
        {
            return _repository.RetrieveProductByName(productName);
        }

        public void UpdateProduct(string productName, Product product)
        {
            _repository.UpdateProduct(productName, product);
        }
    }
}
