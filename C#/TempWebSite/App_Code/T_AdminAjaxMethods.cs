using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections;

/// <summary>
///AdminAjaxMethods 的摘要说明
/// </summary>
public  class T_AdminAjaxMethods 
{
    MyClass.User.Login.UserInfo userInfo = new MyClass.User.Login.UserInfo();
    Index index = new Index();
   
    public enum NavParam
    {
        index,
        news,
        type,
        user,
        login
    }


    public T_AdminAjaxMethods()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
     
       
    }
    [AjaxPro.AjaxMethod]
    public int CheckUser(string user, string pwd)
    {
        userInfo.UserName = user;
        userInfo.Pwd = pwd;
        MyClass.User.Login.ILogin login = new MyClass.User.Login.SqlLogin(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
        bool isLogin = login.Login(userInfo, "UserInfo", "u_name", "u_pwd", "");

        switch (login.LoginResoult)
        {
            case MyClass.User.Login.BaseLogin.LoginState.NotExistUser: return 1; 
            case MyClass.User.Login.BaseLogin.LoginState.PasswordError: return 2; 
            case MyClass.User.Login.BaseLogin.LoginState.Sucess: return 0; 
            default: return -1;
        }
    }
    [AjaxPro.AjaxMethod]
    public string ChangeNavItem(string pageName)
    {
        string url = string.Empty;
        switch (pageName)
        {
            case "news.html": url = T_AdminBasePage.TemplateNews; break;
            case "user.html": url = T_AdminBasePage.TemplateUser; break;
            case "type.html": url = T_AdminBasePage.TemplateType; break;
            case "roles.html": url = T_AdminBasePage.TemplateRoles; break;
            case "news_update.html": url = T_AdminBasePage.TemplateNewsUpdate; break;
            case "user_update.html": url = T_AdminBasePage.TemplateUserUpdate; break;
            case "type_update.html": url = T_AdminBasePage.TemplateTypeUpdate; break;
            case "roles_update.html": url = T_AdminBasePage.TemplateRolesUpdate; break;
        }
        Hashtable h_link = new Hashtable();
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_AdminBasePage.TemplatePhysicsRoot + url, h_link);
        return html;
    }


    [AjaxPro.AjaxMethod]
    public bool ResponseFilterIndex()
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + T_AdminBasePage.Filter_root + T_AdminBasePage.Filter_index;
        string indexHTML = index.GetIndexHTML();
        return MyClass.FileOpeation.MyStream.StreamWrite(path, System.Text.Encoding.UTF8, indexHTML);
    }
    [AjaxPro.AjaxMethod]
    public string GetCurrentNewsInfo(int cur)
    {
        int totalCount=0;
        MyClass.PageHelper.SqlPagerHelper sqlPager = new MyClass.PageHelper.SqlPagerHelper("*", "n_id", "asc", "newsinfo", "", 15, "", T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
        DataSet ds = sqlPager.GetCurrentPageData(cur, out totalCount);
        string res = ds.ToJson(totalCount);
        return res;
    }
    
}
