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
        private static int NewsCount;
        NewsInfo ni = new NewsInfo();
        public NewsInfoForm()
        {
            InitializeComponent();
            this.Height = 25;
            lblText.Top = 5;
            timer1.Interval = 10;
            timer1.Enabled = true;
            timer1.Start();
            this.Top = 5;  
            
            DataSet ds = new DataSet();
            ds.ReadXml("NewsInfo.xml");
            try
            {
                NewsCount = ds.Tables[0].Rows.Count - 1;
            }
            catch
            {
                return;
            }
        }

        private void NewsInfoForm_Load(object sender, EventArgs e)
        {
            this.Location = new Point((System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width-this.Width)/2,0);
            //设置背景为透明
            this.BackColor = Color.FromArgb(255, 200, 0);
            this.TransparencyKey = BackColor;

        }
       void ReadXML()
        {
            DataSet ds = new DataSet();
            ds.ReadXml("NewsInfo.xml");
            try
            {
                if (NewsCount >= 0)
                {
                    string text = ds.Tables[0].Rows[NewsCount][0].ToString();
                    string wbadvid = ds.Tables[0].Rows[NewsCount][1].ToString();
                    string wbadvurl = ds.Tables[0].Rows[NewsCount][2].ToString();
                    string wbadvcontent = ds.Tables[0].Rows[NewsCount][3].ToString();
                    string advtime = ds.Tables[0].Rows[NewsCount][5].ToString();
                    ni.Newstext = text;
                    ni.Wb_Adv_Id = wbadvid;
                    ni.Wb_Adv_Url = wbadvurl;
                    ni.Wb_Adv_Content = wbadvcontent;
                    ni.Adv_Time = advtime;
                }
                else
                {

                    NewsCount = ds.Tables[0].Rows.Count - 1;
                    ni.Newstext = ds.Tables[0].Rows[NewsCount][0].ToString();
                    ni.Wb_Adv_Id = ds.Tables[0].Rows[NewsCount][1].ToString();
                    ni.Wb_Adv_Url = ds.Tables[0].Rows[NewsCount][2].ToString();
                    ni.Wb_Adv_Content = ds.Tables[0].Rows[NewsCount][3].ToString();
                    ni.Adv_Time = ds.Tables[0].Rows[NewsCount][5].ToString();
                }
            }
            catch
            {
                return;
            }
        }
        private void btnRed_Click(object sender, EventArgs e)
        {
            this.lblText.LinkColor = Color.Red;
        }

        private void btnWhite_Click(object sender, EventArgs e)
        {
            this.lblText.LinkColor = Color.White;
        }

        private void btnBule_Click(object sender, EventArgs e)
        {
            this.lblText.LinkColor = Color.Blue;
        }

        private void btnGreen_Click(object sender, EventArgs e)
        {
            this.lblText.LinkColor = Color.Green;
        }

        private void btnYellow_Click(object sender, EventArgs e)
        {
            this.lblText.LinkColor = Color.Yellow;
        }

        private void btnBlack_Click(object sender, EventArgs e)
        {
            this.lblText.LinkColor = Color.Black;
        }

        private void btnPurple_Click(object sender, EventArgs e)
        {
            this.lblText.LinkColor = Color.Purple;
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

        private void btnNewForm_Click(object sender, EventArgs e)
        {
            WriteNewInfoForm wnif = new WriteNewInfoForm();
            wnif.Show();

            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (lblText.Right < 0)
            {
                lblText.Left = this.Width-450;
                ReadXML();
                //读取新信息
                this.lblText.Text = ni.Newstext;
                NewsCount--;
            }
            else
            {
                lblText.Left--;
            }
        }
        public string GetText
        {
            get { return lblText.Text; }
            set { lblText.Text = value; }
        }
        public Color GetNewColor
        {
            get { return this.lblText.ForeColor; }
            set { this.lblText.ForeColor = value; }
        }

        private void lblText_MouseLeave(object sender, EventArgs e)
        {
            this.timer1.Start();
        }

        private void lblText_MouseMove(object sender, MouseEventArgs e)
        {
            this.timer1.Stop();
        }

        private void lblText_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DelOrUpdateNewsInfo doun = new DelOrUpdateNewsInfo();
            doun.Show();

            this.Close();
        }
    }
}