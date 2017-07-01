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
    public class UserOnlineList : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        Label PageListText;
        TextBox searchKey;
        DropDownList searchRegTime;
        DropDownList searchOrderby;
        LinkButton serachButton;
        Repeater dataRepeater;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            PageListText = (Label)FindControl("PageListText");
            searchKey = (TextBox)FindControl("searchKey");
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
            //controls.PageListNavigate pagelist = new controls.PageListNavigate();
            Control.PageListNavigate IPageListNavigate = new Control.PageListNavigate();

            DataProviders.UserOnLineDataProvider MyUserOnLine = DataProviders.UserOnLineDataProvider.Instance();
            Components.Components.UserOnLine IUserOnLine = new Components.Components.UserOnLine();

            //DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            //Components.Components.User IUser = new Components.Components.User();

            //DataProviders.UserOnlineDataProviders userOnline = new DataProviders.UserOnlineDataProviders();
            int deleteUserOnLineID = IDoNetBbs.GetQueryInt("deleteUserOnLineID");
            if (deleteUserOnLineID != 0)
            {
                MyUserOnLine.DeleteUserOnLine(deleteUserOnLineID);
                Components.CsCache.Clear();
                HttpContext.Current.Response.Write("<script>parent.window.location.reload();</script>");
                Response.End();
            }


            if (IDoNetBbs.GetQueryString("searchKey") != string.Empty)
            {
                searchKey.Text = IDoNetBbs.GetQueryString("searchKey");
            }
            searchOrderby.Items.Add(new ListItem("���", "UserOnLineLastTime"));
            searchOrderby.Items.Add(new ListItem("�û��ǳ�", "UserOnLineUserNickName"));
            searchOrderby.Items.Add(new ListItem("�û�ID", "UserOnLineUserID"));
            searchOrderby.Items.Add(new ListItem("IP", "UserOnLineIP"));
            searchOrderby.Items.Add(new ListItem("��Դ", "UserOnLineComeFromPath"));
            searchOrderby.Items.Add(new ListItem("����λ��", "UserOnLineBrowserPath"));
            searchOrderby.Items.Add(new ListItem("�����", "UserOnLineBrowser"));
            searchOrderby.Items.Add(new ListItem("����ϵͳ", "UserOnLineSystem"));
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


            string sql = string.Empty;
            sql = " from DoNetBbs_UserOnLine  where 1=1";
            if (searchKey.Text.Trim() != string.Empty)
            {
                sql += " and UserOnLineUserNickName like '%" + searchKey.Text.Trim() + "%'";
            }
            if (searchRegTime.SelectedValue != string.Empty)
            {
                sql += " and DateDiff(" + IDoNetBbs.GetAccessDate("d") + ",UserOnLineLastTime,'" + DateTime.Now + "')<=" + searchRegTime.SelectedValue + "";
            }//
            IPageListNavigate.page = IDoNetBbs.GetQueryInt("page");

            IPageListNavigate.pagenumber = 16;
            IPageListNavigate.countnumber = MyUserOnLine.SetUserOnLineCount("select count(UserOnLineID) as CountNumber " + sql, false);

            if (IPageListNavigate.countnumber == 0)
            {
                return;
            }
            IUserOnLine.Arraylist = MyUserOnLine.SetUserOnLineList("select * " + sql + " order by " + searchOrderby.Text + " desc", (IPageListNavigate.page - 1) * IPageListNavigate.pagenumber, IPageListNavigate.pagenumber, false);

            dataRepeater.DataSource = IUserOnLine.Arraylist;
            dataRepeater.DataBind();
            IPageListNavigate.SetNavigate();
            IPageListNavigate.navigateurl = "searchKey=" + searchKey.Text + "&searchRegTime=" + searchRegTime.SelectedValue + "&searchOrderby=" + searchOrderby.SelectedValue + "";
            IPageListNavigate.DisplayPageInput = false;
            PageListText.Text = IDoNetBbs.GetFormat(IPageListNavigate.GetPageListNavigateTitle, "PageInputName", "");
        }
        protected void serachButton_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("UserOnlineList.aspx?searchKey=" + searchKey .Text+ "&searchRegTime=" + searchRegTime .Text+ "&searchOrderby=" + searchOrderby .Text+ "");
            Response.End();
        }
    }
}
