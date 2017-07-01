using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using czy.MyDAL;

namespace czy.SQLAccess.DataPager
{
    public class DataPagerDALParams
    {
        /// <summary>
        /// 链接类型Config的key或字符型
        /// </summary>
        public DataBase.ConnStringType ConnStringType { get; set; }
        /// <summary>
        /// 链接字符窜
        /// </summary>
        public string ConnString { get; set; }
    }
    public class DataPagerQueryParams
    {
        /// <summary>
        /// 存储过程序名
        /// </summary>
        public string ProcName { get; set; }
        /// <summary>
        /// 查找的列名,全部则为*
        /// </summary>
        public string Colums { get; set; }
        /// <summary>
        /// 表ID
        /// </summary>
        public string TableId { get; set; }
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// ID列排序 [asc]或[desc]
        /// </summary>
        public string KeyFieldOrder { get; set; }
        /// <summary>
        /// 页大小
        /// </summary>
        public int Size { get; set; }
        /// <summary>
        /// 其它排序 [列名]+[desc]|[asc]
        /// </summary>
        public string Order { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public string Filter { get; set; }
    }
}
