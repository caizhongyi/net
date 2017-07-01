//===============================================
//��������������������\\\|///                      
//��������������������\\��- -��//                   
//��������������������  ( @ @ )                    
//��������������������oOOo-(_)-oOOo��������          
//��                                     ��
//��             �� �� ԭ ����           ��
//��      lenlong ��Ʒ���뱣������Ϣ��   ��
//��      ** lenlenlong@hotmail.com **   ��
//��                                     ��
//����������������������������Dooo��     ��
//�������������������� oooD��-(�� )��������
//�������������������� (  )��  ) /
//����������������������\ (�� (_/
//���������������������� \_)
//===============================================
using System;
using System.Data;
using System.Text;

namespace DataProviders
{
    public abstract class WebSiteDataProvider
    {
        private static WebSiteDataProvider _defaultInstance = null;
        static WebSiteDataProvider()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// ʵ��WebSiteDataProvider�ӿ�.
        /// </summary>
        /// <returns></returns>
        public static WebSiteDataProvider Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.WebSiteSqlDataProvider");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as WebSiteDataProvider;
        }
        /// <summary>
        /// ����վ��IDȡ��վ����Ϣ
        /// </summary>
        /// <param name="WebSiteID">վ��ID</param>
        /// <param name="Cach">�Ƿ񻺳�</param>
        /// <returns>���ظ�վ�����Ϣ����������ڣ��򷵻�Null</returns>
        public abstract DataRow SetWebSite(int webSiteID, bool Cach);
        public abstract void UpdateWebSite(Components.Components.WebSite webSite);
    }
}
