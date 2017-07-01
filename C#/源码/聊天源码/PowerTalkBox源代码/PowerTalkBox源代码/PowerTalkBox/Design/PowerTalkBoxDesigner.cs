using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.UI;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.Design;
using System.Resources;
using System.Collections;
using System.Collections.Specialized;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using PowerTalkBox;
namespace PowerTalkBoxContrls.Design
{
    public class PowerTalkBoxDesigner : ControlDesigner
    {
        /// <summary>
        /// ×îÍâ²ãµÄ¿ò¿ò
        /// </summary>
        Panel PTB = new Panel();
        private PowerTalkBox.PowerTalkBox ptbc = null;
        public override void Initialize(IComponent component)
        {
            ptbc = (PowerTalkBox.PowerTalkBox)component;
            base.Initialize(component);
        }
        public string TableContentDesigner()
        {            

         
            StringWriter sw = new StringWriter();

            HtmlTextWriter htw = new HtmlTextWriter(sw);
            HtmlTable t = new HtmlTable();
            t.CellPadding = 3;
            t.CellSpacing = 0;
            t.BorderColor = "#6699cc";
            t.BgColor = "#6699cc";
            t.Width = ptbc.Width.ToString();
            t.Height = ptbc.Height.ToString();
            HtmlTableRow tr = new HtmlTableRow();
            HtmlTableCell td = new HtmlTableCell();
            td.VAlign = "top";
            td.Align = "center";

            // inner table for iframe
            HtmlTable iframe = new HtmlTable();
            iframe.BgColor = "#FFFFFF";
            iframe.Width = "600px";
            iframe.Height = "500px";
            iframe.CellPadding = 0;
            iframe.CellSpacing = 0;
            iframe.Style.Add("border", "1 solid " + "#6699cc");
            HtmlTableRow tr2 = new HtmlTableRow();
            HtmlTableCell td2 = new HtmlTableCell();
            td2.VAlign = "middle";
            td2.Align = "center";
            td2.Controls.Add(new LiteralControl("<b><font face=arial size=2><font color=green>Power</font>TalkBox:</b> " + ptbc.ID+ "</font>"));
            tr2.Cells.Add(td2);
            iframe.Rows.Add(tr2);

            td.Controls.Add(iframe);
            td.Controls.Add(new LiteralControl("<br><br><br>"));
            tr.Cells.Add(td);
            t.Rows.Add(tr);
            t.RenderControl(htw);
            return sw.ToString();
        }
       public override string GetDesignTimeHtml()
     {
   
         return TableContentDesigner();
        }
    }
}
