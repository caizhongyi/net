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
namespace WebSite.Forums
{
    public class Post : System.Web.UI.Page
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
        DataProviders.DataConnectionHepler MyConnection = DataProviders.DataConnectionHepler.Instance();
        DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
        DataProviders.ForumDataProvider MyForum = DataProviders.ForumDataProvider.Instance();
        DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
        DosOrg.User.User currentUser = new DosOrg.User.User();
        Components.Components.Topic ITopic = new Components.Components.Topic();
        Components.Components.TopicInfo ITopicInfo = new Components.Components.TopicInfo();
        Components.Components.Board IBoard = new Components.Components.Board();
        DataRow dt;
        public override void DataBind()
        {
            base.DataBind();
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardNavigate");


            Control.BoardNavigate IBoardNavigate = new Control.BoardNavigate();
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardAllListNavigate", IBoardNavigate.GetBoardAllNavigateTitle);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
            string actions = IDoNetBbs.GetQueryString("actions");
            int BoardID = IDoNetBbs.GetQueryInt("BoardID");
            int TopicInfoID = IDoNetBbs.GetQueryInt("TopicInfoID");
            int TopicID = IDoNetBbs.GetQueryInt("TopicID");
            bool boolMaster = false;


            if (actions != string.Empty)
            {
                if (actions == "re")
                {
                    actions = "ReTopic";
                    if (TopicID == 0)
                    {
                        Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsRePost"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_RePostInfoTitle"));
                        UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                        UserControlMaster.BindMsater();
                        return;
                    }
                    else
                    {
                        dt = MyForum.SetTopic(TopicID, true);
                        if (dt==null)
                        {
                            Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsRePost"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_RePostInfoTitle"));
                            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                            UserControlMaster.BindMsater();
                            return;
                        }
                        else
                        {
                            ITopic.SetDataProviders(dt);
                            Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsRePost"), ITopic.TopicTitle);
                            BoardID = ITopic.TopicBoardID;
                            if (BoardID != 0)
                            {
                                IBoardNavigate.boardid = BoardID;
                                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", IBoardNavigate.GetBoardListNavigateTitle + MyConnection.GetResourcesXmlNode("Resource_RePostInfoTitle"));
                            }
                            else
                            {
                                Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsRePost"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_RePostInfoTitle"));
                                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                                UserControlMaster.BindMsater();
                                return;
                            }
                        }
                    }
                }
                else if (actions == "edit")
                {
                    actions = "EditTopic";
                    if (TopicInfoID == 0)
                    {
                        Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsRePostEdit"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_EditPostInfoTitle"));
                        UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                        UserControlMaster.BindMsater();
                        return;
                    }
                    else
                    {
                        dt = MyForum.SetTopicInfo(TopicInfoID, true);
                        //topicInfo.SetTopicInfo(TopicInfoID, true);
                        if (dt==null)
                        {
                            Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsRePostEdit"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_EditPostInfoTitle"));
                            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                            UserControlMaster.BindMsater();
                            return;
                        }
                        else
                        {
                            ITopicInfo.SetDataProviders(dt);

                            dt = MyForum.SetTopic(ITopicInfo.TopicInfoRootID, true);
                            //ITopic.SetTopic(topicInfo.TopicInfoRootID, true);
                            if (dt==null)
                            {
                                Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsRePostEdit"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_EditPostInfoTitle"));
                                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                                UserControlMaster.BindMsater();
                                return;
                            }
                            else
                            {
                                ITopic.SetDataProviders(dt);

                                Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsRePostEdit"), ITopicInfo.TopicInfoTitle);
                                BoardID = ITopic.TopicBoardID;
                                if (BoardID != 0)
                                {
                                    if (currentUser.UserID != 0)
                                    {
                                        if (currentUser.IsSystemAdministrator || currentUser.IsBoardAdministrator || currentUser.IsTopicAdministrator || (ITopicInfo.TopicInfoUserID == currentUser.UserID))
                                        {
                                            boolMaster = true;
                                        }
                                        if (!boolMaster)
                                        {
                                            dt = MyForum.SetBoard(BoardID, true);
                                            IBoard.SetDataProviders(dt);
                                            if (dt == null)
                                            {
                                                Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsRePostEdit"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                                                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_EditPostInfoTitle"));
                                                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                                                UserControlMaster.BindMsater();
                                                return;
                                            }
                                            //board.SetBoard(BoardID, true);
                                            if (IBoard.BoardMaster != string.Empty)
                                            {
                                                if (IDoNetBbs.GetComparison(IBoard.BoardMaster, true, currentUser.UserID.ToString(), true))
                                                {
                                                    boolMaster = true;
                                                }
                                            }
                                        }
                                    }
                                    if (!boolMaster)
                                    {
                                        Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsRePostEdit"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_EditPostInfoTitle"));
                                        UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                                        UserControlMaster.BindMsater();
                                        return;
                                    }

                                    IBoardNavigate.boardid = BoardID;
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", IBoardNavigate.GetBoardListNavigateTitle + MyConnection.GetResourcesXmlNode("Resource_EditPostInfoTitle"));
                                }
                                else
                                {
                                    Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsRePostEdit"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_EditPostInfoTitle"));
                                    UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                                    UserControlMaster.BindMsater();
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    actions = "CreateTopic";
                }
            }
            else
            {
                actions = "CreateTopic";
                Components.SiteWebSetting.WebSiteTitle = MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsPost");
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("Resource_PostInfoTitle"));
            }
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_PostBody");
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicExpression", MyConnection.GetTempXmlNode("Resource_TopicExpression"));

            if (actions == "ReTopic")
            {
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "PostInfoTitle", "Re:" + ITopic.TopicTitle);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("TopicParentID", false, false);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("TopicSubjectID", false, false);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("TopicInfoTitle", "Re:" + ITopic.TopicTitle, false);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicInfoStatic", null);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Resource_WebSiteReTopicEmail", null);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("TopicImages", false, false);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("CheckBoxUploadFile", false, false);
                if (currentUser.UserID == 0)
                {
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserName", currentUser.UserGuestName, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckBox("GuestPost", true, false);
                }
                else
                {
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserName", currentUser.UserName, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserPassWord", IDoNetBbs.GetEncryptChar(currentUser.UserPassWord), false);
                }

                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("TopicInfoID", ITopic.TopicID.ToString(),false);
            }
            else if (actions == "EditTopic")
            {
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "PostInfoTitle", "Edit:" + ITopicInfo.TopicInfoTitle);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserName", ITopicInfo.TopicInfoUserNickName, false);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("TopicInfoTitle", ITopicInfo.TopicInfoTitle, false);
                if (ITopic.TopicFalse == 1)
                {
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckBox("TopicFalse", true, false);
                }
                if (ITopicInfo.TopicInfoParentID == 0)
                {
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("TopicImages", ITopic.TopicImages, false);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Resource_WebSiteReTopicEmail", MyConnection.GetTempXmlNode("Resource_WebSiteReTopicEmail"));
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckRadio("RePostEmail", ITopic.TopicRePostEmail.ToString(), false);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicInfoStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoStatic"));
                    if (ITopic.TopicTotalAtTop == 2)
                    {
                        UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckBox("AllTotalAtTop", true, false);
                    }
                    if (ITopic.TopicTotalAtTop == 1)
                    {
                        UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckBox("TotalAtTop", true, false);
                    }
                    if (ITopic.TopicRecommend == 1)
                    {
                        UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckBox("TopicRecommend", true, false);
                    }
                    if (ITopic.TopicBest == 1)
                    {
                        UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckBox("TopicBest", true, false);
                    }
                    IBoardNavigate.boardid = BoardID;
                    UserControlMaster.WebSiteMasterBody += IBoardNavigate.GetBoardTreeNavigateTitle;
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("TopicParentID", false, false);
                }
                else
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Resource_WebSiteReTopicEmail", null);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicInfoStatic", null);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("TopicImages", false, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("CheckBoxUploadFile", false, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("TopicParentID", false, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("TopicSubjectID", false, false);
                }
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("UserName", false, false);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("UserPassWord", false, false);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptDisabled("GuestPost", false, false);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckRadio("TopicInfoFace", ITopicInfo.TopicInfoFace, false);
                //UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckRadio("RePostEmail", ITopicInfo.TopicInfoRePostEmail.ToString(), false);

                if (ITopicInfo.TopicInfoReply == 1)
                {
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckBox("TopicInfoReply", true, false);
                }
                if (ITopicInfo.TopicInfoBuyMoney != 0)
                {
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("PostBuyMoney", ITopicInfo.TopicInfoBuyMoney.ToString(), false);
                }

                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("TopicInfoViewRole", ITopicInfo.TopicInfoViewRole, false);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("TopicInfoRePostRole", ITopicInfo.TopicInfoRePostRole, false);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("TopicInfoViewUserGroup", ITopicInfo.TopicInfoViewUserGroup, false);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("TopicInfoRePostUserGroup", ITopicInfo.TopicInfoRePostUserGroup, false);

                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("TopicInfoID", ITopicInfo.TopicInfoID.ToString(), false);
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("PostBodyHtml", ITopicInfo.TopicInfoHtml.Replace("\r\n", ""), false);
            }
            else
            {
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "PostInfoTitle", MyConnection.GetResourcesXmlNode("Resource_PostInfoTitle"));
                //UserControlMaster.WebSiteMasterBody += doNetBbsClass.WriteDisabled("TopicParentID", false);
                //UserControlMaster.WebSiteMasterBody += doNetBbsClass.WriteDisabled("TopicSubjectID", false);
                //UserControlMaster.WebSiteMasterBody += doNetBbsClass.WriteInput("TopicInfoTitle", "Re:" + topic.TopicTitle);
                if (currentUser.UserID == 0)
                {
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserName", currentUser.UserGuestName, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckBox("GuestPost", true, false);
                }
                else
                {
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserName", currentUser.UserName, false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserPassWord", IDoNetBbs.GetEncryptChar(currentUser.UserPassWord), false);
                }
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicInfoStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoStatic"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Resource_WebSiteReTopicEmail", MyConnection.GetTempXmlNode("Resource_WebSiteReTopicEmail"));
                IBoardNavigate.boardid = BoardID;
                UserControlMaster.WebSiteMasterBody += IBoardNavigate.GetBoardTreeNavigateTitle;
            }
            UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("actions", actions, false);



            UserControlMaster.BindMsater();
        }
    }
}
