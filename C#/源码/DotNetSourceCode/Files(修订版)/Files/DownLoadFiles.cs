using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;

namespace Files
{
    class DownLoadFiles : IFiles
    {
        #region DownloadFiles ���ط������ļ����ͻ���
        /// <summary>
        /// ���ط������ļ����ͻ���
        /// </summary>
        /// <param name="URL">�����ص��ļ���ַ������·�� ��ʽ�磺"http://www.wb66.cn/liucheng.doc"</param>
        /// <param name="Dir">���ŵ�Ŀ¼ ��ʽ�磺@"D:\"</param>
        public void DownloadFiles(string URL, string Dir)
        {
            WebClient client = new WebClient();
            string fileName = URL.Substring(URL.LastIndexOf("/") + 1);  //�����ص��ļ���

            string Path = Dir + fileName;   //���Ϊ�ľ���·�����ļ���

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
                client.DownloadFile(URL, fileName);
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader r = new BinaryReader(fs);
                byte[] mbyte = r.ReadBytes((int)fs.Length);

                FileStream fstr = new FileStream(Path, FileMode.OpenOrCreate,FileAccess.Write);

                fstr.Write(mbyte, 0, (int)fs.Length);
                fstr.Close();

            }
            catch
            {
                //MessageBox.Show(exp.Message,"Error");
            }
        }
        #endregion
    }
}
