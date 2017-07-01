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

public partial class admin_NewAdd : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fun.bind(DropDownList1, "select * from news_class", "class1", "id", "所有一级类别");
            DropDownList2.Items.Add(new ListItem("所有二级类别", "0"));
            TextBox1.Text = "20";
            
           
        }
        string id = Request.QueryString["id"];
        if (fun.CheckStr(id)&&fun.IsMatch(id))
        {
            Label1.Text = "修改新闻";
            int pid = int.Parse(id);
            if (!IsPostBack)
            {
                string sql = string.Format("select * from news where id={0}", pid);
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {

                    txtName.Text = dt.Rows[0]["title"].ToString();
                    int sjid = Convert.ToInt32(fun.GetDataTable("select * from news_class2 where id=" + Convert.ToInt32(dt.Rows[0]["class1_id"].ToString())).Rows[0]["sjid"]);
                    
                    DropDownList2.Items.Clear();
                    fun.bind(DropDownList2, "select * from news_class2 where sjid=" + sjid, "type", "id", "所有二级类别");

                    DropDownList2.SelectedValue = dt.Rows[0]["class1_id"].ToString();
                    
                    DropDownList1.SelectedValue = Convert.ToString(fun.GetDataTable("select * from news_class where id=" + sjid).Rows[0]["id"]);
                    FCKeditor1.Value = dt.Rows[0]["content"].ToString();
                    TextBox1.Text = dt.Rows[0]["paixu"].ToString();
                    if (dt.Rows[0]["istj"].ToString() == "是")
                    {
                        rdy.Checked = true;
                    }
                    else
                    {
                        rdn.Checked = true;
                    }
                }
            }
           
        }
        else
        {
            Label1.Text = "添加新闻";
            if (!IsPostBack)
            {
                rdn.Checked = true;
            }
        }
    }
    protected void btnOk_Click(object sender, EventArgs e)
    {
        if (Label1.Text == "添加新闻")
        {
            AddNew();
        }
        else if (Label1.Text == "修改新闻")
        {
            UpdateNew();
        }
    }


    protected void AddNew()
    {
        string name = txtName.Text;
        string paixu=TextBox1.Text;
        if (!fun.CheckStr(name))
        {
            Response.Write("<script>alert('标题不能为空');</script>");
        }
        else if (!fun.CheckStr(paixu) || !fun.IsMatch(paixu))
        {
            Response.Write("<script>alert('排序只能用数字');</script>");
        }
        else
        {
            int typeid = int.Parse(DropDownList2.SelectedValue);
            if (typeid == 0)
            {
                fun.AJAXalert(this, "请选择二级分类");
            }
            else
            {
                string remark = fun.GetSafeStr(FCKeditor1.Value);
                name = fun.GetSafeStr(name);
                string sql = string.Format("insert into news (title,class1_id,[content],istj,paixu) values ('{0}',{1},'{2}','{3}','{4}')", name, typeid, remark,Request.Form["rdtj"],int.Parse(TextBox1.Text));
                fun.DoSql(this, sql, Request.Url.ToString());
            }
        }
    }
    protected void UpdateNew()
    {
        string name = txtName.Text;
        if (!fun.CheckStr(name))
        {
            Response.Write("<script>alert('标题不能为空');</script>");
        }
        else
        {
            int typeid = int.Parse(DropDownList2.SelectedValue);
            if (typeid == 0)
            {
                fun.AJAXalert(this, "请选择二级分类");
            }
            else
            {
                string remark = fun.GetSafeStr(FCKeditor1.Value);
                name = fun.GetSafeStr(name);
                int id = int.Parse(Request.QueryString["id"]);
                string sql = string.Format("update news set title='{0}',class1_id={1},[content]='{2}',istj='{3}',paixu={4} where id={5}", name, typeid, remark,Request.Form["rdtj"],int.Parse(TextBox1.Text), id);
                fun.DoSql(this, sql, Request.Url.ToString());
            }
        }
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = int.Parse(DropDownList1.SelectedValue);
        DropDownList2.Items.Clear();
        fun.bind(DropDownList2, "select * from news_class2 where sjid=" + id, "type", "id", "所有二级类别");
    }
}
