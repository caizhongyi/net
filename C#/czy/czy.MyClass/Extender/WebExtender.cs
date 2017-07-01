using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
public static class WebExtender
{
    /// <summary>
    /// 获取安全的GET参数，以防址SQL注入
    /// </summary>
    /// <param name="request">HttpRequest</param>
    /// <param name="queryStringName">参数名称</param>
    /// <returns></returns>
    public static string SQLSafeQueryString(this HttpRequest request, string queryStringName)
    {
        if (request.QueryString[queryStringName] != null)
        {
            string value = request.QueryString[queryStringName];
            string unsafeString = "',select,union,insert,update,join,sp_,drop,\",exec,xp_";
            string[] items = unsafeString.Split(',');
            foreach (string item in items)
            {
                if (value.ToLower().IndexOf(item) != -1)
                {
                   //czy.MyClass.Web.JavaScript.Alert("非法字符！");
                    return string.Empty;
                }
            }
            return value;
        }
        else
        {
            return string.Empty;
        }
    }
}

