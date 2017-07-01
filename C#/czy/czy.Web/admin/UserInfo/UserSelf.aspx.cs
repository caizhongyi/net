using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using czy.MyClass;


namespace mston.AdminManager.UserInfo
{
    public partial class UserSelf : System.Web.UI.Page
    {
        czy.Models.UserInfo user = new czy.Models.UserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase.AuthenticationLogin();

            if (Request.QueryString["id"] != null)
            {
                user.u_id = Convert.ToInt32(Request.QueryString["id"]);
            }
            if (!IsPostBack)
            {
              
                DataSet ds = GetData(Convert .ToInt32( user.u_id));
                TxtUserName.Text = ds.Tables[0].Rows[0]["u_name"].ToString();
                LabRoles.Text = ds.Tables[0].Rows[0]["r_name"].ToString();
                LabState.Text =ds.Tables[0].Rows[0]["u_state"].ToString ()=="True"?"启用":"禁用";
                

            }

        }
        private DataSet GetData(int id)
        {
            return new DataSet();
        }

        private bool UpdateUserInfo(czy.Models.UserInfo userInfo)
        {
            string sql = string.Format("update userInfo set u_name=N'{0}' where u_id='{1}'", userInfo.u_name.ToSQLString (), userInfo.u_id.ToString ());
            return czy.BBL.UserInfo.Update(Convert.ToInt32( userInfo.u_id),userInfo)  > 0 ? true : false;
        }

  
        protected void BtnAdd_Click1(object sender, EventArgs e)
        {

            user.u_name = TxtUserName.Text.Trim();
      
            UpdateUserInfo(user);
            JavaScriptHelper.ShowMsg(this,"修改成功!");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("../News/News.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdatePassword.aspx?url=UserSelf.aspx?id=" + user.u_id.ToString() );
        }
    }
}
