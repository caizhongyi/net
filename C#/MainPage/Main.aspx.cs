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

public partial class Main : System.Web.UI.Page
{
    private string GetDayOfWeek()
    {

        DateTime dt = DateTime.Now;
        switch (dt.DayOfWeek.ToString())
        {
            case "Monday": return "星期一";
                break;
            case "Tuesday": return "星期二";
                break;
            case "Wednesday": return "星期三";
                break;
            case "Thursday ": return "星期四";
                break;
            case "Friday ": return "星期五";
                break;
            case "Saturday": return "星期六";
                break;
            case "Sunday": return "星期日";
                break;
            default : return "";
        }
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DateTime dt = DateTime.Now;
        Label1.Text =dt.ToLongDateString();

        Label2.Text = GetDayOfWeek();
    }
}
