using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace czy.MyClass.WinForm
{
    class ChangeIEFirstPage
    {
        /// <summary>
        ///  修改IE首页
        /// </summary>
        /// <param name="IEpageUrl">IEurl</param>

        public void ChageIEFiest(string IEpageUrl)
        {

            RegistryKey pregkey;
            pregkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\main", true);
            pregkey.SetValue("Start Page", @IEpageUrl);//设置键值

        }
    }
}
