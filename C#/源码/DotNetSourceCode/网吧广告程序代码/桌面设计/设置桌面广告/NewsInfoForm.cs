using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WbSystem
{
    public partial class NewsInfoForm : Form
    {
        public NewsInfoForm()
        {
            InitializeComponent();
        }

        private void NewsInfoForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width-this.Width)/2,0);
            //设置背景为透明
            this.BackColor = Color.FromArgb(255, 200, 0);
            this.TransparencyKey = BackColor;

            this.noticeBar1.GetText = "云舟计算机网络公司　　　　新信息.....";

        }

        private void btnRed_Click(object sender, EventArgs e)
        {
            this.noticeBar1.GetNewColor = Color.Red;
        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            this.noticeBar1.GetNewColor = Color.White;
        }

        private void btnBule_Click(object sender, EventArgs e)
        {
            this.noticeBar1.GetNewColor = Color.Blue;
        }

        private void btnGreen_Click(object sender, EventArgs e)
        {
            this.noticeBar1.GetNewColor = Color.Green;
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            this.noticeBar1.GetNewColor = Color.Yellow;
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            this.noticeBar1.GetNewColor = Color.Black;
        }

        private void btnPurple_Click(object sender, EventArgs e)
        {
            this.noticeBar1.GetNewColor = Color.Purple;
        }

        private void noticeBar1_MouseMove(object sender, MouseEventArgs e)
        {
            this.btnRed.Visible = true;
            this.btnWhite.Visible = true;
            this.btnPurple.Visible = true;
            this.btnYellow.Visible = true;
            this.btnGreen.Visible = true;
            this.btnBule.Visible = true;
            this.btnBlack.Visible = true;
        }

        private void noticeBar1_MouseLeave(object sender, EventArgs e)
        {
            this.btnRed.Visible = false;
            this.btnWhite.Visible = false;
            this.btnPurple.Visible = false;
            this.btnYellow.Visible = false;
            this.btnGreen.Visible = false;
            this.btnBule.Visible = false;
            this.btnBlack.Visible = false;
        }
    }
}