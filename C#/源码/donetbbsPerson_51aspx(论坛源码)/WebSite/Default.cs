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
namespace WebSite
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
        public override void DataBind()
        {
            base.DataBind();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            DataProviders.DataConnectionHepler MyConnection = DataProviders.DataConnectionHepler.Instance();
            Components.SiteWebSetting.WebSiteTitle = string.Format(MyConnection.GetTreeXmlNode("WebSiteTitle", "WebSiteDeFault"), MyConnection.GetTreeXmlNode("WebSiteTitle", "DeFault"));
            UserControlMaster.WebSiteMasterBody += MyConnection.GetTempXmlNode("Resource_WebSiteDeFault");
            DosOrg.User.User currentUser = new DosOrg.User.User();
            Components.Components.User IUser = new Components.Components.User();
            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            DataRow dr;
            if (currentUser.UserID == 0)
            {
                UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultUserLoginInfo", MyConnection.GetTempXmlNode("Resource_DeFaultUserNotLoginInfo"));
            }
            else
            {
                dr = MyUser.SetUserInfo(currentUser.UserID, true);
                if (dr == null)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultUserLoginInfo", MyConnection.GetTempXmlNode("Resource_DeFaultUserNotLoginInfo"));
                }
                else
                {
                    IUser.SetDataProviders(dr);
                    Control.UserInfoControl IUserInfoControl = new Control.UserInfoControl();
                    IUserInfoControl.userOnlineStatus = IUser.UserOnLineStatic;
                    IUserInfoControl.userLastActTime = IUser.UserLastActTime;
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultUserLoginInfo", MyConnection.GetTempXmlNode("Resource_DeFaultUserLoginInfo"));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserFace", IUser.UserFace);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserID", IUser.UserID.ToString());
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserNickName", IUser.UserNickName.ToString());
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserOnLineStatic", IUserInfoControl.UserOnlineStatus);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserPrestige", IUser.UserPrestige.ToString());
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "UserOnlineTime", (IUser.UserOnlineTime / 60).ToString());
                }
            }
            DataProviders.ForumDataProvider MyForum = DataProviders.ForumDataProvider.Instance();
            Components.Components.Board IBoard = new Components.Components.Board();
            IBoard.Arraylist = MyForum.SetBoardList(0, true);

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
            if (Boardlist != string.Empty)
            {
                Components.Components.Topic ITopic = new Components.Components.Topic();
                string MySql;

                Components.Components.TopicInfo ITopicInfo = new Components.Components.TopicInfo();
                MySql = " where TopicBoardID in (" + Boardlist + ") and TopicImages<>'' order by TopicLastReTime desc";
                ITopic.Arraylist = MyForum.SetTopic(MySql, 0, 6, true);
                foreach (Components.Components.Topic rs in ITopic.Arraylist)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultInfo", MyConnection.GetTempXmlNode("WebSite_DeFaultInfo"));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicTitle", IDoNetBbs.GetFewChars(rs.TopicTitle, 60));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicImages", rs.TopicImages);
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicID", rs.TopicID.ToString());

                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicPostUserID", rs.TopicPostUserID.ToString());
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicPostUserNickName", rs.TopicPostUserNickName.ToString());
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicPostTime", rs.TopicPostTime.ToString());

                    dr = MyForum.SetRootTopicInfo(rs.TopicID, true);
                    if (dr == null)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoText", null);
                    }
                    else
                    {
                        ITopicInfo.SetDataProviders(dr);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicInfoText", IDoNetBbs.GetFewChars(ITopicInfo.TopicInfoText, 300));
                    }

                    dr = MyForum.SetBoard(rs.TopicBoardID, true);
                    if (dr == null)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", null);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", null);
                    }
                    else
                    {
                        IBoard.SetDataProviders(dr);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", IDoNetBbs.GetFewChars(IBoard.BoardName, 12));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", IBoard.BoardID.ToString());
                    }
                }


                MySql = " where TopicBoardID in (" + Boardlist + ") order by TopicLastReTime desc";
                ITopic.Arraylist = MyForum.SetTopic(MySql, 0, 10, true);
                foreach (Components.Components.Topic rs in ITopic.Arraylist)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultNewTopic", MyConnection.GetTempXmlNode("WebSite_DeFaultTopic"));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicTitle", IDoNetBbs.GetFewChars(rs.TopicTitle, 24));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicID", rs.TopicID.ToString());
                    dr = MyForum.SetBoard(rs.TopicBoardID, true);
                    if (dr == null)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", null);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", null);
                    }
                    else
                    {
                        IBoard.SetDataProviders(dr);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", IDoNetBbs.GetFewChars(IBoard.BoardName, 8));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", IBoard.BoardID.ToString());
                    }
                }
                MySql = " where TopicBoardID in (" + Boardlist + ") and TopicRecommend=1 order by TopicLastReTime desc";
                ITopic.Arraylist = MyForum.SetTopic(MySql, 0, 10, true);
                foreach (Components.Components.Topic rs in ITopic.Arraylist)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultReCommendTopic", MyConnection.GetTempXmlNode("WebSite_DeFaultTopic"));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicTitle", IDoNetBbs.GetFewChars(rs.TopicTitle, 24));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicID", rs.TopicID.ToString());
                    dr = MyForum.SetBoard(rs.TopicBoardID, true);
                    if (dr == null)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", null);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", null);
                    }
                    else
                    {
                        IBoard.SetDataProviders(dr);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", IDoNetBbs.GetFewChars(IBoard.BoardName, 8));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", IBoard.BoardID.ToString());
                    }
                }
                MySql = " where TopicBoardID in (" + Boardlist + ") and TopicBest=1 order by TopicLastReTime desc";
                ITopic.Arraylist = MyForum.SetTopic(MySql, 0, 10, true);
                foreach (Components.Components.Topic rs in ITopic.Arraylist)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultBestTopic", MyConnection.GetTempXmlNode("WebSite_DeFaultTopic"));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicTitle", IDoNetBbs.GetFewChars(rs.TopicTitle, 24));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicID", rs.TopicID.ToString());
                    dr = MyForum.SetBoard(rs.TopicBoardID, true);
                    if (dr == null)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", null);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", null);
                    }
                    else
                    {
                        IBoard.SetDataProviders(dr);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", IDoNetBbs.GetFewChars(IBoard.BoardName, 8));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", IBoard.BoardID.ToString());
                    }
                }
                MySql = " where TopicBoardID in (" + Boardlist + ") order by TopicViewNumber desc";
                ITopic.Arraylist = MyForum.SetTopic(MySql, 0, 10, true);
                foreach (Components.Components.Topic rs in ITopic.Arraylist)
                {
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormatRepeat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultHotTopic", MyConnection.GetTempXmlNode("WebSite_DeFaultTopic"));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicTitle", IDoNetBbs.GetFewChars(rs.TopicTitle, 24));
                    UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "TopicID", rs.TopicID.ToString());
                    dr = MyForum.SetBoard(rs.TopicBoardID, true);
                    if (dr == null)
                    {
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", null);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", null);
                    }
                    else
                    {
                        IBoard.SetDataProviders(dr);
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardName", IDoNetBbs.GetFewChars(IBoard.BoardName, 8));
                        UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "BoardID", IBoard.BoardID.ToString());
                    }
                }
                
            }

            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultInfo", null);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultNewTopic", null);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultReCommendTopic", null);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultBestTopic", null);
            UserControlMaster.WebSiteMasterBody = IDoNetBbs.GetFormat(UserControlMaster.WebSiteMasterBody, "WebSite_DeFaultHotTopic", null);
            
                
            
                
            
            UserControlMaster.BindMsater();
        }
    }
}
