//===============================================
//　　　　　　　　　　\\\|///                      
//　　　　　　　　　　\\　- -　//                   
//　　　　　　　　　　  ( @ @ )                    
//┏━━━━━━━━━oOOo-(_)-oOOo━━━┓          
//┃                                     ┃
//┃             东 网 原 创！           ┃
//┃      lenlong 作品，请保留此信息！   ┃
//┃      ** lenlenlong@hotmail.com **   ┃
//┃                                     ┃
//┃　　　　　　　　　　　　　Dooo　     ┃
//┗━━━━━━━━━ oooD━-(　 )━━━┛
//　　　　　　　　　　 (  )　  ) /
//　　　　　　　　　　　\ (　 (_/
//　　　　　　　　　　　 \_)
//===============================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace WebSite.Admin.User
{
    public class UserOnlineList : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        Label PageListText;
        TextBox searchKey;
        DropDownList searchRegTime;
        DropDownList searchOrderby;
        LinkButton serachButton;
        Repeater dataRepeater;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            PageListText = (Label)FindControl("PageListText");
            searchKey = (TextBox)FindControl("searchKey");
            searchRegTime = (DropDownList)FindControl("searchRegTime");
            searchOrderby = (DropDownList)FindControl("searchOrderby");
            serachButton = (LinkButton)FindControl("serachButton");
            dataRepeater = (Repeater)FindControl("dataRepeater");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (!currentUser.IsSystemAdministrator && !currentUser.IsMembershipAdministrator)
            {
                IDoNetBbs.WriteAlert("您没有操作的权利", false);
                IDoNetBbs.WriteWindowClose(false);
                Page.Response.End();
            }
            if (IsPostBack)
            {
            }
            else
            {
                DataBind();
            }
        }
        public override void DataBind()
        {
            base.DataBind();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            //controls.PageListNavigate pagelist = new controls.PageListNavigate();
            Control.PageListNavigate IPageListNavigate = new Control.PageListNavigate();

            DataProviders.UserOnLineDataProvider MyUserOnLine = DataProviders.UserOnLineDataProvider.Instance();
            Components.Components.UserOnLine IUserOnLine = new Components.Components.UserOnLine();

            //DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            //Components.Components.User IUser = new Components.Components.User();

            //DataProviders.UserOnlineDataProviders userOnline = new DataProviders.UserOnlineDataProviders();
            int deleteUserOnLineID = IDoNetBbs.GetQueryInt("deleteUserOnLineID");
            if (deleteUserOnLineID != 0)
            {
                MyUserOnLine.DeleteUserOnLine(deleteUserOnLineID);
                Components.CsCache.Clear();
                HttpContext.Current.Response.Write("<script>parent.window.location.reload();</script>");
                Response.End();
            }


            if (IDoNetBbs.GetQueryString("searchKey") != string.Empty)
            {
                searchKey.Text = IDoNetBbs.GetQueryString("searchKey");
            }
            searchOrderby.Items.Add(new ListItem("最后活动", "UserOnLineLastTime"));
            searchOrderby.Items.Add(new ListItem("用户昵称", "UserOnLineUserNickName"));
            searchOrderby.Items.Add(new ListItem("用户ID", "UserOnLineUserID"));
            searchOrderby.Items.Add(new ListItem("IP", "UserOnLineIP"));
            searchOrderby.Items.Add(new ListItem("来源", "UserOnLineComeFromPath"));
            searchOrderby.Items.Add(new ListItem("所在位置", "UserOnLineBrowserPath"));
            searchOrderby.Items.Add(new ListItem("浏览器", "UserOnLineBrowser"));
            searchOrderby.Items.Add(new ListItem("操作系统", "UserOnLineSystem"));
            if (IDoNetBbs.GetQueryString("searchOrderby") != string.Empty)
            {
                searchOrderby.Items.FindByValue(IDoNetBbs.GetQueryString("searchOrderby")).Selected = true;
            }
            searchRegTime.Items.Add(new ListItem("全部", ""));
            searchRegTime.Items.Add(new ListItem("最近一天", "1"));
            searchRegTime.Items.Add(new ListItem("最近三天", "3"));
            searchRegTime.Items.Add(new ListItem("最近一周", "7"));
            searchRegTime.Items.Add(new ListItem("本月", "31"));
            searchRegTime.Items.Add(new ListItem("本季度", "90"));
            searchRegTime.Items.Add(new ListItem("最近半年", "180"));
            searchRegTime.Items.Add(new ListItem("最近一年", "365"));
            searchRegTime.Items.Add(new ListItem("最近两年", "730"));
            searchRegTime.Items.Add(new ListItem("最近三年", "1095"));
            if (IDoNetBbs.GetQueryString("searchRegTime") != string.Empty)
            {
                searchRegTime.Items.FindByValue(IDoNetBbs.GetQueryString("searchRegTime")).Selected = true;
            }


            string sql = string.Empty;
            sql = " from DoNetBbs_UserOnLine  where 1=1";
            if (searchKey.Text.Trim() != string.Empty)
            {
                sql += " and UserOnLineUserNickName like '%" + searchKey.Text.Trim() + "%'";
            }
            if (searchRegTime.SelectedValue != string.Empty)
            {
                sql += " and DateDiff(" + IDoNetBbs.GetAccessDate("d") + ",UserOnLineLastTime,'" + DateTime.Now + "')<=" + searchRegTime.SelectedValue + "";
            }//
            IPageListNavigate.page = IDoNetBbs.GetQueryInt("page");

            IPageListNavigate.pagenumber = 16;
            IPageListNavigate.countnumber = MyUserOnLine.SetUserOnLineCount("select count(UserOnLineID) as CountNumber " + sql, false);

            if (IPageListNavigate.countnumber == 0)
            {
                return;
            }
            IUserOnLine.Arraylist = MyUserOnLine.SetUserOnLineList("select * " + sql + " order by " + searchOrderby.Text + " desc", (IPageListNavigate.page - 1) * IPageListNavigate.pagenumber, IPageListNavigate.pagenumber, false);

            dataRepeater.DataSource = IUserOnLine.Arraylist;
            dataRepeater.DataBind();
            IPageListNavigate.SetNavigate();
            IPageListNavigate.navigateurl = "searchKey=" + searchKey.Text + "&searchRegTime=" + searchRegTime.SelectedValue + "&searchOrderby=" + searchOrderby.SelectedValue + "";
            IPageListNavigate.DisplayPageInput = false;
            PageListText.Text = IDoNetBbs.GetFormat(IPageListNavigate.GetPageListNavigateTitle, "PageInputName", "");
        }
        protected void serachButton_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("UserOnlineList.aspx?searchKey=" + searchKey .Text+ "&searchRegTime=" + searchRegTime .Text+ "&searchOrderby=" + searchOrderby .Text+ "");
            Response.End();
        }
    }
}
