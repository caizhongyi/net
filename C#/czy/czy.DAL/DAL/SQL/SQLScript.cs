using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace czy.MyDAL.SQL
{
    /// <summary>
    /// GetSqlSchema 的摘要说明
    /// </summary> 
    public class SQLScript
    {
        private string _dataBaseName;
        private string _InsertDataBasetName;
        private string _connstr;
        private DataBase.ConnStringType _type;
        /// <summary>
        /// 链接的数据库名称
        /// </summary>
        public string DataBaseName
        {
            get { return _dataBaseName; }
            set { _dataBaseName = value; }
        }
       
        /// <summary>
        /// 生成的插入数据库名称
        /// </summary>
        public string InsertDataBasetName
        {
            get { return _InsertDataBasetName; }
            set { _InsertDataBasetName = value; }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dataBaseName">链接的数据库名称</param>
        /// <param name="InsertDataBaseName">生成的插入数据库名称</param>
        /// <param name="constr">链接字符窜</param>
        /// <param name="t">链接字符窜类型</param>
        public SQLScript(string dataBaseName, string InsertDataBaseName, string constr, DataBase.ConnStringType t)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            _dataBaseName = dataBaseName;
            _InsertDataBasetName = InsertDataBaseName;
            this._connstr = constr;
            this._type = t;
        }
        /// <summary>
        /// 生成插入sql脚本
        /// </summary>
        /// <param name="fileSaveUrl">保存路径</param>
        public void CreateInsertSQLScript(string fileSaveUrl)
        {
            StringBuilder sb = new StringBuilder();
            List<ColumsSchema> dt = DBSchema.SelectDataBaseTable(_dataBaseName, _connstr,_type);
            sb.Append("Use " + _dataBaseName + "\n");
            foreach (ColumsSchema dr in dt)
            {
                string tableName = dr.Name.ToString();
                List<ColumsSchema> dtColumns = DBSchema.SelectDataTableColumns(tableName, _connstr, _type);
                DataTable dtRowsData = DBSchema.GetData(tableName, _connstr, _type);

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
                    for (int k = 0; k < dtColumns.Count; k++)
                    {
                        string column = dtColumns[k].Name.ToString();
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