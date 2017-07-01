using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using JuSNS.Config;
using JuSNS.Profile;
using JuSNS.Model;
using JuSNS.Common;

namespace JuSNS.SQLServer
{
    public class DbBase : IDbBase
    {
        /// <summary>
        /// 建立SqlCommand对象
        /// </summary>
        /// <returns></returns>
        DbCommand IDbBase.CreateCommand()
        {
            return new SqlCommand();
        }
        /// <summary>
        /// 建立SqlConnection对象
        /// </summary>
        /// <returns></returns>
        DbConnection IDbBase.CreateConnection()
        {
            return new SqlConnection();
        }
        /// <summary>
        /// 创建SqlDataAdapter对象
        /// </summary>
        /// <returns></returns>
        DbDataAdapter IDbBase.CreateDataAdapter()
        {
            return new SqlDataAdapter();
        }
        /// <summary>
        /// 建立SqlParameter对象
        /// </summary>
        /// <returns></returns>
        DbParameter IDbBase.CreateParameter()
        {
            return new SqlParameter();
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public DbBase()
        {
            DbHelper.Provider = this;
        }

        /// <summary>
        /// 插入更新动态
        /// </summary>
        /// <param name="trans"></param>
        /// <param name="ID">增加0，更新大于0为被操作的ID</param>
        /// <param name="appID">APIID</param>
        /// <param name="dynType">动态类型</param>
        /// <param name="UserID">操作人ID</param>
        /// <param name="bUserID">被操作人ID</param>
        /// <param name="cID">被操作的ID</param>
        /// <param name="Descript">描述</param>
        /// <param name="correspondid">扩展类型</param>
        /// <param name="Num">0增加，1更新</param>
        public int UserDyn(SqlTransaction trans, int ID, int appID, string dynType, int UserID, int bUserID, int cID, string Descript, string infoarr)
        {
            //得到隐私设置。
            SqlParameter[] param = new SqlParameter[9];
            param[0] = new SqlParameter("@appID", SqlDbType.Int, 4);
            param[0].Value = appID;
            param[1] = new SqlParameter("@dynType", SqlDbType.NVarChar, 20);
            param[1].Value = dynType;
            param[2] = new SqlParameter("@cID", SqlDbType.Int, 4);
            param[2].Value = cID;
            param[3] = new SqlParameter("@UserID", SqlDbType.Int, 4);
            param[3].Value = UserID;
            param[4] = new SqlParameter("@bUserID", SqlDbType.Int, 4);
            param[4].Value = bUserID;
            param[5] = new SqlParameter("@Descript", SqlDbType.NVarChar, 30);
            param[5].Value = Descript;
            param[6] = new SqlParameter("@infoarr", SqlDbType.NVarChar, 30);
            param[6].Value = infoarr;
            param[7] = new SqlParameter("@IP", SqlDbType.NVarChar, 15);
            param[7].Value = JuSNS.Common.Public.GetClientIP();
            param[8] = new SqlParameter("@ID", SqlDbType.Int, 4);
            param[8].Value = ID;
            if (trans == null)
            {
                return DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "JuSNS_UserDyn", param);
            }
            else
            {
                return DbHelper.ExecuteNonQuery(trans, CommandType.StoredProcedure, "JuSNS_UserDyn", param);
            }
        }


        /// <summary>
        /// 检查用户是否具有某种权限
        /// </summary>
        /// <param name="cn">已打开的数据库连接对象</param>
        /// <param name="privacy">权限类型</param>
        /// <param name="guest">等待验证的是否有权限的用户ID</param>
        /// <param name="host">主体者的ID</param>
        /// <returns>guest有权限返回true,否则返回false</returns>
        public bool CheckPrivacy(SqlConnection cn, EnumPrivacy privacy, int guest, int host)
        {
            if (guest == host)
                return true;
            switch (privacy)
            {
                case EnumPrivacy.ForWholeSite:
                    return true;
                //case EnumPrivacy.ForNetWorkAndFriends:
                //    if (User.User.IsFriends(cn,host, guest))
                //        return true;
                //    else
                //        return false;
                case EnumPrivacy.ForFriends:
                    if (User.User.IsInSameNetwork(cn, host, guest))
                        return true;
                    else
                    {
                        if (User.User.IsFriends(cn,host, guest))
                            return true;
                        else
                            return false;
                    }
                case EnumPrivacy.ForOwner:
                    if (guest == host)
                        return true;
                    else
                        return false;
                default:
                    return false;
            }
        }
    }
}
