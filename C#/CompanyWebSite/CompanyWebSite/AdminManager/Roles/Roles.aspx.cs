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
using Wuqi.Webdiyer;
using MyClass;


namespace mston.AdminManager.Roles
{
    public partial class Roles : System.Web.UI.Page
    {
        AspNetPager _aspNetPager;
        protected void Page_Load(object sender, EventArgs e)
        {
            Common.AuthenticationLogin(this);
            Common.IsAdmin(this);

            _aspNetPager = new AspNetPager();
            AddAspNetPager(PlaceHolder1, _aspNetPager);
            _aspNetPager.PageChanging += new PageChangingEventHandler(_aspNetPager_PageChanging);
            if (!IsPostBack)
            {
                RolesBind(GetRolesData(), 0, RepRoles);
            }
        }
        void RolesBind(DataSet ds, int cur, Repeater rep)
        {
            AspNetPager(cur, ds, rep, 10, _aspNetPager);
        }

        public void AspNetPager(int cur, DataSet ds, Repeater rep, int pageSize, AspNetPager _aspNetPager)
        {
            PagedDataSource pds = new PagedDataSource();
            pds.DataSource = ds.Tables[0].DefaultView;
            pds.AllowPaging = true;
            pds.PageSize = pageSize;
            if (pds.PageCount - 1 < cur)
            {
                if (cur != 0)
                {
                    cur -= 1;

                }
            }
            pds.CurrentPageIndex = cur;
            _aspNetPager.RecordCount = pds.DataSourceCount;
            _aspNetPager.PageSize = pageSize;
            _aspNetPager.CurrentPageIndex = cur + 1;
            _aspNetPager.CssClass = "paginator";
            rep.DataSource = pds;
            rep.DataBind();
        }

        void AddAspNetPager(PlaceHolder pl, AspNetPager _aspNetPager)
        {

            _aspNetPager.FirstPageText = "首页";
            _aspNetPager.LastPageText = "尾页";
            _aspNetPager.NextPageText = "下一页";
            _aspNetPager.PrevPageText = "上一页";
            _aspNetPager.ShowInputBox = ShowInputBox.Never;

            pl.Controls.Add(_aspNetPager);
        }



        public void _aspNetPager_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
        {
            RolesBind(GetRolesData(), e.NewPageIndex - 1, RepRoles);
        }



        private DataSet  GetRolesData()
        {
            string sql = "select * from RolesInfo";
            return T_BasePage.DB.GetDataSet(sql);
           
        }

        protected void RepRoles_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Company.Models.RolesInfo roles = new Company.Models.RolesInfo();
            roles .r_id= Convert.ToInt32(((Label)RepRoles.Items[e.Item.ItemIndex].FindControl("LabID")).Text);
            if (e.CommandName == "Edit")
            {
                Response.Redirect("RolesUpdate.aspx?id=" + roles.r_id.ToString ());
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>window.open(\"UserInfoUpdate.aspx?id=" + userInfo.UserInfoId+"\",'_self')</script>");
            }
            if (e.CommandName == "Del")
            {
                DelRoles(roles);
                RolesBind(GetRolesData(), _aspNetPager.CurrentPageIndex  - 1, RepRoles);
            }
        }
        private bool DelRoles(Company.Models.RolesInfo roles)
        {
            string sql = string.Format("delete from rolesInfo where r_id='{0}'", roles.r_id);
            return T_BasePage.DB.ExecuteNonQuery(sql) > 0 ? true : false;
        }


        protected void RepRoles_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ListItemType.AlternatingItem == e.Item.ItemType || ListItemType.Item == e.Item.ItemType)
            {

                LinkButton linkbtn = (LinkButton)e.Item.FindControl("linkbtnDel");
                JavaScriptHelper.ShowConFirm(linkbtn, "是否删除？", null);
            }
        }
    }
}
