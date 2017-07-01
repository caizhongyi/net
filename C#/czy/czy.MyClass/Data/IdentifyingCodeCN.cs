using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Drawing;

namespace czy.MyClass.Data
{
    /// <summary>
    /// 生成中文验证码
    /// </summary>
    public class IdentifyingCodeCN
    {
        #region 成员
        int ImageWidth = 100;
        int ImageHeight = 20;
        #endregion

        #region  生成图弄验证码
        /// <summary>
        /// 获取图弄验证码
        /// </summary>
        /// <param name="page">当前页</param>
        /// <returns>验证码</returns>
        public string GetVerificationCode(Page page)
        {
            Encoding gb = Encoding.GetEncoding("gb2312");

            //调用函数产生4个随机中文汉字编码 
            object[] bytes = CreateRegionCode(4);

            //根据汉字编码的字节数组解码出中文汉字 
            string str1 = gb.GetString((byte[])Convert.ChangeType(bytes[0], typeof(byte[])));
            string str2 = gb.GetString((byte[])Convert.ChangeType(bytes[1], typeof(byte[])));
            string str3 = gb.GetString((byte[])Convert.ChangeType(bytes[2], typeof(byte[])));
            string str4 = gb.GetString((byte[])Convert.ChangeType(bytes[3], typeof(byte[])));
            string yzm = str1 + str2 + str3 + str4;

            CreateImage(yzm, page, ImageWidth, ImageHeight);
            page.Response.Cache.SetCacheability(HttpCacheability.NoCache); //IE点后退时验证码也改变 

            return yzm;
        }

        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="strlength">随机数的个数</param>
        /// <returns>返回随机数</returns>
        public static object[] CreateRegionCode(int strlength)
        {
            //定义一个字符串数组储存汉字编码的组成元素 
            string[] rBase = new String[16] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };

            Random rnd = new Random();

            //定义一个object数组用来 
            object[] bytes = new object[strlength];




            /**/
            /*每循环一次产生一个含两个元素的十六进制字节数组，并将其放入bject数组中 
         每个汉字有四个区位码组成 
         区位码第1位和区位码第2位作为字节数组第一个元素 
         区位码第3位和区位码第4位作为字节数组第二个元素 
        */
            for (int i = 0; i < strlength; i++)
            {
                //区位码第1位 
                int r1 = rnd.Next(11, 14);
                string str_r1 = rBase[r1].Trim();

                //区位码第2位 
                rnd = new Random(r1 * unchecked((int)DateTime.Now.Ticks) + i);
                int r2;
                if (r1 == 13)
                {
                    r2 = rnd.Next(0, 7);
                }
                else
                {
                    r2 = rnd.Next(0, 16);
                }
                string str_r2 = rBase[r2].Trim();

                //区位码第3位 
                rnd = new Random(r2 * unchecked((int)DateTime.Now.Ticks) + i);
                int r3 = rnd.Next(10, 16);
                string str_r3 = rBase[r3].Trim();




                //区位码第4位 
                rnd = new Random(r3 * unchecked((int)DateTime.Now.Ticks) + i);
                int r4;
                if (r3 == 10)
                {
                    r4 = rnd.Next(1, 16);
                }
                else if (r3 == 15)
                {
                    r4 = rnd.Next(0, 15);
                }
                else
                {
                    r4 = rnd.Next(0, 16);
                }
                string str_r4 = rBase[r4].Trim();

                //定义两个字节变量存储产生的随机汉字区位码 
                byte byte1 = Convert.ToByte(str_r1 + str_r2, 16);
                byte byte2 = Convert.ToByte(str_r3 + str_r4, 16);
                //将两个字节变量存储在字节数组中 
                byte[] str_r = new byte[] { byte1, byte2 };

                //将产生的一个汉字的字节数组放入object数组中 
                bytes.SetValue(str_r, i);

            }

            return bytes;

        }
   

        
        /// <summary>
        /// 生成图型
        /// </summary>
        /// <param name="strNum">字符窜</param>
        public static void CreateImage(string strNum, Page page, int imageWidth, int imageHeight)
        {
            if (strNum == null || strNum.Trim() == String.Empty)
            { return; }
            else
            {

                System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight);
                Graphics g = Graphics.FromImage(image);

                try
                {
                    //生成随机生成器
                    Random random = new Random();

                    //清空图片背景色
                    g.Clear(Color.FromArgb(255, 255, 255));

                    Font font = new System.Drawing.Font("Arial", 12, (System.Drawing.FontStyle.Bold));
                    System.Drawing.Drawing2D.LinearGradientBrush brush = new System.Drawing.Drawing2D.LinearGradientBrush(new Rectangle(0, 0, image.Width, image.Height), Color.Maroon, Color.Coral, 90f, true);
                    g.DrawString(strNum, font, brush, -3, -1);


                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    page.Response.ClearContent();
                    page.Response.ContentType = "image/Bmp";
                    page.Response.BinaryWrite(ms.ToArray());

                }
                finally
                {
                    g.Dispose();
                    image.Dispose();
                }
            }
        }
        #endregion


    }
}
