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
using DAL;


public partial class insertData : System.Web.UI.Page
{
    Util util = new Util();
    private int insert(string cID,string name,string reamark)
    {
        string cmd = "insert into T_AreaInfo values('" + cID + "','" + name + "','" + reamark + "')";
        return Util.GetExecuteNonQuery(cmd);
    }
    private int delete(string cID, string name, string reamark)
    {
        string cmd = "delete T_AreaInfo";
        return Util.GetExecuteNonQuery(cmd);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        DataSet ds = Excel.SelectExcel(@"C:\Documents and Settings\Administrator\桌面\路.xls", "");
        GridView1.DataSource = ds;
        GridView1.DataBind();
        Response.Write(GridView1.Rows[0].Cells[0].Text);
        
    }

    private void insertArearInfo()
    {
        int m = 0;

        for (int j = 0; j < 59; j++)
        {
            for (int i = 1; i < 11; i++)
            {
                if (GridView1.Rows[j].Cells[i].Text != "&nbsp;" && GridView1.Rows[j].Cells[i].Text != "")
                {
                    if (j>=0 && j<11)
                    {
                       // m = insert("1", GridView1.Rows[j].Cells[i].Text + "附近", "鼓楼区");
                    }
                    else if (j >= 11 && j < 17)
                    {
                        m = insert("2", GridView1.Rows[j].Cells[i].Text + "附近", "台江区");
                    }
                    else if (j >= 17 && j < 25)
                    {
                        m = insert("3", GridView1.Rows[j].Cells[i].Text + "附近", "晋安区");
                    }
                    else if (j >= 25 && j < 34)
                    {
                        m = insert("4", GridView1.Rows[j].Cells[i].Text + "附近", "仓山区");
                    }
                    else if (j >= 34 && j < 37)
                    {
                        m = insert("5", GridView1.Rows[j].Cells[i].Text + "附近", "马尾区");
                    }
                    else if (j >= 38 && j < 39)
                    {
                        m = insert("6", GridView1.Rows[j].Cells[i].Text + "附近", "闽侯县");
                    }
                    else if (j >= 40 && j < 44)
                    {
                        m = insert("7", GridView1.Rows[j].Cells[i].Text + "附近", "福清市");
                    }
                    else if (j >= 45 && j < 47)
                    {
                        m = insert("8", GridView1.Rows[j].Cells[i].Text + "附近", "平潭县");
                    }
                    else if (j >= 48 && j < 50)
                    {
                        m = insert("9", GridView1.Rows[j].Cells[i].Text + "附近", "连江县");
                    }
                    else if (j >= 51 && j < 52)
                    {
                        m = insert("10", GridView1.Rows[j].Cells[i].Text + "附近", "闽清县");
                    }
                    else if (j >= 53 && j < 55)
                    {
                        m = insert("11", GridView1.Rows[j].Cells[i].Text + "附近", "长乐市");
                    }
                    else if (j >= 56 && j < 57)
                    {
                        m = insert("12", GridView1.Rows[j].Cells[i].Text + "附近", "罗源县");
                    }
                    else if (j >= 58 && j < 59)
                    {
                        m = insert("13", GridView1.Rows[j].Cells[i].Text + "附近", "永泰县");
                    }

                   
                }

            }

        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        insertArearInfo();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}
