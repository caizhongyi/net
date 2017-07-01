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
    public class ShowTopicInfo : System.Web.UI.Page
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
            DataProviders.ForumDataProvider MyForum = DataProviders.ForumDataProvider.Instance();
            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DataRow dt;

            int TopicID = IDoNetBbs.GetQueryInt("TopicID");
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardNavigate");
            Control.BoardNavigate IBoardNavigate = new Control.BoardNavigate();
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardAllListNavigate", IBoardNavigate.GetBoardAllNavigateTitle);

            if (TopicID == 0)
            {
                Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsTopicView"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("WebSiteShowTopicInfoNavigate"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                UserControlMaster.BindMsater();
                return;
            }
            Components.Components.Topic ITopic = new Components.Components.Topic();
            dt = MyForum.SetTopic(TopicID, true);
            if (dt == null)
            {
                Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsTopicView"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("WebSiteShowTopicInfoNavigate"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                UserControlMaster.BindMsater();
                return;
            }
            ITopic.SetDataProviders(dt);
            Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsTopicView"), ITopic.TopicTitle);

            IBoardNavigate.boardid = ITopic.TopicBoardID;

            Components.Components.Board IBoard = new Components.Components.Board();
            dt = MyForum.SetBoard(ITopic.TopicBoardID, true);
            if (dt != null)
            {
                IBoard.SetDataProviders(dt);
            }
            if ((IBoard.BoardID == 0) || (IBoard.BoardFalse == 1))
            {
                Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsTopicView"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("WebSiteShowTopicInfoNavigate"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardErr");
                UserControlMaster.BindMsater();
                return;
            }



            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", IBoardNavigate.GetBoardListNavigateTitle);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", ITopic.TopicTitle);

           // int Icount = MyForum.GetTopicInfoCount(ITopic.TopicID, false);

            Control.PageListNavigate IPageListNavigate = new Control.PageListNavigate();
            IPageListNavigate.pagenumber = int.Parse(MyConnection.GetWebSiteConfig("WebSite_ListNumber"));
            IPageListNavigate.countnumber = MyForum.SetTopicInfoCount(ITopic.TopicID, false);
            IPageListNavigate.page = IDoNetBbs.GetQueryInt("page");

            if (IPageListNavigate.countnumber == 0)
            {
                Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsTopicView"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", MyConnection.GetResourcesXmlNode("WebSiteShowTopicInfoNavigate"));
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                UserControlMaster.BindMsater();
                return;
            }//

            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardAbout");
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardListPostMenu");
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardAbout", IBoard.BoardAbout);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", IBoardNavigate.GetBoardListNavigateTitle + MyConnection.GetResourcesXmlNode("WebSiteShowTopicInfoNavigate"));


            IPageListNavigate.SetNavigate();
            IPageListNavigate.navigateurl = "TopicID=" + TopicID.ToString() + "";




            Components.Components.TopicInfo ITopicInfo = new Components.Components.TopicInfo();
            ITopicInfo.Arraylist = MyForum.SetTopicInfoList(ITopic.TopicID, (IPageListNavigate.page - 1) * IPageListNavigate.pagenumber, IPageListNavigate.pagenumber, true);
            //UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicInfoMenuTitle");
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicInfoMenuTitle");
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteRePostDisplay", null);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicID", ITopic.TopicID.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "PostBoardID", ITopic.TopicBoardID.ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicViewNumber", string.Format(MyConnection.GetTempXmlNode("Resource_WebSiteTopicViewNumber"), ITopic.TopicViewNumber.ToString()));
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicTitle", ITopic.TopicTitle);

            bool boolMaster = false;
            bool boolView = false;
            boolMaster = ((currentUser.IsSystemAdministrator) || (currentUser.IsBoardAdministrator) || (currentUser.IsTopicAdministrator) || (IDoNetBbs.GetComparison(IBoard.BoardMaster, true, currentUser.UserID.ToString(), true)));
            if (boolMaster)
            {
                boolView = true;
            }
            else
            {
                boolView = IDoNetBbs.GetComparison(IBoard.BoardViewRole, true, currentUser.UserRole, true);
            }
            if (!boolView)
            {
                Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsTopicView"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicInfoListErr");
                UserControlMaster.BindMsater();
                return;
            }

            int myReply = 0;
            int CurrentNumber = 0;
            int ArTicleFloor = 0;
            foreach (Components.Components.TopicInfo rs in ITopicInfo.Arraylist)
            {
                bool boolTopicMaster = boolMaster;
                if (!boolTopicMaster)
                {
                    boolTopicMaster = ((rs.TopicInfoUserID == currentUser.UserID) && (currentUser.UserID != 0));
                }

                CurrentNumber++;
                ArTicleFloor = CurrentNumber;
                if (IPageListNavigate.page > 1)
                {
                    ArTicleFloor = (IPageListNavigate.page - 1) * IPageListNavigate.pagenumber + CurrentNumber;
                }

                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicInfoBody");
                if (rs.TopicInfoUserID == 0)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteShowTopicInfoUserInfo", MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicInfoGuestInfo"));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicPostUserInfo", MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicInfoGuestMenu"));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserNickName", rs.TopicInfoUserNickName);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserSign", rs.TopicInfoUserNickName);
                }
                else
                {
                    Components.Components.User IUser = new Components.Components.User();
                    dt = MyUser.SetUserInfo(rs.TopicInfoUserID, true);

                    //userInfo.SetUserInfo(rs.TopicInfoUserID, true);
                    if (dt == null)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteShowTopicInfoUserInfo", MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicInfoGuestInfo"));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicPostUserInfo", MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicInfoGuestMenu"));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserNickName", rs.TopicInfoUserNickName);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserSign", rs.TopicInfoUserNickName);
                    }
                    else
                    {
                        IUser.SetDataProviders(dt);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteShowTopicInfoUserInfo", MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicInfoUserInfo"));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicPostUserInfo", MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicInfoUserInfoMenu"));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserNickName", IUser.UserNickName);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserName", IUser.UserName);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserFace", IUser.UserFace);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserPrestige", IUser.UserPrestige.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserOnlineTime", (IUser.UserOnlineTime / 60).ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserPoint", IUser.UserPoint.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserExp", IUser.UserExp.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserCP", IUser.UserCP.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserMoney", IUser.UserMoney.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserTopicNumber", IUser.UserTopicNumber.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserReTopicNumber", IUser.UserReTopicNumber.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserSpace", IUser.UserWebAddress);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserBlogs", IUser.UserWebLog);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserPhotos", IUser.UserWebGallery);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserID", IUser.UserID.ToString());

                        Control.UserInfoControl IUserInfoControl = new Control.UserInfoControl();
                        IUserInfoControl.userOnlineStatus = IUser.UserOnLineStatic;
                        IUserInfoControl.userLastActTime = IUser.UserLastActTime;
                        IUserInfoControl.userLevelID = IUser.UserLevelID;
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserOnLineStatic", IUserInfoControl.UserOnlineStatus);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserLevelPic", IUserInfoControl.UserLevelPic);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserClass", IUserInfoControl.UserClass);

                        if (rs.TopicInfoSignFalse == 1)
                        {
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserSign", IUser.UserSign);
                        }
                        else
                        {
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserSign", rs.TopicInfoUserNickName);
                        }

                    }
                }
                if (currentUser.ShowPageStatus != 0)
                {
                    UserControlMaster.WebSiteMasterBody += "<script>WebSiteJavaScriptSetPage('WebSiteShowPage','WebSiteUserPage','1');</script>";
                }
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoFace", rs.TopicInfoFace);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicTitle", rs.TopicInfoTitle);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoPostTime", rs.TopicInfoPostTime.ToString());

                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicBottom");
                if (boolMaster)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoEditHistory", rs.TopicInfoEditHistory);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoHtml", rs.TopicInfoHtml);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteShowTopicBottomManagerTitle", MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicBottomManagerTitleRePost") + MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicBottomManagerTitleEditPost") + MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicBottomManagerTitleDelPost"));
                    
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoUserIP", rs.TopicInfoUserIP);
                    if (rs.TopicInfoParentID == 0)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "deleteTopicInfoID", rs.TopicInfoRootID.ToString());
                    }
                    else
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "deleteTopicInfoID", rs.TopicInfoID.ToString());
                    }
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "deleteFalseTopic", rs.TopicInfoParentID.ToString());
                }//
                else
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoEditHistory", null);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoUserIP", rs.TopicInfoPostTime.ToString());
                    if (boolTopicMaster)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoHtml", rs.TopicInfoHtml);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteShowTopicBottomManagerTitle", MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicBottomManagerTitleRePost") + MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicBottomManagerTitleEditPost") + MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicBottomManagerTitleDelPost"));
                        if (rs.TopicInfoParentID == 0)
                        {
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "deleteTopicInfoID", rs.TopicInfoRootID.ToString());
                        }
                        else
                        {
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "deleteTopicInfoID", rs.TopicInfoID.ToString());
                        }
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "deleteFalseTopic", rs.TopicInfoParentID.ToString());
                    }
                    else
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteShowTopicBottomManagerTitle", MyConnection.GetTempXmlNode("Resource_WebSiteShowTopicBottomManagerTitleRePost"));
                        if (rs.TopicInfoFalse == 0)
                        {
                            bool boolShow = false;
                            if (rs.TopicInfoViewRole != string.Empty)
                            {
                                if (!IDoNetBbs.GetComparison(rs.TopicInfoViewRole, true, currentUser.UserRole, true))
                                {
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoHtml", MyConnection.GetResourcesXmlNode("Resource_WebSiteShowInfoRole"));
                                    boolShow = true;
                                }
                            }
                            if ((rs.TopicInfoViewUserGroup != string.Empty) && (!boolShow))
                            {
                                if (!IDoNetBbs.GetComparison(rs.TopicInfoViewUserGroup, true, currentUser.UserGroup, true))
                                {
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoHtml", MyConnection.GetResourcesXmlNode("Resource_WebSiteShowInfoGroup"));
                                    boolShow = true;
                                }
                            }
                            if ((rs.TopicInfoReply == 1) && (!boolShow))
                            {
                                if (currentUser.UserID == 0)
                                {
                                    myReply = 1;
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoHtml", MyConnection.GetResourcesXmlNode("Resource_WebSiteShowInfoReply"));
                                    boolShow = true;
                                }
                                else
                                {
                                    if (myReply == 0)
                                    {
                                        if (MyForum.SetMyReply(currentUser.UserID, rs.TopicInfoRootID, true))
                                        {
                                            myReply = 2;
                                        }
                                        else
                                        {
                                            myReply = 1;
                                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoHtml", MyConnection.GetResourcesXmlNode("Resource_WebSiteShowInfoReply"));
                                            boolShow = true;
                                        }
                                    }
                                    else if (myReply == 1)
                                    {
                                        myReply = 1;
                                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoHtml", MyConnection.GetResourcesXmlNode("Resource_WebSiteShowInfoReply"));
                                        boolShow = true;
                                    }
                                }
                            }
                            if ((rs.TopicInfoBuyMoney != 0) && (!boolShow))
                            {
                                if (!currentUser.BoolUserPlayMoney(rs.TopicInfoID))
                                {
                                    boolShow = true;
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoHtml", MyConnection.GetTempXmlNode("Resource_WebSitePostBuyMoney"));
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "PostBuyMoney", rs.TopicInfoBuyMoney.ToString());
                                }
                            }
                            if (!boolShow)
                            {
                                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoHtml", rs.TopicInfoHtml);
                            }
                        }
                        else
                        {
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoHtml", MyConnection.GetResourcesXmlNode("Resource_WebSiteShowInfoFalse"));
                        }
                    }//
                }
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoID", rs.TopicInfoID.ToString());
                if (IDoNetBbs.GetMod(CurrentNumber, 2) == 0)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardLaberNum", "2");
                }
                else
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardLaberNum", "1");
                }//

                //

                if (ArTicleFloor == 1)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "ArTicleFloor", MyConnection.GetResourcesXmlNode("WebSiteShowTopicInfoArTicleFloor"));
                }
                else
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "ArTicleFloor", string.Format(MyConnection.GetResourcesXmlNode("WebSiteShowTopicInfoArTicleFloorNumber"), ArTicleFloor.ToString()));
                }

            }
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicID", ITopic.TopicID.ToString());
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardTopicPageListInfo");
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardOptionApplication", IBoardNavigate.GetBoardOptionNavigateTitle);

            //string PageListNavigateTitle = IPageListNavigate.GetPageListNavigateTitle;
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", IPageListNavigate.GetPageListNavigateTitle);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "PageInputName", IDoNetBbs.GetRandom(3).ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListBottomInfo", IPageListNavigate.GetPageListNavigateTitle);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "PageInputName", IDoNetBbs.GetRandom(4).ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageSearchUrl", IPageListNavigate.navigateurl);

            if (ITopicInfo.Arraylist.Count != 0)
            {
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteEditBody");
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePostInfoTitle", "Re:" + ITopic.TopicTitle);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicExpression", MyConnection.GetTempXmlNode("Resource_TopicExpression"));
                UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("TopicInfoID", ITopic.TopicID.ToString(), false);
                if (currentUser.UserID != 0)
                {
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserName", currentUser.UserName,false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserPassWord", IDoNetBbs.GetEncryptChar(currentUser.UserPassWord), false);
                }
                else
                {
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptInput("UserName", currentUser.UserGuestName,false);
                    UserControlMaster.WebSiteMasterBody += IDoNetBbs.GetJavaScriptCheckBox("GuestPost", true, false);
                }
            }
            UserControlMaster.BindMsater();
            MyForum.UpdateTopicView(ITopic.TopicID);
        }
    }
}
