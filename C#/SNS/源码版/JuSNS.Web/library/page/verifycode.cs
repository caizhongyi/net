using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Web.SessionState;
using NVelocity;


namespace JuSNS.Web.library.page
{
    public class verifycode : JuSNS.UI.Page.BasePage
    {
        public override void Page_Load(ref VelocityContext context)
        {
            this.GetVerfiyCode();
        }

        protected void GetVerfiyCode()
        {
            int nRet = 0;
            string checkCode = String.Empty;
            System.Random random = new Random();
            string strMark = string.Empty;
            int nNum1 = random.Next(30);
            int nNum2 = random.Next(20);
            int nNum3 = random.Next(12);
            int nNum4 = random.Next(10);
            int nMark = random.Next(10);
            if (nMark % 2 == 0)
            {
                strMark = "+";
            }
            else
            {
                strMark = "×";
            }
            if (strMark == "+")
            {
                nRet = nNum1 + nNum2;
                checkCode = nNum1.ToString() + strMark + nNum2.ToString() + "=";
            }
            else
            {
                nRet = nNum3 * nNum4;
                checkCode = nNum3.ToString() + strMark + nNum4.ToString() + "=";
            }
            HttpContext.Current.Session["JuSNSCheckCode"] = nRet;
            CreateImages(checkCode);
        }

        /// <summary>
        ///  生成验证图片
        /// </summary>
        /// <param name="checkCode">验证字符</param>
        protected void CreateImages(string checkCode)
        {
            int iwidth = (int)(checkCode.Length * 15);
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 30);
            Graphics g = Graphics.FromImage(image);
            g.Clear(Color.White);
            //定义颜色
            Color[] c = { Color.Red, Color.Blue, Color.Orange, Color.Green, Color.Black, Color.DarkBlue, Color.Red, Color.YellowGreen, Color.Red };
            //定义字体 
            string[] font = { "Arial", "Microsoft Sans Serif", "Comic Sans MS", "Verdana", "Candara", "Comic Sans MS" };
            Random rand = new Random();
            //随机输出噪点
            for (int i = 0; i < 1; i++)
            {
                int x = rand.Next(image.Width);
                int y = rand.Next(image.Height);
                g.DrawPie(new Pen(Color.Azure, 9), x, y, 6, 6, 1, 1);
            }

            //输出不同字体和颜色的验证码字符
            for (int i = 0; i < checkCode.Length; i++)
            {
                int cindex = rand.Next(7);
                int findex = rand.Next(6);
                Font _font = new System.Drawing.Font(font[findex], 14, System.Drawing.FontStyle.Bold);
                Brush b = new System.Drawing.SolidBrush(c[cindex]);
                int ii = 4;
                if ((i + 1) % 2 == 0)
                {
                    ii = 2;
                }
                g.DrawString(checkCode.Substring(i, 1), _font, b, 3 + (i * 12), ii);
            }

            //画一个边框
            g.DrawRectangle(new Pen(Color.Red, 0), 100, 0, image.Width - 1, image.Height - 1);
            //输出到浏览器
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ContentType = "image/jpeg";
            HttpContext.Current.Response.BinaryWrite(ms.ToArray());
            g.Dispose();
            image.Dispose();
        }
    }
}