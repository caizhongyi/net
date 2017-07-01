using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using System.IO;

namespace SWFUpload.SWFUpload
{
    public partial class upload : System.Web.UI.Page
    {
        System.Drawing.Image thumbnail_image = null;
        System.Drawing.Image original_image = null;
        System.Drawing.Bitmap final_image = null;
        System.Drawing.Graphics graphic = null;
        MemoryStream ms = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cmd = SWFUrlOper.GetStringParamValue("cmd");
                if (cmd == string.Empty)
                    this.uploadFile();
                else if (cmd == "del")
                    this.DelFile();
            }
        }

        public void uploadFile()
        {
            //保存的路径
            string savePath = SWFUrlOper.GetFormStringParamValue("path");
            //旧文件名,方便删除
            string oldFileName = SWFUrlOper.GetFormStringParamValue("fn");
            //是否需要小图
            bool isSmall = SWFUrlOper.GetFormStringParamValue("small").ToLower() == "true" ? true : false;
            //是否需要水印
            bool isWaterMark = SWFUrlOper.GetFormStringParamValue("wm").ToLower() == "true" ? true : false;
            //小图宽度
            int smallWidth = SWFUrlOper.GetFormIntParamValue("sw");
            //小图高度
            int smallHeight = SWFUrlOper.GetFormIntParamValue("sh");
            //图片扩展名
            string[] imgExtension = new string[] { "jpg", "gif", "png", "bmp" };
            try
            {
                // 获取上传的文件信息
                HttpPostedFile file_upload = Request.Files["Filedata"];
                string extension = string.Empty;
                string fileName = string.Empty;
                //bool isImg = false;
                if (file_upload.ContentLength > 0)
                {
                    fileName = file_upload.FileName;
                    if (fileName.IndexOf(".") != -1)
                        extension = fileName.Substring(fileName.LastIndexOf(".") + 1, fileName.Length - fileName.LastIndexOf(".") - 1);

                    //判断是否是图片文件
                    //foreach (string item in imgExtension)
                    //{
                    //    if (item == extension)
                    //    {
                    //        isImg = true;
                    //        break;
                    //    }
                    //}

                    SWFUploadFile uf = new SWFUploadFile();
                    if (isSmall)
                    {
                        uf.SmallPic = true;
                        uf.MaxWith = smallWidth == 0 ? uf.MaxWith : smallWidth;
                        uf.MaxHeight = smallHeight == 0 ? uf.MaxHeight : smallHeight;
                    }
                    uf.IsWaterMark = isWaterMark;
                    int state = 0;
                    uf.SaveFile(file_upload, Request.Form["path"], oldFileName, ref state);
                    //0:上传成功.  1:没有选择要上传的文件.  2:上传文件类型不符.   3:上传文件过大  -1:应用程序错误.
                    int statusCode = 500;
                    string msg = "内部服务器错误!";
                    switch (state)
                    {
                        case 0:
                            statusCode = 200;
                            msg = uf.NewFileName;
                            break;
                        case 1:
                            statusCode = 500;
                            msg = "没有选择要上传的文件!";
                            break;
                        case 2:
                            statusCode = 500;
                            msg = "上传文件类型不符!";
                            break;
                        case 3:
                            statusCode = 500;
                            msg = "上传文件过大!";
                            break;
                        case -1:
                            statusCode = 500;
                            msg = "应用程序错误!";
                            break;
                    }
                    Response.StatusCode = statusCode;
                    Response.Write(msg);
                }
            }
            catch
            {
                //内部服务器错误
                Response.StatusCode = 500;
                Response.Write("内部服务器错误");
            }
            Response.End();
        }
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns></returns>
        public void DelFile()
        {
            string name = SWFUrlOper.GetStringParamValue("name");
            string msg = new SWFUploadFile().Delete(SWFUrlOper.GetStringParamValue("f"), SWFUrlOper.GetStringParamValue("name"), true);
            // return msg;
            Response.Write(msg);
            Response.End();
        }
    }
}
