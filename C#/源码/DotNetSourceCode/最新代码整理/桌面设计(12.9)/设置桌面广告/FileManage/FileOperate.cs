using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WbSystem.FileManage
{
    class FileOperate:IFileOperate
    {
        public void CreateFolder(string dirpath)
        {
            if (Directory.Exists(dirpath))
            {
                DelFolder(dirpath);
                Directory.CreateDirectory(dirpath);
            }
            else
            {
                Directory.CreateDirectory(dirpath);
            } 
        }

        public void DelFolder(string dirpath)
        {
            DirectoryInfo di = new DirectoryInfo(dirpath);
            int n = di.GetFiles("*").Length;
            if (n >= 1)
            {
                foreach (FileInfo fi in di.GetFiles("*"))
                {
                    File.Delete(fi.FullName);
                }
            }
            Directory.Delete(dirpath);
        }

        public void RechristenFolderName(string oldname, string newname)
        {
            try
            {
                if (Directory.Exists(newname))
                {
                    DelFolder(newname);
                }
                Directory.Move(oldname, newname);
            }
            catch
            {
                MessageBox.Show("你要重命名的文件夹不存在！");
            }
        }
    }
}
