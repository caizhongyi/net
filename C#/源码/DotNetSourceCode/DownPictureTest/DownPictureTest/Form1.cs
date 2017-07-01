using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Files;
using System.Collections;

namespace DownPictureTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[,] Picture = null;
            IFiles file = FilesFactory.GetDownLoadFiles();

            DataSet ds = new DataSet();
            ds.ReadXml("AdvInfo.xml");
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string type = ds.Tables[0].Rows[i][3].ToString();
                switch (type)
                {
                    case "2":
                        string s = ds.Tables[0].Rows[i][2].ToString();
                        Picture[i, 0] = s;//背景广告  
                        break;
                    case "3":
                        Picture[i, 1] = ds.Tables[0].Rows[i][2].ToString();//IE首页广告
                        break;
                    case "5":
                        string t = ds.Tables[0].Rows[i][2].ToString();
                        Picture[i, 2] = t;//IE置顶广告
                        break;
                    case "6":
                        Picture[i, 3] = ds.Tables[0].Rows[i][2].ToString();//右侧广告1 
                        break;
                    case "7":
                        Picture[i, 4] = ds.Tables[0].Rows[i][2].ToString();//右侧广告2 
                        break;
                    case "8":
                        Picture[i, 5] = ds.Tables[0].Rows[i][2].ToString();//右侧广告3 
                        break;
                    case "9":
                        Picture[i, 6] = ds.Tables[0].Rows[i][2].ToString();//右侧广告4 
                        break;
                    case "10":
                        Picture[i, 7] = ds.Tables[0].Rows[i][2].ToString();//右侧广告5 
                        break;
                }
            }
        }
    }
}