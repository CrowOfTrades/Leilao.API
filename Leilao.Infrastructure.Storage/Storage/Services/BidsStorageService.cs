using Leilao.Infrastructure.Storage.Storage.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Configuration;

namespace Leilao.Infrastructure.Storage.Storage.Services
{
    public class BidsStorageService
    {
        private readonly string tableName = "Bids";
        private readonly string dataBaseName = "leilaoDB";
        private readonly string connString = ReadJsonService.LoadJson().connString;

        public List<Bid> SelectMany(int size)
        {
            string query = $"USE {dataBaseName}; " +
                $"SELECT TOP {size} Bids.ID, Bids.Price, Bids.CreateOn, Bids.ProductId, Bids.UserId, Users.Name as UserName, Products.Name as ProductName from {tableName} " +
                $"INNER JOIN Users On Bids.UserId = Users.ID " +
                $"INNER JOIN Products On Bids.ProductId = Products.ID";
            return DoQuery(query);
        }

        public List<Bid> SelectByUser(int size, Guid UserId)
        {
            string query = $"USE {dataBaseName}; " +
                $"SELECT TOP {size} Bids.ID, Bids.Price, Bids.CreateOn, Bids.ProductId, Bids.UserId, Users.Name as UserName, Products.Name as ProductName from {tableName} " +
                $"INNER JOIN Users On Bids.UserId = Users.ID " +
                $"INNER JOIN Products On Bids.ProductId = Products.ID " +
                $"WHERE Users.ID = '{UserId}'";
            return DoQuery(query);
        }

        public void Insert(Bid bid)
        {
            string query = $"USE {dataBaseName}; " +
                $"INSERT INTO {tableName} (ID, UserId, ProductId, Price, CreateOn) " +
                $"VALUES ('{Guid.NewGuid()}', '{bid.UserId}', '{bid.ProductId}', {bid.Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}, '{DateTime.Now}');";
            DoQuery(query);
        }

        private List<Bid> DoQuery(string Query)
        {
            List<Bid> ls = new List<Bid>();

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
                            ls.Add(new Bid()
                            {
                                Id = new Guid(dr["ID"].ToString()),
                                UserId = new Guid(dr["UserId"].ToString()),
                                ProductId = new Guid(dr["ProductId"].ToString()),
                                Price = (decimal)dr["Price"],
                                CreateOn = (DateTime)dr["CreateOn"],
                                UserName = dr["UserName"].ToString(),
                                ProductName = dr["ProductName"].ToString(),
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
