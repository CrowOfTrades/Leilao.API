using Leilao.Infrastructure.Storage.Storage.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Leilao.Infrastructure.Storage.Storage.Services
{
    public class UsersStorageService
    {
        private readonly string tableName = "Users";
        private readonly string dataBaseName = "leilaoDB";
        private readonly string connString = ReadJsonService.LoadJson().connString;

        public List<User> SelectMany(int size)
        {
            string query = $"USE {dataBaseName}; SELECT TOP {size} * from {tableName}";
            return DoQuery(query);
        }

        public void Insert(User user)
        {
            string query = $"USE {dataBaseName}; INSERT INTO {tableName} (ID, Name, Birthdate, CreateOn) VALUES ('{Guid.NewGuid()}', '{user.Name}', '{user.Birthdate}', '{DateTime.Now}');";
            DoQuery(query);
        }

        private List<User> DoQuery(string Query)
        {
            List<User> ls = new List<User>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(Query, conn);

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            ls.Add(new User()
                            {
                                Id = new Guid(dr["ID"].ToString()),
                                Name = dr["Name"].ToString(),
                                Birthdate = (DateTime)dr["Birthdate"],
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
