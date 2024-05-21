using MongoDB.Driver;
using InventoryManagement.Models;
using System.Collections.Generic;
using MongoDB.Bson;
using InventoryManagement.Utils;
namespace InventoryManagement.Controllers
{
    public class MongoDataBase
    {
        private IMongoClient _client;
        private IMongoCollection<BsonDocument> _collection;
        private IMongoDatabase _database;

        public MongoDataBase()
        {
            SetUpMongoDB();
        }
        private void SetUpMongoDB()
        {

            string connectionStrong = DBSettings.mongodbConnectionString;
            string dbName = DBSettings.dataBaseName;
            string collectionName = DBSettings.collectionName;
            try
            {
                _client = new MongoClient(connectionStrong);
                _database = _client.GetDatabase(dbName);
                _collection = _database.GetCollection<BsonDocument>(collectionName);

            }
            catch{ throw; }

        }

        public List<Product> GetProducts()
        {
            var products = new List<Product>();
            var documents = _collection.Find(new BsonDocument()).ToList();
            foreach (var document in documents)
            {
                string productName = (string)document.GetValue(1);
                int productQuantity = (int)document.GetValue(2);
                double productPrice = (double)document.GetValue(3);
                Product product = new(productName, productQuantity, productPrice);
                products.Add(product);

            }
            return products;
        }

        public void InsertProduct(Product product)
        {
            var document = new BsonDocument
        {
          { "productName", product.Name },
          { "productQuantity", product.Quantity },
          { "productPrice", product.Price }};
            _collection.InsertOne(document);
        }
        

    }
}
