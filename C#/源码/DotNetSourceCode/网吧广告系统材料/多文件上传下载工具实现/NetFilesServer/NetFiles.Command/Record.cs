using System;
using System.Collections.Generic;
using System.Text;

namespace NetFiles.Command
{
    [Serializable]
    public class Record
    {
        public Record()
        {
        }
        public Record(string fullname)
        {
            FullName = fullname;
        }
        private string mName;
        public string Name
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }
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
        private long mSize;
        public long Size
        {
            get
            {
                return mSize;
            }
            set
            {
                mSize = value;
            }
        }
        private RecoreType mType = RecoreType.File;
        public RecoreType Type
        {
            get
            {
                return mType;
            }
            set
            {
                mType = value;
            }
        }
        public static string BootTag ="boot";
    }

    /// <summary>
    /// ������¼����
    /// </summary>
    public enum RecoreType
    {
        /// <summary>
        /// Ŀ¼
        /// </summary>
        Folder,
        /// <summary>
        /// �ļ�
        /// </summary>
        File
    }
}
