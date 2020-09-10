using Leilao.domain.Contracts.SQL;
using System;
using System.Data.SqlClient;

namespace Leilao.Api.SQL
{
    public class StorageService : IStorageService
    {

        public void Test()
        {
            doQuery("select * from Persons");
        }
        public void doQuery(string Query)
        {
            string serverName = "localhost";
            string dataBaseName = "master";

            string connString = $"Server={serverName};Database={dataBaseName};Trusted_Connection = True;";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(Query, conn);

                    SqlDataReader dr = cmd.ExecuteReader();

                    //check if there are records
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            Console.WriteLine(dr.GetString(0));
                        }
                    }
                    else
                    {
                        Console.WriteLine("No data found.");
                    }
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }
        }
    }
}
