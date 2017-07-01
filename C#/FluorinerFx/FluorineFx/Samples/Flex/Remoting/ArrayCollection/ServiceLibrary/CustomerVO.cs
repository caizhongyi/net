using System;
using System.Collections.Generic;

namespace ServiceLibrary
{
    public class CustomerVO
    {
        private string _firstname;

        public string Firstname
        {
            get { return _firstname; }
            set { _firstname = value; }
        }
        private string _lastname;

        public string Lastname
        {
            get { return _lastname; }
            set { _lastname = value; }
        }

        private List<PhoneVO> _phoneNumbers;

        public List<PhoneVO> PhoneNumbers
        {
            get 
            {
                if (_phoneNumbers == null)
                    _phoneNumbers = new List<PhoneVO>();
                return _phoneNumbers; 
            }
            set { _phoneNumbers = value; }
        }
    }
}
