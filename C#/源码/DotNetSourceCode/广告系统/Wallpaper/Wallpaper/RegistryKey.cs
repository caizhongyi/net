using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using  System.Runtime.InteropServices;
using Microsoft.Win32; 

namespace SetWallpaper
{
    class RegistryKey : SetWallpaper.IRegistryKey
    {
        /// <summary>
        /// ÐÞ¸ÄIEÊ×Ò³
        /// </summary>
        /// <param name="IEpageUrl">IEURl</param>
        public void ChageIEFiest(string IEpageUrl)
        {

            //RegistryKey pregkey;
            //pregkey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Internet Explorer\main", true);

            //pregkey.SetValue("Start Page", @IEpageUrl);
        }
 
    }
}
