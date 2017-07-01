using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Files;
using WbSystem.FileManage;

namespace WbSystem
{
    public class DownPicture
    {
        #region 下载图片
        /// <summary>
        /// 下载图片信息
        /// </summary>
        public static void DownPictureInfo()
        {
            IFileOperate ifo = FileFactory.GetFileInfo();
            IFiles file = FilesFactory.GetDownLoadFiles();

            DataSet ds = new DataSet();
            ds.ReadXml("AdvInfo.xml");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                file.DownloadFiles("http://www.wb66.cn/Adpictures/" + ds.Tables[0].Rows[i][2].ToString().Replace("~/", ""), Application.StartupPath + @"\NewAdpictures\");
            }
            ifo.RechristenFolderName("NewAdpictures", "Adpictures");
        }
        #endregion
    }
}
