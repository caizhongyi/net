using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime .Remoting;
using System.Runtime .InteropServices;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace printscreen
{
    public class PrintScreen : printscreen.IPrintScreen
    {
         public void printScreen(string path,string name)
        {
               // this.Hide(); 
                IntPtr dc1 = CreateDC ( "DISPLAY" , null , null , ( IntPtr ) null ) ; 
                //创建显示器的DC 
                Graphics g1 = Graphics.FromHdc ( dc1 ) ; 
                //由一个指定设备的句柄创建一个新的Graphics对象 
                Bitmap MyImage = new Bitmap ( Screen.PrimaryScreen.Bounds.Width , Screen.PrimaryScreen.Bounds.Height , g1 ) ; 
                //根据屏幕大小创建一个与之相同大小的Bitmap对象 
                Graphics g2 = Graphics.FromImage ( MyImage ) ; 
                //获得屏幕的句柄 
                IntPtr dc3 = g1.GetHdc ( ) ; 
                //获得位图的句柄 
                IntPtr dc2 = g2.GetHdc ( ) ; 
                //把当前屏幕捕获到位图对象中 
                BitBlt ( dc2 , 0 , 0 , Screen.PrimaryScreen.Bounds.Width , Screen.PrimaryScreen.Bounds.Height , dc3 , 0 , 0 , 13369376 ) ; 
                //把当前屏幕拷贝到位图中 
                g1.ReleaseHdc ( dc3 ) ; 
                //释放屏幕句柄 
                g2.ReleaseHdc ( dc2 ) ; 
                //释放位图句柄 
                MyImage.Save(path+name, ImageFormat.MemoryBmp); 
                //MessageBox.Show ( "保存ＯＫ" ) ; 
                //this.Show(); 
                } 

                [ System.Runtime.InteropServices.DllImportAttribute ( "gdi32.dll" ) ] 
                private static extern bool BitBlt ( 
                IntPtr hdcDest , //目标设备的句柄 
                int nXDest , // 目标对象的左上角的X坐标 
                int nYDest , // 目标对象的左上角的X坐标 
                int nWidth , // 目标对象的矩形的宽度 
                int nHeight , // 目标对象的矩形的长度 
                IntPtr hdcSrc , // 源设备的句柄 
                int nXSrc , // 源对象的左上角的X坐标 
                int nYSrc , // 源对象的左上角的X坐标 
                System.Int32 dwRop // 光栅的操作值 
                ) ; 

                [ System.Runtime.InteropServices.DllImportAttribute ( "gdi32.dll" ) ] 
                private static extern IntPtr CreateDC ( 
                string lpszDriver , // 驱动名称 
                string lpszDevice , // 设备名称 
                string lpszOutput , // 无用，可以设定位"NULL" 
                IntPtr lpInitData // 任意的打印机数据 
                 ); 
        }
    }

