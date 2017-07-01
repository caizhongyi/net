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
    public class UserRoleList : System.Web.UI.Page
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
            dataRepeater = (Repeater)FindControl("dataRepeater");
            PageListText = (Label)FindControl("PageListText");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (!currentUser.IsSystemAdministrator)
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

            //controls.PageListNavigate pagelist = new controls.PageListNavigate();
            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            Components.Components.UserRole IUserRole = new Components.Components.UserRole();

            IUserRole.Arraylist = MyUser.SetUserRole(false);
            //DataProviders.UserRoleDataProviders userRole = new DataProviders.UserRoleDataProviders();
            IPageListNavigate.page = IDoNetBbs.GetQueryInt("page");
            IPageListNavigate.pagenumber = 9;
            IPageListNavigate.countnumber = IUserRole.Arraylist.Count;
            if (IPageListNavigate.countnumber == 0)
            {
                return;
            }
            ArrayList Arraylist = new ArrayList();
            for (int i = (IPageListNavigate.page - 1) * IPageListNavigate.pagenumber; i < ((IPageListNavigate.page - 1) * IPageListNavigate.pagenumber + IPageListNavigate.pagenumber); i++)
            {
                if (i < IUserRole.Arraylist.Count)
                {
                    Arraylist.Add(IUserRole.Arraylist[i]);
                }
            }


            //userRole.SetUserRole((pagelist.page - 1) * pagelist.pagenumber, pagelist.pagenumber, false);
            dataRepeater.DataSource = Arraylist;
            dataRepeater.DataBind();
            IPageListNavigate.SetNavigate();
            IPageListNavigate.DisplayPageInput = false;
            PageListText.Text = IDoNetBbs.GetFormat(IPageListNavigate.GetPageListNavigateTitle, "PageInputName", "");
        }
    }
}
