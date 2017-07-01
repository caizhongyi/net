using System;
using System.Collections.Generic;
using System.Text;

namespace WbSystem.FileManage
{
    public interface IFileOperate
    {
        void CreateFolder(string dirpath);
        void DelFolder(string dirpath);
        void RechristenFolderName(string oldname,string newname);
    }
}
