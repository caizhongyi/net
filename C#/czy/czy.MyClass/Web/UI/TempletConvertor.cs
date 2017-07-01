using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace czy.MyClass.Web.UI
{

    public class TempletConverterInfo
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public TemplateConvertor.TemplateType TemplateType { get; set; }
    }
    /// <summary>
    /// HTML模版读取
    /// </summary>
    public class TemplateConvertor : HTMLReader
    {
        public TemplateConvertor()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        public enum TemplateType
        {
            Template,
            Label
        }

        #region 生成替换后的新的HTML页

        /// <summary>
        /// 生成替换后的新的HTML页[UTF8编码]
        /// </summary>
        /// <param name="path">所在的模版路径</param>
        /// <param name="key">替换的字符</param>
        /// <param name="value">替换后的字符</param>
        /// <param name="type">替换的类型</param>
        /// <returns></returns>
        public static string ReplaceTemplate(string path, string key, string html, TemplateType type)
        { 
            string modelString= StreamReadHTML(path);
            switch (type)
            {
                case TemplateType.Template:
                    html = html.Replace("{#Template*" + key + "#}", modelString); 
                    break;
                case TemplateType.Label:
                    html = html.Replace("{#Label*" + key + "#}", modelString);
                    break;
                default: html = html.Replace("{#Template*" + key + "#}", modelString); 
                    break;
            }
            return html;
        }
        public static string ReplaceData(string html,string key, string value)
        {
            //string modelString = html;
            html = html.Replace("{#Data*" + key + "#}", value);
            return html;
        }
        /// <summary>
        /// 生成替换后的新的HTML页[UTF8编码]
        /// </summary>
        /// <param name="path">所在的模版路径</param>
        /// <param name="list">TemplateReaderInfo对像集合</param>
        /// <returns></returns>
        public static string ReplaceTemplate(string path, List<TempletConverterInfo> list)
        {
            string modelString = StreamReadHTML(path);
            foreach (TempletConverterInfo t in list)
            {
                modelString = ReplaceTemplate(path, t.Key, t.Value, t.TemplateType);
            }
            return modelString;
        }
        #endregion
        /// <summary>
        /// Web.config appsetting 内容替换
        /// </summary>
        /// <param name="content">需替换的内容</param>
        /// <param name="configKey">config Key值</param>
        /// <param name="replaceKey">需替换的字符</param>
        /// <returns></returns>
        public static string ReadConfig(string content,string configKey,string replaceKey)
        {
            
            return content.Replace(replaceKey, System.Web.Configuration.WebConfigurationManager.AppSettings[configKey]);
        }
        /// <summary>
        /// DataSet生成HTML列表(此HTML父级元素为UL元素)
        /// </summary>
        /// <param name="ds">数据表</param>
        /// <param name="herf">所在链接herf可为空或null时</param>
        /// <param name="visIdColumn">id列是否显示</param>
        /// <param name="idColumn">id所在的列</param>
        /// <param name="param">其它参数无则为空</param>
        /// <param name="rowClassName">行的样式名样[UL标签]</param>
        /// <returns>HTML列表.</returns>
        public static string TableConvertToHTML(DataSet ds,string herf,int idColumn,bool visIdColumn,string param,string rowClassName)
        {
            StringBuilder sb = new StringBuilder();
            int j=0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {

                sb.AppendLine("<li>");
                if (ds.Tables[0].Columns.Count == 0)
                {
                    sb.AppendLine(dr[0].ToString());
                }
                else
                {
                    string hr = "";
                    if (string.IsNullOrEmpty(param))
                    {
                        hr ="onclick=\"window.open('"+ herf + "?id=" + ds.Tables[0].Rows[j][idColumn]+"','_self')\"";
                    }
                    else
                    {
                        hr = "onclick=\"window.open('" + herf + "?id=" + ds.Tables[0].Rows[j][idColumn] + "&" + param+ "','_self')\"";
                    }
                    sb.AppendLine("<li><ul style='margin:0px;list-style-type:none;' " + herf + " className='" + rowClassName + "'>");
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        if (visIdColumn)
                        {
                            sb.AppendLine("<li>");
                            sb.AppendLine(dr[i].ToString());
                            sb.AppendLine("</li>");
                        }
                        else
                        {
                            if (i != idColumn)
                            {
                                sb.AppendLine("<li>");
                                sb.AppendLine(dr[i].ToString());
                                sb.AppendLine("</li>");
                            }
                            
                        }
                    }
                    sb.AppendLine("</ul></li>");
                }
                sb.AppendLine("</li>");
                j++;
            }
            return sb.ToString();
        }
    
        /// <summary>
        /// DataSet生成Grid头列表HTML(此HTML父级元素为UL元素)
        /// </summary>
        /// <param name="ds">数据表</param>
        /// <param name="herf">所在链接herf可为空或null时</param>
        /// <param name="idColumn">id所在的列</param>
        /// <param name="param">其它参数无则为空</param>
        /// <returns>HTML列表.</returns>
        public static string ListConvertToHTML(DataSet ds, string herf, int idColumn, string param)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                string h = string.Empty;
                if(string .IsNullOrEmpty(param))
                {
                   h = string.IsNullOrEmpty(herf) ? string.Empty : "herf='" + herf + "?id=" + ds.Tables[0].Rows[0] + "'";
                }
                else
                {
                   h = string.IsNullOrEmpty(herf) ? string.Empty : "herf='" + herf + "?id=" + ds.Tables[0].Rows[0] + "'&" +param;
                }
                sb.AppendLine("<li>");
                sb.AppendLine("<a "+h+">");
                sb.AppendLine(dr[0].ToString ());
                sb.AppendLine("<a>");
                sb.AppendLine("</li>");

            }
            return sb.ToString();
        }
       
    }
}
