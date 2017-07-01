using System;
using System.Collections.Generic;
using System.Text;

namespace Flex
{
    public class CustomerVO
    {
        public string firstname;
        public string lastname;
        public string phone;

        public override string ToString()
        {
            return firstname + " " + lastname + " " + phone;
        }
    }
}
