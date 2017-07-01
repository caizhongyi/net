using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace WbSystem
{
    public partial class NoticeBarControl : UserControl
    {
        public NoticeBarControl()
        {
            InitializeComponent();
            this.Height = 25;
            lbNum.Top = 0;
            timer1.Interval = 10;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lbNum.Right < 0)
            {
                lbNum.Left = this.Width;
            }
            else
            {
                lbNum.Left--;
            }
        }
    }
}
