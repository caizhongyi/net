using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Model;
public partial class newDetail : System.Web.UI.Page
{
    public string pic="";
    string sql = "";
    int id = 0;
    string type = "";
    string userId = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.WriteMeta(this);
        //fun.bind(Repeater1, "select * from news_class2");
        id = fun.getQueryInt("id");
        type = fun.getQueryString("type");
        if (type == "advice")
        {
            sql= "select * from advice where id=" + id;
            fun.bind(Repeater3, sql);
        }
        else if (type == "ggfw")
        {
           sql="select * from intro";
           fun.bind(Repeater3, sql);
        }
        else if (type == "lxwm")
        {
            sql= "select * from contact";
            fun.bind(Repeater3, sql);
        }
        else
        {
         
           
                sql = "select * from news where id=" + id;
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataTable dtType = fun.GetDataTable("select * from news_class2 where id="+dt.Rows[0]["class1_id"]);
                    liebiao.InnerHtml = "<a href=newlist.aspx?id=" + dt.Rows[0]["class1_id"] + ">" + dtType.Rows[0]["type"] + "</a> --" + dt.Rows[0]["title"];
                    fun.DoSqlAJAX("update news set hot=hot+1 where id=" + id);
                    

                    fun.bind(Repeater2, dt);
                }

                pic = fun.getById(id.ToString(), "id", "news", "pic");
                
            
        }


        CommentsBind();
        BindFlashPic();

    }
    protected void BindFlashPic()
    {
        string sql = "select top 15 * from flashpic where type=1";
        DataTable dt = fun.GetDataTable(sql);
        Repeater4.DataSource = dt;
        Repeater4.DataBind();
    }
    bool IsLogin()
    {
        if (Session["userinfo"] != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


    void CommentsBind()
    {
        sql = "select * from CommentsView where c_newsId=" + id;
        DataTable comments = fun.GetDataTable(sql);
        if (comments.Rows.Count > 0)
        {
            CommentsList.DataSource = comments;
            CommentsList.DataBind();
            Comments.InnerHtml = string .Empty ;
        }
        else
        {
            Comments.InnerHtml = "暂无评论...";
        }
    }


    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        if (type != "advice" && type != "ggfw" && type != "lxwm")
        {
            News n = new News();
            fun.getModel(n," where id>"+id+" and class1_id="+fun.getById(id.ToString(),"id","news","class1_id")+" order by id asc");
            if (n.Id > 0)
            {
                Response.Redirect("newdetail.aspx?id=" + n.Id);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('已经是第一页')", true);
            }
        }
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        if (type != "advice" && type != "ggfw" && type != "lxwm")
        {
            News n = new News();
            fun.getModel(n, " where id<" + id + " and class1_id=" + fun.getById(id.ToString(), "id", "news", "class1_id") + " order by id desc");
            if (n.Id > 0)
            {
                Response.Redirect("newdetail.aspx?id=" + n.Id);
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "script", "alert('已经是最后一页')", true);
            }
        }
    }
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        string sql="";
        if (IsLogin())
        {
            userId = (Session["userinfo"] as userCenter).Id.ToString ();
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        sql = "insert into comments values('" + id + "','" + userId + "','','"+comentsArea.Value+"')";
        bool res=fun.DoSqlAJAX(sql);
        if (res)
        {
            CommentsBind();
        }

    }
}
