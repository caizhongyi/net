using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Command
{
    public class GET_FILE_REQUEST:CommandBase
    {
        private string mFullName;
        public string FullName
        {
            get
            {
                return mFullName;
            }
            set
            {
                mFullName = value;
            }
        }
        protected override void OnLoadState(object data)
        {
            object[] values = (object[])data;
            base.OnLoadState(values[0]);
            FullName = (string)values[1];
        }
        protected override object OnSaveState()
        {
            return new object[] { base.OnSaveState(), FullName };
        }
        
    }
}
