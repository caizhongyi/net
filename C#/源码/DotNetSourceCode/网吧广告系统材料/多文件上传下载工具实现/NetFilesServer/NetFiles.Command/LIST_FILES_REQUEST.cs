using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Command
{
    /// <summary>
    /// ����ĳ��Ŀ¼�µ��ļ�
    /// </summary>
    public class LIST_FILE_REQUEST:CommandBase
    {
        private Record mFolder = new Record(Record.BootTag);
        public Record Folder
        {
            get
            {
                return mFolder;
            }
            set
            {
                mFolder = value;
            }
          
        }
        protected override void OnLoadState(object data)
        {
            object[] values = (object[])data;
            base.OnLoadState(values[0]);
            mFolder = (Record)values[1];
        }
        protected override object OnSaveState()
        {
            return new object[]{base.OnSaveState(),Folder};
        }
    }
    
}
