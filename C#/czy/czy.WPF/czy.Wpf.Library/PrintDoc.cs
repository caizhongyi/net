using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace czy.Wpf.Library
{
    public class PrintDoc
    {
        #region 对像
        /// <summary>
        /// 预览对像
        /// </summary>
        private PrintPreviewDialog ppd = new PrintPreviewDialog();

        public PrintPreviewDialog Ppd
        {
            get { return ppd; }
            set { ppd = value; }
        }
        /// <summary>
        /// 打印对像
        /// </summary>
        private PrintDocument pd = new PrintDocument();

        public PrintDocument Pd
        {
            get { return pd; }
            set { pd = value; }
        }
        #endregion

        #region 变量

        #endregion

        #region 事件
        public event PrintPageEventHandler PrintPage;
      
        #endregion
     
        public PrintDoc()
        {
            ppd.Document = pd;
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            
        }

        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            if(PrintPage!=null){PrintPage(sender,e );}
        }

        public void ShowViewDialog()
        {
            ppd.Document = pd;
            ppd.ShowDialog();
        }
        public void ShowView()
        {
            ppd.Document = pd;
            ppd.Show();
        }
     
    }
    
}
