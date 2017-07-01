using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using SetWallpaper;

namespace AdSystem
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            this.Top = 0;
            this.Left = SystemInformation.PrimaryMonitorSize.Width - this.Width;
            this.Height = SystemInformation.PrimaryMonitorSize.Height;
            this.ShowInTaskbar = false;

            SetWallpaper.IChangeAdPictureService iaps = Factory.GetNewAdPicture();
           string path = iaps.UpdateAdPicture();
           pictureBox1.Image = System.Drawing.Image.FromFile(path);
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }
    }
}