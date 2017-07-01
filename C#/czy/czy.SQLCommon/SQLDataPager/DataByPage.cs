//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Data;
//using System.Data.SqlClient;
//using System.Collections;

//namespace czy.MyClass.PageHelper
//{
//    /// <summary>
//    /// 分页程序
//    /// </summary>
//    public class DataByPage
//    {
//        private string sql;
//        private int pageSize=15;

//        private string query;
//        private string table;
//        private string where;
//        private string order;
//        private string orderDesc;
//        private int recordCount=0;
//        private int pageCount = 0;
//        private string orderby = "";

        
//        private int pageIndex = 0;
//        private System.Data.SqlClient.SqlDataReader recordSet;
//        private Hashtable para;
//        private string pageToolBar = "";
//        private string receptionToolBar = "";


//        private MyDAL.IDataBase idb;

//        public string OrderBy
//        {
//            get { return orderby; }
//            set { orderby = value; }
//        }
        
//        public int PageCount
//        {
//            get { return pageCount;}
//            set { pageCount = value;}
//        }

//        public string Sql
//        {
//            get { return sql; }
//            set { sql = value; }
//        }

//        public int PageSize
//        {
//            get { return pageSize; }
//            set { pageSize = value; }
//        }

//        public System.Data.SqlClient.SqlDataReader DataReader
//        {
//            get { return recordSet; }
//        }

//        public int PageIndex
//        {
//            get { return pageIndex; }
//            set { pageIndex = value; }
//        }

//        public string PageToolBar
//        {
//            get { return pageToolBar; }
//            set { pageToolBar = value; }
//        }

//        public string ReceptionToolBar
//        {
//            get { return receptionToolBar; }
//            set { receptionToolBar = value; }
//        }

//        public DataByPage(string connstring,MyDAL.Enumeration.ConnStringType type)
//        {
//            idb = new MyDAL.SQLDataBase(connstring, type);
//        }


//        public void GetRecordSetByPage()
//        {
//            GetPara();
//            SplitSql();
//            DescOrder();
//            GetCount();
            
//            GetPageCount();
//            GetToolBar();
//            ReceptionGetToolBar();
//            GetRecordSet();
//        }
//        public void GetRecordFreelabelByPage()
//        {
//            GetPara();
//            SplitSqlFreelabel();
//            DescOrder();
//            GetCount();

//            GetPageCount();
//            GetToolBar();
//            ReceptionGetToolBar();
//            GetRecordSet();
//        }
//        private string GetWhere()
//        {
//            StringBuilder sqlWhere = new StringBuilder();
//            int postion = 0;
//            string key = "";
//            string value = "";
//            foreach (DictionaryEntry de in para)
//            {
//                key = de.Key.ToString();
//                value = de.Value.ToString();
//                //大于
//                postion = key.ToString().IndexOf("w_g_");
//                if (postion >= 0)
//                {
//                    sqlWhere.Append(" and " + key.Substring(postion + 4) + ">='" + value + "'");
//                    continue;
//                }

//                //小于
//                postion = key.ToString().IndexOf("w_e_");
//                if (postion >= 0)
//                {
//                    sqlWhere.Append(" and " + key.Substring(postion + 4) + "<='" + value + "'");
//                    continue;
//                }
//                //等于
//                postion=key.ToString().IndexOf("w_d_");
//                if (postion >= 0)
//                {
//                    sqlWhere.Append(" and " + key.Substring(postion+4) + "='"+value+"'");
//                    continue;
//                }

//                //不等于
//                postion = key.ToString().IndexOf("w_n_");
//                if (postion >= 0)
//                {
//                    sqlWhere.Append(" and " + key.Substring(postion + 4) + "<>'" + value + "'");
//                    continue;
//                }

//                //模糊查询 like '%%'
//                postion = key.ToString().IndexOf("w_l_");
//                if (postion >= 0)
//                {
//                    sqlWhere.Append(" and " + key.Substring(postion + 4) + " like '%" + value + "%'");
//                    continue;
//                }

//                //左边模糊 like '%'
//                postion = key.ToString().IndexOf("w_z_");
//                if (postion >= 0)
//                {
//                    sqlWhere.Append(" and " + key.Substring(postion + 4) + " like '" + value + "%'");
//                    continue;
//                }

//                //右边模糊
//                postion = key.ToString().IndexOf("w_r_");
//                if (postion >= 0)
//                {
//                    sqlWhere.Append(" and " + key.Substring(postion + 4) + " like '%" + value + "'");
//                    continue;
//                }

//                //截取字符串
//                postion = key.ToString().IndexOf("w_s_");
//                if (postion >= 0)
//                {
//                    sqlWhere.Append(" and Substring(" + key.Substring(postion + 4) + "," + value + ",1)=1");
//                    continue;
//                }
//            }
//            return sqlWhere.ToString();
//        }
//        private void SplitSql()
//        {
//            if (!sql.Equals(""))
//            {
//                sql = sql.Replace("[select]", "");
//                sql = sql.Replace("[from]", "|");
//                sql = sql.Replace("[where]", "|");
//                sql = sql.Replace("[order by]", "|");
//                string[] sqlArr = sql.Split('|');
//                query = sqlArr[0];
//                table = sqlArr[1];
//                where = sqlArr[2] + GetWhere();
//                if (orderby.Equals(""))
//                {
//                    order = sqlArr[3];
//                }else
//                {
//                    order = orderby;
//                }
                
//            }
//        }
//        private void SplitSqlFreelabel()
//        {
//            if(!sql.Equals(""))
//            {
//                sql = sql.Replace("select", "");
//                sql = sql.Replace("from", "|");
//                sql = sql.Replace("where", "|");
//                sql = sql.Replace("order by", "|");
//                string[] sqlArr = sql.Split('|');
//                query = sqlArr[0];
//                table = sqlArr[1];
//                where = sqlArr[2] + GetWhere();
//                if (orderby.Equals(""))
//                {
//                    order = sqlArr[3];
//                }
//                else
//                {
//                    order = orderby;
//                }
//            }
//        }
//        private void GetPara()
//        {
//            if (pageIndex < 1)
//            {
//                pageIndex = WebPage.PageRequest.GetInt("pageindex");
//            }
//            if (pageIndex<1)
//            {
//                pageIndex = 1;
//            }
//            para = WebPage.PageRequest.GetPara();
//        }

//        private void DescOrder()
//        {
//            orderDesc = order.Replace("desc", "as_1dec");
//            orderDesc = orderDesc.Replace("asc", "desc");
//            orderDesc = orderDesc.Replace("as_1dec", "asc");
//        }

//        private void GetCount()
//        {
//            try
//            {
//                string strSql = "";
//                strSql = "select count(*) from " + table + " where  " + where;
//                string strCount = DataBase.SQLServerHelper.GetSingle(strSql).ToString();

//                if (Data.Validate.IsNumeric(strCount))
//                {
//                    recordCount = int.Parse(strCount);
//                }
//                else
//                {
//                    recordCount = 0;
//                }
//            }
//            catch {
//                recordCount = 0;
//            }
//        }

//        private void GetRecordSet()
//        {
//            sql = "";
//            if (recordCount > 0)
//            {
//                //如果只有一页就把所有的都取出来
//                if (pageCount <= 1 || pageIndex <= 1)
//                {
//                    sql = "select top " + PageSize + " " + query + " from " + table + " where " + where + " order by " + order;
//                }
//                else if (pageCount.Equals(pageIndex))
//                {
//                    sql = "select top " + (recordCount - PageSize * (pageIndex - 1)) + " " + query + " from " + table + " where " + where + " order by " + orderDesc;
//                    sql = " select * from (" + sql + ")temptable order by " + order;
//                }
//                else
//                {
//                    sql = "select top " + (PageSize * pageIndex) + " " + query + " from " + table + " where " + where + " order by " + order;
//                    if (pageIndex > 1)
//                    {
//                        sql = "select top " + PageSize + " * from (" + sql + ") temptable order by " + orderDesc;
//                        sql = " select * from (" + sql + ")temptable order by " + order;
//                    }
//                }


//                //得到数据
//                recordSet = DataBase.SQLServerHelper.ExecuteReader(sql);
//            }
//        }

//        private void GetPageCount()
//        {
//            pageCount = (recordCount + PageSize - 1) / PageSize;
//            if (pageIndex < 1)
//            {
//                pageIndex = 1;
//            }
//            if (pageIndex > pageCount)
//            {
//                pageIndex = pageCount;
//            }
//        }

//        #region 前台分页
//        private void ReceptionGetToolBar()
//        {

//            StringBuilder tool = new StringBuilder();
//            string parastring = "";
//            if (para.Count > 0)
//            {
//                foreach (DictionaryEntry de in para)
//                {
//                    parastring = parastring + "&" + de.Key + "=" + de.Value;
//                }
//            }
//            //开始填充信息
//            //如果是首页
//            tool.AppendLine("共找到 " + recordCount + " 条记录    分 " + pageCount + " 页显示");
//            if (pageIndex <= 1)
//            {
//                tool.AppendLine("   <span class=\"disabled\">首页</span>");
//                tool.AppendLine("   <span class=\"disabled\">上一页</span>");
//            }
//            else
//            {
//                tool.AppendLine("   <a href=?pageindex=1" + parastring + ">首页</a>");
//                tool.AppendLine("   <a href=?pageindex=" + (pageIndex - 1) + parastring + ">上一页</a>");
//            }
//            //中间代码
//            for (int i = (pageIndex - 4) > 0 ? (pageIndex - 4) : 1; i <= ((pageIndex + 4) < pageCount ? (pageIndex + 4) : pageCount); i++)
//            {
//                if (i == pageIndex)
//                {
//                    tool.AppendLine("<span class=\"current\">" + i + "</span>");
//                }
//                else
//                {
//                    tool.AppendLine("<a href=\"?pageindex=" + i + parastring + "\">" + i + "</a>");
//                }
//            }
//            if ((pageIndex + 5) < pageCount)
//            {
//                tool.AppendLine("...");
//                tool.AppendLine("<a href=\"?pageindex=" + pageCount + parastring + "\">" + pageCount + "</a>");
//            }

//            //生成位码
//            if (pageIndex >= pageCount)
//            {
//                tool.AppendLine("   <span class=\"disabled\">下一页</span>");
//                tool.AppendLine("   <span class=\"disabled\">尾页</span>");
//            }
//            else
//            {
//                tool.AppendLine("   <a href=?pageindex=" + (pageIndex + 1) + parastring + ">下一页</a>");
//                tool.AppendLine("   <a href=?pageindex=" + pageCount + parastring + ">尾页</a>");
//            }
//            this.receptionToolBar = "\n<ul class=\"quotes\">\n" + tool.ToString() + "</ul>\n";
//        }
//        #endregion

//        private void GetToolBar()
//        {

//            StringBuilder tool=new StringBuilder();
//            string parastring = "";
//            if (para.Count > 0)
//            {
//                foreach (DictionaryEntry de in para)
//                {
//                    parastring = parastring + "&"+de.Key+"="+de.Value;
//                }
//            }
//            //开始填充信息
//            //如果是首页
//            if (pageIndex <= 1)
//            {
//                tool.AppendLine("   <span class=\"disabled\"><<</span>");
//                tool.AppendLine("   <span class=\"disabled\"><</span>");
//            }
//            else
//            {
//                tool.AppendLine("   <a href=?pageindex=1" + parastring + "><<</a>");
//                tool.AppendLine("   <a href=?pageindex="+(pageIndex-1) + parastring + "><</a>");
//            }
//            //中间代码
//            for (int i = (pageIndex - 4) > 0 ? (pageIndex - 4) : 1; i <= ((pageIndex + 4) < pageCount ? (pageIndex + 4) : pageCount); i++)
//            {
//                if (i == pageIndex)
//                {
//                    tool.AppendLine( "<span class=\"current\">" + i + "</span>");
//                }
//                else
//                {
//                    tool.AppendLine("<a href=\"?pageindex=" + i + parastring + "\">" + i + "</a>");
//                }
//            }
//            if ((pageIndex + 5) < pageCount)
//            {
//                tool.AppendLine("...");
//                tool.AppendLine("<a href=\"?pageindex=" + pageCount + parastring + "\">" + pageCount + "</a>");
//            }

//            //生成位码
//            if (pageIndex >= pageCount)
//            {
//                tool.AppendLine("   <span class=\"disabled\">></span>");
//                tool.AppendLine("   <span class=\"disabled\">>></span>");
//            }
//            else
//            {
//                tool.AppendLine("   <a href=?pageindex=" + (pageIndex + 1) + parastring + ">></a>");
//                tool.AppendLine("   <a href=?pageindex="+pageCount + parastring + ">>></a>");
//            }
//            this.pageToolBar ="\n<div class=\"quotes\">\n"+ tool.ToString()+"</div>\n";
//        }
//        public void Dispose()
//        {
//            if (recordSet != null)
//            {
//                recordSet.Close();
//                recordSet.Dispose();
//                recordSet = null;
//            }
//            pageToolBar = null;
//            sql = null;
//            query = null;
//            table = null;
//            where = null;
//            order = null;
//            orderDesc = null;
//            GC.Collect();
//        }
//    }
//}
