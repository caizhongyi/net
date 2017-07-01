using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


public static class DirectoryExtender
    {
        /// <summary>
        /// 随机读取目录中的文件
        /// </summary>
        /// <param name="directory">DirectoryInfo目录对像</param>
        /// <param name="searchPattern">匹配的文件,如"*.txt"</param>
        /// <returns></returns>
        public static FileInfo ReadRandomFilePath(this DirectoryInfo directory, string searchPattern)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            FileInfo[] fileInfos=directory.GetFiles(searchPattern);
            return fileInfos[r.Next(0, fileInfos.Length - 1)];
        }
        /// <summary>
        /// 随机读取目录中的文件
        /// </summary>
        /// <param name="directory">DirectoryInfo目录对像</param>
        /// <returns></returns>
        public static FileInfo ReadRandomFilePath(this DirectoryInfo directory)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            FileInfo[] fileInfos = directory.GetFiles();
            return fileInfos[r.Next(0, fileInfos.Length - 1)];
        }
        /// <summary>
        /// 随机读取目录中的文件
        /// </summary>
        /// <param name="directory">路径</param>
        /// <param name="searchPattern">匹配的文件,如"*.txt"</param>
        /// <returns>路径</returns>
        public static string ReadRandomFilePath(string path,string searchPattern)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            string[] fileInfos = Directory.GetFiles(path,searchPattern);
            return fileInfos[r.Next(0, fileInfos.Length - 1)];
        }
        /// <summary>
        /// 随机读取目录中的文件
        /// </summary>
        /// <param name="directory">路径</param>
        /// <returns>路径</returns>
        public static string ReadRandomFilePath(string path)
        {
            Random r = new Random(DateTime.Now.Millisecond);
            string[] fileInfos = Directory.GetFiles(path);
            return fileInfos[r.Next(0, fileInfos.Length - 1)];
        }
    }
