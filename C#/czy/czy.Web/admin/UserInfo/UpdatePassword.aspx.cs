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
    public partial class UpdatePassword : System.Web.UI.Page
    {
        czy.Models.UserInfo userInfo=new czy.Models.UserInfo ();
        string tempPwd = string.Empty;
        string url = "UserInfo.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase.AuthenticationLogin();
           
            if (Request.QueryString["id"] != null)
            {
                userInfo.u_id = Convert.ToInt32(Request.QueryString["id"]);
            }
            if (Request.QueryString["url"] != null)
            {
                url = Request.QueryString["url"].ToString();
            }
            userInfo.u_pwd= TxtPwd.Text.Trim();
            tempPwd = TxtPwd1.Text.Trim();
            userInfo.u_name = GetData(Convert .ToInt32( userInfo.u_id)).Tables[0].Rows[0]["u_name"].ToString();
            LabUserName.Text = userInfo.u_name;
            
        }
        private bool UpdateUserInfo(czy.Models.UserInfo userInfo)
        {
            return false;
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            if (ChekcPwd(userInfo))
            {
                if (UpdateUserInfo(userInfo))
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('密码修改成功!');window.open('" + url + "','_self');</script>");
                }
                else
                {
                    JavaScriptHelper.ShowMsg(this, "密码修改失败!");
                }
            }
        }

        private bool ChekcPwd(czy.Models.UserInfo userInfo)
        {
            if (userInfo.u_pwd == string.Empty)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('请输入密码!')</script>");
                return false;
            }
            else if (userInfo.u_pwd.Trim() != tempPwd.Trim())
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('密码不一致请重新输入!')</script>");
                return false;
            }
            else
            {
                return true;
            }
        }

        private DataSet GetData(int id)
        {
           return  czy.BBL.UserInfo.Select();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}
