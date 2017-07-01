using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// DataProvider 的摘要说明
/// </summary>
public abstract class DataProvider
{
	public DataProvider()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public  abstract DataSet GreatDs(string sql);
    public  abstract void DoSql(Control c, string sql);
    public  abstract void DoSql(Control c, string sql, string url);
    public  abstract DataTable GetDataTable(string sql);
    public  abstract bool DoSqlAJAX(string sql);
}
