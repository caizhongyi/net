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
    public class PlaySqlDataProvider : PlayDataProvider
    {
        private string MySql;
        public override void InsertPlayMoney(Components.Components.PlayMoney paly)
        {
            string sql = string.Empty;
            string sqlinsert = string.Empty;
            sql = "insert into DoNetBbs_PlayMoney (";
            sqlinsert += ") values (";
            sql += "PlayMoneys,";
            sqlinsert += "'" + paly.PlayMoneys + "',";
            sql += "PlayMoneyFromUserID,";
            sqlinsert += "'" + paly.PlayMoneyFromUserID + "',";
            sql += "PlayMoneyContent,";
            sqlinsert += "'" + paly.PlayMoneyContent + "',";
            sql += "PlayMoneyReceiveUserID,";
            sqlinsert += "'" + paly.PlayMoneyReceiveUserID + "',";
            sql += "PlayMoneyCreateTime,";
            sqlinsert += "'" + paly.PlayMoneyCreateTime + "',";
            sql += "PlayMoneyType";
            sqlinsert += "'" + paly.PlayMoneyType + "'";
            sql += sqlinsert + ")";

            //HttpContext.Current.Response.Write(sql);
            //return;
            DataConnectionHepler.Instance().ExceCuteSql(sql);
        }
    }
}
