using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using DAL;


namespace MyClass
{
    /// <summary>
    /// GetSqlSchema 的摘要说明
    /// </summary>
    public class GetSqlSchema
    {
        public GetSqlSchema()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        /// <summary>
        /// 获取表名
        /// </summary>
        /// <param name="dataBaseName">数据库名</param>
        /// <returns>返回表名(Table)</returns>
        public static DataTable SelectDataBaseTable(string dataBaseName)
        {
            string cmd = "select Name from " + dataBaseName + "..sysobjects where xtype='u' and status>=0";
            return Util.GetDataTable(cmd);
        }
        /// <summary>
        /// 获取列名
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>返回列名(DataTable)</returns>
        public static DataTable SelectDataTableColumns(string tableName)
        {
            string cmd = "select a.name as [column],b.name as type from syscolumns a,systypes b where a.id=object_id('Adv_Info') and a.xtype=b.xtype";
            return Util.GetDataTable(cmd);

        }
        /// <summary>
        /// 获取表内容
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>返回表内容(DataTable)</returns>
        public static DataTable GetData(string tableName)
        {
            string cmd = "select * from " + tableName;
            return Util.GetDataTable(cmd);
        }
    }

}