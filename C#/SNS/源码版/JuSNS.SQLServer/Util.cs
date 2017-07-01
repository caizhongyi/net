using System;
using System.Collections.Generic;
using System.Text;
using JuSNS.Model;
using JuSNS.Common;

namespace JuSNS.SQLServer
{
    /// <summary>
    /// SQL Server层通用类
    /// </summary>
    internal class Util
    {
        /// <summary>
        /// 将传入参数做相应的处理(如:加'号)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        static public string GetStrByType(SqlConditionInfo info)
        {
            switch (info.ParamType)
            {
                case TypeCode.DateTime:
                    return "'" + info.ParamValue + "'";
                case TypeCode.Char:
                case TypeCode.String:
                    return "'" + Input.Filter((string)info.ParamValue) + "'";
                default:
                    return info.ParamValue.ToString();
            }
        }
        /// <summary>
        /// 将传入参数做相应的处理(如:加'号)
        /// </summary>
        /// <param name="info"></param>
        /// <param name="blur">1表示前面模糊,2表示后模糊,3表示前后模糊,其余的数字表示非模糊</param>
        /// <returns></returns>
        static public string GetStr(SqlConditionInfo info)
        {
            switch (info.Blur)
            {
                case 1:
                    return "'%" + info.ParamValue + "'";
                case 2:
                    return "'" + info.ParamValue + "%'";
                case 3:
                    return "'%" + info.ParamValue + "%'";
                default:
                    return GetStrByType(info);
            }
        }
    }
}
