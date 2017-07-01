using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace czy.MyDAL
{
    /// <summary>
    /// 数据访问接口
    /// </summary>
    public interface IDataBase
    {

        /// <summary>
        /// DataReader读取器读取事件
        /// </summary>
        event DataBase.DataReadEvent DataRead;
        /// <summary>
        /// 链接字符或ConfigKey
        /// </summary>
        string ConnString { get; set; }
        /// <summary>
        /// 链接字符类型,默认为ConfigKey
        /// </summary>
        DataBase.ConnStringType ConnType { get; set; }
        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(string sql);
        /// <summary>
        /// 获取SqlDataReader读取器
        /// </summary>
        /// <param name="sql">sql语句</param>
        void ExecuteReader(string sql);
        /// <summary>
        /// 读取单行单列值
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>对像值</returns>
        object ExecuteScalar(string sql);
        /// <summary>
        /// 返回一个数据集操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>数据集</returns>
        DataSet GetDataSet(string sql);
        /// <summary>
        /// 返回一个数据表操作
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns>数据表</returns>
        DataTable GetDataTable(string sql);


       
    }
}
