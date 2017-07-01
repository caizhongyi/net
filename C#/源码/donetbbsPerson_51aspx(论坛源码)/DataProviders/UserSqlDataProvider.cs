//===============================================
//　　　　　　　　　　\\\|///                      
//　　　　　　　　　　\\　- -　//                   
//　　　　　　　　　　  ( @ @ )                    
//┏━━━━━━━━━oOOo-(_)-oOOo━━━┓          
//┃                                     ┃
//┃             东 网 原 创！           ┃
//┃      lenlong 作品，请保留此信息！   ┃
//┃      ** lenlenlong@hotmail.com **   ┃
//┃                                     ┃
//┃　　　　　　　　　　　　　Dooo　     ┃
//┗━━━━━━━━━ oooD━-(　 )━━━┛
//　　　　　　　　　　 (  )　  ) /
//　　　　　　　　　　　\ (　 (_/
//　　　　　　　　　　　 \_)
//===============================================
using System;
using System.Data;
using System.Text;
using System.Collections;

namespace DataProviders
{
    public class UserSqlDataProvider : UserDataProvider
    {
        private string MySql;
        /// <summary>
        /// 根据用户ID取该用户信息
        /// </summary>
        /// <param name="userID">用户ID</param>
        /// <param name="Cach">是否缓冲</param>
        /// <returns>返回该用户的信息，如果不存在，则返回Null</returns>
        public override DataRow SetUserInfo(int userID, bool Cach)
        {
            MySql = "select * from DoNetBbs_UserInfo where UserID=" + userID.ToString() + "";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-UserInfo-" + userID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserInfo");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserInfo");
            }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            else
            {
                return null;
            }
        }

        public override DataRow SetUserInfo(string userName, bool Cach)
        {
            MySql = "select * from DoNetBbs_UserInfo where UserName='" + userName.ToString() + "'";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-UserInfo-" + userName.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserInfo");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserInfo");
            }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            else
            {
                return null;
            }
        }

        public override DataRow SetUserEmailInfo(string userEmail, bool Cach)
        {
            MySql = "select * from DoNetBbs_UserInfo where UserEmail='" + userEmail.ToString() + "'";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-UserEmailInfo-" + userEmail.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserInfo");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserInfo");
            }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            else
            {
                return null;
            }
        }

        public override int SetUserListCount(string sql, bool Cach)
        {
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-UserList-count" + sql.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(sql, 0, 1, "DoNetBbs_UserInfo");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(sql, 0, 1, "DoNetBbs_UserInfo");
            }

            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }
        public override ArrayList SetUserList(string sql, int index, int count, bool Cach)
        {
            Components.Components.User Rs = new Components.Components.User();
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-UserInfo-List" + sql.ToString() + "-" + index.ToString() + "-" + count.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(sql, index, count, "DoNetBbs_UserInfo");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(sql, index, count, "DoNetBbs_UserInfo");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.User _Arraylist = new Components.Components.User();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }
        public override void UpdateUserInfo(Components.Components.User user)
        {
            MySql = "update DoNetBbs_UserInfo set ";
            MySql += " UserName='" + user.UserName + "',";
            MySql += "UserNickName='" + user.UserNickName + "',";
            MySql += "UserPassWord='" + user.UserPassWord + "',";
            MySql += "UserPassWordAnswer='" + user.UserPassWordAnswer + "',";
            MySql += "UserEmail='" + user.UserEmail + "',";
            MySql += "UserRecommendUserID='" + user.UserRecommendUserID + "',";
            MySql += "UserSex='" + user.UserSex + "',";
            MySql += "UserBirthday='" + user.UserBirthday + "',";
            MySql += "UserTrueName='" + user.UserTrueName + "',";
            MySql += "UserComeFrom='" + user.UserComeFrom + "',";
            MySql += "UserContactAddress='" + user.UserContactAddress + "',";
            MySql += "UserCode='" + user.UserCode + "',";
            MySql += "UserContactTel='" + user.UserContactTel + "',";
            MySql += "UserMobile='" + user.UserMobile + "',";
            MySql += "UserOICQ='" + user.UserOICQ + "',";
            MySql += "UserIdCard='" + user.UserIdCard + "',";
            MySql += "UserMaritalStatus='" + user.UserMaritalStatus + "',";
            MySql += "UserWorkUnit='" + user.UserWorkUnit + "',";
            MySql += "UserSchool='" + user.UserSchool + "',";
            MySql += "UserSign='" + user.UserSign + "',";
            MySql += "UserAbout='" + user.UserAbout + "',";
            MySql += "UserFace='" + user.UserFace + "',";
            MySql += "UserLastIP='" + user.UserLastIP + "',";
            MySql += "UserPrivacy='" + user.UserPrivacy + "',";
            MySql += "UserFalse='" + user.UserFalse + "',";
            MySql += "UserRegTime='" + user.UserRegTime + "',";
            MySql += "UserLastLoginTime='" + user.UserLastLoginTime + "',";
            MySql += "UserLoginNumber='" + user.UserLoginNumber + "',";
            MySql += "UserOnlineTime='" + user.UserOnlineTime + "',";
            MySql += "UserLastActTime='" + user.UserLastActTime + "',";
            MySql += "UserPoint='" + user.UserPoint + "',";
            MySql += "UserPrestige='" + user.UserPrestige + "',";
            MySql += "UserRmb='" + user.UserRmb + "',";
            MySql += "UserTicket='" + user.UserTicket + "',";
            MySql += "UserMoney='" + user.UserMoney + "',";
            MySql += "UserExp='" + user.UserExp + "',";
            MySql += "UserCP='" + user.UserCP + "',";
            MySql += "UserOnLineStatic='" + user.UserOnLineStatic + "',";
            MySql += "UserGroupID='" + user.UserGroup + "',";
            MySql += "UserLevelID='" + user.UserLevelID + "',";
            MySql += "UserRole='" + user.UserRole + "',";
            MySql += "UserPostNumber='" + user.UserPostNumber + "',";
            MySql += "UserTopicNumber='" + user.UserTopicNumber + "',";
            MySql += "UserReTopicNumber='" + user.UserReTopicNumber + "',";
            MySql += "UserDelTopicNumber='" + user.UserDelTopicNumber + "',";
            MySql += "UserWebAddress='" + user.UserWebAddress + "',";
            MySql += "UserWebLog='" + user.UserWebLog + "',";
            MySql += "UserWebGallery='" + user.UserWebGallery + "',";
            MySql += "UserInterests='" + user.UserInterests + "',";
            MySql += "UserTrueMoney='" + user.UserTrueMoney + "',";
            MySql += "UserReceiveType='" + user.UserReceiveType + "'";
            MySql += " Where UserID=" + user.UserID + "";
            DataConnectionHepler.Instance().ExceCuteSql(MySql);
        }

        public override void InsertUserInfo(Components.Components.User user)
        {
            string sql = string.Empty;
            string sqlinsert = string.Empty;
            sql = "insert into DoNetBbs_UserInfo (";
            sqlinsert += ") values (";
            sql += "UserName,";
            sqlinsert += "'" + user.UserName + "',";
            sql += "UserNickName,";
            sqlinsert += "'" + user.UserNickName + "',";
            sql += "UserPassWord,";
            sqlinsert += "'" + user.UserPassWord + "',";
            sql += "UserPassWordAnswer,";
            sqlinsert += "'" + user.UserPassWordAnswer + "',";
            sql += "UserEmail,";
            sqlinsert += "'" + user.UserEmail + "',";
            sql += "UserRecommendUserID,";
            sqlinsert += "'" + user.UserRecommendUserID + "',";
            sql += "UserSex,";
            sqlinsert += "'" + user.UserSex + "',";
            sql += "UserBirthday,";
            sqlinsert += "'" + user.UserBirthday + "',";
            sql += "UserTrueName,";
            sqlinsert += "'" + user.UserTrueName + "',";
            sql += "UserComeFrom,";
            sqlinsert += "'" + user.UserComeFrom + "',";
            sql += "UserContactAddress,";
            sqlinsert += "'" + user.UserContactAddress + "',";
            sql += "UserCode,";
            sqlinsert += "'" + user.UserCode + "',";
            sql += "UserContactTel,";
            sqlinsert += "'" + user.UserContactTel + "',";
            sql += "UserMobile,";
            sqlinsert += "'" + user.UserMobile + "',";
            sql += "UserOICQ,";
            sqlinsert += "'" + user.UserOICQ + "',";
            sql += "UserIdCard,";
            sqlinsert += "'" + user.UserIdCard + "',";
            sql += "UserMaritalStatus,";
            sqlinsert += "'" + user.UserMaritalStatus + "',";
            sql += "UserWorkUnit,";
            sqlinsert += "'" + user.UserWorkUnit + "',";
            sql += "UserSchool,";
            sqlinsert += "'" + user.UserSchool + "',";
            sql += "UserSign,";
            sqlinsert += "'" + user.UserSign + "',";
            sql += "UserAbout,";
            sqlinsert += "'" + user.UserAbout + "',";
            sql += "UserFace,";
            sqlinsert += "'" + user.UserFace + "',";
            sql += "UserLastIP,";
            sqlinsert += "'" + user.UserLastIP + "',";
            sql += "UserPrivacy,";
            sqlinsert += "'" + user.UserPrivacy + "',";
            sql += "UserFalse,";
            sqlinsert += "'" + user.UserFalse + "',";
            sql += "UserRegTime,";
            sqlinsert += "'" + user.UserRegTime + "',";
            sql += "UserLastLoginTime,";
            sqlinsert += "'" + user.UserLastLoginTime + "',";
            sql += "UserLoginNumber,";
            sqlinsert += "'" + user.UserLoginNumber + "',";
            sql += "UserOnlineTime,";
            sqlinsert += "'" + user.UserOnlineTime + "',";
            sql += "UserLastActTime,";
            sqlinsert += "'" + user.UserLastActTime + "',";
            sql += "UserPoint,";
            sqlinsert += "'" + user.UserPoint + "',";
            sql += "UserPrestige,";
            sqlinsert += "'" + user.UserPrestige + "',";
            sql += "UserRmb,";
            sqlinsert += "'" + user.UserRmb + "',";
            sql += "UserTicket,";
            sqlinsert += "'" + user.UserTicket + "',";
            sql += "UserMoney,";
            sqlinsert += "'" + user.UserMoney + "',";
            sql += "UserExp,";
            sqlinsert += "'" + user.UserExp + "',";
            sql += "UserCP,";
            sqlinsert += "'" + user.UserCP + "',";
            sql += "UserOnLineStatic,";
            sqlinsert += "'" + user.UserOnLineStatic + "',";
            sql += "UserGroupID,";
            sqlinsert += "'" + user.UserGroup + "',";
            sql += "UserLevelID,";
            sqlinsert += "'" + user.UserLevelID + "',";
            sql += "UserRole,";
            sqlinsert += "'" + user.UserRole + "',";
            sql += "UserPostNumber,";
            sqlinsert += "'" + user.UserPostNumber + "',";
            sql += "UserTopicNumber,";
            sqlinsert += "'" + user.UserTopicNumber + "',";
            sql += "UserReTopicNumber,";
            sqlinsert += "'" + user.UserReTopicNumber + "',";
            sql += "UserDelTopicNumber,";
            sqlinsert += "'" + user.UserDelTopicNumber + "',";
            sql += "UserWebAddress,";
            sqlinsert += "'" + user.UserWebAddress + "',";
            sql += "UserWebLog,";
            sqlinsert += "'" + user.UserWebLog + "',";
            sql += "UserWebGallery,";
            sqlinsert += "'" + user.UserWebGallery + "',";
            sql += "UserInterests,";
            sqlinsert += "'" + user.UserInterests + "',";
            sql += "UserTrueMoney,";
            sqlinsert += "'" + user.UserTrueMoney + "',";
            sql += "UserReceiveType";
            sqlinsert += "'" + user.UserReceiveType + "'";
            sql += sqlinsert + ")";

            //HttpContext.Current.Response.Write(sql);
            //return;
            DataConnectionHepler.Instance().ExceCuteSql(sql);
        }



        public override DataRow SetUserLevel(int levelID, bool Cach)
        {
            MySql = "select * from DoNetBbs_UserLevel where UserLevelID=" + levelID.ToString() + "";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-UserLevel-" + levelID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserLevel");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserLevel");
            }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            else
            {
                return null;
            }
        }

        public override DataRow SetLastUserLevel(int userPoint, bool Cach)
        {
            MySql = "select * FROM DoNetBbs_UserLevel  where UserLevelPoint<=" + userPoint + " ORDER BY UserLevelPoint DESC";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-UserLastLevel-" + userPoint.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserLevel");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserLevel");
            }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            else
            {
                return null;
            }
        }

        public override ArrayList SetUserLevel(bool Cach)
        {
            Components.Components.UserLevel Rs = new Components.Components.UserLevel();
            DataTable dt;
            MySql = "select * from DoNetBbs_UserLevel";
            if (Cach)
            {
                string key = "WebSite-UserLeve-List";
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_UserLevel");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_UserLevel");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.UserLevel _Arraylist = new Components.Components.UserLevel();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }

        public override void DeleteUserLevel(int userLevelID)
        {
            DataConnectionHepler.Instance().ExceCuteSql("delete from DoNetBbs_UserLevel where UserLevelID = " + userLevelID + "");
        }

        public override void InsertUserLevel(Components.Components.UserLevel userLevel)
        {
            string sql = string.Empty;
            string sqlinsert = string.Empty;
            sql = "insert into DoNetBbs_UserLevel (";
            sqlinsert += ") values (";
            sql += "UserLevelImages,";
            sqlinsert += "'" + userLevel.UserLevelImages + "',";
            sql += "UserLevelPoint,";
            sqlinsert += "'" + userLevel.UserLevelPoint + "',";
            sql += "UserLevelTitle";
            sqlinsert += "'" + userLevel.UserLevelTitle + "'";
            sql += sqlinsert + ")";

            //DoNetBbs.DoNetBbsClassHepler.Instance().GetHttpContext().Response.Write(sql);
            //HttpContext.Current.Response.Write("alert('"+sql+"')");
            //return;
            DataConnectionHepler.Instance().ExceCuteSql(sql);
        }

        public override void UpdateUserLevel(Components.Components.UserLevel userLevel)
        {
            MySql = "update DoNetBbs_UserLevel set UserLevelImages='" + userLevel.UserLevelImages + "'";
            MySql += ",UserLevelPoint='" + userLevel.UserLevelPoint + "'";
            MySql += ",UserLevelTitle='" + userLevel.UserLevelTitle + "'";
            MySql += " where UserLevelID='" + userLevel.UserLevelID + "'";
            DataProviders.DataConnectionHepler.Instance().ExceCuteSql(MySql);
        }

        public override DataRow SetUserRole(int roleID, bool Cach)
        {
            MySql = "select * from DoNetBbs_UserRole where UserRoleID=" + roleID.ToString() + "";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-UserRole-" + roleID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "UserRole");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "UserRole");
            }
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            else
            {
                return null;
            }
        }
        public override ArrayList SetUserRole(string roleID, bool Cach)
        {
            Components.Components.UserRole Rs = new Components.Components.UserRole();
            DataTable dt;
            MySql = "select * from DoNetBbs_UserRole where UserRoleID in (" + roleID + ")";
            if (Cach)
            {
                string key = "WebSite-UserRole-List" + roleID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_UserRole");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_UserRole");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.UserRole _Arraylist = new Components.Components.UserRole();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }

        public override ArrayList SetUserRole(bool Cach)
        {
            Components.Components.UserRole Rs = new Components.Components.UserRole();
            DataTable dt;
            MySql = "select * from DoNetBbs_UserRole";
            if (Cach)
            {
                string key = "WebSite-UserRole-List";
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_UserRole");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_UserRole");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.UserRole _Arraylist = new Components.Components.UserRole();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }




        public override void DeleteUserRole(int roleID)
        {
            DataConnectionHepler.Instance().ExceCuteSql("delete from DoNetBbs_UserRole where UserRoleID = " + roleID + "");
        }

        public override void InsertUserRole(Components.Components.UserRole userRole)
        {
            string sql = string.Empty;
            string sqlinsert = string.Empty;
            sql = "insert into DoNetBbs_UserRole (";
            sqlinsert += ") values (";
            sql += "UserRoleFalse,";
            sqlinsert += "'" + userRole.UserRoleFalse + "',";
            sql += "UserRolePostCP,";
            sqlinsert += "'" + userRole.UserRolePostCP + "',";
            sql += "UserRolePostExp,";
            sqlinsert += "'" + userRole.UserRolePostExp + "',";
            sql += "UserRolePostMoney,";
            sqlinsert += "'" + userRole.UserRolePostMoney + "',";
            sql += "UserRolePostPoint,";
            sqlinsert += "'" + userRole.UserRolePostPoint + "',";
            sql += "UserRoleRePostCP,";
            sqlinsert += "'" + userRole.UserRoleRePostCP + "',";
            sql += "UserRoleRePostExp,";
            sqlinsert += "'" + userRole.UserRoleRePostExp + "',";
            sql += "UserRoleRePostMoney,";
            sqlinsert += "'" + userRole.UserRoleRePostMoney + "',";
            sql += "UserRoleRePostPoint,";
            sqlinsert += "'" + userRole.UserRoleRePostPoint + "',";
            sql += "UserRoles,";
            sqlinsert += "'" + userRole.UserRoles + "',";
            sql += "UserRoleTitle";
            sqlinsert += "'" + userRole.UserRoleTitle + "'";
            sql += sqlinsert + ")";

            //DoNetBbs.DoNetBbsClassHepler.Instance().GetHttpContext().Response.Write(sql);
            //HttpContext.Current.Response.Write("alert('"+sql+"')");
            //return;
            DataConnectionHepler.Instance().ExceCuteSql(sql);
        }

        public override void UpadateUserRole(Components.Components.UserRole userRole)
        {
            MySql = "update DoNetBbs_UserRole set UserRoleFalse='" + userRole.UserRoleFalse + "'";
            MySql += ",UserRolePostCP='" + userRole.UserRolePostCP + "'";
            MySql += ",UserRolePostExp='" + userRole.UserRolePostExp + "'";
            MySql += ",UserRolePostMoney='" + userRole.UserRolePostMoney + "'";
            MySql += ",UserRolePostPoint='" + userRole.UserRolePostPoint + "'";
            MySql += ",UserRoleRePostCP='" + userRole.UserRoleRePostCP + "'";
            MySql += ",UserRoleRePostExp='" + userRole.UserRoleRePostExp + "'";
            MySql += ",UserRoleRePostMoney='" + userRole.UserRoleRePostMoney + "'";
            MySql += ",UserRoleRePostPoint='" + userRole.UserRoleRePostPoint + "'";
            MySql += ",UserRoles='" + userRole.UserRoles + "'";
            MySql += ",UserRoleTitle='" + userRole.UserRoleTitle + "'";
            MySql += " where UserRoleID=" + userRole.UserRoleID + "";
            DataProviders.DataConnectionHepler.Instance().ExceCuteSql(MySql);
        }






        public override ArrayList SetUserGroup(string groupID, bool Cach)
        {
            Components.Components.UserGroup Rs = new Components.Components.UserGroup();
            DataTable dt;
            MySql = "select * from DoNetBbs_UserGroup where UserGroupID in (" + groupID + ")";
            if (Cach)
            {
                string key = "WebSite-UserGroup-List" + groupID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_UserGroup");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_UserGroup");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.UserGroup _Arraylist = new Components.Components.UserGroup();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }


        public override ArrayList SetUserGroup(bool Cach)
        {
            Components.Components.UserGroup Rs = new Components.Components.UserGroup();
            DataTable dt;
            MySql = "select * from DoNetBbs_UserGroup";
            if (Cach)
            {
                string key = "WebSite-UserGroup-List";
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_UserGroup");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_UserGroup");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.UserGroup _Arraylist = new Components.Components.UserGroup();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }

    }
}
