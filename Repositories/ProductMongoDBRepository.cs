using MongoDB.Driver;
using InventoryManagement.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Threading.Tasks;
namespace InventoryManagement.Repositories
{
    public class ProductMongoDBRepository : IProductRepository
    {
        private IMongoClient _client;
        private IMongoCollection<Product> _collection;
        private IMongoDatabase _database;


        public void SetupDB()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string connectionStrong = config["MongoDB:connectionString"];

            try
            {
                _client = new MongoClient(connectionStrong);
                _database = _client.GetDatabase("inventory");
                _collection = _database.GetCollection<Product>("Inventory");

            }
            catch { throw; }

        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = new List<Product>();
            var documents = await _collection.Find(_ => true).ToListAsync();
            foreach (var document in documents)
            {
                string productName = document.Name;
                int productQuantity = document.Quantity;
                double productPrice = document.Price;
                Product product = new(productName, productQuantity, productPrice);
                products.Add(product);

            }
            return products;
        }

        public async Task InsertProduct(Product product) => await _collection.InsertOneAsync(product); 


        public async Task UpdateProduct(string productName, Product product)
        {
            var filter = Builders<Product>.Filter.Eq(d => d.Name, product.Name);

            try
            {
                await _collection.FindOneAndReplaceAsync(filter, product);
            }
            catch { throw; }

        }

        public async Task<Product?> RetrieveProductByName(string productName)
        {
            var filter = Builders<Product>.Filter.Eq(d => d.Name, productName);
            var document = await _collection.Find(filter).FirstOrDefaultAsync();
            if (document != null)
            {
                string name = document.Name;
                int quantity = document.Quantity;
                double price = document.Price;

                return new Product(name, quantity, price); 
            }

            return null;
        }
        public async Task<bool> DeleteProduct(string productName)
        {
            var filter = Builders<Product>.Filter.Eq(d => d.Name, productName);
            var result = await _collection.DeleteOneAsync(filter);
            return result.DeletedCount > 0;

        }


    }
}
