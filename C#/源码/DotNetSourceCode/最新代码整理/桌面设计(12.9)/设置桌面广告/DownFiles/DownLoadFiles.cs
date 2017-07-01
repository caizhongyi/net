using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace Files
{
    class DownLoadFiles : IFiles
    {
        #region DownloadFiles 下载服务器文件至客户端
        /// <summary>
        /// 下载服务器文件至客户端
        /// </summary>
        /// <param name="URL">被下载的文件地址，绝对路径 格式如："http://www.wb66.cn/liucheng.doc"</param>
        /// <param name="Dir">另存放的目录 格式如：@"D:\"</param>
        public void DownloadFiles(string URL, string Dir)
        {
            WebClient client = new WebClient();
            string fileName = URL.Substring(URL.LastIndexOf("/") + 1);  //被下载的文件名

            string Path = Dir + fileName;   //另存为的绝对路径＋文件名

            try
            {
                WebRequest myre = WebRequest.Create(URL);
            }
            catch
            {
                //MessageBox.Show(exp.Message,"Error"); 
            }

            try
            {
                client.DownloadFile(URL, Path);

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
