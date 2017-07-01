using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class main : System.Web.UI.Page
{
    Models.UserInfo user = new Models.UserInfo();
    protected long id;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        czy.SQLCommon.Login.ILogin login = czy.IFactory.Factory.GetLogin();
        login.IsLoginAndRedirect("Login.aspx", "Index.aspx");
        lab_userName.Text = login.UserName;
        lab_userRole.Text = login.UserInfo.RoleName;
    }

    private DataSet GetUserData(Models.UserInfo user)
    {
       // string sql = string.Format("select * from v_userInfo where u_name='{0}'", user.u_name.Trim ());
       // return T_BasePage.DB.GetDataSet(sql);
        return new DataSet();
    }



    protected void logout_Click(object sender, ImageClickEventArgs e)
    {
       // ilogin.LoginOut();
        czy.SQLCommon.Login.ILogin login = czy.IFactory.Factory.GetLogin();
        login.LoginOut();
        Response.Redirect("Login.aspx");

    }
}
