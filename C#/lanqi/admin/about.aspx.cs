﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class admin_about : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    
        if (!IsPostBack)
        {
            string sql = "select * from intro";
            DataRow dr = fun.GetDataTable(sql).Rows[0];
            if (dr != null)
            {
                FCKeditor1.Value = dr["content"].ToString();
                Image1.ImageUrl = dr["picture"].ToString();
            }
           
            
        }
    }


    protected void btnOk_Click(object sender, EventArgs e)
    {
        string pic = "";
        string sqlstr = "";


        if (file1.PostedFile.FileName != "")
        {
            DataRow dr = fun.GetDataTable("select * from intro").Rows[0];
            fun.delFile(Server.MapPath(dr["picture"].ToString()));
            UploadFile_Single u = new UploadFile_Single();
            u.IsUploadImage = true;
            if (u.Upload(file1.PostedFile))
            {
                pic = u.UploadPath;
                sqlstr = "update intro set [content]='" + FCKeditor1.Value + "' ,picture='" + pic+"'";
               
            }
            else
            {
                fun.AJAXalert(this, u.UploadResultMessage);
                return;
            }
        }
        else
        {
            sqlstr = "update intro set [content]= '" +fun.GetSafeStr( FCKeditor1.Value)+"'";
        }

        fun.DoSql(this,sqlstr,Request.Url.ToString());
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataRow dr = fun.GetDataTable("select * from intro ").Rows[0];
        fun.delFile(Server.MapPath(dr["picture"].ToString()));
        string sql = "update intro set picture='' ";
        fun.DoSql(this, sql, Request.Url.ToString());
    }
}