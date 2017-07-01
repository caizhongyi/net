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
using System.Text;


namespace mston.AdminManger.UserManger
{
    public partial class UserInfoEdit : System.Web.UI.Page
    {
        string tempPwd = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.AuthenticationLogin(this);
            Common.IsAdmin(this);

            if (!IsPostBack)
            {
                RolesBind(DropRoles);
            }
        }

        protected void BtnAdd_Click1(object sender, EventArgs e)
        {
            Company.Models.UserInfo user = new Company.Models.UserInfo();
            user.u_name = TxtUserName.Text.Trim();
            user.u_pwd =TxtPwd.Text.Trim();
            tempPwd = TxtPwd1.Text.Trim();
            user.u_createDate = DateTime.Now;
            user.u_loginDate = DateTime.Now;
            user.u_state = "1";
            user.u_isDel = false;
            user.u_loginTotalTime = 0;
            user.u_loginTime = 0;
            user.u_roleId =Convert .ToInt32( DropRoles.SelectedValue);
            if (CheckData(user))
            {
                if (Insert(user) > 0)
                {
                    Response.Redirect("UserInfo.aspx");
                }
            }
        }
        private int Insert(Company.Models.UserInfo userInfo)
        {
            //try
            //{
            object[] param=new  object[]{
                userInfo .u_name.ToSQLString (),
                MyClass.Encrypt.MD5Encrypt.Md5Code(userInfo.u_pwd),
                userInfo.u_roleId,
                userInfo.u_createDate.ToString("yyyy-MM-dd"),
                userInfo.u_isDel,
                userInfo.u_loginDate.ToString("yyyy-MM-dd"),
                userInfo.u_loginTime,
                userInfo.u_loginTotalTime,
                userInfo.u_state};
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("insert userinfo values(N'{0}',N'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}')", param);
            return T_BasePage.DB.ExecuteNonQuery(sb.ToString()) ;
            //}
            //catch 
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('添加失败!')</script>");
            //    return 0;
            //}
        }

        private void RolesBind(DropDownList DropRoles)
        {
            string sql = "select * from rolesInfo";
            DropRoles.DataTextField = "r_name";
            DropRoles.DataValueField = "r_id";
            DropRoles.DataSource =T_BasePage.DB.GetDataSet(sql);
            DropRoles.DataBind();
        }

        private bool CheckData(Company.Models.UserInfo userInfo)
        {
            if (userInfo.u_name == string.Empty)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('请输入用户名!')</script>");
                return false;
            }
            else if (MyClass.MyValidate.Validate.HasIllegalStr(userInfo.u_name))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('用户名不能包行：”#……%等特殊字!')</script>");
                return false;
            }
            else if (IsExitsUser(userInfo))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('用户名已存在!')</script>");
                return false;
            }
            else if (userInfo.u_pwd == string.Empty)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('请输入密码!')</script>");
                return false;
            }
            else if (userInfo.u_pwd!= tempPwd )
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('密码不一致请重新输入!')</script>");
                return false;
            }
            else
            {
                return true;
            }
        }

        private bool IsExitsUser(Company.Models.UserInfo userInfo)
        {
            string sql =string .Format ("select count(*) from userinfo where u_name=N'{0}'",userInfo.u_name);
            return Convert .ToInt32( T_BasePage.DB.ExecuteScalar(sql)) > 0 ? true : false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserInfo.aspx");
        }

     
    }
}
