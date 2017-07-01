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

namespace DataProviders
{
    public abstract class WebSiteDataProvider
    {
        private static WebSiteDataProvider _defaultInstance = null;
        static WebSiteDataProvider()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// 实现WebSiteDataProvider接口.
        /// </summary>
        /// <returns></returns>
        public static WebSiteDataProvider Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.WebSiteSqlDataProvider");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as WebSiteDataProvider;
        }
        /// <summary>
        /// 根据站点ID取该站点信息
        /// </summary>
        /// <param name="WebSiteID">站点ID</param>
        /// <param name="Cach">是否缓冲</param>
        /// <returns>返回该站点的信息，如果不存在，则返回Null</returns>
        public abstract DataRow SetWebSite(int webSiteID, bool Cach);
        public abstract void UpdateWebSite(Components.Components.WebSite webSite);
    }
}
