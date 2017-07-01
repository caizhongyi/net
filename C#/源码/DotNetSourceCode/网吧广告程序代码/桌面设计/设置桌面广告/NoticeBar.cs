using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace GunDongDeZi
{
    public partial class NoticeBar : UserControl
    {
        public NoticeBar()
        {
            InitializeComponent();
            this.Height = 25;
            lblText.Top =0;
            timer1.Interval = 10;
            timer1.Enabled = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lblText.Right < 0)
            {
                lblText.Left = this.Width;
            }
            else
            {
                lblText.Left--;
            }
        }
        public string GetText
        {
            get { return lblText.Text; }
            set { lblText.Text= value; }
        }
        public Color GetNewColor
        {
            get { return this.lblText.ForeColor; }
            set { this.lblText.ForeColor = value; }
        }

        private void lblText_MouseMove(object sender, MouseEventArgs e)
        {
            timer1.Stop();
        }

        private void lblText_MouseLeave(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void lblText_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
 
            }
        }
    }
}
