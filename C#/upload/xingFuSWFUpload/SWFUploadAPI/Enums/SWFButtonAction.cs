using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMMENSITY.SWFUploadAPI.Enums
{
    /// <summary>
    /// 文件选择方式
    /// </summary>
    public enum SWFButtonAction
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
}
