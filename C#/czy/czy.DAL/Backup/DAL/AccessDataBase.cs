using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

/// <summary>
/// AccessDataBase 的摘要说明
/// </summary>
public class AccessDataBase
{
    /// <summary>
    /// 虚拟路径
    /// </summary>
    private string _dbUrl;

    public string DbUrl
    {
        get { return _dbUrl; }
    }
    private string strConnection;

    public string StrConnection
    {
        get { return strConnection; }
    }
    /// <summary>
    /// 初始化实例
    /// </summary>
    /// <param name="dbUrl">虚拟路径</param>
    /// <param name="pwd">密码,无则为空</param>
    public AccessDataBase(string dbUrl,string pwd)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
        _dbUrl = dbUrl;
        strConnection = "Provider=Microsoft.Jet.OLEDB.4.0; " +
       "Data Source=" + System.Web.HttpContext.Current.Server.MapPath(dbUrl) + ";Jet OLEDB:Database password="+pwd;
    }
    /// <summary>
    /// access连接数据库
    /// </summary>
    /// <param name="cmd"></param>
    /// <returns></returns>
    public  int oledbExecuteNonQuery(string cmd)
    {
        OleDbConnection myConnection = new OleDbConnection(strConnection);
        myConnection.Open();
        OleDbCommand myCommand = new OleDbCommand(cmd, myConnection);
        int i = myCommand.ExecuteNonQuery();
        myConnection.Close();
        return i;
    }

    public  DataTable oledbGetDateTable(string cmd)
    {
        
        OleDbConnection myConnection = new OleDbConnection(strConnection);
        myConnection.Open();
        OleDbDataAdapter da = new OleDbDataAdapter();
        da.SelectCommand = new OleDbCommand(cmd, myConnection);
        DataTable dt = new DataTable();
        da.Fill(dt);
        myConnection.Close();
        return dt;
   
    }
}
