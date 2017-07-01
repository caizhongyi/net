using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ScreenProtect
{
    public partial class Form1 : Form
    {
        private readonly int screenWidth;
        private readonly int screenHeight;
        private Bitmap bitmap = new Bitmap("ms.bmp");
        private Random random = new Random();
        private int x = 0;
        private int y = 0;

        public Form1()
        {
            InitializeComponent();
            Rectangle rect = Screen.PrimaryScreen.Bounds;
            screenWidth = rect.Width;
            screenHeight = rect.Height;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            x = random.Next(screenWidth);
            y = random.Next(screenHeight);
            if (x + bitmap.Width > screenWidth)
            {
                x = screenWidth - bitmap.Width;
            }
            if (y + bitmap.Height > screenHeight)
            {
                y = screenHeight - bitmap.Height;
            }
            this.Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, x, y, bitmap.Width,bitmap.Height);
        }

        private void Form1_MouseCaptureChanged(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Application.Exit();
        }
    }
}