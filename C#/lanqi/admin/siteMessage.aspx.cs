using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Model;
public partial class admin_siteMessage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SiteInfo s = new SiteInfo();
            s.Id = 1;
            fun.getModel(s);
            txtKey.Value = s.Keywords;
            txtDes.Value = s.Description;
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        SiteInfo s = new SiteInfo();
        s.Id = 1;
        s.Keywords = txtKey.Value;
        s.Description = txtDes.Value;
        fun.DoSql(this,fun.update(s),Request.Url.ToString());
    }
}
