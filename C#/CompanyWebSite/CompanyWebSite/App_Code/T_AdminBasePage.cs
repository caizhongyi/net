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

/// <summary>
///AdminBasePage 的摘要说明
/// </summary>
public class T_AdminBasePage 
{
    #region 属性
    /// <summary>
    /// 模版根目录
    /// </summary>
    public static string TemplateRoot{ get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminRoot"].ToString();} }
    /// <summary>
    /// 模版物理根目录
    /// </summary>
    public static string TemplatePhysicsRoot{ get { return AppDomain.CurrentDomain.BaseDirectory +  System.Configuration.ConfigurationManager.AppSettings["tp_adminRoot"].ToString(); } }
    /// <summary>
    /// 链接页面
    /// </summary>
    public static string TemplateLink
    {
        get { return  System.Configuration.ConfigurationManager.AppSettings["tp_adminLink"].ToString(); }
    }
    /// <summary>
    /// 主页
    /// </summary>
    public static string TemplateIndex
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminIndex"].ToString(); }
    }
    public static string TemplateLeft
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminULeft"].ToString();}
    }
    public static string TemplateRight
    {
        get { return  System.Configuration.ConfigurationManager.AppSettings["tp_adminURight"].ToString();}
    }
    public static string TemplateCenter
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminUCenter"].ToString();}
    }
    public static string TemplateHead
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminUHead"].ToString();}
    }
    public static string TemplateFoot
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminUFoot"].ToString(); }
    }
    public static string TemplateLogin
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminLogin"].ToString();}
    }
    public static string TemplateLoginLink
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminLoginLink"].ToString();}
    }
    public static string TemplateUser
    {
        get { return  System.Configuration.ConfigurationManager.AppSettings["tp_adminUser"].ToString();}
    }
    public static string TemplateRoles
    {
        get { return  System.Configuration.ConfigurationManager.AppSettings["tp_adminRoles"].ToString();}
    }
    public static string TemplateNews
    {
        get { return  System.Configuration.ConfigurationManager.AppSettings["tp_adminNews"].ToString(); }
    }
    public static string TemplateType
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminType"].ToString(); }
    }
    public static string TemplateUserUpdate
    {
        get { return  System.Configuration.ConfigurationManager.AppSettings["tp_adminUserUpdate"].ToString(); }
    }
    public static string TemplateRolesUpdate
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminRolesUpdate"].ToString();}
    }
    public static string TemplateNewsUpdate
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminNewsUpdate"].ToString(); }
    }
    public static string TemplateTypeUpdate
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_adminTypeUpdate"].ToString(); }
    }
    #endregion

    public T_AdminBasePage()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public static void AuthenticationLogin(Page page)
    {
        MyClass.User.Login.ILogin l = new MyClass.User.Login.SqlLogin(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
        if (!l.IsLogin)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "", "<script>this.parent.window.open('../Login.aspx','_self');</script>");
        }
        else
        {
            //  IsAdmin(page, right);
        }
    }
    public static void AuthenticationLogin(Page page, string url)
    {
        MyClass.User.Login.ILogin l = new MyClass.User.Login.SqlLogin(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String);
        if (!l.IsLogin)
        {
            page.ClientScript.RegisterClientScriptBlock(page.GetType(), "", "<script>this.parent.window.open('" + url + "','_self');</script>");
        }
        else
        {
            //  IsAdmin(page, right);
        }
    }
}
