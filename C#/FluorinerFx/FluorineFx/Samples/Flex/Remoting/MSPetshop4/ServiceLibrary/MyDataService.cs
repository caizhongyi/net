using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using FluorineFx;

namespace ServiceLibrary
{
    [RemotingService]
    public class MyDataService
    {
        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MSPetShop4"].ConnectionString;
            return connectionString;
        }

        [DataTableType("Flex.CategoryVO")]
        public DataTable GetCategories()
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM dbo.Category", connection);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable result = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }

        [DataTableType("Flex.ProductVO")]
        public DataTable GetProducts(string category)
        {
            using (SqlConnection connection = new SqlConnection(GetConnectionString()))
            {
                SqlCommand command = new SqlCommand("SELECT ProductId, Name, Descn, Image FROM dbo.Product WHERE CategoryId LIKE '" + category + "'", connection);
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable result = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }

    }
}
