using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web;

namespace czy.SQLAccess.DataPager
{
    /// <summary>
    /// ajax输出
    /// </summary>
    public class AjaxaGetDataServer
    {
        #region 成员

        public enum resType
        {
            xml,
            str
        }


        int currentPage = 0;
        /// <summary>
        /// 0为第一页
        /// </summary>
        public int CurrentPage
        {
            get { return currentPage; }
            set { currentPage = value; }
        }
        int totalPage = 0;

        public int TotalPage
        {
            get { return totalPage; }
        }
        int totalCount = 0;
        /// <summary>
        /// 总项数
        /// </summary>
        public int TotalCount
        {
            get { return totalCount; }
        }

        DataSet _ds;
        public DataSet Ds
        {
            get { return _ds; }
        }
        int _pageSize=5;
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
        }

        #endregion

        public AjaxaGetDataServer(DataSet  ds, int pageSize)
        {
            totalCount = ds.Tables [0].Rows.Count;
            if (totalCount % pageSize == 0)
            {
                totalPage = totalCount / pageSize;
            }
            else
            {
                totalPage = totalCount / pageSize+1;
            }
            _pageSize = pageSize;
            _ds = ds;
        }

       
        /// <summary>
        /// 获取当页的内容
        /// </summary>
        /// <param name="current_Page">当前页</param>
        /// <returns>当页内容table</returns>
        public DataSet GetCurrentPageData(int current_Page)
        {
            string html = string.Empty;
            DataSet ds1 = _ds.Copy();
            ds1.Tables[0].Clear();
            for (int i = current_Page * _pageSize; i < (current_Page + 1) * _pageSize; i++)
            {
                if (i + 1 > totalCount)
                {
                    continue;
                }
                DataRow dr = ds1.Tables[0].NewRow();
                for (int j = 0; j < _ds.Tables[0].Columns.Count; j++)
                {
                    dr[j] = _ds.Tables[0].Rows[i].ItemArray[j];
                }
                ds1.Tables[0].Rows.Add(dr);
            }
            currentPage = current_Page;
            return ds1;
        }
        /// <summary>
        /// 返回传进来的条件参数
        /// </summary>
        /// <param name="Cols">变量名</param>
        /// <param name="page">当前页</param>
        /// <returns>返回一维数组,第0个是变量名,第1个是值</returns>
        public string[] GetRequestString(string Cols)
        {

            if (HttpContext.Current.Request.QueryString[Cols] != null)
            {
                if (HttpContext.Current.Request.QueryString[Cols] != string.Empty)
                {
                    return new string[] { Cols, HttpContext.Current.Request.QueryString[Cols].ToString() };
                }
                else
                {
                    return new string[0];
                }
            }
            else { 
            
                return new string[0];
            }
        }
        /// <summary>
        /// 获取XML
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="type">输出类型</param>
        /// <param name="ds">数据集合</param>
        public void ResponseData(resType type)
        {
            switch (type)
            {
                case resType.xml:
                    HttpContext.Current.Response.Charset = "UTF-8";
                    HttpContext.Current.Response.ContentType="text/xml";
                    HttpContext.Current.Response.Expires=60;
                    HttpContext.Current.Response.ContentType = @"text/xml";
                    HttpContext.Current.Response.Write(@"<?xml version='1.0' encoding='utf-8' ?>");
                    HttpContext.Current.Response.Write(_ds.GetXml());
                    break;
                case resType.str: HttpContext.Current.Response.Write(_ds.GetXml ());
                    break;
                default:
                    HttpContext.Current.Response.Charset = "UTF-8";
                    HttpContext.Current.Response.ContentType="text/xml";
                    HttpContext.Current.Response.Expires = 60;
                    HttpContext.Current.Response.ContentType = @"text/xml";
                    HttpContext.Current.Response.Write(@"<?xml version='1.0' encoding='utf-8' ?>");
                    HttpContext.Current.Response.Write(_ds.GetXml());
                    break;
           }

        }

        /// <summary>
        /// 获取XML
        /// </summary>
        /// <param name="page">当前页</param>
        /// <param name="type">输出类型</param>
        /// <param name="ds">数据集合</param>
        public void ResponseCurrentPageData(resType type, int current_page)
        {
            DataSet ds = GetCurrentPageData(current_page);
            DataTable dt = new DataTable("PageTable");
            dt.Columns.Add(new DataColumn("TotalPage"));
            dt.Columns.Add(new DataColumn("TotalCount"));
            dt.Columns.Add(new DataColumn("PageSize"));
            dt.Columns.Add(new DataColumn("CurrentPage"));
            DataRow dr = dt.NewRow();
            dr["TotalPage"] = totalPage;//总页
            dr["TotalCount"] = totalCount;//总条数
            dr["PageSize"] = _pageSize;//一页的条数
            dr["CurrentPage"] = current_page;//当前页
            dt.Rows.Add(dr);
            ds.Tables.Add(dt);
            //int count = ds.Tables.Count;
            switch (type)
            {
                case resType.xml: HttpContext.Current.Response.ContentType = "text/xml";
                    HttpContext.Current.Response.Write("<?xml version='1.0' encoding='utf-8' ?>");
                    string xml = ds.GetXml();
                    HttpContext.Current.Response.Write(xml);
                    break;
                case resType.str: HttpContext.Current.Response.Write(ds.GetXml());
                    break;
                default: HttpContext.Current.Response.Write(ds.GetXml());
                    break;
            }
            currentPage = current_page;

        }
    }
}
