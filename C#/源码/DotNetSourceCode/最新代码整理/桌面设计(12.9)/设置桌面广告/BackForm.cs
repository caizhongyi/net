using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SetWallpaper;
using System.Diagnostics;
using System.Threading;
using WindowsApplication1.ListenInfo;
using WindowsApplication1.SendInfo;
using ClassStartUdpThreadnamespace;
using WbSystem.FileManage;
using System.IO;
using WbSystem.GetIp;
using GetIpInfo;

namespace WbSystem
{
    public partial class BackForm : Form
    {
        public static int Formwidth;
        IPInfo ipi = new IPInfo();
        FileDateTimeInfo fdti = new FileDateTimeInfo();
       ClassStartUdpThread csut = new ClassStartUdpThread();
        public BackForm()
        {
            //�����߳�����
            BackForm.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            Formwidth = this.Width;
            LanTimeManageInfo.ColumnInit();

        }
        private void BackForm_Load(object sender, EventArgs e)
        {  
            //�ж��ļ����Ƿ����
            IFileOperate ifo = FileFactory.GetFileInfo();
            ifo.CreateFolder(Directory.GetCurrentDirectory() + "\\" + "NewAdpictures");

            //��ȡ��������Ϣ
            ClassStartUdpThread startUdpThread = new ClassStartUdpThread();
            Thread tUdpThread = new Thread(new ThreadStart(startUdpThread.StartUdpThread));
            tUdpThread.IsBackground = true;
            tUdpThread.Start();

            //��ȡ�Լ���������
            GetMyNewTime();

            //��ȡ�������ϵ���������
            ServiceTime();

            //��ȡ�������ڵ���������
            CompareLanNewDay();
            //ֹͣȡ������IP
            LanTimeManageInfo.IsTrue = false;
            //�Ƚ�ʱ�����
            csut.GetTimeInfo();

        }

        private void BackForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //��ȡ�Լ����������ڽ��й㲥
        void GetMyNewTime()
        {
            //��ȡ�ļ�����ʱ��
            DataSet ds = new DataSet();
            if (File.Exists("AdvInfo.xml"))
            {
                ds.ReadXml("AdvInfo.xml");
                fdti.Filedatetime = Convert.ToDateTime(ds.Tables[1].Rows[0][0].ToString());
            }
            else
                fdti.Filedatetime = Convert.ToDateTime("2008-01-01 00:00:00");
        }
        //��ȡ�����ϵ�����ʱ��
        void ServiceTime()
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("Wb_Id", System.Configuration.ConfigurationManager.AppSettings["WbID"].ToString());
            try
            {
                byte[] TimeInfo = WebClientObj.UploadValues("http://www.wb66.cn/SelIssueTime.aspx", "POST", PostVars);
                string IssueTime = System.Text.Encoding.GetEncoding("UTF-8").GetString(TimeInfo).Trim();
                //���ǻ�ȡ������Ϣ
                fdti.Servicetime = Convert.ToDateTime(IssueTime);
            }
            catch
            {
                fdti.Servicetime = Convert.ToDateTime("2008-01-01 00:00:00");
            }
            WebClientObj.Dispose();
        }
        //��þ������ڵ���������������
        void CompareLanNewDay()
        {
            DataView dv =WbSystem.GetIp.LanTimeManageInfo.mydt.DefaultView;
            dv.Sort = "mDAY desc";
            DataTable dt2 = dv.Table.Copy();
            if (WbSystem.GetIp.LanTimeManageInfo.mydt.Rows.Count> 0)
            {
                //��ȡ����
                fdti.Lantime = Convert.ToDateTime(dt2.Rows[0][1].ToString());
   
                //��ȡIP��ַ
                ipi.Ipadress = dt2.Rows[0][0].ToString();
            }
            else
            {
                fdti.Lantime =Convert.ToDateTime("2008-01-01 00:00:00");
            }
        }
    }
}