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
namespace WebSite.Admin
{
    public class WebSiteSitting : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        TextBox Forum_MaxOnline;
        TextBox Forum_MaxOnlineDate;
        TextBox Forum_TopicNumber;
        TextBox Forum_PostNumber;
        TextBox Forum_TodayNumber;
        TextBox Forum_UserNumber;
        TextBox Forum_YesTerdayNumber;
        TextBox Forum_MaxPostNumber;
        TextBox Forum_MaxPostDate;
        TextBox Forum_LastUserID;
        TextBox Forum_LockIP;
        TextBox Forum_TodyDate;
        TextBox Forum_StartDate;
        TextBox Forum_LastUserNickName;
        TextBox Forum_UserOnline;
        TextBox Forum_GuestOnline;
        TextBox Forum_AllOnline;
        TextBox Forum_UserIllegal;
        TextBox Forum_SystemIllegal;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            Forum_MaxOnline = (TextBox)FindControl("Forum_MaxOnline");
            Forum_MaxOnlineDate = (TextBox)FindControl("Forum_MaxOnlineDate");
            Forum_TopicNumber = (TextBox)FindControl("Forum_TopicNumber");
            Forum_PostNumber = (TextBox)FindControl("Forum_PostNumber");
            Forum_TodayNumber = (TextBox)FindControl("Forum_TodayNumber");
            Forum_UserNumber = (TextBox)FindControl("Forum_UserNumber");
            Forum_YesTerdayNumber = (TextBox)FindControl("Forum_YesTerdayNumber");
            Forum_MaxPostNumber = (TextBox)FindControl("Forum_MaxPostNumber");
            Forum_MaxPostDate = (TextBox)FindControl("Forum_MaxPostDate");
            Forum_LastUserID = (TextBox)FindControl("Forum_LastUserID");
            Forum_LockIP = (TextBox)FindControl("Forum_LockIP");
            Forum_TodyDate = (TextBox)FindControl("Forum_TodyDate");
            Forum_StartDate = (TextBox)FindControl("Forum_StartDate");
            Forum_LastUserNickName = (TextBox)FindControl("Forum_LastUserNickName");
            Forum_UserOnline = (TextBox)FindControl("Forum_UserOnline");
            Forum_GuestOnline = (TextBox)FindControl("Forum_GuestOnline");
            Forum_AllOnline = (TextBox)FindControl("Forum_AllOnline");
            Forum_UserIllegal = (TextBox)FindControl("Forum_UserIllegal");
            Forum_SystemIllegal = (TextBox)FindControl("Forum_SystemIllegal");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (!currentUser.IsSystemAdministrator)
            {
                IDoNetBbs.WriteAlert("您没有操作的权利", false);
                IDoNetBbs.WriteWindowClose(false);
                Page.Response.End();
            }
            if (IsPostBack)
            {
                DataPost();
            }
            else
            {
                DataBind();
            }
        }
        public override void DataBind()
        {
            base.DataBind();
            DataProviders.WebSiteDataProvider MyWebSite = DataProviders.WebSiteDataProvider.Instance();
            //DataProviders.WebSiteDataProviders webSite =new DataProviders.WebSiteDataProviders();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            DataRow dr;
            dr = MyWebSite.SetWebSite(int.Parse(IDoNetBbs.GetConfiguration("WebSite_WebSiteID")), false);



            if (dr==null)
            {
                IDoNetBbs.WriteAlert("系统错误", false);
                IDoNetBbs.WriteWindowClose(false);
                Page.Response.End();
            }
            Components.Components.WebSite IWebSite = new Components.Components.WebSite();
            IWebSite.SetDataProviders(dr);

            Forum_MaxOnline.Text = IWebSite.Forum_MaxOnline.ToString();
            Forum_MaxOnlineDate.Text = IWebSite.Forum_MaxOnlineDate.ToString("yyyy-MM-dd HH:mm:ss");
            Forum_TopicNumber.Text = IWebSite.Forum_TopicNumber.ToString();
            Forum_PostNumber.Text = IWebSite.Forum_PostNumber.ToString();
            Forum_TodayNumber.Text = IWebSite.Forum_TodayNumber.ToString();
            Forum_UserNumber.Text = IWebSite.Forum_UserNumber.ToString();
            Forum_YesTerdayNumber.Text = IWebSite.Forum_YesTerdayNumber.ToString();
            Forum_MaxPostNumber.Text = IWebSite.Forum_MaxPostNumber.ToString();
            Forum_MaxPostDate.Text = IWebSite.Forum_MaxPostDate.ToString("yyyy-MM-dd HH:mm:ss"); ;
            Forum_LastUserID.Text = IWebSite.Forum_LastUserID.ToString();
            Forum_LockIP.Text = IWebSite.Forum_LockIP;
            Forum_TodyDate.Text = IWebSite.Forum_TodyDate.ToString("yyyy-MM-dd HH:mm:ss"); ;
            Forum_StartDate.Text = IWebSite.Forum_StartDate.ToString("yyyy-MM-dd HH:mm:ss");
            Forum_LastUserNickName.Text = IWebSite.Forum_LastUserNickName;
            Forum_UserOnline.Text = IWebSite.Forum_UserOnline.ToString();
            Forum_GuestOnline.Text = IWebSite.Forum_GuestOnline.ToString();
            Forum_AllOnline.Text = IWebSite.Forum_AllOnline.ToString();
            Forum_UserIllegal.Text = IWebSite.Forum_UserIllegal;
            Forum_SystemIllegal.Text = IWebSite.Forum_SystemIllegal;
        }
        private void DataPost()
        {
            DataProviders.WebSiteDataProvider MyWebSite = DataProviders.WebSiteDataProvider.Instance();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            Components.Components.WebSite IWebSite = new Components.Components.WebSite();
            DataRow dr;
            dr = MyWebSite.SetWebSite(int.Parse(IDoNetBbs.GetConfiguration("WebSite_WebSiteID")), false);

            if (dr==null)
            {
                IDoNetBbs.WriteAlert("系统错误", false);
                IDoNetBbs.WriteWindowClose(false);
                Page.Response.End();
            }
            IWebSite.SetDataProviders(dr);
            IWebSite.Forum_MaxOnline = int.Parse(Forum_MaxOnline.Text);
            IWebSite.Forum_MaxOnlineDate = System.Convert.ToDateTime(Forum_MaxOnlineDate.Text);
            IWebSite.Forum_TopicNumber = int.Parse(Forum_TopicNumber.Text);
            IWebSite.Forum_PostNumber = int.Parse(Forum_PostNumber.Text);
            IWebSite.Forum_TodayNumber = int.Parse(Forum_TodayNumber.Text);
            IWebSite.Forum_UserNumber = int.Parse(Forum_UserNumber.Text);
            IWebSite.Forum_YesTerdayNumber = int.Parse(Forum_YesTerdayNumber.Text);
            IWebSite.Forum_MaxPostNumber = int.Parse(Forum_MaxPostNumber.Text);
            IWebSite.Forum_MaxPostDate = System.Convert.ToDateTime(Forum_MaxPostDate.Text);
            IWebSite.Forum_LastUserID = int.Parse(Forum_LastUserID.Text);
            IWebSite.Forum_LockIP = Forum_LockIP.Text;
            IWebSite.Forum_TodyDate = System.Convert.ToDateTime(Forum_TodyDate.Text);
            IWebSite.Forum_StartDate = System.Convert.ToDateTime(Forum_StartDate.Text);
            IWebSite.Forum_LastUserNickName = Forum_LastUserNickName.Text;
            IWebSite.Forum_UserOnline = int.Parse(Forum_UserOnline.Text);
            IWebSite.Forum_GuestOnline = int.Parse(Forum_GuestOnline.Text);
            IWebSite.Forum_AllOnline = int.Parse(Forum_AllOnline.Text);
            IWebSite.Forum_UserIllegal = Forum_UserIllegal.Text;
            IWebSite.Forum_SystemIllegal = Forum_SystemIllegal.Text;
            MyWebSite.UpdateWebSite(IWebSite);
            IDoNetBbs.WriteAlert("更新成功", false);
            Page.Response.End();
        }
    }
}
