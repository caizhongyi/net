using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web;

namespace czy.MyClass.Web
{
    /// <summary>
    /// 文件下载
    /// </summary>
    public class DownLoadFile
    {
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="page">页面</param>
        /// <param name="path">虚拟路径</param>
        /// <param name="fileName">文件名称</param>
        public static  void DownLoad(string path,string fileName)
        {

            //string fileName = this.ltv.Text.Substring(this.ltv.Text.LastIndexOf('/') + 1);//客户端保存的文件名 

            string filePath = HttpContext.Current.Server.MapPath(path);//路径

            FileInfo fileInfo = new FileInfo(filePath);

            HttpContext.Current.Response.Clear();

            HttpContext.Current.Response.ClearContent();

            HttpContext.Current.Response.ClearHeaders();

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

            HttpContext.Current.Response.AddHeader("Content-Length", fileInfo.Length.ToString());

            HttpContext.Current.Response.AddHeader("Content-Transfer-Encoding", "binary");

            HttpContext.Current.Response.ContentType = "application/octet-stream";

            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.Default;

            HttpContext.Current.Response.WriteFile(fileInfo.FullName);

            HttpContext.Current.Response.Flush();

            HttpContext.Current.Response.End();


        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="path">虚拟路径</param>
        /// <param name="page">页面</param>
        public static void DownLoad(string path)
        {

            System.IO.Stream iStream = null;

            byte[] buffer = new Byte[10000];

            int length;

            long dataToRead;

            string filename = System.IO.Path.GetFileName(path);


            try
            {

                try
                {
                    if (File.Exists(path))
                    {
                        iStream = new System.IO.FileStream(path, System.IO.FileMode.Open,
                        System.IO.FileAccess.Read, System.IO.FileShare.Read);
                    }
                    else
                    {
                        iStream = new System.IO.FileStream(HttpContext.Current.Server.MapPath(path), System.IO.FileMode.Open,
                         System.IO.FileAccess.Read, System.IO.FileShare.Read);
                    }
                }
                catch
                {
                    HttpContext.Current.Response.Write("文件下载时路径不正确出现错误!");
                }

                dataToRead = iStream.Length;

                HttpContext.Current.Response.ContentType = "application/octet-stream";

                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" +
                HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));

                while (dataToRead > 0)
                {

                    if (HttpContext.Current.Response.IsClientConnected)
                    {

                        length = iStream.Read(buffer, 0, 10000);

                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);

                        HttpContext.Current.Response.Flush();


                        buffer = new Byte[10000];

                        dataToRead = dataToRead - length;

                    }

                    else
                    {

                        dataToRead = -1;

                    }

                }

            }

            catch (Exception ex)
            {

                HttpContext.Current.Response.Write("文件下载时出现错误!");

            }

            finally
            {

                if (iStream != null)
                {

                    iStream.Close();

                }

            }

        }
    }
}
