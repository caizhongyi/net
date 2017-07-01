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
using System.IO;
using System.Threading;

public partial class admin_ProductAdd1 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fun.bind(DropDownList2, "select * from user_producttype2", "type", "id");

          
            DropDownList3.Items.Clear();
            fun.bind(DropDownList3, "select * from user_productType3 where sjid=" + int.Parse(DropDownList2.SelectedValue), "type", "id");
         
        }
        string id = Request.QueryString["id"];
        if (fun.CheckStr(id) && fun.IsMatch(id))
        {
            Label1.Text = "修改产品";
            int pid = int.Parse(id);
            if (!IsPostBack)
            {
                string sql = string.Format("select * from user_product where id={0}", pid);
                DataTable dt = fun.GetDataTable(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    name.Value = dr["name"].ToString();
                    spec.Value = dr["spec"].ToString();

                    string id1 = fun.getById(dr["typeid"].ToString(), "id", "user_producttype3", "sjid");
                   
                    string id3 = fun.getById(id1, "id", "user_producttype2", "id");

                    DropDownList2.Items.Clear();
                    fun.bind(DropDownList2, "select * from user_productType2 " , "type", "id");

                    DropDownList3.Items.Clear();
                    fun.bind(DropDownList3, "select * from user_productType3 where sjid=" + id3, "type", "id");

                    DropDownList3.SelectedValue = dr["typeid"].ToString();
                  
                    DropDownList2.SelectedValue = id3;

                    number.Value = dr["product_num"].ToString();

                    makername.Value = dr["maker_name"].ToString();
                    address.Value = dr["maker_address"].ToString();
                    tel.Value = dr["maker_tel"].ToString();
                    explain.Value = dr["explain"].ToString();
                    oldprice.Value = dr["oldprice"].ToString();
                    newprice.Value = dr["newprice"].ToString();

                    ImageBig.ImageUrl = dr["picture"].ToString();
                    ImageSmall.ImageUrl = dr["smallpicture"].ToString();
                    if (dt.Rows[0]["istj"].ToString() == "是")
                    {
                        rdy.Checked = true;
                    }
                    else
                    {
                        rdn.Checked = true;
                    }
                }
            }

        }
        else
        {

            Label1.Text = "添加产品";
            if (!IsPostBack)
            {
                rdn.Checked = true;

            }
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string name = Request.Form["name"];
        if (!fun.CheckStr(name))
        {
            fun.AJAXalert(this, "名称不能为空");
        }
        else if (DropDownList3.SelectedValue == "0")
        {
            fun.AJAXalert(this, "请选择产品分类");
        }
        else
        {

            name = fun.GetSafeStr(name);
            string num = number.Value;
            int typeid = int.Parse(DropDownList3.SelectedValue);
            string spec = fun.GetSafeStr(Request.Form["spec"]);
            string address = fun.GetSafeStr(Request.Form["address"]);
            string markname = fun.GetSafeStr(Request.Form["makername"]);
            string tel = fun.GetSafeStr(Request.Form["tel"]);
            string oldprice = fun.GetSafeStr(Request.Form["oldprice"]);
            string newprice = fun.GetSafeStr(Request.Form["newprice"]);
            if (fun.CheckStr(oldprice))
            {
                if (!fun.IsMatch(oldprice))
                {
                    fun.AJAXalert(this, "原价格不能为非数字");
                    return;
                }
            }
            if (fun.CheckStr(newprice))
                if (!fun.IsMatch(newprice))
                {
                    {
                        fun.AJAXalert(this, "现价格不能为非数字");
                        return;
                    }
                }
            if (fun.CheckStr(num))
            {
                if (!fun.IsMatch(num))
                {
                    fun.AJAXalert(this, "库存不能为非数字");
                    return;
                }
            }
            string explain = fun.GetSafeStr(Request.Form["explain"]);
            string pic = "";
            string sqlstr = "";
            string smallpic = "";



            string sid = Request.QueryString["id"];
            if (fun.CheckStr(sid) && fun.IsMatch(sid))
            {

                int pid = int.Parse(sid);
                string sql = "select * from user_product where id=" + pid;
                DataRow dr = fun.GetDataTable(sql).Rows[0];
                if (fileBig.PostedFile.FileName == "")
                {

                    pic = dr["picture"].ToString();
                }
                else
                {

                    if (File.Exists(Server.MapPath(dr["picture"].ToString())))
                    {
                        File.Delete(Server.MapPath(dr["picture"].ToString()));

                    }
                    UploadFile_Single u = new UploadFile_Single();
                    u.IsUploadImage = true;
                    u.IsUseRandFileName = true;
                    if (u.Upload(fileBig.PostedFile))
                    {
                        pic = u.UploadPath;

                    }
                    else
                    {
                        fun.AJAXalert(this, u.UploadResultMessage);
                        return;
                    }
                }
                Thread.Sleep(1000);
                if (fileSmal.PostedFile.FileName == "")
                {

                    smallpic = dr["smallpicture"].ToString();
                }
                else
                {
                    if (File.Exists(Server.MapPath(dr["smallpicture"].ToString())))
                    {
                        File.Delete(Server.MapPath(dr["smallpicture"].ToString()));

                    }

                    UploadFile_Single u = new UploadFile_Single();
                    u.IsUploadImage = true;
                    u.IsUseRandFileName = true;
                    if (u.Upload(fileSmal.PostedFile))
                    {
                        smallpic = u.UploadPath;
                    }
                    else
                    {
                        fun.AJAXalert(this, u.UploadResultMessage);
                        return;
                    }
                }


                sqlstr = string.Format("update user_product set name='{0}',typeid={1},product_num='{2}',maker_name='{3}',maker_address='{4}',maker_tel='{5}',oldprice='{6}',newprice='{7}',picture='{8}',explain='{9}',spec='{10}',smallpicture='{11}',istj='{12}' where id={13}", name, typeid, num, markname, address, tel, oldprice, newprice, pic, explain, spec, smallpic, Request.Form["rdtj"], pid);

            }
            else
            {
                if (fileBig.PostedFile.FileName != "")//上传大图
                {
                    UploadFile_Single u = new UploadFile_Single();
                    u.IsUploadImage = true;
                    u.IsUseRandFileName = true;
                    if (u.Upload(fileBig.PostedFile))
                    {
                        pic = u.UploadPath;

                    }
                    else
                    {
                        fun.AJAXalert(this, u.UploadResultMessage);
                        return;
                    }
                }
                Thread.Sleep(1000);
                if (fileSmal.PostedFile.FileName != "")//上传小图
                {
                    UploadFile_Single u = new UploadFile_Single();
                    u.IsUploadImage = true;
                    u.IsUseRandFileName = true;
                    if (u.Upload(fileSmal.PostedFile))
                    {
                        smallpic = u.UploadPath;
                    }
                    else
                    {
                        fun.AJAXalert(this, u.UploadResultMessage);
                        return;
                    }
                }
                sqlstr = string.Format("insert into user_product(name,typeid,spec,product_num,maker_name,maker_address,maker_tel,oldprice,newprice,picture,explain,join_date,smallPicture,istj) values ('{0}',{1},'{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", name, typeid, spec, num, markname, address, tel, oldprice, newprice, pic, explain, DateTime.Now, smallpic, Request.Form["rdtj"]);
            }
            fun.DoSql(this, sqlstr, Request.Url.ToString());




        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = int.Parse(DropDownList2.SelectedValue);
        DropDownList3.Items.Clear();
        fun.bind(DropDownList3, "select * from user_productType3 where sjid=" + id, "type", "id");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        if (fun.CheckStr(id) && fun.IsMatch(id))
        {

            int pid = int.Parse(id);
            string sqlstr = string.Format("select * from user_product where id={0}", pid);
            DataTable dt = fun.GetDataTable(sqlstr);
            if (dt.Rows.Count > 0)
            {
                fun.delFile(Server.MapPath(dt.Rows[0]["picture"].ToString()));
                string sql = string.Format("update user_product set picture='' where id={0}", pid);
                fun.DoSql(this, sql, Request.Url.ToString());
            }



        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string id = Request.QueryString["id"];
        if (fun.CheckStr(id) && fun.IsMatch(id))
        {

            int pid = int.Parse(id);
            string sqlstr = string.Format("select * from user_product where id={0}", pid);
            DataTable dt = fun.GetDataTable(sqlstr);
            if (dt.Rows.Count > 0)
            {
                fun.delFile(Server.MapPath(dt.Rows[0]["smallpicture"].ToString()));
                string sql = string.Format("update user_product set smallpicture='' where id={0}", pid);
                fun.DoSql(this, sql, Request.Url.ToString());
            }


        }
    }
}
