using Leilao.Infrastructure.Storage.Storage.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Leilao.Infrastructure.Storage.Storage.Services
{
    public class ProductsStorageService
    {
        private readonly string tableName = "Products";
        private readonly string dataBaseName = "leilaoDB";
        private readonly string connString = ReadJsonService.LoadJson().connString;

        public List<Product> SelectMany(int size)
        {
            string query = $"USE {dataBaseName}; SELECT TOP {size} * from {tableName}";
            return DoQuery(query);
        }

        public void Insert(Product product)
        {
            string query = $"USE {dataBaseName}; INSERT INTO {tableName} (ID, Name, Price, CreateOn) VALUES ('{Guid.NewGuid()}', '{product.Name}', {product.Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}, '{DateTime.Now}');";
            DoQuery(query);
        }

        public void Update(Guid productId, Dictionary<string, string> values)
        {
            string query = $"USE {dataBaseName}; UPDATE {tableName} SET ";

            int count = 0;
            foreach(var item in values)
            {
                count++;
                query += $"{item.Key} = '{item.Value}'";
                if (count < values.Count)
                    query += ", ";
            }
            query += $" WHERE ID = '{productId}'";

            DoQuery(query);

        }

        public void UpdatePrice(Guid productId, decimal value)
        {
            string query = $"USE {dataBaseName}; UPDATE {tableName} SET Price = {value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)} WHERE ID = '{productId}'";
            DoQuery(query);

        }

        private List<Product> DoQuery(string Query)
        {
            List<Product> ls = new List<Product>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(Query, conn);

                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ls.Add(new Product()
                            {
                                Id = new Guid(dr["ID"].ToString()),
                                Name = dr["Name"].ToString(),
                                Price = (decimal)dr["Price"],
                                CreateOn = (DateTime)dr["CreateOn"]
                            });
                        }
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return ls;
        }
    }
}
