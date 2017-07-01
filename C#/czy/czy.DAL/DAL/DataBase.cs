using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace czy.MyDAL
{
    public abstract class DataBase
    {
        public delegate void DataReadEvent(object DataReader);
        
        #region 私有成员
        protected string _uid = string.Empty;
        protected string _pwd = string.Empty;
        protected string _dbName = string.Empty;
        protected string _connString = string.Empty;
        protected ConnStringType _connType = ConnStringType.ConfigKey;
        #endregion

        #region 枚举
        /// <summary>
        /// 链接字符类型,默认为ConfigKey
        /// </summary>
        public enum ConnStringType
        {
            /// <summary>
            /// ConfigKey值
            /// </summary>
            ConfigKey,
            /// <summary>
            /// 字符窜类型
            /// </summary>
            String
        }
        #endregion

        #region 属性
        /// <summary>
        /// 数据库密码
        /// </summary>
        protected string Pwd
        {
            get { return _pwd; }
            set { _pwd = value; }
        }
        /// <summary>
        /// 数据库名称
        /// </summary>
        protected string DbName
        {
            get { return _dbName; }
            set { _dbName = value; }
        }
        /// <summary>
        /// 数据库用户名
        /// </summary>
        protected string Uid
        {
            get { return _uid; }
            set { _uid = value; }
        }
        /// <summary>
        /// 链接字符或ConfigKey
        /// </summary>
        public string ConnString
        {
            get { return _connString; }
            set { _connString = value; }
        }
        /// <summary>
        /// 链接字符类型,默认为ConfigKey
        /// </summary>
        public ConnStringType ConnType
        {
            get { return _connType; }
            set { _connType = value; }
        }
        #endregion
    }
}
