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
    public class MessagesSqlDataProvider : MessagesDataProvider
    {
        private string MySql;
        public override void InsertMessages(Components.Components.Messages messages)
        {
            string sql = string.Empty;
            string sqlinsert = string.Empty;
            sql = "insert into DoNetBbs_Messages (";
            sqlinsert += ") values (";
            sql += "MessagesFromUserID,";
            sqlinsert += "'" + messages.MessagesFromUserID + "',";
            sql += "MessagesFromUserName,";
            sqlinsert += "'" + messages.MessagesFromUserName + "',";
            sql += "MessagesFromUserIP,";
            sqlinsert += "'" + messages.MessagesFromUserIP + "',";
            sql += "MessagesReceiveUserID,";
            sqlinsert += "'" + messages.MessagesReceiveUserID + "',";
            sql += "MessagesReceiveUserName,";
            sqlinsert += "'" + messages.MessagesReceiveUserName + "',";
            sql += "MessagesCreateTime,";
            sqlinsert += "'" + messages.MessagesCreateTime + "',";
            sql += "MessagesFromDelete,";
            sqlinsert += "'" + messages.MessagesFromDelete + "',";
            sql += "MessagesReceiveDelete,";
            sqlinsert += "'" + messages.MessagesReceiveDelete + "',";
            sql += "MessagesReadFalse,";
            sqlinsert += "'" + messages.MessagesReadFalse + "',";
            sql += "MessagesNoticeFalse,";
            sqlinsert += "'" + messages.MessagesNoticeFalse + "',";
            sql += "MessagesTitle,";
            sqlinsert += "'" + messages.MessagesTitle + "',";
            sql += "MessagesContent,";
            sqlinsert += "'" + messages.MessagesContent + "',";
            sql += "MessagesSystemFalse,";
            sqlinsert += "'" + messages.MessagesSystemFalse + "',";
            sql += "MessagesReceiveUserIP";
            sqlinsert += "'" + messages.MessagesReceiveUserIP + "'";
            sql += sqlinsert + ")";

            //HttpContext.Current.Response.Write("alert('"+sql+"')");
            //return;
            DataConnectionHepler.Instance().ExceCuteSql(sql);
        }

        public override ArrayList SetMessagesList(int userID, string userIP, bool Cach)
        {
            MySql = "select * from DoNetBbs_Messages";
            if (userID == 0)
            {
                MySql += " where MessagesReceiveUserIP='" + userIP + "'";
            }
            else
            {
                MySql += " where MessagesReceiveUserID='" + userID + "'";
            }
            MySql += " and MessagesReceiveDelete='0' and MessagesNoticeFalse='0'";
            MySql += " order by MessagesCreateTime asc";

            Components.Components.Messages Rs = new Components.Components.Messages();
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-Messages-List" + userID.ToString() + "-" + userIP;
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, int.Parse(DataProviders.DataConnectionHepler.Instance().GetWebSiteConfig("WebSite_RefreshMessageNumber")), "DoNetBbs_Messages");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, int.Parse(DataProviders.DataConnectionHepler.Instance().GetWebSiteConfig("WebSite_RefreshMessageNumber")), "DoNetBbs_Messages");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.Messages _Arraylist = new Components.Components.Messages();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }

        public override void UpdateMessages(Components.Components.Messages messages)
        {
            MySql = "update DoNetBbs_Messages set MessagesFromUserID='" + messages.MessagesFromUserID + "'";
            MySql += ",MessagesFromUserName='" + messages.MessagesFromUserName + "'";
            MySql += ",MessagesFromUserIP='" + messages.MessagesFromUserIP + "'";
            MySql += ",MessagesReceiveUserID='" + messages.MessagesReceiveUserID + "'";
            MySql += ",MessagesReceiveUserName='" + messages.MessagesReceiveUserName + "'";
            MySql += ",MessagesReceiveUserIP='" + messages.MessagesReceiveUserIP + "'";
            MySql += ",MessagesCreateTime='" + messages.MessagesCreateTime + "'";
            MySql += ",MessagesFromDelete='" + messages.MessagesFromDelete + "'";
            MySql += ",MessagesReceiveDelete='" + messages.MessagesReceiveDelete + "'";
            MySql += ",MessagesReadFalse='" + messages.MessagesReadFalse + "'";
            MySql += ",MessagesNoticeFalse='" + messages.MessagesNoticeFalse + "'";
            MySql += ",MessagesTitle='" + messages.MessagesTitle + "'";
            MySql += ",MessagesContent='" + messages.MessagesContent + "'";
            MySql += ",MessagesSystemFalse='" + messages.MessagesSystemFalse + "'";
            MySql += " where MessagesID='" + messages.MessagesID + "'";
            DataProviders.DataConnectionHepler.Instance().ExceCuteSql(MySql);
        }
    }
}
