using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WbSystem
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
            skinEngine1.SkinFile = "Wave.ssk";
            this.contextMenuStrip1.Items.Add(this.tsmiShow);
            this.contextMenuStrip1.Items.Add(this.tsmiExit);
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.notifyIcon1.Visible = false;
            Application.Exit();
        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
            {
                this.Visible = false;
                this.notifyIcon1.Visible = true;
            }

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks==2)
            {
                this.Visible = true;
                this.notifyIcon1.Visible = false;
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            Change();
        }

        private void tsmiShow_Click(object sender, EventArgs e)
        {
            this.Visible = true;
            this.notifyIcon1.Visible = false;
        }

        private void tsmiExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //实现窗口渐变
        private void Change()
        {
            this.Opacity = 0.5;
            for (int i = 0; i < 10; i++)
            {
                this.ClientSize = new System.Drawing.Size(779 + i, 608 + i);
                this.Opacity += 0.05;
                this.Refresh();
            }
        }

    }
}