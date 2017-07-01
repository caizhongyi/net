using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace czy.MyClass
{
    public class Log 
    {
        /// <summary>
        /// 日志类
        /// </summary>
        /// <param name="directoryName">日志所在路径</param>
        /// <param name="content">返回写入是否成功</param>
        public static void  Write(string directoryName,string content)
        {
            string name=directoryName + "/" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            if (!Directory.Exists(directoryName))
            {
               DirectoryInfo d= Directory.CreateDirectory(directoryName);
            }
            FileMode mode = File.Exists(name) ? FileMode.Append : FileMode.OpenOrCreate;
            FileStream fs = new FileStream(name, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            sw.WriteLine(content);
            sw.Close();
            fs.Close();
        }
        /// <summary>
        /// 日志类
        /// </summary>
        /// <param name="directoryName">日志所在路径</param>
        /// <param name="fileName">文件名称</param>
        /// <param name="content">返回写入是否成功</param>
        public static void Write(string directoryName,string fileName, string content)
        {
            string name = directoryName + "/" + fileName;
            if (!Directory.Exists(directoryName))
            {
                DirectoryInfo d = Directory.CreateDirectory(directoryName);
            }
            FileMode mode = File.Exists(name) ? FileMode.Append : FileMode.OpenOrCreate;
            FileStream fs = new FileStream(name, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            sw.WriteLine(content);
            sw.Close();
            fs.Close();
        }    
        /// <summary>
        /// 日志类
        /// </summary>
        /// <param name="content">返回写入是否成功</param>
        public static void Write(string content)
        {
            string name = DateTime.Now.ToString("yyyyMMdd") + ".log";
            FileMode mode = File.Exists(name) ? FileMode.Append : FileMode.OpenOrCreate;
            FileStream fs = new FileStream(name, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs, Encoding.Unicode);
            sw.WriteLine(content);
            sw.Close();
            fs.Close();
        }
    }
}
