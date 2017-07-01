using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web.UI;

namespace MyClass.MyFileOpeation
{
    /// <summary>
    /// 文件流
    /// </summary>
    public class MyStream
    {
        /// <summary>
        /// FileSteam上传文件(考备上传)
        /// </summary>
        /// <param name="requestPath">上传文件的路径</param>
        /// <param name="page">页面</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static void FileSteamUpLoad(string requestPath, Page page, string fileName)
        {
            try
            {
                
                int count = 1;
                FileStream fs1 = new FileStream(requestPath, FileMode.Open);
                FileStream fs2 = new FileStream(page.Server.MapPath(page.Request.ApplicationPath) + @"\" + fileName, FileMode.Create);
                byte[] b = new byte[2046];
                while (count != 0)
                {
                    count = fs1.Read(b, 0, b.Length);
                    fs2.Write(b, 0, b.Length);
                }
                fs1.Close();
                fs2.Close();
               
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static bool StreamWrite(string path, Encoding encodeing, string str)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(path, false, encodeing))
                {
                    sw.WriteLine(str);
                }
                return true;
            }
            catch { return false; }
        }

        public static string StreamRead(string path, Encoding encodeing)
        {
            using (StreamReader sr = new StreamReader(path, encodeing))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
