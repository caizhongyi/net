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
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Net;
using MySocket;


public partial class adminAdManger : System.Web.UI.Page
{
   
    public string GetdonwLoadPath(string IP,string type,string filename)
    {
        string picturespath = @"\Adpictures\";
        string path="";
        switch (type)
        {
            case "type1": path =IP +picturespath +@"type1\" + filename;
                break;
            case "type2": path = IP + picturespath + @"type2\" + filename;
                break;
            case "type3": path = IP + picturespath + @"type3\" + filename;
                break;
            case "type4": path = IP + picturespath + @"type4\" + filename;
                break;
            case "backgroupWall": path = IP+picturespath + @"backgroupWall\" + filename;
                break;
        }
        return path;
    }
     private void getMessage()
    {  
        string path=GetdonwLoadPath("192.168.1.1",ad_type.Text ,"aa");;
        string serverIP=System .Configuration.ConfigurationManager.AppSettings["ipaddress"];
        string serverProt=System .Configuration.ConfigurationManager.AppSettings["port"];
        MySocket.Imysocket socket = MySocket.Factory.Getmysocket();
        socket.BeginSend(serverIP, serverProt, path);
    }
    BBL.IAdInfo adInfo = BBL.BBLFactory.GetAdInfo();
    BBL.IWbInfo wbInfo = BBL.BBLFactory.GetWbInfo();
    protected void Page_Load(object sender, EventArgs e)
    {
        adInfo.SelectAdInfo();
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Thread th = new Thread(new ThreadStart(getMessage));
        th.Start();
        
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        ad_name.Text = GridView1.SelectedRow.Cells[1].Text;
        ad_type.Text = GridView1.SelectedRow.Cells[3].Text;

    }
}
