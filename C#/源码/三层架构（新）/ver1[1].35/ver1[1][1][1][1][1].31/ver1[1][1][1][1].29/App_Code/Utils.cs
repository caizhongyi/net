using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Utils 的摘要说明
/// </summary>
public class Utils
{
	public Utils()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string PictureUpload(FileUpload fileUpload,string path)
    {
         bool fileOk = false;
       
        string fileExt = "";
        string fileName = "";

        if (fileUpload.HasFile)
        {
            fileExt = System.IO.Path.GetExtension(fileUpload.FileName).ToLower();
            string[] allowedFileExt ={ ".gif", ".jpg", ".jpeg", ".png" };
            foreach (string ext in allowedFileExt)
            {
                if (fileExt == ext)
                {
                    fileOk = true;
                    break;
                }
            }
        }

        if (fileOk)
        {

             fileName = DateTime.Now.ToString().Replace(":", "").Replace(" ", "").Replace("-", "");
            //添加随机数 小于１００
             Random rand = new Random();
             fileName+=rand.Next(100).ToString();

             fileName += fileExt;
            fileUpload.PostedFile.SaveAs(path + fileName);
            //........................
        }
        return fileName;
    }



}
