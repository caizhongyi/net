using System;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Text;
using System.IO;


/// <summary>
/// 单个文件上传类
/// 可以自定义：上传目录，上传文件类型、大小，是否使用随机上传文件名。
/// </summary>
public sealed class UploadFile_Single
{
    /* web.config设置：
     * <appSettings>        
     *     <add key="uploadDir" value="/UploadFiles/" />
     *   <add key="uploadFileExt" value="gif|jpg|jpeg|png|bmp|doc|xls|hlp|chm|rar|zip" />    
     *   <add key="uploadFileSize" value="2048" />
     *   <add key="isUseRandFileName" value="true" />            
     * </appSettings>
     * 
     * private string uploadDir = ConfigurationSettings.AppSettings["uploadDir"];;
     * private string uploadFileExt = ConfigurationSettings.AppSettings["uploadFileExt"];
     * private int uploadFileSize = int.Parse(ConfigurationSettings.AppSettings["uploadFileExt"]);
     * private bool isUseRandFileName = bool.Parse(ConfigurationSettings.AppSettings["uploadFileExt"]);
     * */

    private string uploadDir = "/upImages/";
    private string uploadFileExt = "gif|jpg|jpeg|png|bmp|rar|doc|xls|mp3|wma";
    private int uploadFileSize = 10240;
    private bool isUseRandFileName = true;
    private bool isUploadImage = false;
    private string uploadResultMessage = "";
    private bool uploadSuccessFlag = false;
    private string uploadPath = "";

    /// <summary>
    /// 上传目录，相对于本文件路径；以“/”结尾；如果目录不存在，会自动创建；如果要启用绝对路径，写法:“/虚拟目录名称/上传文件目录名称/”
    /// </summary>
    public string UploadDir
    {
        get { return uploadDir; }
        set { uploadDir = value; }
    }
    /// <summary>
    /// 允许上传文件类型，逗号隔开
    /// </summary>
    public string UploadFileExt
    {
        get { return uploadFileExt; }
        set { uploadFileExt = value; }
    }
    /// <summary>
    /// 允许上传文件大小，k 为单位
    /// </summary>
    public int UploadFileSize
    {
        get { return uploadFileSize; }
        set { uploadFileSize = value; }
    }
    /// <summary>
    /// 是否使用随机上传文件名；如果不使用，会覆盖上传目录的同名文件
    /// </summary>
    public bool IsUseRandFileName
    {
        get { return isUseRandFileName; }
        set { isUseRandFileName = value; }
    }
    /// <summary>
    /// 是否上传的是图片
    /// </summary>
    public bool IsUploadImage
    {
        get { return isUploadImage; }
        set { isUploadImage = value; }
    }
    /// <summary>
    /// 上传结果信息
    /// </summary>
    public string UploadResultMessage
    {
        get { return uploadResultMessage; }
    }
    /// <summary>
    /// 上传成功或失败标识
    /// </summary>
    public bool UploadSuccessFlag
    {
        get { return uploadSuccessFlag; }
    }
    /// <summary>
    /// 上传文件保存路径
    /// </summary>
    public string UploadPath
    {
        get { return uploadPath; }
    }


    //上传文件方法
    public bool Upload(HttpPostedFile uploadFile)
    {
        try
        {
            //获取上传文件属性
            string fileFullName = uploadFile.FileName;
            string contentType = uploadFile.ContentType.ToLower();
            int contentLength = uploadFile.ContentLength;
            //取得文件名
            string fileName = System.IO.Path.GetFileName(fileFullName);
            //取得扩展名
            string fileExt = System.IO.Path.GetExtension(fileName).Remove(0, 1);
            //检测文件类型
            if (isUploadImage && !contentType.StartsWith("image/"))
            {
                uploadResultMessage = "您上传的不是图片，请重新上传！";
                uploadSuccessFlag = false;
                return false;
            }
            if (("|" + UploadFileExt.ToLower() + "|").IndexOf(("|" + fileExt.ToLower() + "|")) < 0)
            {
                uploadResultMessage = "上传文件格式错误！允许上传文件类型（" + uploadFileExt + "）";
                uploadSuccessFlag = false;
                return false;
            }
            //检测文件大小
            if (contentLength < 1 || contentLength > uploadFileSize * 1024)
            {
                uploadResultMessage = "上传文件大小错误！最大允许上传 " + uploadFileSize + " KB";
                uploadSuccessFlag = false;
                return false;
            }

            if (!uploadDir.EndsWith("/")) uploadDir += "/";

            //如果上传目录不存在，创建目录
            string mapPath = System.Web.HttpContext.Current.Server.MapPath(uploadDir);
            if (!Directory.Exists(mapPath))
            {
                Directory.CreateDirectory(mapPath);
            }

            //随机文件名
            if (isUseRandFileName)
            {
                fileName = DateTime.Now.ToString("yyyyMMddhhmmss") + (new Random()).Next(100, 1000).ToString() + "." + fileExt;
            }

            string saveFilePath = System.Web.HttpContext.Current.Server.MapPath(uploadDir) + fileName;
            uploadFile.SaveAs(saveFilePath);
            uploadPath = uploadDir + fileName;
            uploadResultMessage = "文件上传成功！";
            uploadSuccessFlag = true;
        }
        catch (Exception ex)
        {
            uploadResultMessage = "未知原因导致文件上传失败！";
            uploadSuccessFlag = false;
            return false;
        }
        return true;
    }
}

