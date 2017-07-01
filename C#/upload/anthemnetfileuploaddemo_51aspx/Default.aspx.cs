using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);
    }
    protected void defaultUploadButton_Click(object sender, EventArgs e)
    {
        defaultResultLabel.Text = string.Format("File \"{0}\" uploaded ({1} bytes).",
            defaultFileUpload.FileName,
            defaultFileUpload.FileBytes.Length
        );
    }
    protected void anthemUploadButton_Click(object sender, EventArgs e)
    {
        anthemResultLabel.Text = string.Format("File \"{0}\" uploaded ({1} bytes).",
            anthemFileUpload.FileName,
            anthemFileUpload.FileBytes.Length
        );
        anthemResultLabel.UpdateAfterCallBack = true;
    }
}
