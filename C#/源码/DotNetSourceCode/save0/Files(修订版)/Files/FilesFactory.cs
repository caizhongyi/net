using System;
using System.Collections.Generic;
using System.Text;

namespace Files
{
    public class FilesFactory
    {
        public static IFiles GetDownLoadFiles() 
        {
            return new DownLoadFiles();
        }
    }
}
