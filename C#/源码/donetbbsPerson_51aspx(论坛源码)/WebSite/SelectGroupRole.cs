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
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
namespace WebSite
{
    public class SelectGroupRole : System.Web.UI.Page
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
            DataBind();
        }
        public override void DataBind()
        {
            base.DataBind();

            DataProviders.DataConnectionHepler MyConnection = DataProviders.DataConnectionHepler.Instance();
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            string html = string.Empty;
            html = MyConnection.GetTempXmlNode("Resource_WebSiteSelectGroupRole");
            html = IDoNetBbs.GetFormat(html, "WebSiteGroupListValue", IDoNetBbs.GetQueryString("Value"));
            html = IDoNetBbs.GetFormat(html, "WebSiteGroupListName", IDoNetBbs.GetQueryString("Name"));
            if (IDoNetBbs.GetQueryInt("type") == 0)
            {
                DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
                //DataProviders.UserRoleDataProviders userRole = new DataProviders.UserRoleDataProviders();
                Components.Components.UserRole IUserRole = new Components.Components.UserRole();
                IUserRole.Arraylist = MyUser.SetUserRole(true);
                foreach (Components.Components.UserRole rs in IUserRole.Arraylist)
                {
                    if (rs.UserRoleFalse == 0)
                    {
                        html = IDoNetBbs.GetFormatRepeat(html, "WebSiteSlectList", MyConnection.GetTempXmlNode("Resource_WebSiteOptionList"));
                        html = IDoNetBbs.GetFormat(html, "OptionSelected", null);
                        html = IDoNetBbs.GetFormat(html, "OptionVolue", rs.UserRoleID.ToString());
                        html = IDoNetBbs.GetFormat(html, "OptionTitle", rs.UserRoleTitle);
                    }
                }
                html = IDoNetBbs.GetFormat(html, "WebSiteSlectList", null);
                html = IDoNetBbs.GetFormat(html, "WebSiteSelectGroupRoleTitle", MyConnection.GetResourcesXmlNode("Resource_WebSiteSelectGroupRoleTitle"));

            }
            else if (IDoNetBbs.GetQueryInt("type") == 1)
            {
                //DataProviders.UserGroupDataProviders userGroup = new DataProviders.UserGroupDataProviders();
                DataProviders.UserDataProvider MyUser = DataProviders.UserDataProvider.Instance();
                Components.Components.UserGroup IUserGroup = new Components.Components.UserGroup();
                IUserGroup.Arraylist = MyUser.SetUserGroup(true);
                //userGroup.SetAllUserGroup(0, true);
                foreach (Components.Components.UserGroup rs in IUserGroup.Arraylist)
                {
                    if (rs.UserGroupFalse == 0)
                    {
                        html = IDoNetBbs.GetFormatRepeat(html, "WebSiteSlectList", MyConnection.GetTempXmlNode("Resource_WebSiteOptionList"));
                        html = IDoNetBbs.GetFormat(html, "OptionSelected", null);
                        html = IDoNetBbs.GetFormat(html, "OptionVolue", rs.UserGroupID.ToString());
                        html = IDoNetBbs.GetFormat(html, "OptionTitle", rs.UserGroupTitle);
                    }
                }
                html = IDoNetBbs.GetFormat(html, "WebSiteSlectList", null);
                html = IDoNetBbs.GetFormat(html, "WebSiteSelectGroupRoleTitle", MyConnection.GetResourcesXmlNode("Resource_WebSiteSelectGroupTitle"));

            }
            else
            {
                DataRow dr = MyConnection.GetTreeXmlRow("UserRole");
                if (dr != null)
                {
                    for (int i = 0; i < dr.Table.Columns.Count; i++)
                    {
                        html = IDoNetBbs.GetFormatRepeat(html, "WebSiteSlectList", MyConnection.GetTempXmlNode("Resource_WebSiteOptionList"));
                        html = IDoNetBbs.GetFormat(html, "OptionSelected", null);
                        html = IDoNetBbs.GetFormat(html, "OptionTitle", dr[i].ToString());
                        html = IDoNetBbs.GetFormat(html, "OptionVolue", dr.Table.Columns[i].ToString());
                    }
                }
                html = IDoNetBbs.GetFormat(html, "WebSiteSlectList", null);
                html = IDoNetBbs.GetFormat(html, "WebSiteSelectGroupRoleTitle", MyConnection.GetResourcesXmlNode("Resource_WebSiteSelectRoleTitle"));

            }
            HttpContext.Current.Response.Write(html);
            Response.End();
        }
    }
}
