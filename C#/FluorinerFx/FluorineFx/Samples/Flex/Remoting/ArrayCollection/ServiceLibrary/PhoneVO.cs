using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLibrary
{
    public class PhoneVO
    {
        string _number;

        public string Number
        {
            get { return _number; }
            set { _number = value; }
        }
        string _areaCode;

        public string AreaCode
        {
            get { return _areaCode; }
            set { _areaCode = value; }
        }
    }
}
