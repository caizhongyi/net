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
    public abstract class UserDataProvider
    {
        private static UserDataProvider _defaultInstance = null;
        static UserDataProvider()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// ʵ��UserDataProvider�ӿ�.
        /// </summary>
        /// <returns></returns>
        public static UserDataProvider Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.UserSqlDataProvider");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as UserDataProvider;
        }
        /// <summary>
        /// �����û�IDȡ���û���Ϣ
        /// </summary>
        /// <param name="userID">�û�ID</param>
        /// <param name="Cach">�Ƿ񻺳�</param>
        /// <returns>���ظ��û�����Ϣ����������ڣ��򷵻�Null</returns>
        public abstract DataRow SetUserInfo(int userID, bool Cach);
        public abstract DataRow SetUserInfo(string userName, bool Cach);
        public abstract DataRow SetUserEmailInfo(string userEmail, bool Cach);
        public abstract void UpdateUserInfo(Components.Components.User user);
        public abstract void InsertUserInfo(Components.Components.User user);
        public abstract int SetUserListCount(string sql, bool Cach);
        public abstract ArrayList SetUserList(string sql, int index, int count, bool Cach);

        public abstract DataRow SetUserLevel(int levelID, bool Cach);
        public abstract ArrayList SetUserLevel(bool Cach);
        public abstract DataRow SetLastUserLevel(int userPoint, bool Cach);
        public abstract void DeleteUserLevel(int userLevelID);
        public abstract void UpdateUserLevel(Components.Components.UserLevel userLevel);
        public abstract void InsertUserLevel(Components.Components.UserLevel userLevel);


        public abstract ArrayList SetUserRole(string roleID, bool Cach);
        public abstract DataRow SetUserRole(int roleID, bool Cach);
        public abstract ArrayList SetUserRole(bool Cach);
        public abstract void DeleteUserRole(int roleID);
        public abstract void InsertUserRole(Components.Components.UserRole userRole);
        public abstract void UpadateUserRole(Components.Components.UserRole userRole);

        public abstract ArrayList SetUserGroup(string groupID, bool Cach);
        public abstract ArrayList SetUserGroup(bool Cach);


    }
}
