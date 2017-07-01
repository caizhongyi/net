using System;
using System.Net.Mail;
using JuSNS.Config;

namespace JuSNS.Common
{
    /// <summary>
    /// �����ʼ�
    /// </summary>
    public abstract class EMail
    {
        protected string _From = EmailConfig.from;
        protected string _Subject;
        protected string _Body;
        protected string _To;
        /// <summary>
        /// �ʼ�����
        /// </summary>
        public string Subject
        {
            get { return _Subject; }
            set { _Subject = value; }
        }
        /// <summary>
        /// �ʼ�����
        /// </summary>
        public string Body
        {
            get { return _Body; }
            set { _Body = value; }
        }
        /// <summary>
        /// �ռ���ַ
        /// </summary>
        public string To
        {
            get { return _To; }
            set { _To = value; }
        }
        /// <summary>
        /// ������,Ĭ��Ϊϵͳ����
        /// </summary>
        public string From
        {
            get { return _From; }
            set { _From = value; }
        }
        /// <summary>
        /// ����
        /// </summary>
        public abstract void Send();
        /// <summary>
        /// Ⱥ���ʼ�
        /// </summary>
        public abstract void GroupSend(string[] groupTo);
        /// <summary>
        /// ����һ���ʼ�ʵ��
        /// </summary>
        /// <returns></returns>
        public static EMail CreateInstance()
        {
            return new SmtpMail();
        }
    }

    /// <summary>
    /// SMTP�ʼ�
    /// </summary>
    public class SmtpMail : EMail
    {
        private SmtpClient _smtpClient;
        public SmtpMail()
        {
            _smtpClient = new SmtpClient();
            _smtpClient.Timeout = 100000;
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//ָ�������ʼ����ͷ�ʽ
            _smtpClient.Host = EmailConfig.host;//ָ��SMTP������
            _smtpClient.Port = EmailConfig.port;
            _smtpClient.Credentials = new System.Net.NetworkCredential(EmailConfig.username, EmailConfig.password);//�û���������
            _smtpClient.EnableSsl = EmailConfig.enablessl;
            if (_smtpClient.Port != 25)
            {//gmail:587
                _smtpClient.EnableSsl = true;
            }
        }


        /// <summary>
        /// �����ʼ�
        /// </summary>
        public override void Send()
        {
            MailMessage _mailMessage = PreSend();
            _mailMessage.To.Add(new MailAddress(_To));
            SendOut(_mailMessage);
        }

        /// <summary>
        /// Ⱥ��
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
        /// ׼��MailMessage��
        /// </summary>
        /// <returns></returns>
        private MailMessage PreSend()
        {
            MailMessage _mailMessage = new MailMessage();
            _mailMessage.From = new MailAddress(_From);
            _mailMessage.Subject = this._Subject;
            _mailMessage.Body = this._Body;//����
            _mailMessage.BodyEncoding = System.Text.Encoding.Default;//���ı���
            _mailMessage.SubjectEncoding = System.Text.Encoding.Default;
            _mailMessage.IsBodyHtml = true;//����ΪHTML��ʽ
            _mailMessage.Priority = MailPriority.Normal;//���ȼ�
            return _mailMessage;
        }

        /// <summary>
        /// ����
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
                //To��CC �� BCC ��û���ռ��ˡ�
            }
            catch (ObjectDisposedException e)
            {
                throw e;
                // �˶����ѱ��ͷš�
            }
            catch (InvalidOperationException e)
            {
                throw e;
                //�� SmtpClient ��һ�� SendAsync �������ڽ��С�- �� - 
                //Host Ϊ �����ã��� Visual Basic ��Ϊ Nothing����- �� -
                //Host �ǿ��ַ��� ("")������ Port ���㡣
            }
            catch (SmtpFailedRecipientsException e)
            {
                throw e;
                //message �޷����ݸ� To��CC �� BCC �е�һ�������ռ��ˡ� 
            }
            catch (SmtpException e)
            {
                throw e;
                //���ӵ� SMTP ������ʧ�ܡ�- �� -�����֤ʧ�ܡ�- �� -������ʱ��
            }
        }
    }
}
