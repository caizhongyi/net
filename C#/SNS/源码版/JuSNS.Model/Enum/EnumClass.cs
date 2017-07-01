using System;
using System.Collections.Generic;
using System.Text;

namespace JuSNS.Model
{
    public class EnumClass
    {
        static public string GetFriendDetailType(EnumFriendDetailType type)
        {
            switch (type)
            {
                case EnumFriendDetailType.Schoolmate:
                    return "同学";
                default:
                    return "不认识";
            }
        }

        /// <summary>
        /// 取得邮件通知的值组合，如1,2,3,15,...
        /// </summary>
        /// <returns></returns>
        static public string GetEmailNotifyStr()
        {
            string s = string.Empty;
            foreach (int n in Enum.GetValues(typeof(EnumEmailSet)))
            {
                s += n.ToString();
                s += ",";
            }
            return s;
        }
        
    }
}
