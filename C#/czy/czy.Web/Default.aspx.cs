using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;


public partial class DefaultDemo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        PageLoad();

    }

    private void PageLoad()
    {
        string html = czy.MyClass.Web.UI.TemplateConvertor.StreamReadHTML(Template.IndexTemplate);
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceTemplate(Template.LinkTemplate, "Link", html, czy.MyClass.Web.UI.TemplateConvertor.TemplateType.Template);
      
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceTemplate(Template.HeadTemplate, "Head", html, czy.MyClass.Web.UI.TemplateConvertor.TemplateType.Template);
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceTemplate(Template.BodyTemplate, "Body", html, czy.MyClass.Web.UI.TemplateConvertor.TemplateType.Template);
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceTemplate(Template.FootTemplate, "Foot", html, czy.MyClass.Web.UI.TemplateConvertor.TemplateType.Template);

        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "URL", Template.LinkUrl);

        html=PageBase.GetProductList(html);
       
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Example", PageBase.GetExampleList(5));
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Culture", PageBase.GetCultureList(5));
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "Contact", PageBase.GetContact());
        html = czy.MyClass.Web.UI.TemplateConvertor.ReplaceData(html, "About", PageBase.GetAbout().ToViewLength(250));
        Response.Write(html);
    }

 


    
}