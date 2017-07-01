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
using System.Web;
namespace DataProviders
{
    public class UserOnLineSqlDataProvider : UserOnLineDataProvider
    {
        private string MySql;
        public override void InsertUserOnLine(Components.Components.UserOnLine online)
        {
            string sql = string.Empty;
            string sqlinsert = string.Empty;
            sql = "insert into DoNetBbs_UserOnLine (";
            sqlinsert += ") values (";
            sql += "UserOnLineUserID,";
            sqlinsert += "'" + online.UserOnLineUserID + "',";
            sql += "UserOnLineLastTime,";
            sqlinsert += "'" + online.UserOnLineLastTime + "',";
            sql += "UserOnLineUserNickName,";
            sqlinsert += "'" + online.UserOnLineUserNickName + "',";
            sql += "UserOnLineComeFromPath,";
            sqlinsert += "'" + online.UserOnLineComeFromPath + "',";
            sql += "UserOnLineBrowserPath,";
            sqlinsert += "'" + online.UserOnLineBrowserPath + "',";
            sql += "UserOnLineBrowserTitle,";
            sqlinsert += "'" + online.UserOnLineBrowserTitle + "',";
            sql += "UserOnLineSystem,";
            sqlinsert += "'" + online.UserOnLineSystem + "',";
            sql += "UserOnLineIP";
            sqlinsert += "'" + online.UserOnLineIP + "'";
            sql += sqlinsert + ")";

            //HttpContext.Current.Response.Write("window.open('" + sql.Replace("\r\n", "").Trim() + "');");
            //return;
            DataConnectionHepler.Instance().ExceCuteSql(sql);
        }
        public override void DeleteUserOnLine(int userOnLineID)
        {
            DataConnectionHepler.Instance().ExceCuteSql("delete from DoNetBbs_UserOnLine where UserOnLineID = " + userOnLineID + "");
        }

        public override ArrayList SetRefreshUserOnlineList(int count, bool Cach)
        {
            Components.Components.UserOnLine Rs = new Components.Components.UserOnLine();
            DataTable dt;
            MySql = "select top " + count + " * from DoNetBbs_UserOnLine order by UserOnLineLastTime desc";
            if (Cach)
            {
                string key = "WebSite-RefreshUserOnline-List" + count.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, count, "DoNetBbs_UserOnLine");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, count, "DoNetBbs_UserOnLine");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.UserOnLine _Arraylist = new Components.Components.UserOnLine();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }
        public override int SetUserOnLineCount(string sql, bool Cach)
        {
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-UserOnLine-count" + sql.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(sql, 0, 1, "DoNetBbs_UserOnLine");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(sql, 0, 1, "DoNetBbs_UserOnLine");
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

        public override ArrayList SetUserOnLineList(string sql, int index, int count, bool Cach)
        {
            Components.Components.UserOnLine Rs = new Components.Components.UserOnLine();
            DataTable dt;
           // MySql = "select top " + count + " * from DoNetBbs_UserOnLine order by UserOnLineLastTime desc";
            if (Cach)
            {
                string key = "WebSite-UserOnLine-List" + sql.ToString() + "-" + index.ToString() + "-" + count.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(sql, index, count, "DoNetBbs_UserOnLine");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(sql, index, count, "DoNetBbs_UserOnLine");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.UserOnLine _Arraylist = new Components.Components.UserOnLine();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }
        public override DataRow SetUserOnLineDistinct(string sql, int index, int count, bool Cach)
        {
            Components.Components.UserOnLine Rs = new Components.Components.UserOnLine();
            DataTable dt;
            // MySql = "select top " + count + " * from DoNetBbs_UserOnLine order by UserOnLineLastTime desc";
            if (Cach)
            {
                string key = "WebSite-UserOnLineDistinct-List" + sql.ToString() + "-" + index.ToString() + "-" + count.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(sql, index, count, "DoNetBbs_UserOnLine");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(sql, index, count, "DoNetBbs_UserOnLine");
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
        public override DataRow SetUserOnLine(int userOnLineID, bool Cach)
        {
            DataTable dt;
            MySql = "select * from DoNetBbs_UserOnLine where UserOnLineID=" + userOnLineID + "";
            if (Cach)
            {
                string key = "WebSite-UserOnLine-ID" + userOnLineID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserOnLine");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserOnLine");
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
        public override DataRow SetLastUserOnLine(int userID, bool Cach)
        {
            DataTable dt;
            MySql = "select top 1 * from DoNetBbs_UserOnLine where UserOnlineUserID=" + userID + " order by UserOnLineLastTime desc";
            if (Cach)
            {
                string key = "WebSite-LastUserOnLine-UserID" + userID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserOnLine");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_UserOnLine");
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
    }
}