using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.IO;
using System.Data;

namespace czy.Web.Controls
{
    public class DataGrid
    {
        string cssPath=AppDomain.CurrentDomain.BaseDirectory+"css/datagrid.css";
        int[] columnWidths=new int[0];
        DataSet _ds;
        string _FunctionName = "GetRequestData";
        AjaxParams _ap;
        public   AjaxParams AjaxParams
        {
            get { return _ap; }
            set { _ap = value; }
        }
        public DataSet DataSet
        {
            get { return _ds; }
            set { _ds = value; }
        }
        public string CssPath
        {
            get { return cssPath; }
            set { cssPath = value; }
        }
        public int[] ColumnWidths
        {
            get { return columnWidths; }
            set { columnWidths = value; }
        }
     
        public  string FunctionName
        {
            set { value = _FunctionName; }
            get { return _FunctionName; }
        }
        public DataGrid(DataSet ds)
        {
            _ds = ds;
        }
        public DataGrid()
        {
        }
        public  string  AjaxLoad(AjaxParams ap)
        {
            _ap = ap;
            return AjaxLoad();
        }
        public string AjaxLoad()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("var currentPage=1;");
            sb.AppendLine("function " + _FunctionName + "(params) {");
            sb.AppendLine("new czyjs.Ajax.Request(\"" + _ap.URL + "\", { Params: params, Method: \"" + _ap.Mathod + "\", Ansy: " + _ap.Ansy.ToString().ToLower() + ", AnsyCallBack:" + _ap.CallBack + "});");
            sb.AppendLine("}");
            sb.AppendLine("" + _FunctionName + "('currentPage=1&order=');");
            sb.AppendLine("</script>");
            return sb.ToString();
        }
        public string ResponseBody()
        {

            CreateCssFile(ResonseCss(), cssPath);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<table class='grid'>");
            foreach (DataRow dr in _ds.Tables[0].Rows)
            {
                sb.AppendLine( GetRow( dr,columnWidths));
            }
            sb.AppendLine("</table>");
            return sb.ToString();
        }
        /// <summary>
        /// 生成行[table行tr]
        /// </summary>
        /// <param name="dr">DataRow</param>
        /// <returns></returns>
        public virtual string GetRow(DataRow dr,int[] columnWidths)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<tr>");
            int i = 0;
            foreach (DataColumn dc in _ds.Tables[0].Columns)
            {
                sb.AppendLine("<td " + (i < columnWidths.Length ? " style='width:" + columnWidths[i].ToString() + "px'" : "") + ">");
                sb.AppendLine(dr[dc.ColumnName].ToString ());
                sb.AppendLine("</td>");
                i++;
            }
            sb.AppendLine("</tr>");
            return sb.ToString();
        }
        public String ResponseHead()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<ul style='list-style-type:none;margin:0px;' class='toolbar'>");
            sb.AppendLine(GetColumns(_ds));
            sb.AppendLine("</ul>");
            return sb.ToString();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="columnName">显示的列</param>
        /// <returns></returns>
        public String ResponseHead(string[] columnName)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("<script type=\"text/javascript\">");
            sb.AppendLine("var currentPage=1;");
            sb.AppendLine("function " + _FunctionName + "(params) {");
            sb.AppendLine("new czyjs.Ajax.Request(\"" + _ap.URL + "\", { Params: params, Method: \"" + _ap.Mathod + "\", Ansy: " + _ap.Ansy.ToString().ToLower() + ", AnsyCallBack:" + _ap.CallBack + "});");
            sb.AppendLine("}");
            sb.AppendLine("</script>");

            sb.AppendLine("<ul style='list-style-type:none;margin:0px;' class='toolbar'>");
            sb.AppendLine(GetColumns(_ds, columnName));
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
                sb.AppendLine("<span>" + dc.ColumnName + "</span>");
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
        public virtual string GetColumns(DataSet ds, string[] columnName)
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
    

        public string ResonseCss()
        {
            StringBuilder sb=new StringBuilder ();
            sb.AppendLine(".grid{border-collapse:collapse; line-height:20px; }");
            sb.AppendLine(".grid td{ border:solid 1px #60a33a}");
            return sb.ToString();
            
        }

        public void CreateCssFile(string cssText,string path)
        {
            if (!File.Exists(path))
            {
               FileStream fs= File.Create(path);
               StreamWriter sw = new StreamWriter(fs);
               sw.Write(ResonseCss());
               sw.Close();
               fs.Close();
            }
        }
    }
}
