using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScreenSavers
{
    public partial class FormScreen : Form
    {
        //定义一个屏保宽度
        private readonly int ScreenWidth;
        //定义一个屏保高度
        private readonly int ScreenHight;
        //定义初始屏保时的X轴坐标
        private int XStart = 0;
        //定义初始屏保时的Y轴坐标
        private int YStart = 0;
        //定义X，Y轴的值
        private int x = 0;
        private int y = 0;
        //初始化随机数
        private  Random rd = new Random();
        //定义一个图片
        private Bitmap bitmap = new Bitmap("标志.jpg");

        public FormScreen()
        {
            InitializeComponent();

            //隐藏光标
            Cursor.Hide();
            //读取显示器的宽度，高度
            Rectangle rt = Screen.PrimaryScreen.Bounds;
            ScreenWidth = rt.Width;
            ScreenHight = rt.Height;
        }

        private void FormScreen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Cursor.Show();
                Application.Exit();
            }
        }

        private void FormScreen_MouseMove(object sender, MouseEventArgs e)
        {
            //记录鼠标刚刚开始的坐标
            if (XStart == 0 && YStart == 0)
            {
                XStart = e.X;
                YStart = e.Y;
                return;
            }
            else if( e.X != XStart||e.Y != YStart)
            {
                Cursor.Show();
                Application.Exit();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //判断图片的运动的轨迹
            x = rd.Next(ScreenWidth);
            y = rd.Next(ScreenHight);
            if (x + bitmap.Width > ScreenWidth)
            {
                x = ScreenWidth - bitmap.Width;
            }
            if (y + bitmap.Height > ScreenHight)
            {
                y = ScreenHight - bitmap.Height;
            }
            //使得该区域无效
            this.Invalidate();
        }

        private void FormScreen_Paint(object sender, PaintEventArgs e)
        {
            //描绘图片
            e.Graphics.DrawImage(bitmap,x,y,bitmap.Width,bitmap.Height);
        }
    }
}