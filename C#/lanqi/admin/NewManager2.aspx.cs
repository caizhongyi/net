﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class admin_NewManager2 : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        fun.quanxian("news");
        if (!IsPostBack)
        {
            fun.bind(DropDownList2, "select * from news_class2", "type", "id", "所有类别");
         
           
                //fun.bind(rpNews, "select * from news  order by join_date desc", 6, Label1, Label2, start, prev, next, max);
            fun.BindPage("select * from news  where 1=1 order by paixu asc, join_date desc", AspNetPager1, rpNews, 6);
            Session["sql"] = "select * from news  where 1=1 order by paixu asc, join_date desc";
         
        }
      
    }

   

    protected void Button1_Click(object sender, EventArgs e)
    {
        string Id_str = Request["ch"], sql = "";
        if (fun.CheckStr(Id_str))
        {
            sql = "delete from  news where id in (" + Id_str + ")";
            fun.DoSql(this, sql, Request.Url.ToString());
        }
        else
        {
            fun.AJAXalert(this, "请至少选择一项！");
        }


       
   
    }


    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        string sql = "select * from news  where 1=1";
        
        int class2Id = int.Parse(DropDownList2.SelectedValue);
        if (class2Id != 0)
        {

            sql += "and class1_id=" + class2Id;
        }
       
        sql += " and title like '%" + TextBox1.Text + "%'";
        sql += " order by paixu asc, join_date desc";
        Session["sql"] = sql;
        fun.BindPage(sql , AspNetPager1, rpNews, 6);
        AspNetPager1.CurrentPageIndex = 1;
    }
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
        fun.BindPage(Session["sql"].ToString() , AspNetPager1, rpNews, 6);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        foreach (RepeaterItem r in rpNews.Items)
        {
            TextBox t = r.FindControl("txtPaiXu") as TextBox;
            if (!fun.CheckStr(t.Text) || !fun.IsMatch(t.Text))
            {
                fun.AJAXalert(this,"排序只能用数字");
                return;
            }
            int paixu =int.Parse( t.Text);
            Label cb = r.FindControl("lblId") as Label;
            int id =int.Parse( cb.Text);
            string sql = "update news set paixu="+paixu+" where id="+id;
            fun.DoSql(this,sql,Request.Url.ToString());


        }
        
    }

    protected void rpNews_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
       
    }

   
}