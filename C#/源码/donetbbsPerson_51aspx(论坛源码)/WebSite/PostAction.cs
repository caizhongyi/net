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
namespace WebSite
{
    public class PostAction : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        public override void DataBind()
        {
            base.DataBind();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            DataProviders.DataConnectionHepler MyConnection = DataProviders.DataConnectionHepler.Instance();
            string actions = string.Empty;
            if (!IDoNetBbs.GetDisableForm())
            {
                HttpContext.Current.Response.Write("alert('" + MyConnection.GetResourcesXmlNode("Resource_DisableForm") + "')");
                Page.Response.End();
            }
            else
            {
                if (IDoNetBbs.GetFormString("actions", false) == string.Empty)
                {
                    HttpContext.Current.Response.Write("alert('" + MyConnection.GetResourcesXmlNode("Resource_DisableForm") + "')");
                    Page.Response.End();
                }//
                else
                {
                    actions = IDoNetBbs.GetFormString("actions", false);
                    Control.PostHepler.PostUserHepler postUser = Control.PostHepler.PostUserHepler.Instance();
                    Control.PostHepler.PostTopicHepler postTopic = Control.PostHepler.PostTopicHepler.Instance();
                    switch (actions)
                    {
                        case "UserLogin":
                            postUser.CheckUserLogin();
                            break;
                        case "EditUserInfo":
                            postUser.EditUserInfo();
                            break;
                        case "ForgottenPassword":
                            postUser.ForgottenPassword();
                            break;
                        case "CreateUserInfo":
                            postUser.CreateUser();
                            break;
                        case "CreateTopic":
                            postTopic.CreateTopic();
                            break;
                        case "ReTopic":
                            postTopic.ReTopic();
                            break;
                        case "EditTopic":
                            postTopic.EditTopic();
                            break;
                        case "ManageTopic":
                            postTopic.ManageTopic();
                            break;
                        case "UserOnlineStatus":
                            postUser.UpdateUserStatus();
                            break;
                        case "DeleteTopicInfo":
                            postTopic.DeleteTopicInfo();
                            break;
                        case "RefreshWebSiteInfo":
                            DosOrg.Systems.SystemsInfoHelper.Instance().RefreshWebSiteInfo();
                            break;
                        case "ChangeUserStyle":
                            postUser.ChangeUserStyle();
                            break;
                        case "ChangeShowPage":
                            postUser.ChangeShowPage();
                            break;
                        case "ShowPostBuyMoney":
                            postTopic.ShowPostBuyMoney();
                            break;
                        default:
                            HttpContext.Current.Response.Write("alert('" + MyConnection.GetResourcesXmlNode("Resource_DisableForm") + "')");
                            break;
                    }
                    Page.Response.End();
                }//
            }
        }
    }
}
