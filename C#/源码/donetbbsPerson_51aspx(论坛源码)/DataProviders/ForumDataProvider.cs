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
namespace DataProviders
{
    public abstract class ForumDataProvider
    {
        private static ForumDataProvider _defaultInstance = null;
        static ForumDataProvider()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// 实现ForumDataProvider接口.
        /// </summary>
        /// <returns></returns>
        public static ForumDataProvider Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.ForumSqlDataProvider");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as ForumDataProvider;
        }

        /// <summary>
        /// 根据论坛ID，取论坛信息
        /// </summary>
        /// <param name="boardID">论坛ID</param>
        /// <param name="Cach">是否缓冲</param>
        /// <returns>返回该论坛信息</returns>
        public abstract DataRow SetBoard(int boardID, bool Cach);

        public abstract ArrayList SetBoardList(int boardID, bool Cach);

        public abstract ArrayList SetBoardParentList(int boardID, bool Cach);
        public abstract void InsertBoardInfo(Components.Components.Board board);
        public abstract void UpdateBoardInfo(Components.Components.Board board);
        public abstract void DeleteBoard(int boardID);
        public abstract ArrayList SetBoardTopic(int boardID, bool Cach);

        public abstract int SetTopicCount(string query, bool Cach);

        public abstract ArrayList SetTopic(string query, int start, int size, bool Cach);
        public abstract DataRow SetTopic(int topicID, bool Cach);

        public abstract int SetTopicInfoCount(int topicID, bool Cach);
        public abstract ArrayList SetTopicInfoList(int topicID, int index, int count, bool Cach);

        public abstract void UpdateTopic(Components.Components.Topic topic);
        public abstract void UpdateTopicView(int topicID);

        public abstract DataRow SetTopicInfo(int topicInfoID, bool Cach);
        public abstract DataRow SetRootTopicInfo(int rootID, bool Cach);


        public abstract void InsertTopic(Components.Components.Topic topic);

        public abstract DataRow SetLastTopic(string topicTitle, int boardID, int userID, bool Cach);

        public abstract void InsertTopicInfo(Components.Components.TopicInfo topicInfo);

        public abstract void UpdateTopicInfo(Components.Components.TopicInfo topicInfo);

        public abstract void DeleteTopicInfo(int topicInfoID);
        public abstract void DeleteTopic(int topicID);

        public abstract bool SetMyReply(int userID, int topicID, bool Cach);

    }
}
