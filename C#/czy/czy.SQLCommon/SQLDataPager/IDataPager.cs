using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace czy.SQLAccess.DataPager
{
    /// <summary>
    /// 数据分类接口
    /// </summary>
    public interface IDataPager
    {
        /// <summary>
        /// 当前页
        /// </summary>
        long CurrentPageIndex { get; set; }
        /// <summary>
        /// 页的大小
        /// </summary>
        int Size { get; }
        /// <summary>
        /// 数据内容
        /// </summary>
        DataTable DataTable { get; }
        /// <summary>
        /// 总条数
        /// </summary>
        long TotalCount { get;  }
        /// <summary>
        /// 总页数
        /// </summary>
        long PageCount { get; }
        /// <summary>
        /// 获取总页条数
        /// </summary>
        /// <returns></returns>
        long GetTotalCount();
        /// <summary>
        /// 获取当页数据
        /// </summary>
        /// <param name="cur">当前页</param>
        /// <returns>当前面内容</returns>
        DataSet GetCurrentPageData(long cur);
    }
}
