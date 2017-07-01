using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;
namespace PowerTalkBoxEnum.Enum
{
   
   public class ContrlList
    {
    }
    public enum ChatMode
    { 
        /// <summary>
        /// 一对一聊天模式
        /// </summary>
        OneToOne,
        /// <summary>
        /// 一对多聊天模式
        /// </summary>
        OneToMore,
    }
    public enum SystemMode
    { 
        WebToWeb,
        WebToIm  
    }
 
}
