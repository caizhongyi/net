using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace YZWBSM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           // textBox1.Text = ClsConstant.snewday;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int irow = ClsConstant.mydt.Rows.Count;
            for (int i = 0; i < irow; i++)
            {
                string s1 = ClsConstant.mydt.Rows[i][0].ToString();
                string s2 = ClsConstant.mydt.Rows[i][1].ToString();
                string s3 = "IP:" + s1 + " DATA:" + s2;
                textBox1.Text  = s3;
                ClsConstant.bListenNewD = false;
            }
            
        }
    }
}