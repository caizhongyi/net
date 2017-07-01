using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;

namespace czy.Web.Controls
{
    /// <summary>
    /// 分页
    /// </summary>
    public class DataPager
    {
        #region 属性
        int _currentPage = 0;
        /// <summary>
        /// Convert.ToInt32(Request.QueryString["page"])
        /// </summary>
        public int CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value; }
        }
        int _pageSize = 10;
        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        int _totalPage = 0;
        /// <summary>
        /// 总页
        /// </summary>
        public int TotalPage
        {
            get { return _totalPage; }
            set { _totalPage = value; }
        }
        int _totalCount = 0;
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount
        {
            get { return _totalCount; }
            set { _totalCount = value; }
        }
        bool _showNumberLabel = true;
        /// <summary>
        /// 是否显示页的相关信息
        /// </summary>
        public bool ShowNumberLabel
        {
            get { return _showNumberLabel; }
            set { _showNumberLabel = value; }
        }
        DataSet _dataSet;

        public DataSet DataSet
        {
            get { return _dataSet; }
            set { _dataSet = value; }
        }

        string _pagerCssStype = "datapager";
        /// <summary>
        /// 非当前页样式名称
        /// </summary>
        public string PagerCssStype
        {
            get { return _pagerCssStype; }
            set { _pagerCssStype = value; }
        }
        string _currentCssStyle = "current";
        /// <summary>
        /// 当前页样式名称
        /// </summary>
        public string CurrentCssStyle
        {
            get { return _currentCssStyle; }
            set { _currentCssStyle = value; }
        }
        string _normalCssStyle = "normal";
        /// <summary>
        /// 非当前页样式名称
        /// </summary>
        public string NormalCssStyle
        {
            get { return _normalCssStyle; }
            set { _normalCssStyle = value; }
        }
        #endregion

        int _parentPageSize = 10;//显示页的个数
        int _parentCurrentPage = 0;//页的当前值
        int _parentTotalPage = 0;//页的总值
        int _parentTotalCount = 0;//页的总数

        public DataPager(int totalCount)
        {
            _totalCount = totalCount;
            init();
        }
        public DataPager(DataSet ds,int totalCount)
        {
            _dataSet = ds;
            _totalCount = totalCount;
            init();
        }

        public virtual  string GetPagerNumbers()
        {
            StringBuilder sb = new StringBuilder();
            if (_showNumberLabel)
            {
                sb.Append("<span>第&nbsp;<font color='red'>" + _currentPage + "</font>&nbsp页&nbsp;/&nbsp;共&nbsp;<font color='red'>" + _totalPage + "</font>&nbsp页</span><span>共&nbsp;<font color='red'>" + _totalCount + "</font>&nbsp条</span>");
            }
            return sb.ToString();

        }

        public string GetHTMLPager(string pageName, string param)
        {
            this._currentPage =HttpContext.Current.Request.QueryString["page"]!=null? Convert .ToInt32( HttpContext.Current.Request.QueryString["page"]):1;
            bool disable = true;
            StringBuilder sb = new StringBuilder();
            param = param == string.Empty ? string.Empty : "&" + param;
            _parentCurrentPage=GetParentCurrentPage(_currentPage);
            sb.AppendFormat("<div class='" + _pagerCssStype + "'>");
            if (_showNumberLabel)
            {
                sb.Append(GetPagerNumbers());
            }
            
            disable= IsDisable(_currentPage,_totalPage,0);
            sb.AppendFormat("<a " + GetHref(disable) + " target='_self' style='" + SetStyle(disable) + "'>{3}</a>", pageName, 0, param, "首页");
           
            
            disable= IsDisable(_currentPage,_totalPage,1);
            sb.AppendFormat("<a " + GetHref(disable) + " target='_self'  style='" + SetStyle(disable) + "'>{3}</a>", pageName, _currentPage - 1, param, "上一页");

            disable = IsDisable(_currentPage, _totalPage, 4);
            if (!disable)
                sb.AppendFormat("<a  href='{0}?page=0{1}{2}' target='_self'  style='" + SetStyle(disable) + "'>{3}</a>", pageName, _currentPage - _pageSize, param, "...");
          
            for (int i = GetParentFirstPage(); i < GetParentLastPage(); i++)
            {
                string cssStyle = i + 1 == _currentPage ? _currentCssStyle : _normalCssStyle;
                sb.AppendFormat("<a href='{0}?page=0{1}{2}' target='_self' style='cursor:pointer;' class='" + cssStyle + "'>{3}</a>", pageName, i+1, param, i + 1);
            }
            disable = IsDisable(_currentPage, _totalPage, 5);
            if (!disable)
                sb.AppendFormat("<a  href='{0}?page=0{1}{2}' target='_self'  style='" + SetStyle(disable) + "'>{3}</a>", pageName, _currentPage + _pageSize, param, "...");
            
            disable= IsDisable(_currentPage,_totalPage,2);
            sb.AppendFormat("<a " + GetHref(disable) + " target='_self'  style='" + SetStyle(disable) + "'>{3}</a>", pageName, _currentPage + 1, param, "下一页");
           
            disable = IsDisable(_currentPage, _totalPage, 3);
            sb.AppendFormat("<a " + GetHref(disable) + " target='_self'  style='" + SetStyle(disable) + "'>{3}</a>", pageName, _totalPage, param, "尾页");
            sb.AppendFormat("</div>");
            return sb.ToString();
        }

        private void init()
        {
            _totalPage = _totalCount % _pageSize == 0 ? _totalCount / _pageSize : (_totalCount / _pageSize) + 1;
            _parentTotalPage = _totalPage % _parentPageSize == 0 ? _totalPage / _parentPageSize : (_totalPage / _parentPageSize) + 1;
            _parentCurrentPage=GetParentCurrentPage(_currentPage);
        }

        #region helper

        private int GetParentCurrentPage(int currentPage)
        {
            return (currentPage ) / _pageSize;
        }
        /// <summary>
        /// 获取显示页的开始值
        /// </summary>
        /// <returns></returns>
        private int GetParentFirstPage()
        {
            return _parentCurrentPage * _parentPageSize;
        }
        /// <summary>
        /// 获取显示页的结束值
        /// </summary>
        /// <returns></returns> 
        private int GetParentLastPage()
        {
            int last=(_parentCurrentPage+1) * _parentPageSize;
            return last > _totalPage ? _totalPage : last;
        }

        private string GetHref(bool disable)
        {
            return disable ? string.Empty : "href='{0}?page=0{1}{2}'";
        }
        private bool IsDisable(int currentPage,int totalPage,int type)
        {
            switch (type)
            {
                case 0: return currentPage <= 1 ? true : false;
                case 1: return currentPage <= 1 ? true : false;
                case 2: return currentPage >= totalPage ? true : false;
                case 3: return currentPage >= totalPage ? true : false;
                case 4: return currentPage <= _pageSize ? true : false;
                case 5: return currentPage > totalPage - _pageSize ? true : false;
                default: return false ;
            }
        }
        private string SetStyle(bool isDisable)
        {
           return isDisable?string .Empty :"cursor:pointer;";
        }
        #endregion
    }
}
