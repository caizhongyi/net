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
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace WebSite.UserInfo
{
    public class ForgottenPassword : System.Web.UI.Page
    {
        WebSiteMaster UserControlMaster;
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            UserControlMaster = (WebSiteMaster)FindControl("WebUserActControl");
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
            DataProviders.DataConnectionHepler MyConnection = DataProviders.DataConnectionHepler.Instance();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            Components.SiteWebSetting.WebSiteTitle = MyConnection.GetTreeXmlNode("WebSiteTitle", "UserInfoForgottenPassword");
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardNavigate");
            Control.BoardNavigate IBoardNavigate = new Control.BoardNavigate();
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardAllListNavigate", IBoardNavigate.GetBoardAllNavigateTitle);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_WebSiteForgottenPasswordTitle"));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteForgottenPassword");
            UserControlMaster.BindMsater();
        }
    }
}
