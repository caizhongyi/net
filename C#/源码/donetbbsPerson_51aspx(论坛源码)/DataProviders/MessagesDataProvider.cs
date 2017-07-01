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
    public abstract class MessagesDataProvider
    {
        private static MessagesDataProvider _defaultInstance = null;
        static MessagesDataProvider()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// 实现MessagesDataProvider接口.
        /// </summary>
        /// <returns></returns>
        public static MessagesDataProvider Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.MessagesSqlDataProvider");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as MessagesDataProvider;
        }
        public abstract void InsertMessages(Components.Components.Messages messages);
        public abstract ArrayList SetMessagesList(int userID, string userIP, bool Cach);
        public abstract void UpdateMessages(Components.Components.Messages messages);
    }
}
