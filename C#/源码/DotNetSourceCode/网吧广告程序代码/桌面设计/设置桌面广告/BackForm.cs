using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SetWallpaper;
using System.Diagnostics;

namespace WbSystem
{
    public partial class BackForm : Form
    {
        public static int Formwidth;
        public BackForm()
        {
            InitializeComponent();
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            Formwidth = this.Width;
        }

        private void BackForm_Load(object sender, EventArgs e)
        {
            //����AdͼƬλ��
            this.Top = 0;
            this.Left = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width - this.Width;
            this.Height = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height;

            //�������汳��
            Factory.GetNewPicture().UpdateWallpaper();

            ////���ؽ���
            //hideproess.hideproess hd = new hideproess.hideproess();
            //hd.HideProess(f1);

            //��ʾAd��洰��
            AdForm bf = new AdForm();
            bf.ShowDialog();
        }
    }
}