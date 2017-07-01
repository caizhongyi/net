using System;
using System.Collections.Generic;
using System.Text;

namespace MySocket
{
   public static  class Factory
    {
       public static Imysocket Getmysocket()
       {
           return new mysocket();
       }
    }
}
