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
namespace WebSite.Admin.User
{
    public class UserList : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        Label PageListText;
        TextBox searchKey;
        DropDownList searchSex;
        DropDownList searchRegTime;
        DropDownList searchOrderby;
        LinkButton serachButton;
        Repeater dataRepeater;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            PageListText = (Label)FindControl("PageListText");
            searchKey = (TextBox)FindControl("searchKey");
            searchSex = (DropDownList)FindControl("searchSex");
            searchRegTime = (DropDownList)FindControl("searchRegTime");
            searchOrderby = (DropDownList)FindControl("searchOrderby");
            serachButton = (LinkButton)FindControl("serachButton");
            dataRepeater = (Repeater)FindControl("dataRepeater");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (!currentUser.IsSystemAdministrator && !currentUser.IsMembershipAdministrator)
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
            Control.PageListNavigate IPageListNavigate = new Control.PageListNavigate();
            //controls.PageListNavigate pagelist = new controls.PageListNavigate();
           // DataProviders.DataConnectionHepler MyConnection = DataProviders.DataConnectionHepler.Instance();

            searchKey.Text = IDoNetBbs.GetQueryString("searchKey");
            searchSex.Items.Add(new ListItem("ȫ��", ""));
            searchSex.Items.Add(new ListItem("����", "M"));
            searchSex.Items.Add(new ListItem("Ů��", "F"));
            searchSex.Items.Add(new ListItem("����", "N"));

            if (IDoNetBbs.GetQueryString("searchSex") != string.Empty)
            {
                searchSex.Items.FindByValue(IDoNetBbs.GetQueryString("searchSex")).Selected = true;
            }

            searchOrderby.Items.Add(new ListItem("�û�����", "UserName"));
            searchOrderby.Items.Add(new ListItem("�û��ǳ�", "UserNickName"));
            searchOrderby.Items.Add(new ListItem("�û�ID", "UserID"));
            searchOrderby.Items.Add(new ListItem("�û��Ա�", "UserSex"));
            searchOrderby.Items.Add(new ListItem("ע��ʱ��", "UserRegTime"));
            searchOrderby.Items.Add(new ListItem("���", "UserLastActTime"));
            searchOrderby.Items.Add(new ListItem("��¼����", "UserLoginNumber"));
            if (IDoNetBbs.GetQueryString("searchOrderby") != string.Empty)
            {
                searchOrderby.Items.FindByValue(IDoNetBbs.GetQueryString("searchOrderby")).Selected = true;
            }
            searchRegTime.Items.Add(new ListItem("ȫ��", ""));
            searchRegTime.Items.Add(new ListItem("���һ��", "1"));
            searchRegTime.Items.Add(new ListItem("�������", "3"));
            searchRegTime.Items.Add(new ListItem("���һ��", "7"));
            searchRegTime.Items.Add(new ListItem("����", "31"));
            searchRegTime.Items.Add(new ListItem("������", "90"));
            searchRegTime.Items.Add(new ListItem("�������", "180"));
            searchRegTime.Items.Add(new ListItem("���һ��", "365"));
            searchRegTime.Items.Add(new ListItem("�������", "730"));
            searchRegTime.Items.Add(new ListItem("�������", "1095"));
            if (IDoNetBbs.GetQueryString("searchRegTime") != string.Empty)
            {
                searchRegTime.Items.FindByValue(IDoNetBbs.GetQueryString("searchRegTime")).Selected = true;
            }

            
            //DataProviders.UserInfoDataProviders userInfo = new DataProviders.UserInfoDataProviders();

            //HttpContext.Current.Response.Write(IDoNetBbs.GetAccessDate("d"));
            string sql = string.Empty;
            sql = " where 1=1";
            if (searchKey.Text.Trim() != string.Empty)
            {
                sql += " and UserName like '%" + searchKey.Text.Trim() + "%'";
            }
            if (searchSex.SelectedValue!= string.Empty)
            {
                sql += " and UserSex = '" + searchSex.SelectedValue + "'";
            }
            if (searchRegTime.SelectedValue != string.Empty)
            {
                sql += " and DateDiff(" + IDoNetBbs.GetAccessDate("d") + ",UserRegTime,'" + DateTime.Now + "')<=" + searchRegTime.SelectedValue + "";
            }//
           
            IPageListNavigate.page = IDoNetBbs.GetQueryInt("page");

            IPageListNavigate.pagenumber = 16;

            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            Components.Components.User IUser = new Components.Components.User();

            IPageListNavigate.countnumber = MyUser.SetUserListCount("select count(UserID) as CountNumber from DoNetBbs_UserInfo " + sql, false);

            if (IPageListNavigate.countnumber == 0)
            {
                return;
            }
            //HttpContext.Current.Response.Write("select count(UserID) as CountNumber from DoNetBbs_UserInfo" + sql);
            //return;
            IUser.Arraylist = MyUser.SetUserList("select * from DoNetBbs_UserInfo " + sql + " order by " + searchOrderby.Text + " desc", (IPageListNavigate.page - 1) * IPageListNavigate.pagenumber, IPageListNavigate.pagenumber, false);

            dataRepeater.DataSource = IUser.Arraylist;
            dataRepeater.DataBind();
            IPageListNavigate.SetNavigate();
            IPageListNavigate.navigateurl = "searchKey=" + searchKey.Text + "&searchSex=" + searchSex.SelectedValue + "&searchRegTime=" + searchRegTime.SelectedValue + "&searchOrderby=" + searchOrderby.SelectedValue + "";

            IPageListNavigate.DisplayPageInput = false;
            PageListText.Text = IDoNetBbs.GetFormat(IPageListNavigate.GetPageListNavigateTitle, "PageInputName", "");
        }
        protected void serachButton_Click(object sender, EventArgs e)
        {
            //thread = new Thread(new ThreadStart(this.ThreadListen));
            //thread.Start();
            //return;
            Page.Response.Redirect("UserList.aspx?searchKey=" + searchKey .Text+ "&searchSex=" + searchSex .Text+ "&searchRegTime=" + searchRegTime .Text+ "&searchOrderby=" + searchOrderby .Text+ "");
            Response.End();
        }






        //public delegate void ShowData();
        //public bool m_bConnected = false;
        //public IPEndPoint ipEndPoint = null;
        //public Socket socKet = null;
        //public NetworkStream networkStream = null;
        //public TextReader textReader = null;
        //public TextWriter textWriter = null;
        //public Thread thread = null;
        //public void ThreadListen()
        //{
        //    IPAddress serIP;
        //    serIP = IPAddress.Parse("127.0.0.1");
        //    ipEndPoint = new IPEndPoint(serIP, 9999);
        //    socKet = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        //    socKet.Connect(ipEndPoint);
        //    textWriter = new StreamWriter(new NetworkStream(socKet));
        //    m_bConnected = true;
        //    string tmp = string.Empty;
        //    bool boolThreadListen = true;
        //    while (boolThreadListen)
        //    {
        //        try
        //        {

        //            if (socKet.Connected)
        //            {
        //                m_bConnected = true;
        //                byte[] recvBytes = new byte[1024];
        //                int bytes = 0;
        //                bytes = socKet.Receive(recvBytes, recvBytes.Length, 0);
        //                if (bytes > 0)
        //                {
        //                    charConntent = Encoding.UTF8.GetString(recvBytes, 0, bytes) + "\n";
        //                    //ShowData sbbb;
        //                    //sbbb.Invoke = new ShowData(ShowFour);
        //                    //searchKey.Init(new ShowData(ShowFour));
        //                }
        //                else
        //                {
        //                    //boolReturn = false;
        //                }
        //            }
        //        }
        //        catch
        //        {
        //        }
        //    }
        //} //����

        //private void ShowFour()
        //{
        //    searchKey.Text = charConntent;
        //}
        //private string _chatConntent = "dssddsd";
        //private string charConntent
        //{
        //    get { return _chatConntent; }
        //    set { _chatConntent = value; }
        //}

    }
}
