using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ReadImageName
{
    class TSqlHelp
    {
        private string connstr = "Data Source=202.75.219.210;Initial Catalog=sq_dbggcmxt;User ID=sq_dbggcmxt;Password=yunzhou168";

        public DataSet dataSet(string sql) 
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            SqlCommand comm = new SqlCommand(sql, conn);
            comm.Connection = conn;
            SqlDataAdapter dt = new SqlDataAdapter(sql, conn);
            dt.Fill(ds);
            conn.Close();
            return ds;
        }
    }
}
