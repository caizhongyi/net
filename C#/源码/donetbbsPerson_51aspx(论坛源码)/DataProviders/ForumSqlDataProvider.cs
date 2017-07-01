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
using System.Text;
using System.Data;
using System.Collections;
using System.Web;
namespace DataProviders
{
    public class ForumSqlDataProvider : ForumDataProvider
    {
        private string MySql;
        /// <summary>
        /// 根据论坛ID，取论坛信息
        /// </summary>
        /// <param name="boardID">论坛ID</param>
        /// <param name="Cach">是否缓冲</param>
        /// <returns>返回该论坛信息</returns>
        public override DataRow SetBoard(int boardID, bool Cach)
        {
            MySql = "select * from DoNetBbs_Board where BoardID=" + boardID.ToString() + "";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-Board" + boardID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_Board");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_Board");
            }

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            else
            {
                return null;
            }

        }//
        public override ArrayList SetBoardList(int boardID, bool Cach)
        {
            Components.Components.Board Rs = new Components.Components.Board();
            DataTable dt;
            MySql = "select * from DoNetBbs_Board order by BoardParentID asc,BoardOrders asc";
            if (Cach)
            {
                string key = "DoNetBbs-Board-List-" + boardID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_Board");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_Board");
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (boardID == 0)
                    {
                        if (int.Parse(row["BoardParentID"].ToString()) == 0)
                        {
                            Components.Components.Board _Arraylist = new Components.Components.Board();
                            _Arraylist.SetDataProviders(row);
                            Rs.Arraylist.Add(_Arraylist);
                            _Arraylist.Length = 0;
                            SetBoardList(dt, int.Parse(row["BoardID"].ToString()), 1,Rs);
                        }
                    }
                    else
                    {
                        if (boardID == int.Parse(row["BoardID"].ToString()))
                        {
                            Components.Components.Board _Arraylist = new Components.Components.Board();
                            _Arraylist.SetDataProviders(row);
                            _Arraylist.Length = 0;
                            Rs.Arraylist.Add(_Arraylist);
                            SetBoardList(dt, int.Parse(row["BoardID"].ToString()), 1,Rs);
                            return Rs.Arraylist;
                        }
                    }
                }
            }
            return Rs.Arraylist;
        }



        private void SetBoardList(DataTable de, int id, int length, Components.Components.Board Rs)
        {
            DataRow[] rows = de.Select("BoardParentID='" + id + "'", "BoardOrders asc,BoardID asc");//取出id子节点进行绑定
            for (int i = 0; i < rows.Length; i++)
            {
                Components.Components.Board _Arraylist = new Components.Components.Board();
                _Arraylist.SetDataProviders(rows[i]);
                _Arraylist.Length = length;
                Rs.Arraylist.Add(_Arraylist);
                SetBoardList(de, int.Parse(rows[i]["BoardID"].ToString()), length + 1, Rs);
            }

        }//

        public override ArrayList SetBoardParentList(int boardID, bool Cach)
        {
            Components.Components.Board Rs = new Components.Components.Board();
            DataTable dt;
            MySql = "select * from DoNetBbs_Board order by BoardParentID asc,BoardOrders asc";
            if (Cach)
            {
                string key = "DoNetBbs-Board-Parent-" + boardID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_Board");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_Board");
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (boardID == int.Parse(row["BoardID"].ToString()))
                    {
                        Components.Components.Board _Arraylist = new Components.Components.Board();
                        _Arraylist.SetDataProviders(row);
                        _Arraylist.Length = 0;
                        Rs.Arraylist.Add(_Arraylist);
                        SetBoardParentList(dt, int.Parse(row["BoardParentID"].ToString()), 1, Rs);
                        Rs.Arraylist.Reverse();
                        return Rs.Arraylist;
                    }
                }
            }
            return Rs.Arraylist;
        }
        private void SetBoardParentList(DataTable de, int id, int length, Components.Components.Board Rs)
        {
            DataRow[] rows = de.Select("BoardID='" + id + "'", "BoardOrders asc,BoardID asc");//取出id子节点进行绑定
            for (int i = 0; i < rows.Length; i++)
            {
                Components.Components.Board _Arraylist = new Components.Components.Board();
                _Arraylist.SetDataProviders(rows[i]);
                _Arraylist.Length = length;
                Rs.Arraylist.Add(_Arraylist);
                SetBoardParentList(de, int.Parse(rows[i]["BoardParentID"].ToString()), length + 1, Rs);
            }

        }//

        public override void UpdateBoardInfo(Components.Components.Board board)
        {
            MySql = "update DoNetBbs_Board set ";
            MySql += "BoardParentID='" + board.BoardParentID + "',";
            MySql += "BoardName ='" + board.BoardName + "',";
            MySql += "BoardTypeID ='" + board.BoardTypeID + "',";
            MySql += "BoardSubject ='" + board.BoardSubject + "',";
            MySql += "BoardOrders ='" + board.BoardOrders + "',";
            MySql += "BoardMaster ='" + board.BoardMaster + "',";
            MySql += "BoardLastTopicTitle ='" + board.BoardLastTopicTitle + "',";
            MySql += "BoardLastTopicID ='" + board.BoardLastTopicID + "',";
            MySql += "BoardLastTopicTime ='" + board.BoardLastTopicTime.ToString() + "',";
            MySql += "BoardLastTopicUserNickName ='" + board.BoardLastTopicUserNickName + "',";
            MySql += "BoardLastTopicUserID ='" + board.BoardLastTopicUserID + "',";
            MySql += "BoardFalse ='" + board.BoardFalse + "',";
            MySql += "BoardImages ='" + board.BoardImages + "',";
            MySql += "BoardAbout ='" + board.BoardAbout + "',";
            MySql += "BoardPostNumber = '" + board.BoardPostNumber + "',";
            MySql += "BoardTodayPostNumber ='" + board.BoardTodayPostNumber + "',";
            MySql += "BoardTopicNumber ='" + board.BoardTopicNumber + "',";
            MySql += "BoardDelPoint ='" + board.BoardDelPoint + "',";
            MySql += "BoardPostRole ='" + board.BoardPostRole + "',";
            MySql += "BoardRePostRole ='" + board.BoardRePostRole + "',";
            MySql += "BoardViewRole ='" + board.BoardViewRole + "'";
            MySql += " where BoardID=" + board.BoardID + "";
            //HttpContext.Current.Response.Write(MySql);
            DataConnectionHepler.Instance().ExceCuteSql(MySql);
        }

        public override void DeleteBoard(int boardID)
        {
            DataConnectionHepler.Instance().ExceCuteSql("delete from DoNetBbs_Board where BoardID = " + boardID + "");
        }

        public override ArrayList SetBoardTopic(int boardID, bool Cach)
        {
            MySql = "select * from DoNetBbs_Topic where TopicBoardID=" + boardID.ToString() + "";
            Components.Components.Topic Rs = new Components.Components.Topic();
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-BoardTopic-List" + boardID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_Topic");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 0, "DoNetBbs_Topic");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.Topic _Arraylist = new Components.Components.Topic();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }

        public override void InsertBoardInfo(Components.Components.Board board)
        {
            string sql = string.Empty;
            string sqlinsert = string.Empty;

            sql = "insert into DoNetBbs_Board (";
            sqlinsert = ") values (";
            sql += "BoardParentID,";
            sqlinsert += "'" + board.BoardParentID + "',";
            sql += "BoardName,";
            sqlinsert += "'" + board.BoardName + "',";
            sql += "BoardTypeID,";
            sqlinsert += "'" + board.BoardTypeID + "',";
            sql += "BoardSubject,";
            sqlinsert += "'" + board.BoardSubject + "',";
            sql += "BoardOrders,";
            sqlinsert += "'" + board.BoardOrders + "',";
            sql += "BoardMaster,";
            sqlinsert += "'" + board.BoardMaster + "',";
            sql += "BoardLastTopicTitle,";
            sqlinsert += "'" + board.BoardLastTopicTitle + "',";
            sql += "BoardLastTopicID,";
            sqlinsert += "'" + board.BoardLastTopicID + "',";
            sql += "BoardLastTopicTime,";
            sqlinsert += "'" + board.BoardLastTopicTime + "',";
            sql += "BoardLastTopicUserNickName,";
            sqlinsert += "'" + board.BoardLastTopicUserNickName + "',";
            sql += "BoardLastTopicUserID,";
            sqlinsert += "'" + board.BoardLastTopicUserID + "',";
            sql += "BoardFalse,";
            sqlinsert += "'" + board.BoardFalse + "',";
            sql += "BoardImages,";
            sqlinsert += "'" + board.BoardImages + "',";
            sql += "BoardAbout,";
            sqlinsert += "'" + board.BoardAbout + "',";
            sql += "BoardPostNumber,";
            sqlinsert += "'" + board.BoardPostNumber + "',";
            sql += "BoardTodayPostNumber,";
            sqlinsert += "'" + board.BoardTodayPostNumber + "',";
            sql += "BoardTopicNumber,";
            sqlinsert += "'" + board.BoardTopicNumber + "',";
            sql += "BoardDelPoint,";
            sqlinsert += "'" + board.BoardDelPoint + "',";
            sql += "BoardPostRole,";
            sqlinsert += "'" + board.BoardPostRole + "',";
            sql += "BoardRePostRole,";
            sqlinsert += "'" + board.BoardRePostRole + "',";
            sql += "BoardViewRole";
            sqlinsert += "'" + board.BoardViewRole + "'";


            sql += sqlinsert + ")";
            DataConnectionHepler.Instance().ExceCuteSql(sql);
        }

        public override int SetTopicCount(string query, bool Cach)
        {
            MySql = "select count(TopicID) as CountNumber from DoNetBbs_Topic " + query;
            //DoNetBbs.DoNetBbsClassHepler.Instance().GetHttpContext().Response.Write(MySql);
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-Topic-count" + query.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_Topic");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_Topic");
            }

            if (dt.Rows.Count > 0)
            {
                return int.Parse(dt.Rows[0][0].ToString());
            }
            else
            {
                return 0;
            }
        }//

        public override ArrayList SetTopic(string query, int start, int size, bool Cach)
        {
            Components.Components.Topic Rs = new Components.Components.Topic();
            DataTable dt;
            MySql = "select * from DoNetBbs_Topic " + query;
            if (Cach)
            {
                string key = "WebSite-Topic" + query.ToString() + "-" + start.ToString() + "-" + size.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, start, size, "DoNetBbs_Topic");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, start, size, "DoNetBbs_Topic");
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.Topic _Arraylist = new Components.Components.Topic();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }//

        public override DataRow SetTopic(int topicID, bool Cach)
        {
            MySql = "select * from DoNetBbs_Topic where TopicID=" + topicID.ToString() + "";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-Topic" + topicID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_Topic");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_Topic");
            }

            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0];
            }
            else
            {
                return null;
            }

        }//

        public override int SetTopicInfoCount(int topicID, bool Cach)
        {
            MySql = "select count(TopicInfoID) as CountNumber from DoNetBbs_TopicInfo where TopicInfoRootID=" + topicID + "";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-TopicInfo-Count-" + topicID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_TopicInfo");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_TopicInfo");
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
        
        //
        public override ArrayList SetTopicInfoList(int topicID, int index, int count, bool Cach)
        {
            Components.Components.TopicInfo Rs = new Components.Components.TopicInfo();
            DataTable dt;
            MySql = "select * from DoNetBbs_TopicInfo where TopicInfoRootID=" + topicID + " order by TopicInfoPostTime asc";
            if (Cach)
            {
                string key = "DoNetBbs-TopicInfo-List-" + topicID.ToString() + "-" + index.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, index, count, "DoNetBbs_TopicInfo");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, index, count, "DoNetBbs_TopicInfo");
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Components.Components.TopicInfo _Arraylist = new Components.Components.TopicInfo();
                    _Arraylist.SetDataProviders(row);
                    Rs.Arraylist.Add(_Arraylist);
                }
            }
            return Rs.Arraylist;
        }//

        public override void UpdateTopic(Components.Components.Topic topic)
        {
            MySql = "update DoNetBbs_Topic set ";
            MySql += "TopicTitle = '" + topic.TopicTitle + "',";
            MySql += "TopicBoardID =  '" + topic.TopicBoardID + "',";
            MySql += "TopicImages = '" + topic.TopicImages + "',";
            MySql += "TopicRePostEmail = '" + topic.TopicRePostEmail + "',";
            MySql += "TopicReNumber = '" + topic.TopicReNumber + "',";
            MySql += "TopicViewNumber = '" + topic.TopicViewNumber + "',";
            MySql += "TopicReLastUserID = '" + topic.TopicReLastUserID + "',";
            MySql += "TopicReLastUserNickName = '" + topic.TopicReLastUserNickName + "',";
            MySql += "TopicBest =  '" + topic.TopicBest + "',";
            MySql += "TopicRecommend = '" + topic.TopicRecommend + "',";
            MySql += "TopicTotalAtTop = '" + topic.TopicTotalAtTop + "',";
            MySql += "TopicLastReTime = '" + topic.TopicLastReTime + "',";
            MySql += "TopicSubjectID = '" + topic.TopicSubjectID + "',";
            MySql += "TopicFalse = '" + topic.TopicFalse + "',";
            MySql += "TopicPostUserID = '" + topic.TopicPostUserID + "',";
            MySql += "TopicPostUserNickName = '" + topic.TopicPostUserNickName + "',";
            MySql += "TopicPostTime = '" + topic.TopicPostTime + "',";
            MySql += "TopicSpecialTitle = '" + topic.TopicSpecialTitle + "'";
            MySql += " where TopicID=" + topic.TopicID + "";
            DataConnectionHepler.Instance().ExceCuteSql(MySql);
        }
        public override void UpdateTopicView(int topicID)
        {
            MySql = "update DoNetBbs_Topic set TopicViewNumber=TopicViewNumber+1 where TopicID=" + topicID + "";
            DataProviders.DataConnectionHepler.Instance().ExceCuteSql(MySql);
        }

        public override DataRow SetTopicInfo(int topicInfoID, bool Cach)
        {
            MySql = "select * from DoNetBbs_TopicInfo where TopicInfoID=" + topicInfoID.ToString() + "";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-TopicInfo" + topicInfoID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_TopicInfo");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_TopicInfo");
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

        public override DataRow SetRootTopicInfo(int rootID, bool Cach)
        {
            MySql = "select * from DoNetBbs_TopicInfo where TopicInfoRootID=" + rootID.ToString() + " and TopicInfoParentID=0";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-Root-TopicInfo" + rootID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_TopicInfo");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_TopicInfo");
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

        public override void InsertTopic(Components.Components.Topic topic)
        {
            string sql = string.Empty;
            string sqlinsert = string.Empty;

            sql = "insert into DoNetBbs_Topic (";
            sqlinsert = ") values (";
            sql += "TopicTitle,";
            sqlinsert += "'" + topic.TopicTitle + "',";
            sql += "TopicBoardID,";
            sqlinsert += "'" + topic.TopicBoardID + "',";
            sql += "TopicImages,";
            sqlinsert += "'" + topic.TopicImages + "',";
            sql += "TopicReNumber,";
            sqlinsert += "'" + topic.TopicReNumber + "',";
            sql += "TopicViewNumber,";
            sqlinsert += "'" + topic.TopicViewNumber + "',";
            sql += "TopicReLastUserID,";
            sqlinsert += "'" + topic.TopicReLastUserID + "',";
            sql += "TopicReLastUserNickName,";
            sqlinsert += "'" + topic.TopicReLastUserNickName + "',";
            sql += "TopicBest,";
            sqlinsert += "'" + topic.TopicBest + "',";
            sql += "TopicRecommend,";
            sqlinsert += "'" + topic.TopicRecommend + "',";
            sql += "TopicTotalAtTop,";
            sqlinsert += "'" + topic.TopicTotalAtTop + "',";
            sql += "TopicLastReTime,";
            sqlinsert += "'" + topic.TopicLastReTime + "',";
            sql += "TopicSubjectID,";
            sqlinsert += "'" + topic.TopicSubjectID + "',";
            sql += "TopicFalse,";
            sqlinsert += "'" + topic.TopicFalse + "',";
            sql += "TopicPostUserID,";
            sqlinsert += "'" + topic.TopicPostUserID + "',";
            sql += "TopicPostUserNickName,";
            sqlinsert += "'" + topic.TopicPostUserNickName + "',";
            sql += "TopicPostTime,";
            sqlinsert += "'" + topic.TopicPostTime + "',";
            sql += "TopicRePostEmail,";
            sqlinsert += "'" + topic.TopicRePostEmail + "',";
            sql += "TopicSpecialTitle";
            sqlinsert += "'" + topic.TopicSpecialTitle + "'";
            sql += sqlinsert + ")";
            //HttpContext.Current.Response.Write("window.open()");
            //return;
            DataConnectionHepler.Instance().ExceCuteSql(sql);
        }

        public override DataRow SetLastTopic(string topicTitle, int boardID, int userID, bool Cach)
        {
            MySql = "select * from DoNetBbs_Topic where TopicTitle='" + topicTitle.ToString() + "' and TopicBoardID=" + boardID + " and TopicPostUserID=" + userID + " order by TopicID desc";
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-LastTopic" + topicTitle.ToString() + "-" + boardID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_Topic");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_Topic");
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

        public override void InsertTopicInfo(Components.Components.TopicInfo topicInfo)
        {
            string sql = string.Empty;
            string sqlinsert = string.Empty;
            sql = "insert into DoNetBbs_TopicInfo (";
            sqlinsert = ") values (";
            sql += "TopicInfoTitle,";
            sqlinsert += "'" + topicInfo.TopicInfoTitle + "',";
            sql += "TopicInfoUserID,";
            sqlinsert += "'" + topicInfo.TopicInfoUserID + "',";
            sql += "TopicInfoRootID,";
            sqlinsert += "'" + topicInfo.TopicInfoRootID + "',";
            sql += "TopicInfoParentID,";
            sqlinsert += "'" + topicInfo.TopicInfoParentID + "',";
            sql += "TopicInfoUserNickName,";
            sqlinsert += "'" + topicInfo.TopicInfoUserNickName + "',";
            sql += "TopicInfoUserIP,";
            sqlinsert += "'" + topicInfo.TopicInfoUserIP + "',";
            sql += "TopicInfoFalse,";
            sqlinsert += "'" + topicInfo.TopicInfoFalse + "',";
            sql += "TopicInfoHtml,";
            sqlinsert += "'" + topicInfo.TopicInfoHtml + "',";
            sql += "TopicInfoText,";
            sqlinsert += "'" + topicInfo.TopicInfoText + "',";
            sql += "TopicInfoViewRole,";
            sqlinsert += "'" + topicInfo.TopicInfoViewRole + "',";
            sql += "TopicInfoRePostRole,";
            sqlinsert += "'" + topicInfo.TopicInfoRePostRole + "',";
            sql += "TopicInfoFace,";
            sqlinsert += "'" + topicInfo.TopicInfoFace + "',";
            sql += "TopicInfoBuyMoney,";
            sqlinsert += "'" + topicInfo.TopicInfoBuyMoney + "',";
            sql += "TopicInfoPostTime,";
            sqlinsert += "'" + topicInfo.TopicInfoPostTime + "',";
            sql += "TopicInfoViewUserGroup,";
            sqlinsert += "'" + topicInfo.TopicInfoViewUserGroup + "',";
            sql += "TopicInfoRePostUserGroup,";
            sqlinsert += "'" + topicInfo.TopicInfoRePostUserGroup + "',";
            sql += "TopicInfoSignFalse,";
            sqlinsert += "'" + topicInfo.TopicInfoSignFalse + "',";
            sql += "TopicInfoReply,";
            sqlinsert += "'" + topicInfo.TopicInfoReply + "',";
            sql += "TopicInfoEditHistory";
            sqlinsert += "'" + topicInfo.TopicInfoEditHistory + "'";
            sql += sqlinsert + ")";
            //HttpContext.Current.Response.Write(sql);
            //return;
            DataConnectionHepler.Instance().ExceCuteSql(sql);
        }

        public override void UpdateTopicInfo(Components.Components.TopicInfo topicInfo)
        {
            MySql = "update DoNetBbs_TopicInfo set ";
            MySql += "TopicInfoTitle = '" + topicInfo.TopicInfoTitle + "',";
            MySql += "TopicInfoUserID = '" + topicInfo.TopicInfoUserID + "',";
            MySql += "TopicInfoRootID = '" + topicInfo.TopicInfoRootID + "',";
            MySql += "TopicInfoParentID = '" + topicInfo.TopicInfoParentID + "',";
            MySql += "TopicInfoUserNickName = '" + topicInfo.TopicInfoUserNickName + "',";
            MySql += "TopicInfoUserIP = '" + topicInfo.TopicInfoUserIP + "',";
            MySql += "TopicInfoFalse = '" + topicInfo.TopicInfoFalse + "',";
            MySql += "TopicInfoHtml = '" + topicInfo.TopicInfoHtml + "',";
            MySql += "TopicInfoText = '" + topicInfo.TopicInfoText + "',";
            MySql += "TopicInfoViewRole = '" + topicInfo.TopicInfoViewRole + "',";
            MySql += "TopicInfoRePostRole ='" + topicInfo.TopicInfoRePostRole + "',";
            MySql += "TopicInfoFace = '" + topicInfo.TopicInfoFace + "',";
            MySql += "TopicInfoBuyMoney = '" + topicInfo.TopicInfoBuyMoney + "',";
            MySql += "TopicInfoPostTime = '" + topicInfo.TopicInfoPostTime + "',";
            MySql += "TopicInfoViewUserGroup = '" + topicInfo.TopicInfoViewUserGroup + "',";
            MySql += "TopicInfoRePostUserGroup = '" + topicInfo.TopicInfoRePostUserGroup + "',";
            MySql += "TopicInfoSignFalse = '" + topicInfo.TopicInfoSignFalse + "',";
            MySql += "TopicInfoReply = '" + topicInfo.TopicInfoReply + "',";
            MySql += "TopicInfoEditHistory = '" + topicInfo.TopicInfoEditHistory + "'";
            MySql += " where TopicInfoID = " + topicInfo.TopicInfoID + "";
            DataConnectionHepler.Instance().ExceCuteSql(MySql);
        }

        public override void DeleteTopic(int topicID)
        {
            DataConnectionHepler.Instance().ExceCuteSql("delete from DoNetBbs_Topic where TopicID = " + topicID + "");
        }

        public override void DeleteTopicInfo(int topicInfoID)
        {
            DataConnectionHepler.Instance().ExceCuteSql("delete from DoNetBbs_TopicInfo where TopicInfoID = " + topicInfoID + "");
        }

        public override bool SetMyReply(int userID, int topicID, bool Cach)
        {
            MySql = "select TopicInfoID from DoNetBbs_TopicInfo where TopicInfoUserID='" + userID + "' and TopicInfoRootID='" + topicID + "'";
            //DoNetBbs.DoNetBbsClassHepler.Instance().GetHttpContext().Response.Write(MySql);
            DataTable dt;
            if (Cach)
            {
                string key = "WebSite-Topic-myReply" + userID.ToString() + "-" + topicID.ToString();
                DataTable _cachetable = Components.CsCache.Get(key) as DataTable;
                if (_cachetable == null)
                {
                    dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_TopicInfo");
                    Components.CsCache.Insert(key, dt, null);
                }
                else
                {
                    dt = _cachetable;
                }
            }
            else
            {
                dt = DataConnectionHepler.Instance().DataAdapter(MySql, 0, 1, "DoNetBbs_TopicInfo");
            }

            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

      
    }
}
