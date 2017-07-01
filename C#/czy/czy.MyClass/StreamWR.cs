using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace czy.MyClass
{
    public class StreamWR
    {
        /// <summary>
        /// 存在时读取，不存在时写入
        /// </summary>
        /// <param name="wirteText">写入内容</param>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static string ExiFReadOrWrite(string wirteText,string path)
        {
            if (File.Exists(path))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                   return  sr.ReadToEnd();
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                     sw.Write(wirteText);
                     return wirteText;
                }
            }
        }


    }
}
