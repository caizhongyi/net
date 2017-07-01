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
    public abstract class PlayDataProvider
    {
        private static PlayDataProvider _defaultInstance = null;
        static PlayDataProvider()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// ʵ��PlayDataProvider�ӿ�.
        /// </summary>
        /// <returns></returns>
        public static PlayDataProvider Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.PlaySqlDataProvider");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as PlayDataProvider;
        }
        public abstract void InsertPlayMoney(Components.Components.PlayMoney paly);
    }
}
