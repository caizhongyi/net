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
    public class DeleteBoard : System.Web.UI.Page
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
            int boardID = IDoNetBbs.GetQueryInt("boardid");
            if (boardID == 0)
            {
                IDoNetBbs.WriteAlert("�������", false);
                IDoNetBbs.WriteWindowClose(false);
                Page.Response.End();
            }
            DataProviders.ForumDataProvider MyForumDate = DataProviders.ForumDataProvider.Instance();


            //DataProviders.BoardDataProviders board = new DataProviders.BoardDataProviders();
            //DataProviders.TopicDataProviders topic = new DataProviders.TopicDataProviders();
            //DataProviders.TopicInfoDataProviders topicInfo = new DataProviders.TopicInfoDataProviders();
            //DataProviders.UserInfoDataProviders userInfo = new DataProviders.UserInfoDataProviders();

            Components.Components.Board IBoard = new Components.Components.Board();
            IBoard.Arraylist = MyForumDate.SetBoardList(boardID, false);

            Components.Components.Topic ITopic = new Components.Components.Topic();
            Components.Components.TopicInfo ITopicInfo = new Components.Components.TopicInfo();
            Components.Components.User IUser = new Components.Components.User();
            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            DataRow dr;
            //board.SetBoardList(boardID, false);
            foreach (Components.Components.Board rs in IBoard.Arraylist)
            {
                //ɾ����̳δ���
                ITopic.Arraylist = MyForumDate.SetBoardTopic(rs.BoardID, false);
                ////topic.SetBoardTopic(rs.BoardID, false);
                foreach (Components.Components.Topic ts in ITopic.Arraylist)
                {
                    //topicInfo.SetTopicInfo(ts.TopicID, 0, 0, 0, false);
                    ITopicInfo.Arraylist = MyForumDate.SetTopicInfoList(ts.TopicID, 0, 0, false);
                    foreach (Components.Components.TopicInfo fs in ITopicInfo.Arraylist)
                    {
                        if (fs.TopicInfoUserID != 0)
                        {
                            dr = MyUser.SetUserInfo(fs.TopicInfoUserID, false);
                            if (dr != null)
                            {
                                IUser.SetDataProviders(dr);
                                IUser.UserPostNumber -= 1;
                                if (fs.TopicInfoParentID == 0)
                                {
                                    IUser.UserTopicNumber -= 1;
                                }
                                else
                                {
                                    IUser.UserReTopicNumber -= 1;
                                }
                                MyUser.UpdateUserInfo(IUser);//�����û���Ϣ
                            }
                        }//�û�
                        MyForumDate.DeleteTopicInfo(fs.TopicInfoID);//ɾ������
                    }//
                    MyForumDate.DeleteTopic(ts.TopicID);//ɾ������
                }//
                MyForumDate.DeleteBoard(rs.BoardID);//ɾ����̳
            }//

            Components.CsCache.Clear();
            HttpContext.Current.Response.Write("<script>parent.JsXmlHttp('Board/BoardTree.aspx','BoardTreeTable');window.location='about:blank';</script>");
            Response.End();
        }
    }
}
