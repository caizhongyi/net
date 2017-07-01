using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using FluorineFx;

namespace ServiceLibrary
{
    [RemotingService]
    public class DataService
    {
        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MSAccessConnectionString"].ConnectionString;
            return connectionString;
        }

        [PageSize(10)]
        public object GetCountries()
        {
            int offset = PagingContext.Current.Offset;
            int limit = PagingContext.Current.Limit;
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
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

        public int GetCountriesCount()
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                string sql = "SELECT COUNT(*) FROM Countries";
                OleDbCommand command = new OleDbCommand(sql, connection);
                connection.Open();
                object result = command.ExecuteScalar();
                return System.Convert.ToInt32(result);
            }
        }

        [PageSize(10)]
        public object GetCountries2(string capital)
        {
            int offset = PagingContext.Current.Offset;
            int limit = PagingContext.Current.Limit;
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                string sql = string.Format("SELECT t.ID, t.Country, t.Capital FROM ( SELECT TOP {0} ID, Country FROM ( SELECT TOP {1} ID, Country FROM Countries WHERE Capital LIKE '{2}%' ORDER BY Country ASC, ID ASC) as foo ORDER BY Country DESC, ID DESC) as bar INNER JOIN Countries AS t ON bar.ID = t.ID ORDER BY bar.Country ASC, bar.ID ASC", limit, offset + limit, capital);
                OleDbCommand command = new OleDbCommand(sql, connection);
                connection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                return dataTable;
            }
        }

        public int GetCountries2Count(string capital)
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                string sql = string.Format("SELECT COUNT(*) FROM Countries WHERE Capital LIKE '{0}%'", capital);
                OleDbCommand command = new OleDbCommand(sql, connection);
                connection.Open();
                object result = command.ExecuteScalar();
                return System.Convert.ToInt32(result);
            }
        }

    }
}
