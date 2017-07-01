using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace czy.Silverlight.Library.Data
{
    public class IdentifyingCode
    {

        public static WriteableBitmap CreateImage(int iNumBit, int iw, int ih,out string number)
        {
            string Text=ValidateNum(iNumBit);
            Random r = new Random(DateTime.Now.Millisecond);
            Grid Gx = new Grid();
            Canvas cv1 = new Canvas();
            for (int i = 0; i < 6; i++)
            {
                Polyline p = new Polyline();
                for (int ix = 0; ix < r.Next(3, 6); ix++)
                {
                    p.Points.Add(new Point(r.NextDouble() * iw,
                        r.NextDouble() * ih));
                }
                byte[] Buffer = new byte[3];
                r.NextBytes(Buffer);
                SolidColorBrush SC = new SolidColorBrush(Color.FromArgb(255,
                    Buffer[0], Buffer[1], Buffer[2]));
                p.Stroke = SC;
                p.StrokeThickness = 0.5;
                cv1.Children.Add(p);
            }
            Canvas cv2 = new Canvas();
            int y = 0;
            int lw = 6;
            double w = (iw - lw) / Text.Length;
            int h = (int)ih;
            foreach (char x in Text)
            {
                byte[] Buffer = new byte[3];
                r.NextBytes(Buffer);
                SolidColorBrush SC = new SolidColorBrush(Color.FromArgb(255,
                    Buffer[0], Buffer[1], Buffer[2]));
                TextBlock t = new TextBlock();
                t.TextAlignment = TextAlignment.Center;
                t.FontSize = r.Next(h - 3, h);
                t.Foreground = SC;
                t.Text = x.ToString();
                t.Projection = new PlaneProjection()
                {
                    RotationX = r.Next(-30, 30),
                    RotationY = r.Next(-30, 30),
                    RotationZ = r.Next(-10, 10)
                };
                cv2.Children.Add(t);
                Canvas.SetLeft(t, lw / 2 + y * w);
                Canvas.SetTop(t, 0);
                y++;
            }
            Gx.Children.Add(cv1);
            Gx.Children.Add(cv2);
            number=Text;
            return czy.Silverlight.Library.DrawImage.DrawStringImage(Gx);
             
        }
        # region 获取随机数
        /// <summary>
        /// 获取随机数。
        /// </summary>
        /// <param name="iNumBit">获取几位</param>
        /// <returns>获取的随机数</returns>
        public static string ValidateNum(int iNumBit)
        {
            string strValidate = "1,2,3,4,5,6,7,8,9,0,a,b,c,d,e,f,g,h,i,j,k,l,m,n,o,p,q,r,s,t,u,v,w,s,y,z";
            string[] strVArray = strValidate.Split(new Char[] { ',' });
            string strVNum = "";
            int iTemp = -1;

            Random objRandom = new Random();

            for (int i = 1; i < iNumBit + 1; i++)
            {
                if (iTemp != -1)
                {
                    objRandom = new Random(i * iTemp * unchecked((int)DateTime.Now.Ticks));
                }

                int iT = objRandom.Next(9);

                if (iTemp != -1 && iTemp == iT)
                {
                    return ValidateNum(iNumBit);
                }

                iTemp = iT;
                strVNum += strVArray[iT];
            }
            return strVNum;
        }
        # endregion
    }
}