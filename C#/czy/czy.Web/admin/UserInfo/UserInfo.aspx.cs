using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_UserInfo_UserInfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Bind();

    }
    private void Bind()
    {
        Menu.DataSource = BBL.UserInfo.Select();
        Menu.DataBind();
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {

    }
    protected void Menu_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToString() == "Del")
        {
            if (BBL.UserInfo.Delete(e.CommandArgument) > 0)
            {
                czy.MyClass.Web.JavaScript.Alert("删除成功！"); Bind();
            }
        }
    }
    protected void Menu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (ListItemType.AlternatingItem == e.Item.ItemType || ListItemType.Item == e.Item.ItemType)
        {

            LinkButton linkbtn = (LinkButton)e.Item.FindControl("linkbtnDel");
            czy.MyClass.Web.JavaScript.AlertConfirm(linkbtn, "确定是否删除？", null);
        }
    }
}