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
    public class UserInfo : System.Web.UI.Page
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
            Components.Components.User IUser=new Components.Components.User();
            DataRow dt;
            //DataProviders.UserInfoDataProviders userInfo = new DataProviders.UserInfoDataProviders();

            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardNavigate");
            Control.BoardNavigate IBoardNavigate = new Control.BoardNavigate();
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardAllListNavigate", IBoardNavigate.GetBoardAllNavigateTitle);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
            int UserID = IDoNetBbs.GetQueryInt("UserID");
            if (UserID == 0)
            {
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteInfoNotExistErr");
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", string.Format(MyConnection.GetResourcesXmlNode("Resource_WebSiteDisplayUserInfoTitle"), string.Empty));
                UserControlMaster.BindMsater();
                return;
            }
            else
            {
                dt = MyUser.SetUserInfo(UserID, true);
                //userInfo.SetUserInfo(UserID, true);
                if (dt == null)
                {
                    Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "UserInfoView"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                    UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteInfoNotExistErr");
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", string.Format(MyConnection.GetResourcesXmlNode("Resource_WebSiteDisplayUserInfoTitle"), string.Empty));
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
                            Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "UserInfoView"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteInfoNotExistErr");
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", string.Format(MyConnection.GetResourcesXmlNode("Resource_WebSiteDisplayUserInfoTitle"), string.Empty));
                            UserControlMaster.BindMsater();
                            return;
                        }
                    }
                }
            }
            Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "UserInfoView"), IUser.UserNickName);
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteShowUserInfo");

            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", string.Format(MyConnection.GetResourcesXmlNode("Resource_WebSiteDisplayUserInfoTitle"), IUser.UserNickName));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserNickName", IUser.UserNickName);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserName", IUser.UserName);

            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserPoint", IUser.UserPoint.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserPrestige", IUser.UserPrestige.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserComeFrom", IUser.UserComeFrom);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserSign", IUser.UserSign);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserLoginNumber", IUser.UserLoginNumber.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserExp", IUser.UserExp.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserMoney", IUser.UserMoney.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserTicket", IUser.UserTicket.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserPostNumber", IUser.UserPostNumber.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserTopicNumber", IUser.UserTopicNumber.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserReTopicNumber", IUser.UserReTopicNumber.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserWebAddress", IUser.UserWebAddress);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserWebLog", IUser.UserWebLog);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserWebGallery", IUser.UserWebGallery);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserInterests", IUser.UserInterests);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserCP", IUser.UserCP.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserFace", IUser.UserFace);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserOnlineTime", (IUser.UserOnlineTime / 60).ToString());

            if (IUser.UserRecommendUserID != 0)
            {
                //DataProviders.UserInfoDataProviders userRecommendUser = new DataProviders.UserInfoDataProviders();
                dt = MyUser.SetUserInfo(IUser.UserRecommendUserID, true);
                //userRecommendUser.SetUserInfo(userInfo.UserRecommendUserID, true);
                if (dt != null)
                {
                    Components.Components.User RUser = new Components.Components.User();
                    RUser.SetDataProviders(dt);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserRecommendName", RUser.UserNickName);
                }
            }
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserRecommendName", null);

            Control.UserInfoControl IUserInfoControl = new Control.UserInfoControl();
            IUserInfoControl.userOnlineStatus = IUser.UserOnLineStatic;
            IUserInfoControl.userLastActTime = IUser.UserLastActTime;
            IUserInfoControl.userLevelID = IUser.UserLevelID;
            IUserInfoControl.userRoleID = IUser.UserRole;
            IUserInfoControl.userGroupID = IUser.UserGroup;
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserOnLineStatic", IUserInfoControl.UserOnlineStatus);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserClassTitle", IUserInfoControl.UserClass);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserGroupID", IUserInfoControl.UserGroupTitle);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserRoleTitle", IUserInfoControl.UserRoleTitle);

            bool boolMaster = false;
            bool boolFriend = false;

            if ((currentUser.IsSystemAdministrator) || (currentUser.IsMembershipAdministrator) || (currentUser.UserID == IUser.UserID))
            {
                boolMaster = true;
                boolFriend = true;
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserIdCard", IUser.UserIdCard);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserTrueMoney", IUser.UserTrueMoney.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserRegTime", IUser.UserRegTime.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserLastActTime", IUser.UserLastActTime.ToString());
            }
            else
            {
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserIdCard", MyConnection.GetResourcesXmlNode("Resource_ShowUserPrivacy"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserTrueMoney", MyConnection.GetResourcesXmlNode("Resource_ShowUserPrivacy"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserRegTime", MyConnection.GetResourcesXmlNode("Resource_ShowUserPrivacy"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserLastActTime", MyConnection.GetResourcesXmlNode("Resource_ShowUserPrivacy"));
            }
            if ((IUser.UserPrivacy == 3) && (!boolFriend) && (currentUser.UserID != 0))
            {
                //DataProviders.FriendDataProviders friend = new DataProviders.FriendDataProviders();
                //friend.SetFriend(userInfo.UserID, users.UserID, true);
                //if (friend.FriendID != 0)
                //{
                //    boolMaster = true;
                //}
            }//

            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserEmail", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, IUser.UserEmail));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserTrueName", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, IUser.UserTrueName));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserSchool", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, IUser.UserSchool));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserMobile", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, IUser.UserMobile));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserBirthday", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, (System.Convert.ToInt16(DateTime.Now.Year - IUser.UserBirthday.Year) + 1).ToString()));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserMaritalStatus", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, MyConnection.GetResourcesXmlNode("Resource_UserMaritalStatus" + IUser.UserMaritalStatus)));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserAbout", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, IUser.UserAbout));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserOICQ", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, IUser.UserOICQ));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserCode", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, IUser.UserCode));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserWorkUnit", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, IUser.UserWorkUnit));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserContactAddress", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, IUser.UserContactAddress));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserContactTel", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, IUser.UserContactTel));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserSex", GetPrivacyInfo(currentUser.UserID, IUser.UserPrivacy, boolFriend, boolMaster, MyConnection.GetResourcesXmlNode("Resource_UserSex" + IUser.UserSex)));
            UserControlMaster.BindMsater();
        }
        private string GetPrivacyInfo(int userID, int privacyID, bool boolFriend, bool boolmaster, string chars)
        {
            if (!boolmaster)
            {
                DataProviders.DataConnectionHepler MyConnection = DataProviders.DataConnectionHepler.Instance();
                if (privacyID == 0)
                {
                    return MyConnection.GetResourcesXmlNode("Resource_ShowUserPrivacy");
                }
                else if (privacyID == 1)
                {
                    return chars;
                }
                else if (privacyID == 2)
                {
                    if (userID == 0)
                    {
                        return MyConnection.GetResourcesXmlNode("Resource_ShowUserPrivacyLogin");
                    }
                }
                else
                {
                    if (!boolFriend)
                    {
                        return MyConnection.GetResourcesXmlNode("Resource_ShowUserPrivacyFriend");
                    }
                }
            }
            return chars;
        }
    }
}
