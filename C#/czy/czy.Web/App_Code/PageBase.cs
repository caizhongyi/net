using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
///PageBase 的摘要说明
/// </summary>
public class PageBase
{
    public PageBase()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }
    static int pageSize = 10;

    public static void  AuthenticationLogin()
    {
    }
    public static string GetProductList(string html)
    {
        List<Models.NewsInfo> titles = BBL.NewsInfo.SelectList("top 10 n_id,n_title", string.Format("n_newsTypeId={0}", 2));
        string LeftHTML = string.Empty;
        foreach (Models.NewsInfo n in titles)
        {
            LeftHTML += "<a href=\"product.aspx?id=" + n.n_id + "\"  target=\"_self\" class=\"productclass_dolphin\">" + n.n_title + "</a>";
        }
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "ProductTitleList", LeftHTML);
        return html;
    }
    private static string GetNewsHTML(List<Models.NewsInfo> list)
    {
        string titleStyle = "style='text-align:center; padding:10px;font-size:20px;color:black;font-weight:bold;'";
        string temp = string.Empty;
        if (list.Count > 0)
        {
            temp = "<div " + titleStyle + ">" + list[0].n_title + "<span style='font-size:12px; margin-left:10px' >时间:[" + list[0].n_createDate + "]</span></div>";
            temp += list[0].n_content;
        }
        return temp;
    }
    public static void SubPageLoad(string Name)
    {
       

        string html = czy.MyClass.Web.UI.TemplateConvertor.StreamReadHTML(Template.SubPageTemplate);
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceTemplate(Template.LinkTemplate, "Link", html, czy.MyClass.Web.UI.TemplateConvertor.TemplateType.Template);

        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceTemplate(Template.HeadTemplate, "Head", html, czy.MyClass.Web.UI.TemplateConvertor.TemplateType.Template);
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceTemplate(Template.SubBodyTemplate, "SubBody", html, czy.MyClass.Web.UI.TemplateConvertor.TemplateType.Template);
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceTemplate(Template.FootTemplate, "Foot", html, czy.MyClass.Web.UI.TemplateConvertor.TemplateType.Template);

        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "URL", Template.LinkUrl);
        int id = 0;
        List<Models.NewsInfo> list;
        string temp=string .Empty ;

        html= GetProductList( html);
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Contact", PageBase.GetContact());
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "About", PageBase.GetAbout());
        switch (Name)
        {
            case "About": html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Title","公司简介");
                temp = GetAbout();
                html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Content", temp);
                break;
            case "Contact": html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Title", "联系我们");
                temp = GetContact();
                html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Content", temp);
                break;
            case "Product": html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Title", "产品中心");
                //if (HttpContext.Current.Request.QueryString["type"] == "class")
                //{
                //    list = BBL.NewsInfo.SelectList(2);
                //    html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Content", list[0].n_content);
                //}
                //else
                //{
                if (HttpContext.Current.Request["id"] != null)
                {
                    id = Convert.ToInt32(HttpContext.Current.Request["id"]);
                    list = BBL.NewsInfo.SelectList(id);
                    //temp = GetList(list, Convert.ToInt32(BBL.NewsInfo.GetTotleCount(10)), "Product.aspx");
                    temp = GetNewsHTML(list);
                    html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Content", temp);
                    //}
                }
                else
                {
                    list = BBL.NewsInfo.SelectList("*", string.Format("n_newsTypeId={0}", 2));
                    temp = GetNewsHTML(list);
                    html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Content", temp);
                }
                break;
            case "Culture": html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Title", "企业文化");
                if (HttpContext.Current.Request.QueryString["type"] == "cls")
                {
                  //  id = Convert.ToInt32(HttpContext.Current.Request["id"]);
                    temp=GetCultureList();
                    html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Content", temp);
                }
                else
                {
                    id = Convert.ToInt32(HttpContext.Current.Request["id"]);
                    list = BBL.NewsInfo.SelectList(id);
                   
                   // temp = GetList(list, Convert.ToInt32(BBL.NewsInfo.GetTotleCount(10)), "Culture.aspx");
                    temp = GetNewsHTML(list);
                    html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Content", temp);
                }
                break;
            case "Example": html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Title", "方案与案例");
                if (HttpContext.Current.Request.QueryString["type"] == "cls")
                {
                   // id = Convert.ToInt32(HttpContext.Current.Request["id"]);
                    temp=GetExampleList();
                    html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Content", temp);
                }
                else
                {
                    id = Convert.ToInt32(HttpContext.Current.Request["id"]);
                    list = BBL.NewsInfo.SelectList(id);
                    //temp = GetList(list, Convert.ToInt32(BBL.NewsInfo.GetTotleCount(10)), "Example.aspx");
                    temp = GetNewsHTML(list);
                    html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Content", temp);
                }
                break;
         
        }
        HttpContext.Current.Response.Write(html);
    }


    private static string GetList(List<Models.NewsInfo> list, int count, string pageName,int cur)
    {
        string html = string.Empty;
        czy.MyClass.Web.Controls.DataPager dp = new czy.MyClass.Web.Controls.DataPager(count);
        dp.CurrentPage = cur;
        html += "<ul style='list-style-type:none; '>";
        foreach (Models.NewsInfo n in list)
        {
            html += string.Format("<li style='line-height:20px;clear:both'><div style='float:left;text-align:left;'><a href='{1}{2}' >{0}</a></div><div style='float:right;width:100px;'>{3}</div></li>", n.n_title, pageName, "?id=" + n.n_id, n.n_createDate.ToString("yyyy年MM月dd日"));
        }
        html += "</ul>";
        html += dp.GetHTMLPager(pageName, "type=cls");
        return html;
    }

    public static string GetAbout()
    {
        string temp = string.Empty;
        List<Models.NewsInfo> list=BBL.NewsInfo.SelectList("*", string.Format("n_newsTypeId={0}", 11));
        if (list.Count > 0) temp = list[0].n_content;
        return temp;
    }
    public static string GetContact()
    {
        string temp = string.Empty;
        List<Models.NewsInfo> list = BBL.NewsInfo.SelectList("*", string.Format("n_newsTypeId={0}", 6));
        if (list.Count > 0) temp = list[0].n_content;
        return temp;
    }

    public static string GetCultureList()
    {
        int page=1;
        if (HttpContext.Current.Request.QueryString["page"] != null)
        {
            page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
        }
        string temp = string.Empty;
        List<Models.NewsInfo> list = BBL.NewsInfo.GetListCurrentData(page,pageSize,"n_id","asc","*",string.Format("n_newsTypeId={0}",10));
        if (list.Count > 0) temp = list[0].n_content;
        temp = GetList(list, Convert.ToInt32(BBL.NewsInfo.GetTotleCount(pageSize, string.Format("n_newsTypeId={0}", Menu.Culture.ToString()))), "Culture.aspx", page);
        return temp;
   
    }
    public static string GetCultureList(int top)
    {
        string html = string.Empty;
        czy.MyClass.Web.Controls.DataPager dp = new czy.MyClass.Web.Controls.DataPager(Convert.ToInt32(BBL.NewsInfo.GetTotleCount(pageSize)));
        List<Models.NewsInfo> list = BBL.NewsInfo.SelectList(" top " + top + "*", string.Format("n_newsTypeId={0}", 10));
        foreach (Models.NewsInfo n in list)
        {
            html += string.Format("<li  class=\"newslist\" style='clear:both'><div style='float:left;text-align:left;'><a href='{1}{2}' target=\"_self\" class=\"newslist\">{0}</a></div></li>", n.n_title, "Culture.aspx", "?id=" + n.n_id);
           // html += string.Format("<li  class=\"newslist\" style='clear:both'><div style='float:left;text-align:left;'><a href='{1}{2}' target=\"_self\" class=\"newslist\">{0}</a></div><div style='float:right;width:100px;font-size:11px;'>{3}</div></li>", n.n_title, "Culture.aspx", "?id=" + n.n_id, n.n_createDate.ToString("yyyy年MM月dd日"));
        }
        return html;

    }
    public static string GetExampleList()
    {
        int page = 1;
        if (HttpContext.Current.Request.QueryString["page"] != null)
        {
            page = Convert.ToInt32(HttpContext.Current.Request.QueryString["page"]);
        }
        string temp = string.Empty;
        List<Models.NewsInfo> list = BBL.NewsInfo.GetListCurrentData(page, pageSize, "n_id", "asc", "*", string.Format("n_newsTypeId={0}", 12));
        temp = GetList(list, Convert.ToInt32(BBL.NewsInfo.GetTotleCount(pageSize, string.Format("n_newsTypeId={0}", Menu.Example.ToString()))), "Example.aspx", page);
        return temp;
    }
    public static string GetExampleList(int top)
    {
        string html = string.Empty;
        czy.MyClass.Web.Controls.DataPager dp = new czy.MyClass.Web.Controls.DataPager(Convert.ToInt32(BBL.NewsInfo.GetTotleCount(pageSize)));
        List<Models.NewsInfo> list = BBL.NewsInfo.SelectList(" top " + top + " *", string.Format("n_newsTypeId={0}", 12));
        foreach (Models.NewsInfo n in list)
        {
           // html += string.Format("<li  class=\"newslist\"><div style='float:left;text-align:left;'><a href='{1}{2}' target=\"_self\" class=\"newslist\">{0}</a></div><div style='float:right;width:100px;font-size:11px;'>{3}</div></li>", n.n_title, "Example.aspx", "?id=" + n.n_id, n.n_createDate.ToString("yyyy年MM月dd日"));
            html += string.Format("<li  class=\"newslist\"><div style='float:left;text-align:left;'><a href='{1}{2}' target=\"_self\" class=\"newslist\">{0}</a></div></li>", n.n_title, "Example.aspx", "?id=" + n.n_id);
        }
        return html;
    }

    public static void  CheckLogin()
    {
        czy.SQLCommon.Login.ILogin iLogin = czy.IFactory.Factory.GetLogin();
        if (iLogin.IsLogin)
        {
            HttpContext.Current.Response.Redirect("/admin/index.aspx");
        }
        else
        {
            HttpContext.Current.Response.Redirect("/admin/Login.aspx");
        }
    }


    public static void SetFCKEditer(ref FredCK.FCKeditorV2.FCKeditor editor)
    {

    }

 

    public struct Menu
    {
        public const int Default = 0;
        public const int About = 11;
        public const int Example = 12;
        public const int Culture = 10;
        public const int Contact = 6;
        public const int Product = 2;
    }
}