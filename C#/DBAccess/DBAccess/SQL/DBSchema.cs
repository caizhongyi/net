using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.OleDb;
using System.Collections.Generic;



namespace czy.MyDAL.SQL
{
    /// <summary>
    /// GetSqlSchema 的摘要说明
    /// </summary>
    public sealed partial class DBSchema
    {
        static IDataBaseAdvance db;
        public DBSchema() 
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            
        }

        #region 获取表名
        /// <summary>
        /// 获取表名
        /// </summary> 
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>返回表名(Table)</returns>
        public static List<ColumsSchema> SelectDataBaseTable(string dataBaseName, string connstr, DataBase.ConnStringType type)
        {
            db = new SQLDataBase(connstr, type);
            string cmd = "select Name from " + dataBaseName + "..sysobjects where xtype='u' or xtype='v' and status>=0";
            return db.GetDataTable(cmd).TableToList<ColumsSchema>();
           
        }
        /// <summary>
        /// 返回Mdb数据库中所有表表名
        /// </summary>
        public static List<ColumsSchema> SelectAccessTable(string database_path, string database_password)
        { 
            //获取数据表
            OleDbConnection conn = new OleDbConnection();
            try
            {
                conn.ConnectionString =  string.Format("Provider=Microsoft.Jet.OLEDB.4.0; " +
                    "Data Source={0};Jet OLEDB:Database password={1}", database_path, database_password);
                
                conn.Open();
                DataTable shemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                DataTable column=conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, null, "Columns" });
                int n = shemaTable.Rows.Count;
                List<ColumsSchema> strTable =new List<ColumsSchema>();
                int m = shemaTable.Columns.IndexOf("TABLE_NAME");
                for (int i = 0; i < n; i++)
                {
                    DataRow m_DataRow = shemaTable.Rows[i];
                    ColumsSchema cs = new ColumsSchema();
                    cs.Name=m_DataRow.ItemArray.GetValue(m).ToString();
                    strTable.Add(cs);
                }
                return strTable;
            }
            catch (OleDbException ex)
            {
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
        }
        #endregion

        #region 获取列名
        /// <summary>
        /// 获取列名和类型 dr[0]为列名,dr[1]为类型
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>返回列名(DataTable)</returns>
        public static List<ColumsSchema> SelectDataTableColumns(string tableName, string connstr, DataBase.ConnStringType type)
        {
            db = new SQLDataBase(connstr, type);
            string cmd = "select a.name as [column],b.name as type from syscolumns a,systypes b where a.id=object_id('" + tableName + "') and a.xtype=b.xtype and b.name !='sysname'";
            List<ColumsSchema> csList = new List<ColumsSchema>();
            csList =  db.GetDataTable(cmd).TableToList<ColumsSchema>();
            for (int i = 0; i < csList.Count; i++)
            {
                csList[i].Type = GetSQLType(csList[i].Type);
            }
            return csList;
        }
          /// <summary>
        /// 返回Mdb数据库中所有表表名
        /// </summary>
        public static List<ColumsSchema> SelectAccessColumns(string tableName,string database_path, string database_password)
        {
            List<ColumsSchema> list = new List<ColumsSchema>();
            //获取数据表
            OleDbConnection conn = new OleDbConnection();
            try
            {
                string connstr = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; " +
                    "Data Source={0};Jet OLEDB:Database password={1}", database_path, database_password);
                conn.ConnectionString = connstr;
                conn.Open();

                // DataTable shemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Catalogs, new object[] { null, null, null, "TABLE" });
                DataTable columnTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName.ToString(), null });
                // string str = string.Empty;
                foreach (DataRow dr2 in columnTable.Rows)
                {
                    ColumsSchema cs=new ColumsSchema();
                    cs.Name=dr2["COLUMN_NAME"].ToString();
                    cs.Type = GetAccessDataType(Convert.ToInt32(dr2["DATA_TYPE"]));
                    list.Add(cs);
                }
                return list;
            }
            catch (OleDbException ex)
            {
                return null;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();
                };
            }
        }

        private static string GetAccessDataType(int type)
        {
            try
            {
                switch (type)
                {
                    case 6: return "Double";
                    case 7: return "DateTime";
                    case 3: return "int";
                    case 130: return "String";
                    default: return "String";
                }
            }
            catch (Exception ex)
            {
                return ex.ToString ();
            }
        }

        /// <summary>
        /// 类型
        /// </summary>
        /// <param name="sqlType"></param>
        /// <returns></returns>
        private static string GetSQLType(string sqlType)
        {
            switch (sqlType)
            {
                case "varchar": return "String";
                case "char": return "String";
                case "text": return "String";
                case "datetime": return "DateTime";
                case "money": return "Decimal";
                case "decimal": return "Decimal";
                case "numeric": return "Decimal";
                case "double": return "Double";
                case "smalldatetime": return "DateTime";
                case "bigint": return "Int64";
                case "smallint": return "Int16";
                case "int": return "Int32";
                case "bit": return "bool";
                case "float": return "float";
                case "tinyint": return "Int16";
                case "nvarchar": return "String";
                case "image": return "String";
                case "guid": return "Guid";
                case "gid": return "Guid";
                default: return "String";

            }
        }
        #endregion

        #region  获取表内容
        /// <summary>
        /// 获取表内容
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>返回表内容(DataTable)</returns>
        public static DataTable GetData(string tableName, string connstr, DataBase.ConnStringType type)
        {
            db = new SQLDataBase(connstr, type);
            string cmd = "select * from " + tableName;
            return db.GetDataTable(cmd);
        }
        #endregion


    }
    
    public class ColumsSchema
    {
        /// <summary>
        /// 字段名称
        /// </summary>
        public string Name{get;set;}
        public string Type{get;set;}
        //public ColumsSchema(string name,string type)
        //{
        //    this.Name=name;
        //    this.Type=type;
        //}
    }
}
