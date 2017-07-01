using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace czy.MyClass.UI
{
    /// <summary>
    /// 模版数据替换类
    /// </summary>
    public class DataConvertor
    {
        DataSet _dataSet;
        List<DataConvertorInfo> _dataConvertorInfos;
        List<object> _list;
        /// <summary>
        /// 对像集合
        /// </summary>
        public List<object> List
        {
            get { return _list; }
            set { _list = value; }
        }
        /// <summary>
        /// 数据集
        /// </summary>
        public DataSet DataSet
        {
            get { return _dataSet; }
            set { _dataSet = value; }
        }
        /// <summary>
        /// List数据集合
        /// </summary>
        public List<DataConvertorInfo> DataConvertorInfos
        {
            get { return _dataConvertorInfos; }
            set { _dataConvertorInfos = value; }
        }

        public DataConvertor()
        { }
        public DataConvertor(DataSet ds)
        {
            _dataSet = ds;
        }
        public DataConvertor(List<DataConvertorInfo> dataConvertorInfos)
        {
            _dataConvertorInfos = dataConvertorInfos;
        }
        public DataConvertor(List<object> list)
        {
            _list = list;
        }
        /// <summary>
        /// 获取替换后的结果
        /// </summary>
        /// <param name="templateHTML">HTML字符</param>
        /// <returns></returns>
        public string ReplaceData(string templateHTML)
        {
            StringBuilder sb = new StringBuilder();
            if (_dataSet != null)
            {
                foreach (DataRow dr in _dataSet.Tables[0].Rows)
                {
                    foreach (DataColumn c in _dataSet.Tables[0].Columns)
                    {
                        templateHTML = templateHTML.Replace(string.Format("[#{0}]", c.ColumnName), dr[c.ColumnName].ToString());
                    }
                }
            }

            if (_dataConvertorInfos != null)
            {
                foreach (DataConvertorInfo info in _dataConvertorInfos)
                {
                    templateHTML = templateHTML.Replace(string.Format("[#{0}]", info.Key), info.Value.ToString ());
                }
            }

            if (_list != null)
            {
                foreach (object o in _list)
                {
                    foreach (System.Reflection.PropertyInfo p in o.GetType().GetProperties())
                    {
                        templateHTML=templateHTML.Replace(string.Format("[#{0}]", p.Name), p.GetValue(o, null).ToString());
                    }
                }
            }
            return sb.ToString();
        }
        private string GetKey(string key)
        {
            return key.Substring(key.IndexOf("[#"), key.Length - key.LastIndexOf("]"));
        }
    }
    /// <summary>
    ///模版数据替换集合
    /// </summary>
    public class DataConvertorInfo
    {
        public string Key{get;set;}
        public object Value{get;set;}
    }
}
