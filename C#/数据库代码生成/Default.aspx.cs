﻿using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
 
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string dataBaseName = TextBox1.Text.Trim();
        try
        {
            WriteToTxt.WriteInterFaceToText(dataBaseName);
            WriteToTxt.WriteFactoryToText(dataBaseName);
            if (IsRBProcType.Checked)
            {
                WriteToTxt.WriteSqlProcToText(dataBaseName);
            }
            else if(NotRBProcType.Checked)
            {
                WriteToTxt.WriteSqlToText(dataBaseName);
            }
         
            Response.Write("生成成功");
        }
        catch
        {
            Response.Write("生成失败");
        }
    }
}
