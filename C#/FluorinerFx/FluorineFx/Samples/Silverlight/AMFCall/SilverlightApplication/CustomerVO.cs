using System;
using System.Collections.Generic;
using System.Text;

namespace Flex
{
    public class CustomerVO
    {
        string _firstname;

        public string firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }

        string _lastname;

        public string lastname
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        string _phone;

        public string phone
        {
            get { return _phone; }
            set { _phone = value; }
        }

        public override string ToString()
        {
            return firstname + " " + lastname + " " + phone;
        }
    }
}
