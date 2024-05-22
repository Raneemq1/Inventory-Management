using System.Collections.Generic;

namespace InventoryManagement.Repositories
{
    public interface IDatabaseRepository<T>
    {
        public void SetupDB();
        public IEnumerable<T> GetProducts();
        public void InsertProduct(T product);
        public void UpdateProduct(string productName, T product);
        public T? RetrieveProductByName(string productName);
        public bool DeleteProduct(string productName);
    }
}
