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
using MyClass;

namespace mston.AdminManager.UserInfo
{
    public partial class UpdatePassword : System.Web.UI.Page
    {
        Company.Models.UserInfo userInfo=new Company.Models.UserInfo ();
        string tempPwd = string.Empty;
        string url = "UserInfo.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.AuthenticationLogin(this);

           
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
        private bool UpdateUserInfo(Company.Models.UserInfo userInfo)
        {
            string sql = string.Format("update userInfo set u_pwd=N'{0}' where u_id='{1}'",
               MyClass.Encrypt.MD5Encrypt.Md5Code(userInfo.u_pwd.ToSQLString()),
                userInfo.u_id.ToString());
            return T_BasePage.DB. ExecuteNonQuery(sql) > 0 ? true : false;
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

        private bool ChekcPwd(Company.Models.UserInfo userInfo)
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
            string sql = string.Format("select * from userInfo where u_id={0}", id);
            return T_BasePage.DB.GetDataSet(sql);
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect(url);
        }
    }
}
