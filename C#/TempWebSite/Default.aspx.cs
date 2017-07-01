using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using MyClass.FileOpeation;

public partial class DefaultDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageLoad();
       

    }


    private void PageLoad()
    {


        string headerHTML = GetHeaderHTML();
        string leftHTML = GetLeftHTML();
        string footHTML = GetFootHTML();

        Hashtable h_link = new Hashtable();
        string indexLinkHTML = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateIndexLink, h_link);
        //html输出
        Hashtable h_index = new Hashtable();
        h_index.Add("util_head.html", headerHTML);
        h_index.Add("util_left.html", leftHTML);
        h_index.Add("util_foot.html", footHTML);
        string indexHTML = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateIndex, h_index);
        L_Body.Text = indexHTML;
        L_Link.Text = indexLinkHTML;
    }

    private string GetHeaderHTML()
    {
        Hashtable h_index = new Hashtable();
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateIndex, h_index);
        return html;
    }
    private string GetLeftHTML()
    {
        Hashtable h_index = new Hashtable();
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateLeft, h_index);
        return html;
    }
    private string GetRightHTML()
    {
        Hashtable h_index = new Hashtable();
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateRight, h_index);
        return html;
    }
    private string GetFootHTML()
    {
        Hashtable h_index = new Hashtable();
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateFoot, h_index);
        return html;
    }
    private string GetCenterHTML()
    {
        Hashtable h_index = new Hashtable();
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateCenter, h_index);
        return html;
    }
    
}