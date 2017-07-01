using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace czy.MyClass.Web
{
    /// <summary>
    /// HTTP访问类
    /// </summary>
    public class HttpClient
    {
        /// <summary>
        /// 编码格式
        /// </summary>
        private EncodingType _endcodingType = EncodingType.UTF8;
        /// <summary>
        /// 编码类型
        /// </summary>
        public EncodingType EndcodingType
        {
            get { return _endcodingType; }
            set { _endcodingType = value; }
        }
        /// <summary>
        /// 编码类型
        /// </summary>
        public enum EncodingType
        {
            GB2312,
            UTF8
        }
        /// <summary>
        /// Post访问方式
        /// </summary>
        /// <param name="url"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public string PostUrl(string url, string param)
        {
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            string code="UTF-8";
            switch(_endcodingType)
            {
                case EncodingType.GB2312:code="GB2312";break ;
                case EncodingType.UTF8:code="UTF-8";break ;
                default:code="UTF-8";break ;
            }
            byte[] bs = Encoding.GetEncoding(code).GetBytes(param);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = bs.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            HttpWebResponse rep = (HttpWebResponse)req.GetResponse();
            Stream receiveStream = rep.GetResponseStream();
            
            Encoding encode = System.Text.Encoding.GetEncoding(code);
            // Pipes the stream to a higher level stream reader with the required encoding format. 
            if (receiveStream.CanRead)
            {
                StreamReader readStream = new StreamReader(receiveStream, encode);


                Char[] read = new Char[256];
                int count = readStream.Read(read, 0, 256);
                StringBuilder sb = new StringBuilder("");
                while (count > 0)
                {
                    String readstr = new String(read, 0, count);
                    sb.Append(readstr);
                    count = readStream.Read(read, 0, 256);
                }

                rep.Close();
                readStream.Close();
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
           
        }
        /// <summary>
        /// Get访问方式
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public string GetUrl(string url)
        {
            string rt = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "text/HTML";
            // HttpUtility.            

            // Set some reasonable limits on resources used by this request
            request.MaximumAutomaticRedirections = 4;
            request.MaximumResponseHeadersLength = 4;
            // Set credentials to use for this request.
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            // Get the stream associated with the response.
            Stream receiveStream = response.GetResponseStream();
            
            // Pipes the stream to a higher level stream reader with the required encoding format. 
            if (receiveStream.CanRead)
            {
                StreamReader readStream;
                switch (_endcodingType)
                {
                    case EncodingType.GB2312: readStream = new StreamReader(receiveStream, Encoding.Default); break;
                    case EncodingType.UTF8: readStream = new StreamReader(receiveStream, Encoding.UTF8); break;
                    default: readStream = new StreamReader(receiveStream, Encoding.UTF8); break;
                }

                int Rts = int.Parse(readStream.ReadToEnd());
                response.Close();
                readStream.Close();
            }
            return rt;
        }
        public  WebHeaderCollection GetHeaders(Uri uri)
   	{

        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

        // 省略部分代码...... 

        HttpWebResponse response = (HttpWebResponse)request.GetResponse();

        // 省略部分代码...... 

        return response.Headers;

       }
     
    }
}
