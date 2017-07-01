using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.ComponentModel;
using System.Collections.Generic;

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

        private string GetConnectionString2()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MSAccessConnectionString2"].ConnectionString;
            return connectionString;
        }

        /// <summary>
        /// When using JSON-RPC this method will return CustomerVO objects and not the datatable
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        [DataTableType("Flex.CustomerVO")]
        [Description("Filter customer by phone area code")]
        [FluorineFx.Json.JsonRpcMethod]
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

        [FluorineFx.Json.JsonRpcMethod]
        public string SayHello()
        {
            return "Hello";
        }

        [FluorineFx.Json.JsonRpcMethod]
        public object Echo(object obj)
        {
            return obj;
        }

        [FluorineFx.Json.JsonRpcMethod]
        public ProductVO EchoProduct(ProductVO obj)
        {
            return obj;
        }

        [FluorineFx.Json.JsonRpcMethod]
        public List<PersonVO> EchoRecords(List<PersonVO> records)
        {
            return records;
        }

        /// <summary>
        /// When using JSON-RPC this method will return the DataTable as untyped object (Flash style Recordset ASO)
        /// </summary>
        /// <param name="areaCode"></param>
        /// <returns></returns>
        [FluorineFx.Json.JsonRpcMethod]
        public DataTable GetCustomersJson(string areaCode)
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

        [FluorineFx.Json.JsonRpcMethod]
        public int GetCountriesCount()
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString2()))
            {
                OleDbCommand command = new OleDbCommand("SELECT COUNT(*) FROM Countries", connection);
                connection.Open();
                object result = command.ExecuteScalar();
                return System.Convert.ToInt32(result);
            }
        }

        [FluorineFx.Json.JsonRpcMethod]
        [DataTableType("Flex.CountryVO")]
        public DataTable GetCountries(int offset, int limit)
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString2()))
            {
                string sql = string.Format("SELECT t.ID, t.Country, t.Capital FROM ( SELECT TOP {0} ID, Country FROM ( SELECT TOP {1} ID, Country FROM Countries ORDER BY Country ASC, ID ASC) as foo ORDER BY Country DESC, ID DESC) as bar INNER JOIN Countries AS t ON bar.ID = t.ID ORDER BY bar.Country ASC, bar.ID ASC", limit, offset + limit);
                OleDbCommand command = new OleDbCommand(sql, connection);
                connection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }            
        }
    }
}
