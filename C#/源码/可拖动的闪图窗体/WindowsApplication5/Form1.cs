using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsApplication5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            //将窗体的背景色（红色）设置为透明
            InitializeComponent();
            Bitmap bmp = (Bitmap)BackgroundImage;
            bmp.MakeTransparent(Color.Red);
            TransparencyKey = BackColor;
        }
        //拖动label1事件
        private Point _mouseDownPosition;
        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            _mouseDownPosition = e.Location;
         
        }
        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
          if ((e.Button & MouseButtons.Left) == MouseButtons.Left)
            {
                int offsetx = e.X - _mouseDownPosition.X;
                int offsety = e.Y - _mouseDownPosition.Y;
                Location = new Point(Location.X + offsetx, Location.Y + offsety);
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Opacity > 0)
            {
                Opacity -= 0.05;
            }
            else
            {
                timer1.Enabled = false;
                Application.Exit();
            }
        }

        //渐变透明关闭窗体
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }
        

        
     
       
    }
}