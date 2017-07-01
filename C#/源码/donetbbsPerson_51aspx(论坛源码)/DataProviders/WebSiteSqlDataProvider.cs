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

namespace DataProviders
{
    public class WebSiteSqlDataProvider : WebSiteDataProvider
    {
        private string MySql;
        /// <summary>
        /// ����վ��IDȡ��վ����Ϣ
        /// </summary>
        /// <param name="WebSiteID">վ��ID</param>
        /// <param name="Cach">�Ƿ񻺳�</param>
        /// <returns>���ظ�վ�����Ϣ����������ڣ��򷵻�Null</returns>
        public override DataRow SetWebSite(int webSiteID, bool Cach)
        {
            string sql;
            sql = "select * from DoNetBbs_WebSite where webSiteID=" + webSiteID.ToString() + "";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-WebSite" + webSiteID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(sql, 0, 1, "DoNetBbs_WebSite");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(sql, 0, 1, "DoNetBbs_WebSite");
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

        public override void UpdateWebSite(Components.Components.WebSite webSite)
        {
            MySql = "update DoNetBbs_WebSite set ";
            MySql += "Forum_MaxOnline='" + webSite.Forum_MaxOnline + "',";
            MySql += "Forum_MaxOnlineDate ='" + webSite.Forum_MaxOnlineDate + "',";
            MySql += "Forum_TopicNumber ='" + webSite.Forum_TopicNumber + "',";
            MySql += "Forum_PostNumber ='" + webSite.Forum_PostNumber + "',";
            MySql += "Forum_TodayNumber ='" + webSite.Forum_TodayNumber + "',";
            MySql += "Forum_UserNumber ='" + webSite.Forum_UserNumber + "',";
            MySql += "Forum_YesTerdayNumber ='" + webSite.Forum_YesTerdayNumber + "',";
            MySql += "Forum_MaxPostNumber ='" + webSite.Forum_MaxPostNumber + "',";
            MySql += "Forum_MaxPostDate ='" + webSite.Forum_MaxPostDate + "',";
            MySql += "Forum_LastUserID ='" + webSite.Forum_LastUserID + "',";
            MySql += "Forum_LockIP ='" + webSite.Forum_LockIP + "',";
            MySql += "Forum_TodyDate ='" + webSite.Forum_TodyDate + "',";
            MySql += "Forum_StartDate ='" + webSite.Forum_StartDate + "',";
            MySql += "Forum_LastUserNickName ='" + webSite.Forum_LastUserNickName + "',";
            MySql += "Forum_UserOnline ='" + webSite.Forum_UserOnline + "',";
            MySql += "Forum_GuestOnline ='" + webSite.Forum_GuestOnline + "',";
            MySql += "Forum_AllOnline ='" + webSite.Forum_AllOnline + "',";
            MySql += "Forum_UserIllegal ='" + webSite.Forum_UserIllegal + "',";
            MySql += "Forum_SystemIllegal ='" + webSite.Forum_SystemIllegal + "'";
            MySql += " where WebSiteID=" + webSite.WebSiteID + "";
            DataConnectionHepler.Instance().ExceCuteSql(MySql);
        }
    }
}
