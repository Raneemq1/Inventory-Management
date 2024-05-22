using MongoDB.Driver;
using InventoryManagement.Models;
using System.Collections.Generic;
using MongoDB.Bson;
using InventoryManagement.Utils;
using System.Linq;
namespace InventoryManagement.Repositories
{
    public class MongoDataBaseRepository : IDatabaseRepository<Product>
    {
        private IMongoClient _client;
        private IMongoCollection<BsonDocument> _collection;
        private IMongoDatabase _database;


        public void SetupDB()
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
            catch { throw; }

        }

        public IEnumerable<Product> GetProducts()
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

        public void UpdateProduct(string productName, Product product)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("productName", productName);
            var update = Builders<BsonDocument>.Update.Set("productName", product.Name).Set("productQuantity", product.Quantity).Set("productPrice", product.Price); ;

            try
            {
                _collection.UpdateOne(filter, update);
            }
            catch { throw; }


        }

        public Product? RetrieveProductByName(string productName)
        {


            var filter = Builders<BsonDocument>.Filter.Eq("productName", productName);
            var document = _collection.Find(filter).First();
            if (document != null)
            {
                string name = document.GetValue("productName").AsString;
                int quantity = document.GetValue("productQuantity").AsInt32;
                double price = document.GetValue("productPrice").AsDouble;

                return new Product(name, quantity, price); ;
            }

            return null;
        }
        public bool DeleteProduct(string productName)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("productName", productName);
            var result= _collection.DeleteOne(filter);
            return result.DeletedCount>0;

        }


    }
}
