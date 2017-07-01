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
using System.Web;
namespace DataProviders
{
    public abstract class DataConnectionHepler
    {
        private static DataConnectionHepler _defaultInstance = null;
        static DataConnectionHepler()
        {
            CreateDefaultHepler();
        }
        /// <summary>
        /// 实现DataConnectionHepler接口.
        /// </summary>
        /// <returns></returns>
        public static DataConnectionHepler Instance()
        {
            return _defaultInstance;
        }
        private static void CreateDefaultHepler()
        {
            Type type = Type.GetType("DataProviders.DataConnection");
            object newObject = null;
            if (type != null)
            {
                newObject = Activator.CreateInstance(type);
            }
            _defaultInstance = newObject as DataConnectionHepler;
        }
        /// <summary>
        /// 输入SELECT语句，获得返回数据集,start 为第一条数据，size 为读取的条数，tablename给该数据库表的名称，其中当size为0时，读整个表.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="start"></param>
        /// <param name="size"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public abstract DataTable DataAdapter(string sql, int start, int size, string tablename);
        /// <summary>
        /// 执行SQL语句，如UPDATE，INSERT等.
        /// </summary>
        /// <param name="sql"></param>
        public abstract void ExceCuteSql(string sql);

        /// <summary>
        /// 取PageTemp.xml节点的值
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <returns>返回节点值</returns>
        public abstract string GetTempXmlNode(string node);
        /// <summary>
        /// 取Resources.xml节点的值
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <returns>返回节点值</returns>
        public abstract string GetResourcesXmlNode(string node);
        /// <summary>
        /// 取WebSite.config节点的值
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <returns>返回节点值</returns>
        public abstract string GetWebSiteConfig(string node);
        /// <summary>
        /// 取TreeXml.xml节点的值
        /// </summary>
        /// <param name="name">根节点名称</param>
        /// <param name="node">节点名称</param>
        /// <returns>返回节点值</returns>
        public abstract string GetTreeXmlNode(string name, string node);

        public abstract DataRow GetTreeXmlRow(string node);
    }
}
