using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;


namespace JuSNS.Common
{
    public class Input
    {
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
        /// 将字符转化为HTML编码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返回编译后的字符串</returns>
        public static string HtmlEncode(string Input)
        {
            return HttpContext.Current.Server.HtmlEncode(Input);
        }

        /// <summary>
        /// 将字符HTML解码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string HtmlDecode(string Input)
        {
            if (string.IsNullOrEmpty(Input)) return string.Empty;
            else
                return HttpContext.Current.Server.HtmlDecode(Input);
        }

        /// <summary>
        /// URL地址编码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返编码后的字符串</returns>
        public static string URLEncode(string Input)
        {
            if (string.IsNullOrEmpty(Input)) return string.Empty;
            else
                return HttpContext.Current.Server.UrlEncode(Input);
        }

        /// <summary>
        /// URL地址解码
        /// </summary>
        /// <param name="Input">输入字符串</param>
        /// <returns>返回解码后的字符串</returns>
        public static string URLDecode(string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                return HttpContext.Current.Server.UrlDecode(Input);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 检测是否整数型数据
        /// </summary>
        /// <param name="Input">输入字串符（object）</param>
        /// <returns>返回true或false</returns>
        public static bool IsInteger(object Input)
        {
            if (Input == null) { return false; } else { return IsInteger(Input, true); }
        }

        /// <summary>
        /// 是否全是正整数
        /// </summary>
        /// <param name="Input">输入字符串（object类）</param>
        /// <param name="Plus">true表示是否正整数</param>
        /// <returns>返回true或false</returns>
        public static bool IsInteger(object Input, bool Plus)
        {
            if (Input == null) return false;
            if (string.IsNullOrEmpty(Input.ToString())) { return false;}
            else
            {
                string pattern = "^-?[0-9]+$";
                if (Plus) pattern = "^[0-9]+$";
                if (Regex.Match(Input.ToString(), pattern, RegexOptions.Compiled).Success) { return true; }
                else {  return false; }
            }
        }

        /// <summary>
        /// 判断输入是否为日期类型
        /// </summary>
        /// <param name="s">待检查数据</param>
        /// <returns>返回true或false</returns>
        public static bool IsDate(string s)
        {
            if (s == null)
            {
                return false;
            }
            else
            {
                try
                {
                    DateTime d = DateTime.Parse(s);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// 判断是否是电子邮件
        /// </summary>
        /// <param name="strIn">输入字符串</param>
        /// <returns>返回true或false</returns>
        public static bool isEmail(string strIn)
        {
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }
            return Regex.IsMatch(strIn, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");     
        }

        /// <summary>
        /// 判断是否是国内手机号码,前面不加0
        /// </summary>
        /// <param name="strIn">输入字符串</param>
        /// <returns>返回true或false</returns>
        public static bool isMobile(string strIn)
        {
            if (string.IsNullOrEmpty(strIn))
            {
                return false;
            }
            string regu = "^[1][3,5,8][0-9]{9}$";
            if (Regex.Match(strIn, regu, RegexOptions.Compiled).Success) 
            { 
                return true; 
            }
            else 
            { 
                return false;
            }

        }

        /// <summary>
        /// 过滤字符串中的html代码
        /// </summary>
        /// <param name="Str">传入字符串</param>
        /// <returns>过滤后的字符串</returns>
        public static string LostHTML(string Str)
        {
            string Re_Str = "";
            if (Str != null)
            {
                if (Str != string.Empty)
                {
                    string Pattern = "<\\/*[^<>]*>";
                    Re_Str = Regex.Replace(Str, Pattern, "");
                }
            }
            return (Re_Str.Replace("\\r\\n", "")).Replace("\\r", "");
        }


        /// <summary>
        /// 截取字符串函数
        /// </summary>
        public static string GetSubString(string Str, int Num)
        {
            string sdot = string.Empty;
            if (Str == null || Str == "")
                return string.Empty;
            string outstr = string.Empty;
            int n = 0;
            foreach (char ch in Str)
            {
                n += System.Text.Encoding.Default.GetByteCount(ch.ToString());
                if (n > Num)
                {
                    sdot = "..";
                    break;
                }
                else
                {
                    outstr += ch;
                }
            }
            if (Str.Length > 2)
            {
                return outstr + sdot;
            }
            else
            {
                return outstr;
            }
        }
        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="Str">所要截取的字符串</param>
        /// <param name="Num">截取字符串的长度</param>
        /// <param name="Len">返回实际长度</param>
        /// <returns></returns>
        public static string GetSubString(string Str, int Num, out int Len)
        {
            Len = 0;
            if (string.IsNullOrEmpty(Str))
                return "";
            string outstr = string.Empty;

            foreach (char ch in Str)
            {
                Len += System.Text.Encoding.Default.GetByteCount(ch.ToString());
                if (Len > Num)
                    break;
                else
                    outstr += ch;
            }
            return outstr;
        }

        /// <summary>
        /// 截取字符串函数
        /// </summary>
        /// <param name="Str">所要截取的字符串</param>
        /// <param name="Num">截取字符串的长度</param>
        /// <param name="Num">截取字符串后省略部分的字符串</param>
        /// <returns></returns>
        public static string GetSubString(string Str, int Num, string LastStr)
        {
            return (Str.Length > Num) ? Str.Substring(0, Num) + LastStr : Str;
        }

        /// <summary>
        /// 过滤特殊字符
        /// </summary>
        public static string Htmls(string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                string ihtml = Input.ToLower();
                ihtml = ihtml.Replace("<script", "&lt;script");
                ihtml = ihtml.Replace("script>", "script&gt;");
                ihtml = ihtml.Replace("</script", "&lt;/script");
                ihtml = ihtml.Replace("<%", "&lt;%");
                ihtml = ihtml.Replace("%>", "%&gt;");
                ihtml = ihtml.Replace("<$", "&lt;$");
                ihtml = ihtml.Replace("$>", "$&gt;");
                return ihtml;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        public static String ToTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "\r\n");
            sb.Replace("<br>", "\n");
            sb.Replace("<br />", "\n");
            sb.Replace("<br />", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        public static String ToshowTxt(String Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&quot;", "\"");
            return sb.ToString();
        }

        /// <summary>
        /// 把字符转化为文本格式
        /// </summary>
        /// <param name="Input"></param>
        /// <returns></returns>
        public static string ForTXT(string Input)
        {
            StringBuilder sb = new StringBuilder(Input);
            sb.Replace("<font", " ");
            sb.Replace("<span", " ");
            sb.Replace("<style", " ");
            sb.Replace("<div", " ");
            sb.Replace("<p", "");
            sb.Replace("</p>", "");
            sb.Replace("<label", " ");
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "");
            sb.Replace("<br />", "");
            sb.Replace("<br />", "");
            sb.Replace("&lt;", "");
            sb.Replace("&gt;", "");
            sb.Replace("&amp;", "");
            sb.Replace("<", "");
            sb.Replace(">", "");
            return sb.ToString();
        }

        /// <summary>
        /// 字符串字符处理
        /// </summary>
        public static String ToHtml(string Input)
        {
            if (!string.IsNullOrEmpty(Input))
            {
                StringBuilder sb = new StringBuilder(Input);
                sb.Replace("&", "&amp;");
                sb.Replace("<", "&lt;");
                sb.Replace(">", "&gt;");
                sb.Replace("\r\n", "<br />");
                sb.Replace("\n", "<br />");
                sb.Replace("\t", " ");
                return sb.ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// MD5加密字符串处理
        /// </summary>
        /// <param name="Half">加密是16位还是32位；如果为true为16位</param>
        public static string MD5(string Input, bool Half)
        {
            string output = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(Input, "MD5").ToLower();
            if (Half) output = output.Substring(8, 16);
            return output;
        }

        public static string MD5(string Input)
        {
            return MD5(Input, true);
        }

        /// <summary>
        /// 过滤HTML语法
        /// </summary>
       public static string FilterHTML(object input)
        {
            if (input == null)
            {
                return string.Empty;
            }
            else
            {
                string html = input.ToString();
                if (string.IsNullOrEmpty(html)) return string.Empty;
                System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                html = regex1.Replace(html, string.Empty);
                return html.Replace("\"", "“");
            }
        }

        public static string FilterLogHTML(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            if (html.ToLower().IndexOf("<img") > -1 && html.ToLower().IndexOf(">") > -1)
            {
                html = "<span class=\"sp_reshow\">【图文日志】</span>" + html;
            }
           System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<[^>]*>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
           html = regex1.Replace(html, string.Empty);
           return html;
        }

        /// <summary>
        /// 去除script
        /// </summary>
        public static string loseScript(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<script[\s\S]+</script *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            return html;
        }

        /// <summary>
        /// 去除iframe
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string loseIframe(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            System.Text.RegularExpressions.Regex regex1 = new System.Text.RegularExpressions.Regex(@"<iframe[\s\S]+</iframe *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(@"<frameset[\s\S]+</frameset *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex1.Replace(html, ""); //过滤<script></script>标记
            html = regex2.Replace(html, ""); //过滤href=javascript: (<A>) 属性
            return html;
        }

        /// <summary>
        /// 去除除IMG以外的HTML
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string loseExceptImg(string html)
        {
            if (string.IsNullOrEmpty(html)) return string.Empty;
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"<(?!/?img)[\s\S]*?>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            html = regex.Replace(html, "");
            return html;
        }

        /// <summary>
        /// 把图文分解成图，文
        /// </summary>
        /// <param name="html">输入HTML</param>
        /// <param name="img">图</param>
        /// <param name="text">文</param>
        public static void splitImgAndText(string html,out string img,out string text)
        {
            if (html == null)
            {
                img = "";
                text = "";
                return;
            }
            Regex regex = new System.Text.RegularExpressions.Regex(@"<img[\s\S]+</img *>|<img[^>]+/? *>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            Match m=regex.Match(html);
            string imgstr = string.Empty;
            while (m.Success)
            {
                imgstr += m.Value;
                m = m.NextMatch();
            }
            string textStr = FilterHTML(html);
            img=imgstr;
            text=textStr;
        }

        /// <summary>
        /// 去除多于的逗号
        /// </summary>
        public static string FixCommaStr(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string[] arr = str.Split(',');
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < arr.Length; i++)
                {
                    if (arr[i].Trim() != "")
                    {
                        sb.Append(arr[i]);
                        sb.Append(",");
                    }
                }
                string sbstr = sb.ToString();
                if (!string.IsNullOrEmpty(sbstr))
                {
                    sbstr = sbstr.Substring(0, sbstr.Length - 1);
                }
                return sbstr;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 过滤字符
        /// </summary>
        public static string Filter(string sInput)
        {
            if (sInput == null || sInput.Trim() == string.Empty)
                return null;
            string sInput1 = sInput.ToLower();
            string output = sInput;
            string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
            if (Regex.Match(sInput1, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
            {
                throw new Exception("字符串中含有非法字符!");
            }
            else
            {
                output = output.Replace("'", "''");
            }
            return output;
        }

        /// <summary>
        /// 替换表情
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ReplaceSmaile(object content)
        {
            if (content == null) return string.Empty;
            string Input = content.ToString();
            if (string.IsNullOrEmpty(Input)) return string.Empty;
            string s = string.Empty;
            Match match = Regex.Match(Input, "\\[em\\:[^\\:\\]\\[em\\:><,/]*\\:\\]", RegexOptions.IgnoreCase | RegexOptions.RightToLeft);
            while (match.Success)
            {
                s = match.Value;
                Input = Input.Replace(s, "<img src=\"" + rootDir + "/template/images/face/" + s + ".gif\"   align=\"middle\" style=\"border:0\" />");
                match = match.NextMatch();
            }
            string rList =  Input.Replace("[em:",string.Empty);
            rList = rList.Replace(":]", string.Empty);
            return rList;
        }

        /// <summary>
        /// 上传文件(小图片，可以出入路径和生成缩略图)
        /// </summary>
        /// <param name="pf">HttpPostedFile</param>
        /// <param name="makeSmall">是否生成缩略图</param>
        /// <param name="path">路径</param>
        /// <param name="filename">输出文件名</param>
        /// <returns>错误信息</returns>
        public static string UploadFile(HttpPostedFile pf, bool makeSmall,string savepath, out string filename)
        {
            string fileExt = GetFileExt(pf.FileName);
            if (!AllowFileExt(fileExt))
            {
                filename = string.Empty;
                return "扩展名不被允许";
            }
            filename = GetNewFileName() + "." + fileExt;
            string path = HttpContext.Current.Server.MapPath("~" + "/" + savepath + "/" + filename);
            pf.SaveAs(path);
            if (makeSmall)
            {
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(path);
                if (bitmap.Width <= 200)
                {
                    bitmap.Save(HttpContext.Current.Server.MapPath("~" + "/" + savepath + "/s_" + filename));
                    pf.InputStream.Dispose();
                    bitmap.Dispose();
                    return string.Empty;
                }
                byte[] data = new byte[pf.ContentLength];
                pf.InputStream.Read(data, 0, pf.ContentLength);
                byte[] data1;
                int w = 200;
                int h = bitmap.Height * (w / bitmap.Width);
                CreateThumbnail(data, out data1, w, h);
                System.Drawing.Image img = ByteToImage(data1);
                img.Save(HttpContext.Current.Server.MapPath("~" + "/" + savepath + "/s_" + filename));
                img.Dispose();
                bitmap.Dispose();
                pf.InputStream.Dispose();
            }
            return string.Empty;
        }
        /// <summary>
        /// 上传文件 
        /// </summary>
        /// <param name="pf">HttpPostedFile</param>
        /// <param name="filename">输出文件名</param>
        /// <returns>错误信息</returns>
        public static string UploadFile(HttpPostedFile pf,string path, out string filename)
        {
            return UploadFile(pf, false, path, out filename);
        }

        /// <summary>
        /// 新文件名
        /// </summary>
        /// <returns></returns>
        public static string GetNewFileName()
        {
            return DateTime.Now.ToString("MMddhhmmss") + Common.Rand.Number(6);
        }
        /// <summary>
        /// 取得文件扩展名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns></returns>
        static public string GetFileExt(string filename)
        {
            int pos = filename.LastIndexOf(".");
            return filename.Substring(pos + 1);
        }
        /// <summary>
        /// 取得主文件名
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        static public string GetFileMainName(string filename)
        {
            int pos = filename.LastIndexOfAny(new char[] { '\\', '/' });
            int pos1 = 0;
            int len = pos - pos1;
            return filename.Substring(pos1 + 1, len);
        }
        /// <summary>
        /// 允许的文件类型
        /// </summary>
        /// <param name="ext">文件类型</param>
        /// <returns></returns>
        static public bool AllowFileExt(string ext)
        {
            try
            {
                string ex = "jpg,gif,png,bmp,jpeg";
                foreach (string s in ex.Split(','))
                {
                    if (ext.ToLower() == s.ToLower())
                    {
                        return true;
                    }
                }
            }
            catch { return true; }
            return false;
        }
        /// <summary>
        /// 生成缩略图纯数据
        /// </summary>
        /// <param name="data1">数据1</param>
        /// <param name="data2">数据2</param>
        /// <param name="LimitW">限宽</param>
        /// <param name="LimitH">限高</param>
        static public void CreateThumbnail(byte[] data1, out byte[] data2, double LimitW, double LimitH)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(data1)) as System.Drawing.Bitmap;
            System.Drawing.SizeF size = new System.Drawing.SizeF(image.Width, image.Height);
            size.Width = (float)LimitW;
            size.Height = (float)LimitH;
            if (LimitH == 0) LimitH = LimitW;
            if (LimitW == 0) LimitW = LimitH;
            if (size.Width == 0)
            {
                size.Width = size.Height;
            }
            if (size.Height == 0)
            {
                size.Height = size.Width;
            }
            if (size.Height > image.Height && size.Width > image.Width)
            {
                size.Width = image.Width;
                size.Height = image.Height;
            }
            else
            {
                if (size.Width < size.Height)
                {
                    if (image.Width > size.Width)
                    {
                        size.Height = image.Height * size.Width / image.Width;
                    }
                    else
                    {
                        size.Width = image.Width * size.Height / image.Height;
                    }
                }
                else
                {
                    if (image.Height > size.Height)
                    {
                        size.Width = image.Width * size.Height / image.Height;
                    }
                    else
                    {

                        size.Height = image.Height * size.Width / image.Width;
                    }
                }
                if (size.Width > LimitW)
                {
                    size.Width = (float)LimitW;
                    size.Height = image.Height * size.Width / image.Width;
                }
                if (size.Height > LimitH)
                {
                    size.Height = (float)LimitH;
                    size.Width = image.Width * size.Height / image.Height;
                }
            }
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(Convert.ToInt16(size.Width), Convert.ToInt16(size.Height));
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            g.DrawImage(image, rect, new System.Drawing.Rectangle(0, 0, image.Width, image.Height), System.Drawing.GraphicsUnit.Pixel);
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = ImageCodecInfo.GetImageEncoders()[0];
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, myImageCodecInfo, myEncoderParameters);
            data2 = ms.ToArray();
            myEncoderParameter.Dispose();
            myEncoderParameters.Dispose();
            image.Dispose();
            bitmap.Dispose();
            g.Dispose();
            ms.Dispose();
        }
        /// <summary>
        /// 可拉伸的图片
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <param name="LimitW"></param>
        /// <param name="LimitH"></param>
        static public void CreateThumbnailByPulling(byte[] data1, out byte[] data2, double LimitW, double LimitH)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(data1)) as System.Drawing.Bitmap;
            System.Drawing.SizeF size = new System.Drawing.SizeF(image.Width, image.Height);
            size.Width = (float)LimitW;
            size.Height = (float)LimitH;
            System.Drawing.Image bitmap = new System.Drawing.Bitmap(Convert.ToInt16(size.Width), Convert.ToInt16(size.Height));
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(Color.Transparent);
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            g.DrawImage(image, rect, new System.Drawing.Rectangle(0, 0, image.Width, image.Height), System.Drawing.GraphicsUnit.Pixel);
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = ImageCodecInfo.GetImageEncoders()[0];
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, myImageCodecInfo, myEncoderParameters);
            data2 = ms.ToArray();
            myEncoderParameter.Dispose();
            myEncoderParameters.Dispose();
            image.Dispose();
            bitmap.Dispose();
            g.Dispose();
            ms.Dispose();
        }
        /// <summary>
        /// byte数组转换为image
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static public System.Drawing.Image ByteToImage(byte[] data)
        {
            System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(data)) as System.Drawing.Bitmap;
            return image;
        }
        /// <summary>
        /// 将 image 转成 byte 数组
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        static public byte[] ImageToByte(Image img)
        {
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
            ImageCodecInfo myImageCodecInfo;
            System.Drawing.Imaging.Encoder myEncoder;
            EncoderParameter myEncoderParameter;
            EncoderParameters myEncoderParameters;
            myImageCodecInfo = ImageCodecInfo.GetImageEncoders()[0];
            myEncoder = System.Drawing.Imaging.Encoder.Quality;
            myEncoderParameters = new EncoderParameters(1);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            MemoryStream ms = new MemoryStream();
            img.Save(ms, myImageCodecInfo, myEncoderParameters);
            byte[] data = ms.ToArray();
            myEncoderParameter.Dispose();
            myEncoderParameters.Dispose();
            g.Dispose();
            ms.Dispose();
            return data;
        }
    }
}
