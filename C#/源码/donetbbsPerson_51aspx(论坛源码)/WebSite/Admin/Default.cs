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
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace WebSite.Admin
{
    public class DeFault : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        Panel ManageBoardPanel;
        Panel ManageUserPanel;
        Panel ManageSystemPanel;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            ManageBoardPanel = (Panel)FindControl("ManageBoardPanel");
            ManageUserPanel = (Panel)FindControl("ManageUserPanel");
            ManageSystemPanel = (Panel)FindControl("ManageSystemPanel");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
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
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (!currentUser.IsHaveAdministrator)
            {
                IDoNetBbs.WriteAlert("您没有管理的权限",false);
                IDoNetBbs.WriteRedirect("../", true, false);
                Page.Response.End();
            }
            if (currentUser.IsSystemAdministrator || currentUser.IsBoardAdministrator)
            {
                ManageBoardPanel.Visible = true;
            }

            if (currentUser.IsSystemAdministrator || currentUser.IsMembershipAdministrator)
            {
                ManageUserPanel.Visible = true;
            }

            if (currentUser.IsSystemAdministrator)
            {
                ManageSystemPanel.Visible = true;
            }
        }
    }
}
