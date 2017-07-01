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
    public class EditUserInfo : System.Web.UI.Page
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
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            Components.Components.User IUser = new Components.Components.User();

            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardNavigate");
            Control.BoardNavigate IBoardNavigate = new Control.BoardNavigate();
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardAllListNavigate", IBoardNavigate.GetBoardAllNavigateTitle);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_WebSiteEditUserInfoTitle"));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);

            if (currentUser.UserID == 0)
            {
                Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "UserInfoEdit"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteUserLoginErr");
                UserControlMaster.BindMsater();
                return;
            }
            else
            {
                DataRow dt;
                dt = MyUser.SetUserInfo(currentUser.UserID, false);
                //userInfo.SetUserInfo(users.UserID, false);
                if (dt == null)
                {
                    Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "UserInfoEdit"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                    UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteInfoNotExistErr");
                    UserControlMaster.BindMsater();
                    return;
                }
                else
                {
                    IUser.SetDataProviders(dt);
                    if (IUser.UserFalse == 1)
                    {
                        if ((!currentUser.IsMembershipAdministrator) && (!currentUser.IsSystemAdministrator))
                        {
                            Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "UserInfoEdit"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteUserInfoFalseErr");
                            UserControlMaster.BindMsater();
                            return;
                        }
                    }
                    UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteEditUserBodyBody");
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserComeFrom", IUser.UserComeFrom);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserFace", IUser.UserFace);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserSelectImagesBody", MyConnection.GetTempXmlNode("Resource_WebSiteUserFaceBody"));
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserName", IUser.UserName, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserNickName", IUser.UserNickName, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserTrueName", IUser.UserTrueName, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserEmail", IUser.UserEmail, false);
                    if (IUser.UserRecommendUserID != 0)
                    {
                        //DataProviders.UserInfoDataProviders userRecommendUser = new DataProviders.UserInfoDataProviders();
                        //userRecommendUser.SetUserInfo(userInfo.UserRecommendUserID, true);
                        //if (userRecommendUser.UserID != 0)
                        //{
                        //    UserControlMaster.WebSiteMasterBody += doNetBbsClass.GetJavaScriptInput("UserRecommendName", userRecommendUser.UserName);
                        //}
                        dt = MyUser.SetUserInfo(IUser.UserRecommendUserID, true);
                        //userRecommendUser.SetUserInfo(userInfo.UserRecommendUserID, true);
                        if (dt != null)
                        {
                            Components.Components.User RUser = new Components.Components.User();
                            RUser.SetDataProviders(dt);
                            UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserRecommendName", RUser.UserName, false);
                        }
                    }
                    Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "UserInfoEdit"), IUser.UserNickName);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserSign", IUser.UserSign, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserAbout", IUser.UserAbout, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckRadio("UserSex", IUser.UserSex, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptSelect("UserOnLineStatic", IUser.UserOnLineStatic, false);

                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserIdCard", IUser.UserIdCard, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserBirthday", IUser.UserBirthday.ToString("yyy-MM-dd"), false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckRadio("UserMaritalStatus", IUser.UserMaritalStatus.ToString(), false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserSchool", IUser.UserSchool, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserWorkUnit", IUser.UserWorkUnit, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserMobile", IUser.UserMobile, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserContactTel", IUser.UserContactTel, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserOICQ", IUser.UserOICQ, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserCode", IUser.UserCode, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserContactAddress", IUser.UserContactAddress, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserWebAddress", IUser.UserWebAddress, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserWebLog", IUser.UserWebLog, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserWebGallery", IUser.UserWebGallery, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptSelect("UserPrivacy", IUser.UserPrivacy.ToString(), false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptSelect("UserReceiveType", IUser.UserReceiveType.ToString(), false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserFace", IUser.UserFace, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckBoxGroup("UserInterests", IUser.UserInterests, false);
                }
            }
            UserControlMaster.BindMsater();
        }
    }
}
