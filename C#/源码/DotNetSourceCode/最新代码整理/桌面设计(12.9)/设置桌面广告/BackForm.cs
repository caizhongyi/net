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
            //处理线程问题
            BackForm.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            Formwidth = this.Width;
            LanTimeManageInfo.ColumnInit();

        }
        private void BackForm_Load(object sender, EventArgs e)
        {  
            //判断文件夹是否存在
            IFileOperate ifo = FileFactory.GetFileInfo();
            ifo.CreateFolder(Directory.GetCurrentDirectory() + "\\" + "NewAdpictures");

            //获取局域网信息
            ClassStartUdpThread startUdpThread = new ClassStartUdpThread();
            Thread tUdpThread = new Thread(new ThreadStart(startUdpThread.StartUdpThread));
            tUdpThread.IsBackground = true;
            tUdpThread.Start();

            //获取自己最新日期
            GetMyNewTime();

            //获取服务器上的最新日期
            ServiceTime();

            //获取局域网内的最新日期
            CompareLanNewDay();
            //停止取局域网IP
            LanTimeManageInfo.IsTrue = false;
            //比较时间过程
            csut.GetTimeInfo();

        }

        private void BackForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //获取自己的最新日期进行广播
        void GetMyNewTime()
        {
            //获取文件最新时间
            DataSet ds = new DataSet();
            if (File.Exists("AdvInfo.xml"))
            {
                ds.ReadXml("AdvInfo.xml");
                fdti.Filedatetime = Convert.ToDateTime(ds.Tables[1].Rows[0][0].ToString());
            }
            else
                fdti.Filedatetime = Convert.ToDateTime("2008-01-01 00:00:00");
        }
        //获取服务上的最新时间
        void ServiceTime()
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("Wb_Id", System.Configuration.ConfigurationManager.AppSettings["WbID"].ToString());
            try
            {
                byte[] TimeInfo = WebClientObj.UploadValues("http://www.wb66.cn/SelIssueTime.aspx", "POST", PostVars);
                string IssueTime = System.Text.Encoding.GetEncoding("UTF-8").GetString(TimeInfo).Trim();
                //这是获取返回信息
                fdti.Servicetime = Convert.ToDateTime(IssueTime);
            }
            catch
            {
                fdti.Servicetime = Convert.ToDateTime("2008-01-01 00:00:00");
            }
            WebClientObj.Dispose();
        }
        //获得局域网内电脑数据最新日期
        void CompareLanNewDay()
        {
            DataView dv =WbSystem.GetIp.LanTimeManageInfo.mydt.DefaultView;
            dv.Sort = "mDAY desc";
            DataTable dt2 = dv.Table.Copy();
            if (WbSystem.GetIp.LanTimeManageInfo.mydt.Rows.Count> 0)
            {
                //获取日期
                fdti.Lantime = Convert.ToDateTime(dt2.Rows[0][1].ToString());
   
                //获取IP地址
                ipi.Ipadress = dt2.Rows[0][0].ToString();
            }
            else
            {
                fdti.Lantime =Convert.ToDateTime("2008-01-01 00:00:00");
            }
        }
    }
}