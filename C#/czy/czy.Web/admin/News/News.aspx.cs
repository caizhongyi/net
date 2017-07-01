using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_Menu_Menu : System.Web.UI.Page
{
    int size = 15;
    protected void Page_Load(object sender, EventArgs e)
    {
        Bind(1);
        if (!IsPostBack)
        {
            AspNetPager1.RecordCount = Convert.ToInt32(BBL.V_NewsInfo.GetTotleCount(size));
        }
    }
    private void Bind(int cur)
    {
        Menu.DataSource = BBL.V_NewsInfo.GetCurrentData(cur, size);
        Menu.DataBind();
    }

    protected void AspNetPager1_PageChanging(object src, Wuqi.Webdiyer.PageChangingEventArgs e)
    {
        Bind(e.NewPageIndex);
    }
    protected void Menu_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.ToString ()== "Del")
        {
            if (BBL.V_NewsInfo.Delete(e.CommandArgument) > 0)
            {
                czy.MyClass.Web.JavaScript.Alert("删除成功！"); Bind(AspNetPager1.CurrentPageIndex);
            }
        }
    }
    protected void Menu_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (ListItemType.AlternatingItem == e.Item.ItemType || ListItemType.Item == e.Item.ItemType)
        {

            LinkButton linkbtn = (LinkButton)e.Item.FindControl("linkbtnDel");
            czy.MyClass.Web.JavaScript.AlertConfirm(linkbtn, "确定是否删除？", null);
            AspNetPager1.RecordCount = Convert.ToInt32(BBL.V_NewsInfo.GetTotleCount(size));
        }
    }
}