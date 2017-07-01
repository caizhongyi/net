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

public partial class admin_model : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string id=Request.QueryString["id"];
        if (fun.CheckStr(id) && fun.IsMatch(id))
        {
            string sql = "select * from modelyp where id="+id;
            DataTable dt = fun.GetDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                subject.Value=dr["name"].ToString();
                catid.Value=dr["sex"].ToString();
                join_phone1.Value=dr["aihao"].ToString();
                if (dr["tongyi"].ToString() == "同意")
                {
                    j1.Checked = true;
                }
                else
                {
                    j2.Checked = true;
                }
                leixing.InnerHtml = dr["leixin"].ToString();
                join_size.Value=dr["shengao"].ToString();
                join_phone.Value=dr["lianxi"].ToString();
                join_job.Value = dr["zhiye"].ToString();
                join_show.Value = dr["bisai"].ToString();
                join_train.Value = dr["zhuangye"].ToString();
                geren.InnerHtml = "<a href='../" + dr["gerenpic"].ToString() + "' target=_blank>查看图片</a>";
                shenghuo.InnerHtml = "<a href='../" + dr["shenghuopic"].ToString() + "' target=_blank>查看图片</a>";
                yishu.InnerHtml = "<a href='../" + dr["yishupic"].ToString() + "' target=_blank>查看图片</a>";
            }
        }
    }
}
