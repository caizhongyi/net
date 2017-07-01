using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class admin_Main : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        AjaxPro.Utility.RegisterTypeForAjax(typeof(T_AdminAjaxMethods));
        PageLoad();

    }

    private void PageLoad()
    {
        string footHTML = GetFootHTML();
        string topHTML = GetTopHTML();
        string leftHTML = GetLeftHTML();
        string rightHTML = GetRightHTML();

        Hashtable h_link = new Hashtable();
        h_link.Add("root", T_AdminBasePage.TemplateRoot.Substring(T_AdminBasePage.TemplateRoot.IndexOf('/')+1));
        string indexLinkHTML = MyClass.UI.HTMLTempletReader.GetNewHTML(T_AdminBasePage.TemplatePhysicsRoot + T_AdminBasePage.TemplateLink, h_link);
        //html输出
        Hashtable h_index = new Hashtable();
        h_index.Add("util_head.html", topHTML);
        h_index.Add("util_left.html", leftHTML);
        h_index.Add("util_right.html", rightHTML);
        h_index.Add("util_foot.html", footHTML);
        string indexHTML = MyClass.UI.HTMLTempletReader.GetNewHTML(T_AdminBasePage.TemplatePhysicsRoot + T_AdminBasePage.TemplateIndex, h_index);
        L_Body.Text = indexHTML;
        L_Link.Text = indexLinkHTML;
    }

  
    private string GetTopHTML()
    {
        Hashtable h_link = new Hashtable();
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_AdminBasePage.TemplatePhysicsRoot + T_AdminBasePage.TemplateHead, h_link);
        return html;
    }
    private string GetLeftHTML()
    {
        Hashtable h_link = new Hashtable();
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_AdminBasePage.TemplatePhysicsRoot + T_AdminBasePage.TemplateLeft, h_link);
        return html;
    }
    private string GetRightHTML()
    {
        Hashtable h_link = new Hashtable();
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_AdminBasePage.TemplatePhysicsRoot + T_AdminBasePage.TemplateRight, h_link);
        return html;
    }


    private string GetFootHTML()
    {
        Hashtable h_link = new Hashtable();
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_AdminBasePage.TemplatePhysicsRoot + T_AdminBasePage.TemplateFoot, h_link);
        return html;
    }
}
