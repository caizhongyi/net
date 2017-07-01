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

public partial class admin_Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(T_AdminAjaxMethods));
        PageLoad();
        
        
     
    }

    private void PageLoad()
    {
        Hashtable h_link = new Hashtable();
        string loginLinkHTML = MyClass.UI.HTMLTempletReader.GetNewHTML(T_AdminBasePage.TemplatePhysicsRoot + T_AdminBasePage.TemplateLoginLink, h_link);
        //html输出
        Hashtable h_login = new Hashtable();
        string loginHTML = MyClass.UI.HTMLTempletReader.GetNewHTML(T_AdminBasePage.TemplatePhysicsRoot + T_AdminBasePage.TemplateLogin, h_login);
        L_Body.Text = loginHTML;
        L_Link.Text = loginLinkHTML;
    }

}
