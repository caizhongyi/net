using System;
using System.IO;
using System.Web;
using System.Xml;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using JuSNS.Config;
using JuSNS.Model;

namespace JuSNS.Common
{
    public class Public
    {
        /// <summary>
        /// 去掉结尾,
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string LostDot(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            else
            {
                if (input.IndexOf(",") > -1)
                {
                    int intLast = input.LastIndexOf(",");
                    if ((intLast+1) == input.Length)
                    {
                        return input.Remove(intLast);
                    }
                    else
                    {
                        return input;
                    }
                }
                else
                {
                    return input;
                }
            }
        }

        /// <summary>
        /// 根目录
        /// </summary>
        public static string rootDir
        {
            get
            {
                string root = HttpContext.Current.Request.ApplicationPath;
                if (root == "/") root = string.Empty;
                return root;
            }
        }

        /// <summary>
        /// 根目录
        /// </summary>
        public static string ExName
        {
            get
            {
                return GetXMLValue("siteExName");
            }
        }

        /// <summary>
        /// 得到积分
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static int JSplit(int num)
        {
            try
            {
                string[] arr = GetXMLValue("intelnum").Split(',');
                return Convert.ToInt32(arr[num]);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 得到可见度
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        static public string GetPrivacy(object k)
        {
            string listSTR = string.Empty;
            string[] ARR = { "全站用户可见", "仅好友可见", "仅自己可见" };
            for (int j = 0; j < ARR.Length; j++)
            {
                if (Convert.ToInt16(k) == j)
                {
                    listSTR += "<option value=\"" + j + "\" selected>" + ARR[j] + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + j + "\">" + ARR[j] + "</option>";
                }
            }
            return listSTR;
        }

        /// <summary>
        /// 得到可见度
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        static public string GetPublics(object k)
        {
            string listSTR = string.Empty;
            string[] ARR = { "所有人可加入", "需要身份验证", "拒绝加入" };
            for (int j = 0; j < ARR.Length; j++)
            {
                if (Convert.ToInt16(k) == j)
                {
                    listSTR += "<option value=\"" + j + "\" selected>" + ARR[j] + "</option>";
                }
                else
                {
                    listSTR += "<option value=\"" + j + "\">" + ARR[j] + "</option>";
                }
            }
            return listSTR;
        }


        /// <summary>
        /// 读取模板
        /// </summary>
        /// <param name="path">模板路径</param>
        /// <returns></returns>
        public static string GetTempleContent(string path)
        {
                string result = string.Empty;
                string sFileName = HttpContext.Current.Server.MapPath(path);
                if (File.Exists(sFileName))
                {
                    try
                    {
                        using (StreamReader sr = new StreamReader(sFileName))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                    catch
                    {
                        result = "读取模板文件(" + path + ")出错";
                    }
                }
                else
                {
                    result = "找不到模板文件：" + path;
                }
                return result;
        }

        /// <summary>
        /// 检查当前IP是否是受限IP
        /// </summary>
        /// <param name="LimitedIP">受限的IP，格式如:192.168.1.110|212.235.*.*|232.*.*.*</param>
        /// <returns>返回true表示IP未受到限制</returns>
        static public bool ValidateIP(string LimitedIP)
        {
            string CurrentIP = GetClientIP();
            if (string.IsNullOrEmpty(LimitedIP))
                return true;
            LimitedIP.Replace(".", @"\.");
            LimitedIP.Replace("*", @"[^\.]{1,3}");
            Regex reg = new Regex(LimitedIP, RegexOptions.Compiled);
            Match match = reg.Match(CurrentIP);
            return !match.Success;
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        public static void DelFile(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch{ }
        }


        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        public static void DelDir(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path);
                }
            }
            catch{ }
        }


        /// <summary>
        /// 取得用户客户端IP(穿过代理服务器取远程用户真实IP地址)
        /// </summary>
        public static string GetClientIP()
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_VIA"] != null)
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else
            {
                return HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            }
        }

        /// <summary>
        /// 取得前一个（上次提交或链接进来的）网页的URL
        /// </summary>
        /// <returns></returns>
        public static string GetReferrerUrl()
        {
            Uri uri = HttpContext.Current.Request.UrlReferrer;
            if (uri != null)
            {
                return uri.ToString();
            }
            else
            {
                return string.Empty;
            }
        }


        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="strTarget">接点名</param>
        /// <param name="strValue">新值</param>
        /// <param name="strSource">路径</param>
        public static void SaveXmlConfig(string strTarget, string strValue, string strSource)
        {
            string xmlPath = HttpContext.Current.Server.MapPath(strSource);
            System.Xml.XmlDocument xdoc = new XmlDocument();
            xdoc.Load(xmlPath);
            XmlElement root = xdoc.DocumentElement;
            XmlNodeList elemList = root.GetElementsByTagName(strTarget);
            elemList[0].InnerXml = strValue;
            xdoc.Save(xmlPath);
        }

        /// <summary>
        /// 取得与当前时间的间隔(MM-dd hh:mm 刚刚更新)
        /// </summary>
        public static string getTimeSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (second < 0) { second = 0; }
            if (days == 0 && hours == 0 && Minutes == 0)
            {
                strTime = "刚刚更新";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("yyyy-MM-dd HH:mm");
            }
            return strTime;
        }


        /// <summary>
        /// 取得与当前时间的间隔(yyyy年MM月dd日 刚刚更新)
        /// </summary>
        public static string getTimeLEXYearSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (days == 0 && hours == 0 && Minutes == 0)
            {
                strTime = "刚刚更新";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("yyyy年MM月dd日");
            }
            return strTime;
        }

        /// <summary>
        /// 取得与当前时间的间隔(yy-MM-dd 刚刚更新)
        /// </summary>
        public static string getTimeEXYearSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (days == 0 && hours == 0 && Minutes == 0)
            {
                strTime = "刚刚更新";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("yyyy-MM-dd");
            }
            return strTime;
        }


        /// <summary>
        /// 取得与当前时间的间隔(MM-dd 刚刚更新)
        /// </summary>
        public static string getTimeEXSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (days == 0 && hours == 0 && Minutes == 0)
            {
                //strTime = second + "秒钟前";
                strTime = "刚刚更新";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("MM-dd");
            }
            return strTime;
        }

        /// <summary>
        /// 取得与当前时间的间隔(MM月dd日 刚刚更新)
        /// </summary>
        public static string getTimeEXPINSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (days == 0 && hours == 0 && Minutes == 0)
            {
                strTime = "刚刚更新";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("MM月dd日");
            }
            return strTime;
        }

        /// <summary>
        /// 取得与当前时间的间隔(MM月dd日 刚刚)
        /// </summary>
        public static string getTimeEXTSpan(DateTime time1)
        {
            string strTime = "";
            DateTime date1 = DateTime.Now;
            DateTime date2 = time1;
            TimeSpan dt = date1 - date2;

            // 相差天数
            int days = dt.Days;
            // 时间点相差小时数
            int hours = dt.Hours;
            // 相差总小时数
            double Minutes = dt.Minutes;
            // 相差总秒数
            int second = dt.Seconds;

            if (days == 0 && hours == 0 && Minutes == 0)
            {
                strTime = "刚刚";
            }
            else if (days == 0 && hours == 0)
            {
                strTime = Minutes + "分钟前";
            }
            else if (days == 0)
            {
                strTime = hours + "小时前";
            }
            else
            {
                strTime = time1.ToString("MM月dd日");
            }
            return strTime;
        }

        /// <summary>
        /// 获取时间相隔天数
        /// </summary>
        /// <param name="time1">时间1</param>
        /// <returns></returns>
        public static string getDaySpan(DateTime time1)
        {
            TimeSpan ts = DateTime.Now - time1;
            return ts.Days.ToString();
        }

        /// <summary>
        /// 读取并返回一个文本文件的内容
        /// </summary>
        /// <param name="filePath">文件的物理路径</param>
        /// <returns></returns>
        public static string GetTextFileContent(string filePath)
        {
            string result = string.Empty;
            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamReader sr = new StreamReader(filePath))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                catch
                { }
            }
            return result;
        }

        /// <summary>
        /// 替换文本中的空格和换行
        /// </summary>
        public static string ReplaceSpace(string str)
        {
            string s = str;
            s = s.Replace(" ", "&nbsp;");
            s = s.Replace("\n", "<BR />");
            return s;
        }

        /// <summary>
        /// 过滤关键字，查看是否是禁用姓名
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="flag">true：关键字 false：姓名</param>
        /// <returns></returns>
        public static string CheckKey(string key, bool flag)
        {
            string fg = "";
            string strKey = "";
            if (flag)
            {
                strKey = JuSNS.Config.SysConfig.KeyWord;
            }
            else
            {
                strKey = JuSNS.Config.SysConfig.UserName;
            }

            string[] Str = strKey.Split('|');
            for (int i = 0; i < Str.Length; i++)
            {
                try
                {
                    if (key.Contains(Str[i].ToString()))
                    {
                        fg = Str[i].ToString();
                        break;
                    }
                }
                catch { }
            }
            return fg;
        }

        /// <summary>
        /// 取得文件扩展名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>扩展名</returns>
        public static string GetFileEXT(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return "";
            }
            if (filename.IndexOf(@".") == -1)
            {
                return "";
            }
            int pos = -1;
            if (!(filename.IndexOf(@"\") == -1))
            {
                pos = filename.LastIndexOf(@"\");
            }
            string[] s = filename.Substring(pos + 1).Split('.');
            return s[1];
        }

        /// <summary>
        /// 读取基本配置 jusite.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLValue(string target)
        {
            return GetXMLValue(target, "~/config/base/jusite.config");
        }

        /// <summary>
        /// 读取基本配置 base.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLBaseValue(string target)
        {
            return GetXMLValue(target, "~/config/base/base.config");
        }

        /// <summary>
        /// 读取基本配置 page.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLPageValue(string target)
        {
            return GetXMLValue(target, "~/config/base/page.config");
        }

        /// <summary>
        /// 读取基本配置 group.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLGroupValue(string target)
        {
            return GetXMLValue(target, "~/config/base/group.config");
        }

        /// <summary>
        /// 读取基本配置 album.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLAlbumValue(string target)
        {
            return GetXMLValue(target, "~/config/base/album.config");
        }

        /// <summary>
        /// 读取基本配置 ask.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLAskValue(string target)
        {
            return GetXMLValue(target, "~/config/base/ask.config");
        }
        /// <summary>
        /// 读取基本配置 ative.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLAtiveValue(string target)
        {
            return GetXMLValue(target, "~/config/base/ative.config");
        }

        /// <summary>
        /// 读取基本配置 shop.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLShopValue(string target)
        {
            return GetXMLValue(target, "~/config/base/shop.config");
        }
        /// <summary>
        /// 读取基本配置 vote.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLVoteValue(string target)
        {
            return GetXMLValue(target, "~/config/base/vote.config");
        }

        /// <summary>
        /// 读取基本配置 share.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLShareValue(string target)
        {
            return GetXMLValue(target, "~/config/base/share.config");
        }
        /// <summary>
        /// 读取基本配置 gift.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLGiftValue(string target)
        {
            return GetXMLValue(target, "~/config/base/gift.config");
        }
        /// <summary>
        /// 读取基本配置 poke.config
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string GetXMLPokeValue(string target)
        {
            return GetXMLValue(target, "~/config/base/poke.config");
        }

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="Target">节点</param>
        /// <param name="Path">配置文件的路径</param>
        /// <returns></returns>
        public static string GetXMLValue(string Target,string Path)
        {
            try
            {
                string XmlPath = HttpContext.Current.Server.MapPath(Path);
                System.Xml.XmlDocument xdoc = new XmlDocument();
                xdoc.Load(XmlPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNode node = root.SelectSingleNode(Target);
                if (node != null)
                {
                    return node.InnerXml;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// 得到用户的配置信息
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <param name="Target">节点</param>
        /// <returns>返回获得的结果</returns>
        public static string GetXMLUserValue(object UserID, string Target)
        {
            string ResultSTR = string.Empty;
            string Path = "~/space/info/my/user0.config";
            if (File.Exists(HttpContext.Current.Server.MapPath("~/space/info/my/user" + UserID + ".config")))
            {
                Path = "~/space/info/my/user" + UserID + ".config";
            }
            ResultSTR = GetXMLValue(Target, Path);
            if (Target == "menulist")
            {
                if (string.IsNullOrEmpty(ResultSTR) && ResultSTR.Length < 3)
                {
                    ResultSTR = GetXMLUserValue(0, "menulist");
                }
            }
            return ResultSTR;
        }


        public static string Menulist(object userid,bool IsMenu)
        {
            string listSTR = string.Empty;
            try
            {
                string[] menuARR = GetXMLUserValue(userid, "menulist").Split(',');
                string URLSTR = string.Empty;
                for (int i = 0; i < menuARR.Length; i++)
                {
                    string appName = Public.GetXMLValue(menuARR[i], "~/config/base/menu.xml");
                    URLSTR = menuARR[i];
                    string CSS = "ico-" + URLSTR;
                    string nextButton = string.Empty;
                    switch (menuARR[i])
                    {
                        case "twitter":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/default"+ExName+"?r=add\">发表</a></em>";
                            break;
                        case "blog":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/new" + GetXMLValue("siteExName") + "\">发表</a></em>";
                            break;
                        case "album":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/new" + GetXMLValue("siteExName") + "\">上传</a></em>";
                            break;
                        case "group":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/new" + GetXMLValue("siteExName") + "\">创建</a></em>";
                            break;
                        case "news":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/new" + GetXMLValue("siteExName") + "\">发布</a></em>";
                            break;
                        case "buy":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/new" + GetXMLValue("siteExName") + "\">发起</a></em>";
                            break;
                        case "ative":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/new" + GetXMLValue("siteExName") + "\">发起</a></em>";
                            break;
                        case "vote":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/new" + GetXMLValue("siteExName") + "\">发起</a></em>";
                            break;
                        case "ask":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/new" + GetXMLValue("siteExName") + "\">提问</a></em>";
                            break;
                        case "bill":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/new" + GetXMLValue("siteExName") + "\">记账</a></em>";
                            break;
                        case "note":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/new" + GetXMLValue("siteExName") + "\">记录</a></em>";
                            break;
                        case "favorite":
                            nextButton = "<em class=\"r\"><a href=\"" + rootDir + "/app/" + menuARR[i] + "/new" + GetXMLValue("siteExName") + "\">增加</a></em>";
                            break;
                        default:
                            if (menuARR[i].IndexOf("|") > -1)
                            {
                                appName = menuARR[i].Split('|')[0];
                                URLSTR = menuARR[i].Split('|')[1];
                                CSS = "ico-app";
                            }
                            break;
                    }
                    if (IsMenu)
                    {
                        listSTR += "<li class=\"" + CSS + "\"><span class=\"l\"><a href=\"" + rootDir + "/app/" + URLSTR + "/default" + ExName + "\"><b></b>" + appName + "</a></span>" + nextButton + "</li>\r\n";
                    }
                    else
                    {
                        listSTR += "<li param=\"p_" + URLSTR + "\" class=\"" + CSS + "\" style=\"cursor:move\"><span class=\"l\"><b></b>" + appName + "<input type=\"hidden\" name=\"menulistNames\" value=\"" + menuARR[i] + "\"></span><span class=\"r\"><a href=\"javascript:;\" onclick=\"DeleteMenu('" + URLSTR + "')\" class=\"showok1\"></a></span></li>\r\n";
                    }
                }
            }
            catch
            {
                listSTR = Menulist(0, IsMenu);
            }
            return listSTR;
        }

        /// <summary>
        /// 取得用户ID,未验证是否合法
        /// </summary>
        /// <returns></returns>
        static public int GetUserID()
        {
            int UserID = 0;
            HttpCookie cookieToken = HttpContext.Current.Request.Cookies["SNSUserPassPort"];
            if (cookieToken == null || cookieToken["token"] == null || cookieToken["token"] == "")
                return UserID;
            string userCookie = cookieToken["token"];
            string desstr = DES.Decrypt(userCookie, SecretConfig.UserKey, SecretConfig.UserIV);
            string[] userInfo = desstr.Split(SecretConfig.UserSeparator);
            UserID = int.Parse(userInfo[0]);
            return UserID;
        }

        /// <summary>
        /// 设置节点值
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="xpath"></param>
        /// <param name="value"></param>
        static public void setXmlInnerText(string filepath, string xpath, string value)
        {
            XmlDocument xmldoc = new XmlDocument();
            string physicsPath = HttpContext.Current.Server.MapPath(filepath);
            xmldoc.Load(physicsPath);
            XmlNode node = xmldoc.SelectSingleNode(xpath);
            if (node != null)
            {
                node.InnerText = value;
                xmldoc.Save(physicsPath);
            }
        }

        /// <summary>
        /// 创建xml文件
        /// </summary>
        /// <param name="path"></param>
        static public void CreatUserConfig(string path)
        {
            string npath = HttpContext.Current.Server.MapPath(path);
            if (!File.Exists(npath))
            {
                using (StreamWriter sw = new StreamWriter(npath, true))
                {
                    sw.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    sw.WriteLine("<configuration>");
                    sw.WriteLine("<menulist>" + GetXMLUserValue(0, "menulist") + "</menulist>");
                    sw.WriteLine("<killuser>0</killuser>");
                    sw.WriteLine("<dynnumber>30</dynnumber>");
                    sw.WriteLine("<space>default</space>");
                    sw.WriteLine("</configuration>");
                }
            }
        }

        /// <summary>
        /// 新文件名
        /// </summary>
        /// <returns></returns>
        public static  string GetNewFileName()
        {
            return DateTime.Now.ToString("yyMMdd-hhmmss") + "-" + Common.Rand.Str(6);
        }
        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="pf">文件</param>
        /// <param name="avalExt">允许的扩展名</param>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        public static  void UpdateIcon(HttpPostedFile pf, string avalExt, out string filename)
        {
            filename = GetNewFileName();
            string orgFileName = pf.FileName;
            string ext = pf.FileName.Substring(pf.FileName.LastIndexOf('.') + 1);
            bool aval = false;
            foreach (string s in avalExt.Split(','))
            {
                if (ext.ToLower() == s.ToLower())
                {
                    aval = true;
                    break;
                }
            }
            if (aval == false)
            {
                filename = "err";
                return;
            }
            filename = filename + "." + ext ;
            string filepath = HttpContext.Current.Server.MapPath(rootDir + "/" + JuSNS.Config.PicConfig.AppDir + "/" + filename);

            pf.SaveAs(filepath);
        }

        /// <summary>
        /// 转换头像路径为缩略路径
        /// </summary>
        /// <param name="orgPicPath">原始路径</param>
        /// <param name="size">缩小级别</param>
        /// <returns></returns>
        public static string GetSmallHeadPic(object orgHeadPicPath, int size)
        {
            if (orgHeadPicPath == null || orgHeadPicPath == DBNull.Value)
                return GetSmallHeadPic((object)"default.jpg", size);
            if (orgHeadPicPath.ToString().Trim() == "")
                return GetSmallHeadPic((object)"default.jpg", size);
            string name = size.ToString();
            Dictionary<string, PicConfigInfo> _portrait = PicConfig.Portrait;
            string rvalue = string.Empty;
            try
            {
                rvalue = (string)orgHeadPicPath;
            }
            catch { }
            if (rvalue.Trim() == string.Empty)
                return string.Empty;
            if (_portrait[name].Directory.EndsWith("/"))
            {
                rvalue = _portrait[name].Directory + rvalue;
            }
            else
            {
                rvalue = _portrait[name].Directory + "/" + rvalue;
            }
            if (rvalue.StartsWith("/"))
            {
                return rootDir + rvalue;
            }
            if (rvalue.StartsWith("http"))
            {
                return rvalue;
            }
            return rootDir + "/" + rvalue;
        }
        /// <summary>
        /// 取得原始头像路径
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        public static string GetOrgHeadPic(string filename)
        {
            if (string.IsNullOrEmpty(filename) || filename.Trim() == "")
                return GetOrgHeadPic("default.jpg");
            string dir = "";
            dir += PicConfig.ProtraitServer + "/";
            dir += PicConfig.ProtraitRoot;
            if (dir.StartsWith("~/"))
            {
                dir = dir.Substring(2);
            }
            string rvalue = filename;
            rvalue = dir + "/" + filename;
            if (rvalue.StartsWith("http") || rvalue.StartsWith("/"))
            {
                return rvalue;
            }
            return rootDir + "/" + rvalue;
        }
        /// <summary>
        /// 取得原始照片路径
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        static public string GetOrgPic(string filename)
        {
            if (string.IsNullOrEmpty(filename))
            {
                return GetOrgPic("default.jpg");
            }
            string dir = string.Empty;
            dir += PicConfig.PhotoServer + "/";
            dir += PicConfig.PhotoRoot;
            string rvalue = filename;
            if (dir.StartsWith("~/"))
            {
                dir = dir.Substring(2);
            }
            rvalue = dir + "/" + filename;
            if (rvalue.StartsWith("http") || rvalue.StartsWith("/"))
            {
                return rvalue;
            }
            return rootDir + "/" + rvalue;
        }
        /// <summary>
        /// 转换照片路径为缩略图路径
        /// </summary>
        /// <param name="orgPicPath">原始路径</param>
        /// <param name="name">缩小级别</param>
        /// <returns></returns>
        static public string GetSmallPic(string orgPicPath, int size)
        {
            if (string.IsNullOrEmpty(orgPicPath) || orgPicPath.Trim() == "")
                return GetSmallPic("default.jpg", size);
            string name = size.ToString();
            Dictionary<string, PicConfigInfo> _photo = PicConfig.Photo;
            if (string.IsNullOrEmpty(orgPicPath))
            {
                return string.Empty;
            }
            if (orgPicPath.StartsWith(rootDir + "/template/" + JuSNS.Config.UiConfig.SkinStyle + "/images"))
            {
                return orgPicPath.Substring(0);
            }
            string rvalue = orgPicPath;
            if (_photo[name].Directory.EndsWith("/"))
            {
                rvalue = _photo[name].Directory + rvalue;
            }
            else
            {
                rvalue = _photo[name].Directory + "/" + rvalue;
            }
            if (rvalue.StartsWith("http") || rvalue.StartsWith("/"))
            {
                return rvalue;
            }
            return rootDir + "/" + rvalue;
        }

        /// <summary>
        /// 取得群组头像
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="n">大小级别</param>
        /// <returns></returns>
        static public string GetGroupHead(object filename, int n)
        {
            string defaultImg = "" + rootDir + "/template/" + JuSNS.Config.UiConfig.SkinStyle + "/images/000.jpg";
            string name = string.Empty;
            if (filename == DBNull.Value || filename == null)
            {
                return defaultImg;
            }
            name = filename.ToString();
            if (string.IsNullOrEmpty(name))
            {
                return defaultImg;
            }
            Dictionary<string, PicConfigInfo> config = PicConfig.GroupHead;
            string path = rootDir + "/" + config[n.ToString()].Directory + "/" + filename;
            return path;
        }

        /// <summary>
        /// 得到本地文件，并上传
        /// </summary>
        /// <param name="hpf"></param>
        /// <param name="allowExt"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        static public string GetFile(HttpPostedFile hpf, string allowExt, string path)
        {
            string filename = Common.Input.GetNewFileName();
            return GetFile(hpf, allowExt, path, filename);
        }

        /// <summary>
        /// 得到本地文件，并上传
        /// </summary>
        /// <param name="upload"></param>
        /// <param name="allowExt"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        static public string GetFile(HttpPostedFile hpf, string allowExt, string path, string filename)
        {
            try
            {
                int PicSize = hpf.ContentLength;
                if (PicSize > 0)
                {
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(path)))
                    {
                        System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                    }
                    int AllowSize = Convert.ToInt32(GetXMLValue("picsize")) * 1024 * 1024;
                    if (PicSize > AllowSize) return string.Empty;
                    string ext = Common.Input.GetFileExt(hpf.FileName);
                    if (allowExt.IndexOf(ext) > -1)
                    {
                        string newpath = path + "/" + filename + "." + ext;
                        newpath = HttpContext.Current.Server.MapPath(newpath);
                        hpf.SaveAs(newpath);
                        return filename + "." + ext;
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            catch { return string.Empty; }
        }

        /// <summary>
        /// 获取图片的高或宽
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="Flag">0宽，1高度</param>
        /// <returns>返回尺寸</returns>
        static public int GetSize(string path,int Flag)
        {
            int n = 0;
            try
            {
                System.Drawing.Image image = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(path));
                if (Flag == 0)
                {
                    n=image.Width;
                }
                else
                {
                    n = image.Height;
                }
                image.Dispose();
            }
            catch
            {
                return 0;
            }
            return n;
        }

        static public string ShowPlayer(string FlvSource)
        {
            if (FlvSource.ToLower().IndexOf(".swf") > -1 || FlvSource.ToLower().IndexOf(".flv") > -1)
            {
                return Player(FlvSource);
            }
            else
            {
                string flvid = string.Empty;
                if (FlvSource.IndexOf("v.youku.com") > -1)
                {
                    flvid = Regex.Match(FlvSource, @"\/id_(?<content>[^\/]*?)\.html$", RegexOptions.Compiled | RegexOptions.IgnoreCase).Groups["content"].Value;
                    return Player("http://player.youku.com/player.php/sid/"+flvid+"/v.swf");
                }
                else if (FlvSource.IndexOf("tudou.com")>-1)
                {
                    flvid = Regex.Match(FlvSource, @"\/(?<content>[^\/]*?)\/$", RegexOptions.Compiled | RegexOptions.IgnoreCase).Groups["content"].Value;
                    return Player("http://www.tudou.com/v/" + flvid + "");
                }
                else if (FlvSource.IndexOf("v.ku6.com") > -1)
                {
                    flvid = Regex.Match(FlvSource, @"\/(?<content>[^\/]*?)\.html$", RegexOptions.Compiled | RegexOptions.IgnoreCase).Groups["content"].Value;
                    return Player("http://player.ku6.com/refer/" + flvid + "/v.swf");
                }
            }
            return string.Empty;
        }

        static public string Player(string FlvSource)
        {
            string listSTR = "<object classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" codebase=\"http://download.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=7,0,19,0\" width=\"225\" height=\"200\">";
            listSTR += "<param name=\"movie\" value=\"" + FlvSource + "\" />";
            listSTR += "<param name=\"quality\" value=\"high\" />";
            listSTR += "<param name=\"allowFullScreen\" value=\"true\" />";
            listSTR += "<embed src=\"" + FlvSource + "\" allowfullscreen=\"true\" quality=\"high\" pluginspage=\"http://www.macromedia.com/go/getflashplayer\" type=\"application/x-shockwave-flash\" width=\"225\" height=\"200\"></embed>";
            listSTR += "</object>";
            return listSTR;
        }

        /// <summary>
        /// 打招呼表情
        /// </summary>
        public static string GetAction(int pokekey, string from, string to, string isPub,string username)
        {
            string astr = string.Empty;
            string isPubs = string.Empty;
            if (isPub == "0")
            {
                isPubs = "<span class=\"reshow\">偷偷的</span>";
            }
            if (pokekey == -1)
            {
                astr = from + "你" + to;
            }
            else
            {
                astr = JuSNS.Config.PokeConfig.Config[pokekey].More;
            }
            return isPubs + astr.Replace("xx", username);
        }

        static public string URLWrite(object q, string Type)
        {
            if (q == null) return string.Empty;
            string listSTR = string.Empty;
            if (string.IsNullOrEmpty(q.ToString()))
            {
                return string.Empty;
            }
            else
            {
                switch (Type)
                {
                    case "blog":
                        if (GetXMLBaseValue("BlogURLWrite") == "1")
                        {
                            listSTR = rootDir + "/info/blog-" + q + ".html";
                        }
                        else
                        {
                            listSTR = rootDir + "/app/blog/info" + ExName + "?bid=" + q;
                        }
                        break;
                    case "news":
                        if (GetXMLBaseValue("ContentURLWrite") == "1")
                        {
                            listSTR = rootDir + "/info/news-" + q + ".html";
                        }
                        else
                        {
                            listSTR = rootDir + "/app/news/info" + ExName + "?nid=" + q;
                        }
                        break;
                    case "user":
                        if (UiConfig.SpaceUrl == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/user/" + q;
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/user/default" + ExName + "?uid=" + q;
                        }
                        break;
                    case "ask":
                        if (GetXMLAskValue("URL") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/info/ask-" + q + ".html";
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/app/ask/info" + ExName + "?aid=" + q;
                        }
                        break;
                    case "ative":
                        if (GetXMLAtiveValue("URL") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/info/ative-" + q + ".html";
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/app/ative/info" + ExName + "?aid=" + q;
                        }
                        break;
                    case "active":
                        if (GetXMLAtiveValue("URL") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/info/ative-" + q + ".html";
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/app/ative/info" + ExName + "?aid=" + q;
                        }
                        break;
                    case "goods":
                        if (GetXMLShopValue("URL") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/info/goods-" + q + ".html";
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/app/shop/goods" + ExName + "?gid=" + q;
                        }
                        break;
                    case "shop":
                        if (GetXMLShopValue("URL") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/shop/" + q;
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/user/shop/default"+ExName+"?sid=" + q;
                        }
                        break;
                    case "multe":
                        if (GetXMLShopValue("URL") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/info/multe-" + q + ".html";
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/app/shop/multe" + ExName + "?mid=" + q;
                        }
                        break;
                    case "twitter":
                        if (GetXMLBaseValue("TwitterURLWriter") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/info/twitter-" + q + ".html";
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/app/twitter/info" + ExName + "?tid=" + q;
                        }
                        break;
                    case "vote":
                        if (GetXMLVoteValue("URLWrite") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/info/vote-" + q + ".html";
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/app/vote/view"+ExName+"?vid=" + q;
                        }
                        break;
                    case "photo":
                        if (GetXMLAlbumValue("PURLWrite") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/info/photo-" + q + ".html";
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/app/album/photoview" + ExName + "?pid=" + q;
                        }
                        break;
                    case "album":
                        if (GetXMLAlbumValue("AURLWrite") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/info/album-" + q + ".html";
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/app/album/albumview"+ExName+"?aid=" + q;
                        }
                        break;
                    case "group":
                        if (GetXMLGroupValue("QURLWrite") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/info/group-" + q + ".html";
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/app/group/group" + ExName + "?gid=" + q;
                        }
                        break;
                    case "topic":
                        if (GetXMLGroupValue("TURLWrite") == "1")
                        {
                            listSTR = UiConfig.RootUrl + "/info/topic-" + q + ".html";
                        }
                        else
                        {
                            listSTR = UiConfig.RootUrl + "/app/group/topic" + ExName + "?tid=" + q;
                        }
                        break;
                    case "app":
                        listSTR = UiConfig.RootUrl + "/app/center/info" + ExName + "?appid=" + q;
                        break;
                }
            }
            return listSTR;
        }

        /// <summary>
        /// 得到转化后的社群标志图片
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public string GetGroupPortrait(object filename)
        {
            if (filename == null) return string.Empty;
            return (rootDir + GetXMLGroupValue("GroupPicPath") + "/" + filename.ToString()).Replace("//", "/");
        }

        static public string GetBirthday(DateTime birthday, int flag)
        {
            switch (flag)
            {
                case 0:
                    return birthday.ToString("yyyy年MM月dd日");
                    break;
                case 1:
                    return birthday.ToString("MM月dd日");
                    break;
                case 2:
                    return birthday.ToString("yyyy年");
                    break;
                default:
                    return string.Empty;
                    break;
            }
        }

        /// <summary>
        /// 得到会员等级
        /// </summary>
        /// <param name="number">分数</param>
        /// <returns></returns>
        static public int GetMemberlevels(int number)
        {
            try
            {
                string memberleves = Config.UiConfig.memberleves;
                string[] memberARR = memberleves.Split(',');
                int levels1 = Convert.ToInt32(memberARR[0]);
                int levels2 = Convert.ToInt32(memberARR[1]);
                int levels3 = Convert.ToInt32(memberARR[2]);
                int levels4 = Convert.ToInt32(memberARR[3]);
                int levels5 = Convert.ToInt32(memberARR[4]);
                int levels6 = Convert.ToInt32(memberARR[5]);
                int levels7 = Convert.ToInt32(memberARR[6]);
                int levels8 = Convert.ToInt32(memberARR[7]);
                if (number <= levels2) return 1;
                if (number <= levels3 && number > levels2) return 2;
                if (number <= levels4 && number > levels3) return 3;
                if (number <= levels5 && number > levels4) return 4;
                if (number <= levels6 && number > levels5) return 5;
                if (number <= levels7 && number > levels6) return 6;
                if (number <= levels8 && number > levels7) return 7;
                if (number > levels8) return 8;
                return 1;
            }
            catch
            {
                return 1;
            }
        }

        /// <summary>
        /// 得到教育信息
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
       static public string GetLevel(object n)
        {
            string[] estr = { "小学", "初中", "高中", "大学", "硕士", "博士" };
            string eduSTR = string.Empty;
            for (int j = 0; j < estr.Length; j++)
            {
                if (Convert.ToInt32(n) == j)
                    eduSTR = estr[j];
            }
            return eduSTR;
        }



       static public string GetEnumStateOp(object State, int infoid, int uid, string type)
       {
           string resultSTR = string.Empty;
           if (type == "group" 
               || type == "blog"
               || type == "twitter" 
               || type == "photo" 
               || type == "blogcomment"
               || type == "twittercomment" 
               || type == "photocomment"
               || type == "goodscomment"
               || type == "activecomment"
               || type == "ads"
               )
           {
               switch (Convert.ToByte(State))
               {
                   case (byte)EnumCusState.ForLock:
                       resultSTR += "<input type=\"button\" class=\"btn_blue2\" title=\"点击通过审核\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",0,'" + type + "')\" value=\"通过\" /> ";
                       break;
                   case (byte)EnumCusState.ForNormal:
                       resultSTR += "<input type=\"button\" class=\"btn_blue2\" title=\"停止\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",1,'" + type + "')\" value=\"停止\" />";
                       break;
               }
           }
           else if (type == "app")
           {
               switch (Convert.ToByte(State))
               {
                   case 0:
                       resultSTR += "<input type=\"button\" class=\"btn_blue2\" title=\"停止\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",4,'" + type + "')\" value=\"锁定\" /> ";
                       break;
                   case 1:
                       resultSTR += "<input type=\"button\" class=\"btn_blue2\" title=\"通过\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",0,'" + type + "')\" value=\"通过\" />";
                       break;
                   case 2:
                       resultSTR += "<input type=\"button\" class=\"btn_blue2\" title=\"锁定\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",4,'" + type + "')\" value=\"锁定\" /> ";
                       break;
                   case 3:
                       resultSTR += "<input type=\"button\" class=\"btn_blue2\" title=\"锁定\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",4,'" + type + "')\" value=\"锁定\" /> ";
                       break;
                   case 4:
                       resultSTR += "<input type=\"button\" class=\"btn_blue2\" title=\"启用\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",0,'" + type + "')\" value=\"启用\" />";
                       break;
               }
           }
           else
           {
               switch (Convert.ToByte(State))
               {
                   case (byte)EnumCusState.ForLock:
                       resultSTR += "<input type=\"button\" class=\"btn_blue2\" title=\"点击通过审核\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",0,'" + type + "')\" value=\"通过\" /> ";
                       resultSTR += "<input type=\"button\" class=\"btn_blue2\" title=\"不通过审核\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",2,'" + type + "')\" value=\"不通过\" />";
                       break;
                   case (byte)EnumCusState.ForNormal:
                       resultSTR += "<input type=\"button\" class=\"btn_blue2\" title=\"停止\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",3,'" + type + "')\" value=\"停止\" />";
                       break;
                   case (byte)EnumCusState.ForStop:
                       resultSTR += "<input type=\"button\" class=\"btn_blue4\" title=\"设为正常\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",0,'" + type + "')\" value=\"设为正常\" />";
                       break;
                   case (byte)EnumCusState.ForUnPass:
                       resultSTR += "<input type=\"button\" class=\"btn_blue2\" title=\"通过\" onclick=\"CheckInfoState(" + infoid + "," + uid + ",0,'" + type + "')\" value=\"通过\" />";
                       break;
               }
           }
           return resultSTR;
       }
        
        
        static public string GetEnumState(object State)
       {
           string resultSTR = string.Empty;
           switch (Convert.ToByte(State))
           {
               case (byte)EnumCusState.ForLock:
                   resultSTR = "审核中";
                   break;
               case (byte)EnumCusState.ForNormal:
                   resultSTR = "正常";
                   break;
               case (byte)EnumCusState.ForStop:
                   resultSTR = "已停止";
                   break;
               case (byte)EnumCusState.ForUnPass:
                   resultSTR = "未通过";
                   break;
           }
           return resultSTR;
       }
    }
}
