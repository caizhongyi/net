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
    Company.Models.UserInfo user = new Company.Models.UserInfo();
    protected long id;
    MyClass.User.Login.ILogin ilogin = new MyClass.User.Login.SqlLogin(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!ilogin.IsLogin)
        {
            Response.Redirect("Login.aspx");
        }
        else
        {

            user.u_name = ilogin.UserName;
            lab_userName.Text = user.u_name;
            DataSet ds = GetUserData(user);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lab_userRole.Text = ds.Tables[0].Rows[0]["r_name"].ToString();
            }
        }
    }

    private DataSet GetUserData(Company.Models.UserInfo user)
    {
        string sql = string.Format("select * from v_userInfo where u_name='{0}'", user.u_name.Trim ());
        return T_BasePage.DB.GetDataSet(sql);
    }



    protected void logout_Click(object sender, ImageClickEventArgs e)
    {
        ilogin.LoginOut();
        Response.Redirect("Login.aspx");

    }
}
