﻿using InventoryManagement.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace InventoryManagement.Controller
{
    public class SqlDataBase
    {
        private SqlConnection _connection = new();
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
            string connectionString = "Server=desktop-g7mhp6v\\mssqlserver01;Database=inventory;Trusted_Connection=True;";
            _connection.ConnectionString = connectionString;
        }

        private void CloseConnection()
        {
            if (_connection.State != System.Data.ConnectionState.Closed)
                try { _connection.Close(); }
                catch { throw; }

        }

        public IEnumerable<Product> RetrievAllDataFromTable(string tableName)
        {
            string query = $"select * from {tableName}";
            OpenConnection();
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

            CloseConnection();
        }


        public void InsertProduct(string tableName, Product product)
        {
            string query = $"insert into {tableName} values ('{product.Name}',{product.Quantity},{product.Price})";
            OpenConnection();
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
