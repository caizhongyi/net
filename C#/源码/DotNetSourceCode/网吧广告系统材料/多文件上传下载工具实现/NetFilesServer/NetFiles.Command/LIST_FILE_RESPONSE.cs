using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Command
{
    /// <summary>
    /// 答复文件列表请求
    /// </summary>
    public class LIST_FILE_RESPONSE:CommandBase
    {
        protected override void OnLoadState(object data)
        {
            object[] values = (object[])data;
            base.OnLoadState(values[0]);
            mRecords = (List<Record>)values[1];
        }
        protected override object OnSaveState()
        {
            return new object[] { base.OnSaveState(), mRecords };
        }
        private List<Record> mRecords = new List<Record>();
        public List<Record> Records
        {
            get
            {
                return mRecords;
            }
        }

    }
}
