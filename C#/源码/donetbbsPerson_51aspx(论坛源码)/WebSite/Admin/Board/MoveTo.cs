//===============================================
//��������������������\\\|///                      
//��������������������\\��- -��//                   
//��������������������  ( @ @ )                    
//��������������������oOOo-(_)-oOOo��������          
//��                                     ��
//��             �� �� ԭ ����           ��
//��      lenlong ��Ʒ���뱣������Ϣ��   ��
//��      ** lenlenlong@hotmail.com **   ��
//��                                     ��
//����������������������������Dooo��     ��
//�������������������� oooD��-(�� )��������
//�������������������� (  )��  ) /
//����������������������\ (�� (_/
//���������������������� \_)
//===============================================
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
namespace WebSite.Admin.Board
{
    public class MoveTo : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        TextBox boardID;
        DropDownList parentBoardID;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            boardID = (TextBox)FindControl("boardID");
            parentBoardID = (DropDownList)FindControl("parentBoardID");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (!currentUser.IsSystemAdministrator || !currentUser.IsBoardAdministrator)
            {
                IDoNetBbs.WriteAlert("��û�в�����Ȩ��", false);
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
            if (boardid == 0)
            {
                IDoNetBbs.WriteAlert("�������", false);
                IDoNetBbs.WriteWindowClose(false);
                Page.Response.End();
            }
            boardID.Text = boardid.ToString();
            DataProviders.ForumDataProvider MyForum = DataProviders.ForumDataProvider.Instance();
            Components.Components.Board IBoard = new Components.Components.Board();
            //DataProviders.BoardDataProviders board = new DataProviders.BoardDataProviders();
            //board.SetBoardList(0, false);
            IBoard.Arraylist = MyForum.SetBoardList(0, true);
            foreach (Components.Components.Board rs in IBoard.Arraylist)
            {
                parentBoardID.Items.Add(new ListItem(TreeLength(rs.Length) + rs.BoardName, rs.BoardID.ToString()));
            }
        }
        private void PostData()
        {
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (int.Parse(boardID.Text) == 0)
            {
                IDoNetBbs.WriteAlert("�������", false);
                IDoNetBbs.WriteWindowClose(false);
                Page.Response.End();
            }
            if (int.Parse(boardID.Text) == int.Parse(parentBoardID.SelectedValue))
            {
                IDoNetBbs.WriteAlert("�㲻�ܽ�����̳ת�Ƶ���ͬ����̳", false);
                Page.Response.End();
            }
            DataProviders.ForumDataProvider MyForum = DataProviders.ForumDataProvider.Instance();
            Components.Components.Board IBoard = new Components.Components.Board();
            IBoard.Arraylist = MyForum.SetBoardList(int.Parse(boardID.Text), true);
            //board.SetBoardList(int.Parse(boardID.Text), false);
            foreach (Components.Components.Board rs in IBoard.Arraylist)
            {
                if (rs.BoardID == int.Parse(parentBoardID.SelectedValue))
                {
                    IDoNetBbs.WriteAlert("�㲻�ܽ�����̳ת�Ƶ�������̳��", false);
                    Page.Response.End();
                }
            }
            DataRow dr;
            dr = MyForum.SetBoard(int.Parse(parentBoardID.SelectedValue), false);
            if (dr==null)
            {
                IDoNetBbs.WriteAlert("��Ҫת�Ƶ�����̳������", false);
                Page.Response.End();
            }
            dr = MyForum.SetBoard(int.Parse(boardID.Text), false);
            if (dr == null)
            {
                IDoNetBbs.WriteAlert("��Ҫת�Ƶ���̳�����ڣ�", false);
                Response.End();
            }
            IBoard.SetDataProviders(dr);
            if (IBoard.BoardParentID == 0)
            {
                IDoNetBbs.WriteAlert("������ת�ƴ������̳��", false);
                Response.End();
            }

            IBoard.BoardParentID = int.Parse(parentBoardID.SelectedValue);
            MyForum.UpdateBoardInfo(IBoard);
            Components.CsCache.Clear();
            HttpContext.Current.Response.Write("<script>dialogArguments.JsXmlHttp('Board/BoardTree.aspx','BoardTreeTable');parent.window.close();</script>");
            Response.End();
        }
        private string TreeLength(int length)
        {
            string lengthChar = string.Empty;
            for (int i = 0; i < length; i++)
            {
                lengthChar += "��";
            }
            return lengthChar;
        }
    }
}
