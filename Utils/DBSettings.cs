namespace InventoryManagement.Utils
{
    public class DBSettings
    {
        public const string sqlConnectionString = "Server=desktop-g7mhp6v\\mssqlserver01;Database=inventory;Trusted_Connection=True;";
        public const string mongodbConnectionString = "mongodb+srv://mongoDB:mongoPassword@inventorycluster.dqj4pz7.mongodb.net/?retryWrites=true&w=majority&appName=InventoryCluster";
        public const string sqlTableName = "Inventory";
        public const string dataBaseName = "inventory";
        public const string collectionName = "Inventory";
    }
}
