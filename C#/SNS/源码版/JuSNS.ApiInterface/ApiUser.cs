using System;
using System.Collections.Generic;
using System.Text;
using JuSNS.Factory.ApiInterface;

namespace JuSNS.ApiInterface
{
    /// <summary>
    /// 用户基本信息访问类
    /// </summary>
    public class ApiUser
    {
        private int _UserID;
        private IApiUser dal;       
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="UserID">用户ID</param>        
        public ApiUser(int UserID)
        {
            this.UserID = UserID;
            dal = DataAccess.CreateApiUser();
        }
        /// <summary>
        /// 取得字段属性
        /// </summary>
        /// <param name="Field">字段名称</param>        
        /// <returns></returns>
        public string GetProperty(string Field)
        {
            return dal.GetProperty(this.UserID, Field);
        }
        /// <summary>
        /// 取得所有的字段属性
        /// </summary>
        /// <param name="Fields">字段名称</param>        
        /// <returns></returns>
        public Dictionary<string, string> GetAllProperty(string[] Fields)
        {
            return dal.GetAllProperty(this.UserID, Fields);
        }
        /// <summary>
        /// 是否有这个字段
        /// </summary>
        /// <param name="Field">字段名称</param>
        /// <returns></returns>
        public bool HasField(string Field)
        {
            return dal.HasField(Field);
        }
        /// <summary>
        /// 取得所有字段名称
        /// </summary>
        /// <returns></returns>
        public string[] GetFields()
        {
            return dal.GetFields();
        }
        /// <summary>
        /// 设置某个字段的值
        /// </summary>        
        /// <param name="Field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public int SetField(string Field, object value)
        {
            return dal.SetField(this.UserID, Field, value);
        }
        /// <summary>
        /// 某个字段是否可写
        /// </summary>
        /// <param name="Field">字段名称</param>
        /// <returns></returns>
        public bool CanWrite(string Field)
        {
            return dal.CanWrite(Field);
        }
        /// <summary>
        /// 是否添加某个应用
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="AppID"></param>
        /// <returns></returns>
        public bool IsAppAdded(int UserID, int AppID)
        {
            return dal.IsAppAdded(UserID, AppID);
        }
        /// <summary>
        /// 是否添加某个应用
        /// </summary>
        /// <param name="AppID"></param>
        /// <returns></returns>
        public bool IsAppAdded(int AppID)
        {
            return dal.IsAppAdded(this.UserID, AppID);
        }
        /// <summary>
        /// 取得头像
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public string GetHeadImg(int UserID)
        {
             return dal.GetHeadImg(UserID);
        }
        /// <summary>
        /// 取得头像
        /// </summary>
        /// <returns></returns>
        public string GetHeadImg()
        {
            return dal.GetHeadImg(this.UserID);
        }
        /// <summary>
        /// 用户ID是否存在
        /// </summary>
        /// <param name="UserID">用户ID</param>
        /// <returns></returns>
        public bool ExistUserID(int UserID)
        {
            return ExistUserID(UserID);
        }
        /// <summary>
        /// 存在Email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ExistEmail(string email)
        {
            return dal.ExistEmail(email);
        }
    }
}
