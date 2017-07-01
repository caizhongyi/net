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
using System.Web;
namespace WebSite
{
    public partial class WebSiteMaster : DosOrg.WebSiteMaster
    {
        protected override void DataBindChildren()
        {
            base.DataBindChildren();
            SetWebSiteMaster();
        }
        /// <summary>
        /// 设置页面信息
        /// </summary>
        private void SetWebSiteMaster()
        {
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            DataProviders.DataConnectionHepler MyConnection = DataProviders.DataConnectionHepler.Instance();
            DosOrg.User.User currentUser = new DosOrg.User.User();
            WebSiteHead = MyConnection.GetTempXmlNode("Resource_WebSiteHead");
            WebSiteHead = IDoNetBbs.GetFormat(WebSiteHead, "WebSite_PageStyle", currentUser.UserStyle);
            
            WebSiteMasterBody = MyConnection.GetTempXmlNode("Resource_WebSiteWebTop");
            WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteNavigate");
            if (currentUser.UserID == 0)
            {
                WebSiteMasterBody = IDoNetBbs.GetFormat(WebSiteMasterBody, "WebSite_UserLoginControl", MyConnection.GetTempXmlNode("Resource_WebSiteUserLoginControlOut"));
                WebSiteMasterBody = IDoNetBbs.GetFormat(WebSiteMasterBody, "WebSite_UserLoginOut", null);
                WebSiteMasterBody = IDoNetBbs.GetFormat(WebSiteMasterBody, "UserNickName", currentUser.UserGuestName);
            }
            else
            {
                WebSiteMasterBody = IDoNetBbs.GetFormat(WebSiteMasterBody, "WebSite_UserLoginControl", MyConnection.GetTempXmlNode("Resource_WebSiteUserLoginControlLogined"));
                WebSiteMasterBody = IDoNetBbs.GetFormat(WebSiteMasterBody, "WebSite_UserLoginOut", MyConnection.GetTempXmlNode("Resource_WebSite_UserLoginOut"));
                WebSiteMasterBody = IDoNetBbs.GetFormat(WebSiteMasterBody, "UserNickName", currentUser.UserNickName);
            }
            if (currentUser.IsHaveAdministrator)
            {
                WebSiteMasterBody = IDoNetBbs.GetFormat(WebSiteMasterBody, "WebSite_UserLoginPanl", MyConnection.GetTempXmlNode("Resource_WebSiteUserLoginPanl"));
            }
            else
            {
                WebSiteMasterBody = IDoNetBbs.GetFormat(WebSiteMasterBody, "WebSite_UserLoginPanl", null);
            }
        }
    }
}