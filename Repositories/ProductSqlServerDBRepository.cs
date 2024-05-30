using InventoryManagement.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Threading.Tasks;

namespace InventoryManagement.Repositories
{
    public class ProductSqlServerDBRepository : IProductRepository, IDisposable
    {
        private SqlConnection _connection;

        public void SetupDB()
        {
            _connection = new();
            try
            {
                SetUpConnectionString();
                _connection.Open();
            }
            catch { throw; }
        }
        private void SetUpConnectionString()
        {
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            string connectionString = config["SqlServerDB:connectionString"];
            _connection.ConnectionString = connectionString;
        }

        private void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
                try { _connection.Close(); }
                catch { throw; }

        }
        public void Dispose()
        {
            CloseConnection();
            _connection.Dispose();
        }
        public async Task<IEnumerable<Product>> GetProducts()
        {
            var products = new List<Product>();
            string query = $"select * from Inventory";
            using SqlCommand cmd = new(query, _connection);
            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (reader.Read())
            {
                string productName = reader.GetString("productName");
                int productQuantity = reader.GetInt32("productQuatity");
                double productPrice = (double)reader.GetDecimal("productPrice");
                Product product = new(productName, productQuantity, productPrice);
               products.Add(product);
            }
            return products;
        }

        public async Task InsertProduct(Product product)
        {
            string query = $"insert into Inventory values ('{product.Name}',{product.Quantity},{product.Price})";
            SqlCommand cmd = new(query, _connection);
            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch { throw; }
        }

        public async Task UpdateProduct(string productName, Product product)
        {
            string query = $"update Inventory set productName='{product.Name}',productQuatity={product.Quantity},productPrice={product.Price} where productName='{productName}'";

            try
            {
                SqlCommand cmd = new(query, _connection);
                await cmd.ExecuteNonQueryAsync();
            }
            catch { throw; }
        }

        private async Task<bool> CheckProduct(string productName)
        {
            string query = $"select top 1 * from Inventory where productName='{productName}'";
            try
            {
                SqlCommand cmd = new(query, _connection);
                int n = (int)await cmd.ExecuteScalarAsync();
                return true;
            }
            catch { return false; }
        }

        public async Task<Product?> RetrieveProductByName(string productName)
        {
            if (await CheckProduct(productName))
            {
                string query = $"select top 1 * from Inventory where productName='{productName}'";
                try
                {
                    SqlCommand cmd = new(query, _connection);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                       if (await reader.ReadAsync())
                        {
                            string name = reader.GetString("productName");
                            int quantity = reader.GetInt32("productQuatity");
                            double price = (double)reader.GetDecimal("productPrice");
                            Product product = new(name, quantity, price);
                            return product;
                        }
                        else { return null; }
                }
                catch { throw; }
            }
            return null;
        }
        public async Task<bool> DeleteProduct(string productName)
        {
            if (await CheckProduct(productName))
            {
                string query = $"delete from Inventory where productName='{productName}'";
                try
                {
                    SqlCommand cmd = new(query, _connection);
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
                catch { return false; }
            }
            return false;
        }


    }
}
