using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Configuration;
using System.Data.SqlClient;
/// <summary>
/// sql 的摘要说明
/// </summary>
public class sqlData:DataProvider
{
    public sqlData()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    //public static string conn1 = @"Data Source=(local);Initial Catalog=EastSeaDB;Persist Security Info=True;User ID=sa;Password=sasasa;Connect Timeout=500;";

    public static string conn1 = ConfigurationManager.ConnectionStrings["LocalSqlServer"].ConnectionString;
    //public static string conn2 = ConfigurationManager.ConnectionStrings["conn2"].ConnectionString;
   // public static string connstring = ConfigurationManager.ConnectionStrings["conn1"].ConnectionString;
    //public static SqlConnection GetSqlConnection()
    //{
    //    try
    //    {
    //        return new SqlConnection(ConnString);
    //    }
    //    catch
    //    {
    //        throw new Exception("没有提供数据庫连接字符串！");
    //    }
    //}
    //public static DataTable SqlHelper(string strSql)
    //{

    //    DataTable dt = null;
    //    using (SqlConnection Conn = GetSqlConnection())
    //    {
    //        SqlCommand dbCommand = new SqlCommand();
    //        dbCommand.Connection = Conn;
    //        try
    //        {
    //            dbCommand.CommandText = strSql;
    //            dbCommand.CommandType = CommandType.Text;

    //            IDataAdapter idApdapter = new SqlDataAdapter((SqlCommand)dbCommand);
    //            DataSet ds = new DataSet();
    //            Conn.Open();

    //            idApdapter.Fill(ds);
    //            dbCommand.Parameters.Clear();

    //            if (null != ds && ds.Tables.Count > 0)
    //                dt = ds.Tables[0];

    //            dbCommand.Dispose();
    //            Conn.Dispose();
    //            Conn.Close();
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new ApplicationException(ex.Message);
    //        }
    //        finally
    //        {
    //            dbCommand.Dispose();
    //            Conn.Dispose();
    //            Conn.Close();
    //        }
    //    }
    //    return dt;

    //}
    //public static DataTable DataTablePage(DataTable dtSource, int pageIndex, int pageSize)
    //{
    //    //if (null== dtSource || dtSource.Rows.Count<=0)
    //    //{
    //    //    return null;
    //    //}

    //    DataTable dtResult = dtSource.Clone();

    //    int count = (pageIndex + 1) * pageSize;
    //    int beginIndex = pageIndex * pageSize;

    //    if (count > dtSource.Rows.Count)
    //        count = dtSource.Rows.Count;

    //    for (int i = beginIndex; i < count; i++)
    //    {
    //        dtResult.Rows.Add(dtSource.Rows[i].ItemArray);
    //    }
    //    return dtResult;
    //}

    //public static DataTable getDataTable(string strSql, int pageIndex, int pageSize, out int recordCount)
    //{
    //    recordCount = 0;
    //    DataTable dt = SqlHelper(strSql);
    //    if (null != dt && dt.Rows.Count > 0)
    //    {
    //        recordCount = dt.Rows.Count;
    //        if (dt.Rows.Count > pageSize)
    //        {
    //            DataTable dtResult = DataTablePage(dt, pageIndex, pageSize);
    //            return dtResult;
    //        }
    //    }
    //    return dt;
    //}

    public override DataSet GreatDs(string sql)
    {
        SqlDataAdapter Dar = new SqlDataAdapter(sql, conn1);

        DataSet ds = new DataSet();
        Dar.Fill(ds);
        return ds;
    }

    public override void DoSql(Control c, string sql)
    {
        string alert = "";
        string str = sql.Substring(0, 1).ToLower();
        if (str == "i")
        {
            alert = "添加";
        }
        else if (str == "u")
        {
            alert = "修改";
        }
        else if (str == "d")
        {
            alert = "删除";
        }
        if (sql.Contains("shopping") && str != "d")
        {
            alert = "购买";
        }



        SqlConnection conn = new SqlConnection();//创建连接对象
        conn.ConnectionString = conn1;//给连接字符串赋值
        conn.Open();//打开数据库

        SqlCommand cmd = new SqlCommand(sql, conn);
        int i = cmd.ExecuteNonQuery();
        conn.Close();//关闭数据库
        if (i >= 1)
        {
            fun.AJAXalert(c, alert + "成功");
        }
        else
        {
            fun.AJAXalert(c, alert + "失败");
        }
    }

    public override void DoSql(Control c, string sql, string url)
    {
        string alert = "";
        string str = sql.Substring(0, 1).ToLower();
        if (str == "i")
        {
            alert = "添加";
        }
        else if (str == "u")
        {
            alert = "修改";
        }
        else if (str == "d")
        {
            alert = "删除";
        }
        if (sql.Contains("shopping") && str != "d")
        {
            alert = "购买";
        }



        SqlConnection conn = new SqlConnection();//创建连接对象
        conn.ConnectionString = conn1;//给连接字符串赋值
        conn.Open();//打开数据库

        SqlCommand cmd = new SqlCommand(sql, conn);
        int i = cmd.ExecuteNonQuery();
        conn.Close();//关闭数据库
        if (i >= 1)
        {
            fun.AJAXalert(c, "alert('" + alert + "成功');location='" + url + "'");
        }
        else
        {
            fun.AJAXalert(c, "alert('" + alert + "失败');location='" + url + "'");
        }
    }




    public override DataTable GetDataTable(string sql)
    {
        SqlDataAdapter Dar = new SqlDataAdapter(sql, conn1);

        DataSet ds = new DataSet();
        Dar.Fill(ds);
        return ds.Tables[0];
    }

    public override bool DoSqlAJAX(string sql)
    {



        SqlConnection conn = new SqlConnection();//创建连接对象
        conn.ConnectionString = conn1;//给连接字符串赋值
        conn.Open();//打开数据库

        SqlCommand cmd = new SqlCommand(sql, conn);
        int i = cmd.ExecuteNonQuery();
        conn.Close();//关闭数据库
        if (i >= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
