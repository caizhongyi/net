using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

public partial class AdminManager_PageBuilder : System.Web.UI.Page
{
    string newsRoot = "NewsPages";
    string newsPageName = "newsPages";
    string newsDetailRoot = "NewsDetails";
    string newsDetailName= "newsDetails";
    string path = string.Empty;

    int currentId = 4;
    protected  struct menuId
    {
         public const int news = 2;
         public const int zhengquang = 1;
         public const int index = 4;
         public const int about = 5;
         public const int employee = 6;
         public const int contact = 7;
         public const int dichang = 8;
         public const int xuntong = 10;
   
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        path = AppDomain.CurrentDomain.BaseDirectory + newsRoot;
        Common.AuthenticationLogin(this, "Login.aspx");
        btn_firstPage.Attributes.Add("onclick", "document.getElementById('lab_index').innerHTML='生成中...'");
        btn_newsDetail.Attributes.Add("onclick", "document.getElementById('btn_newsDetail').innerHTML='生成中...'");
        btn_about.Attributes.Add("onclick", "document.getElementById('lab_about').innerHTML='生成中...'");
        btn_xuntong.Attributes.Add("onclick", "document.getElementById('lab_xuntong').innerHTML='生成中...'");
        btn_dichang.Attributes.Add("onclick", "document.getElementById('lab_dichang').innerHTML='生成中...'");
        btn_zhengquang.Attributes.Add("onclick", "document.getElementById('lab_zhengquang').innerHTML='生成中...'");
        btn_news.Attributes.Add("onclick", "document.getElementById('lab_news').innerHTML='生成中...'");
        btn_contact.Attributes.Add("onclick", "document.getElementById('lab_contact').innerHTML='生成中...'");
        btn_employee.Attributes.Add("onclick", "document.getElementById('lab_employee').innerHTML='生成中...'");
        btn_all.Attributes.Add("onclick",
             "document.getElementById('lab_all').innerHTML='生成中...';"+
             "document.getElementById('lab_index').innerHTML='生成中...;'"+
             "document.getElementById('lab_about').innerHTML='生成中...';"+
             "document.getElementById('lab_dichang').innerHTML='生成中...';"+
             "document.getElementById('lab_zhengquang').innerHTML='生成中...';" +
             "document.getElementById('lab_xuntong').innerHTML='生成中...';" +
             "document.getElementById('lab_news').innerHTML='生成中...';"+
             "document.getElementById('lab_contact').innerHTML='生成中...';" +
             "document.getElementById('lab_employee').innerHTML='生成中...';"+
             "document.getElementById('btn_newsDetail').innerHTML='生成中...';"
            );
    }
    protected void btn_firstPage_Click(object sender, EventArgs e)
    {
       
        CreateIndexPage();
    }
    protected void btn_about_Click(object sender, EventArgs e)
    {
        CreateAboutPage();
    }
    protected void btn_xuntong_Click(object sender, EventArgs e)
    {
        
        CreateXunTongPage();
    }
    protected void btn_dichang_Click(object sender, EventArgs e)
    {
        CreateDiChangPage();
    }
    protected void btn_zhengquang_Click(object sender, EventArgs e)
    {
        CreateZhengQuangPage();
    }
    protected void btn_news_Click(object sender, EventArgs e)
    {
        CreateNewsPages();
       
    }
    protected void btn_newsDetail_Click(object sender, EventArgs e)
    {
        CreateNewsDetails();
    }
    protected void btn_contact_Click(object sender, EventArgs e)
    {
        CreateContactPage();
    }
    protected void btn_employee_Click(object sender, EventArgs e)
    {
        CreateEmployeePage();
    }
    protected void btn_all_Click(object sender, EventArgs e)
    {
        CreateIndexPage();
        CreateAboutPage();
        CreateSubAboutPage();
        CreateNewsPages();
        CreateNewsDetails();
        CreateContactPage();
        CreateEmployeePage();

        CreateXunTongPage();
        CreateZhengQuangPage();
        CreateDiChangPage();

        lab_all.Text = "生成完成!";
    }

    private DataSet  GetAboutHTML()
    {
        return Company.BBL.NewsInfo.Select(menuId.about);
    }
    /// <summary>
    /// 生成新闻页
    /// </summary>
    private void CreateNewsPages()
    {
        currentId = menuId.news; 

        int first = 0;
        int prev = 0;
        int next = 0;
        int last = 0;

        string title = string.Empty;
        string createDate = string.Empty;
        string currentPage = string.Empty;
        string firstPage = string.Empty;
        string lastPage = string.Empty;
        string prevPage = string.Empty;
        string nextPage = string.Empty;
        string currentIndex = string.Empty;
        string totalCountStr = string.Empty;
        string totalPageCountStr = string.Empty;
        string newsList = string.Empty;
        ArrayList resoultsEr = new ArrayList();
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        int totalCount = 0;
        int pageCount = 0;
        int pageSize = 10;
        DataSet ds = Company.BBL.NewsInfo.GetCurrentNews(0, pageSize,menuId.news, out totalCount, out pageCount);

        // currentPage = newsDetailRoot+"/"+newsDetailName+i.ToString ()+".html";
        MyClass.PageHelper.NavPagerBuilder.GetNavgationNumber(0, pageCount, out first, out prev, out next, out last);
            firstPage = "/" + newsRoot + "/" + newsPageName + first.ToString ()+ ".html";
            lastPage = "/" + newsRoot + "/" + newsPageName + last.ToString() + ".html";
            prevPage = "/" + newsRoot + "/" + newsPageName + prev.ToString() + ".html";
            nextPage = "/" + newsRoot + "/" + newsPageName + next.ToString() + ".html";

        currentIndex = "1";

        totalCountStr = totalCount.ToString();
        totalPageCountStr = pageCount.ToString();
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            title = ds.Tables[0].Rows[i]["n_title"].ToString();
            createDate = T_BasePage.ConvertDateTime(Convert.ToDateTime(ds.Tables[0].Rows[i]["n_createDate"]));
            currentPage = "/" + newsDetailRoot + "/" + newsDetailName + i.ToString() + ".html";
            newsList += "<dl class=\"newstitle left\">";
            newsList += "<dt><strong><a href='" + currentPage + "'>" + title + "</a></strong><span>發布時間:" + createDate + "</span></dt>";
            newsList += "<dd class=\"newsmorebtn left\"><a href=\"" + currentPage + "\">查看全文</a></dd>";
            newsList += "</dl>";
        }


        Hashtable h_link = new Hashtable();
        h_link.Add("newsList", newsList);
        h_link.Add("newsFirstPage", firstPage);
        h_link.Add("newsLastPage", lastPage);
        h_link.Add("newsPrevPage", prevPage);
        h_link.Add("newsNextPage", nextPage);
        h_link.Add("newsCurrentIndex", currentIndex);
        h_link.Add("newsTotalCount", totalCountStr);
        h_link.Add("newsTotalPageCount", totalPageCountStr);
        string indexLinkHTML = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateNews, h_link);//error

        //html输出
        bool res = MyClass.FileOpeation.MyStream.StreamWrite(path + "/" + newsPageName + "0" + ".html", System.Text.Encoding.UTF8, CreateMode(indexLinkHTML, "../"));
        if (!res) { resoultsEr.Add(res); }
        for (int i = 1; i < pageCount; i++)
        {
            DataSet ds1 = Company.BBL.NewsInfo.GetCurrentNews(i, pageSize, menuId.news, out totalCount, out pageCount);
            currentIndex = (i + 1).ToString();
            MyClass.PageHelper.NavPagerBuilder.GetNavgationNumber(i, pageCount, out first, out prev, out next, out last);
            firstPage = "/" + newsRoot + "/" + newsPageName + first.ToString() + ".html";
            lastPage = "/" + newsRoot + "/" + newsPageName + last.ToString() + ".html";
            prevPage = "/" + newsRoot + "/" + newsPageName + prev.ToString() + ".html";
            nextPage = "/" + newsRoot + "/" + newsPageName + next.ToString() + ".html";
            newsList = string.Empty;
            for (int j = 0; j < ds1.Tables[0].Rows.Count; j++)
            {
                title = ds1.Tables[0].Rows[j]["n_title"].ToString();
                createDate = T_BasePage.ConvertDateTime(Convert.ToDateTime(ds1.Tables[0].Rows[j]["n_createDate"]));
                currentPage = "/" + newsDetailRoot + "/" + newsDetailName + ((i * pageSize) + j).ToString() + ".html";
                newsList += "<dl class=\"newstitle left\">";
                newsList += "<dt><strong><a href='" + currentPage + "'>" + title + "</a></strong><span>發布時間:" + createDate + "</span></dt>";
                newsList += "<dd class=\"newsmorebtn left\"><a href=\"" + currentPage + "\">查看全文</a></dd>";
                newsList += "</dl>";
            }

            Hashtable h_link1 = new Hashtable();
            h_link1.Add("newsList", newsList);
            h_link1.Add("newsFirstPage", firstPage);
            h_link1.Add("newsLastPage", lastPage);
            h_link1.Add("newsPrevPage", prevPage);
            h_link1.Add("newsNextPage", nextPage);
            h_link1.Add("newsCurrentIndex", currentIndex);
            h_link1.Add("newsTotalCount", totalCountStr);
            h_link1.Add("newsTotalPageCount", totalPageCountStr);
            string indexLinkHTML1 = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateNews, h_link1);

            //html输出
            res = MyClass.FileOpeation.MyStream.StreamWrite(path + "/" + newsPageName + i + ".html", System.Text.Encoding.UTF8, CreateMode(indexLinkHTML1, "../"));
            if (!res) { resoultsEr.Add(res); }
        }

        if (res)
        {
            lab_news.Text = "生成完成!";
        }
        else
        {
            string s = string.Empty;
            foreach (object o in resoultsEr)
            {
                s += o.ToString() + ",";
            }
            lab_news.Text = "生成失败!";
        }

    }
    /// <summary>
    /// 生成新闻详细页
    /// </summary>
    private void CreateNewsDetails()
    {
        currentId = menuId.news; 
        ArrayList resoultsEr = new ArrayList();
        DataSet ds = Company.BBL.NewsInfo.Select(menuId.news);
        bool res=false;
        string rootPath = AppDomain.CurrentDomain.BaseDirectory + newsDetailRoot;
        if (!Directory.Exists(rootPath))
        {
            Directory.CreateDirectory(rootPath);
        }
        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
        {
            string newsTitleHTML=string .Empty ;
            string newsDetailHTML=string .Empty ;
            DateTime createData = new DateTime();
            string author = string.Empty;
            if(ds.Tables[0].Rows.Count > 0)
            {
                newsTitleHTML = ds.Tables[0].Rows[i]["n_title"].ToString();
                newsDetailHTML= ds.Tables[0].Rows[i]["n_content"].ToString() ;
                createData = Convert.ToDateTime(ds.Tables[0].Rows[i]["n_createDate"]);
                author = ds.Tables[0].Rows[i]["n_author"].ToString();
            }
            Hashtable hsNews = new Hashtable();
            hsNews.Add("title", newsTitleHTML);
            hsNews.Add("newsDetail", newsDetailHTML);
            hsNews.Add("createDate", T_BasePage.ConvertDateTime( createData));
            hsNews.Add("author", author);
            string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateNewsDetail, hsNews);
            string path=AppDomain.CurrentDomain.BaseDirectory + newsDetailRoot+"/"+newsDetailName+i.ToString ()+".html";
            res = MyClass.FileOpeation.MyStream.StreamWrite(path, System.Text.Encoding.UTF8, CreateMode(html,"../"));
            if (!res) { resoultsEr.Add(path); }
        }
        if (res)
        {
            lab_newsDetail.Text = "生成完成!";
        }
        else
        {
            string s = string.Empty;
            foreach (object o in resoultsEr)
            {
                s += o.ToString() + ",";
            }
            lab_newsDetail.Text ="生成" +s+"失败!";
        }
    }

    /// <summary>
    /// 生成首页
    /// </summary>
    private void CreateIndexPage()
    {
        currentId = menuId.index;


        string newsURL = T_BasePage.TemplateRoot;
        string p = T_BasePage.TemplateMain;
        DataSet about = GetAboutHTML();
        DataSet news = Company.BBL.NewsInfo.Select(2,7);
        string aboutHTML = string.Empty;
        string newsHTML = string.Empty;
        int i = 0;
        foreach (DataRow dr in news.Tables[0].Rows)
        {
            newsHTML += "<li><a href='" + newsDetailRoot + "/" + newsDetailName + i.ToString() + ".html'>" + dr["n_title"] + "</a></li>";
            i++;
        }
        //foreach (DataRow dr in about)
        //{ 
        if (about.Tables[0].Rows.Count > 0)
        aboutHTML = about.Tables[0].Rows[0]["n_content"].ToString();
        //}
        Hashtable hsNews = new Hashtable();
        hsNews.Add("news", newsHTML);
        hsNews.Add("about", aboutHTML);
        hsNews.Add("newsList", newsRoot + "/" + newsPageName + "0.html");
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + p, hsNews);
        bool res = MyClass.FileOpeation.MyStream.StreamWrite(AppDomain.CurrentDomain.BaseDirectory + T_BasePage.TemplateIndex, System.Text.Encoding.UTF8, CreateMode(html, ""));
        if (res)
        {
            lab_index.Text = "生成完成!";
        }
        else
        {
            lab_index.Text = "生成失败!";
        }
    }
     
    private void CreateXunTongPage()
    {
        currentId = menuId.xuntong; 


        Hashtable hsNews = new Hashtable();
        string tempHTML = string.Empty;
        DataSet ds=Company.BBL.NewsInfo.Select(Convert .ToInt32(menuId.xuntong));
        foreach(DataRow dr in ds.Tables [0].Rows )
        {
            tempHTML += dr["n_content"].ToString();
        }
        hsNews.Add("data", tempHTML);
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateXunTong, hsNews);
        bool res = MyClass.FileOpeation.MyStream.StreamWrite(AppDomain.CurrentDomain.BaseDirectory + T_BasePage.TemplateXunTong, System.Text.Encoding.UTF8, CreateMode(html, ""));
      
       
        tempHTML = string.Empty;
        ds=Company.BBL.TypeInfo.Select(menuId.xuntong);
        foreach(DataRow dr in ds.Tables [0].Rows )
        {
            hsNews = new Hashtable();
            string c = string.Empty;
            DataSet  ds1 = Company.BBL.NewsInfo.Select(Convert .ToInt32( dr["t_id"]));
            if (ds1.Tables[0].Rows.Count > 0)
            {
                c = ds1.Tables[0].Rows[0]["n_content"].ToString();
            }
            string t = dr["t_name"].ToString();
            string url = dr["t_url"].ToString();
            hsNews.Add("data", c);
            hsNews.Add("title", t);
            html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateXunTongInfo, hsNews);
            res = MyClass.FileOpeation.MyStream.StreamWrite(AppDomain.CurrentDomain.BaseDirectory + url, System.Text.Encoding.UTF8, CreateMode(html, ""));
        }

        if (res)
        {
            lab_xuntong.Text = "生成完成!";
        }
        else
        {
            lab_xuntong.Text = "生成失败!";
        }
    }
    private void CreateDiChangPage()
    {
        currentId = menuId.dichang ; 

        Hashtable hsNews = new Hashtable();
        string tempHTML = string.Empty;
        DataSet ds = Company.BBL.NewsInfo.Select((Convert .ToInt32(menuId.dichang)));
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            tempHTML += dr["n_content"].ToString();
        }
        hsNews.Add("data", tempHTML);
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateDiChang, hsNews);
        bool res = MyClass.FileOpeation.MyStream.StreamWrite(AppDomain.CurrentDomain.BaseDirectory + T_BasePage.TemplateDiChang, System.Text.Encoding.UTF8, CreateMode(html, ""));

       
        ds = Company.BBL.TypeInfo.Select(menuId.dichang);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            hsNews = new Hashtable();
            string c = string.Empty;
            DataSet  ds1 = Company.BBL.NewsInfo.Select((Convert .ToInt32(dr["t_id"])));
            if (ds1.Tables[0].Rows.Count > 0)
            {
                c = ds1.Tables[0].Rows[0]["n_content"].ToString();
            }
            string t = dr["t_name"].ToString();
            string url = dr["t_url"].ToString();
            hsNews.Add("data", c);
            hsNews.Add("title", t);
            html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateDiChangInfo, hsNews);
            res = MyClass.FileOpeation.MyStream.StreamWrite(AppDomain.CurrentDomain.BaseDirectory + url, System.Text.Encoding.UTF8, CreateMode(html, ""));
        }
      
       

        if (res)
        {
            lab_dichang.Text = "生成完成!";
        }
        else
        {
            lab_dichang.Text = "生成失败!";
        }
    }
    private void CreateZhengQuangPage()
    {
        currentId = menuId.zhengquang; 

        Hashtable hsNews = new Hashtable();
        string tempHTML = string.Empty;
        DataSet ds = Company.BBL.NewsInfo.Select(menuId.zhengquang);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            tempHTML += dr["n_content"].ToString();
        }
        hsNews.Add("data", tempHTML);
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateZhengQuang, hsNews);
        bool res = MyClass.FileOpeation.MyStream.StreamWrite(AppDomain.CurrentDomain.BaseDirectory + T_BasePage.TemplateZhengQuang, System.Text.Encoding.UTF8, CreateMode(html, ""));

     
        ds = Company.BBL.TypeInfo.Select(menuId.zhengquang);
        foreach (DataRow dr in ds.Tables[0].Rows)
        {
            hsNews = new Hashtable();
            string c = string.Empty;
            DataSet ds1 = Company.BBL.NewsInfo.Select(Convert .ToInt32(dr["t_id"]));
            if (ds1.Tables[0].Rows.Count > 0)
            {
                c = ds1.Tables[0].Rows[0]["n_content"].ToString();
            }
            string t = dr["t_name"].ToString();
            string url = dr["t_url"].ToString();
            hsNews.Add("data", c);
            hsNews.Add("title", t);
            html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateZhengQuangInfo, hsNews);
            res = MyClass.FileOpeation.MyStream.StreamWrite(AppDomain.CurrentDomain.BaseDirectory + url, System.Text.Encoding.UTF8, CreateMode(html, ""));
        }

       
        if (res)
        {
            lab_zhengquang.Text = "生成完成!";
        }
        else
        {
            lab_zhengquang.Text = "生成失败!";
        }
    }
    /// <summary>
    /// 生成关于页
    /// </summary>
    private void CreateAboutPage()
    {
        currentId = menuId.about;

        string p =T_BasePage.TemplateAbout;
        DataSet ds = GetAboutHTML();
        string aboutHTML = string.Empty;
        if (ds.Tables[0].Rows.Count > 0)
        aboutHTML = ds.Tables[0].Rows [0]["n_content"].ToString ();
        Hashtable hsNews = new Hashtable();
        hsNews.Add("about", aboutHTML);
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot +p, hsNews);
        bool res = MyClass.FileOpeation.MyStream.StreamWrite(AppDomain.CurrentDomain.BaseDirectory + p, System.Text.Encoding.UTF8, CreateMode(html, ""));
        if (res)
        {
            lab_about.Text = "生成完成!";
        }
        else
        {
            lab_about.Text = "生成失败!";
        }

    }
    /// <summary>
    /// 生成子公司介绍页
    /// </summary>
    private void CreateSubAboutPage()
    { }
    /// <summary>
    /// 生成招聘页
    /// </summary>
    private void CreateEmployeePage()
    {
        currentId = menuId.employee;

        string p = T_BasePage.TemplateEmployee;
        DataSet ds = Company.BBL.NewsInfo.Select(menuId.employee);
        string employeeHTML = string.Empty;
        if(ds.Tables[0].Rows.Count >0)
        employeeHTML = ds.Tables[0].Rows[0]["n_content"].ToString();
        Hashtable hsNews = new Hashtable();
        hsNews.Add("data", employeeHTML);
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + p, hsNews);
        bool res = MyClass.FileOpeation.MyStream.StreamWrite(AppDomain.CurrentDomain.BaseDirectory + p, System.Text.Encoding.UTF8, CreateMode(html, ""));
        if (res)
        {
            lab_employee.Text = "生成完成!";
        }
        else
        {
            lab_employee.Text = "生成失败!";
        }

    }
    /// <summary>
    /// 生成联系我们页
    /// </summary>
    private void CreateContactPage()
    {
        currentId = menuId.contact; 

        string p = T_BasePage.TemplateContact;
        DataSet ds = Company.BBL.NewsInfo.Select(menuId.contact);
        string contactHTML = string.Empty;
        if (ds.Tables[0].Rows.Count > 0)
        contactHTML = ds.Tables[0].Rows[0]["n_content"].ToString ();
        Hashtable hsNews = new Hashtable();
        hsNews.Add("data", contactHTML);
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + p, hsNews);
        bool res = MyClass.FileOpeation.MyStream.StreamWrite(AppDomain.CurrentDomain.BaseDirectory + p, System.Text.Encoding.UTF8, CreateMode(html,""));
        if (res)
        {
            lab_contact.Text = "生成完成!";
        }
        else
        {
            lab_contact.Text = "生成失败!";
        }

    }
    /// <summary>
    /// 创建模版页
    /// </summary>
    /// <param name="rightHTML">右HTML</param>
    /// <returns>返回新的页</returns>
    private string CreateMode(string rightHTML,string root)
    {
        string linkHTML=GetUtilLinkHTML(root);
        string footHTML = GetFootHTML();
        string topHTML = GetTopHTML();
        string leftHTML = GetLeftHTML();

       
        
        //html输出
        Hashtable h_index = new Hashtable();
        h_index.Add("util_link.html", linkHTML);
        h_index.Add("util_head.html", topHTML);
        h_index.Add("util_left.html", leftHTML);
        h_index.Add("util_right.html", rightHTML);
        h_index.Add("util_foot.html", footHTML);

        string indexHTML = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateIndex, h_index);
        return indexHTML;
    }


    private void repeat_DataBinding(object o, MyClass.UI.MyRepeatEventArgs e)
    {
        if (e.ItemType == MyClass.UI.MyRepeat.ItemType.head)
        {
            e.Html += "<ul>";
        }
        if (e.ItemType == MyClass.UI.MyRepeat.ItemType.body)
        {
            e.Html += "<li></li>";
        }
        if (e.ItemType == MyClass.UI.MyRepeat.ItemType.foot)
        {
            e.Html += "</ul>";
        }
    }

    private string GetUtilLinkHTML(string root)
    {
        Hashtable hsLink = new Hashtable();
        hsLink.Add("root", root);
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateLink, hsLink);
        return html;
    }

    private string GetTopHTML()
    {
        DataSet xuntong=Company.BBL.TypeInfo.Select(menuId.xuntong);
        DataSet dichan = Company.BBL.TypeInfo.Select(menuId.dichang);
        DataSet zhenquang = Company.BBL.TypeInfo.Select(menuId.zhengquang);
        string xuntongString = string.Empty;
        string dichanString = string.Empty;
        string zhenquangString = string.Empty;
        foreach (DataRow dr in xuntong.Tables[0].Rows)
        {
            xuntongString += "<dd><a href='"+dr["t_url"]+"'>"+dr["t_name"]+"</a></dd>";
        }
        foreach (DataRow dr in dichan.Tables[0].Rows)
        {
            dichanString += "<dd><a href='" + dr["t_url"] + "'>" + dr["t_name"] + "</a></dd>";
        }
        foreach (DataRow dr in zhenquang.Tables[0].Rows)
        {
            zhenquangString += "<dd><a href='" + dr["t_url"] + "'>" + dr["t_name"] + "</a></dd>";
        }
        Hashtable hsTop = new Hashtable();
        hsTop.Add("xuntong", xuntongString);
        hsTop.Add("dichan", dichanString);
        hsTop.Add("zhengquang", zhenquangString);
        string className = "tmbg";
        switch (currentId)
        {
            case menuId.index: hsTop.Add("indexClass", className); break;
            case menuId.news: hsTop.Add("newsClass", className); break;
            case menuId.employee: hsTop.Add("employeeClass", className); break;
            case menuId.dichang: hsTop.Add("dichangClass", className); break;
            case menuId.contact: hsTop.Add("contactClass", className); break;
            case menuId.xuntong: hsTop.Add("xuntongClass", className); break;
            case menuId.zhengquang: hsTop.Add("zhengquangClass", className); break;
            case menuId.about: hsTop.Add("aboutClass", className); break;
            default: hsTop.Add("indexClass", className); break;

        }
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateHead, hsTop);
        return html;
    }
    private string GetLeftHTML()
    {
        Hashtable hsLeft = new Hashtable();

        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateLeft, hsLeft);
        return html;
    }
    //private string GetRightHTML()
    //{
    //    Hashtable h_link = new Hashtable();
    //    string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateRight, h_link);
    //    return html;
    //}


    private string GetFootHTML()
    {
        Hashtable hsFoot = new Hashtable();
        string html = MyClass.UI.HTMLTempletReader.GetNewHTML(T_BasePage.TemplatePhysicsRoot + T_BasePage.TemplateFoot, hsFoot);
        return html;
    }
}
