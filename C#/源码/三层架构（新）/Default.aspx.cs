using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string nameSpace = this.ns.Text;
        string className = this.tn.Text;
        string[] columns = this.cn.Text.Split(',');
        string[] ctypes = this.ct.Text.Split(',');
        string path = Server.MapPath("~/Code")+"/model";
        
        try
        {
            Directory.CreateDirectory(path);

            StreamWriter sw = File.CreateText(path+"/"+className+".cs");
            sw.WriteLine("using System;");
            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("using System.Text;");
            sw.WriteLine("");
            sw.WriteLine("///////////////////////////////////////");
            sw.WriteLine("///////XXXX工作室");
            sw.WriteLine("//////作者:XXXXX");
            sw.WriteLine("//////版权所有 代码生成器自动生成请勿乱改");
            sw.WriteLine("//////创建时间:"+System.DateTime.Now);
            sw.WriteLine("///////////////////////////////////////");

            sw.WriteLine("");
            sw.WriteLine("");
            sw.WriteLine("namespace "+nameSpace);
            sw.WriteLine("{");
            sw.WriteLine("    public class "+className);
            sw.WriteLine("    {");

            for (int i = 0; i < columns.Length;i++ )
            {
                string col = columns[i];
                string type = ctypes[i];
                sw.WriteLine("        private " + type + " _" + col + ";");
                sw.WriteLine("");
                sw.WriteLine("        public "+type+" "+col);
                sw.WriteLine("        {");
                sw.WriteLine("            get { return _"+col+"; }");
                sw.WriteLine("            set { _"+col+" = value; }");
                sw.WriteLine("        }");

            }

            sw.WriteLine("    }");
            sw.WriteLine("}");





            sw.Close();

        }
        catch (Exception ex)
        {

            Response.Write(ex.Message);
        }
    }
}
