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
    public class BoardTree : System.Web.UI.Page
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
            DosOrg.User.User currentUser = new DosOrg.User.User();
            if (!currentUser.IsSystemAdministrator && !currentUser.IsBoardAdministrator)
            {
                Page.Response.End();
            }

            DataProviders.ForumDataProvider MyForum = DataProviders.ForumDataProvider.Instance();
            Components.Components.Board IBoard = new Components.Components.Board();
            //DataProviders.BoardDataProviders board = new DataProviders.BoardDataProviders();
            //board.SetBoardList(0, false);
            IBoard.Arraylist = MyForum.SetBoardList(0, false);
            string tree=string.Empty;
            tree="<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">";
            foreach (Components.Components.Board rs in IBoard.Arraylist)
            {
                if (rs.BoardParentID == 0)
                {
                    tree += "<tr><td height=\"20\" ><img src=\"images/expand.gif\" > <a href=\"javascript:;\" style=\"cursor:default\" onDblClick=\"JsBoardDblClick(0,'" + rs.BoardID + "');\" onMouseDown=\"JsBoardTree(0,'" + rs.BoardID + "');\">" + rs.BoardName + "</a> </td></tr>";
                }
                else
                {
                    tree += "<tr><td height=\"20\">" + TreeLength(rs.Length) + "<img src=\"images/bullet.gif\" > <a href=\"javascript:;\" style=\"cursor:default\" onDblClick=\"JsBoardDblClick(1,'" + rs.BoardID + "');\" onMouseDown=\"JsBoardTree(1,'" + rs.BoardID + "');\">" + rs.BoardName + "</a> </td></tr>";
                }
            }
            tree += "</table>";
            HttpContext.Current.Response.Write(tree);
            Response.End();
        }
        private string TreeLength(int length)
        {
            string lengthChar = string.Empty;
            for (int i = 0; i < length; i++)
            {
                lengthChar += "<img src=\"images/noexpand.gif\" >";
            }
            return lengthChar;
        }
    }
}
