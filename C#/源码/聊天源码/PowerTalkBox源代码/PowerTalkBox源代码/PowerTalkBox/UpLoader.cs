using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/*Programmer:Engine
*PowerBy:EngineSystem
*Oicq:282602809    
*Msn:Cangta2002@hotmail.com
*E-Mail:Hee_jun1985@163.com
*Authorization:Free
*/
namespace PowerTalkBox
{
    /// <summary>
    /// 流处理中心
    /// </summary>
    public class UpLoader
    {
        /// <summary>
        /// 输入服务器文件夹路径地址，返回一个流:WebForm
        /// </summary>
        /// <param name="FileName">服务器文件路径</param>
        /// <returns></returns>
        public System.IO.Stream ReadFileStream(string FileName)
        {
            string path = HttpContext.Current.Server.MapPath(FileName);
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        /// <summary>
        /// 输入一个流，输出到http:WebForm
        /// </summary>
        /// <param name="ImageString">图片流</param>
        public void OutResponse(System.IO.Stream ImageString)
        {
            System.IO.Stream ms = ImageString;
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ExpiresAbsolute = System.DateTime.Now.AddMilliseconds(0);
            HttpContext.Current.Response.Expires = 0;
            HttpContext.Current.Response.CacheControl = "no-cache";
            HttpContext.Current.Response.AppendHeader("Pragma", "No-Cache");
            HttpContext.Current.Response.ClearContent();
            //   HttpContext.Current.Response.ContentType = "image/Png";     
            int buffersize = (int)ms.Length;
            byte[] buffer = new byte[buffersize];
            int count = ms.Read(buffer, 0, buffersize);
            //  HttpContext.Current.Response.BinaryWrite(buffer);
            HttpContext.Current.Response.OutputStream.Write(buffer, 0, count);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
        }
        /// <summary>
        /// 输出一个流，输出到http:WebForm
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        public void OutResponse(string FilePath)
        {
            FilePath = HttpContext.Current.Server.MapPath(FilePath);
            string Filname = FilePath.Substring(FilePath.LastIndexOf('/') + 1);
            FileInfo fi = new FileInfo(FilePath);
            FileStream MyFileStream = fi.OpenRead();
            long FileSize;
            FileSize = MyFileStream.Length;
            byte[] Buffer = new byte[(int)FileSize];
            MyFileStream.Read(Buffer, 0, (int)FileSize);
            MyFileStream.Close();
            HttpContext.Current.Response.ContentType = "application/octet-stream";
            HttpContext.Current.Response.AddFileDependency(Filname);
            HttpContext.Current.Response.BinaryWrite(Buffer);
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.Close();
        }
        /// <summary>
        /// 把流存成文件到指定位置:WebForm
        /// </summary>
        /// <param name="FlStream">文件流</param>
        /// <param name="FilePath">服务器文件路径</param>
        public void SaveStreamToFile(Stream FlStream, string FilePath)
        {
            //读
            string path = HttpContext.Current.Server.MapPath(FilePath);
            int buffersize = (int)FlStream.Length;
            byte[] buffer = new byte[buffersize];
            int count = FlStream.Read(buffer, 0, buffersize);
            //写
            FileInfo fi = new FileInfo(path);
            FileStream fsw = fi.Create();
            fsw.Write(buffer, 0, buffersize);
            fsw.Close();

        }
        /// <summary>
        /// 读流进入字节:Common
        /// </summary>
        ///<param name="FilePath">路径</param>
        public byte[] ReadStream(string FilePath)
        {
            Stream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            int buffersize = (int)fs.Length;
            byte[] buffer = new byte[buffersize];
            int count = fs.Read(buffer, 0, buffersize);
            fs.Close();
            return buffer;
        }
    }
}