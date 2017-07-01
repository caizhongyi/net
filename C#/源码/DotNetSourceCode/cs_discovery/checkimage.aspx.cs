/*
 
	C#发现之旅系列教程配套演示代码
	
	本代码仅供学习和参考使用

	编制 袁永福 2008－5－15 
	
	MSN			yyf9989@hotmail.com
	
	QQ			28348092
	
	作者博客	http://xdesigner.cnblogs.com/
	
	使用者请作者的尊重知识产权。

*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace cs_discovery
{
	/// <summary>
	/// 生成验证码图片的页面
	/// </summary>
	public class checkimage : System.Web.UI.Page
	{
		/// <summary>
		/// 检查指定的文本是否匹配验证码
		/// </summary>
		/// <param name="text">要判断的文本</param>
		/// <returns>是否匹配</returns>
		public static bool CheckCode( string text )
		{
			string txt = System.Web.HttpContext.Current.Session["checkcode"] as string ;
			return text == txt ;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// 创建一个包含随机内容的验证码文本
			System.Random rand = new Random();
			int len = rand.Next(4 , 6 );
			char[] chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			for( int iCount = 0 ; iCount < len ; iCount ++ )
			{
				myStr.Append( chars[ rand.Next( chars.Length )]);
			}

			string text = myStr.ToString();
			// 保存验证码到 session 中以便其他模块使用
			this.Session["checkcode"] = text ;

			Size ImageSize = Size.Empty ;
			Font myFont = new Font("MS Sans Serif" , 20 );

			// 计算验证码图片大小
			using( Bitmap bmp = new Bitmap( 10 , 10 ))
			{
				using( Graphics g = Graphics.FromImage( bmp ))
				{
					SizeF size = g.MeasureString( text , myFont , 10000 );
					ImageSize.Width = ( int ) size.Width + 8 ;
					ImageSize.Height = ( int ) size.Height + 8 ;
				}
			}

			// 创建验证码图片
			using( Bitmap bmp = new Bitmap( ImageSize.Width , ImageSize.Height ))
			{
				// 绘制验证码文本
				using( Graphics g = Graphics.FromImage( bmp ))
				{
					g.Clear( Color.White );
					using( StringFormat f = new StringFormat())
					{
						f.Alignment = StringAlignment.Near ;
						f.LineAlignment = StringAlignment.Center ;
						f.FormatFlags = StringFormatFlags.NoWrap ;
						g.DrawString( 
							text , 
							myFont , 
							Brushes.Black , 
							new RectangleF( 
							0 , 
							0 , 
							ImageSize.Width ,
							ImageSize.Height ),
							f );
					}//using
				}//using

				// 制造噪声 杂点面积占图片面积的 30%
				int num = ImageSize.Width * ImageSize.Height * 30 / 100 ;
	
				for( int iCount = 0 ; iCount < num ; iCount ++ )
				{
					// 在随机的位置使用随机的颜色设置图片的像素
					int x = rand.Next( ImageSize.Width );
					int y = rand.Next( ImageSize.Height );
					int r = rand.Next( 255 );
					int g = rand.Next( 255 );
					int b = rand.Next( 255 );
					Color c = Color.FromArgb( r , g , b );
					bmp.SetPixel( x , y , c );
				}//for

				// 输出图片
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				bmp.Save( ms , System.Drawing.Imaging.ImageFormat.Png );
				this.Response.ContentType = "image/png";
				ms.WriteTo( this.Response.OutputStream );
				ms.Close();

			}//using

			myFont.Dispose();

		}//private void Page_Load(object sender, System.EventArgs e)

		#region Web 窗体设计器生成的代码
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: 该调用是 ASP.NET Web 窗体设计器所必需的。
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}