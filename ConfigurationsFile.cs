using InventoryManagement.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace InventoryManagement
{
    public class ConfigurationsFile
    {
        public static IProductRepository Read(string filePath)
        {
            if(!File.Exists(filePath)) { throw new FileNotFoundException(); }

            var config= new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(filePath).Build();
            
            bool mongoEnabled = bool.Parse(config["MongoDB:enabled"]);
            if (mongoEnabled) { return new ProductMongoDBRepository(); }

            bool sqlServerEnabled = bool.Parse(config["SqlServerDB:enabled"]);
             if (sqlServerEnabled)  {return new ProductSqlServerDBRepository();} 

            bool memoryEnabled = bool.Parse(config["MemoryDB:enabled"]);
             if (memoryEnabled) { return new ProductMemoryDBRepository(); }

             throw new InvalidCastException("not valid db");

        }
    }
}
