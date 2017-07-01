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
    public class Default : System.Web.UI.Page
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
        DataRow dt;
        public override void DataBind()
        {
            base.DataBind();
            boardid = IDoNetBbs.GetQueryInt("boardid");
            type = IDoNetBbs.GetQueryInt("type");
            if ((boardid != 0) || (type != 0))
            {
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardNavigate");
                Control.BoardNavigate IBoardNavigate = new Control.BoardNavigate();
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardAllListNavigate", IBoardNavigate.GetBoardAllNavigateTitle);
                if (boardid != 0)
                {
                    IBoardNavigate.boardid = boardid;
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", IBoardNavigate.GetBoardListNavigateTitle);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", IBoardNavigate.GetDeFaultWebSiteNavigateTitle);

                    Components.Components.Board IBoard = new Components.Components.Board();
                    dt = MyForum.SetBoard(boardid, true);
                    if (dt == null)
                    {
                        Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsDeFaultView"), MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteErr"));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
                        UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardErr");
                        UserControlMaster.BindMsater();
                        return;
                    }
                    IBoard.SetDataProviders(dt);

                    Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsDeFaultView"), IBoard.BoardName);
                    UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardAbout");
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardAbout", IBoard.BoardAbout);
                    SetBoardGroup();
                    if (IBoard.BoardTypeID == 1)
                    {
                        UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardListPostMenu");
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteRePostDisplay", "none");
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicID", null);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "PostBoardID", boardid.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicViewNumber", null);
                        UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardTopicNavigate");
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", boardid.ToString());
                        if (IBoard.BoardMaster != string.Empty)
                        {
                            for (int i = 0; i < IDoNetBbs.GetUniqueArraylist(IBoard.BoardMaster, false, null).Split(',').Length; i++)
                            {
                                Components.Components.User IUser = new Components.Components.User();
                                dt = MyUser.SetUserInfo(int.Parse(IDoNetBbs.GetUniqueArraylist(IBoard.BoardMaster, false, null).Split(',')[i]), true);
                                if (dt == null)
                                {
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "BoardMasterList", null);
                                }
                                else
                                { 
                                    IUser.SetDataProviders(dt);
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "BoardMasterList", MyConnection.GetTempXmlNode("Resource_WebSiteUserInfo"));
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "AddUserID", IUser.UserID.ToString());
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "AddUserNickName", IUser.UserNickName);

                                }
                            }//
                        }//
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardMasterList", null);



                        if (IBoard.BoardSubject != string.Empty)
                        {
                            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardSubjectInfoe");

                            for (int i = 0; i < IDoNetBbs.GetUniqueArraylist(IBoard.BoardSubject, false, null).Split(',').Length; i++)
                            {
                                if (IDoNetBbs.GetUniqueArraylist(IBoard.BoardSubject, false, null).Split(',')[i].ToString() != "")
                                {
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardBoardSubjectInfo", MyConnection.GetTempXmlNode("Resource_WebSiteBoardSubjectInfoeBar"));
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardSubjectName", IDoNetBbs.GetUniqueArraylist(IBoard.BoardSubject, false, null).Split(',')[i].ToString());
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardSubjectNumber", (i + 1).ToString());
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardSubjectBoardID", IBoard.BoardID.ToString());
                                }

                            }//
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardBoardSubjectInfo", null);
                        }
                        SetTopicList();
                        UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardTopicListExampl");
                    }
                    else
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
                        UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicDiagramExample");
                    }
                }
                else
                {
                    Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsDeFaultView"), IBoardNavigate.GetDeFaultWebSiteNavigateTitle);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardListNavigate", IBoardNavigate.GetDeFaultWebSiteNavigateTitle);
                    UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardListPostMenu");
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteRePostDisplay", "none");
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicID", null);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "PostBoardID", boardid.ToString());
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicViewNumber", null);
                    SetTopicList();
                    UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardTopicListExampl");
                }
                ///
            }
            else
            {
                Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "FroumsDeFault"), MyConnection.GetTreeXmlNode("WebSiteTitle", "DeFault"));
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBbsUserLoginHead");
                if (currentUser.UserID == 0)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBbsUserLoginHeadUserLoginInfo", MyConnection.GetTempXmlNode("Resource_WebSiteBbsUserLoginHeadUserLoginInfoNot"));
                }
                else
                {
                    Components.Components.User IUser=new Components.Components.User();
                    dt = MyUser.SetUserInfo(currentUser.UserID, true);

                    if (dt == null)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBbsUserLoginHeadUserLoginInfo", MyConnection.GetTempXmlNode("Resource_WebSiteBbsUserLoginHeadUserLoginInfoNot"));                        
                    }
                    else
                    {
                        IUser.SetDataProviders(dt);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBbsUserLoginHeadUserLoginInfo", MyConnection.GetTempXmlNode("Resource_WebSiteBbsUserLoginHeadUserLoginInfo"));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserName", IUser.UserName.ToString());
                        Control.UserInfoControl IUserInfoControl = new Control.UserInfoControl();
                        IUserInfoControl.userOnlineStatus = IUser.UserOnLineStatic;
                        IUserInfoControl.userLastActTime = IUser.UserLastActTime;
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserOnLineStatic", IUserInfoControl.UserOnlineStatus);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserPrestige", IUser.UserPrestige.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserLoginNumber", IUser.UserLoginNumber.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserOnlineTime", (IUser.UserOnlineTime / 60).ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserTrueMoney", IUser.UserTrueMoney.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserExp", IUser.UserExp.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserPoint", IUser.UserPoint.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserMoney", IUser.UserMoney.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserTicket", IUser.UserTicket.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserPostNumber", IUser.UserPostNumber.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserTopicNumber", IUser.UserTopicNumber.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserReTopicNumber", IUser.UserReTopicNumber.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserDelTopicNumber", IUser.UserDelTopicNumber.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserFace", IUser.UserFace);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserID", IUser.UserID.ToString());
                    }
                }
                SetBoardGroup();
                //UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBbsFriendLink");
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBbsCountInfo");

                //站点信息
                DataProviders.WebSiteDataProvider MyWebSite = DataProviders.WebSiteDataProvider.Instance();
                dt = MyWebSite.SetWebSite(int.Parse(IDoNetBbs.GetConfiguration("WebSite_WebSiteID")), true);
                
                
                Components.Components.WebSite IWebSite = new Components.Components.WebSite();
                IWebSite.SetDataProviders(dt);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_UserNumber", IWebSite.Forum_UserNumber.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_LastUserNickName", IWebSite.Forum_LastUserNickName);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_TodayNumber", IWebSite.Forum_TodayNumber.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_TopicNumber", IWebSite.Forum_TopicNumber.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_PostNumber", IWebSite.Forum_PostNumber.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_YesTerdayNumber", IWebSite.Forum_YesTerdayNumber.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_MaxPostNumber", IWebSite.Forum_MaxPostNumber.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_MaxPostDate", IWebSite.Forum_MaxPostDate.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_LastUserID", IWebSite.Forum_LastUserID.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_AllOnline", IWebSite.Forum_AllOnline.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_StartDate", IWebSite.Forum_StartDate.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserIP", currentUser.UserIP);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_UserOnline", IWebSite.Forum_UserOnline.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_MaxOnline", IWebSite.Forum_MaxOnline.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_GuestOnline", IWebSite.Forum_GuestOnline.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "Forum_MaxOnlineDate", IWebSite.Forum_MaxOnlineDate.ToString());
                HttpBrowserCapabilities bc = new HttpBrowserCapabilities();
                bc = HttpContext.Current.Request.Browser;
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserSystem", bc.Platform.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserExplorer", bc.Type.ToString());
            }
            UserControlMaster.BindMsater();
        }
        /// <summary>
        /// 设置论坛列表
        /// </summary>
        private void SetBoardGroup()
        { 
            Components.Components.Board IBoard = new Components.Components.Board();
            IBoard.Arraylist = MyForum.SetBoardList(boardid, true);
            if (IBoard.Arraylist.Count <= 1)
            {
                return;
            }
            foreach (Components.Components.Board rs in IBoard.Arraylist)
            {
                if ((rs.BoardParentID == 0) || (rs.BoardID == boardid))
                {
                    UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBbsBoardHead");
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", rs.BoardName);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", rs.BoardID.ToString());
                    foreach (Components.Components.Board cs in IBoard.Arraylist)
                    {
                        if (cs.BoardParentID == rs.BoardID)
                        {
                            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBbsBoardBody");

                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", cs.BoardName);
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardAbout", cs.BoardAbout);
                            if (cs.BoardImages != string.Empty)
                            {
                                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardImages", MyConnection.GetTempXmlNode("Resource_WebSiteBbsBoardImages"));
                                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardImages", cs.BoardImages);
                            }
                            else
                            {
                                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardImages", null);
                            }
                            if (cs.BoardFalse == 1)
                            {
                                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardListStates", MyConnection.GetTempXmlNode("Resource_WebSiteBoardListStatesLock"));
                            }
                            else
                            {
                                if (cs.BoardTodayPostNumber != 0)
                                {
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardListStates", MyConnection.GetTempXmlNode("Resource_WebSiteBoardListStatesIsNews"));
                                }
                                else
                                {
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardListStates", MyConnection.GetTempXmlNode("Resource_WebSiteBoardListStatesNotNews"));
                                }
                            }
                            foreach (Components.Components.Board sb in IBoard.Arraylist)
                            {
                                if (sb.BoardParentID == cs.BoardID)
                                {
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "SubForumsList", MyConnection.GetTempXmlNode("Resource_WebSiteSubBbsBoardBody"));
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "SubBoardID", sb.BoardID.ToString());
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "SubBoardName", sb.BoardName);
                                }
                            }
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "SubForumsList", null);

                            if (IDoNetBbs.GetUniqueArraylist(cs.BoardMaster, true, null) != null)
                            {
                                for (int i = 0; i < IDoNetBbs.GetUniqueArraylist(cs.BoardMaster, true, null).Split(',').Length; i++)
                                {
                                    dt = MyUser.SetUserInfo(int.Parse(IDoNetBbs.GetUniqueArraylist(cs.BoardMaster, false, null).Split(',')[i]), true);
                                    if (dt != null)
                                    {
                                        Components.Components.User IUser = new Components.Components.User();
                                        IUser.SetDataProviders(dt);
                                        if (IUser.UserID != 0)
                                        {
                                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "BoardMasterList", MyConnection.GetTempXmlNode("Resource_WebSiteUserInfo"));
                                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "AddUserID", IUser.UserID.ToString());
                                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "AddUserNickName", IUser.UserNickName);
                                        }//
                                        else
                                        {
                                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "BoardMasterList", null);
                                        }
                                    }
                                    else
                                    {
                                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "BoardMasterList", null);
                                    }
                                }//
                            }//

                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardMasterList", null);

                            if (cs.BoardFalse == 1)
                            {
                                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePowerShowBoard", MyConnection.GetTempXmlNode("Resource_WebSiteErrShowBoard"));
                            }
                            else
                            {
                                if ((currentUser.IsSystemAdministrator) || (currentUser.IsBoardAdministrator) || (currentUser.IsTopicAdministrator) || (IDoNetBbs.GetComparison(cs.BoardMaster, true, currentUser.UserID.ToString(), true)) || (IDoNetBbs.GetComparison(cs.BoardViewRole, true, currentUser.UserRole, true)))
                                {
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePowerShowBoard", MyConnection.GetTempXmlNode("Resource_WebSiteShowBoardInfo"));
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardLastTopicTitle", cs.BoardLastTopicTitle);
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardLastTopicUserNickName", cs.BoardLastTopicUserNickName);
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardLastTopicTime", cs.BoardLastTopicTime.ToString());
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardLastTopicID", cs.BoardLastTopicID.ToString());
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardLastTopicUserID", cs.BoardLastTopicUserID.ToString());

                                }
                                else
                                {
                                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePowerShowBoard", MyConnection.GetTempXmlNode("Resource_WebSitePowerShowBoard"));
                                }
                            }

                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardTodayPostNumber", cs.BoardTodayPostNumber.ToString());
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardTopicNumber", cs.BoardTopicNumber.ToString());
                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardPostNumber", cs.BoardPostNumber.ToString());

                            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", cs.BoardID.ToString());
                        }
                    }
                }//
                if (rs.BoardID == boardid)
                {
                    return;
                }
            }
        }//


        private void SetTopicList()
        {
            Control.Query.Board queryBoard = new Control.Query.Board();
            //Components.Current.Users users = new Components.Current.Users();
            //DataProviders.DataConnectionHepler MyConnection = DataProviders.DataConnectionHepler.Instance();
            //DoNetBbs.DoNetBbsClassHepler doNetBbsClass = DoNetBbs.DoNetBbsClassHepler.Instance();
            //DataProviders.BoardDataProviders board = new DataProviders.BoardDataProviders();
            //controls.PageListNavigate pagelist = new controls.PageListNavigate();
            Control.PageListNavigate IPageListNavigate = new Control.PageListNavigate();
            
            Components.Components.Board IBoard = new Components.Components.Board();
            IBoard.Arraylist = MyForum.SetBoardList(boardid, true);

            //board.SetBoardList(query.boardid, true);
            if (IBoard.Arraylist.Count == 0)
            {
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageSearchUrl", queryBoard.GetSearchUrl);
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicListErr");
                return;
            }
            string Boardlist = string.Empty;
            foreach (Components.Components.Board rs in IBoard.Arraylist)
            {
                if ((rs.BoardTypeID == 1) && (rs.BoardFalse == 0))
                {
                    if ((currentUser.IsSystemAdministrator) || (currentUser.IsBoardAdministrator) || (currentUser.IsTopicAdministrator) || (IDoNetBbs.GetComparison(rs.BoardMaster, true, currentUser.UserID.ToString(), true)) || (IDoNetBbs.GetComparison(rs.BoardViewRole, true, currentUser.UserRole, true)))
                    {
                        if (Boardlist == string.Empty)
                        {
                            Boardlist = rs.BoardID.ToString();
                        }
                        else
                        {
                            Boardlist += "," + rs.BoardID.ToString();
                        }
                    }
                }
            }//
            if (Boardlist == string.Empty)
            {
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageSearchUrl", queryBoard.GetSearchUrl);
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicListErr");
                return;
            }
            string Query = queryBoard.GetTopicQuery(Boardlist);

            //DataProviders.TopicDataProviders Topic = new DataProviders.TopicDataProviders();

            IPageListNavigate.page = IDoNetBbs.GetQueryInt("page");


            IPageListNavigate.pagenumber = int.Parse(MyConnection.GetWebSiteConfig("WebSite_TopicNumber"));

            //IPageListNavigate.countnumber = Topic.SetCount(Query, false);
            IPageListNavigate.countnumber = MyForum.SetTopicCount(Query, false);

            if (IPageListNavigate.countnumber == 0)
            {
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", null);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageSearchUrl", queryBoard.GetSearchUrl);
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicListErr");
                return;
            }
            IPageListNavigate.SetNavigate();
            IPageListNavigate.navigateurl = queryBoard.GetSearchUrl;
            string order = " Order by ";
            if (boardid > 0)
            {
                order += " TopicTotalAtTop desc,";
            }//
            if (type == 2)
            {
                order += " TopicReNumber desc";
            }
            else
            {
                order += " TopicLastReTime desc";
            }
            Components.Components.Topic ITopic = new Components.Components.Topic();
            ITopic.Arraylist = MyForum.SetTopic(Query + order, (IPageListNavigate.page - 1) * IPageListNavigate.pagenumber, IPageListNavigate.pagenumber, true);
            //Topic.SetTopic(Query + order, (pagelist.page - 1) * pagelist.pagenumber, pagelist.pagenumber, true);
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardTopicHead");
            foreach (Components.Components.Topic rs in ITopic.Arraylist)
            {
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardTopicListInfo");
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserNickName", rs.TopicPostUserNickName);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicReNumber", rs.TopicReNumber.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicViewNumber", rs.TopicViewNumber.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicLastReTime", rs.TopicLastReTime.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserNickName", rs.TopicPostUserNickName);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserID", rs.TopicPostUserID.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "ReUserNickName", rs.TopicReLastUserNickName);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "ReUserID", rs.TopicReLastUserID.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicTitle", rs.TopicTitle);
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicID", rs.TopicID.ToString());
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicPostTime", rs.TopicPostTime.ToString());


                if (boardid == rs.TopicBoardID)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteArticleBoardList", null);
                }
                else
                {
                    dt = MyForum.SetBoard(rs.TopicBoardID, true);
                    //board.SetBoard(rs.TopicBoardID, true);
                    if (dt == null)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteArticleBoardList", null);
                    }
                    else
                    {
                        IBoard.SetDataProviders(dt);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteArticleBoardList", MyConnection.GetTempXmlNode("Resource_WebSiteArticleBoardList"));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteArticleBoardID", rs.TopicBoardID.ToString());
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteArticleBoardName", IBoard.BoardName);
                    }
                }



                if (rs.TopicReNumber > 0)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicRePostStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicRePostStatic1"));
                }
                else
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicRePostStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicRePostStatic2"));
                }

                if ((IDoNetBbs.GetQueryString("ManagerIDs") != string.Empty) || (IDoNetBbs.GetQueryString("ManagerID") != string.Empty))
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicStaticCheckBox"));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicID", rs.TopicID.ToString());

                }

                else
                {
                    if (rs.TopicFalse == 1)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicStatic3"));
                    }
                    else if (rs.TopicTotalAtTop == 2)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicStatic6"));
                    }
                    else if (rs.TopicTotalAtTop == 1)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicStatic7"));
                    }
                    else if (rs.TopicBest == 1)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicStatic4"));
                    }
                    else if (rs.TopicRecommend == 1)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicStatic5"));
                    }
                    else if (rs.TopicReNumber > 10)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicStatic2"));
                    }
                    else
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteTopicStatic", MyConnection.GetTempXmlNode("Resource_WebSiteTopicStatic1"));
                    }
                }
            }
            if ((IDoNetBbs.GetQueryString("ManagerIDs") != string.Empty) || (IDoNetBbs.GetQueryString("ManagerID") != string.Empty))
            {
                UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteTopicManager");
            }
            Control.BoardNavigate IBoardNavigate = new Control.BoardNavigate();
            //controls.BoardNavigate boardNavigate = new controls.BoardNavigate();
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteBoardTopicPageListInfo");
            IBoardNavigate.boardid = boardid;
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSiteBoardOptionApplication", IBoardNavigate.GetBoardOptionNavigateTitle);

            string PageListNavigateTitle = IPageListNavigate.GetPageListNavigateTitle;
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListTopInfo", PageListNavigateTitle);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "PageInputName", IDoNetBbs.GetRandom(3).ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageListBottomInfo", PageListNavigateTitle);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "PageInputName", IDoNetBbs.GetRandom(4).ToString());
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSitePageSearchUrl", IPageListNavigate.navigateurl);

        }//
        private int _boardid;
        private int boardid
        {
            get { return _boardid; }
            set { _boardid = value; }
        }
        private int _type;
        private int type
        {
            get { return _type; }
            set { _type = value; }
        }
    }
}
