using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ReadImageName
{
    class ReadImageName:IReadImageName
    {
        public string[] ImageName()
        {
            string sql = "select Ad_Url from AdInfo";
            TSqlHelp ts = new TSqlHelp();
            DataSet ds = ts.dataSet(sql);
            int count = ds.Tables[0].Rows.Count;
            string[] imageName = new string[count];
            for (int i = 0; i < count; i++) 
            {
                imageName[i] = ds.Tables[0].Rows[i][0].ToString();
            }
            return imageName;
        }
    }
}
