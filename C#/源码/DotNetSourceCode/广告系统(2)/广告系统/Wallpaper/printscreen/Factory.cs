using System;
using System.Collections.Generic;
using System.Text;

namespace printscreen
{
    public class Factory
    {
        public static IPrintScreen GetPrintScreen()
        {
            return new PrintScreen();
        }
    }
}
