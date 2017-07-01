using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace czy.MyDAL
{
    /// <summary>
    /// 读取Excel类[继承至OleDbDataBase类]
    /// </summary>
    public class ExcelDataBase : OleDbDataBase , IDataBase
    {
        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="dbUrl">虚拟路径,为非路径则取Config</param>
        /// <param name="pwd">密码,无则为空</param>
        public ExcelDataBase(string dbUrl, string pwd)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            string strConnection = string.Empty;
            if (dbUrl.Substring(dbUrl.LastIndexOf('.'), dbUrl.Length - dbUrl.LastIndexOf('.') - 1).ToLower() == "accdb")
            {
                strConnection = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; " +
                    "Data Source={0};Jet OLEDB:Database password={1}", dbUrl, pwd);
            }
            else
            {
                strConnection = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; " +
                       "Data Source={0};Jet OLEDB:Database password={1}", dbUrl, pwd);
            }

            this._connString = strConnection;
        }
        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="constr">链接字符窜</param>
        /// <param name="type">链接类型</param>
        public ExcelDataBase(string constr, ConnStringType type)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            string strConnection = string.Empty;
            if (type == ConnStringType.ConfigKey)
            {
                strConnection = System.Configuration.ConfigurationSettings.AppSettings[constr].ToString();
            }
            else
            {
                strConnection = constr;
            }
            this._connType = type;
            this._connString = strConnection;
      
        }
        /// <summary>
        /// 单行单列查询
        /// </summary>
        /// <param name="columns">列</param>
        /// <param name="pageName">查询的Excel页名</param>
        /// <param name="filter">条件</param>
        /// <returns>单行单列值</returns>
        public object ExecuteScalar(string columns,string pageName,string filter)
        {
            string cmd = "select " + columns + " FROM [" + pageName + "$] " + filter;
            return ExecuteScalar(cmd);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="columns">列</param>
        /// <param name="pageName">查询的Excel页名</param>
        /// <param name="filter">条件</param>
        /// <returns>数据集</returns>
        public DataSet GetDataSet(string columns, string pageName, string filter)
        {
            string cmd = "select " + columns + " FROM [" + pageName + "$] " + filter;
            return GetDataSet(cmd);
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="columns">列</param>
        /// <param name="pageName">查询的Excel页名</param>
        /// <param name="filter">条件</param>
        /// <returns>表</returns>
        public DataTable GetDataTable(string columns, string pageName, string filter)
        {
            string cmd = "select " + columns + " FROM [" + pageName + "$] " + filter;
            return GetDataTable(cmd);
        }

        /// <summary>
        /// Reader读取
        /// </summary>
        /// <param name="columns">列</param>
        /// <param name="pageName">查询的Excel页名</param>
        /// <param name="filter">条件</param>
        /// <returns></returns>
        public void ExecuteReader(string columns, string pageName, string filter)
        {
            string cmd = "select " + columns + " FROM [" + pageName + "$] " + filter;
            ExecuteReader(cmd);
        }
    }
}
