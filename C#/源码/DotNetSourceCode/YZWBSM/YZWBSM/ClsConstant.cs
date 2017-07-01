using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Net;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Threading;

namespace YZWBSM
{
    class ClsConstant
    {

        public static DataSet myds = new DataSet();
        public static DataTable mydt = new DataTable("stest");
        public static DataRow mydr;
        public static bool bListenNewD = true;
        public static bool bCompareNewD = false ;
        public static string snewday;
        public static string sWb_Id;
       // public static string mycomip;
        public static void ColumnInit()
        {
            mydt.Columns.Add(new DataColumn("mIP", typeof(string)));
            mydt.Columns.Add(new DataColumn("mDAY", typeof(string)));

        }
        public static void Addmydr(string smip,string smDay)
        {
            string sip = smip;
            string sday = smDay;
            int irow = mydt.Rows.Count;
            int iCheckrow = 0;
            for (int i = 0; i < irow; i++)
            {
                string s1 = mydt.Rows[i][0].ToString();
                string s2 = mydt.Rows[i][1].ToString();
                if (s1 == sip)
                {
                    iCheckrow = i+1;

                }
            }
            if (iCheckrow > 0)
            {
                mydt.Rows[iCheckrow - 1][1] = smDay;
            }
            else
            {
                mydr = mydt.NewRow();
                mydr[0] = sip;
                mydr[1] = sday;
                mydt.Rows.Add(mydr);
            }
            
        }

        public static void CompareLocalNewDay()   //与本机数据日期进行比较
        {
            bCompareNewD = true;


        }

        public static void CompareLanNewDay()   //与局域网内电脑数据日期进行比较
        {
            DataView dv = mydt.DefaultView;
            dv.Sort = "mDAY desc";
            DataTable dt2 = dv.Table.Copy();
            string nnewday = dt2.Rows[0][1].ToString();
            bCompareNewD = true;

        }

        public static void Getnewday()
        {
            System.Net.WebClient WebClientObj = new System.Net.WebClient();
            System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
            PostVars.Add("WB_ID", "YZ001");
            //PostVars.Add("A2", "0");
            //PostVars.Add("A3", "000");
            try
            {
                byte[] byRemoteInfo = WebClientObj.UploadValues("http://www.wb66.cn/xiangmu/yztest.asp", "POST", PostVars);
                //下面都没用啦，就上面一句话就可以了
                string sRemoteInfo = System.Text.Encoding.Default.GetString(byRemoteInfo);
                //这是获取返回信息
                snewday = sRemoteInfo;
            }
            catch
            { }
            WebClientObj.Dispose(); 
        }
        public static void GetXmlInfo()
        {
            try 
            {
    
               System.Net.WebClient WebClientObj = new System.Net.WebClient();
               System.Collections.Specialized.NameValueCollection PostVars = new System.Collections.Specialized.NameValueCollection();
               PostVars.Add("Wb_Id", sWb_Id);

               byte[] byRemoteInfo = WebClientObj.UploadValues("http://www.wb66.cn/SelAdvInfoByWbId.aspx", "POST", PostVars);

               string sRemoteInfo = System.Text.Encoding.GetEncoding("UTF-8").GetString(byRemoteInfo).Trim();

               //Console.Write(sRemoteInfo);
               /*               
               
               Random random = new Random();

               for (int i = 0; i < 10000; i++)
               {
                   ClsUtility.Beep(random.Next(10000), 100);
               }
               */
                //bool rtvl;
                //rtvl = ClsUtility.RemoveDirectory("G:\\csharp\\zz\\YZWBSM\\YZWBSM\\bin\\Debug\\newaddpicture");

                //string urlString = "http://www.wb66.cn/SelAdvInfoByWbId.aspx";    
                //WebClient myWebClient = new WebClient();    
                //string postData = "Wb_Id="+sWb_Id;    
                //myWebClient.Headers.Add("Content-Type","application/x-www-form-urlencoded");
                //myWebClient.Headers.Add("Cookie", "Wb_Id="+sWb_Id +";");    
                //byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(postData);;
                //byte[] byRemoteInfo = myWebClient.UploadData(urlString, "POST", byteArray);
                //string sRemoteInfo = System.Text.Encoding.GetEncoding("UTF-8").GetString(byRemoteInfo).Trim();
  
               string CurPath = Environment.CurrentDirectory;
               string PicNewDir = CurPath + "\\newaddpicture";
               string XmlPath = PicNewDir + "\\AdvInfo.xml";
               if (Directory.Exists(PicNewDir))
                {
                    DeleteInDir(PicNewDir);
                }
                Directory.CreateDirectory(PicNewDir);
               
                StreamWriter sw = File.CreateText(XmlPath);
                sw.Write(sRemoteInfo);
                sw.Close();

                DataSet myDS = new DataSet();
                myDS.ReadXml(XmlPath);
                for (int i = 0; i < myDS.Tables[0].Rows.Count; i++)
                {
                    if (myDS.Tables[0].Rows[i][2].ToString() != "")
                    {
                       DownloadFilesFS("http://www.wb66.cn/Adpictures/" + myDS.Tables[0].Rows[i][2].ToString().Replace("~/", ""), PicNewDir);//下载..
                    }
                }

                //追加最新广告发布日期到XML信息文件
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(XmlPath);
                XmlNode root = xmlDoc.SelectSingleNode("NewDataSet");//查找<NewDataSet>
                XmlElement xe1 = xmlDoc.CreateElement("Time");
                XmlElement xe2 = xmlDoc.CreateElement("CreateTime");
                xe2.InnerText = snewday;
                xe1.AppendChild(xe2);
                root.AppendChild(xe1);//添加到<NewDataSet>节点中
                xmlDoc.Save(XmlPath);
                WebClientObj.Dispose(); 
            }
            //catch (WebException webEx)
            catch
            {
                //Console.Write(webEx.Message);
            }
            

        }

        public static void DeleteInDir(string szDirPath)
        {
            if (szDirPath.Trim() == "" || !Directory.Exists(szDirPath))
                return;
            DirectoryInfo dirInfo = new DirectoryInfo(szDirPath);

            FileInfo[] fileInfos = dirInfo.GetFiles();
            if (fileInfos != null && fileInfos.Length > 0)
            {
                foreach (FileInfo fileInfo in fileInfos)
                {
                    File.Delete(fileInfo.FullName); //删除文件
                }
            }

            DirectoryInfo[] dirInfos = dirInfo.GetDirectories();
            if (dirInfos != null && dirInfos.Length > 0)
            {
                foreach (DirectoryInfo childDirInfo in dirInfos)
                {
                    DeleteInDir(childDirInfo.FullName); //递归
                }
            }
            Directory.Delete(dirInfo.FullName, true); //删除目录
        }

        public static void DownloadFilesFS(string URL, string Dir)
        {
            WebClient client = new WebClient();
            string fileName = URL.Substring(URL.LastIndexOf("/") + 1);  //被下载的文件名

            string Path = Dir + "\\" + fileName;   //另存为的绝对路径＋文件名

            try
            {
                WebRequest myre = WebRequest.Create(URL);
            }
            catch
            {
                //MessageBox.Show(exp.Message,"Error"); 
            }

            try
            {
                client.DownloadFile(URL, Path);

            }
            catch
            {
               // MessageBox.Show(exp.Message,"Error");
            }
        }



    }
}
