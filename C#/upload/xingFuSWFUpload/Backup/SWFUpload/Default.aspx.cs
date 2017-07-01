using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;

//源码下载 www.51aspx.com
namespace SWFUpload
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.UC_SWFUpload1.swfUploadInfo = new SWFUploadInfo() 
            { 
                UploadMode = UpMode.LIST, 
                File_size_limit = 300,
                SubmitButtonId=this.Button1.ClientID 
            };
            this.UC_SWFUpload2.swfUploadInfo = new SWFUploadInfo()
            {
                File_types = "*.jpg;*.gif",
                File_types_description = "图片文件",
                IsSmall = true,
                IsWaterMark = true,
                SubmitButtonId=this.Button2.ClientID
            };
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            this.lbPath.Items.Clear();
            this.lbName.Items.Clear();
            foreach (string item in this.UC_SWFUpload1.FilePathList)
                this.lbPath.Items.Add(item);
            foreach (string item in this.UC_SWFUpload1.FileNameList)
                this.lbName.Items.Add(item);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            this.lbPath2.Items.Clear();
            this.lbName2.Items.Clear();
            foreach (string item in this.UC_SWFUpload2.FilePathList)
                this.lbPath2.Items.Add(item);
            foreach (string item in this.UC_SWFUpload2.FileNameList)
                this.lbName2.Items.Add(item);
        }
    }
}
