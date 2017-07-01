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
    public abstract class UserOnLineDataProvider
    {
        private static UserOnLineDataProvider _defaultInstance = null;
        static UserOnLineDataProvider()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// ʵ��PlayDataProvider�ӿ�.
        /// </summary>
        /// <returns></returns>
        public static UserOnLineDataProvider Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.UserOnLineSqlDataProvider");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as UserOnLineDataProvider;
        }
        public abstract void InsertUserOnLine(Components.Components.UserOnLine online);
        public abstract void DeleteUserOnLine(int userOnLineID);

        public abstract ArrayList SetRefreshUserOnlineList(int count, bool Cach);

        public abstract int SetUserOnLineCount(string sql, bool Cach);
        public abstract ArrayList SetUserOnLineList(string sql, int index, int count, bool Cach);

        public abstract DataRow SetUserOnLineDistinct(string sql, int index, int count, bool Cach);
        public abstract DataRow SetUserOnLine(int userOnLineID, bool Cach);
        public abstract DataRow SetLastUserOnLine(int userID, bool Cach);
    }
}
