using System;

namespace JuSNS.Model
{
    /// <summary>
    /// SQL查询时所用的条件
    /// </summary>
    public class SqlConditionInfo
    {
        private string _pname;
        private object _pvalue;
        private TypeCode _ptype;
        private int _blur = 0;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="paramname">参数名称</param>
        /// <param name="paramvalue">参数的值</param>
        /// <param name="paramtype">参数类型</param>
        public SqlConditionInfo(string paramname, object paramvalue, TypeCode paramtype)
        {
            _pname = paramname;
            _pvalue = paramvalue;
            _ptype = paramtype;
        }
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamName
        {
            get { return _pname; }
        }
        /// <summary>
        /// 参数的值
        /// </summary>
        public object ParamValue
        {
            get { return _pvalue; }
        }
        /// <summary>
        /// 参数类型
        /// </summary>
        public TypeCode ParamType
        {
            get { return _ptype; }
        }
        /// <summary>
        /// 1表示前面模糊,2表示后模糊,3表示前后模糊,其余的数字表示精确查询(默认项)
        /// </summary>
        public int Blur
        {
            set { _blur = value; }
            get { return _blur; }
        }

    }
}
