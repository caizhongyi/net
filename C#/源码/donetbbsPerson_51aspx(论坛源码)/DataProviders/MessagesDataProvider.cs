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
    public abstract class MessagesDataProvider
    {
        private static MessagesDataProvider _defaultInstance = null;
        static MessagesDataProvider()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// ʵ��MessagesDataProvider�ӿ�.
        /// </summary>
        /// <returns></returns>
        public static MessagesDataProvider Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.MessagesSqlDataProvider");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as MessagesDataProvider;
        }
        public abstract void InsertMessages(Components.Components.Messages messages);
        public abstract ArrayList SetMessagesList(int userID, string userIP, bool Cach);
        public abstract void UpdateMessages(Components.Components.Messages messages);
    }
}
