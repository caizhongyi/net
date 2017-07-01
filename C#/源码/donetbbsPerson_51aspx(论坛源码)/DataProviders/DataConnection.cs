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
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Web;
using System.Xml;
using System.Collections;
using System.Web.Caching;
namespace DataProviders
{

    public class DataConnection : DataConnectionHepler
    {
        private static string _AccessData;
        public static string AccessData
        {
            get {return _AccessData;}
            set { _AccessData = value; }
        }
        /// <summary>
        /// 输入SELECT语句，获得返回数据集,start 为第一条数据，size 为读取的条数，tablename给该数据库表的名称，其中当size为0时，读整个表.
        /// </summary>
        /// <param name="sql">查询语句</param>
        /// <param name="start">数据开始位置</param>
        /// <param name="size">数据结束位置，为0时，读全部数据</param>
        /// <param name="tablename">数据库表的名称</param>
        /// <returns>返回DataTable</returns>
        public override DataTable DataAdapter(string sql, int start, int size, string tablename)
        {
        DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
        if (IDoNetBbs.GetConfiguration("ConnectionType").ToString() == "Access")
            {
                return AccessDataAdapter(sql, start, size, tablename);
            }
            else
            {
                return SqlserverDataAdapter(sql, start, size, tablename);
            }
        }
        /// <summary>
        /// 执行SQL语句，如UPDATE，INSERT等.
        /// </summary>
        /// <param name="sql"></param>
        public override void ExceCuteSql(string sql)
        {
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            if (IDoNetBbs.GetConfiguration("ConnectionType").ToString() == "Access")
            {
                AccessExceCuteSql(sql);
            }
            else
            {
                SqlserverExceCuteSql(sql);
            }
        }
        private DataTable AccessDataAdapter(string sql, int start, int size, string tablename)
        {
            try
            {
                string conn;
                DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
                //string file = IDoNetBbs.GetHttpContext().Server.MapPath("~" + IDoNetBbs.GetConfiguration("AccessData").ToString());
                conn = IDoNetBbs.GetConfiguration("AccessConnection").ToString() + AccessData;
                OleDbDataAdapter OleDbDataAdapter1 = new OleDbDataAdapter(sql, conn);
                DataSet DataSet1 = new DataSet();
                if (size == 0)
                {
                    OleDbDataAdapter1.Fill(DataSet1, tablename);
                }
                else
                {
                    OleDbDataAdapter1.Fill(DataSet1, start, size, tablename);
                }
                return DataSet1.Tables[0];
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private void AccessExceCuteSql(string sql)
        {
            OleDbConnection conn;    
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            //string file = IDoNetBbs.GetHttpContext().Server.MapPath("~" + IDoNetBbs.GetConfiguration("AccessData").ToString());
            conn = new OleDbConnection(IDoNetBbs.GetConfiguration("AccessConnection").ToString() + AccessData);
            try
            {
                OleDbCommand OleDbCmd = new OleDbCommand(sql, conn);
                conn.Open();
                OleDbCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
        }//
        private DataTable SqlserverDataAdapter(string sql, int start, int size, string tablename)
        {
            try
            {
                string conn;
                DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
                conn = IDoNetBbs.GetConfiguration("SqlserverConnection").ToString();
                SqlDataAdapter SqlDataAdapter1 = new SqlDataAdapter(sql, conn);
                DataSet DataSet1 = new DataSet();
                if (size == 0)
                {
                    SqlDataAdapter1.Fill(DataSet1, tablename);
                }
                else
                {
                    SqlDataAdapter1.Fill(DataSet1, start, size, tablename);
                }
                return DataSet1.Tables[0];
            }
            catch (Exception ex)
            {

                throw (ex);

            }
        }
        private void SqlserverExceCuteSql(string sql)
        {
            SqlConnection conn;
            DoNetBbs.DoNetBbsClassHepler IDoNetBbs = DoNetBbs.DoNetBbsClassHepler.Instance();
            conn = new SqlConnection(IDoNetBbs.GetConfiguration("SqlserverConnection").ToString());
            try
            {
                SqlCommand SqlCommand1 = new SqlCommand(sql, conn);
                conn.Open();
                SqlCommand1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                conn.Close();
            }
        }//

        /// <summary>
        /// 取PageTemp.xml节点的值
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <returns>返回节点值</returns>
        public override string GetTempXmlNode(string node)
        {
            string key = "zh-CN-PageTemp";
            Hashtable _cachetable = Components.CsCache.Get(key) as Hashtable;
            Hashtable resources;
            if (_cachetable == null)
            {
                HttpContext context = HttpContext.Current;
                string file = context.Server.MapPath("~/XmlFile/Resource/zh-CN/PageTemp.xml");
                resources = new Hashtable();
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                foreach (XmlNode n in doc.SelectSingleNode("WebSite").ChildNodes)
                {
                    resources.Add(n.Attributes["name"].Value, n.InnerXml);
                }
                CacheDependency dep = new CacheDependency(file);
                Components.CsCache.Max(key, resources, dep);
            }
            else
            {
                resources = _cachetable;
            }
            if (resources[node] == null)
            {
                return string.Empty;
            }
            else
            {
                return resources[node].ToString().Replace("<!--", null).Replace("-->", null);
            }


        }
        /// <summary>
        /// 取Resources.xml节点的值
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <returns>返回节点值</returns>
        public override string GetResourcesXmlNode(string node)
        {
            string key = "zh-CN-Resources";
            Hashtable _cachetable = Components.CsCache.Get(key) as Hashtable;
            Hashtable resources;
            if (_cachetable == null)
            {
                HttpContext context = HttpContext.Current;
                string file = context.Server.MapPath("~/XmlFile/Resource/zh-CN/Resources.xml");
                resources = new Hashtable();
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                foreach (XmlNode n in doc.SelectSingleNode("WebSite").ChildNodes)
                {
                    resources.Add(n.Attributes["name"].Value, n.InnerXml);
                }
                CacheDependency dep = new CacheDependency(file);
                Components.CsCache.Max(key, resources, dep);
            }
            else
            {
                resources = _cachetable;
            }
            if (resources[node] == null)
            {
                return string.Empty;
            }
            else
            {
                return resources[node].ToString();
            }
        }
        /// <summary>
        /// 取WebSite.config节点的值
        /// </summary>
        /// <param name="node">节点名称</param>
        /// <returns>返回节点值</returns>
        public override string GetWebSiteConfig(string node)
        {
            string key = "WebSite-Config";
            Hashtable _cachetable = Components.CsCache.Get(key) as Hashtable;
            Hashtable resources;
            if (_cachetable == null)
            {
                HttpContext context = HttpContext.Current;
                string file = context.Server.MapPath("~/WebSite.config");
                resources = new Hashtable();
                XmlDocument doc = new XmlDocument();
                doc.Load(file);
                foreach (XmlNode n in doc.SelectSingleNode("WebSite").ChildNodes)
                {
                    resources.Add(n.Attributes["name"].Value, n.Attributes["value"].Value);
                }
                CacheDependency dep = new CacheDependency(file);
                Components.CsCache.Max(key, resources, dep);
            }
            else
            {
                resources = _cachetable;
            }
            if (resources[node] == null)
            {
                return string.Empty;
            }
            else
            {
                return resources[node].ToString();
            }
        }
        /// <summary>
        /// 取TreeXml.xml节点的值
        /// </summary>
        /// <param name="name">根节点名称</param>
        /// <param name="node">节点名称</param>
        /// <returns>返回节点值</returns>
        public override string GetTreeXmlNode(string name, string node)
        {
            DataSet DataSet1 = new DataSet();
            string key = "WebSite-TreeXml";
            DataSet _cachetable = Components.CsCache.Get(key) as DataSet;
            if (_cachetable == null)
            {
                HttpContext context = HttpContext.Current;
                string file = context.Server.MapPath("~/XmlFile/Resource/zh-CN/TreeXml.xml");
                DataSet1.ReadXml(file);
                Components.CsCache.Insert(key, DataSet1, null);
            }
            else
            {
                DataSet1 = _cachetable;
            }
            for (int i = 0; i < DataSet1.Tables[name].Columns.Count; i++)
            {
                if (node == DataSet1.Tables[name].Columns[i].ToString())
                {
                    return DataSet1.Tables[name].Rows[0][node].ToString();
                }
            }
            return null;
        }

        public override DataRow GetTreeXmlRow(string node)
        {
            DataSet DataSet1 = new DataSet();
            string key = "WebSite-TreeXml";
            DataSet _cachetable = Components.CsCache.Get(key) as DataSet;
            if (_cachetable == null)
            {
                HttpContext context = HttpContext.Current;
                string file = context.Server.MapPath("~/XmlFile/Resource/zh-CN/TreeXml.xml");
                DataSet1.ReadXml(file);
                Components.CsCache.Insert(key, DataSet1, null);
            }
            else
            {
                DataSet1 = _cachetable;
            }
            if (DataSet1.Tables[node].Columns.Count > 0)
            {
                return DataSet1.Tables[node].Rows[0];
            }
            else
            {
                return null;
            }
        }
    }
}
