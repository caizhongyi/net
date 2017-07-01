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
using BBL;

public partial class WbAdAddManger : System.Web.UI.Page
{
    protected string fileName;
    protected string filepath = @"~/Adpictures/";

    public void upload(string rootpath, FileUpload file, string filepathroot)
    {
        
        string path = Server.MapPath(rootpath);
        string ext = System.IO.Path.GetExtension(file.FileName).ToLower();
        DateTime dt = DateTime.Now;
        if (ext == ".jpg" || ext == ".bmp" || ext == ".gif")
        {
            if (dt.ToString().StartsWith("/"))
            {
                fileName = dt.ToString().Replace(" ", "").Replace("/", "").Replace(":", "");
                fileName += ext;
                file.SaveAs(path + filepathroot + @"/" + fileName);

            }
            else
            {
                string strDt = dt.ToString();
                fileName = strDt.Replace(" ", "").Replace("-", "").Replace(":", "");
                fileName += ext;
                file.SaveAs(path + filepathroot + @"/" + fileName);
            }
        }
        else
        {
            Response.Write("<script>alert('图片格式不正确')</script>");
        }
        //}
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        try
        {
            IAdInfo AdInfo = BBLFactory.GetAdInfo();
            upload(filepath, FileUpload1, AdType.SelectedItem.Text );
            //Response.Write(AdType.SelectedValue.ToString());
            int i = AdInfo.InsetAdInfo(Convert.ToInt32(AdID.Text), AdName.Text, fileName, "否", Convert.ToInt32(AdType.SelectedValue), AdRemark.Text);
            if (i > 0)
            {
                Response.Write("<script javascript:language>alert('增加成功');</script>");
                Response.Redirect("wbAdMagner.aspx");
            }
            else
            {
                Response.Write("<script javascript:language>alert('增加失败');</script>");
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script javascript:language>alert('"+ex.Message+"')</script>");
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
    }
}
