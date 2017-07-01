using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace czy.MyClass.Web.UI
{
    /// <summary>
    /// 创建CSS模型类
    /// </summary>
    public sealed partial class CssModel
    {
        /// <summary>
        /// 生成Css模版
        /// </summary>
        /// <param name="savePath">保存路径</param>
        public static void CreateCssModel(string savePath)
        {
            string cssPath=savePath+"/css";
            Directory.CreateDirectory(cssPath);

            StringBuilder cssSb = new StringBuilder();
            cssSb.AppendLine("/*Common公用样式*/");
            cssSb.AppendLine("*{}");
            cssSb.AppendLine("body{font-size:13px;font-family:宋体;}");
            cssSb.AppendLine("ul{}");
            cssSb.AppendLine("img{}");
            cssSb.AppendLine(".defaultTxt{ border:solid 1px #9db3e6; width:130px;}");
            

            cssSb.AppendLine("/*CommonStruct公用结构样式*/");
            cssSb.AppendLine("defaultUl{padding:0px; margin:0px; list-style-type:none;}");
            cssSb.AppendLine("defaultUl li{}");
            cssSb.AppendLine("defaultA{text-decoration:none; color:#514e4e }");
            cssSb.AppendLine("defaultA:hover{ text-decoration:underline}");

            cssSb.AppendLine(".contenter{ width:980px;margin-left:auto;margin-right:auto; overflow:hidden;}");
            cssSb.AppendLine(".header{ height:200px; margin-bottom:10px; background-color:#999999;overflow:hidden;}");
            cssSb.AppendLine(".menu{ height:30px;margin-bottom:10px;  background-color:Red;overflow:hidden;}");
            cssSb.AppendLine(".content{ margin-bottom:10px;overflow:hidden;}");
            cssSb.AppendLine(".content .sideBarLeft{ float:left;height:500px; width:200px; margin-right:10px; background-color:Blue;overflow:hidden;}");
            cssSb.AppendLine(".content .sideBarRight{ float:right;height:500px; width:200px;margin-left:10px; background-color:Green;overflow:hidden;}");
            cssSb.AppendLine(".content .mainContent{height:500px; background-color:Black;overflow:hidden;}");
            cssSb.AppendLine(".footer{height:50px; background-color:#9db3e6;overflow:hidden;}");


            cssSb.AppendLine("/*Default样式*/");
            cssSb.AppendLine("");
            cssSb.AppendLine("/*Page1样式*/");
            cssSb.AppendLine("");
            cssSb.AppendLine("/*Page2样式*/");
            cssSb.AppendLine("");
  

            MyStream.StreamWrite(cssPath + "/default.css", Encoding.UTF8, cssSb.ToString());

            StringBuilder sbHtml = new StringBuilder();
            sbHtml.AppendLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sbHtml.AppendLine("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sbHtml.AppendLine("<head>");
            sbHtml.AppendLine("<title>CssDemo</title>");
            sbHtml.AppendLine("<link   type=\"text/css\" href=\"css/default.css\"  rel=\"Stylesheet\"/>");
            sbHtml.AppendLine("<meta http-equiv=\"X-UA-Compatible\" content=\"IE=7\" /> ");
            sbHtml.AppendLine("<meta name=\"keywords\" content=\"keywords\" />");
            sbHtml.AppendLine("<meta name=\"description\" content=\"description\" />");
          
            sbHtml.AppendLine("</head>");
            sbHtml.AppendLine("<body>");
            sbHtml.AppendLine("<form id=\"form1\" >");

            sbHtml.AppendLine("<div class=\"contenter\">");

            sbHtml.AppendLine("<div class=\"header\">");
            sbHtml.AppendLine("</div>");

            sbHtml.AppendLine("<div class=\"menu\">");
            sbHtml.AppendLine("</div>");

            sbHtml.AppendLine("<div class=\"content\">");
            sbHtml.AppendLine("<div class=\"sideBarLeft\">");
            sbHtml.AppendLine("</div>");
            sbHtml.AppendLine("<div class=\"sideBarRight\">");
            sbHtml.AppendLine("</div>");
            sbHtml.AppendLine("<div class=\"mainContent\">");
            sbHtml.AppendLine("</div>");

            sbHtml.AppendLine("</div>");

            sbHtml.AppendLine("<div class=\"footer\">");
            sbHtml.AppendLine("</div>");

            sbHtml.AppendLine("</div>");

            sbHtml.AppendLine("</form>");
            sbHtml.AppendLine("</body>");
            sbHtml.AppendLine("</html>");
            MyStream.StreamWrite(savePath + "/default.html", Encoding.UTF8, sbHtml.ToString());
        }
    }
}
