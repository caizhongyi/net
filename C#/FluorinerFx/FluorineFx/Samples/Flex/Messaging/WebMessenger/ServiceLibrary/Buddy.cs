using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLibrary
{
    class Buddy
    {
        private string _userId;
        private string _buddyId;

        public string userId
        {
            get { return _userId; }
            set { _userId = value; }
        }

        public string buddyId
        {
            get { return _buddyId; }
            set { _buddyId = value; }
        }
    }
}
