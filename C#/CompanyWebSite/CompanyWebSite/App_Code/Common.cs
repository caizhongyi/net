using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
///Common 的摘要说明
/// </summary>
public class Common
{
    static int right = 0;
    public Common()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    public static void AuthenticationLogin(Page page)
    {
        MyClass.User.Login.ILogin l = new MyClass.User.Login.SqlLogin(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
        if (!l.IsLogin)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "", "<script>this.parent.window.open('../Login.aspx','_self');</script>");
        }
        else
        {
            //  IsAdmin(page, right);
        }
    }
    public static void AuthenticationLogin(Page page,string url)
    {
        MyClass.User.Login.ILogin l = new MyClass.User.Login.SqlLogin(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
        if (!l.IsLogin)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "", "<script>this.parent.window.open('"+url+"','_self');</script>");
        }
        else
        {
            //  IsAdmin(page, right);
        }
    }
    public static void IsAdmin(Page page)
    {
        MyClass.User.Login.ILogin l = new MyClass.User.Login.SqlLogin(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
        int userId = Convert.ToInt32(l.UserId);
        string sql = "select r_right from v_userinfo where u_id=" + userId;
        int r = Convert .ToInt32 ( T_BasePage.DB.ExecuteScalar(sql));
        if (r != right)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "", "<script>alert('您所在的帐户组没有权限访问！');this.parent.window.open('../index.aspx','_self');</script>");
        }

    }
}
