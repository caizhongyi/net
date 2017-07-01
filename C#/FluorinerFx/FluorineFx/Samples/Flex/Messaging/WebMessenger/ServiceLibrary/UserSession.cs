using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLibrary
{
    class UserSession
    {
        private string _userId;
        private string _password;
        private string _status;

        public string userId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string status
        {
            get { return _status; }
            set { _status = value; }
        }
    }
}
