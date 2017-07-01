using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Browser;
using System.Windows.Controls;


namespace czy.Silverlight.Library.Data
{
    /// <summary>
    /// 类据验证类
    /// </summary>
    public class Validate
    {

        #region 验证是否为空
        /// <summary>
        /// 验证文本框是否为空
        /// </summary>
        /// <param name="txt">要验证的文本框</param>
        /// <returns>true则为空,false则不为空</returns>
        public static bool ChackTxtIsEmpty(string txt)
        {
            if (txt == string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 验证两个文本框内容是否相同
        /// <summary>
        /// 验证两个文本框内容是否相同
        /// </summary>
        /// <param name="txt1">第一个文本框</param>
        /// <param name="txt2">第二个文本框</param>
        /// <returns>true为相同,false为不同</returns>
        public static bool ChackTxtCompare(string txt1, string txt2)
        {
            if (txt1 == txt2)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 验证输入的是否是数字
        /// <summary>
        /// 验证输入的是否是数字
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(string str)
        {
            if (str == null || str.Length == 0)
            { return false; }
            foreach (char c in str)
            {
                if (!Char.IsNumber(c))
                { return false; }
            }
            return true;
        }
        #endregion 
        
        #region 验证输入的是否是数字(正则表达式)
        /// <summary>
        /// 验证输入的是否是数字(正则表达式)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric1(string str)
        {
            System.Text.RegularExpressions.Regex reg1 = new System.Text.RegularExpressions.Regex(@"^[-]?\d+[.]?\d*$");
            return reg1.IsMatch(str);
        }
        #endregion

        #region 整数或者小数
        /// <summary>
        /// 整数或者小数
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <returns></returns>
        public static bool IsIntOrFolat(string str) 
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[0-9]+\.{0,1}[0-9]{0,2}$");
            return reg.IsMatch(str);
        }
        #endregion

        #region 只能输入n位的数字
        /// <summary>
        /// 只能输入n位的数字
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static bool LimitTextLength(string str,int length)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^\d{" + length + "}$");
            return reg.IsMatch(str);
        }
        #endregion

        #region  只能输入至少n位的数字
        /// <summary>
        /// 只能输入至少n位的数字
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public static bool OnlyLength(string str,int length)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^\d{" + length + ",}$");
            return reg.IsMatch(str);
        }
        #endregion

        #region 只能输入m~n位的数字
        /// <summary>
        /// 只能输入m~n位的数字
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <param name="maxLength">最大长度</param>
        /// <param name="minLength">最小长度</param>
        /// <returns></returns>
        public static bool BetweenMaxAndMinLength(string str, int maxLength, int minLength)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^\d{" + minLength + "," + maxLength + "}$");
            return reg.IsMatch(str);
        }
        #endregion

        #region 只能输入由数字、26个英文字母或者下划线组成的字符串
        /// <summary>
        /// 只能输入由数字、26个英文字母或者下划线组成的字符串
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <returns></returns>
        public static bool _OnlyStrAndNumber(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^\w+$");
            return reg.IsMatch(str);
        }
        #endregion

        #region 只能输入由数字和26个英文字母组成的字符串
        /// <summary>
        /// 只能输入由数字和26个英文字母组成的字符串
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <returns></returns>
        public static bool OnlyStrAndNumber(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[A-Za-z0-9]+$");
            return reg.IsMatch(str);
        }
        #endregion

        #region 用户验证 .以字母开头，长度在6~18之间，只能包含字符、数字和下划线。
        /// <summary>
        /// 用户验证 .以字母开头，长度在6~18之间，只能包含字符、数字和下划线。
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <returns></returns>
        public static bool CheckUser(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]\w{5,17}$");
            return reg.IsMatch(str);
        }
        #endregion

        #region 验证是否含有^%&',;=?$\"等字符
        /// <summary>
        /// 验证是否含有^%&',;=?$\"等字符
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <returns></returns>
        public static bool IsContentNotNeedChar(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[a-zA-Z]\w{5,17}$");
            return reg.IsMatch(str);
        }
        #endregion

        #region 只能输入汉字
        /// <summary>
        /// 只能输入汉字
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <returns></returns>
        public static bool IsChinese(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^[\u4e00-\u9fa5]{0,}$");
            return reg.IsMatch(str);
        }
        #endregion

        #region 验证Email地址
        /// <summary>
        /// 验证Email地址
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <returns></returns>
        public static bool IsEmail(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
            return reg.IsMatch(str);
        }
        #endregion

        #region 验证InternetURL
        /// <summary>
        /// 验证InternetURL
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <returns></returns>
        public static bool IsInternetUrl(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$");
            return reg.IsMatch(str);
        }
        #endregion

        #region  验证电话号码.正确格式为："XXX-XXXXXXX"、"XXXX-XXXXXXXX"、"XXX-XXXXXXX"、"XXX-XXXXXXXX"、"XXXXXXX"和"XXXXXXXX"
        /// <summary>
        /// 验证电话号码.正确格式为："XXX-XXXXXXX"、"XXXX-XXXXXXXX"、"XXX-XXXXXXX"、"XXX-XXXXXXXX"、"XXXXXXX"和"XXXXXXXX"
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <returns></returns>
        public static bool IsPhone(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^(\(\d{3,4}-)|\d{3.4}-)?\d{7,8}$");
            return reg.IsMatch(str);
        }
        #endregion

        #region 验证身份证号（15位或18位数字）
        /// <summary>
        /// 验证身份证号（15位或18位数字）
        /// </summary>
        /// <param name="str">输入字符</param>
        /// <returns></returns>
        public static bool IsID(string str)
        {
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^\d{15}|\d{18}$");
            return reg.IsMatch(str);
        }

        #endregion

        #region 图片类型验证
        /// <summary>
        /// 判断是否是图片
        /// </summary>
        /// <param name="file">文件名</param>
        /// <returns>true为图片,false则不为图片</returns>
        public static bool IsImage(string file)
        {
            bool is_Image = false;
            const string ImageFormat = "jpg|jpeg|gif|bmp|png";
            string[] array = ImageFormat.Split('|');
            foreach (string a in array)
            {
                file=file.Substring (file.LastIndexOf('.')+1);
                if (file.ToLower() == a)
                {
                    return is_Image = true;
                }
             
            }
            return is_Image;
        }

        #endregion

     
     
     
        #region 特殊字符验证
        /// <summary>
        /// 特殊字符验证
        /// </summary>
        /// <param name="str">字符窜</param>
        /// <returns>返回true说明是特殊字符，false则说明不是</returns>
        public static bool HasIllegalStr(string str)
        {
            string[] specialStrEN=new string []{"~","!","`","@","#","$","%","^","&","*","(",")","-","_","+","=","\\","|","{","[","]","}",":","'","\"",";","<",">",",",".","?","/"};
            string[] specialStrCN=new string []{"！","·","#","￥","%","……","—","*","（","）","-","——","。","+","|","、","《","》","，","：","；","“","‘","？"};
            foreach (string s in specialStrEN)
            {
                if(str.IndexOf(s)!=-1)
                {
                    return true ;
                }
            }
            foreach (string s in specialStrCN)
            {
                if(str.IndexOf(s)!=-1)
                {
                    return true;
                }
            }
            return false ;
        }
        #endregion 

        #region 比较验证码

        /// <summary>
        /// 比较验证码
        /// </summary>
        /// <param name="TxtCheckCode">需要对比的文本框</param>
        /// <param name="strCodeNum">验证码</param>
        /// <returns>true为对比正确,false为对比错误</returns>
        public static bool ContrastVerificationCode(TextBox TxtCheckCode, string strCodeNum)
        {

            if (!TxtCheckCode.Text.Trim().Equals(strCodeNum) || strCodeNum.Equals(string.Empty))
            {
                TxtCheckCode.Text = string.Empty;
                return false;
            }
            else
            {
                return true;
            }
        }
        #endregion

        #region 是否是字符
        public static bool IsString(object o)
        {
            if (Type.GetTypeCode(o.GetType()) ==  TypeCode.String)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        private static Regex RegPhone = new Regex("^[0-9]+[-]?[0-9]+[-]?[0-9]$");
        private static Regex RegNumber = new Regex("^[0-9]+$");
        private static Regex RegNumberSign = new Regex("^[+-]?[0-9]+$");
        private static Regex RegDecimal = new Regex("^[0-9]+[.]?[0-9]+$");
        private static Regex RegDecimalSign = new Regex("^[+-]?[0-9]+[.]?[0-9]+$"); //等价于^[+-]?\d+[.]?\d+$
        private static Regex RegEmail = new Regex("^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");//w 英文字母或数字的字符串，和 [a-zA-Z0-9] 语法一样 
        private static Regex RegCHZN = new Regex("[\u4e00-\u9fa5]");
        private static Regex RegTel = new Regex(@"^((13[0-9])|(15[02789])|(18[679]))\d{8}$"); //手机号正则表达
        #region 中文检测

        /// <summary>
        /// 检测是否有中文字符
        /// </summary>
        /// <param name="inputData"></param>
        /// <returns></returns>
        public static bool IsHasCHZN(string inputData)
        {
            Match m = RegCHZN.Match(inputData);
            return m.Success;
        }

        #endregion

        public static bool IsTel(string tel)
        {
            Match m = RegTel.Match(tel);
            return m.Success;
        }

        #region 日期格式判断
        /// <summary>
        /// 日期格式字符串判断
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsDateTime(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    DateTime.Parse(str);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 是否由特定字符组成
        public static bool isContainSameChar(string strInput)
        {
            string charInput = string.Empty;
            if (!string.IsNullOrEmpty(strInput))
            {
                charInput = strInput.Substring(0, 1);
            }
            return isContainSameChar(strInput, charInput, strInput.Length);
        }

        public static bool isContainSameChar(string strInput, string charInput, int lenInput)
        {
            if (string.IsNullOrEmpty(charInput))
            {
                return false;
            }
            else
            {
                Regex RegNumber = new Regex(string.Format("^([{0}])+$", charInput));
                //Regex RegNumber = new Regex(string.Format("^([{0}]{{1}})+$", charInput,lenInput));
                Match m = RegNumber.Match(strInput);
                return m.Success;
            }
        }
        #endregion

        #region 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// <summary>
        /// 检查输入的参数是不是某些定义好的特殊字符：这个方法目前用于密码输入的安全检查
        /// </summary>
        public static bool isContainSpecChar(string strInput)
        {
            string[] list = new string[] { "123456", "654321" };
            bool result = new bool();
            for (int i = 0; i < list.Length; i++)
            {
                if (strInput == list[i])
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        #endregion

        #region  检查字符串最大长度，返回指定长度的串
        /// <summary>
        /// 检查字符串最大长度，返回指定长度的串
        /// </summary>
        /// <param name="sqlInput">输入字符串</param>
        /// <param name="maxLength">最大长度</param>
        /// <returns></returns>			
        public static string SqlText(string sqlInput, int maxLength)
        {
            if (sqlInput != null && sqlInput != string.Empty)
            {
                sqlInput = sqlInput.Trim();
                if (sqlInput.Length > maxLength)//按最大长度截取字符串
                    sqlInput = sqlInput.Substring(0, maxLength);
            }
            return sqlInput;
        }
        #endregion

        #region 其它
        
		/// <summary>
		/// 字符串编码
		/// </summary>
		/// <param name="inputData"></param>
		/// <returns></returns>
		public static string HtmlEncode(string inputData)
		{
			return HttpUtility.HtmlEncode(inputData);
		}
		/// <summary>
		/// 设置Label显示Encode的字符串
		/// </summary>
		/// <param name="lbl"></param>
		/// <param name="txtInput"></param>
		public static void SetLabel(System.Windows.Controls.TextBlock lbl, string txtInput)
		{
			lbl.Text  = HtmlEncode(txtInput);
		}
        public static void SetLabel(System.Windows.Controls.TextBlock lbl, object inputObj)
		{
			SetLabel(lbl, inputObj.ToString());
		}		
		/// <summary>
        /// 字符串清理
		/// </summary>
		/// <param name="inputString"></param>
		/// <param name="maxLength"></param>
		/// <returns></returns>
		public static string InputText(string inputString, int maxLength) 
		{			
			StringBuilder retVal = new StringBuilder();

			// 检查是否为空
			if ((inputString != null) && (inputString != String.Empty)) 
			{
				inputString = inputString.Trim();
				
				//检查长度
				if (inputString.Length > maxLength)
					inputString = inputString.Substring(0, maxLength);
				
				//替换危险字符
				for (int i = 0; i < inputString.Length; i++) 
				{
					switch (inputString[i]) 
					{
						case '"':
							retVal.Append("&quot;");
							break;
						case '<':
							retVal.Append("&lt;");
							break;
						case '>':
							retVal.Append("&gt;");
							break;
						default:
							retVal.Append(inputString[i]);
							break;
					}
				}				
				retVal.Replace("'", " ");// 替换单引号
			}
			return retVal.ToString();
			
		}
		/// <summary>
		/// 转换成 HTML code
		/// </summary>
		/// <param name="str">string</param>
		/// <returns>string</returns>
		public static string Encode(string str)
		{			
			str = str.Replace("&","&amp;");
			str = str.Replace("'","''");
			str = str.Replace("\"","&quot;");
			str = str.Replace(" ","&nbsp;");
			str = str.Replace("<","&lt;");
			str = str.Replace(">","&gt;");
			str = str.Replace("\n","<br>");
			return str;
		}
		/// <summary>
		///解析html成 普通文本
		/// </summary>
		/// <param name="str">string</param>
		/// <returns>string</returns>
		public static string Decode(string str)
		{			
			str = str.Replace("<br>","\n");
			str = str.Replace("&gt;",">");
			str = str.Replace("&lt;","<");
			str = str.Replace("&nbsp;"," ");
			str = str.Replace("&quot;","\"");
			return str;
		}

        public static string SqlTextClear(string sqlText)
        {
            if (sqlText == null)
            {
                return null;
            }
            if (sqlText == "")
            {
                return "";
            }
            sqlText = sqlText.Replace(",", "");//去除,
            sqlText = sqlText.Replace("<", "");//去除<
            sqlText = sqlText.Replace(">", "");//去除>
            sqlText = sqlText.Replace("--", "");//去除--
            sqlText = sqlText.Replace("'", "");//去除'
            sqlText = sqlText.Replace("\"", "");//去除"
            sqlText = sqlText.Replace("=", "");//去除=
            sqlText = sqlText.Replace("%", "");//去除%
            sqlText = sqlText.Replace(" ", "");//去除空格
            return sqlText;
        }
		#endregion
    }
}
