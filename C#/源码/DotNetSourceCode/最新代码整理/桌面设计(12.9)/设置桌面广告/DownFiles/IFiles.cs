using System;
using System.Collections.Generic;
using System.Text;

namespace Files
{
    public interface IFiles
    {
        #region DownloadFiles ���ط������ļ����ͻ���
        /// <summary>
        /// ���ط������ļ����ͻ���
        /// </summary>
        /// <param name="URL">�����ص��ļ���ַ������·�� ��ʽ�磺"http://www.wb66.cn/liucheng.doc"</param>
        /// <param name="Dir">���ŵ�Ŀ¼ ��ʽ�磺@"D:\"</param>
        void DownloadFiles(string URL, string Dir);
        #endregion
    }
}
