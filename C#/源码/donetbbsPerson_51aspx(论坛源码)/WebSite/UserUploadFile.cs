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
using System.IO;
namespace WebSite
{
    public class UserUploadFile : System.Web.UI.Page
    {
        FileUpload UploadFile;
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            UploadFile = (FileUpload)FindControl("UploadFile");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                DataBind();
            }
            else
            {
            }
        }
        public override void DataBind()
        {
            base.DataBind();
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            DataProviders.DataConnectionHepler MyConnection = DataProviders.DataConnectionHepler.Instance();
            if (!IDoNetBbs.GetComparison(currentUser.UserRole, true, MyConnection.GetWebSiteConfig("Config_WebSiteUpFileRole"), true))
            {
                IDoNetBbs.WriteAlert(MyConnection.GetResourcesXmlNode("Resource_WebSiteUpFileRoleErr"), false);
                IDoNetBbs.WriteJavaScript("parent.ScreenClear()", false);
                IDoNetBbs.WriteRedirect(MyConnection.GetTreeXmlNode("SiteUrls", "WebSiteUploadFile"), false, false);
                Page.Response.End();
            }
            if (UploadFile.PostedFile.FileName.Length == 0)
            {
                IDoNetBbs.WriteAlert(MyConnection.GetResourcesXmlNode("Resource_WebSiteUpFileNotExist"), false);
                IDoNetBbs.WriteJavaScript("parent.ScreenClear()", false);
                IDoNetBbs.WriteRedirect(MyConnection.GetTreeXmlNode("SiteUrls", "WebSiteUploadFile"), false, false);
                Page.Response.End();
            }
            string paraMeter2 = IDoNetBbs.GetQueryString("paraMeter2");
            string paraMeter3 = IDoNetBbs.GetQueryString("paraMeter3");
            string paraMeter4 = IDoNetBbs.GetQueryString("paraMeter4");
            string paraMeter5 = IDoNetBbs.GetQueryString("paraMeter5");
            string paraMeter6 = IDoNetBbs.GetQueryString("paraMeter6");
            string paraMeter7 = IDoNetBbs.GetQueryString("paraMeter7");


            string Ext = "";
            Ext = Path.GetExtension(UploadFile.PostedFile.FileName).ToLower();
            if (!IDoNetBbs.GetComparison(Ext, false, MyConnection.GetWebSiteConfig("Config_WebSiteUpFileType"), false))
            {
                IDoNetBbs.WriteAlert(string.Format(MyConnection.GetResourcesXmlNode("Resource_WebSiteUpFileTypeErr"), MyConnection.GetWebSiteConfig("Config_WebSiteUpFileType")), false);
                IDoNetBbs.WriteJavaScript("parent.ScreenClear()", false);
                IDoNetBbs.WriteRedirect(MyConnection.GetTreeXmlNode("SiteUrls", "WebSiteUploadFile"), false, false);
                Page.Response.End();
            }
            string fileurl = MyConnection.GetWebSiteConfig("Config_WebSiteUploadFolder") + string.Format(MyConnection.GetWebSiteConfig("Config_WebSiteUpFileName"), currentUser.UserName, IDoNetBbs.GetRandom(6).ToString(), Convert.ToString(DateTime.Now.ToFileTime())) + Ext;
            string savefile = HttpContext.Current.Server.MapPath("~") + fileurl;
            UploadFile.SaveAs(savefile);

            IDoNetBbs.WriteJavaScript("parent.DosJavaScriptUpLoadFileOk('" + fileurl.Replace("\r\n", "") + "','" + paraMeter2 + "','" + paraMeter3 + "','" + paraMeter4 + "','" + paraMeter5 + "','" + paraMeter6 + "','" + paraMeter7 + "')", false);
            IDoNetBbs.WriteRedirect(MyConnection.GetTreeXmlNode("SiteUrls", "WebSiteUploadFile"), false, false);
            Page.Response.End();
        }
    }
}
