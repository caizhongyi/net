using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace czy.MyClass.WinForm
{
    public class WPrintDoc
    {
        #region 对像
        /// <summary>
        /// 预览对像
        /// </summary>
        public PrintPreviewDialog ppd = new PrintPreviewDialog();
        /// <summary>
        /// 打印对像
        /// </summary>
        public PrintDocument pd = new PrintDocument();
        #endregion

        #region 变量
        private Margins _margin = new Margins(20, 20, 20, 20);
        private PaperSize _paperSize = new PaperSize("PageName", 800, 600);

        public PaperSize PaperSize
        {
            get { return _paperSize; }
            set { pd.DefaultPageSettings.PaperSize = value; _paperSize = value; }
        }
        public Margins Margin
        {
            get {  return _margin; }
            set { pd.DefaultPageSettings.Margins = value; _margin = value; }
        }
        #endregion

        #region 事件
        public delegate void EventHandle(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e);
        public event EventHandle Printting;
        #endregion
       //public string[] GetFontFamliy()
       //{
 
       //}
        public WPrintDoc()
       {
           pd.DefaultPageSettings.PaperSize =_paperSize;
           pd.DefaultPageSettings.Margins = _margin;
           pd.PrintPage +=new PrintPageEventHandler(pd_PrintPage);
       }

       public void ShowPrintPreview()
       {
           if (ppd.ShowDialog() == DialogResult.OK)
           {
             //  pd.PrinterSettings=ppd.
           }
       }
       public void pd_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
       {
           if (Printting != null) { Printting(sender, e); e.Graphics.Dispose(); }
       }

       public void Print()
       {
           pd.Print();
       }
    }

  
    
}
