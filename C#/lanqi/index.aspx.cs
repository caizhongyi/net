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

public partial class index : System.Web.UI.Page
{
   int maxlen = 100;
    int maxCount = 45;
    public string str = "";
    public string lin = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        init();
        fun.bind(Repeater1,"select top 9 * from news_class2");
        fun.bind(Repeater2, "select top 10 * from advice order by id desc");
        fun.bind(Repeater3, "select top 6 * from user_product where sjid=1 order by paixu asc,id desc");
        fun.bind(Repeater4, "select top 6 * from user_product where sjid=2 order by paixu asc,id desc");
        fun.bind(Repeater5, "select top 6 * from user_product where sjid=3 order by paixu asc,id desc");
        fun.bind(Repeater6, "select top 10 * from user_product where sjid=4 and typeid=24 order by paixu asc,id desc");
        fun.bind(Repeater7, "select top 8 * from user_product where sjid=5 order by paixu asc,id desc");
        fun.bind(hotProduct, "select top 18 * from user_product where typeid=33 order by paixu asc,id desc");
        //fun.bind(Repeater7, "select top 10  * from culture where picture<>''  order by id desc");
        fun.bind(Repeater8, "select   * from friendlink where type=0  order by id desc");
       // fun.bind(Repeater9, "select top 10   * from ygfc   order by id desc");
	fun.bind(Repeater9, "select top 10 * from user_product where typeid=38 order by paixu asc,id desc");

       // fun.bind(Repeater10, "select top 10   * from news where class1_id=13   order by id desc");
       // fun.bind(Repeater11, "select top 10   * from news where class1_id=15   order by id desc");
        TravelBind();
        CommnuityBind();
        HotBind();
      //  InterestBind();
      //  FunBind();
        LinsBind();
        fun.WriteMeta(this);

        
    }

    void TravelBind()
    {
        fun.bind(TravelList, "select top " + maxCount + " * from news where class1_id=26 order by paixu asc");
        DataTable dt = fun.GetDataTable("select top 1 * from  news where class1_id=26 and istj='��'  order by paixu asc");
        if(dt.Rows.Count >0 )
        {
            string con=dt.Rows[0]["content"].ToString();
            int len = con.Length > maxlen ? maxlen : con.Length;
            TravelFirst.InnerHtml = con.Length > maxlen ? con.Substring(0, maxlen) + "...<a href='newdetail.aspx?id=" + dt.Rows[0]["id"] + "'>[����]</a>" : con;
        }
    }
    void CommnuityBind()
    {
        fun.bind(CommnuityList, "select top " + maxCount + " * from news where class1_id=14  order by paixu asc");
    }

    void HotBind()
    {
        fun.bind(HotList, "select top " + maxCount + " * from news where class1_id=22  order by paixu asc");
        DataTable dt = fun.GetDataTable("select top 1 * from news where class1_id=22 and  istj='��' order by paixu asc");
        if (dt.Rows.Count > 0)
        {
            string con = dt.Rows[0]["content"].ToString();
            HotFirst.InnerHtml = con.Length > maxlen ? con.Substring(0, maxlen) + "...<a href='newdetail.aspx?id=" + dt.Rows[0]["id"] + "'>[����]</a>" : con;
        }
    }

   /* void InterestBind()
    {
        fun.bind(InterestList, "select top " + maxCount + " * from news where class1_id=27   order by paixu asc");
    }
    void FunBind()
    {
        fun.bind(FunList, "select top " + maxCount + " * from news where class1_id=28   order by paixu asc");
    }*/

    private void init()
    {
        DataTable dt = fun.GetDataTable("select * from flashpic where type=0 ");

        foreach (DataRow dr in dt.Rows)
        {
            str += dr["pic"].ToString() + "|";
            lin += dr["web_address"].ToString() + "|";
        }
        if (str.Length > 0)
        {
            str = str.Substring(0, str.Length - 1);
        }
    }

    void LinsBind()
    {
      //  fun.bind(Links, "select top 8 * from friendlink where type=1  order by  addtime desc");
    }
}
