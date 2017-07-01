using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Web.UI.HtmlControls;

namespace czy.Web.Controls
{
    public class GridToolBar
    {
        DataSet _ds;
        AjaxParams ap;
        string _FunctionName = "GetRequestData";
        public AjaxParams AjaxParams
        {
            get { return ap; }
            set { ap = value; }
        }
        public string FunctionName
        {
            set { value = _FunctionName; }
            get { return _FunctionName; }
        }
        public DataSet DataSet
        {
            get { return _ds; }
            set { _ds = value; }
        }
        public GridToolBar()
        {
            ap = new AjaxParams();
        }
        public String Response(DataSet ds)
        {
            _ds = ds;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<ul style='list-style-type:none;margin:0px;' class='toolbar'>");
            sb.AppendLine(GetColumns(ds));
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="columnName">显示的列</param>
        /// <returns></returns>
        public String Response(DataSet ds, string[] columnName)
        {
            _ds = ds;
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("var currentPage=1;");
            sb.AppendLine("function " + _FunctionName + "(params) {");
            sb.AppendLine("new czyjs.Ajax.Request(\"" + ap.URL + "\", { Params: params, Method: \"" + ap.Mathod + "\", Ansy: " + ap.Ansy.ToString().ToLower() + ", AnsyCallBack:" + ap.CallBack + "});");
            sb.AppendLine("}");
            sb.AppendLine("</script>");

            sb.AppendLine("<ul style='list-style-type:none;margin:0px;' class='toolbar'>");
            sb.AppendLine(GetColumns(ds, columnName));
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
        public virtual string GetColumns(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {

                sb.AppendLine("<li style='cursor:pointer;' onclick=\"" + _FunctionName + "('currentPage='+currentPage+'&order='+this.childNodes[0].value)\">");
                sb.AppendLine("<input id='" + dc.ColumnName + "_order' type='hidden' value='" + dc.ColumnName + " asc" + "'></input>");
                sb.AppendLine("<span>"+dc.ColumnName+"</span>");
                sb.AppendLine("</li>");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="columnName">显示的列</param>
        /// <returns></returns>
        public virtual string GetColumns(DataSet ds,string[] columnName)
        {
            StringBuilder sb = new StringBuilder();
           
            foreach (DataColumn dc in ds.Tables[0].Columns)
            {
                if (columnName.Equals(dc.ColumnName))
                {
                    sb.AppendLine("<li style='cursor:pointer;' onclick=\"" + _FunctionName + "('currentPage='+currentPage+'&order='+this.childNodes[0].value\">");
                    sb.AppendLine("<input  id='" + dc.ColumnName + "_order' type='hidden' value='" + dc.ColumnName + ",asc" + "'></input>");
                    sb.AppendLine("<span>" + dc.ColumnName + "</span>");
                    sb.AppendLine("</li>");
                }
            }
            return sb.ToString();
        }

    }
}
