using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Win32;


namespace czy.Wpf.Library
{
    public class Register
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="pathNames"></param>
        /// <returns></returns>
       public static bool  IsRegist(object value,string keyName,string[] pathNames)
       {
           RegistryKey pregkey;
           try
           {
               pregkey = Registry.LocalMachine.OpenSubKey("SYSTEM", true).OpenSubKey("ControlSet001", true).OpenSubKey("Control", true).OpenSubKey("Session Manager", true).OpenSubKey("Environment", true);
               if (pathNames != null)
               {
                   foreach (string name in pathNames)
                   {
                       pregkey=Registry.LocalMachine.OpenSubKey(name, true);
                   }
               }
               if (null != pregkey)
               {
                   //if (null == pregkey.GetValue(keys))
                   //{
                   //    pregkey.SetValue(keys, "TRUE");
                   //    pregkey.Close();

                   //}s
                   keyName = keyName == null ? "InvoicePrinting" : keyName;
                   RegistryKey key = pregkey.OpenSubKey("InvoicePrinting");
                   if (key == null)
                   {
                       pregkey.CreateSubKey("InvoicePrinting");
                       pregkey.SetValue("InvoicePrinting", value);
                   }
                   else
                   {
                      object o= pregkey.GetValue("InvoicePrinting", "false");
                      if (o.ToString ()==value )
                      {   
                          pregkey.Close();
                          return true;
                      }
                   }
               } 
               pregkey.Close();
               return false;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
    }
}
