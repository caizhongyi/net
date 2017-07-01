using System;
using System.Collections.Generic;
using System.Text;

namespace WbSystem.FileManage
{
    public class FileFactory
    {
        public static IFileOperate GetFileInfo()
        {
            return new FileOperate();
        }
    }
}
