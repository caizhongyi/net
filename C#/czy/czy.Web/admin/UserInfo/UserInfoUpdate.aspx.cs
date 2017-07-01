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
using czy.BBL;


namespace mston.AdminManager.UserInfo
{
    public partial class UserInfoUpdate : System.Web.UI.Page
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
               RolesBind(DropRoles);
               DataSet ds= GetData(Convert .ToInt32( user.u_id));
               user.u_name = ds.Tables[0].Rows[0]["u_name"].ToString();
               user.u_roleId = Convert.ToInt32(ds.Tables[0].Rows[0]["u_roleId"]);
               user.u_state = ds.Tables[0].Rows[0]["u_state"].ToString();
               SetValue(user);
            
            }

        }
        private DataSet GetData(int id)
        {
            return new DataSet();
        }

        private bool UpdateUserInfo(czy.Models.UserInfo userInfo)
        {
            return false;
        }

        private void SetValue(czy.Models.UserInfo userInfo)
        {
            TxtUserName.Text = userInfo.u_name;
            bool state=Convert.ToBoolean( userInfo.u_state);
            if (state)
            {
                RadioUse.Checked = true;
            }
            else
            {
                RadioUnUse.Checked = true;
            }
            DropRoles.SelectedIndex = DropRoles.Items.IndexOf(DropRoles.Items.FindByValue(userInfo.u_roleId.ToString()));
        }
        private void RolesBind(DropDownList DropRoles)
        {
            string sql = "select * from rolesInfo";
            DropRoles.DataTextField = "r_name";
            DropRoles.DataValueField = "r_id";
            DropRoles.DataSource = RolesInfo.Select();
            DropRoles.DataBind();
        }
        private bool CheckData(czy.Models.UserInfo userInfo)
        {
             if (czy.MyClass.Data.Validate.HasIllegalStr(userInfo.u_name))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('用户名不能包行：”#……%等特殊字!')</script>");
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void BtnAdd_Click1(object sender, EventArgs e)
        {
           
            user.u_name = TxtUserName.Text.Trim();
            if (RadioUnUse.Checked == true)
            {
                user.u_state = "0";
            }
            else
            {
                user.u_state = "1";
            }
            user.u_roleId =Convert .ToInt32(DropRoles.SelectedValue);
            if (CheckData(user))
            {
                UpdateUserInfo(user);
            }
            Response.Redirect("UserInfo.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfo.aspx");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Write(string.Format("<script>window.open('UpdatePassword.aspx?id={0}','_self');</script>",user.u_id.ToString ()));
        }
    }
}
