
using System;
using System.Security.Permissions;
using System.Web;
using PowerTalkBox;
namespace Events
{
    public delegate void ReceiveMessageEventHandler(object source, ReceiveMessageEventArgs args);
    [AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal), AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
    public class ReceiveMessageEventArgs : EventArgs
    {
        private string _sender;
        private string _reciver;
        private string value;

        public ReceiveMessageEventArgs()
        {
 
        }
        /// <summary>
        /// ������
        /// </summary>
        public string reciver
        {
            get
            {
                return this._reciver;
            }
            set
            {
                this._reciver = value;
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string sender
        {
            get
            {
                return this._sender;
            }
            set
            {
                this._sender = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
            }
        }
 
   
    }
}
