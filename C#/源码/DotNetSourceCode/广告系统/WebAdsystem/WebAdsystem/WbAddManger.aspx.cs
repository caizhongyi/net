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

public partial class WbAddManger : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            BBL.IWbInfo IWb_Info = BBL.BBLFactory.GetWbInfo();
            int i = IWb_Info.InsetWbInfo(Convert.ToInt32(WB_ID.Text), WbName.Text, WbIP.Text, WbArea.Text, WbTel.Text, WbRemark.Text);
            if (i > 0)
            {
                Label2.Text ="<script javascript:language>alert('增加成功')</script>";
            }
            else
            {
                Response.Write("<script javascript:language>alert('增加失败')</script>");
            }

            // Panel1.Visible = true;
        }
        catch (Exception ex)
        {
            Response.Write("<script javascript:language>alert('"+ex.Message+"')</script>");
        }
    }
}
