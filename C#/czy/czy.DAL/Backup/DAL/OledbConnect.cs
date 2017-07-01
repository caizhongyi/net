using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Data .SqlClient ;


class OledbConnect
{
    public static int a = 0, d = 0;

    public static int k = 0;
    /// <summary>
    /// 连接数Oledb数据库
    /// </summary>
    private static string ConnStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Card.mdb;Persist Security Info=True";
    /// <summary>
    /// 数据连接
    /// </summary>
    /// <param name="cmdText">Oledb语句</param>
    /// <returns>非查询操作结果返回值为0或1</returns>
    public static int ExcuteOledb(string cmdText, CommandType Type)
    {
        OleDbConnection conn = null;
    
        try
        {
           
            conn = new OleDbConnection(ConnStr);
            OleDbCommand comm = new OleDbCommand(cmdText, conn);
            conn.Open();
            comm.Connection = conn;
            comm.CommandType = Type;
            //foreach (OleDbParameter p in param)
            //{
            //    comm.Parameters.Add(p);
            //}
            int i = comm.ExecuteNonQuery();
            return i;

        }
        catch (System.Exception e)
        {

            throw e;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
        }
    }
   

    public static int ExcuteOledb(string cmdText)
    {
        OleDbConnection conn = null;

        try
        {
            conn = new OleDbConnection(ConnStr);
            OleDbCommand comm = new OleDbCommand(cmdText, conn);
            conn.Open();
            comm.Connection = conn;
            int i = comm.ExecuteNonQuery();
            return i;

        }
        catch (System.Exception e)
        {

            throw e;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
        }
    }

    public static OleDbDataReader GetDateReader(string cmdText)
    {

        OleDbConnection conn = null;
        try
        {
            conn = new OleDbConnection(ConnStr);
            conn.Open();
            OleDbCommand comm = new OleDbCommand(cmdText, conn);
            OleDbDataReader dr = comm.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
            //if (conn != null)
            //{
            //    if (conn.State  != ConnectionState.Closed)
            //        conn.Close();
            //}
            //关闭读取器的同时关闭Oledb、连接


        }
        catch (System.Exception e)
        {

            throw e;
        }

    }
    public static DataSet GetDataSet(string cmdText)
    {
        OleDbConnection conn = null;
        DataSet ds = new DataSet();
        conn = new OleDbConnection(ConnStr);
        OleDbDataAdapter da = null;
        OleDbCommandBuilder dc = null;
        da = new OleDbDataAdapter(cmdText, conn);
        da.Fill(ds);
        dc = new OleDbCommandBuilder(da);
        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
        return (ds);
    }

    public static DataTable GetDataTable(string cmdText)
    {
        OleDbConnection conn = null;
        DataTable dt = new DataTable();
        conn = new OleDbConnection(ConnStr);
        OleDbDataAdapter da = null;
        OleDbCommandBuilder dc = null;
        da = new OleDbDataAdapter(cmdText, conn);
        da.Fill(dt);
        dc = new OleDbCommandBuilder(da);
        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
        return (dt);
    }


    public static DataSet GetDataSet(string cmdText, string tablename)
    {
        OleDbConnection conn = null;
        DataSet ds = new DataSet();
        conn = new OleDbConnection(ConnStr);
        OleDbDataAdapter da = null;
        OleDbCommandBuilder dc = null;
        da = new OleDbDataAdapter(cmdText, conn);
        da.Fill(ds, tablename);
        dc = new OleDbCommandBuilder(da);
        da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
        //da.Update(table);

        return (ds);
    }

    public static OleDbDataAdapter GetAdapter(string cmdText)
    {
        DataSet ds = new DataSet();
        OleDbDataAdapter da = new OleDbDataAdapter(cmdText, ConnStr);
        da.Fill(ds);
        return (da);
    }


}


