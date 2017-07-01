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
namespace WebSite.Admin.User
{
    public class UserLevel : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        Label LabelType1;
        Label LabelType2;
        Label userLevelID;
        TextBox UserLevelTitle;
        TextBox UserLevelImages;
        TextBox UserLevelPoint;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            userLevelID = (Label)FindControl("userLevelID");
            LabelType1 = (Label)FindControl("LabelType1");
            LabelType2 = (Label)FindControl("LabelType2");
            UserLevelTitle = (TextBox)FindControl("UserLevelTitle");
            UserLevelImages = (TextBox)FindControl("UserLevelImages");
            UserLevelPoint = (TextBox)FindControl("UserLevelPoint");
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            DosOrg.User.User currentUser = new DosOrg.User.User();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (!currentUser.IsSystemAdministrator && !currentUser.IsMembershipAdministrator)
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
            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            Components.Components.UserLevel IUserLevel = new Components.Components.UserLevel();

            //DataProviders.UserLevelDataProviders userLevel = new DataProviders.UserLevelDataProviders();
            if (IDoNetBbs.GetQueryInt("deleteUserLevelID") != 0)
            {
                MyUser.DeleteUserLevel(IDoNetBbs.GetQueryInt("deleteUserLevelID"));
                Components.CsCache.Clear();
                HttpContext.Current.Response.Write("<script>parent.window.location.reload();window.location='about:blank';</script>");
                Response.End();
            }
            else
            {
                if (IDoNetBbs.GetQueryInt("UserLevelID") == 0)
                {
                    LabelType1.Visible = false;
                    LabelType2.Visible = true;
                }
                else
                {
                    LabelType1.Visible = true;
                    LabelType2.Visible = false;

                    DataRow dr;
                    dr = MyUser.SetUserLevel(IDoNetBbs.GetQueryInt("UserLevelID"), false);
                    //userLevel.SetUserLevel(IDoNetBbs.GetQueryInt("UserLevelID"), false);
                    if (dr==null)
                    {
                        IDoNetBbs.WriteAlert("该等级不存在", false);
                        IDoNetBbs.WriteWindowClose(false);
                        Page.Response.End();
                    }
                    IUserLevel.SetDataProviders(dr);
                    userLevelID.Text = IUserLevel.UserLevelID.ToString();
                    UserLevelTitle.Text = IUserLevel.UserLevelTitle.ToString();
                    UserLevelImages.Text = IUserLevel.UserLevelImages.ToString();
                    UserLevelPoint.Text = IUserLevel.UserLevelPoint.ToString();
                }
            }
        }

        private void PostData()
        {
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            Components.Components.UserLevel IUserLevel = new Components.Components.UserLevel();

            if (LabelType1.Visible)
            {//修改 
                if (int.Parse(userLevelID.Text) == 0)
                {
                    IDoNetBbs.WriteAlert("该等级不存在", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                DataRow dr;
                dr = MyUser.SetUserLevel(IDoNetBbs.GetQueryInt("UserLevelID"), false);
                //userLevel.SetUserLevel(IDoNetBbs.GetQueryInt("UserLevelID"), false);
                if (dr == null)
                {
                    IDoNetBbs.WriteAlert("该等级不存在", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                IUserLevel.SetDataProviders(dr);
            }
            if (UserLevelTitle.Text.Trim() == string.Empty)
            {
                IDoNetBbs.WriteAlert("请输入等级名称", false);
                IDoNetBbs.WriteFocus("UserLevelTitle", false);
                Page.Response.End();
            }

            IUserLevel.UserLevelTitle = UserLevelTitle.Text;
            IUserLevel.UserLevelImages = UserLevelImages.Text;
            if (UserLevelPoint.Text.Trim() != string.Empty)
            {
                IUserLevel.UserLevelPoint = int.Parse(UserLevelPoint.Text);
            }
            else
            {
                IUserLevel.UserLevelPoint = 0;
            }
            if (LabelType2.Visible)
            {//增加 
                MyUser.InsertUserLevel(IUserLevel);
            }
            else
            {//修改 
                MyUser.UpdateUserLevel(IUserLevel);
            }
            Components.CsCache.Clear();
            HttpContext.Current.Response.Write("<script>alert('操作成功!');dialogArguments.window.location.reload();window.close();</script>");
            Response.End();
        }
    }
}
