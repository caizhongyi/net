using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Command
{
    public class CommandBase:HFSoft.Net.Messages.MessageAdapter
    {
        protected override void OnLoadState(object data)
        {
            mParentChannelID = (string)data;
        }
        protected override object OnSaveState()
        {
            return mParentChannelID;
        }
        private string mParentChannelID;
        /// <summary>
        /// ÃèÊö¸¸Í¨¹ýID
        /// </summary>
        public string ParentChannelID
        {
            get {
                return mParentChannelID;
            }
            set
            {
                mParentChannelID = value;
            }
        }
    }

}
