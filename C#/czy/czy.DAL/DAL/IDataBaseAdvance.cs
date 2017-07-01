using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace czy.MyDAL
{
    /// <summary>
    /// 包含存储过程的数据访问接口
    /// </summary>
    public interface IDataBaseAdvance : IDataBase
    {

        /// <summary>
        /// 执行操作(存储过程)
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlParam">SqlParameter参数数组</param>
        /// <returns>受影响的行数</returns>
        int ExecuteNonQuery(string procName, object[] sqlParams);
        /// <summary>
        /// 获取SqlDataReader读取器(存储过程)
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlParam">SqlParameter参数数组</param>
        void ExecuteReader(string procName, object[] sqlParams);
        /// <summary>
        /// 读取单行单列值(存储过程)
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlParam">SqlParameter参数数组</param>
        /// <returns>对像值</returns>
        object ExecuteScalar(string procName, object[] sqlParams);
        /// <summary>
        /// 返回一个数据集操作(存储过程)
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlParam">SqlParameter参数数组</param>
        /// <returns>数据集</returns>
        DataSet GetDataSet(string procName, object[] sqlParams);
        /// <summary>
        /// 返回一个数据表操作(存储过程)
        /// </summary>
        /// <param name="procName">存储过程名</param>
        /// <param name="sqlParam">SqlParameter参数数组</param>
        /// <returns>数据表</returns>
        DataTable GetDataTable(string procName, object[] sqlParams);
    }
}
