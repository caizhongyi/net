using System;
using System.Collections.Generic;
using System.Text;

namespace Files
{
    public interface IFiles
    {
        #region DownloadFiles 下载服务器文件至客户端
        /// <summary>
        /// 下载服务器文件至客户端
        /// </summary>
        /// <param name="URL">被下载的文件地址，绝对路径 格式如："http://www.wb66.cn/liucheng.doc"</param>
        /// <param name="Dir">另存放的目录 格式如：@"D:\"</param>
        void DownloadFiles(string URL, string Dir);
        #endregion
    }
}
