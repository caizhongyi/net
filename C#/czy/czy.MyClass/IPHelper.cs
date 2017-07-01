using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Net;
using System.IO;
using System.Data.OleDb;

namespace czy.MyClass.MyIPAddress
{
    /// <summary>
    /// IP信息类
    /// </summary>
    [System.ComponentModel.DataObject]
    public sealed partial class IPHelper
    {

        public IPHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 获取各客户端IP
        /// <summary>
        /// 获取各客户端IP(无视代理)
        /// </summary>
        /// <returns>IP</returns>
        public static string GetRequestHostIP_NoDeputize(Page page)
        {
            string ip;
            if (page.Request.ServerVariables["HTTP_VIA"] != null) // 服务器， using proxy
            {
                //得到真实的客户端地址
                ip = page.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString(); // Return real client IP.
            }
            else//如果没有使用代理服务器或者得不到客户端的ip not using proxy or can't get the Client IP
            {


                // 得到服务端的地址
                ip = page.Request.ServerVariables["REMOTE_ADDR"].ToString(); //While it can't get the Client IP, it will return proxy IP.
            }
            return ip;
        }
        /// <summary>
        /// 获取各客户端IP(无视代理)
        /// </summary>
        /// <returns>IP</returns>
        public static string GetRequestHostIP(Page page)
        {
            return page.Request.UserHostAddress;
        }
        #endregion

        #region  获取客户端DNSName
        /// <summary>
        /// 获取客户端DNSName
        /// </summary>
        /// <param name="page">当前页</param>
        /// <returns></returns>
        public static string GetRequestHostDNSName(Page page)
        {
            return page.Request.UserHostName;
        }
        #endregion

        #region  将IP 地址转化为数字
        /// <summary>
        /// 将IP 地址转化为数字
        /// </summary>
        /// <param name="Ip">IP</param>
        /// <returns></returns>
        public long IPtoNum(string Ip)
        {
            string[] stringip = new string[4];
            stringip = Ip.Split('.');
            long ipnum = Convert.ToInt64((stringip[0])) * 16777216 + Convert.ToInt64(stringip[1]) * 65536 + Convert.ToInt64(stringip[2]) * 256 + Convert.ToInt64(stringip[3]);
            return ipnum;
        }
        #endregion

        #region 获取客户端的ip地址
        /// <summary>
        /// 获取客户端的ip地址
        /// </summary>
        /// <returns></returns>
        public string GetClientIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            if (null == result || result == String.Empty)
            {
                result = HttpContext.Current.Request.UserHostAddress;
            }
            return result;
        }
        #endregion

        #region HostIP
        /// <summary>
        /// 获取主机IP
        /// </summary>
        /// <returns></returns>
        public static string GetHostIP()
        {
            return ((IPHostEntry)Dns.Resolve(Dns.GetHostName())).AddressList[0].ToString();
        }
        /// <summary>
        /// 获取主机名称
        /// </summary>
        /// <returns></returns>
        public static string GetHostName()
        {
            return Dns.GetHostName();
        }

        /// <summary>
        /// 获取域名
        /// </summary>
        /// <returns></returns>
        public static string GetDomain()
        {
            return HttpContext.Current.Request.Url.ToString().Replace(HttpContext.Current.Request.CurrentExecutionFilePath, "");
        }
        #endregion

        #region EndPoint帮助

        /// <summary>
        /// 获取IP字符窜
        /// </summary>
        /// <param name="endPoint">EndPoint</param>
        /// <returns>IP</returns>
        public static string GetRemoteEndPointAddress(EndPoint endPoint)
        {
            return Convert.ToString(IPAddress.Parse(((IPEndPoint)endPoint).Address.ToString()));
        }
        /// <summary>
        /// 获取端口
        /// </summary>
        /// <param name="endPoint">EndPoint</param>
        /// <returns>端口</returns>
        public static int GetRemoteEndPointPort(EndPoint endPoint)
        {
            return ((IPEndPoint)endPoint).Port;
        }

        #endregion

        #region 跟据网站获取IP所在的地区
        /// <summary>
        /// 跟据网站获取IP所在的地区
        /// </summary>
        /// <param name="ip">ip</param>
        /// <param name="url">IP网站</param>
        /// <returns></returns>
        public static string GetAddressByIP(string ip, string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                url = "http://www.ip138.com/ips8.asp";
            }
            //Encoding myEncoding = Encoding.GetEncoding("gb2312");
            //string param = HttpUtility.UrlEncode("ip", myEncoding) + "=" + HttpUtility.UrlEncode("202.101.98.55", myEncoding);
            string param = "ip=" + ip + "&action=1";
            byte[] bs = Encoding.ASCII.GetBytes(param);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);

            //HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = "POST";
            //req.ContentType = "application/x-www-form-urlencoded;charset=gb2312";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = bs.Length;

            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            using (WebResponse wr = req.GetResponse())
            {

                //在这里对接收到的页面内容进行处理
                StreamReader stream = new StreamReader(wr.GetResponseStream(), Encoding.Default);
                //StreamWriter sw;
                string res = stream.ReadToEnd();
                stream.Close();
                //sw = File.CreateText(Server.MapPath("Test12.htm"));

                //sw.WriteLine(res);

                //sw.Close();

                //Response.WriteFile(Server.MapPath("Test12.htm"));

                int start = res.IndexOf("<ul ");
                int length = res.LastIndexOf("</ul>") - start;
                res = res.Substring(start, length);

                start = res.IndexOf("：") + 1;
                length = res.LastIndexOf("参考数据一") - start - 9;
                res = res.Substring(start, length);

                return res;
            }
        }
        #endregion
    }
 



}