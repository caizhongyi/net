using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///BasePage 的摘要说明
/// </summary>
public class T_BasePage 
{
  private static string connectString = System.Configuration.ConfigurationManager.AppSettings["constr"].ToString();
    private static string templateRoot = System.Configuration.ConfigurationManager.AppSettings["tempUrl_root"].ToString();
    private static string templatePhysicsRoot;
    private static string templateLink = System.Configuration.ConfigurationManager.AppSettings["tempUrl_link"].ToString();
    private static string templateIndex = System.Configuration.ConfigurationManager.AppSettings["tempUrl_index"].ToString();
    private static string templateLeft = System.Configuration.ConfigurationManager.AppSettings["tempUrl_uitlLeft"].ToString();
    private static string templateHead = System.Configuration.ConfigurationManager.AppSettings["tempUrl_utilHead"].ToString();
    private static string templateFoot = System.Configuration.ConfigurationManager.AppSettings["tempUrl_utilFoot"].ToString();
    private static string templateIndexLink = System.Configuration.ConfigurationManager.AppSettings["tempUrl_indexLink"].ToString();

   public static string ConnectString
    {
        get { return T_BasePage.connectString; }
    }
    public static string TemplateRoot
    {
        get { return T_BasePage.templateRoot; }
    }
  
    public static string TemplatePhysicsRoot
    {
        get { return AppDomain.CurrentDomain.BaseDirectory + T_BasePage.templateRoot; }
    }
  
    public static string TemplateLink
    {
        get { return T_BasePage.templateLink; }
    }

    public static string TemplateIndex
    {
        get { return T_BasePage.templateIndex; }
    }

    public static string TemplateLeft
    {
        get { return T_BasePage.templateLeft; }
    }
   
    public static string TemplateHead
    {
        get { return T_BasePage.templateHead; }
    }
 
    public static string TemplateFoot
    {
        get { return T_BasePage.templateFoot; }
    }
 
    public static string TemplateIndexLink
    {
        get { return T_BasePage.templateIndexLink; }
    }

    T_AjaxMethods ajaxMethods = new T_AjaxMethods();
    public T_BasePage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

 
}