using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.IO;
using System.Web.UI;
using System.Web;

namespace czy.MyClass
{
    /// <summary>
    /// 文件类路径类
    /// </summary>
    public sealed partial  class PathHelper
    {
        private static ArrayList allFilesPath;

        #region 获取目录下的所有文件
        /// <summary>
        /// 获取目录下的所有文件
        /// </summary>
        /// <param name="root">根目录</param>
        /// <returns>目录下的所有文件</returns>
        public static ArrayList GetRootAllFilesPath(string root)
        {
            allFilesPath = new ArrayList();
            GetRootFilePath(root);
            return allFilesPath;
        }

        private static void GetRootFilePath(string root)
        {
            string[] filesPath = Directory.GetFiles(root);

            foreach (string s in filesPath)
            {
                allFilesPath.Add(s);
            }
            string[] directoriesPath = Directory.GetDirectories(root);
            foreach (string s in directoriesPath)
            {
                GetRootFilePath(s);
            }
        }
        #endregion

        #region 获取目录下的所有文件名
        /// <summary>
        /// 获取目录下的所有文件名
        /// </summary>
        /// <param name="root">根目录</param>
        /// <returns>目录下的所有文件名</returns>
        public static ArrayList GetRootAllFilesName(string root)
        {
            allFilesPath = new ArrayList();
            GetRootFilesName(root);
            return allFilesPath;
        }

        private static void GetRootFilesName(string root)
        {
            string[] filesPath = Directory.GetFiles(root);

            foreach (string s in filesPath)
            {
                allFilesPath.Add(GetFileName(s));
            }
            string[] directoriesPath = Directory.GetDirectories(root);
            foreach (string s in directoriesPath)
            {
                GetRootFilesName(s);
            }
        }
        #endregion

        #region 文件名
        /// <summary>
        /// 获取文件名
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>文件名</returns>
        public static string GetFileName(string path)
        {
            return path.Substring(path.LastIndexOf("\\"));
        }

        /// <summary>
        /// 获取文件上一级目录
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>目录</returns>
        public static string GetFileParentRoot(string path)
        {
            return path.Substring(0,path.Length - path.LastIndexOf("\\")-1);
        }
  
        /// <summary>
        /// 获取文件格式
        /// </summary>
        /// <param name="fileName">文件名或文件路径</param>
        /// <returns>获取文件格式</returns>
        public static string GetFileFormat(string fileName)
        {
            return fileName.Substring(fileName.LastIndexOf("."));
          
        }
        #endregion
     
    }


}
