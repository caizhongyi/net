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
using System.Collections;
namespace WebSite.Admin.User
{
    public class UserLevelList : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        Label PageListText;
        Repeater dataRepeater;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            PageListText = (Label)FindControl("PageListText");
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
            Control.PageListNavigate IPageListNavigate = new Control.PageListNavigate();
            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            Components.Components.UserLevel IUserLevel = new Components.Components.UserLevel();
            //controls.PageListNavigate pagelist = new controls.PageListNavigate();
            //DataProviders.DataConnectionHepler MyConnection = DataProviders.DataConnectionHepler.Instance();


            IUserLevel.Arraylist = MyUser.SetUserLevel(false);
            //DataProviders.UserLevelDataProviders userLevel = new DataProviders.UserLevelDataProviders();


            //pagelist.navigateurl = "searchKey=" + searchKey.Text + "&searchSex=" + searchSex.SelectedValue + "&searchRegTime=" + searchRegTime.SelectedValue + "&searchOrderby=" + searchOrderby.SelectedValue + "";
            IPageListNavigate.page = IDoNetBbs.GetQueryInt("page");

            IPageListNavigate.pagenumber = 9;
            IPageListNavigate.countnumber = IUserLevel.Arraylist.Count;

            if (IPageListNavigate.countnumber == 0)
            {
                return;
            }
            //userLevel.SetUserLevel((IUserLevel.page - 1) * IUserLevel.pagenumber, IUserLevel.pagenumber, false);
            //IUserLevel.Arraylist.Add(IUserLevel.Arraylist[1]);

            ArrayList Arraylist = new ArrayList();
            for (int i = (IPageListNavigate.page - 1) * IPageListNavigate.pagenumber; i < ((IPageListNavigate.page - 1) * IPageListNavigate.pagenumber+IPageListNavigate.pagenumber); i++)
            {
                if (i < IUserLevel.Arraylist.Count)
                {
                    Arraylist.Add(IUserLevel.Arraylist[i]);
                }
            }

            dataRepeater.DataSource = Arraylist;
            dataRepeater.DataBind();
            IPageListNavigate.SetNavigate();
            IPageListNavigate.DisplayPageInput = false;
            PageListText.Text = IDoNetBbs.GetFormat(IPageListNavigate.GetPageListNavigateTitle, "PageInputName", "");
        }
        protected void serachButton_Click(object sender, EventArgs e)
        {
            //Page.Response.Redirect("GroupList.aspx?searchKey=" + searchKey.Text + "&searchSex=" + searchSex.Text + "&searchRegTime=" + searchRegTime.Text + "&searchOrderby=" + searchOrderby.Text + "");
            Response.End();
        }
    }
}
