using DataBindingExampleCRUD.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBindingExampleCRUD.Data
{
    class ProductRepository
    {
        private static readonly string connString = @"Server=tcp:skeena.database.windows.net,1433;
                                                      Initial Catalog=comp2614;
                                                      User ID=student;
                                                      Password=93nu5/nrCKX;
                                                      Encrypt=True;
                                                      TrustServerCertificate=False;
                                                      Connection Timeout=30;";

        private static readonly string productTableName = "Product838629";

        public static int AddProduct(Product product)
        {
            int rowsAffected;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = $@"INSERT INTO {productTableName}
                                 (Quantity, Sku, Description, Cost, Taxable)
                                  VALUES (@quantity, @sku, @description, @cost, @taxable)";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@quantity", product.Quantity);

                    if (product.Sku != null)
                    {
                        cmd.Parameters.AddWithValue("@sku", product.Sku);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@sku", DBNull.Value);
                    }
                    
                    if (product.Description != null)
                    {
                        cmd.Parameters.AddWithValue("@description", product.Description);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@description", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@cost", product.Cost);
                    cmd.Parameters.AddWithValue("@taxable", product.Taxable);

                    conn.Open();

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }


        public static int UpdateProduct(Product product)
        {
            int rowsAffected;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = $@"UPDATE {productTableName}
                                  SET Quantity = @quantity,
                                      Sku = @sku,
                                      Description = @description,
                                      Cost = @cost,
                                      Taxable = @taxable
                                  WHERE ProductId = @productId";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = conn;

                    cmd.Parameters.AddWithValue("@quantity", product.Quantity);

                    if (product.Sku != null)
                    {
                        cmd.Parameters.AddWithValue("@sku", product.Sku);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@sku", DBNull.Value);
                    }

                    if (product.Description != null)
                    {
                        cmd.Parameters.AddWithValue("@description", product.Description);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@description", DBNull.Value);
                    }

                    cmd.Parameters.AddWithValue("@cost", product.Cost);
                    cmd.Parameters.AddWithValue("@taxable", product.Taxable);
                    cmd.Parameters.AddWithValue("@productId", product.ProductId);

                    conn.Open();

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }

        public static ProductCollection GetProducts()
        {
            ProductCollection products;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = $@"SELECT ProductId, Quantity, Sku, Description, Cost, Taxable
                                  FROM {productTableName}
                                  ORDER BY Sku";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = conn;

                    conn.Open();

                    products = new ProductCollection();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        int productId;
                        int quantity;
                        string sku;
                        string description;
                        decimal cost;
                        bool taxable;

                        while (reader.Read())
                        {
                            productId = (int)reader["ProductId"];
                            quantity = (int)reader["Quantity"];
                            sku = reader["Sku"] as string;

                            if (!reader.IsDBNull(3))
                            {
                                description = reader["Description"] as string;
                            }
                            else
                            {
                                description = null;
                            }

                            cost = (decimal)reader["Cost"];
                            taxable = (bool)reader["Taxable"];

                            products.Add(new Product
                            {
                                 ProductId = productId
                               , Quantity = quantity
                               , Sku = sku
                               , Description = description
                               , Cost = cost
                               , Taxable = taxable
                            });
                        }
                    }
                }
            }

            return products;
        }

        public static int DeleteProduct(Product product)
        {
            int rowsAffected;

            using (SqlConnection conn = new SqlConnection(connString))
            {
                string query = $@"DELETE {productTableName}
                                  WHERE ProductId = @productId";

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = query;
                    cmd.Connection = conn;
                    cmd.Parameters.AddWithValue("@productId", product.ProductId);

                    conn.Open();

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            return rowsAffected;
        }
    }
}
