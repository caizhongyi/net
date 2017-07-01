using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;
using System.Web;

namespace MyClass.MyFileOpeation
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
        public static  void DownLoad(Page page,string path,string fileName)
        {

            //string fileName = this.ltv.Text.Substring(this.ltv.Text.LastIndexOf('/') + 1);//客户端保存的文件名 

            string filePath = page.Server.MapPath(path);//路径

            FileInfo fileInfo = new FileInfo(filePath);

            page.Response.Clear();

            page.Response.ClearContent();

            page.Response.ClearHeaders();

            page.Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);

            page.Response.AddHeader("Content-Length", fileInfo.Length.ToString());

            page.Response.AddHeader("Content-Transfer-Encoding", "binary");

            page.Response.ContentType = "application/octet-stream";

            page.Response.ContentEncoding = System.Text.Encoding.Default;

            page.Response.WriteFile(fileInfo.FullName);

            page.Response.Flush();

            page.Response.End();


        }
        /// <summary>
        /// 文件下载
        /// </summary>
        /// <param name="path">虚拟路径</param>
        /// <param name="page">页面</param>
        public static void DownLoad(string path, Page page)
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
                        iStream = new System.IO.FileStream(page.Server.MapPath(path), System.IO.FileMode.Open,
                         System.IO.FileAccess.Read, System.IO.FileShare.Read);
                    }
                }
                catch
                {
                    page.Response.Write("文件下载时路径不正确出现错误!");
                }

                dataToRead = iStream.Length;

                page.Response.ContentType = "application/octet-stream";

                page.Response.AddHeader("Content-Disposition", "attachment; filename=" +
                HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8));

                while (dataToRead > 0)
                {

                    if (page.Response.IsClientConnected)
                    {

                        length = iStream.Read(buffer, 0, 10000);

                        page.Response.OutputStream.Write(buffer, 0, length);

                        page.Response.Flush();


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

                page.Response.Write("文件下载时出现错误!");

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
