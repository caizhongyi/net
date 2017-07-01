using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace NetFiles.Command
{
    public class SEND_FILE:CommandBase
    {
        private FileState mState = FileState.None;
        public FileState State
        {
            get
            {
                return mState;
            }
            set
            {
                mState = value;
            }
        }
        private byte[] mDate;
        public byte[] Data
        {
            get
            {
                return mDate;
            }
            set
            {
                mDate = value;
            }
        }
        private long mFileSize;
        /// <summary>
        /// 文件总长度
        /// </summary>
        public long FileSize
        {
            get
            {
                return mFileSize;
            }
            set
            {
                mFileSize = value;
            }
        }
        private long mIndex;
        /// <summary>
        /// 文件数据包在文件中起如位置
        /// </summary>
        public long Index
        {
            get
            {
                return mIndex;
            }
            set
            {
                mIndex = value;
            }
        }
        protected override void OnLoadState(object data)
        {
            object[] values = (object[])data;
            base.OnLoadState(values[0]);
            State = (FileState)values[1];
            Data = (byte[])values[2];
            FileSize = (long)values[3];
            Index = (long)values[4];
            Count = (int)values[5];
            mFolder = (Record)values[6];
            Name = (string)values[7];
        }
        protected override object OnSaveState()
        {
            return new object[] { base.OnSaveState(), State, Data, FileSize, Index,Count,Folder,Name };
        }
        private int mCount;
        /// <summary>
        /// 文件数据包总数
        /// </summary>
        public int Count
        {
            get
            {
                return mCount;
            }
            set
            {
                mCount = value;
            }
        }

        private Record mFolder;
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
        public static SEND_FILE FilePackage(Stream stream, int packagesize, long index, int count,byte[] data)
        {

            SEND_FILE sf;

            sf = new SEND_FILE();

            if (index == 0)
            {
                sf.State = FileState.Start;
            }
            else if (index == count - 1)
            {
                sf.State = FileState.End;
            }
            else
            {
                sf.State = FileState.None;
            }
            sf.Count = count;
            sf.FileSize = stream.Length;
            sf.Index = index * packagesize;
            sf.Data = data;
            stream.Read(sf.Data, 0, packagesize);
           
            return sf;
        }
        public static int PackageCount(Stream stream, int packagesize)
        {
            int count;
            if (stream.Length < packagesize)
            {
                count = 1;
            }
            else
            {
                count = (int)stream.Length % packagesize == 0 ? (int)stream.Length / packagesize : (int)stream.Length / packagesize + 1;
            }
            return count;
        }
        public static void SaveFile(SEND_FILE response,System.IO.Stream stream)
        {
            stream.Write(response.Data, 0, response.Data.Length);
            stream.Flush();
            
        }
    }
    public enum FileState
    {
        /// <summary>
        /// 文件开始数据包
        /// </summary>
        Start,
        /// <summary>
        /// 文件数据包
        /// </summary>
        None,
        /// <summary>
        /// 文件结构数据包
        /// </summary>
        End
    }
}
