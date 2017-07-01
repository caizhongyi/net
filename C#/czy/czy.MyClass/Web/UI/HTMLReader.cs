using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using System.Net;

namespace czy.MyClass.Web.UI
{
    /// <summary>
    /// ReadHTML 的摘要说明
    /// </summary>
    public class HTMLReader
    {
        public HTMLReader()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 获取静态页面字符窜
        /// </summary>
        /// <param name="URL">地址</param>
        /// <param name="encoding">编码</param>
        /// <returns>静态页面字符窜</returns>
        public static string ReadHTML(string URL, Encoding encoding)
        {
            //浏览
            HttpWebRequest HttpWebRequest;
            HttpWebResponse WebResponse;
            Stream getStream;
            StreamReader streamReader;
            string getString;
            HttpWebRequest = (HttpWebRequest)WebRequest.Create(URL);//传进来的地址
            HttpWebRequest.Accept = "*/*";
            HttpWebRequest.Referer = "http://www.XXX.cn/";
            //HttpWebRequest.CookieContainer = co;//这个最重要了，就是COOKIE，你在登入的时候也会用到吧？比如说先得到验证码，这个时候就肯定需要获取COOKIE然后再来登入，否则会一直提示验证错误，就是这个，自己改吧
            HttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727)";
            HttpWebRequest.Method = "Get";
            WebResponse = (HttpWebResponse)HttpWebRequest.GetResponse();
            getStream = WebResponse.GetResponseStream();
            streamReader = new StreamReader(getStream, encoding);
            getString = streamReader.ReadToEnd();
            streamReader.Close();
            getStream.Close();
            return getString;//这里返回的就是网页代码了
        }

        /// <summary>
        /// WebClient下载数据
        /// </summary>
        /// <param name="URL">网址</param>
        /// <returns></returns>
        public static string WebClientReadHTML(string URL)
        {
            string result = string.Empty;
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Encoding = Encoding.UTF8;
                    result = wc.DownloadString(URL);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;

        }
        /// <summary>
        /// 以数据流的型式读取HTML[UTF8编码]
        /// </summary>
        /// <param name="path">物理路径</param>
        /// <returns></returns>
        public static string StreamReadHTML(string path)
        {  
            string result = string.Empty;
            if (File.Exists(path))
            {
                try
                {

                    using (StreamReader sr = new StreamReader(path, Encoding.UTF8))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                catch { }
            }
            else
            {
                result = "error:模板" + path + "不存在!";
            }
            return result;
        }
    }
}