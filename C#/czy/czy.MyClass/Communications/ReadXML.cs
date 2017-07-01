using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace MyClass
{
    /// <summary>
    /// ReadXML 的摘要说明
    /// </summary>
    public class ReadXML
    {
        private string _mapPath;
        /// <summary>
        /// 物理路径
        /// </summary>
        public string MapPath
        {
            get { return _mapPath; }
        }
        private string _key;
        /// <summary>
        /// XML文件名
        /// </summary>
        public string Key
        {
            get { return _key; }
        }
        private string _file;
        /// <summary>
        /// 服务器上的文件物理路径
        /// </summary>
        public string File
        {
            get { return _file; }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="mapPath"></param>
        public ReadXML(string mapPath)
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            int startIndex = mapPath.LastIndexOf('/') + 1;
            int length = mapPath.Length - startIndex - 1;
            _key = mapPath.Substring(startIndex, length);
            _mapPath = mapPath;
            HttpContext context = HttpContext.Current;
            _file = context.Server.MapPath(mapPath);

        }
        /// <summary>
        /// 取TreeXml.xml节点的值
        /// </summary>
        /// <param name="name">根节点名称</param>
        /// <param name="node">节点名称</param>
        /// <returns>返回节点值</returns>
        public string GetTreeXmlNode(string name, string node)
        {
            DataSet DataSet1 = new DataSet();
            DataSet1.ReadXml(_file);
            for (int i = 0; i < DataSet1.Tables[name].Columns.Count; i++)
            {
                if (node == DataSet1.Tables[name].Columns[i].ToString())
                {
                    return DataSet1.Tables[name].Rows[0][node].ToString();
                }
            }
            return null;
        }

        public DataRow GetTreeXmlRow(string node)
        {
            DataSet DataSet1 = new DataSet();
            DataSet1.ReadXml(_file);
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