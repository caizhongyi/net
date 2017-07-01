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

namespace mston.AdminManger.UserManger
{
    public partial class UserInfo : System.Web.UI.Page
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
                UserInfoBind(GetUserInfoData(), 0, RepUserInfo);
            }
        }

        void UserInfoBind(DataSet ds, int cur, Repeater rep)
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
            UserInfoBind(GetUserInfoData(), e.NewPageIndex - 1, RepUserInfo);
        }

 

        DataSet  GetUserInfoData()
        {
            string sql = "select * from v_userinfo";
            return T_BasePage.DB.GetDataSet(sql);
        }

        protected void RepUserInfo_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Company.Models.UserInfo userInfo = new Company.Models.UserInfo();
            userInfo.u_id = Convert.ToInt32(((Label)RepUserInfo.Items[e.Item.ItemIndex].FindControl("LabID")).Text);
            if (e.CommandName == "Edit")
            {
                Response .Redirect  ("UserInfoUpdate.aspx?id="+userInfo.u_id);
                //ClientScript.RegisterClientScriptBlock(this.GetType(), "", "<script>window.open(\"UserInfoUpdate.aspx?id=" + userInfo.UserInfoId+"\",'_self')</script>");
            }
            if (e.CommandName == "Del")
            {
                DelUserInfo(userInfo);
                UserInfoBind(GetUserInfoData(), _aspNetPager.CurrentPageIndex - 1, RepUserInfo);
            }
        }

        private bool DelUserInfo(Company.Models.UserInfo userInfo)
        {
            string sql = string.Format("delete from userinfo where u_id='{0}'", userInfo.u_id);
            return T_BasePage.DB.ExecuteNonQuery(sql)  > 0 ? true : false;
        }

        protected void RepUserInfo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (ListItemType.AlternatingItem == e.Item.ItemType || ListItemType.Item == e.Item.ItemType)
            {
              
                LinkButton linkbtn = (LinkButton)e.Item.FindControl("linkbtnDel");
                //linkbtn.Attributes.Add("onclick", "<script>return confirm('是否删除？')</script>");
                JavaScriptHelper.ShowConFirm(linkbtn,"是否删除？",null);
            }
        }
    }
}
