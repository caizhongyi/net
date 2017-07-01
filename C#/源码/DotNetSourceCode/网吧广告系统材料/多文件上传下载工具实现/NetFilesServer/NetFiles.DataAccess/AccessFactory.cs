using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.DataAccess
{
    public class AccessFactory
    {
        public static IBoot CreateBoot()
        {
            
                return new Boot();
            
        }
    }
}
