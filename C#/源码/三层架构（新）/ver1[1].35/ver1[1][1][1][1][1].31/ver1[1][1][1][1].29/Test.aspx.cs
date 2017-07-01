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

public partial class Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string path = Server.MapPath("~/Upload/Pic/");



        for (int i = 0; i < Request.Files.Count; i++)
        {
            HttpPostedFile file = Request.Files[i];
            if (file.FileName != "")
            {
                string fileName = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace("-", "");
                fileName += System.IO.Path.GetExtension(file.FileName).ToLower();
                file.SaveAs(path + fileName);
                for (int j = 0; j < 100000000; j++) { }
            }
            
        }
        
    }
  
}
