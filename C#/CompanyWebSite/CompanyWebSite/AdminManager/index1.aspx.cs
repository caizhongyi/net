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
using mston;


public partial class index : System.Web.UI.Page
{
    Company.Models.UserInfo user=new Company.Models.UserInfo ();
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
            user.u_id = Convert .ToInt64( ilogin.UserId);
            id = user.u_id;
            LabUserName.Text = user.u_name;
            DataSet ds=GetUserData(user);
            if (ds.Tables[0].Rows.Count > 0)
            {
                LabRoles.Text = ds.Tables[0].Rows[0]["roles_name"].ToString();
            }
        }
    }

    private DataSet GetUserData(Company.Models.UserInfo user)
    {
        string sql = string.Format("select * from v_userInfo where u_id='{0}'", user.u_id);
        return T_BasePage.DB.GetDataSet(sql);
    }

    protected void LoginOut_Click(object sender, EventArgs e)
    {
        ilogin.LoginOut();
        Response.Redirect("Login.aspx");
    }



   
}
