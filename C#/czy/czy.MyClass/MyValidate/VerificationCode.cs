using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Drawing;

namespace czy.MyClass.MyValidate
{
    /// <summary>
    /// 生成验证码类
    /// </summary>
    public class VerificationCode
    {
        # region 生成图型验证码
        /// <summary>
        /// 生成图型验证码
        /// </summary>
        /// <param name="iNumBit">获取几位</param>
        /// <returns>获取图型验证码</returns>
        public static string GetVerificationCode(int iNumBit, Page page)
        {
            string strNum = ValidateNum(iNumBit);
            CreateImage(strNum,page);//当前页面生成图片
            return strNum;
        }
        # endregion

        #region 生成图型
        /// <summary>
        /// 生成图型
        /// </summary>
        /// <param name="strNum">字符窜</param>
        public  static void CreateImage(string strNum, Page page)
        {
            if (strNum == null || strNum.Trim() == String.Empty)
            { return; }
            else
            {

                System.Drawing.Bitmap image = new System.Drawing.Bitmap(46, 14);
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
