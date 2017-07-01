using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Data;

namespace czy.MyClass.Web
{
    /// <summary>
    /// 生成Excel报表
    /// </summary>
    public class ExportExcel
    {
        string filename;
        /// <summary>
        /// 保存的Excel文件名
        /// </summary>
        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
        DataSet ds = null;
        /// <summary>
        /// 数据
        /// </summary>
        public DataSet DataSet
        {
            get { return ds; }
            set { ds = value; }
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_fn">保存的Excel文件名</param>
        /// <param name="_ds">数据</param>
        public ExportExcel(string _fn, DataSet _ds)
        {
            //
            //TODO: 在此处添加构造函数逻辑
            //
            this.ds = _ds;
            this.filename = _fn;
           
        }
        public ExportExcel()
        {

        } 
        /// <summary>
        /// DataSet多表转化为Excel文件
        /// </summary>
        public void ExportCSV()
        {
            DataSet dataset = this.ds;
            StringWriter sw = new StringWriter();

            string cloumsHeader = string.Empty;
            string[] cloums = new string[dataset.Tables[0].Columns.Count];
            for (int i = 0; i < dataset.Tables[0].Columns.Count; i++)
            {
                cloums[i] = dataset.Tables[0].Columns[i].ToString();
            }
            cloumsHeader = string.Join(",", cloums);

            

            for (int i = 0; i < dataset.Tables.Count; i++)
            {

                DataTable dt = dataset.Tables[i];
                sw.WriteLine(dt.TableName);
                sw.WriteLine(cloumsHeader);

                foreach (DataRow dr in dt.Rows)
                {

                    string rowData = string.Empty;
                    for (int j = 0; j < dataset.Tables[0].Columns.Count; j++)
                    {
                        cloums[j] = dr[j].ToString();
                    }
                    rowData = string.Join(",", cloums);
                    sw.WriteLine(rowData);
                }
            }
            sw.Close();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + 
                HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ".csv\"");
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.Write(sw);
            HttpContext.Current.Response.End();
        }


        /// <summary>
        /// DataSet多表转化为Xls文件
        /// </summary>
        public void ExportXls()
        {
            DataSet dataset = this.ds;
            StringWriter sw = new StringWriter();

            string cloumsHeader = string.Empty;
            string[] cloums = new string[dataset.Tables[0].Columns.Count];
            for (int i = 0; i < dataset.Tables[0].Columns.Count; i++)
            {
                cloums[i] = dataset.Tables[0].Columns[i].ToString();
            }
            cloumsHeader = string.Join("\t", cloums);

           

            for (int i = 0; i < dataset.Tables.Count; i++)
            {

                DataTable dt = dataset.Tables[i];
                sw.WriteLine(dt.TableName);
                sw.WriteLine(cloumsHeader);
                foreach (DataRow dr in dt.Rows)
                {

                    string rowData = string.Empty;
                    for (int j = 0; j < dataset.Tables[0].Columns.Count; j++)
                    {
                        cloums[j] = dr[j].ToString();
                    }
                    rowData = string.Join("\t", cloums);
                    sw.WriteLine(rowData);
                }
            }
            sw.Close();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=\"" + 
                HttpUtility.UrlEncode(filename, System.Text.Encoding.UTF8) + ".xls\"");
            HttpContext.Current.Response.ContentType = "application/ms-excel";
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("GB2312");
            HttpContext.Current.Response.Write(sw);
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// DataSet多表转化为Xls文件
        /// </summary>
        /// </summary>
        /// <param name="path">保存径</param>
        public void ExportXls(string path)
        {
            DataSet dataset = this.ds;
            //StringWriter sw = new StringWriter();

            string name = path + DateTime.Today.ToString("yyyyMMdd") + new Random(DateTime.Now.Millisecond).Next(10000).ToString() + ".xls";//存放到web.config中downloadurl指定的路径，文件格式为当前日期+4位随机数 
            FileStream fs=new FileStream(name,FileMode.Create,FileAccess.Write); 
            StreamWriter sw=new StreamWriter(fs,System.Text.Encoding.GetEncoding("gb2312")); 


            string cloumsHeader = string.Empty;
            string[] cloums = new string[dataset.Tables[0].Columns.Count];
            for (int i = 0; i < dataset.Tables[0].Columns.Count; i++)
            {
                cloums[i] = dataset.Tables[0].Columns[i].ToString();
            }
            cloumsHeader = string.Join("\t", cloums);

         

            for (int i = 0; i < dataset.Tables.Count; i++)
            {

                DataTable dt = dataset.Tables[i];
                sw.WriteLine(dt.TableName);
                sw.WriteLine(cloumsHeader);

                foreach (DataRow dr in dt.Rows)
                {

                    string rowData = string.Empty;
                    for (int j = 0; j < dataset.Tables[0].Columns.Count; j++)
                    {
                        cloums[j] = dr[j].ToString();
                    }
                    rowData = string.Join("\t", cloums);
                    sw.WriteLine(rowData);
                }
            }
            sw.Close();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" +
                HttpContext.Current.Server.UrlEncode(name));
            HttpContext.Current.Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
            HttpContext.Current.Response.WriteFile(name); // 把文件流发送到客户端 
            HttpContext.Current.Response.End(); 
        }


        /// <summary>
        /// DataSet多表转化为CSV文件
        /// </summary>
        /// <param name="path">保存径</param>
        public void ExportCSV(string path)
        {
            DataSet dataset = this.ds;
            //StringWriter sw = new StringWriter();

            string name = path + DateTime.Today.ToString("yyyyMMdd") + new Random(DateTime.Now.Millisecond).Next(10000).ToString() + ".csv";//存放到web.config中downloadurl指定的路径，文件格式为当前日期+4位随机数 
            FileStream fs = new FileStream(name, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.GetEncoding("gb2312"));


            string cloumsHeader = string.Empty;
            string[] cloums = new string[dataset.Tables[0].Columns.Count];
            for (int i = 0; i < dataset.Tables[0].Columns.Count; i++)
            {
                cloums[i] = dataset.Tables[0].Columns[i].ToString();
            }
            cloumsHeader = string.Join(",", cloums);

            

            for (int i = 0; i < dataset.Tables.Count; i++)
            {

                DataTable dt = dataset.Tables[i];
                sw.WriteLine(dt.TableName);
                sw.WriteLine(cloumsHeader);

                foreach (DataRow dr in dt.Rows)
                {

                    string rowData = string.Empty;
                    for (int j = 0; j < dataset.Tables[0].Columns.Count; j++)
                    {
                        cloums[j] = dr[j].ToString();
                    }
                    rowData = string.Join(",", cloums);
                    sw.WriteLine(rowData);
                }
            }
            sw.Close();
            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment; filename=" + 
            HttpContext.Current.Server.UrlEncode(name));
            HttpContext.Current.Response.ContentType = "application/ms-excel";// 指定返回的是一个不能被客户端读取的流，必须被下载 
            HttpContext.Current.Response.WriteFile(name); // 把文件流发送到客户端 
            HttpContext.Current.Response.End();
        }
    }
}