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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace WebSite.Admin.Board
{
    public class TreeBoard : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        Label LabelType1;
        Label LabelType2;
        TextBox BoardID;
        TextBox BoardName;
        RadioButtonList BoardFalse;
        TextBox BoardOrders;
        RadioButtonList BoardTypeID;
        TextBox BoardSubject;
        TextBox BoardLastTopicTitle;
        TextBox BoardPostNumber;
        TextBox BoardTopicNumber;
        TextBox BoardTodayPostNumber;
        TextBox BoardDelPoint;
        TextBox BoardLastTopicUserNickName;
        TextBox BoardLastTopicUserID;
        TextBox BoardLastTopicTime;
        TextBox BoardLastTopicID;
        TextBox BoardMaster;
        TextBox BoardViewRole;
        TextBox BoardRePostRole;
        TextBox BoardPostRole;
        TextBox BoardImages;
        TextBox BoardAbout;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            LabelType1 = (Label)FindControl("LabelType1");
            LabelType2 = (Label)FindControl("LabelType2");
            BoardID = (TextBox)FindControl("BoardID");
            BoardName = (TextBox)FindControl("BoardName");
            BoardFalse = (RadioButtonList)FindControl("BoardFalse");
            BoardOrders = (TextBox)FindControl("BoardOrders");
            BoardTypeID = (RadioButtonList)FindControl("BoardTypeID");
            BoardSubject = (TextBox)FindControl("BoardSubject");
            BoardLastTopicTitle = (TextBox)FindControl("BoardLastTopicTitle");
            BoardPostNumber = (TextBox)FindControl("BoardPostNumber");
            BoardTopicNumber = (TextBox)FindControl("BoardTopicNumber");
            BoardTodayPostNumber = (TextBox)FindControl("BoardTodayPostNumber");
            BoardDelPoint = (TextBox)FindControl("BoardDelPoint");
            BoardLastTopicUserNickName = (TextBox)FindControl("BoardLastTopicUserNickName");
            BoardLastTopicUserID = (TextBox)FindControl("BoardLastTopicUserID");
            BoardLastTopicTime = (TextBox)FindControl("BoardLastTopicTime");
            BoardLastTopicID = (TextBox)FindControl("BoardLastTopicID");
            BoardMaster = (TextBox)FindControl("BoardMaster");
            BoardViewRole = (TextBox)FindControl("BoardViewRole");
            BoardRePostRole = (TextBox)FindControl("BoardRePostRole");
            BoardPostRole = (TextBox)FindControl("BoardPostRole");
            BoardImages = (TextBox)FindControl("BoardImages");
            BoardAbout = (TextBox)FindControl("BoardAbout");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (!currentUser.IsSystemAdministrator && !currentUser.IsBoardAdministrator)
            {
                IDoNetBbs.WriteAlert("您没有操作的权利", false);
                IDoNetBbs.WriteWindowClose(false);
                Page.Response.End();
            }

            if (IsPostBack)
            {
                PostData();
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
            int parentBoardID = 0;
            int boardID = 0;
            if (HttpContext.Current.Request["parentBoardID"] != null)
            {
                parentBoardID = int.Parse(HttpContext.Current.Request["parentBoardID"].ToString());
            }
            if (HttpContext.Current.Request["boardid"] != null)
            {
                boardID = int.Parse(HttpContext.Current.Request["boardid"].ToString());
            }
            if ((parentBoardID == 0) && (boardID == 0))
            {
                IDoNetBbs.WriteAlert("输入错误", false);
                IDoNetBbs.WriteWindowClose(false);
                Page.Response.End();
            }
            if (boardID != 0)
            {
                DataRow dr;
                BoardID.Text = boardID.ToString();
                LabelType1.Visible = false;
                LabelType2.Visible = true;
                DataProviders.ForumDataProvider MyForum = DataProviders.ForumDataProvider.Instance();
                //DataProviders.BoardDataProviders board = new DataProviders.BoardDataProviders();
                //board.SetBoard(boardID, false);
                dr = MyForum.SetBoard(boardID, false);
                if (dr==null)
                {
                    IDoNetBbs.WriteAlert("您要修改的论坛不存在", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                Components.Components.Board IBoard = new Components.Components.Board();
                IBoard.SetDataProviders(dr);
                if (IBoard.BoardParentID == 0)
                {
                    IDoNetBbs.WriteAlert("您要修改的论坛不是小类", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                BoardName.Text = IBoard.BoardName;
                BoardFalse.Items.FindByValue(IBoard.BoardFalse.ToString()).Selected = true;
                BoardTypeID.Items.FindByValue(IBoard.BoardTypeID.ToString()).Selected = true;
                BoardOrders.Text = IBoard.BoardOrders.ToString();
                BoardSubject.Text = IBoard.BoardSubject;
                BoardLastTopicTitle.Text = IBoard.BoardLastTopicTitle;
                BoardPostNumber.Text = IBoard.BoardPostNumber.ToString();
                BoardTopicNumber.Text = IBoard.BoardTopicNumber.ToString();
                BoardTodayPostNumber.Text = IBoard.BoardTodayPostNumber.ToString();
                BoardDelPoint.Text = IBoard.BoardDelPoint.ToString();
                BoardLastTopicUserNickName.Text = IBoard.BoardLastTopicUserNickName;
                BoardLastTopicUserID.Text = IBoard.BoardLastTopicUserID.ToString();
                BoardLastTopicTime.Text = IBoard.BoardLastTopicTime.ToString();
                BoardLastTopicID.Text = IBoard.BoardLastTopicID.ToString();
                BoardMaster.Text = IBoard.BoardMaster;
                BoardViewRole.Text = IBoard.BoardViewRole;
                BoardRePostRole.Text = IBoard.BoardRePostRole;
                BoardPostRole.Text = IBoard.BoardPostRole;
                BoardImages.Text = IBoard.BoardImages;
                BoardAbout.Text = IBoard.BoardAbout;
            }
            else
            {
                BoardID.Text = parentBoardID.ToString();
                LabelType2.Visible = false;
                LabelType1.Visible = true;
                BoardLastTopicTime.Text = DateTime.Now.ToString("yy-MM-dd HH:ss:mm");
            }
        }
        private void PostData()
        {
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (BoardName.Text.Trim() == string.Empty)
            {
                IDoNetBbs.WriteAlert("请输入论坛名称", false);
                IDoNetBbs.WriteFocus("BoardName", false);
                Page.Response.End();
            }
            Components.Components.Board IBoard = new Components.Components.Board();
            DataRow dr;
            DataProviders.ForumDataProvider MyForum = DataProviders.ForumDataProvider.Instance();
            //DataProviders.BoardDataProviders board = new DataProviders.BoardDataProviders();
            int boardID = int.Parse(BoardID.Text);
            if (boardID == 0)
            {
                IDoNetBbs.WriteAlert("输入错误", false);
                IDoNetBbs.WriteWindowClose(false);
                Page.Response.End();
            }
            if (LabelType2.Visible)
            {
                dr = MyForum.SetBoard(boardID, false);
                //board.SetBoard(boardID, false);
                if (dr==null)
                {
                    IDoNetBbs.WriteAlert("您要修改的论坛不存在", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                IBoard.SetDataProviders(dr);
                if (IBoard.BoardParentID == 0)
                {
                    IDoNetBbs.WriteAlert("您要修改的论坛不是小类", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
            }
            else
            {
                Components.Components.Board parentBoard = new Components.Components.Board();
                //DataProviders.BoardDataProviders parentBoard = new DataProviders.BoardDataProviders();
                dr = MyForum.SetBoard(boardID, false);
                parentBoard.SetDataProviders(dr);
                if (dr == null)
                {
                    IDoNetBbs.WriteAlert("您要增加论坛的大类不存在", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                IBoard.BoardParentID = boardID;
            }
            IBoard.BoardName = BoardName.Text;
            IBoard.BoardFalse = int.Parse(BoardFalse.SelectedValue);
            IBoard.BoardTypeID = int.Parse(BoardTypeID.SelectedValue);
            IBoard.BoardOrders = int.Parse(BoardOrders.Text);
            IBoard.BoardSubject = BoardSubject.Text;
            IBoard.BoardLastTopicTitle = BoardLastTopicTitle.Text;
            IBoard.BoardPostNumber = int.Parse(BoardPostNumber.Text);
            IBoard.BoardTopicNumber = int.Parse(BoardTopicNumber.Text);
            IBoard.BoardTodayPostNumber = int.Parse(BoardTodayPostNumber.Text);
            IBoard.BoardDelPoint = int.Parse(BoardDelPoint.Text);
            IBoard.BoardLastTopicUserNickName = BoardLastTopicUserNickName.Text;
            IBoard.BoardLastTopicUserID = int.Parse(BoardLastTopicUserID.Text);
            IBoard.BoardLastTopicTime = System.Convert.ToDateTime(BoardLastTopicTime.Text);
            IBoard.BoardLastTopicID = int.Parse(BoardLastTopicID.Text);
            IBoard.BoardMaster = BoardMaster.Text;
            IBoard.BoardViewRole = BoardViewRole.Text;
            IBoard.BoardRePostRole = BoardRePostRole.Text;
            IBoard.BoardPostRole = BoardPostRole.Text;
            IBoard.BoardImages = BoardImages.Text;
            IBoard.BoardAbout = BoardAbout.Text;
            if (LabelType2.Visible)
            {
                MyForum.UpdateBoardInfo(IBoard);
            }
            else
            {
                MyForum.InsertBoardInfo(IBoard);
            }
            Components.CsCache.Clear();
            HttpContext.Current.Response.Write("<script>dialogArguments.JsXmlHttp('Board/BoardTree.aspx','BoardTreeTable');parent.window.close();</script>");
            Response.End();
        }
    }
}
