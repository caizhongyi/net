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
    public class BigBoard : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        Label TitleType;
        Label TitleType1;
        Label BoardID;
        TextBox BoardName;
        RadioButtonList BoardFalse;
        TextBox BoardOrders;
        TextBox BoardViewRole;
        TextBox BoardRePostRole;
        TextBox BoardPostRole;
        TextBox BoardAbout;
        RadioButtonList BoardTypeID;
        TextBox BoardMaster;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            TitleType = (Label)FindControl("TitleType");
            TitleType1 = (Label)FindControl("TitleType1");
            BoardID = (Label)FindControl("BoardID");
            BoardName = (TextBox)FindControl("BoardName");
            BoardFalse = (RadioButtonList)FindControl("BoardFalse");
            BoardOrders = (TextBox)FindControl("BoardOrders");
            BoardViewRole = (TextBox)FindControl("BoardViewRole");
            BoardRePostRole = (TextBox)FindControl("BoardRePostRole");
            BoardPostRole = (TextBox)FindControl("BoardPostRole");
            BoardAbout = (TextBox)FindControl("BoardAbout");
            BoardTypeID = (RadioButtonList)FindControl("BoardTypeID");
            BoardMaster = (TextBox)FindControl("BoardMaster");
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
            int boardid = IDoNetBbs.GetQueryInt("boardid");
            if (boardid != 0)
            {
                DataRow dr;
                DataProviders.ForumDataProvider MyForum = DataProviders.ForumDataProvider.Instance();
                dr = MyForum.SetBoard(boardid, false);
                if (dr==null)
                {
                    IDoNetBbs.WriteAlert("您要修改的论坛不存在", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                Components.Components.Board IBoard = new Components.Components.Board();
                IBoard.SetDataProviders(dr);
                if (IBoard.BoardParentID != 0)
                {
                    IDoNetBbs.WriteAlert("您要修改的论坛不是大类", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                TitleType.Visible = false;
                TitleType1.Visible = true;
                BoardID.Text = IBoard.BoardID.ToString();
                BoardName.Text = IBoard.BoardName;
                BoardFalse.Items.FindByValue(IBoard.BoardFalse.ToString()).Selected = true;
                BoardTypeID.Items.FindByValue(IBoard.BoardTypeID.ToString()).Selected = true;
                BoardOrders.Text = IBoard.BoardOrders.ToString();
                BoardViewRole.Text = IBoard.BoardViewRole;
                BoardRePostRole.Text = IBoard.BoardRePostRole;
                BoardPostRole.Text = IBoard.BoardPostRole;
                BoardAbout.Text = IBoard.BoardAbout;
                BoardMaster.Text = IBoard.BoardMaster;

            }
        }
        private void PostData()
        {
            //HttpContext.Current.Response.Write(DateTime.Now.ToString());
            //int boardid = 0;
            int boardid = int.Parse(BoardID.Text);
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            DataRow dr;
            DataProviders.ForumDataProvider MyForum = DataProviders.ForumDataProvider.Instance();

            if (BoardName.Text.Trim() == string.Empty)
            {
                IDoNetBbs.WriteAlert("请输入论坛的名称", false);
                Page.Response.End();
            }
            Components.Components.Board IBoard = new Components.Components.Board();

            if (boardid != 0)
            {
                dr = MyForum.SetBoard(boardid, false);
                if (dr == null)
                {
                    IDoNetBbs.WriteAlert("您要修改的论坛不存在", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                IBoard.SetDataProviders(dr);
            }
           
            IBoard.BoardName = BoardName.Text;
            IBoard.BoardFalse = int.Parse(BoardFalse.SelectedValue.ToString());
            IBoard.BoardTypeID = int.Parse(BoardTypeID.SelectedValue.ToString());
            IBoard.BoardOrders = int.Parse(BoardOrders.Text);
            IBoard.BoardViewRole = BoardViewRole.Text;
            IBoard.BoardRePostRole = BoardRePostRole.Text;
            IBoard.BoardPostRole = BoardPostRole.Text;
            IBoard.BoardAbout = BoardAbout.Text;
            IBoard.BoardMaster = BoardMaster.Text;
            if (boardid == 0)
            {
                IBoard.BoardLastTopicTime = DateTime.Now;
                MyForum.InsertBoardInfo(IBoard);
            }
            else
            {
                if (IBoard.BoardParentID != 0)
                {
                    IDoNetBbs.WriteAlert("您要修改的论坛不是大类", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                MyForum.UpdateBoardInfo(IBoard);
            }
            Components.CsCache.Clear();
            HttpContext.Current.Response.Write("<script>dialogArguments.JsXmlHttp('Board/BoardTree.aspx','BoardTreeTable');parent.window.close();</script>");
            Response.End();
        }
    }
}
