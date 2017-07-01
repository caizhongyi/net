using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.OleDb;

namespace czy.MyDAL
{
  
    /// <summary>
    /// AccessDataBase 的摘要说明
    /// </summary>
    public class OleDbDataBase :DataBase, IDataBase
    {

        /// <summary>
        /// DataRead读取器事件
        /// </summary>
        public event DataReadEvent DataRead;
        protected OleDbConnection conn;

        public OleDbDataBase()
        { }

        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="dbUrl">虚拟路径,为非路径则取Config</param>
        /// <param name="pwd">密码,无则为空</param>
        public OleDbDataBase(string dbUrl, string pwd)
        {

            //
            // TODO: 在此处添加构造函数逻辑
            //
            string strConnection = string.Empty;
            if (dbUrl.Substring(dbUrl.LastIndexOf('.'), dbUrl.Length - dbUrl.LastIndexOf('.') - 1).ToLower() == "accdb")
            {
                strConnection = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; " +
                    "Data Source={0};Jet OLEDB:Database password={1}",dbUrl,pwd);
            }
            else
            {
                strConnection =string .Format ( "Provider=Microsoft.ACE.OLEDB.12.0; " +
                       "Data Source={0};Jet OLEDB:Database password={1}",dbUrl,pwd);
            }

            this._connString = strConnection;

        }    
        /// <summary>
        /// 初始化实例
        /// </summary>
        /// <param name="constr">链接字符窜</param>
        /// <param name="type">链接类型</param>
        public OleDbDataBase(string constr, ConnStringType type)
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
        /// 执行操作
        /// </summary>
        /// <param name="OleDb">OleDb语句</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string cmd)
        {
            if (conn == null)
            {
                conn = new OleDbConnection(this._connString);
            }
            conn.Open();
            int i;
            try
            {
                OleDbCommand comm = new OleDbCommand(cmd, conn);
                i = Convert.ToInt32(comm.ExecuteNonQuery());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return i;
        }
        /// <summary>
        /// 读取单行单列值
        /// </summary>
        /// <param name="OleDb">OleDb语句</param>
        /// <returns>对像值</returns>
        public object ExecuteScalar(string cmd)
        {
            if (conn == null)
            {
                conn = new OleDbConnection(this._connString);
            }
            conn.Open();
            object i;
            try
            {
                OleDbCommand comm = new OleDbCommand(cmd, conn);
                i = comm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose(); 
                conn = null;
            }
            return i;
        }
        /// <summary>
        /// 获取OleDbDataReader读取器
        /// </summary>
        /// <param name="OleDb">该读取会一值保持链接</param>
        public void ExecuteReader(string cmd)
        {
            if (conn == null)
            {
                conn = new OleDbConnection(this._connString);
            }
            conn.Open();
            try
            {
                OleDbCommand comm = new OleDbCommand(cmd, conn);
                OleDbDataReader dr = comm.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    DataRead(dr);
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }
        /// <summary>
        /// 返回一个数据集操作
        /// </summary>
        /// <param name="OleDb">OleDb语句</param>
        /// <returns>数据集</returns>
        public DataSet GetDataSet(string cmd)
        {
            if (conn == null)
            {
                conn = new OleDbConnection(this._connString);
            }
            DataSet ds = new DataSet();
            conn.Open();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(cmd, conn);
        
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return ds; 
        }
        /// <summary>
        //  返回一个数据表
        /// </summary>
        /// <param name="OleDb">OleDb语句</param>
        /// <returns>数据表</returns>
        public DataTable GetDataTable(string cmd)
        {
            if (conn == null)
            {
                conn = new OleDbConnection(this._connString);
            }
            DataTable dt = new DataTable();
            conn.Open();
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter(cmd, conn);
                da.Fill(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
            return dt;
        }
    }
}