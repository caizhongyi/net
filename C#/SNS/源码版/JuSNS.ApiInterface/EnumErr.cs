using System;
using System.Collections.Generic;
using System.Text;

namespace JuSNS.ApiInterface
{
    /// <summary>
    /// 错误信息
    /// </summary>
    public enum EnumErr
    {
        /// <summary>
        /// AppKey错误
        /// </summary>
        ErrKey=101,
        /// <summary>
        /// 用户ID不存在
        /// </summary>
        NoExistUserID=102,
        /// <summary>
        /// 用户ID错误
        /// </summary>
        ErrUserID=103,
        /// <summary>
        /// 不支持此字段
        /// </summary>
        ErrField=104,
        /// <summary>
        /// 参数错误
        /// </summary>
        ErrParams=105,
        /// <summary>
        /// 请求类型错误
        /// </summary>
        ErrRequestType=106
    }
}
