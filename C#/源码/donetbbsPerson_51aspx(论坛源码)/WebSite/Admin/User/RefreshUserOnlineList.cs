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
    public class RefreshUserOnlineList : System.Web.UI.Page
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
            if (!currentUser.IsHaveAdministrator)
            {
                Page.Response.End();
            }
            DataBind();
            if (IsPostBack)
            {
            }
            else
            {
                
            }
        }
        public override void DataBind()
        {
            base.DataBind();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            DataProviders.UserOnLineDataProvider uerOnLineData = DataProviders.UserOnLineDataProvider.Instance();
            Components.Components.UserOnLine IUserOnLine = new Components.Components.UserOnLine();
            string html = string.Empty;
            html+="<table width=\"100%\" border=\"0\" cellpadding=\"0\" cellspacing=\"1\">";
            html+="<tr>";
            html+="<td width=\"15%\" height=\"26\" align=\"center\" class=\"Content\">�û�����</td>";
            html+="<td align=\"center\" class=\"Content\">��Դ</td>";
            html+="<td width=\"10%\" align=\"center\" class=\"Content\">IP</td>";
            html+="<td width=\"20%\" align=\"center\" class=\"Content\">����λ��</td>";
            html+="<td width=\"15%\" align=\"center\" class=\"Content\">ʱ��</td>";
            html+="</tr>";
            IUserOnLine.Arraylist = uerOnLineData.SetRefreshUserOnlineList(21, false);
            foreach (Components.Components.UserOnLine rs in IUserOnLine.Arraylist)
            {
                html += "<tr>";
                if (rs.UserOnLineUserID == 0)
                {
                    html += "<td height=\"24\" align=\"left\" class=\"White\">" + rs.UserOnLineUserNickName + "</td>";
                }
                else
                {
                    html += "<td align=\"left\" class=\"White\" height=\"24\"><a href=\"../UserInfo/UserInfo.aspx?UserID=" + rs.UserOnLineUserID + "\" target=\"_blank\">" + rs.UserOnLineUserNickName + "</a></td>";
                }
                html += "<td align=\"left\" class=\"White\"><a href=\"" + rs.UserOnLineComeFromPath + "\" target=\"_blank\">" + IDoNetBbs.GetFewChars(rs.UserOnLineComeFromPath, 60) + "</a></td>";
                html += "<td align=\"left\" class=\"White\">" + rs.UserOnLineIP + "</td>";
                html += "<td align=\"left\" class=\"White\"><a href=\"" + rs.UserOnLineBrowserPath + "\" target=\"_blank\">" + IDoNetBbs.GetFewChars(rs.UserOnLineBrowserTitle, 30) + "</a></td>";
                html += "<td align=\"right\" class=\"White\">" + rs.UserOnLineLastTime.ToString("yyyy-MM-dd HH:mm:ss") + "</td>";
                html += "</tr>";
            }
          html+="</table>";
          HttpContext.Current.Response.Write(html);
          Response.End();
        }
    }
}
