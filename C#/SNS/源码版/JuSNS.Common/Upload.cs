using System.IO;

namespace JuSNS.Common
{
    public class Upload
    {
        private System.Web.HttpPostedFile postedFile = null;
        private string savePath = "";
        private string extension = "";
        private int fileLength = 0;
        private string filename = "";
        /// <summary>
        /// 上传组件
        /// </summary>
        public System.Web.HttpPostedFile PostedFile
        {
            get
            {
                return postedFile;
            }
            set
            {
                postedFile = value;
            }
        }
        /// <summary>
        /// 保存路径
        /// </summary>
        public string SavePath
        {
            get
            {
                if (savePath != "") return savePath;
                return "c:\\";
            }
            set
            {
                savePath = value;
            }
        }
        /// <summary>
        /// 文件大小
        /// </summary>
        public int FileLength
        {
            get
            {
                if (fileLength != 0) return fileLength;
                return 1024;
            }
            set
            {
                fileLength = value * 1024;
            }
        }
        /// <summary>
        /// 文件护展名
        /// </summary>
        public string Extension
        {
            get
            {
                if (extension != "")
                    return extension;
                return "txt";
            }
            set
            {
                extension = value;
            }
        }
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName
        {
            get
            {
                return filename;
            }
            set
            {
                filename = value;
            }
        }
        public string PathToName(string path)
        {
            int pos = path.LastIndexOf(@"\");
            return path.Substring(pos + 1);
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public string UploadStart()
        {
            bool tf = false;
            string returnvalue = "";
            if (PostedFile != null)
            {
                try
                {
                    string fileName = PathToName(PostedFile.FileName);
                    if (filename != "")
                    {
                        fileName = filename;
                    }
                    string _fileName = "";

                    string[] Exten = Extension.Split(',');
                    if (Exten.Length == 0)
                    {
                        returnvalue = "你未设置上传文件类型,系统不允许进行下一步操作!$0";
                    }
                    else
                    {
                        for (int i = 0; i < Exten.Length; i++)
                        {
                            if (fileName.ToLower().EndsWith(Exten[i].ToLower()))
                            {
                                if (PostedFile.ContentLength > FileLength)
                                {
                                    returnvalue = "上传文件限制大小:" + FileLength / 1024 + "kb！$0";
                                }
                                string IsFileex = SavePath + @"\" + fileName;
                                if (!Directory.Exists(SavePath)) { Directory.CreateDirectory(SavePath); }
                                PostedFile.SaveAs(IsFileex);
                                _fileName = fileName;
                                tf = true;
                                returnvalue = IsFileex + "$1";
                            }
                        }
                        if (tf == false)
                        {
                            returnvalue = "只允许上传" + Extension + " 文件!$0";
                        }
                    }
                }
                catch (System.Exception exc)
                {
                    returnvalue = exc.Message;
                }
            }
            else
            {
                returnvalue = "上文件失败!$0";
            }
            return returnvalue;
        }
    }
}
