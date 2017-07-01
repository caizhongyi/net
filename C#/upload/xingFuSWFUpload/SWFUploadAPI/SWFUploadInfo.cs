using System;

namespace IMMENSITY.SWFUploadAPI
{
    [Serializable]
    public class SWFUploadInfo
    {
        /// <summary>
        /// 默认无参构造函数
        /// </summary>
        public SWFUploadInfo()
        {
            this.Upload_url = "/Upload/index";
            this.Path = "/upload/";
            this.File_size_limit = 2;
            this.File_types = "*.*";
            this.File_types_description = "所有文件";
            this.File_upload_limit = 0;
            this.Exist_file_count = 0;
            this.Button_action = BUTTON_ACTION.SELECT_FILES;

            this.OldFileName = string.Empty;
            this.IsSmall = false;
            this.SmallWidth = 0;
            this.SmallHeight = 0;
            this.IsWaterMark = false;

            this.UploadMode = UpMode.BUTTON;
            this.SubmitButtonId = string.Empty;
        }
        #region 与SWFUpload属性相同
        /// <summary>
        /// 文件数据处理URL(默认:/Upload/index)
        /// </summary>
        public string Upload_url { get; set; }

        /// <summary>
        /// 文件存放相对路径(默认:/upload/)
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// 文件大小(MB)(默认:2)
        /// </summary>
        public int File_size_limit { get; set; }

        /// <summary>
        /// 可上传的文件类型(默认:*.*)
        /// </summary>
        public string File_types { get; set; }

        /// <summary>
        /// 可上传的文件类型描述(默认:All File)
        /// </summary>
        public string File_types_description { get; set; }

        /// <summary>
        /// 上传文件数量(默认:0,表示无穷大)
        /// </summary>
        public int File_upload_limit { get; set; }
        #endregion

        #region 自定义属性
        /// <summary>
        /// 已存在文件数量(默认:0,主要用于限制用户上传文件数量)
        /// </summary>
        public int Exist_file_count { get; set; }

        /// <summary>
        /// 文件选择方式(单文件,多文件,启动文件上传)
        /// </summary>
        public BUTTON_ACTION Button_action { get; set; }

        /// <summary>
        /// 旧图片名称,以便删除
        /// </summary>
        public string OldFileName { get; set; }

        /// <summary>
        /// 是否需要小图
        /// </summary>
        public bool IsSmall { get; set; }

        /// <summary>
        /// 是否需要水印
        /// </summary>
        public bool IsWaterMark { get; set; }

        /// <summary>
        /// 小图宽度
        /// </summary>
        public int SmallWidth { get; set; }

        /// <summary>
        /// 小图高度
        /// </summary>
        public int SmallHeight { get; set; }

        /// <summary>
        /// 上传界面模式
        /// </summary>
        public UpMode UploadMode { get; set; }

        /// <summary>
        /// 表单提交ID
        /// </summary>
        public string SubmitButtonId { get; set; }
        #endregion
    }
    /// <summary>
    /// 文件选择方式
    /// </summary>
    public enum BUTTON_ACTION
    {
        /// <summary>
        /// 单文件上传
        /// </summary>
        SELECT_FILE,
        /// <summary>
        /// 多文件上传
        /// </summary>
        SELECT_FILES,
        /// <summary>
        /// 启动文件上传
        /// </summary>
        START_UPLOAD
    }
    /// <summary>
    /// 上传界面模式
    /// </summary>
    public enum UpMode
    {
        /// <summary>
        /// 单独按钮模式
        /// </summary>
        BUTTON,
        /// <summary>
        /// 显示列表模式
        /// </summary>
        LIST
    }
}
