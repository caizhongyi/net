using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Wuqi.Webdiyer;
using System.IO;
using Model;

using System.Reflection;
using System.Text.RegularExpressions;
/// <summary>
/// fun 的摘要说明
/// </summary>
public class fun:Page
{
    
    
	public fun()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}
    public static DataProvider dp = new sqlData();

    public static void SessionTest()
    {
        //userCenter u = new userCenter();
        //u.Id = 2;
        //getModel(u);
        //System.Web.HttpContext.Current.Session["userinfo"] = u;
        fun.checkLog("userinfo","login.aspx");
    }
    public static string insert(object obj)
    {

        string str1 = "";
        string str2 = "";
        Type type = obj.GetType();

        PropertyInfo[] pis = type.GetProperties();     //   获取此对象所有属性. 
        foreach (PropertyInfo pi in pis)
        {
            if (pi != pis[0])
            {
                str1 += pi.Name + ",";
                if (pi.PropertyType.Equals(typeof(System.Int32)) || pi.PropertyType.Equals(typeof(System.Double)))
                {

                    str2 +=GetSafeStr( pi.GetValue(obj, null)) + ",";

                }
                else
                {
                    str2 += "'" +GetSafeStr( pi.GetValue(obj, null)) + "',";
                }


            }
        }
        str1 = str1.Substring(0, str1.Length - 1);
        str2 = str2.Substring(0, str2.Length - 1);
        string sql = "insert into [" + obj.GetType().Name + "] (" + str1 + ")" + " values (" + str2 + ");";
        return sql;
    }

    public static string update(object obj)
    {

        string str1 = "";
        string str2 = "";
        Type type = obj.GetType();

        PropertyInfo[] pis = type.GetProperties();     //   获取此对象所有属性. 
        foreach (PropertyInfo pi in pis)
        {
            if (pi != pis[0])
            {
                str1 += pi.Name + "=";
                if (pi.PropertyType.Equals(typeof(System.Int32)) || pi.PropertyType.Equals(typeof(System.Double)))
                {

                    str1 +=GetSafeStr( pi.GetValue(obj, null)) + ",";

                }
                else
                {
                    str1 += "'" +GetSafeStr( pi.GetValue(obj, null)) + "',";
                }


            }
        }
        str1 = str1.Substring(0, str1.Length - 1);
        str2 = " where " + pis[0].Name + "=" + pis[0].GetValue(obj, null);
        string sql = "update " + obj.GetType().Name + " set " + str1 + str2+";";
        return sql;
    }

    public static string delete(object obj)
    {


        Type type = obj.GetType();

        PropertyInfo[] pis = type.GetProperties();     //   获取此对象所有属性. 


        string sql = "delete from " + obj.GetType().Name + " where " + pis[0].Name + "=" + pis[0].GetValue(obj, null)+";";
        return sql;
    }


    public static string delete(object obj,string where)
    {


        Type type = obj.GetType();

        PropertyInfo[] pis = type.GetProperties();     //   获取此对象所有属性. 


        string sql = "delete from " + obj.GetType().Name + where + ";";
        return sql;
    }

    public static bool getModel(object obj)
    {


        Type type = obj.GetType();

        PropertyInfo[] pis = type.GetProperties();     //   获取此对象所有属性. 
        string sql = "select * from " + obj.GetType().Name + " where " + pis[0].Name + "=" + pis[0].GetValue(obj, null);
        DataTable dt = fun.GetDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            foreach (PropertyInfo pi in pis)
            {

                if (pi.PropertyType.Equals(typeof(System.Int32)))
                {

                    int value = Convert.ToInt32(dt.Rows[0][pi.Name]);
                    pi.SetValue(obj, value, null);

                }
                else if (pi.PropertyType.Equals(typeof(System.Double)))
                {
                    double value = Convert.ToDouble(dt.Rows[0][pi.Name]);
                    pi.SetValue(obj, value, null);
                }
                else if (pi.PropertyType.Equals(typeof(System.Decimal)))
                {
                    decimal value = Convert.ToDecimal(dt.Rows[0][pi.Name]);
                    pi.SetValue(obj, value, null);
                }
                else if (pi.PropertyType.Equals(typeof(System.DateTime)))
                {
                    DateTime value = Convert.ToDateTime(dt.Rows[0][pi.Name]);
                    pi.SetValue(obj, value, null);
                }
                else if (pi.PropertyType.Equals(typeof(System.Boolean)))
                {
                    Boolean value = Convert.ToBoolean(dt.Rows[0][pi.Name]);
                    pi.SetValue(obj, value, null);
                }
                else
                {
                    string value = dt.Rows[0][pi.Name].ToString();
                    pi.SetValue(obj, value, null);
                }
                

            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool getModel(object obj,string where)
    {


        Type type = obj.GetType();

        PropertyInfo[] pis = type.GetProperties();     //   获取此对象所有属性. 
        string sql = "select * from " + obj.GetType().Name +where;
        DataTable dt = fun.GetDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            foreach (PropertyInfo pi in pis)
            {

                if (pi.PropertyType.Equals(typeof(System.Int32)))
                {

                    int value = Convert.ToInt32(dt.Rows[0][pi.Name]);
                    pi.SetValue(obj, value, null);

                }
                else if (pi.PropertyType.Equals(typeof(System.Double)))
                {
                    double value = Convert.ToDouble(dt.Rows[0][pi.Name]);
                    pi.SetValue(obj, value, null);
                }
                else if (pi.PropertyType.Equals(typeof(System.Decimal)))
                {
                    decimal value = Convert.ToDecimal(dt.Rows[0][pi.Name]);
                    pi.SetValue(obj, value, null);
                }
                else if (pi.PropertyType.Equals(typeof(System.DateTime)))
                {
                    DateTime value = Convert.ToDateTime(dt.Rows[0][pi.Name]);
                    pi.SetValue(obj, value, null);
                }
                else if (pi.PropertyType.Equals(typeof(System.Boolean)))
                {
                    Boolean value = Convert.ToBoolean(dt.Rows[0][pi.Name]);
                    pi.SetValue(obj, value, null);
                }
                else
                {
                    string value = dt.Rows[0][pi.Name].ToString();
                    pi.SetValue(obj, value, null);
                }


            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void WriteMeta(System.Web.UI.Page p)
    {

        SiteInfo s = new SiteInfo();
        s.Id = 1;
        fun.getModel(s);
        if (p.Header != null)
        {


            HtmlMeta meta2 = new HtmlMeta();
            meta2.Name = "description";
            meta2.Content = s.Description;
            meta2.EnableViewState = false;
            p.Header.Controls.AddAt(1, meta2);

            HtmlMeta meta = new HtmlMeta();
            meta.Name = "keywords";
            meta.Content = s.Keywords;
            meta.EnableViewState = false;
            p.Header.Controls.AddAt(1, meta);

        }
        else
        {
            throw new Exception("页面上的Head标签必须加上runat='server'");
        }
    }

    public static void bind(Repeater c, string sql, int pageSize, Label Label1, Label Label2, HyperLink start, HyperLink prev, HyperLink next, HyperLink max)
    {

        c.DataSource = pagebind(dp.GreatDs(sql), pageSize, Label1, Label2, start, prev, next, max);
        c.DataBind();

    }
    public static void bind(DataList c, string sql, int pageSize, Label Label1, Label Label2, HyperLink start, HyperLink prev, HyperLink next, HyperLink max)
    {

        c.DataSource = pagebind(dp.GreatDs(sql), pageSize, Label1, Label2, start, prev, next, max);
        c.DataBind();

    }

    private static PagedDataSource pagebind(DataSet dst, int pageSize, Label Label1, Label Label2, HyperLink start, HyperLink prev, HyperLink next, HyperLink max)
    {

        PagedDataSource objpage = new PagedDataSource();
        objpage.DataSource = dst.Tables[0].DefaultView;
        objpage.AllowPaging = true;
        //dg1.PageSize = 20;
        objpage.PageSize = pageSize;
        int CurPage;
        //判断是否有分页的请求；

        ///
        if (System.Web.HttpContext.Current.Request["Page"] != null)
        {
            CurPage = Convert.ToInt32(System.Web.HttpContext.Current.Request["Page"]);
        }
        else
        {
            CurPage = 1;
        }
        //设置当前页；
        objpage.CurrentPageIndex = CurPage - 1;
        Label1.Text = "当前页：第" + CurPage.ToString() + "/" + objpage.PageCount + "页";
        Label2.Text = "共有" + objpage.DataSourceCount + "条记录　" + objpage.PageSize + "/页";
        //判断不是第一页的时候
        if (!objpage.IsFirstPage)
        {
            if (System.Web.HttpContext.Current.Request.QueryString["id"] != null)
            {
                prev.NavigateUrl = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + "?Page=" + (Convert.ToInt32(CurPage) - 1) + "&id=" + System.Web.HttpContext.Current.Request.QueryString["id"];
            }
            else { prev.NavigateUrl = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + "?Page=" + (Convert.ToInt32(CurPage) - 1); }
        }
        if (System.Web.HttpContext.Current.Request.QueryString["id"] != null)
        {
            start.NavigateUrl = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + "?Page=1" + "&id=" + System.Web.HttpContext.Current.Request.QueryString["id"];
        }
        else { start.NavigateUrl = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + "?Page=1"; }
        //判断不是最后一页的时候
        if (!objpage.IsLastPage)
        {
            if (System.Web.HttpContext.Current.Request.QueryString["id"] != null)
            {
                next.NavigateUrl = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + "?page=" + (Convert.ToInt32(CurPage) + 1) + "&id=" + System.Web.HttpContext.Current.Request.QueryString["id"];
            }
            else { next.NavigateUrl = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + "?page=" + (Convert.ToInt32(CurPage) + 1); }
        }
        if (System.Web.HttpContext.Current.Request.QueryString["id"] != null)
        {
            max.NavigateUrl = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + "?Page=" + objpage.PageCount + "&id=" + System.Web.HttpContext.Current.Request.QueryString["id"];
        }
        else { max.NavigateUrl = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath + "?Page=" + objpage.PageCount; }
        return objpage;



    }


    public static string Left(string sSource, int iLength)
    {
        sSource = re.CheckStr(sSource);
        if (iLength > sSource.Length)
        {
            return sSource;
        }
        else
        {
            return sSource.Substring(0, iLength) + "......";
        }
        //return sSource.Substring(0, iLength > sSource.Length ? sSource.Length : iLength);
    }
    public static void delFile(string src)
    {
        if (src != string.Empty)
        {
            if (File.Exists(src))
            {
                File.Delete(src);

            }
        }
    }

    public static string setImg(string src)
    {
        if (!File.Exists( src))
        {


            return "images/noperson.jpg";

        }
        else
        {
            return src;
        }
    }
    public static string getQueryString(string id)
    {
        if (HttpContext.Current.Request.QueryString[id] == null)
        {
            return "";
        }
        return GetSafeStr( HttpContext.Current.Request.QueryString[id]);
    }

    public static string getFormString(string id)
    {
        if (HttpContext.Current.Request.Form[id] == null)
        {
            return "";
        }
        return GetSafeStr(HttpContext.Current.Request.Form[id]);
    }

    public static int getQueryInt(string id)
    {
        string strid = HttpContext.Current.Request.QueryString[id];
        return StrToInt(strid,0);
    }

    public static int StrToInt(object strValue, int defValue)
    {
        if ((strValue == null) || (strValue.ToString() == string.Empty) || (strValue.ToString().Length > 10))
        {
            return defValue;
        }

        string val = strValue.ToString();
        string firstletter = val[0].ToString();

        if (val.Length == 10 && IsNumber(firstletter) && int.Parse(firstletter) > 1)
        {
            return defValue;
        }
        else if (val.Length == 10 && !IsNumber(firstletter))
        {
            return defValue;
        }


        int intValue = defValue;
        if (strValue != null)
        {
            bool IsInt = new Regex(@"^([-]|[0-9])[0-9]*$").IsMatch(strValue.ToString());
            if (IsInt)
            {
                intValue = Convert.ToInt32(strValue);
            }
        }

        return intValue;
    }

    public static bool IsNumber(string strNumber)
    {
        return new Regex(@"^([0-9])[0-9]*(\.\w*)?$").IsMatch(strNumber);
    }

    public static void upFile(string oldImg, HtmlInputFile fileBig,out string pic,out string error)
    {
        delFile(oldImg);
        UploadFile_Single u = new UploadFile_Single();
        u.IsUploadImage = false;
        if (u.Upload(fileBig.PostedFile))
        {
            error = u.UploadResultMessage;
            pic = u.UploadPath;
            

        }
        else
        {
            error= u.UploadResultMessage;
            pic = "";
            
        }
    }

    public static void upFile(string oldImg, HtmlInputFile fileBig, out string pic, out string error,string src)
    {
        delFile(oldImg);
        UploadFile_Single u = new UploadFile_Single();
        u.UploadDir = src;
        u.IsUploadImage = false;
        if (u.Upload(fileBig.PostedFile))
        {
            error = u.UploadResultMessage;
            pic = u.UploadPath;


        }
        else
        {
            error = u.UploadResultMessage;
            pic = "";

        }
    }


 
    public static void AJAXalert(Control c, string script)
    {


        if (script.ToLower().Contains("location"))
        {
          
            System.Web.HttpContext.Current.Response.Write("<script>" + script + "</script>");
            //ScriptManager.RegisterClientScriptBlock(c, c.GetType(), "script",script, true);
        }
        else
        {

            System.Web.HttpContext.Current.Response.Write("<script>alert('" + script + "')</script>");
            //ScriptManager.RegisterClientScriptBlock(c, c.GetType(), "script", "alert('" + script + "')", true);
        }
    }

    public static void AJAXalert( string script)
    {


        if (script.ToLower().Contains("location"))
        {

            System.Web.HttpContext.Current.Response.Write("<script>" + script + "</script>");
            //ScriptManager.RegisterClientScriptBlock(c, c.GetType(), "script",script, true);
        }
        else
        {

            System.Web.HttpContext.Current.Response.Write("<script>alert('" + script + "')</script>");
            //ScriptManager.RegisterClientScriptBlock(c, c.GetType(), "script", "alert('" + script + "')", true);
        }
    }

    public static void bind(DataList c,string sql)
    {


        c.DataSource = dp.GetDataTable(sql);
        c.DataBind();
        
    }

 

    public static void bind(Repeater c, string sql)
    {

        c.DataSource = dp.GetDataTable(sql);
        c.DataBind();

    }

    public static void bind(Repeater c, DataTable dt)
    {

        c.DataSource = dt;
        c.DataBind();

    }




    public static void bind(GridView c, string sql)
    {

        c.DataSource = dp.GetDataTable(sql);
        c.DataBind();

    }
    public static void bind(DropDownList c, string sql,string text,string value)
    {

        c.DataSource = dp.GetDataTable(sql);
        c.DataTextField = text;
        c.DataValueField = value;
        c.DataBind();

    }
    public static void bind(HtmlSelect c, string sql, string text, string value)
    {

        c.DataSource = dp.GetDataTable(sql);
        c.DataTextField = text;
        c.DataValueField = value;
        c.DataBind();

    }
    public static void bind(DropDownList c, string sql,string text,string value,string all)
    {
        c.Items.Clear();
        ListItem li = new ListItem(all,"0");
        c.Items.Add(li);
        DataTable dt = fun.GetDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            ListItem li2 = new ListItem(dr[text].ToString(), dr[value].ToString());
            c.Items.Add(li2);
            
        }

    }
    public static void bind(HtmlSelect c, string sql, string text, string value, string all)
    {
        c.Items.Clear();
        ListItem li = new ListItem(all, "0");
        c.Items.Add(li);
        DataTable dt = fun.GetDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            ListItem li2 = new ListItem(dr[text].ToString(), dr[value].ToString());
            c.Items.Add(li2);

        }

    }
    public static void bind(HtmlSelect c, string sql, string text, string value, string all,string allvalue)
    {
        c.Items.Clear();
        ListItem li = new ListItem(all, allvalue);
        c.Items.Add(li);
        DataTable dt = fun.GetDataTable(sql);
        foreach (DataRow dr in dt.Rows)
        {
            ListItem li2 = new ListItem(dr[text].ToString(), dr[value].ToString());
            c.Items.Add(li2);

        }

    }

    public static void bind(DropDownList c, DataTable dt, string text, string value)
    {

        c.DataSource = dt;
        c.DataTextField = text;
        c.DataValueField = value;
        c.DataBind();

    }
    public static void bind(HtmlSelect c, DataTable dt, string text, string value)
    {

        c.DataSource = dt;
        c.DataTextField = text;
        c.DataValueField = value;
        c.DataBind();

    }
    public static void bind(DropDownList c, DataTable dt, string text, string value, string all)
    {
        c.Items.Clear();
        ListItem li = new ListItem(all, "0");
        c.Items.Add(li);
     
        foreach (DataRow dr in dt.Rows)
        {
            ListItem li2 = new ListItem(dr[text].ToString(), dr[value].ToString());
            c.Items.Add(li2);

        }

    }
    public static void bind(HtmlSelect c, DataTable dt, string text, string value, string all)
    {
        c.Items.Clear();
        ListItem li = new ListItem(all, "0");
        c.Items.Add(li);
       
        foreach (DataRow dr in dt.Rows)
        {
            ListItem li2 = new ListItem(dr[text].ToString(), dr[value].ToString());
            c.Items.Add(li2);

        }

    }

    public static void bind(HtmlSelect c, DataTable dt, string text, string value, string all,string allvalue)
    {
        c.Items.Clear();
        ListItem li = new ListItem(all, allvalue);
        c.Items.Add(li);

        foreach (DataRow dr in dt.Rows)
        {
            ListItem li2 = new ListItem(dr[text].ToString(), dr[value].ToString());
            c.Items.Add(li2);

        }

    }

    public static void BindPage(string sql, AspNetPager AspNetPager1, DataList DataList1,int page)
    {
        
        DataTable dt = fun.GetDataTable(sql);
        
        DataView dv = dt.DefaultView;//根据datatable获取数据源
        PagedDataSource pds = new PagedDataSource();//分页数据源.
        //AspNetPager1.RecordCount = dv.Count - 1;//DataView的Count属性,AspNetPager的RecordCount属性.总页数
        pds.DataSource = dv;//分页数据源以DataView为数据源.
        pds.AllowPaging = true;//启用分页.
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;//当前页索引.
        //pds.PageSize = AspNetPager1.PageSize;//每页要显示的记录条数.
        pds.PageSize = page;//每页要显示的记录条数.

        AspNetPager1.RecordCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dv.Count) / Convert.ToDouble(pds.PageSize)));
        if (dt.Rows.Count > 0)
        {
            DataList1.DataSource = pds;
            DataList1.DataBind();
        }
    }
    public static void BindPage(string sql, AspNetPager AspNetPager1, Repeater DataList1,int page)
    {

        DataTable dt = fun.GetDataTable(sql);

        DataView dv = dt.DefaultView;//根据datatable获取数据源
        PagedDataSource pds = new PagedDataSource();//分页数据源.
        //AspNetPager1.RecordCount = dv.Count - 1;//DataView的Count属性,AspNetPager的RecordCount属性.总页数
        pds.DataSource = dv;//分页数据源以DataView为数据源.
        pds.AllowPaging = true;//启用分页.
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;//当前页索引.
        //pds.PageSize = AspNetPager1.PageSize;//每页要显示的记录条数.
        pds.PageSize = page;//每页要显示的记录条数.

        AspNetPager1.RecordCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dv.Count) / Convert.ToDouble(pds.PageSize)));
       
            DataList1.DataSource = pds;
            DataList1.DataBind();
        
    }
    public static void BindPage(DataTable dt, AspNetPager AspNetPager1, Repeater DataList1, int page)
    {

        

        DataView dv = dt.DefaultView;//根据datatable获取数据源
        PagedDataSource pds = new PagedDataSource();//分页数据源.
        //AspNetPager1.RecordCount = dv.Count - 1;//DataView的Count属性,AspNetPager的RecordCount属性.总页数
        pds.DataSource = dv;//分页数据源以DataView为数据源.
        pds.AllowPaging = true;//启用分页.
        pds.CurrentPageIndex = AspNetPager1.CurrentPageIndex - 1;//当前页索引.
        //pds.PageSize = AspNetPager1.PageSize;//每页要显示的记录条数.
        pds.PageSize = page;//每页要显示的记录条数.

        AspNetPager1.RecordCount = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(dv.Count) / Convert.ToDouble(pds.PageSize)));

        DataList1.DataSource = pds;
        DataList1.DataBind();

    }

   
    public static bool IsMatch(string str)
    {
        if (System.Text.RegularExpressions.Regex.IsMatch(str, @"^\d*$"))
        {
            
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckStr(string str)
    {
       
        if (!string.IsNullOrEmpty(str))
        {
            if (str.Trim() != "")
            {
                return true;
            }
            else
            {

                return false;
            }
        }
      
        else
        {
            return false;
        }
    }
    public static bool CheckName(string table, string column, string name)
    {
        string sql = string.Format("select * from {0} where {1}='{2}'", table, column, name);
        DataTable dt = dp.GreatDs(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool CheckName(string sql)
    {
      
        DataTable dt = dp.GreatDs(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    public static bool CheckName(string table, string column, int name)
    {
        string sql = string.Format("select * from {0} where {1}={2}", table, column, name);
        DataTable dt = dp.GreatDs(sql).Tables[0];
        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static string MD5(string str)//32位加密
    {
      
        return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5").ToLower().Substring(8, 16);

    }
    public static void log(string column, string usn, string pwd,string url,Control c)
    {
        usn = GetSafeStr(usn);
        pwd = MD5(pwd);
        string sql = string.Format("SELECT * FROM {0} where username='{1}'  and userpassword='{2}'",column ,usn, pwd);
        DataTable dt = dp.GetDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            
           Convert.ToInt32( dt.Rows[0]["ID"]);

           System.Web.HttpContext.Current.Session["admincenter"] = Convert.ToInt32(dt.Rows[0]["ID"]); ;
            FormsAuthenticationTicket t = new FormsAuthenticationTicket(1, dt.Rows[0]["username"].ToString(), DateTime.Now, DateTime.Now.AddHours(24), true, "");
            string ht = FormsAuthentication.Encrypt(t);
            System.Web.HttpContext.Current.Response.Cookies[FormsAuthentication.FormsCookieName].Value = ht;
            System.Web.HttpContext.Current.Response.Redirect(url);
        }
        else
        {


            fun.AJAXalert(c, "alert('用户名或密码错误');location='" + System.Web.HttpContext.Current.Request.Url.ToString() + "'");
        }


    }
    public static void userlog(string column, string usn, string pwd, string url, Control c)
    {
        usn = GetSafeStr(usn);
        pwd = MD5(pwd);
        string sql = string.Format("SELECT * FROM {0} where username='{1}'  and userpassword='{2}'", column, usn, pwd);
        DataTable dt = dp.GetDataTable(sql);
        if (dt.Rows.Count > 0)
        {
            userCenter u = new userCenter();
            u.Id =Convert.ToInt32( dt.Rows[0]["ID"]);
            getModel(u);
            System.Web.HttpContext.Current.Session["userinfo"] = u;
          
            System.Web.HttpContext.Current.Response.Redirect(url);
        }
        else
        {


            fun.AJAXalert(c, "alert('用户名密码错误');location='" + System.Web.HttpContext.Current.Request.Url.ToString() + "'");
        }


    }

    public static string logAjax(string column, string usn, string pwd)
    {
      
        if (!CheckStr(usn) || !CheckStr(pwd))
        {
            return "用户名和密码不能为空或含有非法字符";
        }
        else
        {
            usn = GetSafeStr(usn);
            pwd = GetSafeStr(pwd);
            pwd = MD5(pwd);
            string sql = string.Format("SELECT * FROM {0} where username='{1}'  and userpassword='{2}'", column, usn, pwd);
            DataTable dt = dp.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                System.Web.HttpContext.Current.Session["Uid"] = dt.Rows[0]["ID"];
                System.Web.HttpContext.Current.Session["username"] = dt.Rows[0]["username"];
                return "登陆成功";
            }
            else
            {
                return "用户名或密码错误";
            }
        }


    }
    public static string getById(string sid,string column,string table ,string get)
    {
        int id = int.Parse(sid);
        string sql =string.Format( "select * from {0} where {1}={2} ",table,column,id);
        if (dp.GetDataTable(sql).Rows.Count > 0)
        {
            DataRow dr = dp.GetDataTable(sql).Rows[0];
            return dr[get].ToString();
        }
        else
        {
            return "";
        }

    }
    public static int getIntById(string sid, string column, string table, string get)
    {
        
        string sql = string.Format("select * from {0} where {1}={2} ", table, column, sid);
        if (dp.GetDataTable(sql).Rows.Count > 0)
        {
            DataRow dr = dp.GetDataTable(sql).Rows[0];
            return int.Parse(dr[get].ToString());
        }
        else
        {
            return 0;
        }

    }
    public static string getId(string sid, string column, string table, string get)
    {
        
        string sql = string.Format("select * from [{0}] where {1}='{2}' ", table, column, sid);
        if (dp.GetDataTable(sql).Rows.Count > 0)
        {
            DataRow dr = dp.GetDataTable(sql).Rows[0];
            return dr[get].ToString();
        }
        else
        {
            return "";
        }

    }

    /// <summary>
    /// 过滤提交的非法字符串(防止sql注入)
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string GetSafeStr(object str)
    {
        if (str != null)
        {
            //return str.ToString().Replace("'", "''").Replace("\"", "").Replace(";", "").Replace("--", "");

            return str.ToString().Replace("'", "''");
        }
        return "";
    }
 
     public static DataTable DataTablePage(DataTable dtSource, int pageIndex, int pageSize)
        {
            //if (null== dtSource || dtSource.Rows.Count<=0)
            //{
            //    return null;
            //}

            DataTable dtResult = dtSource.Clone();

            int count = (pageIndex + 1) * pageSize;
            int beginIndex = pageIndex * pageSize;

            if (count > dtSource.Rows.Count)
                count = dtSource.Rows.Count;

            for (int i = beginIndex; i < count; i++)
            {
                dtResult.Rows.Add(dtSource.Rows[i].ItemArray);
            }
            return dtResult;
        }

    public static string getTable(string sql,int LieShu,string table,string tr,string td)
    {
        DataTable dt = fun.GetDataTable(sql);
        int count = dt.Rows.Count;//实际多少数据
        int rowsize = LieShu;//每行显示多少条记录

        int maxcount = count;//循环次数

        if (count % rowsize != 0)//判断是否整除
        {
            maxcount = (int)(count / rowsize + 1) * rowsize;
        }
        string str = string.Empty;
        for (int i = 0; i < maxcount; i++)
        {
            if (i == 0)
            {
                str += table;
            }
            if (i % rowsize == 0)
            {
                str += tr;
            }
            if (i < count)//是否有数据
            {
                str += td + (i + 1) + "</td>";
            }
            else
            {
                str += "<td>&nbsp;</td>";
            }
            if (i % rowsize == rowsize - 1)
            {
                str += "</tr>";
            }
            if (i == maxcount - 1)
            {
                str += "</table>";
            }
        }
        return str;
    }

    public static void quanxian(string table)
    {
        //int userid =Convert.ToInt32( System.Web.HttpContext.Current.Session["Uid"]);
        //if (fun.GetDataTable("select * from sheziquanxian where userid=" + userid + " and quanxianid=1").Rows.Count > 0)
        //{

           
        //}
        //else
        //{
        //    string quanxianid = fun.getId(table, "quanxiantable", "quanxian", "id");
        //    DataTable dt = fun.GetDataTable("select * from sheziquanxian where userid=" + userid + " and quanxianid=" + quanxianid);
        //    if (dt.Rows.Count > 0)
        //    {
             
        //    }
        //    else
        //    {
        //        string script = "alert('您没有权限');location='main.aspx'";
        //        System.Web.HttpContext.Current.Response.Write("<script>" + script + "</script>");
        //    }
        //}
    }
    public static void checkLog(string sessionName,string url)
    {
        if (System.Web.HttpContext.Current.Session[sessionName] == null)
        {
            System.Web.HttpContext.Current.Response.Redirect(url);
        }
    }
    public static void DoSql(Control c, string sql)
    {
        dp.DoSql(c,sql);
    }

    public static void DoSql(Control c, string sql, string url,string pic)
    {
        string  s = sql.ToLower();
        string str = sql.Substring(0, 1).ToLower();
       
       
        if (str == "d")
        {
            System.Web.HttpServerUtility Server = HttpContext.Current.Server;
            
            
            DataTable dt = fun.GetDataTable(s.Replace("delete", "select * "));
            foreach (DataRow dr in dt.Rows)
            {

                delFile(Server.MapPath("~/fx_admin/" + dr[pic].ToString()));
            }

        }

        dp.DoSql(c, sql,url);
   
    
        
    }

    public static void DoSql(Control c, string sql, string url, string pic,string pic2)
    {
        string s = sql.ToLower();
        string str = sql.Substring(0, 1).ToLower();


        if (str == "d")
        {
            System.Web.HttpServerUtility Server = HttpContext.Current.Server;


            DataTable dt = fun.GetDataTable(s.Replace("delete", "select * "));
            foreach (DataRow dr in dt.Rows)
            {

                delFile(Server.MapPath("~/fx_admin/" + dr[pic].ToString()));
                delFile(Server.MapPath("~/fx_admin/" + dr[pic2].ToString()));
            }

        }

        dp.DoSql(c, sql, url);



    }
    public static void DoSql(Control c, string sql, string url, string pic, string pic2,string pic3)
    {
        string s = sql.ToLower();
        string str = sql.Substring(0, 1).ToLower();


        if (str == "d")
        {
            System.Web.HttpServerUtility Server = HttpContext.Current.Server;


            DataTable dt = fun.GetDataTable(s.Replace("delete", "select * "));
            foreach (DataRow dr in dt.Rows)
            {

                delFile(Server.MapPath("~/" + dr[pic].ToString()));
                delFile(Server.MapPath("~/" + dr[pic2].ToString()));
                delFile(Server.MapPath("~/" + dr[pic3].ToString()));
            }

        }

        dp.DoSql(c, sql, url);



    }
    public static void DoSql(Control c, string sql, string url)
    {
        dp.DoSql(c,sql,url);
    }
    public static DataTable GetDataTable(string sql)
    {
        return dp.GetDataTable(sql);
    }
    public static DataSet getDataSet(string sql)
    {
        return dp.GreatDs(sql);
    }
    public static bool DoSqlAJAX(string sql)
    {
        return dp.DoSqlAJAX(sql);
    }
   
}
