using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

namespace MyClass.MessageTransact
{
    public class DownLoadFiles
    {
        public static void ResponseFile(string path, Page page)
        {

            System.IO.Stream iStream = null;

            byte[] buffer = new Byte[10000];

            int length;

            long dataToRead;

            string filename = System.IO.Path.GetFileName(path);


            try
            {

                iStream = new System.IO.FileStream(path, System.IO.FileMode.Open,
                System.IO.FileAccess.Read, System.IO.FileShare.Read);

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