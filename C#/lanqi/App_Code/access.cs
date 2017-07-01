using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// access 的摘要说明
/// </summary>
public class access:DataProvider
{
	public access()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static string connstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.Web.HttpContext.Current.Server.MapPath("~/App_Data/WebCpAccess.mdb");
 
public override  DataSet GreatDs(string sql)
    {
        OleDbDataAdapter Dar = new OleDbDataAdapter(sql, connstring);

        DataSet ds = new DataSet();
        Dar.Fill(ds);
        return ds;
    }
    public static access getaccess()
    {
        return new access();
    }
    public override  void DoSql(Control c, string sql)
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
        if (sql.Contains("shopping")&&str!="d")
        {
            alert = "购买";
        }
        


        OleDbConnection conn = new OleDbConnection();//创建连接对象
        conn.ConnectionString = connstring;//给连接字符串赋值
        conn.Open();//打开数据库
       
        OleDbCommand cmd = new OleDbCommand(sql, conn);
        int i=cmd.ExecuteNonQuery();
        conn.Close();//关闭数据库
        if (i >= 1)
        {
            fun.AJAXalert(c,alert+"成功");
        }
        else
        {
            fun.AJAXalert(c,  alert + "失败");
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



        OleDbConnection conn = new OleDbConnection();//创建连接对象
        conn.ConnectionString = connstring;//给连接字符串赋值
        conn.Open();//打开数据库

        OleDbCommand cmd = new OleDbCommand(sql, conn);
        int i = cmd.ExecuteNonQuery();
        conn.Close();//关闭数据库
        if (i >= 1)
        {
            fun.AJAXalert(c,"alert('"+ alert + "成功');location='"+url+"'");
        }
        else
        {
            fun.AJAXalert(c, "alert('" + alert + "失败');location='" + url + "'");
        }
    }




    public override DataTable GetDataTable(string sql)
    {
        OleDbDataAdapter Dar = new OleDbDataAdapter(sql, connstring);

        DataSet ds = new DataSet();
        Dar.Fill(ds);
        return ds.Tables[0];
    }

    public override bool DoSqlAJAX(string sql)
    {



        OleDbConnection conn = new OleDbConnection();//创建连接对象
        conn.ConnectionString = connstring;//给连接字符串赋值
        conn.Open();//打开数据库

        OleDbCommand cmd = new OleDbCommand(sql, conn);
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
