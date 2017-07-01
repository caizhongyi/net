using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace DAL.impl.Sql
{
    public class TSqlHelp
    {
        SqlConnection cn = null;
        public TSqlHelp()
        {
            cn = new SqlConnection(ConfigurationManager.AppSettings["constr"].ToString());
        }

        public DataTable ExecuteQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

            
        /// <summary>
        /// Sql非查询
        /// </summary>
        /// <param name="cmdText">命令</param>
        /// <param name="param">参数</param>
        /// <param name="Type">命令类型</param>
        /// <returns>返回结果结果，成功为1，失败为0</returns>
        public int ExecuteNonQuery(string cmdText, SqlParameter[] param, CommandType Type)
        {
            try
            {
               
                SqlCommand comm = new SqlCommand(cmdText, cn);
                cn.Open();
                comm.Connection = cn;
                comm.CommandType = Type;
                if (Type == CommandType.StoredProcedure)
                {
                    comm.CommandType = CommandType.StoredProcedure;
                }
                else
                {
                    comm.CommandType = CommandType.Text;
                }
                if (param != null)
                {
                    foreach (SqlParameter p in param)
                    {
                        comm.Parameters.Add(p);
                    }
                }
                int i = comm.ExecuteNonQuery();
                return i;

            }
            catch (System.Exception e)
            {

                throw e;
            }
            finally
            {
                if (cn.State != ConnectionState.Closed)
                    cn.Close();
            }
        }

        public void ExecuteNonQuery(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, cn);
            cn.Open();
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        /// <summary>
        /// sql查询
        /// </summary>
        /// <param name="cmd">命令</param>
        /// <returns>返回读取结果</returns>
        public  SqlDataReader SqlGetDataReader(string cmd)
        {
            try
            {

               

                SqlCommand comm = new SqlCommand(cmd, cn);
                cn.Open();
                SqlDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }


        }

        public SqlDataReader SqlGetDataReader(string M_str_sqlstr, SqlParameter[] param, CommandType Type)
        {
           
            SqlCommand sqlcom = new SqlCommand(M_str_sqlstr, cn);
            cn.Open();
            sqlcom.CommandType = Type;
            if (Type == CommandType.StoredProcedure)
            {
                sqlcom.CommandType = CommandType.StoredProcedure;

            }
            else
            {
                sqlcom.CommandType = CommandType.Text;
            }
            if (param != null)
            {
                foreach (SqlParameter p in param)
                {
                    sqlcom.Parameters.Add(p);
                }
            }
            SqlDataReader sqlread = sqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return sqlread;
        }
         
        public  DataSet SqlGetDataSet(string cmdText)
        {

            DataSet ds = new DataSet();
          
            SqlDataAdapter da = null;
            SqlCommandBuilder dc = null;
            da = new SqlDataAdapter(cmdText, cn);
            da.Fill(ds);
            dc = new SqlCommandBuilder(da);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            return (ds);
        }
        public DataSet SqlGetDataSet(string cmdText, CommandType Type)
        {

            DataSet ds = new DataSet();
            cn.Open();
            SqlCommand sqlcom = new SqlCommand(cmdText, cn);
            sqlcom.CommandType = Type;
            SqlDataAdapter da = null;
            SqlCommandBuilder dc = null;

            da = new SqlDataAdapter(sqlcom);
            da.Fill(ds);
            dc = new SqlCommandBuilder(da);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            cn.Close();
            return (ds);
        }

        public DataSet SqlGetDataSet(string cmdText, SqlParameter[] param, CommandType Type)
        {

            DataSet ds = new DataSet();
            cn.Open();
            SqlCommand sqlcom = new SqlCommand(cmdText, cn);
            sqlcom.CommandType = Type;
            if (Type == CommandType.StoredProcedure)
            {
                sqlcom.CommandType = CommandType.StoredProcedure;

            }
            else
            {
                sqlcom.CommandType = CommandType.Text;
            }
            if (param != null)
            {
                foreach (SqlParameter p in param)
                {
                    sqlcom.Parameters.Add(p);
                }
            }
            SqlDataAdapter da = null;
            SqlCommandBuilder dc = null;

            da = new SqlDataAdapter(sqlcom);
            da.Fill(ds);
            dc = new SqlCommandBuilder(da);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            cn.Close();
            return (ds);
        }

    }
}
