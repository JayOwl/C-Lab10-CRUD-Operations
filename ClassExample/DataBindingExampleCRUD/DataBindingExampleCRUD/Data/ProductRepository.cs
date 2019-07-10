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
                                 (Quantity, Sku, Description, Cost, SellPrice, Taxable, Active, Notes)
                                  VALUES (@quantity, @sku, @description, @cost, @sellprice, @taxable, @active, @notes)";

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
                    cmd.Parameters.AddWithValue("@sellprice", product.SellPrice);
                    cmd.Parameters.AddWithValue("@taxable", product.Taxable);
                    cmd.Parameters.AddWithValue("@active", product.Active);

                    if (product.Notes != null)
                    {
                        cmd.Parameters.AddWithValue("@notes", product.Notes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@notes", DBNull.Value);
                    }

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
                                      SellPrice = @sellprice,
                                      Taxable = @taxable,    
                                      Active = @active,
                                      Notes = @notes
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
                    cmd.Parameters.AddWithValue("@sellprice", product.SellPrice);
                    cmd.Parameters.AddWithValue("@taxable", product.Taxable);
                    cmd.Parameters.AddWithValue("@active", product.Active);

                    if (product.Notes != null)
                    {
                        cmd.Parameters.AddWithValue("@notes", product.Notes);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@notes", DBNull.Value);
                    }

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
                string query = $@"SELECT ProductId, Quantity, Sku, Description, Cost, SellPrice, Taxable, Active, Notes
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
                        decimal sellprice;
                        bool taxable;
                        bool active;
                        string notes;

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



                            if (!reader.IsDBNull(5))
                            {
                                sellprice = (decimal)reader["SellPrice"];
                            }
                            else
                            {
                                sellprice = 0.0m;
                            }



                            taxable = (bool)reader["Taxable"];

                            

                            if (!reader.IsDBNull(7))
                            {
                                active = (bool)reader["Active"];
                            }
                            else
                            {
                                active = false;
                            }


                            if (!reader.IsDBNull(8))
                            {
                                notes = reader["Notes"] as string;
                            }
                            else
                            {
                                notes = null;
                            }


                            products.Add(new Product
                            {
                                 ProductId = productId
                               , Quantity = quantity
                               , Sku = sku
                               , Description = description                               
                               , Cost = cost
                               , SellPrice = sellprice                               
                               , Taxable = taxable
                               , Active = active
                               , Notes = notes
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
