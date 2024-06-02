using MongoDB.Bson.Serialization.Attributes;

namespace InventoryManagement.Models
{
    public class Product
    {
        [BsonId]
        public object Id { get; set; }
        [BsonElement("productName")]
        public string Name { get; set; }
        [BsonElement("productQuantity")]
        public int Quantity { get; set; }
        [BsonElement("productPrice")]
        public double Price { get; set; }

        public Product(string name, int quantity, double price)
        {

            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public override string ToString()
        {
            return $"Name={Name}\nPrice={Price.ToString()}\nQuantity={Quantity.ToString()}\n";
        }
    }
}
