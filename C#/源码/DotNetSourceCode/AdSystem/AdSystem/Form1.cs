using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SetWallpaper;


namespace AdSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            SetWallpaper.IChangeWallpaperService i = Factory.GetNewPicture();
            i.UpdateWallpaper();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
            this.Hide();
            this.ShowInTaskbar = false;
        }
    }
}