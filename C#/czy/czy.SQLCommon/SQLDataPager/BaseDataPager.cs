using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using czy.MyDAL;

namespace czy.SQLAccess.DataPager
{
    public abstract class BaseDataPager
    {

        #region  私有成员

        protected long _totalCount = 0;
        protected long _cur = 1;
        protected int _size = 10;
        protected string _keyFieldOrder = "asc";
        protected string _order;
        protected string _clos = "*";
        protected string _procName;
        protected DataTable _dt;
        protected string _tableId;
        protected string _tableName;
        protected string _conn;
    
        #endregion

        #region 属性
        /// <summary>
        /// 当前页
        /// </summary>
        public long CurrentPageIndex
        {
            get { return _cur; }
            set { _cur = value; }
        }
        /// <summary>
        /// 页的大小
        /// </summary>
        public int Size
        {
            get { return _size; }
        }
        /// <summary>
        /// 数据内容
        /// </summary>
        public long TotalCount
        {
            get { return _totalCount; }
        }
        /// <summary>
        /// 数据内容
        /// </summary>
        public DataTable DataTable
        {
            get { return _dt; }
        }

        /// <summary>
        /// 总页数 注:(需先获得总条数)
        /// </summary>
        public long PageCount
        {
            get
            {
                if (this._size == 0) { return 0; }
                return _totalCount % this._size == 0 ? _totalCount / this._size : (_totalCount / this._size) + 1;
            }
        }

        #endregion
    }
}
