using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///BasePage 的摘要说明
/// </summary>
public class T_BasePage 
{

    
    #region 属性
    public static MyDAL.IDataBase DB
    {
        get { return new MyDAL.SQLDataBase(T_BasePage.ConnectString, MyDAL.Enumeration.ConnStringType.String); }
    }
    /// <summary>
    /// 链接字符窜
    /// </summary>
    public static string ConnectString
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["constr"].ToString(); }
    }
    /// <summary>
    /// 模版根目录
    /// </summary>
    public static string TemplateRoot
    {
        get { return  System.Configuration.ConfigurationManager.AppSettings["tp_root"].ToString(); }
    }
    /// <summary>
    ///  模版物理根目录
    /// </summary>
    public static string TemplatePhysicsRoot
    {
        get { return AppDomain.CurrentDomain.BaseDirectory + System.Configuration.ConfigurationManager.AppSettings["tp_root"].ToString(); }
    }
    /// <summary>
    /// 通用链接
    /// </summary>
    public static string TemplateLink
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_link"].ToString(); }
    }
    /// <summary>
    /// 首页模版
    /// </summary>
    public static string TemplateIndex
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_index"].ToString();}
    }
       /// <summary>
   ///首页链接
   /// </summary>
    public static string TemplateIndexLink
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_indexLink"].ToString();}
    }


    /// <summary>
    /// 左模版
    /// </summary>
    public static string TemplateLeft
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_uLeft"].ToString(); }
    }
    /// <summary>
    /// 右模版
    /// </summary>
    public static string TemplateRight
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_uRight"].ToString(); }
    }
    /// <summary>
    /// 中间模版
    /// </summary>
    public static string TemplateCenter
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_uCenter"].ToString(); }
    }
    /// <summary>
    /// 头部模版
    /// </summary>
    public static string TemplateHead
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_uHead"].ToString();}
    }
    /// <summary>
    /// 尾部模版
    /// </summary>
    public static string TemplateFoot
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_uFoot"].ToString();}
    }


    public static string TemplateNews
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_news"].ToString();}
    }
    public static string TemplateAbout
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_about"].ToString();}
    }
    public static string TemplateContact
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_contact"].ToString();}
    }
    public static string TemplateSubAbout
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_subAbout"].ToString();}
    }
    public static string TemplateEmployee
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_employee"].ToString(); }
    }
    public static string TemplateMain
    {
        get { return System.Configuration.ConfigurationManager.AppSettings["tp_main"].ToString(); }
    }
    #endregion
    public T_BasePage()
	{
		//
		//TODO: 在此处添加构造函数逻辑
		//
	}

 
}