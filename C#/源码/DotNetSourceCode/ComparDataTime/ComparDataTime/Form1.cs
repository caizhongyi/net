using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ComparDataTime
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime oldtime =Convert.ToDateTime("2008-12-14 07:12:00");
            DateTime newtime = Convert.ToDateTime("2008-12-14 07:12:01");
            if (DateTime.Compare(oldtime, newtime) > 0)
                MessageBox.Show(oldtime.ToString());
            else
                MessageBox.Show(newtime.ToString());
        }
    }
}