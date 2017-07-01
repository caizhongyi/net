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
using System.Collections;
namespace DataProviders
{
    public abstract class EmailDataProvider
    {
        private static EmailDataProvider _defaultInstance = null;
        static EmailDataProvider()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// ʵ��WebSiteDataProvider�ӿ�.
        /// </summary>
        /// <returns></returns>
        public static EmailDataProvider Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.EmailSqlDataProvider");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as EmailDataProvider;
        }
        public abstract void InsertEmailInfo(Components.Components.EmailWork email);
        public abstract ArrayList SetEmailWork(bool Cach);
        public abstract void UpdateEmailWork(Components.Components.EmailWork email);
    }
}
