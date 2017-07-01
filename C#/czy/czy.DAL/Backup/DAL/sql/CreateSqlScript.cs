using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.IO;

namespace MyClass
{
    /// <summary>
    /// GetSqlSchema 的摘要说明
    /// </summary>
    public class CreateSqlScript
    {
        private string _dataBaseName;

        public string DataBaseName
        {
            get { return _dataBaseName; }
            set { _dataBaseName = value; }
        }
        private string _InsertDataBasetName;

        public string InsertDataBasetName
        {
            get { return _InsertDataBasetName; }
            set { _InsertDataBasetName = value; }
        }
        public CreateSqlScript(string dataBaseName, string InsertDataBaseName)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            _dataBaseName = dataBaseName;
            _InsertDataBasetName = InsertDataBaseName;

        }
        /// <summary>
        /// 生成插入sql脚本
        /// </summary>
        /// <param name="fileSaveUrl">保存路径</param>
        public void CreateInsertSqlScript(string fileSaveUrl)
        {
            StringBuilder sb = new StringBuilder();
            DataTable dt = GetSqlSchema.SelectDataBaseTable(_dataBaseName);
            sb.Append("Use " + _dataBaseName + "\n");
            foreach (DataRow dr in dt.Rows)
            {
                string tableName = dr.ItemArray[0].ToString();
                DataTable dtColumns = GetSqlSchema.SelectDataTableColumns(tableName);
                DataTable dtRowsData = GetSqlSchema.GetData(tableName);

                sb.Append("SET   IDENTITY_INSERT   " + _InsertDataBasetName + ".dbo." + tableName + "   ON \n");
                sb.Append("Go \n");
                foreach (DataRow drData in dtRowsData.Rows)
                {
                    string value = string.Empty;
                    string col = string.Empty;
                    for (int j = 0; j < dtRowsData.Columns.Count; j++)
                    {
                        string columnValue = drData.ItemArray[j].ToString();
                        if (j == 0)
                        { value += "'" + columnValue + "'"; }
                        else
                        { value += ",'" + columnValue + "'"; }
                    }
                    for (int k = 0; k < dtColumns.Rows.Count; k++)
                    {
                        string column = dtColumns.Rows[k].ItemArray[0].ToString();
                        if (k == 0)
                        {
                            col += "[" + column + "]";
                        }
                        else
                        {
                            col += ",[" + column + "]";
                        }
                    }

                    sb.Append("insert into " + _InsertDataBasetName + ".dbo." + tableName + "(" + col + ") values(" + value + ")\n");
                    sb.Append("Go \n");
                }
                sb.Append("SET   IDENTITY_INSERT   " + _InsertDataBasetName + ".dbo." + tableName + "  OFF \n");
                sb.Append("Go \n");
            }
            using (StreamWriter sw = new StreamWriter(fileSaveUrl))
            {
                sw.WriteLine(sb.ToString());
            }
        }
    }

}