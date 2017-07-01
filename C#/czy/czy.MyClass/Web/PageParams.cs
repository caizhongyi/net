using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web.UI;
using System.Net;
using System.Web;

namespace czy.MyClass.Web
{
    public class PageParamHelper
    {
        #region 变量
        #endregion

        #region  获取Get传参的值
        /// <summary>
        /// 获取Get传参的值
        /// </summary>
        /// <param name="name">变量名</param>
        /// <param name="page">当前页面</param>
        /// <returns></returns>
        public static string GetQueryStringValue(string name)
        {
            if (HttpContext.Current.Request.QueryString[name] != null)
            {
                return HttpContext.Current.Request.QueryString[name];
            }
            else
            {
                return string.Empty;
            }
        }
        /// <summary>
        /// 获取Get传来的参数
        /// </summary>
        /// <param name="webGetParamNameList">参数名称</param>
        /// <param name="page">页面</param>
        /// <returns>返回值</returns>
        public static string[] GetQueryStringValue(string[] paramNames)
        {
            string[] temp = new string[paramNames.Length];
            int i=0;
            foreach (string o in paramNames)
            {
                temp[i] = HttpContext.Current.Request.QueryString[o].ToString();
                i++;
            }
            return temp;
        }
        /// <summary>
        /// 获取Get传来的参数
        /// </summary>
        /// <param name="webGetParamNameList">参数名称</param>
        /// <param name="page">页面</param>
        /// <returns>返回值</returns>
        public static object[] GetQueryStringValue(object[] paramNames)
        {
            object[] temp = new object[paramNames.Length];
            int i = 0;
            foreach (object o in paramNames)
            {
                temp[i] = HttpContext.Current.Request.QueryString[o.ToString()];
                i++;
            }
            return temp;
        }
        /// <summary>
        /// 获取Get传来的参数
        /// </summary>
        /// <param name="webGetParamNameList">参数名称</param>
        /// <param name="page">页面</param>
        /// <returns>返回值</returns>
        public static ArrayList GetQueryStringValue(ArrayList paramNames)
        {
              ArrayList alist = new ArrayList();
              foreach (object o in paramNames)
              {
                  alist.Add((HttpContext.Current.Request.QueryString [o.ToString ()]) as Object);
              }
              return alist;
        }
        #endregion


        /// <summary>
        /// WebClient向网页传参数
        /// </summary>
        /// <param name="strList">字符数组</param>
        /// <param name="variableList">变量数组</param>
        /// <param name="url">网址</param>
        /// <param name="Method">传值方式,"post"或"get"</param>
        /// <returns>返回参数</returns>
        public static string PostParams(List<ParamInfo> list, string url, string Method)
        {
           
            WebClient w = new WebClient();
            System.Collections.Specialized.NameValueCollection VarPost = new System.Collections.Specialized.NameValueCollection();
            for (int i = 0; i < list.Count; i++)
            {
                VarPost.Add(list[i].ParamName.ToString (), list[i].ParamValue.ToString ());//将textBox1中的数据变为用a标识的参数，并用POST传值方式传给网页 
            }
            byte[] byRemoteInfo = w.UploadValues(url, Method, VarPost);//将参数列表VarPost中的所有数据用POST传值的方式传给http://申请好的域名/Default.aspx，并将数据以字节流存放到byRemoteInfo中(注：该网页应传到服务器上才能使用) 
            string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);
            return sRemoteInfo;
        }

        /// <summary>
        /// WebClient向网页传参数
        /// </summary>
        /// <param name="strList">字符数组</param>
        /// <param name="variableList">变量数组</param>
        /// <param name="url">网址</param>
        /// <param name="Method">传值方式,"post"或"get"</param>
        /// <returns>返回参数</returns>
        public static string PostParams(Hashtable ht, string url, string Method)
        {

            WebClient w = new WebClient();
            System.Collections.Specialized.NameValueCollection VarPost = new System.Collections.Specialized.NameValueCollection();
            foreach (DictionaryEntry  d in ht )
            {
                VarPost.Add(d.Key.ToString (),d.Value.ToString ());//将textBox1中的数据变为用a标识的参数，并用POST传值方式传给网页 
            }
            byte[] byRemoteInfo = w.UploadValues(url, Method, VarPost);//将参数列表VarPost中的所有数据用POST传值的方式传给http://申请好的域名/Default.aspx，并将数据以字节流存放到byRemoteInfo中(注：该网页应传到服务器上才能使用) 
            string sRemoteInfo = System.Text.Encoding.UTF8.GetString(byRemoteInfo);
            return sRemoteInfo;
        }
    }

    public class ParamInfo
    {
        private object _paramName;

        public object ParamName
        {
            get { return _paramName; }
            set { _paramName = value; }
        }
        private object _paramValue;

        public object ParamValue
        {
            get { return _paramValue; }
            set { _paramValue = value; }
        }
    }
}
