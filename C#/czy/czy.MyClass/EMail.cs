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
using System.Web.Mail;
using System.Net.Mail;
using System.Net;

namespace czy.MyClass
{
    /// <summary>
    /// JMail 的摘要说明
    /// </summary>
    public class EMail
    {
        #region

        string _ADDRESS_FROM = "from@gail.com";
        string _ADDRESS_TO = "to@gmail.com";
        string _USER_ID = "MyAccount";
        string _PASSWORD = "password";
        string _SMTP_SERVER = "smtp.gmail.com"; 
        int _PORT = 587;
        /// <summary>
        /// 发件人地址
        /// </summary>
        public string ADDRESS_FROM
        {
            get { return _ADDRESS_FROM; }
            set { _ADDRESS_FROM = value; }
        }
        
        /// <summary>
        /// 收件人地址
        /// </summary>
        public string ADDRESS_TO
        {
            get { return _ADDRESS_TO; }
            set { _ADDRESS_TO = value; }
        }
      
        /// <summary>
        /// 用户名
        /// </summary>
        public string USER_ID
        {
            get { return _USER_ID; }
            set { _USER_ID = value; }
        }
     
        /// <summary>
        /// 密码
        /// </summary>
        public string PASSWORD
        {
            get { return _PASSWORD; }
            set { _PASSWORD = value; }
        }
       
        /// <summary>
        /// smpt
        /// </summary>
        public string SMTP_SERVER
        {
            get { return _SMTP_SERVER; }
            set { _SMTP_SERVER = value; }
        }
       
        /// <summary>
        /// 端口
        /// </summary>
        public int PORT
        {
            get { return _PORT; }
            set { _PORT = value; }
        }

        #endregion

         /// <summary>
         /// 初始化
         /// </summary>
         /// <param name="fromMailAddress">发送地址</param>
         /// <param name="userName">用户名</param>
         /// <param name="passWord">密码</param>
        /// <param name="smtpServer">服务器地址 例:mail.wb66.cn/smtp.qq.com</param>
         /// <param name="port">端口</param>
        public EMail(string fromMailAddress, string userName, string passWord, string smtpServer,int port)
        {
            this._ADDRESS_FROM = fromMailAddress;
            this._USER_ID = userName;
            this._SMTP_SERVER = smtpServer;
            this._PASSWORD = passWord;
            this._PORT = port;
        }
       

        #region SMTP发送邮件(Html内容)
        /// <summary>
        /// SMTP发送邮件(Html内容)
        /// </summary>
        /// <param name="toMailAddress">接收邮件地址</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="bodyMessage">信息</param>
        public void SendMailBySMTP(string toMailAddress, string mailTitle, string HtmlTitle, string HtmlBody, string HtmlHeadLink)
        {
            #region 发送邮件内容#region 发送邮件内容
            string strBody = "<html>";
            strBody = strBody + "<head>";
            strBody = strBody + "<meta http-equiv='Content-Type' content='text/strBody; charset=gb2312'/>";
            strBody = strBody + "<title>" + HtmlTitle + "</title>";
            strBody = strBody + HtmlHeadLink;
            strBody = strBody + "</head>";
            strBody = strBody + "<body>";
            strBody = strBody + ""+ HtmlBody+"";
            strBody = strBody + "</body>";
            strBody = strBody + "</html>";
            #endregion

            System.Web.Mail.MailMessage newMail = new System.Web.Mail.MailMessage();

            newMail.From = this._ADDRESS_FROM;
            newMail.To = toMailAddress;
            newMail.BodyFormat = MailFormat.Html;
            newMail.BodyEncoding = System.Text.Encoding.Default;
            newMail.Subject = mailTitle;
            newMail.Body = strBody;
            newMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication 
            newMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", this._ADDRESS_FROM); //set your username here 
            newMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", this._PASSWORD); //set your password here 
            newMail.Priority = System.Web.Mail.MailPriority.High;
            
            SmtpMail.SmtpServer = this._SMTP_SERVER;
            SmtpMail.Send(newMail);

            #region 发送邮件:Jmail
            /**/
            /*
            jmail.Message Jmail = new jmail.Message();
            Jmail.Silent = true;
            Jmail.Logging = true;
            Jmail.Charset = "gb2312";
            Jmail.ContentType = "text/html";
            Jmail.AddRecipient(strEmail, "", "");
            Jmail.From = "yjj8756@163.com";
            Jmail.FromName = "xx网系统邮件";
            Jmail.MailServerUserName = "yjj8756@163.com";
            Jmail.MailServerPassWord = "123456";
            Jmail.Subject = "测试发送邮件";
            Jmail.Body = strBody;
            Jmail.Send("mail.163.com", false);
            Jmail.Close();
            */
            #endregion


        }
        #endregion

        #region SMTP发送邮件
        /// <summary>
        /// SMTP发送邮件
        /// </summary>
        /// <param name="toMailAddress">接收邮件地址</param>
        /// <param name="mailTitle">邮件标题</param>
        ///  /// <param name="bodyMessage">信息</param>
        public  void SendMailBySMTP(string toMailAddress, string mailTitle, string bodyMessage)
        {


            System.Web.Mail.MailMessage newMail = new System.Web.Mail.MailMessage();

            newMail.From = this._ADDRESS_FROM;
            newMail.To = toMailAddress;
            newMail.BodyFormat = MailFormat.Html;
            newMail.BodyEncoding = System.Text.Encoding.Default;
            newMail.Subject = mailTitle;
            newMail.Body = bodyMessage;
            newMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1"); //basic authentication 
            newMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", this._ADDRESS_FROM); //set your username here 
            newMail.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", this._PASSWORD); //set your password here 
            newMail.Priority = System.Web.Mail.MailPriority.High;

          

            SmtpMail.SmtpServer = this._SMTP_SERVER;
            SmtpMail.Send(newMail);

            #region 发送邮件:Jmail
            /**/
            /*
            jmail.Message Jmail = new jmail.Message();
            Jmail.Silent = true;
            Jmail.Logging = true;
            Jmail.Charset = "gb2312";
            Jmail.ContentType = "text/html";
            Jmail.AddRecipient(strEmail, "", "");
            Jmail.From = "yjj8756@163.com";
            Jmail.FromName = "xx网系统邮件";
            Jmail.MailServerUserName = "yjj8756@163.com";
            Jmail.MailServerPassWord = "123456";
            Jmail.Subject = "测试发送邮件";
            Jmail.Body = strBody;
            Jmail.Send("mail.163.com", false);
            Jmail.Close();
            */
            #endregion


        }
        #endregion

        #region Net发送邮件
        /// <summary>
        /// Net发送邮件
        /// </summary>
        /// <param name="toMailAddress">接收邮件地址</param>
        /// <param name="mailTitle">邮件标题</param>
        /// <param name="bodyMessage">信息</param>
        public void SendMailByNet(string toMailAddress, string mailTitle, string bodyMessage)
        {
            MailAddress from = new MailAddress(this._ADDRESS_FROM);
            MailAddress to = new MailAddress(toMailAddress);
            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(from, to);
            message.Subject = mailTitle;
            message.IsBodyHtml = true;//邮件是否是HTML格式 
            message.Body = bodyMessage;
            message.BodyEncoding = System.Text.Encoding.Default;
            SmtpClient client = new SmtpClient(this._SMTP_SERVER);
            client.UseDefaultCredentials = true;
            client.Credentials = new System.Net.NetworkCredential(this._USER_ID, this._PASSWORD);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.Send(message);
        }
        #endregion

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="toAddress">接收邮件地址</param>
        /// <param name="title">邮件标题</param>
        /// <param name="content">信息</param>
        /// <param name="enableSsl">是否ssl加密</param>
        /// <param name="isHTML">是否是HTML邮件 </param>
        public string SendMailBySmtpClient(string toMailAddress, string title, string content, bool enableSsl,bool isHTML)
        {

            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            string SendName = toMailAddress;//page

            msg.To.Add(SendName);

            msg.From = new MailAddress(SendName, null, System.Text.Encoding.UTF8);

            msg.Subject = title;//邮件标题 
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
            msg.Body = content;//邮件内容 
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
            msg.IsBodyHtml = isHTML;//是否是HTML邮件 

            
            msg.Priority = System.Net.Mail.MailPriority.High;//邮件优先级 
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(this._ADDRESS_FROM, this._PASSWORD);//上述写你的GMail邮箱和密码 
            client.Port =  this._PORT;//Gmail使用的端口 
            client.Host =this._SMTP_SERVER;
            client.EnableSsl = enableSsl;//经过ssl加密 
            try
            {
                client.Send(msg);
                return "发送成功!";
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return ex.ToString ();
            }



        }
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="toAddress">接收邮件地址</param>
        /// <param name="title">邮件标题</param>
        /// <param name="content">信息</param>
        /// <param name="enableSsl">是否ssl加密</param>
        /// <param name="isHTML">是否是HTML邮件 </param>
        public string SendMailBySmtpClient(string toMailAddress, string title, string HTMLContent, string HTMLTitle, string HTMLHead,bool enableSsl)
        {
            #region 发送邮件内容#region 发送邮件内容
            string strBody = "<html>";
            strBody = strBody + "<head>";
            //strBody = strBody + "<meta http-equiv='Content-Type' content='text/strBody; charset=gb2312'/>";
            strBody = strBody + "<title>" + HTMLTitle + "</title>";
            strBody = strBody + HTMLHead;
            strBody = strBody + "</head>";
            strBody = strBody + "<body>";
            strBody = strBody + "" + HTMLContent + "";
            strBody = strBody + "</body>";
            strBody = strBody + "</html>";
            #endregion


            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            string SendName = toMailAddress;//page

            msg.To.Add(SendName);

            msg.From = new MailAddress(SendName, null, System.Text.Encoding.UTF8);

            msg.Subject = title;//邮件标题 
            msg.SubjectEncoding = System.Text.Encoding.UTF8;//邮件标题编码 
            msg.Body = strBody;//邮件内容 
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码 
            msg.IsBodyHtml = true ;//是否是HTML邮件 


            msg.Priority = System.Net.Mail.MailPriority.High;//邮件优先级 
            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(this._ADDRESS_FROM, this._PASSWORD);//上述写你的GMail邮箱和密码 
            client.Port = this._PORT;//Gmail使用的端口 
            client.Host = this._SMTP_SERVER;
            client.EnableSsl = enableSsl;//经过ssl加密 
            try
            {
                client.Send(msg);
                return "发送成功!";
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                return ex.ToString();
            }



        }

    
    }
}