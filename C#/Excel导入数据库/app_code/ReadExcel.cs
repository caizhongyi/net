using System;
using System.Data;
using System.Data.OleDb;

    /// <summary>
    /// Excel 的摘要说明。
    /// </summary>
 public class Excel
{
        public Excel()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

        }

        public  static DataSet SelectExcel(string path, string condition)
        {
            string strConn;
            strConn = "Provider=Microsoft.Jet.OLEDB.4.0;" +
            "Data Source=" + path + ";" +
            "Extended Properties=Excel 8.0;";
            OleDbConnection conn = new OleDbConnection(strConn);
            OleDbDataAdapter myCommand = new OleDbDataAdapter("select * FROM [sheet1$] " + condition, strConn);//distinct
            DataSet myDataSet = new DataSet();
            myCommand.Fill(myDataSet);
            return myDataSet;
        }

}
