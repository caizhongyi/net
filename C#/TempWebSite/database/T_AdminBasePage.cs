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
public class T_AdminBasePage : System.Web.UI.Page
{


    static string templateRoot = System.Configuration.ConfigurationManager.AppSettings["tempUrl_adminRoot"].ToString();
    static string templatePhysicsRoot = AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.AppSettings["tempUrl_adminRoot"].ToString();
    static string templateLink = System.Configuration.ConfigurationManager.AppSettings["tempUrl_adminLink"].ToString();
    static string templateIndex = System.Configuration.ConfigurationManager.AppSettings["tempUrl_adminIndex"].ToString();
    static string templateLeft = System.Configuration.ConfigurationManager.AppSettings["tempUrl_adminUtilLeft"].ToString();
    static string templateHead = System.Configuration.ConfigurationManager.AppSettings["tempUrl_adminUtilHead"].ToString();
    static string templateFoot = System.Configuration.ConfigurationManager.AppSettings["tempUrl_adminUtilFoot"].ToString();
    static string templateLogin = System.Configuration.ConfigurationManager.AppSettings["tempUrl_adminLogin"].ToString();
    static string templateLoginLink = System.Configuration.ConfigurationManager.AppSettings["tempUrl_adminLoginLink"].ToString();



    public static string TemplateRoot
    {
        get { return T_AdminBasePage.templateRoot; }
    }
    public static string TemplatePhysicsRoot
    {
        get { return T_AdminBasePage.templatePhysicsRoot; }
    }
    public static string TemplateLink
    {
        get { return T_AdminBasePage.templateLink; }
    }
    public static string TemplateIndex
    {
        get { return T_AdminBasePage.templateIndex; }
    }
    public static string TemplateLeft
    {
        get { return T_AdminBasePage.templateLeft; }
    }
    public static string TemplateHead
    {
        get { return T_AdminBasePage.templateHead; }
    }
    public static string TemplateFoot
    {
        get { return T_AdminBasePage.templateFoot; }
    }
    public static string TemplateLogin
    {
        get { return T_AdminBasePage.templateLogin; }
    }
    public static string TemplateLoginLink
    {
        get { return T_AdminBasePage.templateLoginLink; }
    }
   

    T_AdminAjaxMethods ajaxMethods = new T_AdminAjaxMethods();
    public T_AdminBasePage()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
}
