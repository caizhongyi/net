using System;
using System.Net.Mail;
using JuSNS.Config;

namespace JuSNS.Common
{
    /// <summary>
    /// 电子邮件
    /// </summary>
    public abstract class EMail
    {
        protected string _From = EmailConfig.from;
        protected string _Subject;
        protected string _Body;
        protected string _To;
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        /// <summary>
        /// 邮件内容
        /// </summary>
        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }
        /// <summary>
        /// 收件地址
        /// </summary>
        public string To
        {
            get { return _To; }
            set { _To = value; }
        }
        /// <summary>
        /// 发件人,默认为系统邮箱
        /// </summary>
        public string From
        {
            get { return _From; }
            set { _From = value; }
        }
        /// <summary>
        /// 发送
        /// </summary>
        public abstract void Send();
        /// <summary>
        /// 群发邮件
        /// </summary>
        public abstract void GroupSend(string[] groupTo);
        /// <summary>
        /// 创建一个邮件实体
        /// </summary>
        /// <returns></returns>
        public static EMail CreateInstance()
        {
            return new SmtpMail();
        }
    }

    /// <summary>
    /// SMTP邮件
    /// </summary>
    public class SmtpMail : EMail
    {
        private SmtpClient _smtpClient;
        public SmtpMail()
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Timeout = 100000;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.Host = EmailConfig.host;//指定SMTP服务器
            _smtpClient.Port = EmailConfig.port;
            _smtpClient.Credentials = new System.Net.NetworkCredential(EmailConfig.username, EmailConfig.password);//用户名和密码
            _smtpClient.EnableSsl = EmailConfig.enablessl;
            if (_smtpClient.Port != 25)
            {//gmail:587
                _smtpClient.EnableSsl = true;
            }
        }


        /// <summary>
        /// 发送邮件
        /// </summary>
        public override void Send()
        {
            MailMessage _mailMessage = PreSend();
            _mailMessage.To.Add(new MailAddress(_To));
            SendOut(_mailMessage);
        }

        /// <summary>
        /// 群发
        /// </summary>
        /// <param name="groupTo"></param>
        public override void GroupSend(string[] groupTo)
        {
            MailMessage _mailMessage = PreSend();
            foreach (string to in groupTo)
            {
                _mailMessage.To.Add(new MailAddress(to));
            }
            SendOut(_mailMessage);
        }


        /// <summary>
        /// 准备MailMessage类
        /// </summary>
        /// <returns></returns>
        private MailMessage PreSend()
        {
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = new MailAddress(_From);
            _mailMessage.Subject = this._Subject;
            _mailMessage.Body = this._Body;//内容
            _mailMessage.BodyEncoding = System.Text.Encoding.Default;//正文编码
            _mailMessage.SubjectEncoding = System.Text.Encoding.Default;
            _mailMessage.IsBodyHtml = true;//设置为HTML格式
            _mailMessage.Priority = MailPriority.Normal;//优先级
            return _mailMessage;
        }

        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="mailMessage"></param>
        private void SendOut(MailMessage mailMessage)
        {
            try
            {
                _smtpClient.Send(mailMessage);
            }
            catch (ArgumentNullException e)
            {
                throw e;
            }
            catch (ArgumentOutOfRangeException e)
            {
                throw e;
                //To、CC 和 BCC 中没有收件人。
            }
            catch (ObjectDisposedException e)
            {
                throw e;
                // 此对象已被释放。
            }
            catch (InvalidOperationException e)
            {
                throw e;
                //此 SmtpClient 有一个 SendAsync 调用正在进行。- 或 - 
                //Host 为 空引用（在 Visual Basic 中为 Nothing）。- 或 -
                //Host 是空字符串 ("")。或者 Port 是零。
            }
            catch (SmtpFailedRecipientsException e)
            {
                throw e;
                //message 无法传递给 To、CC 或 BCC 中的一个或多个收件人。 
            }
            catch (SmtpException e)
            {
                throw e;
                //连接到 SMTP 服务器失败。- 或 -身份验证失败。- 或 -操作超时。
            }
        }
    }
}
