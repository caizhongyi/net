using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Command
{
    /// <summary>
    /// ÇëÇó´íÎó´ð¸´
    /// </summary>
    public class REQUEST_ERROR:CommandBase
    {
        private string mException;
        public string Exception
        {
            get
            {
                return mException;
            }
            set
            {
                mException = value;
            }
        }
        protected override void OnLoadState(object data)
        {
            object[] values = (object[])data;
            base.OnLoadState(values[0]);
            Exception = (string)values[1];
        }
        protected override object OnSaveState()
        {
            return new object[] { base.OnSaveState(), Exception };
        }
    }
}
