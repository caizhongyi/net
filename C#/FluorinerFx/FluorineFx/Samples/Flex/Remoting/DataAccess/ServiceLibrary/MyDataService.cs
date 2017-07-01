using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.ComponentModel;

using FluorineFx;

namespace ServiceLibrary
{
    [RemotingService]
    [Description("Customer data service")]
    public class MyDataService
    {
        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MSAccessConnectionString"].ConnectionString;
            return connectionString;
        }

        [DataTableType("Flex.CustomerVO")]
        [Description("Filter customer by phone area code")]
        public DataTable GetCustomers(string areaCode)
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand("SELECT firstname, lastname, phone FROM [customer] WHERE phone LIKE '" + areaCode + "%'", connection);
                connection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataTable result = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }

        [DataTableType("Flex.CustomerVO")]
        public DataTable GetCustomerByLastName(string lastname)
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand("SELECT firstname, lastname, phone FROM [customer] WHERE lastname LIKE '" + lastname + "%'", connection);
                connection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataTable result = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }
    }
}
