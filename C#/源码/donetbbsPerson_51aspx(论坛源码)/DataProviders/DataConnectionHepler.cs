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
using System.Text;
using System.Data;
using System.Web;
namespace DataProviders
{
    public abstract class DataConnectionHepler
    {
        private static DataConnectionHepler _defaultInstance = null;
        static DataConnectionHepler()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// ʵ��DataConnectionHepler�ӿ�.
        /// </summary>
        /// <returns></returns>
        public static DataConnectionHepler Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.DataConnection");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as DataConnectionHepler;
        }
        /// <summary>
        /// ����SELECT��䣬��÷������ݼ�,start Ϊ��һ�����ݣ�size Ϊ��ȡ��������tablename�������ݿ������ƣ����е�sizeΪ0ʱ����������.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public abstract DataTable DataAdapter(string sql, int start, int size, string tablename);
        /// <summary>
        /// ִ��SQL��䣬��UPDATE��INSERT��.
        /// </summary>
        /// <param name="sql"></param>
        public abstract void ExceCuteSql(string sql);

        /// <summary>
        /// ȡPageTemp.xml�ڵ��ֵ
        /// </summary>
        /// <param name="node">�ڵ�����</param>
        /// <returns>���ؽڵ�ֵ</returns>
        public abstract string GetTempXmlNode(string node);
        /// <summary>
        /// ȡResources.xml�ڵ��ֵ
        /// </summary>
        /// <param name="node">�ڵ�����</param>
        /// <returns>���ؽڵ�ֵ</returns>
        public abstract string GetResourcesXmlNode(string node);
        /// <summary>
        /// ȡWebSite.config�ڵ��ֵ
        /// </summary>
        /// <param name="node">�ڵ�����</param>
        /// <returns>���ؽڵ�ֵ</returns>
        public abstract string GetWebSiteConfig(string node);
        /// <summary>
        /// ȡTreeXml.xml�ڵ��ֵ
        /// </summary>
        /// <param name="name">���ڵ�����</param>
        /// <param name="node">�ڵ�����</param>
        /// <returns>���ؽڵ�ֵ</returns>
        public abstract string GetTreeXmlNode(string name, string node);

        public abstract DataRow GetTreeXmlRow(string node);
    }
}
