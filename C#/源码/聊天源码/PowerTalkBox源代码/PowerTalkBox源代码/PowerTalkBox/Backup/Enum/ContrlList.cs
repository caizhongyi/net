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
        /// һ��һ����ģʽ
        /// </summary>
        OneToOne,
        /// <summary>
        /// һ�Զ�����ģʽ
        /// </summary>
        OneToMore,
    }
    public enum SystemMode
    { 
        WebToWeb,
        WebToIm  
    }
 
}
