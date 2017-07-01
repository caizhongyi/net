using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web.UI;
using czy.MyDAL;

namespace czy.SQLAccess.DataPager
{
    /// <summary>
    /// Access分页
    /// </summary>
    public  class OleDbPagerHelper :BaseDataPager, IDataPager
    {
        protected IDataBase idb;

        public OleDbPagerHelper(DataPagerQueryParams queryParams,DataPagerDALParams dalParams)
        {
            idb = new OleDbDataBase(dalParams.ConnString, dalParams.ConnStringType);

            this._clos = queryParams.Colums;
            this._keyFieldOrder = queryParams.KeyFieldOrder;
            this._tableId = queryParams.TableId;
            this._tableName = queryParams.TableName;
            this._conn = queryParams.Filter;
            this._size = queryParams.Size;
            this._order = queryParams.Order;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="cur">当前页(1为第一页)</param>
        /// <param name="clos">显示的列[clo1,clo2.. ]或.[*]</param>
        /// <param name="tableId">表ID</param>
        /// <param name="tableName">表名</param>
        /// <param name="keyFieldOrder">表ID排序方式:[desc或asc]</param>
        /// <param name="condition">条件[ [clos1=value]|Empty]</param>
        /// <param name="size">页大小</param>
        /// <param name="orderParam">排序 [[clos] desc/asc] | Empty</param>
        /// <param name="conn">链接字符窜</param>
        /// <param name="type">字符窜的类型</param>
        public OleDbPagerHelper(string clos, string tableId, string tableName,string keyFieldOrder, string condition, int size, string orderParam, string conn,DataBase.ConnStringType type)
        {
            idb = new OleDbDataBase(conn, type);

            this._clos = clos;
            this._keyFieldOrder = keyFieldOrder;
            this._tableId = tableId;
            this._tableName = tableName;
            this._conn = condition;
            this._size = size;
            this._order = orderParam;
            this._conn = conn;
        }

        /// <summary>
        /// 非sql2005以上版本使用
        /// </summary>
        /// <param name="cur">当前页</param>
        /// <param name="totalCount">发返总页数</param>
        /// <returns>当前页内容</returns>
        public DataSet GetCurrentPageData(long cur)
        {
            this._cur = cur;
            string sql = GetSQLCommandString();
            DataSet ds = idb.GetDataSet(sql);
            _dt = ds.Tables[0];
            return ds;
        }

        /// <summary>
        /// 返回SQL分页字符窜
        /// </summary>
        /// <returns>返回字符窜数组string[0]为查询数据，string[1]为查询总条数</returns>
        public string GetSQLCommandString()
        {

            string s = string.Empty;

            StringBuilder sql = new StringBuilder();


            string condition = string.IsNullOrEmpty(this._conn) ? string.Empty : " where " + this._conn;
            string order = string.IsNullOrEmpty(this._order) ? string.Empty : "," + this._order;

            if (this._cur == 1)
            {
                sql.AppendFormat(" select top {0} {1} from {2} order by {4} {3}", this._size, this._clos, this._tableName, this._keyFieldOrder, this._tableId);
 
            }
            else
            {
                if (this._keyFieldOrder.Trim().ToLower() == "asc")
                {
                    sql.AppendFormat(" select top {0} {1} from {2} ", this._size, this._clos, this._tableName);
                    sql.AppendFormat(" where {0}> ", this._tableId);
                    sql.AppendFormat(" (select max({0}) from ", this._tableId);
                    sql.AppendFormat(" (select top {0} {1} from {2} {3} order by {1} asc) as T", (this._cur-1) * this._size, this._tableId, this._tableName, condition);
                    sql.AppendFormat(" ) {0} order by {1} asc {2}", condition, this._tableId, order);
                }
                else
                {
                    sql.AppendFormat(" select top {0} {1} from {2} ", this._size, this._clos, this._tableName);
                    sql.AppendFormat(" where {0}< ", this._tableId);
                    sql.AppendFormat(" (select min({0}) from ", this._tableId);
                    sql.AppendFormat(" (select top {0} {1} from {2} {3} order by {1} desc) as T", (this._cur - 1) * this._size, this._tableId, this._tableName, condition);
                    sql.AppendFormat(" ) {0} order by {1} desc {2}", condition, this._tableId, order);
                }
            }

            s = sql.ToString();
            return s;
        }

        public string GetTotalCountSQLString()
        {
            string order = string.IsNullOrEmpty(this._order) ? string.Empty : " order by " + this._order;
            string query = " from " + this._tableName;
            string condition = string.IsNullOrEmpty(this._conn) ? string.Empty : " where " + this._conn;
            condition += order;
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("select count(*) as totalCount {0}", query + condition);
            return sb.ToString();
        }
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <returns>总条数</returns>
        public long GetTotalCount()
        {
            string sql = this.GetTotalCountSQLString();
            _totalCount= Convert.ToInt64(idb.ExecuteScalar(sql));
            return _totalCount;
        }
    

    }
}

