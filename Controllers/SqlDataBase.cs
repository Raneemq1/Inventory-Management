using InventoryManagement.Models;
using InventoryManagement.Utils;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryManagement.Controllers
{
    public class SqlDataBase
    {
        private SqlConnection _connection = new();
        private string tableName=DBSettings.sqlTableName;

        public SqlDataBase()
        {
            OpenConnection();
        }
        private void OpenConnection()
        {
            try
            {
                SetUpConnectionString();
                _connection.Open();
            }
            catch { throw; }

        }
        private void SetUpConnectionString()
        {
            string connectionString = DBSettings.sqlConnectionString;
            _connection.ConnectionString = connectionString;
        }

        private void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
                try { _connection.Close(); }
                catch { throw; }

        }

        public IEnumerable<Product> GetProducts()
        {
            string query = $"select * from {tableName}";
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
            string query = $"insert into {tableName} values ('{product.Name}',{product.Quantity},{product.Price})";
            try
            {
                SqlCommand cmd = new(query, _connection);
                cmd.ExecuteNonQuery();
                CloseConnection();
               
            }
            catch { throw; }
        }

    }
}
