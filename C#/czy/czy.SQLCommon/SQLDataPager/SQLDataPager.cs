using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wuqi.Webdiyer;
using System.Data;
using System.Data.SqlClient;
using czy.MyDAL;
using czy.SQLAccess.CommandHelper;

namespace czy.SQLAccess.DataPager
{
    /// <summary>
    /// sql分页帮助类
    /// </summary>
    public  class SqlPagerHelper :BaseDataPager, IDataPager
    {
        #region 分页存储过程
        /*分页存储过程
         * set statistics time on --耗时
             Create Procedure [dbo].[P_GetPagerData]
              (
        @pagesize int, --每页的记录条数
        @pagenum int, --当前页面  1为第一页
        @QuerySql varchar(1000),--部分查询字符串,如 '  FROM t_Company where...'
        @keyId varchar(500), --列ID
        @keyIdOrder varchar(500), --ID列排序:desc/asc
        @order varchar(50),--order by [clos] desc/asc
        @clos varchar(50)--orderseq,companycode... 

        )
        AS
        Begin 
        Declare @SqlText AS Varchar(1000)
        declare @SqlText1 AS Varchar(1000)
        set @SqlText=' SELECT * '
        +' FROM (SELECT ROW_NUMBER() OVER(ORDER BY '+@keyId+' '+@keyIdOrder+', '+@keyId+') AS rownum,'+@clos +' '+ @QuerySql+' ) AS D '  
        +' WHERE rownum BETWEEN '+Cast((@pagenum-1)*(@pagesize+1) as varchar(10))+' AND '+Cast(@pagenum*@pagesize as varchar(10))+ '  '+@order +' '
        Exec(@SqlText)
      
        end
       go
        Create Procedure [dbo].[P_GetTotalsCount]
        @tableName varchar(50),
        @filter varchar(500)
        as
        begin
        Declare @SqlText AS Varchar(1000)
        set @SqlText='select count(*) as totalCount from ' + @tableName +' ' + @filter
        Exec(@SqlText)
        end
  
        */
        #endregion

        protected IDataBaseAdvance idba;

        #region 初始化
        /// <summary>
        /// 初始化
        /// </summary>
        public SqlPagerHelper(DataPagerQueryParams queryParams, DataPagerDALParams dalParams)
        {

            idba = new SQLDataBase(dalParams.ConnString, dalParams.ConnStringType);

            _procName = queryParams.ProcName;
            _clos = queryParams.Colums;
            _tableId = queryParams.TableId;
            _keyFieldOrder = queryParams.KeyFieldOrder;
            _tableName = queryParams.TableName;
            _size = queryParams.Size;
            _order = queryParams.Order;
            _conn = queryParams.Filter;

        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="closParam">显示的列[clos1,clos2.. ]或.[*]</param>
        /// <param name="keyField">表ID</param>
        /// <param name="keyFieldOrder">表ID排序方式:[desc或asc]</param>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件 [clos1=value]|Empty</param>
        /// <param name="size">页大小</param>
        /// <param name="orderParam">其它排序: [列名] desc/asc | Empty</param>
        /// <param name="connstr">链接字符窜</param>
        /// <param name="t">字符窜的类型</param>
        public SqlPagerHelper(string procName,string closParam, string keyField, string keyFieldOrder, string tableName, string condition, int size, string orderParam, string connstr, DataBase.ConnStringType t)
        {

            idba = new SQLDataBase(connstr, t);

            _procName = procName;
            _clos = closParam;
            _tableId = keyField;
            _keyFieldOrder = keyFieldOrder;
            _tableName = tableName;
            _conn = condition;
            _size = size;
            _order = orderParam;
            
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="closParam">显示的列[clo1,clo2.. ]或.[*]</param>
        /// <param name="keyField">表ID</param>
        /// <param name="keyFieldOrder">表ID排序方式:[desc或asc]</param>
        /// <param name="tableName">表名</param>
        /// <param name="condition">条件 [clos1=value]|Empty</param>
        /// <param name="size">页大小</param>
        /// <param name="orderParam">排序 [ [clos] desc/asc] | Empty</param>
        /// <param name="connstr">链接字符窜</param>
        /// <param name="t">字符窜的类型</param>
        public SqlPagerHelper(string closParam, string keyField, string keyFieldOrder, string tableName, string condition, int size, string orderParam, string connstr, DataBase.ConnStringType t)
        {
            idba = new SQLDataBase(connstr, t);

            _clos = closParam;
            _tableId = keyField;
            _keyFieldOrder = keyFieldOrder;
            _tableName = tableName;
            _conn = condition;
            _size = size;
            _order = orderParam;
            _conn = connstr;
        }

        #endregion

        #region sql分页2005



        /// <summary>
        /// sql分页
        /// </summary>
        /// <param name="cur">当前页为[1]第一页</param>
        /// <param name="totalCount">总条数</param>
        /// <returns>当前面内容</returns>
        public DataSet GetCurrentPageData(long cur)
        {
            try
            {
                string order = _order == string.Empty ? string.Empty : " order by " + _order;
                string query = " from " + _tableName;
                string condition = _conn == null ? string.Empty : " where " + _conn;
                if (!string.IsNullOrEmpty(_procName))
                {
                    _cur = cur;
                    string sql = _procName;
                    SqlParameter[] paramList = SQLProcParamBuilder.GetSQLParams(
                        new string[] { "@pagesize", "@pagenum", "@QuerySql", "@keyId", "@keyIdOrder", "@order", "@clos" },
                        new object[] { _size, _cur, query + condition, _tableId, _keyFieldOrder, order, _clos }
                    );
                    DataSet ds = idba.GetDataSet(sql, paramList);
                    _dt = ds.Tables[0];
                    return ds;
                }
                else
                {
                    _cur = cur;
                    string sql = GetSQLCommandStringBy2005();
                    DataSet ds = idba.GetDataSet(sql);
                    _dt = ds.Tables[0];
                    return ds;
                }
            }
            catch
            {
                _cur = cur;
                string sql = GetSQLCommandString();
                DataSet ds = idba.GetDataSet(sql);
                _dt = ds.Tables[0];
                return ds;
            }


        }
        /// <summary>
        /// 获取总条数
        /// </summary>
        /// <returns>总条数</returns>
        public long GetTotalCount()
        {
            string order = string.IsNullOrEmpty(_order) ? string.Empty : " order by " + _order;
            string condition = string.IsNullOrEmpty(_conn) ? string.Empty : " where " + _conn;
            condition += order;
            string sql = GetTotalCountSQLString();
            if (!string.IsNullOrEmpty(_procName))
            {
                SqlParameter[] paramList = CommandHelper.SQLProcParamBuilder.GetSQLParams(
                    new string[] { "@tableName", "@filter" },
                    new object[] { _tableName, condition }
                );
                 _totalCount= Convert.ToInt64(idba.ExecuteScalar(sql, paramList));
            }
            else
            {
                _totalCount= Convert.ToInt64(idba.ExecuteScalar(sql));
            }
            return _totalCount;
        }
        #endregion 

        #region 获得字符窜
        /// <summary>
        /// 返回SQL分页字符窜
        /// </summary>
        /// <returns>返回SQL分页字符窜</returns>
        public string GetSQLCommandString()
        {
            string s = string.Empty;

            StringBuilder sql = new StringBuilder();

            string condition = string.IsNullOrEmpty(_conn) ? string.Empty : " where " + _conn;
            string order = string .IsNullOrEmpty(_order)? string.Empty : "," + _order;

            if (_cur == 1)
            {
                sql.AppendFormat(" select top {0} {1} from {2} order by {4} {3}", _size, _clos, _tableName, _keyFieldOrder, _tableId);
    
            }
            else
            {
                if (_keyFieldOrder.Trim().ToLower() == "asc")
                {
                    sql.AppendFormat(" select top {0} {1} from {2} ", _size, _clos, _tableName);
                    sql.AppendFormat(" where {0}> ", _tableId);
                    sql.AppendFormat(" (select max({0}) from ", _tableId);
                    sql.AppendFormat(" (select top {0} {1} from {2} {3} order by {1} asc) as T", (_cur-1) * _size, _tableId, _tableName, condition);
                    sql.AppendFormat(" ) {0} order by {1} asc {2}", condition, _tableId, order);
                }
                else
                {
                    sql.AppendFormat(" select top {0} {1} from {2} ", _size, _clos, _tableName);
                    sql.AppendFormat(" where {0} < ", _tableId);
                    sql.AppendFormat(" (select min({0}) from ", _tableId);
                    sql.AppendFormat(" (select top {0} {1} from {2} {3} order by {1} desc) as T", (_cur - 1) * _size, _tableId, _tableName, condition);
                    sql.AppendFormat(" ) {0} order by {1} desc {2}", condition, _tableId, order);
                }
            }
            s = sql.ToString();
            return s;
        }
        /// <summary>
        /// 获取SQL查询字符
        /// </summary>
        /// <returns></returns>
        public string GetSQLCommandStringBy2005()
        {
            StringBuilder sql = new StringBuilder();
            if (!string.IsNullOrEmpty(_procName))
            {
                sql.Append(_procName);
            }
            else
            {
                string order = string.IsNullOrEmpty(_order) ? string.Empty : " order by " + _order;
                string query = " from " + _tableName;
                string condition = string.IsNullOrEmpty(_conn) ? string.Empty : " where " + _conn;
           
                sql.Append(" DECLARE @pagenum AS INT, @pagesize AS INT ");
                sql.Append(" SET @pagenum = " + _cur);
                sql.Append(" SET @pagesize = " + _size);
                sql.Append(" SELECT * ");
                sql.Append(" FROM (SELECT ROW_NUMBER() OVER(ORDER BY " + _tableId + " " + _keyFieldOrder + ", " + _tableId + ") AS ");
                sql.Append(" rownum, ");
                sql.Append(_clos + " " + query + condition + ") AS D  ");
                sql.Append(" WHERE rownum BETWEEN (@pagenum-1)*@pagesize+1 AND @pagenum*@pagesize  " + order);
            }
            return sql.ToString();
        }

        public string GetTotalCountSQLString()
        {
            string order = string.IsNullOrEmpty(_order) ? string.Empty : " order by " + _order;
            string query = " from " + _tableName;
            string condition = string.IsNullOrEmpty(_conn) ? string.Empty : " where " + _conn;
            condition += order;
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(_procName))
            {
                sb.Append(_procName);
            }
            else
            {
                sb.AppendFormat("select count(*) as totalCount {0}", query + condition);
            }
            return sb.ToString();
        }
        #endregion
    }
}
