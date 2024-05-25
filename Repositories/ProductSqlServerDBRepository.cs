using InventoryManagement.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace InventoryManagement.Repositories
{
    public class ProductSqlServerDBRepository:IProductRepository,IDisposable
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
        public IEnumerable<Product> GetProducts()
        { 
            string query = $"select * from Inventory";
            using (SqlCommand cmd = new(query, _connection))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string productName = reader.GetString("productName");
                        int productQuantity = reader.GetInt32("productQuatity");
                        double productPrice = (double)reader.GetDecimal("productPrice");
                        Product product = new(productName, productQuantity, productPrice);
                        yield return product;
                    }
                }
            }

        }


        public void InsertProduct(Product product)
        {
            string query = $"insert into Inventory values ('{product.Name}',{product.Quantity},{product.Price})";
            try
            {
                SqlCommand cmd = new(query, _connection);
                cmd.ExecuteNonQuery();

            }
            catch { throw; }
        }

        public void UpdateProduct(string productName, Product product)
        {
            string query = $"update Inventory set productName='{product.Name}',productQuatity={product.Quantity},productPrice={product.Price} where productName='{productName}'";

            try
            {
                SqlCommand cmd = new(query, _connection);
                cmd.ExecuteNonQuery();
            }
            catch { throw; }

        }

        private bool CheckProduct(string productName)
        {
            string query = $"select top 1 * from Inventory where productName='{productName}'";
            try
            {
                SqlCommand cmd = new(query, _connection);
                int n = (int)cmd.ExecuteScalar();
                return true;
            }
            catch { return false; }

        }

        public Product? RetrieveProductByName(string productName)
        {
            if (CheckProduct(productName))
            {
                string query = $"select top 1 * from Inventory where productName='{productName}'";
                try
                {
                    SqlCommand cmd = new(query, _connection);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            string name = reader.GetString("productName");
                            int quantity = reader.GetInt32("productQuatity");
                            double price = (double)reader.GetDecimal("productPrice");
                            Product product = new(name, quantity, price);
                            return product;
                        }
                        else { return null; }
                    }


                }
                catch { throw; }
            }
            return null; 
        }
        public bool DeleteProduct(string productName)
        {
            if (CheckProduct(productName))
            {
                string query = $"delete from Inventory where productName='{productName}'";
                try
                {
                    SqlCommand cmd = new(query, _connection);
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch { return false; }
            }
            return false; 
        }

      
    }
}
