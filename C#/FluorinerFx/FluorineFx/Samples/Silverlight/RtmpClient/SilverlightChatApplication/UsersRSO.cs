using System;
using FluorineFx;
using FluorineFx.Net;

namespace SilverlightChatApplication
{
    public class UsersRSO : RemoteSharedObject
    {
        Page _page;

        //This class must have parameterless constructor!
        public UsersRSO()
        {
        }

        public Page Page
        {
            get { return _page; }
            set { _page = value; }
        }

        public void msgFromSrvr(string msg)
        {
            _page.LogMessage(msg);
        }
    }
}
