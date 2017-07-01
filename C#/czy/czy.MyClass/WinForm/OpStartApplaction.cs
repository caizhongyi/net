using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;

namespace czy.MyClass.WinForm
{
    public class OpStartApplaction
    {
        #region 开机起动程序
        /// <summary>
        /// 开机起动程序
        /// </summary>
        /// <param name="FileName">起动的程序名</param>
        public void StartApplaction(string FileName)
        {
            ;
            string ShortFileName = FileName.Substring(FileName.LastIndexOf("\\") + 1);
            //打开子键节点
            RegistryKey MyReg = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (MyReg == null)
            {//如果子键节点不存在，则创建之
                MyReg = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            }
            //在注册表中设置自启动程序
            MyReg.SetValue(ShortFileName, FileName);
        }
        #endregion
    }
}
