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
using BBL;

namespace mston.AdminManager.Roles
{
    public partial class RolesEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PageBase.AuthenticationLogin();
        }


        //private int Insert(czy.Models.RolesInfo roles)
        //{
        //    string sql = string.Format("insert into rolesInfo(r_name,r_right) values(N'{0}','{1}')", roles.r_name, roles.r_right);
        //    return RolesInfo.Insert(roles);
        //}

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            //czy.Models.RolesInfo roles = new czy.Models.RolesInfo();
            //roles.r_name = TxtRolesName.Text.Trim();
            //try
            //{
            //    roles.r_right = Convert.ToInt32(TxtRightValue.Text.Trim());
            //    if (roles.r_right < 0)
            //    {
            //        ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('权限值应为非负值!')</script>");
            //        return;
            //    }

            //}
            //catch { ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('权限值应为数值!')</script>"); return; }

            //if (roles.r_name == string.Empty)
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('请输入角色名称!')</script>");
            //}
            //else if (czy.MyClass.Data.Validate.HasIllegalStr(roles.r_name))
            //{
            //    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('角色名不能包含！·￥#%……等特殊字符!')</script>");
            //}
            //else
            //{
            //    Insert(roles);
            //    Response.Redirect("Roles.aspx");
            //}
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Response.Redirect("Roles.aspx");
        }

  
 
    
    }
}
