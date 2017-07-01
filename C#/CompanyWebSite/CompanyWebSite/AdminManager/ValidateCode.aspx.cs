using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Drawing.Drawing2D;

public partial class ValidateCode : System.Web.UI.Page
{
    private void Page_Load(object sender, EventArgs e)
    {
        char[] chars = "1234567890".ToCharArray();
        System.Random random = new Random();

        string validateCode = string.Empty;
        for (int i = 0; i <3; i++) validateCode += chars[random.Next(0, chars.Length)].ToString();
        Session["ValidateCode"] = validateCode;
        CreateImage(validateCode);
    }

    private void CreateImage(string checkCode)
    {
        int iwidth = (int)(checkCode.Length *16);
        System.Drawing.Bitmap image = new System.Drawing.Bitmap(iwidth, 24);
        Graphics g = Graphics.FromImage(image);
        g.SmoothingMode = SmoothingMode.HighQuality;
        g.Clear(Color.White);
        //定义颜色
        Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Chocolate, Color.Brown, Color.DarkCyan, Color.Purple };
        //定义字体            
        string[] font = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial" };
        Random rand = new Random();
        //随机输出噪点
        for (int i = 0; i < 50; i++)
        {
            int x = rand.Next(image.Width);
            int y = rand.Next(image.Height);
            g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
        }

        //输出不同字体和颜色的验证码字符
        for (int i = 0; i < checkCode.Length; i++)
        {
            int cindex = rand.Next(7);
            int findex = rand.Next(4);

            Font f = new System.Drawing.Font(font[findex], 12, System.Drawing.FontStyle.Bold);
            Brush b = new System.Drawing.SolidBrush(c[cindex]);
            g.DrawString(checkCode.Substring(i, 1), f, b, 1 + (i * 14), 1);
        }
        //画一个边框
        g.DrawRectangle(new Pen(Color.Black, 0), 0, 0, image.Width - 1, image.Height - 1);

        //输出到浏览器
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
        Response.ClearContent();
        Response.ContentType = "image/Jpeg";
        Response.BinaryWrite(ms.ToArray());
        g.Dispose();
        image.Dispose();
    }
}
