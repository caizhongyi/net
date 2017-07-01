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
    public class UserRole : System.Web.UI.Page
    {
        override protected void OnInit(EventArgs e)
        {
            CreateChildControls();
        }
        Label LabelType1;
        Label LabelType2;
        Label userRoleID;
        TextBox UserRoleTitle;
        TextBox UserRolePostPoint;
        TextBox UserRoleRePostPoint;
        RadioButtonList UserRoleFalse;
        TextBox UserRolePostMoney;
        TextBox UserRolePostExp;
        TextBox UserRolePostCP;
        TextBox UserRoleRePostMoney;
        TextBox UserRoleRePostExp;
        TextBox UserRoleRePostCP;
        TextBox UserRoles;
        protected override void CreateChildControls()
        {
            base.CreateChildControls();
            userRoleID = (Label)FindControl("userRoleID");
            LabelType1 = (Label)FindControl("LabelType1");
            LabelType2 = (Label)FindControl("LabelType2");
            UserRoleTitle = (TextBox)FindControl("UserRoleTitle");
            UserRolePostPoint = (TextBox)FindControl("UserRolePostPoint");
            UserRoleRePostPoint = (TextBox)FindControl("UserRoleRePostPoint");
            UserRoleFalse = (RadioButtonList)FindControl("UserRoleFalse");
            UserRolePostMoney = (TextBox)FindControl("UserRolePostMoney");
            UserRolePostExp = (TextBox)FindControl("UserRolePostExp");
            UserRolePostCP = (TextBox)FindControl("UserRolePostCP");
            UserRoleRePostMoney = (TextBox)FindControl("UserRoleRePostMoney");
            UserRoleRePostExp = (TextBox)FindControl("UserRoleRePostExp");
            UserRoleRePostCP = (TextBox)FindControl("UserRoleRePostCP");
            UserRoles = (TextBox)FindControl("UserRoles");
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
            Components.Components.UserRole IUserRole = new Components.Components.UserRole();

            if (IDoNetBbs.GetQueryInt("deleteUserRoleID") != 0)
            {
                MyUser.DeleteUserRole(IDoNetBbs.GetQueryInt("deleteUserRoleID"));
                Components.CsCache.Clear();
                HttpContext.Current.Response.Write("<script>parent.window.location.reload();window.location='about:blank';</script>");
                Response.End();
            }
            else
            {
                if (IDoNetBbs.GetQueryInt("UserRoleID") == 0)
                {
                    LabelType1.Visible = false;
                    LabelType2.Visible = true;
                    UserRoles.Text = "Registered";
                }
                else
                {
                    LabelType1.Visible = true;
                    LabelType2.Visible = false;
                    DataRow dr;
                    dr = MyUser.SetUserRole(IDoNetBbs.GetQueryInt("UserRoleID"), false);
                    //userRole.SetUserRole(IDoNetBbs.GetQueryInt("UserRoleID"), false);
                    if (dr==null)
                    {
                        IDoNetBbs.WriteAlert("该角色不存在", false);
                        IDoNetBbs.WriteWindowClose(false);
                        Page.Response.End();
                    }
                    IUserRole.SetDataProviders(dr);

                    userRoleID.Text = IUserRole.UserRoleID.ToString();

                    UserRoleTitle.Text = IUserRole.UserRoleTitle.ToString();
                    UserRolePostPoint.Text = IUserRole.UserRolePostPoint.ToString();
                    UserRoleRePostPoint.Text = IUserRole.UserRoleRePostPoint.ToString();
                    UserRoleFalse.Items.FindByValue(IUserRole.UserRoleFalse.ToString()).Selected = true;
                    UserRolePostMoney.Text = IUserRole.UserRolePostMoney.ToString();
                    UserRolePostExp.Text = IUserRole.UserRolePostExp.ToString();
                    UserRolePostCP.Text = IUserRole.UserRolePostCP.ToString();
                    UserRoleRePostMoney.Text = IUserRole.UserRoleRePostMoney.ToString();
                    UserRoleRePostExp.Text = IUserRole.UserRoleRePostExp.ToString();
                    UserRoleRePostCP.Text = IUserRole.UserRoleRePostCP.ToString();
                    UserRoles.Text = IUserRole.UserRoles.ToString();
                }
            }
        }

        private void PostData()
        {
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
            Components.Components.UserRole IUserRole = new Components.Components.UserRole();
            if (LabelType1.Visible)
            {//修改 
                if (int.Parse(userRoleID.Text) == 0)
                {
                    IDoNetBbs.WriteAlert("该角色不存在", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                DataRow dr;
                dr = MyUser.SetUserRole(int.Parse(userRoleID.Text), false);
                if (dr==null)
                {
                    IDoNetBbs.WriteAlert("该角色不存在", false);
                    IDoNetBbs.WriteWindowClose(false);
                    Page.Response.End();
                }
                IUserRole.SetDataProviders(dr);
            }
            if (UserRoleTitle.Text.Trim() == string.Empty)
            {
                IDoNetBbs.WriteAlert("该角色不存在", false);
                IDoNetBbs.WriteFocus("UserRoleTitle", false);
                Page.Response.End();
            }
            if (UserRoles.Text.Trim() == string.Empty)
            {
                IDoNetBbs.WriteAlert("请输入角色配置", false);
                IDoNetBbs.WriteFocus("UserRoleTitle", false);
                Page.Response.End();
            }

            IUserRole.UserRoleTitle = UserRoleTitle.Text;
            IUserRole.UserRolePostPoint = int.Parse(UserRolePostPoint.Text);
            IUserRole.UserRoleRePostPoint = int.Parse(UserRoleRePostPoint.Text);
            IUserRole.UserRoleFalse = int.Parse(UserRoleFalse.SelectedValue);
            IUserRole.UserRolePostMoney = int.Parse(UserRolePostMoney.Text);
            IUserRole.UserRolePostExp = int.Parse(UserRolePostExp.Text);
            IUserRole.UserRolePostCP = int.Parse(UserRolePostCP.Text);
            IUserRole.UserRoleRePostMoney = int.Parse(UserRoleRePostMoney.Text);
            IUserRole.UserRoleRePostExp = int.Parse(UserRoleRePostExp.Text);
            IUserRole.UserRoleRePostCP = int.Parse(UserRoleRePostCP.Text);
            IUserRole.UserRoles = UserRoles.Text;
            if (LabelType2.Visible)
            {//增加 
                MyUser.InsertUserRole(IUserRole);
            }
            else
            {//修改 
                MyUser.UpadateUserRole(IUserRole);
            }
            Components.CsCache.Clear();
            HttpContext.Current.Response.Write("<script>alert('操作成功!');dialogArguments.window.location.reload();window.close();</script>");
            Response.End();
        }
    }
}
