using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;
using System.Web;

namespace czy.MyClass.Web
{
    /// <summary>
    /// 控件上传文件
    /// </summary>
    public class UpLoadFile
    {
        #region 成员
        private int _uploadFileLimit = 0;//上传文件数量限制
        private string[] _fileNames;
        private string _savePath;
        private string _uploadFileVirtualPath;
        private UpLoadResoult[] _resoult;
        private UpLoadCountResoult _countResoult;
        private Int64 _fileMaxSize;
        private Int64[] _fileSizes;
        private FileType _filesType;
        private int _fileCount = 0;

        /// <summary>
        /// 上传的文件个数
        /// </summary>
        public int FileCount
        {
            get { return _fileCount; }
            set { _fileCount = value; }
        }

        /// <summary>
        /// 上传文件类型
        /// </summary>
        public FileType FilesType
        {
            get { return _filesType; }
            set { _filesType = value; }
        }

        /// <summary>
        /// 上传文件的大小
        /// </summary>
        public Int64[] FileSizes
        {
            get { return _fileSizes; }
            set { _fileSizes = value; }
        }

        /// <summary>
        /// 上传文件的大小
        /// </summary>
        public Int64 FileMaxSize
        {
            get { return _fileMaxSize; }
            set { _fileMaxSize = value; }
        }

        /// <summary>
        /// 上传文件个数返回结果
        /// </summary>
        public UpLoadCountResoult CountResoult
        {
          get { return _countResoult; }
          set { _countResoult = value; }
        }
        /// <summary>
        /// 上传文件返回结果
        /// </summary>
        public UpLoadResoult[] Resoult
        {
          get { return _resoult; }
          set { _resoult = value; }
        }
        /// <summary>
        /// 上传文件数
        /// </summary>
        public  int UploadFileLimit
        {
            get { return _uploadFileLimit; }
        }

        /// <summary>
        /// 上传的文件名
        /// </summary>
        public string[] FileNames
        {
            get { return _fileNames; }
        }

     
        /// <summary>
        /// 文件保存路径(不包括文件名)
        /// </summary>
        public string SavePath
        {
            get { return _savePath; }
            set { _savePath = value; }
        }

        ///// <summary>
        /////  文件保存路径包括文件名
        ///// </summary>
        //public string UploadFileVirtualPath
        //{
        //    get { return _uploadFileVirtualPath; }
        //    set { _uploadFileVirtualPath = value; }
        //}

        /// <summary>
        /// 上传文件状态返回的结果
        /// </summary>
        public enum UpLoadResoult
        {
            /// <summary>
            /// 上传失败
            /// </summary>
            Fail,
            /// <summary>
            /// 上传成功
            /// </summary>
            Sucess,
            /// <summary>
            /// 上传文件格式错误
            /// </summary>
            FormatError,
            /// <summary>
            /// 其它错误
            /// </summary>
            Error,
            /// <summary>
            /// 存在文件
            /// </summary>
            ExistFile,
            /// <summary>
            /// 超过文件大小
            /// </summary>
            OverFlowSize,
            /// <summary>
            /// 上传文件个数错误
            /// </summary>
            UpLoadCountError,
            /// <summary>
            /// 无文件名称
            /// </summary>
            NoFileName

        }
        /// <summary>
        /// 上传文件个数返回结果
        /// </summary>
        public enum UpLoadCountResoult
        {    
             /// <summary>
             /// 超过文件数
             /// </summary>
             OverFlowFile,
             /// <summary>
             /// 没有上传文件
             /// </summary>
             NoUpLoadFile,
            /// <summary>
            /// 无错返回
            /// </summary>
             Sucess
        }

        /// <summary>
        /// 上传的文件类型
        /// </summary>
        public enum FileType
        {
            All,
            Image,
            Media,
            Flash,
            Exe,
            Rar,
            Other
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 获取上传文件名后缀
        /// </summary>
        /// <param name="FileUpload">FileUpload</param>
        /// <returns>返回文件扩展名(包括点) </returns>
        private string GetNeWext(FileUpload FileUpload)
        {
            string FileName1 = FileUpload.PostedFile.FileName; //获取初始文件名 
            int i = FileName1.LastIndexOf("."); //取得文件名中最后一个"."的索引 
            string newext = FileName1.Substring(i); //获取文件扩展名(包括点) 
            return newext;
        }
        /// <summary>
        /// 获取上传文件名
        /// </summary>
        /// <param name="FileUpload">FileUpload</param>
        /// <returns>返回文件名包括后缀</returns>
        private string GetFileName(FileUpload FileUpload)
        {

            string filename = FileUpload.PostedFile.FileName;
            int i = filename.LastIndexOf(@"\");
            string name = filename.Substring(i + 1);
            return name;
        }
        private string Getpath(FileUpload FileUpload)
        {
            string filename = FileUpload.PostedFile.FileName;
            return filename;
        }


        /// <summary>
        /// 是否是图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool isImage(string fileName)
        {
            int i = fileName.LastIndexOf("."); //取得文件名中最后一个"."的索引 
            string newext = fileName.Substring(i); //获取文件扩展名(包括点) 
            string imgFormat = newext.ToLower();
            if (imgFormat == ".jpg" || imgFormat == ".gif" || imgFormat == ".bmp" || imgFormat == ".png" || imgFormat == ".jpeg")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 是否是flash
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool IsFlash(string fileName)
        {
            int i = fileName.LastIndexOf("."); //取得文件名中最后一个"."的索引 
            string newext = fileName.Substring(i); //获取文件扩展名(包括点) 
            string imgFormat = newext.ToLower();
            if (imgFormat == ".swf" || imgFormat == ".fla")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 是否应用文件
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private bool IsExe(string fileName)
        {
            int i = fileName.LastIndexOf("."); //取得文件名中最后一个"."的索引 
            string newext = fileName.Substring(i); //获取文件扩展名(包括点) 
            string imgFormat = newext.ToLower();
            if (imgFormat == ".exe")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsMedia(string fileName)
        {
            return true;
        }
        private bool IsRar(string fileName)
        {
            int i = fileName.LastIndexOf("."); //取得文件名中最后一个"."的索引 
            string newext = fileName.Substring(i); //获取文件扩展名(包括点) 
            string imgFormat = newext.ToLower();
            if (imgFormat == ".rar" || imgFormat == ".zip")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        #endregion

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="uploadFileLimit">一次信上传的文件数</param>
        /// <param name="savePath">文件保存路径（不包括文件名）</param>
        /// <param name="maxSize">文件最大值</param>
        public UpLoadFile(int uploadFileLimit, string savePath, Int64 maxSize)
        {

            this._uploadFileLimit = uploadFileLimit;
            this._savePath = savePath;
            this._fileMaxSize = maxSize;
            this._filesType = FileType.All;

        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="uploadFileLimit">一次信上传的文件数</param>
        /// <param name="savePath">文件保存路径（不包括文件名）</param>
        public UpLoadFile(int uploadFileLimit,string savePath)
        {

            this._uploadFileLimit = uploadFileLimit;
            this._savePath = savePath;
            this._filesType = FileType.All;
            
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="savePath">文件保存路径（不包括文件名）</param>
        public UpLoadFile(string savePath)
        {
            this._savePath = savePath;
            this._filesType = FileType.All;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="savePath">文件保存路径（不包括文件名）</param>
        /// <param name="maxSize">文件最大值</param>
        public UpLoadFile(string savePath, Int64 maxSize)
        {
            this._savePath = savePath;
            this._fileMaxSize = maxSize;
            this._filesType = FileType.All;
        }



        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="uploadFileLimit">一次信上传的文件数</param>
        /// <param name="savePath">文件保存路径（不包括文件名）</param>
        /// <param name="maxSize">文件最大值</param>
        public UpLoadFile(int uploadFileLimit, string savePath, Int64 maxSize, FileType filesType)
        {

            this._uploadFileLimit = uploadFileLimit;
            this._savePath = savePath;
            this._fileMaxSize = maxSize;
            this._filesType = filesType;
           
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="uploadFileLimit">一次信上传的文件数</param>
        /// <param name="savePath">文件保存路径（不包括文件名）</param>
        public UpLoadFile(int uploadFileLimit, string savePath, FileType filesType)
        {

            this._uploadFileLimit = uploadFileLimit;
            this._savePath = savePath;
            this._filesType = filesType;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="savePath">文件保存路径（不包括文件名）</param>
        public UpLoadFile(string savePath, FileType filesType)
        {
            this._savePath = savePath;
            this._filesType = filesType;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="savePath">文件保存路径（不包括文件名）</param>
        /// <param name="maxSize">文件最大值</param>
        public UpLoadFile(string savePath, Int64 maxSize,FileType filesType)
        {
            this._savePath = savePath;
            this._fileMaxSize = maxSize;
            this._filesType = filesType;
        }
        #endregion
        /// <summary>
        /// 获取上传文件的总数
        /// </summary>
        /// <returns></returns>
        public int GetFileCount()
        {
            return this._fileCount=HttpContext.Current.Request.Files.Count;
        }

        #region 无刷新文件上传
        /// <summary>
        /// 检查文件的个数
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <returns>检查文件的个数返回结果</returns>
        public UpLoadCountResoult CheckFileCount()
        {
            this._uploadFileLimit = this._uploadFileLimit == 0 ? HttpContext.Current.Request.Files.Count : this._uploadFileLimit;
            GetFileCount();
            if (this._uploadFileLimit == 0)
            {
                this._countResoult = UpLoadCountResoult.NoUpLoadFile;
                return this._countResoult;
            }
            else if (this._uploadFileLimit < HttpContext.Current.Request.Files.Count)
            {
                this._countResoult= UpLoadCountResoult.OverFlowFile;
                return this._countResoult;
            }
            else 
            {
                this._countResoult= UpLoadCountResoult.Sucess;
                return this._countResoult;
            }
        }
        /// <summary>
        /// 验证文件格式
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        private FileType CheckFileType(string fileName)
        {

            if (this._filesType == FileType.All)
            {
                return FileType.All;
            }
            if (IsExe(fileName))
            {
                return FileType.Exe;
            }
            else if (IsFlash(fileName))
            {
                return FileType.Flash;
            }
            else if (isImage(fileName))
            {
                return FileType.Image;
            }
            else if (IsRar(fileName))
            {
                return FileType.Rar;
            }
            //else if (IsMedia(fileName))
            //{
            //    return FileType.Other;
            //}
            else
            {
                return FileType.Other;
            }
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="fileNames">重命名文件名</param>
        /// <returns>上传文件状态返回</returns>
        public UpLoadResoult[] UpLoad(string[] fileNames)
        {
            GetFileCount();
            this._uploadFileLimit = this._uploadFileLimit == 0 ? HttpContext.Current.Request.Files.Count : this._uploadFileLimit;
            this._fileNames = new string[this._uploadFileLimit];
            this._resoult = new UpLoadResoult[this._uploadFileLimit];
            this._fileSizes = new Int64[this._uploadFileLimit];
            for (int i = 0; i < this._uploadFileLimit; i++)
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[i];
                this._fileSizes[i] = file.ContentLength;
                this._fileMaxSize = this._fileMaxSize == 0 ? file.ContentLength + 1 : this._fileMaxSize;
                if (file.ContentLength > this._fileMaxSize)
                {
                    this._resoult[i] = UpLoadResoult.OverFlowSize;
                    continue;
                }
                else if (string.IsNullOrEmpty(file.FileName))
                {
                    this._resoult[i] = UpLoadResoult.NoFileName;
                }
                else
                {
                    string fileName = this.FileNames[i];
                    if (CheckFileType(fileName)!=this._filesType)
                    {
                        this._resoult[i] = UpLoadResoult.FormatError;
                        continue;
                    }
                    string path = this._savePath + "\\" + fileName;
                    try
                    {
                        if (!File.Exists(path))
                        {
                            file.SaveAs(path); // 保存文件到路径,用Server.MapPath()取当前文件的绝对目录.在asp.net里"\"必须用""代替
                            this._resoult[i] = UpLoadResoult.Sucess;
                            continue;
                        }
                        else
                        {
                            this._resoult[i] = UpLoadResoult.ExistFile;
                            continue;
                        }
                    }
                    catch
                    {
                        this._resoult[i] = UpLoadResoult.Error;
                        continue;
                    }
                }

            }
            return this._resoult;
  
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="fileNames">重命名文件名</param>
        /// <returns>上传文件状态返回</returns>
        public UpLoadResoult[] UPLoad()
        {
            GetFileCount();
            this._uploadFileLimit = this._uploadFileLimit == 0 ? HttpContext.Current.Request.Files.Count : this._uploadFileLimit;
            this._fileNames = new string[this._uploadFileLimit];
            this._resoult = new UpLoadResoult[this._uploadFileLimit];
            this._fileSizes = new Int64[this._uploadFileLimit];
            for (int i = 0; i < this._uploadFileLimit; i++)
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[i];
                this._fileSizes[i] = file.ContentLength;
                this._fileMaxSize = this._fileMaxSize == 0 ? file.ContentLength + 1 : this._fileMaxSize;
                if (file.ContentLength > this._fileMaxSize)
                {
                    this._resoult[i] = UpLoadResoult.OverFlowSize;
                    continue;
                }
                else if (string.IsNullOrEmpty(file.FileName))
                {
                    this._resoult[i] = UpLoadResoult.NoFileName;
                }
                else
                {
                    string fileName = file.FileName;
                    this._fileNames[i] = fileName;
                    if (CheckFileType(fileName) != this._filesType)
                    {
                        this._resoult[i] = UpLoadResoult.FormatError;
                        continue;
                    }

                    string path = this._savePath + "\\" + fileName;
                    try
                    {
                        if (!File.Exists(path))
                        {
                            file.SaveAs(path); // 保存文件到路径,用Server.MapPath()取当前文件的绝对目录.在asp.net里"\"必须用""代替
                            this._resoult[i] = UpLoadResoult.Sucess;
                            continue;
                        }
                        else
                        {
                            this._resoult[i] = UpLoadResoult.ExistFile;
                            continue;
                        }
                    }
                    catch
                    {
                        this._resoult[i] = UpLoadResoult.Error;
                        continue;
                    }
                }

            }
            return this._resoult;

        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="page">当前页面</param>
        /// <param name="fileNames">重命名文件名</param>
        /// <returns>上传文件状态返回</returns>
        public UpLoadResoult[] UpLoadReNameByDateTime()
        {
            this._uploadFileLimit = this._uploadFileLimit == 0 ? HttpContext.Current.Request.Files.Count : this._uploadFileLimit;
            this._fileNames = new string[this._uploadFileLimit];
            this._resoult = new UpLoadResoult[this._uploadFileLimit];
            this._fileSizes = new Int64[this._uploadFileLimit];
            for (int i = 0; i < this._uploadFileLimit; i++)
            {
                HttpPostedFile file = HttpContext.Current.Request.Files[i];
                this._fileSizes[i] = file.ContentLength;

                this._fileMaxSize = this._fileMaxSize == 0 ? file.ContentLength + 1 : this._fileMaxSize;
                if (file.ContentLength > this._fileMaxSize)
                {
                    this._resoult[i] = UpLoadResoult.OverFlowSize;
                    continue;
                }
                else if (string.IsNullOrEmpty(file.FileName))
                {
                    this._resoult[i] = UpLoadResoult.NoFileName;
                }
                else
                {
                    string fileFormat = file.FileName.Substring(file.FileName.LastIndexOf('.'));//包含.
                    string fileName = DateTime.Now.ToString("yyyyMMddhhssmm");
                    this._fileNames[i] = fileName + fileFormat;
                    if (CheckFileType(fileName) != this._filesType)
                    {
                        this._resoult[i] = UpLoadResoult.FormatError;
                        continue;
                    }

                    string path = this._savePath + "\\" + fileName;
                    try
                    {
                        if (!File.Exists(path))
                        {
                            file.SaveAs(path); // 保存文件到路径,用Server.MapPath()取当前文件的绝对目录.在asp.net里"\"必须用""代替
                            this._resoult[i] = UpLoadResoult.Sucess;
                        }
                        else
                        {
                            this._resoult[i] = UpLoadResoult.ExistFile;
                            continue;
                        }
                    }
                    catch
                    {
                        this._resoult[i] = UpLoadResoult.Error;
                        continue;
                    }
                }

            }
            return this._resoult;
        }

        #endregion
       

        #region BinaryRead上传
        //public  string  JsUpLoad(string fileName, string partid,Page page,string UpLoadPath)
        //{
        //    //string username;

        //    /////文件模块编号
        //    //string partid = Request.QueryString["ps"];
        //    //this.username = Request.QueryString["userName"];
        //    //string fileType = Request.QueryString["fileType"];
        //    //if (partid == "" || partid == null || username == "" || username == null)
        //    //{
        //    //    Response.Write("系统非法访问!");
        //    //    return;
        //    //}

        //    //string filename = username + "考场编排" + fileType;
        //    //string strReturn = "true";

        //    try
        //    {
        //        ///获得文件上传目录
        //        string upPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, UpLoadPath);
        //        if (!upPath.EndsWith("\\") && !upPath.EndsWith("/"))
        //        {
        //            upPath += "\\";
        //        }
        //        if (!Directory.Exists(upPath))
        //        {
        //            Directory.CreateDirectory(upPath);
        //        }
        //        string strDir = upPath;
        //        ///执行文件删除
        //        if (partid == "-1")
        //        {
        //            //filename = Request.Form["fns"].ToString();
        //            if (System.IO.File.Exists(strDir + fileName))
        //            {
        //                System.IO.File.Delete(strDir + fileName);
        //            }
                    
        //        }

        //        ///执行文件上传操作
        //        if (partid == "0")
        //        {
        //            if (!System.IO.Directory.Exists(strDir))
        //            {
        //                System.IO.Directory.CreateDirectory(strDir);
        //            }
        //            if (System.IO.File.Exists(strDir + fileName))
        //            {
        //                System.IO.File.Delete(strDir + fileName);
        //            }
        //        }
        //        byte[] buffer = page.Request.BinaryRead(Convert.ToInt32(page.Request.ContentLength));
        //        System.IO.FileStream FS = new System.IO.FileStream(strDir + fileName, System.IO.FileMode.Append);
        //        FS.Write(buffer, 0, buffer.Length);
        //        FS.Close();

        //        return "true";
        //    }
        //    catch (Exception ex)
        //    {
        //        return  "上传文件出错: " + ex.Message;

        //    }
        //}

        #endregion

        #region 控件上传
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="SaveFilePath">文件存储路径,不包括文件名</param>
        /// <param name="FileUpload">上传控件</param>
        /// <returns>文件名(注:如格式不对,或没上传成功,为format error或error)</returns>
        public UpLoadResoult FileUpLoadByControl(FileUpload FileUpload)
        {
                    string fileName = FileUpload.FileName;
               
                    string imgFormat = GetNeWext(FileUpload);
                    fileName = DateTime.Now.ToString("yyyyMMddhhssmm") + imgFormat;
                    string path =  _savePath + fileName;
                    _uploadFileVirtualPath = _savePath + fileName;
                    if (!File.Exists(path))
                    {
                        FileUpload.PostedFile.SaveAs(path); // 保存文件到路径,用Server.MapPath()取当前文件的绝对目录.在asp.net里"\"必须用""代替 
                        return UpLoadResoult.Sucess;
                    }
                    else
                    {
                        return UpLoadResoult.ExistFile;
                    }

        }

        /// <summary>
        /// 静态控件 File控件上传
        /// </summary>
        /// <param name="file">file控件</param>
        /// <param name="page">页面</param>
        public UpLoadResoult FileUpLoadByControl(System.Web.UI.HtmlControls.HtmlInputFile file, Page page)
        {

            try
            {
                string filename = Path.GetFileName(file.PostedFile.FileName.ToString());
                string path = page.Server.MapPath(_savePath) + "\\" + filename;
                if (!File.Exists(path))
                {
                    file.PostedFile.SaveAs(path);
                    return UpLoadResoult.Sucess;
                }
                else
                {
                    return UpLoadResoult.ExistFile;
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

    }
}


