using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace czy.Wpf.Library.Control
{
    /// <summary>
    /// 分页实体类
    /// </summary>
    public class Pager
    {
        #region Private Member

        private int pageIndex = 1;    //当前页，默认第一页
        private int pageSize = 20;    //页大小，默认20条目
        private int pageCount;      //总共页
        private int recorderCount;  //总共条目
       

        #endregion

        #region Public Attribute
        public event EventHandler ChangePropty;
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value;  }
        }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; if (ChangePropty != null)ChangePropty(this, new EventArgs()); }
        }

        /// <summary>
        /// 总共页
        /// </summary>
        public int PageCount
        {
            get
            {
                return recorderCount % pageSize > 0 ?
                    (recorderCount / pageSize) + 1 :
                    (recorderCount / pageSize);
            }
        }

        /// <summary>
        /// 总共条目数
        /// </summary>
        public int RecorderCount
        {
            get { return recorderCount; }
            set { recorderCount = value; if (ChangePropty != null)ChangePropty(this,new EventArgs ()); }
        }

        #endregion

        #region Initiliazation

        /// <summary>
        /// 无参构造
        /// </summary>
        public Pager() { }

        /// <summary>
        /// 有参构造
        /// </summary>
        /// <param name="pageSize"></param>
        public Pager(int pageSize, int recorderCount)
        {
            this.pageSize = pageSize;
            this.recorderCount = recorderCount;
        }

        #endregion
    }

}
