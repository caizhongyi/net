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

namespace mston.AdminManager.Roles
{
    public partial class RolesUpdate : System.Web.UI.Page
    {
        Company.Models.RolesInfo roles=new Company.Models.RolesInfo ();
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.AuthenticationLogin(this);
            Common.IsAdmin(this);


            if (Request.QueryString["id"] != null)
            {
                roles.r_id = Convert.ToInt32(Request .QueryString ["id"]);
            }
            
            if(!IsPostBack )
            {
                DataSet ds = GetRolesData(roles);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    TxtRolesName.Text = ds.Tables[0].Rows[0]["r_name"].ToString ();
                    TxtRolesRight.Text = ds.Tables[0].Rows[0]["r_right"].ToString();
                }
            }
           
        }

        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            
            roles.r_name = TxtRolesName.Text.Trim();
            try
            {
                roles.r_right = Convert.ToInt32(TxtRolesRight.Text);
                if (roles.r_right < 0)
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('权限值应为非负值!')</script>");
                    return;
                }
            }
            catch { ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('权限值应为数值!')</script>"); return; }

            if (roles.r_name == string.Empty)
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('请输入角色名称!')</script>");
            }
            else if (MyClass.MyValidate.Validate.HasIllegalStr(roles.r_name))
            {
                ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>alert('角色名不能包含！·￥#%……等特殊字符!')</script>");
            }
            else
            {
                Update(roles);
                Response.Redirect("Roles.aspx");
            }
        }

        private DataSet GetRolesData(Company.Models.RolesInfo role)
        {
            string sql = string.Format("select * from RolesInfo where r_id='{0}'", role.r_id);
            return T_BasePage.DB.GetDataSet(sql);
        }

        private bool Update(Company.Models.RolesInfo role)
        {
            string sql = string.Format("Update  RolesInfo set r_name=N'{0}',r_right='{2}' where r_id='{1}'",role.r_name.ToSQLString(),role.r_id.ToString (),roles .r_right.ToString());
            return T_BasePage.DB.ExecuteNonQuery(sql) > 0 ? true : false;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Roles.aspx");
        }
    }
}
