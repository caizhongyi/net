using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace czy.MyClass.Forms
{

    public  class WinFormScreenPrint
    {
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern int BitBlt(HandleRef hDC, int x, int y, int nWidth, int nHeight, HandleRef hSrcDC, int xSrc, int ySrc, int dwRop);
        public static void  ScreenClick(FileDialog saveFileDialog1)
        {
            int screenWidth = System.Windows.Forms.SystemInformation.VirtualScreen.Width;     //屏幕宽度
            int screenHeight = System.Windows.Forms.SystemInformation.VirtualScreen.Height;     //屏幕高度

            Bitmap bmSave = new Bitmap(screenWidth, screenHeight);
            Graphics g = Graphics.FromImage(bmSave);

            g.CopyFromScreen(0, 0, 0, 0, new Size(screenWidth, screenHeight), CopyPixelOperation.SourceCopy);
            g.Dispose();

            SaveFile(bmSave,saveFileDialog1);
        }

        public static void WindowClick(Form form, FileDialog saveFileDialog1)
        {
            Graphics gSrc = form.CreateGraphics();     //创建窗体的Graphics对象
            HandleRef hDcSrc = new HandleRef(null, gSrc.GetHdc());

            int width = form.Width - SystemInformation.FrameBorderSize.Width;     //获取宽度
            int height = form.Height - SystemInformation.FrameBorderSize.Height;     //获取高度

            const int SRCCOPY = 0xcc0020;     //复制图块的光栅操作码

            Bitmap bmSave = new Bitmap(width, height);     //用于保存图片的位图对象
            Graphics gSave = Graphics.FromImage(bmSave);     //创建该位图的Graphics对象
            HandleRef hDcSave = new HandleRef(null, gSave.GetHdc());     //得到句柄

            BitBlt(hDcSave, 0, 0, width, height, hDcSrc, 0, 0, SRCCOPY);

            gSrc.ReleaseHdc();
            gSave.ReleaseHdc();

            gSrc.Dispose();
            gSave.Dispose();

            SaveFile(bmSave,saveFileDialog1);
        }
        private static void  SaveFile(Bitmap bmSave, FileDialog saveFileDialog1)
        {
            if (DialogResult.OK == saveFileDialog1.ShowDialog())
            {
                string fileName = saveFileDialog1.FileName;
                if (1 == saveFileDialog1.FilterIndex)
                {
                    if (!fileName.EndsWith(".bmp"))
                    {
                        fileName += ".bmp";
                    }
                }
                else if (2 == saveFileDialog1.FilterIndex)
                {
                    if (!fileName.EndsWith(".jpg"))
                    {
                        fileName += ".jpg";
                    }
                }
                else if (3 == saveFileDialog1.FilterIndex)
                {
                    if (!fileName.EndsWith(".png"))
                    {
                        fileName += ".png";
                    }
                }
                Save(bmSave, fileName);
            }
        }

        private static void Save(Bitmap bm,string fileName)
        {
            if (fileName.EndsWith(".bmp"))
            {
                bm.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp);
            }
            else if (fileName.EndsWith(".jpg"))
            {
                bm.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            else if (fileName.EndsWith(".png"))
            {
                bm.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
            }
            bm.Dispose();
        }
    }

}
