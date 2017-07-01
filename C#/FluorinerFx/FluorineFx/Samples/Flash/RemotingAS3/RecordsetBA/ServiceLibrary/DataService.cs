using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using FluorineFx;

//Making Flash Remoting easier in Flash CS3 
//http://www.bytearray.org/?p=122

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

        /*
	        In this sample the result is an array of untyped objects.
	        Please note that service.config defines the channel property
	        <legacy-collection>true</legacy-collection> as there is no ArrayCollection in Flash AS3.
            The AS2 Recordset class is missing too.
        */

        [DataTableType("Anonymous.CountryVO")]
        public object GetCountries(string capital)
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                string sql = string.Format("SELECT t.ID, t.Country, t.Capital FROM Countries AS t WHERE Capital LIKE '{0}%'", capital);
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
