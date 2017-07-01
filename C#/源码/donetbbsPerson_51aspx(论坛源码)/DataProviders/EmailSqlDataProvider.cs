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
    public class EmailSqlDataProvider : EmailDataProvider
    {
        private string MySql;
        public override void InsertEmailInfo(Components.Components.EmailWork email)
        {
            string sql = string.Empty;
            string sqlinsert = string.Empty;
            sql = "insert into DoNetBbs_EmailWork (";
            sqlinsert += ") values (";
            sql += "EmailWorkType,";
            sqlinsert += "'" + email.EmailWorkType + "',";
            sql += "EmailWorkFromUserID,";
            sqlinsert += "'" + email.EmailWorkFromUserID + "',";
            sql += "EmailWorkFromName,";
            sqlinsert += "'" + email.EmailWorkFromName + "',";
            sql += "EmailWorkFromEmail,";
            sqlinsert += "'" + email.EmailWorkFromEmail + "',";
            sql += "EmailWorkReceiveName,";
            sqlinsert += "'" + email.EmailWorkReceiveName + "',";
            sql += "EmailWorkReceiveUserID,";
            sqlinsert += "'" + email.EmailWorkReceiveUserID + "',";
            sql += "EmailWorkReceiveEmail,";
            sqlinsert += "'" + email.EmailWorkReceiveEmail + "',";
            sql += "EmailWorkPostTime,";
            sqlinsert += "'" + email.EmailWorkPostTime + "',";
            sql += "EmailWorkCreateTime,";
            sqlinsert += "'" + email.EmailWorkCreateTime + "',";
            sql += "EmailWorkStatus,";
            sqlinsert += "'" + email.EmailWorkStatus + "',";
            sql += "EmailWorkContent,";
            sqlinsert += "'" + email.EmailWorkContent + "',";
            sql += "EmailWorkTitle";
            sqlinsert += "'" + email.EmailWorkTitle + "'";
            sql += sqlinsert + ")";
            DataConnectionHepler.Instance().ExceCuteSql(sql);
        }

        public override ArrayList SetEmailWork(bool Cach)
        {
            MySql = "select * from DoNetBbs_EmailWork where EmailWorkStatus=0";

            Components.Components.EmailWork Rs = new Components.Components.EmailWork();
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-EmailWork-List";
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_EmailWork");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_EmailWork");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.EmailWork _Arraylist = new Components.Components.EmailWork();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }

        public override void UpdateEmailWork(Components.Components.EmailWork email)
        {
            MySql = "update DoNetBbs_EmailWork set ";
            MySql += "EmailWorkContent='" + email.EmailWorkContent + "',";
            MySql += "EmailWorkCreateTime ='" + email.EmailWorkCreateTime + "',";
            MySql += "EmailWorkFromEmail ='" + email.EmailWorkFromEmail + "',";
            MySql += "EmailWorkFromName ='" + email.EmailWorkFromName + "',";
            MySql += "EmailWorkFromUserID ='" + email.EmailWorkFromUserID + "',";
            MySql += "EmailWorkPostTime ='" + email.EmailWorkPostTime + "',";
            MySql += "EmailWorkReceiveEmail ='" + email.EmailWorkReceiveEmail + "',";
            MySql += "EmailWorkReceiveName ='" + email.EmailWorkReceiveName + "',";
            MySql += "EmailWorkReceiveUserID ='" + email.EmailWorkReceiveUserID + "',";
            MySql += "EmailWorkStatus ='" + email.EmailWorkStatus + "',";
            MySql += "EmailWorkTitle ='" + email.EmailWorkTitle + "',";
            MySql += "EmailWorkType ='" + email.EmailWorkType + "'";
            MySql += " where EmailWorkID=" + email.EmailWorkID + "";
            DataConnectionHepler.Instance().ExceCuteSql(MySql);
        }
    }
}
