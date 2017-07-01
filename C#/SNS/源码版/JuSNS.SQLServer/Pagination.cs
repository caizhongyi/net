using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using JuSNS.Factory;
using JuSNS.Model;
using JuSNS.Config;
using JuSNS.Profile;
using JuSNS.Common;

namespace JuSNS.SQLServer
{
    public class Pagination
    {
        /// <summary>
        /// 执行对默认数据库有自定义排序的分页的查询,返回DataTable
        /// </summary>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">表名和条件，至少有一个where 子句，如: " + Pre + "User where (1=1)，必须加上括号。但不要包含order by子句，也不要包含"from"关键字</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        static internal DataTable ProcPage(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            PageCount = 0;
            RecordCount = 0;
            SqlParameter[] param = GetParam(SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "NTP_Page", param);
            RecordCount = (int)param[6].Value;
            PageCount = (int)param[7].Value;
            return dt;
        }
        /// <summary>
        /// 执行对默认数据库有自定义排序的分页的查询
        /// </summary>
        /// <param name="cn">已打开的SqlConnection对象</param>
        /// <param name="SqlAllFields">查询字段，如果是多表查询，请将必要的表名或别名加上，如:a.id,a.name,b.score</param>
        /// <param name="SqlTablesAndWhere">表名和条件，至少有一个where 子句，如: " + Pre + "User where (1=1)，必须加上括号。但不要包含order by子句，也不要包含"from"关键字</param>
        /// <param name="IndexField">用以分页的不能重复的索引字段名，最好是主表的自增长字段，如果是多表查询，请带上表名或别名，如:a.id</param>
        /// <param name="OrderASC">排序方式,如果为true则按升序排序,false则按降序排</param>
        /// <param name="OrderFields">排序字段以及方式如：a.OrderID desc,CnName desc</OrderFields>
        /// <param name="PageIndex">当前页的页码</param>
        /// <param name="PageSize">每页记录数</param>
        /// <param name="RecordCount">输出参数，返回查询的总记录条数</param>
        /// <param name="PageCount">输出参数，返回查询的总页数</param>
        /// <returns>返回查询结果</returns>
        static internal DataTable ProcPage(SqlConnection cn, string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            PageCount = 0;
            RecordCount = 0;
            SqlParameter[] param = GetParam(SqlAllFields, SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize);
            DataTable dt = DbHelper.ExecuteTable(cn, CommandType.StoredProcedure, "NTP_Page", param);
            RecordCount = (int)param[6].Value;
            PageCount = (int)param[7].Value;
            return dt;
        }
        /// <summary>
        /// 取得参数
        /// </summary>
        /// <param name="SqlAllFields"></param>
        /// <param name="SqlTablesAndWhere"></param>
        /// <param name="IndexField"></param>
        /// <param name="OrderFields"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        static SqlParameter[] GetParam(string SqlAllFields, string SqlTablesAndWhere, string IndexField, string OrderFields, int PageIndex, int PageSize)
        {
            SqlParameter[] param = new SqlParameter[8];
            param[0] = new SqlParameter("@IndexField", SqlDbType.VarChar, 50);
            param[0].Value = IndexField;
            param[1] = new SqlParameter("@AllFields", SqlDbType.VarChar, 1000);
            param[1].Value = SqlAllFields;
            param[2] = new SqlParameter("@TablesAndWhere", SqlDbType.VarChar, 1000);
            param[2].Value = SqlTablesAndWhere;
            param[3] = new SqlParameter("@OrderFields", SqlDbType.VarChar, 255);
            param[3].Value = OrderFields;
            param[4] = new SqlParameter("@PageSize", SqlDbType.Int);
            param[4].Value = PageSize;
            param[5] = new SqlParameter("@PageIndex", SqlDbType.Int);
            param[5].Value = PageIndex;
            param[6] = new SqlParameter("@RecordCount", SqlDbType.Int);
            param[6].Direction = ParameterDirection.Output;
            param[7] = new SqlParameter("@PageCount", SqlDbType.Int);
            param[7].Direction = ParameterDirection.Output;
            return param;
        }
    }
}
