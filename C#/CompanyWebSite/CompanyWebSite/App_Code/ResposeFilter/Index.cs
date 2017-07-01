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
///index 的摘要说明
/// </summary>
public class Index
{

    public Index()
    {
        //
        //TODO: 在此处添加构造函数逻辑
        //
    }

    public string GetIndexHTML()
    {
        string headerHTML = GetHeaderHTML();
        string leftHTML = GetLeftHTML();
        string footHTML = GetFootHTML();

        Hashtable hs_index = new Hashtable();
        hs_index.Add("util_head.html", headerHTML);
        hs_index.Add("util_left.html", leftHTML);
        hs_index.Add("util_foot.html", footHTML);
        return MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateIndex, hs_index);
       
    }

    private string GetHeaderHTML()
    {
        string menuHTML = string.Empty;
        menuHTML += "<li><a href='default.aspx'></a></li>";
        menuHTML += "<li><a href='default.aspx'></a></li>";
        menuHTML += "<li><a href='default.aspx'></a></li>";
        menuHTML += "<li><a href='default.aspx'></a></li>";
        Hashtable h_header = new Hashtable();
        h_header.Add("menu", menuHTML);
        h_header.Add("headerContent", "headerContent");
        
        return MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateHead, h_header);
    }
    private string GetLeftHTML()
    {
        Hashtable h_left = new Hashtable();
        h_left.Add ("lefter","lefter");
        string leftHTML = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateLeft, h_left);
        return leftHTML;
    }
    private string GetFootHTML()
    {
        Hashtable h_footer = new Hashtable();
        h_footer.Add("footer", "footer");
        string footHTML = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateFoot, h_footer);
        return footHTML;

    }
}
