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
    public abstract class EmailDataProvider
    {
        private static EmailDataProvider _defaultInstance = null;
        static EmailDataProvider()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// 实现WebSiteDataProvider接口.
        /// </summary>
        /// <returns></returns>
        public static EmailDataProvider Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.EmailSqlDataProvider");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as EmailDataProvider;
        }
        public abstract void InsertEmailInfo(Components.Components.EmailWork email);
        public abstract ArrayList SetEmailWork(bool Cach);
        public abstract void UpdateEmailWork(Components.Components.EmailWork email);
    }
}
