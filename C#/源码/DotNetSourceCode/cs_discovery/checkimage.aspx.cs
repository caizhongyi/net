/*
 
	C#����֮��ϵ�н̳�������ʾ����
	
	���������ѧϰ�Ͳο�ʹ��

	���� Ԭ���� 2008��5��15 
	
	MSN			yyf9989@hotmail.com
	
	QQ			28348092
	
	���߲���	http://xdesigner.cnblogs.com/
	
	ʹ���������ߵ�����֪ʶ��Ȩ��

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
	/// ������֤��ͼƬ��ҳ��
	/// </summary>
	public class checkimage : System.Web.UI.Page
	{
		/// <summary>
		/// ���ָ�����ı��Ƿ�ƥ����֤��
		/// </summary>
		/// <param name="text">Ҫ�жϵ��ı�</param>
		/// <returns>�Ƿ�ƥ��</returns>
		public static bool CheckCode( string text )
		{
			string txt = System.Web.HttpContext.Current.Session["checkcode"] as string ;
			return text == txt ;
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			// ����һ������������ݵ���֤���ı�
			System.Random rand = new Random();
			int len = rand.Next(4 , 6 );
			char[] chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
			System.Text.StringBuilder myStr = new System.Text.StringBuilder();
			for( int iCount = 0 ; iCount < len ; iCount ++ )
			{
				myStr.Append( chars[ rand.Next( chars.Length )]);
			}

			string text = myStr.ToString();
			// ������֤�뵽 session ���Ա�����ģ��ʹ��
			this.Session["checkcode"] = text ;

			Size ImageSize = Size.Empty ;
			Font myFont = new Font("MS Sans Serif" , 20 );

			// ������֤��ͼƬ��С
			using( Bitmap bmp = new Bitmap( 10 , 10 ))
			{
				using( Graphics g = Graphics.FromImage( bmp ))
				{
					SizeF size = g.MeasureString( text , myFont , 10000 );
					ImageSize.Width = ( int ) size.Width + 8 ;
					ImageSize.Height = ( int ) size.Height + 8 ;
				}
			}

			// ������֤��ͼƬ
			using( Bitmap bmp = new Bitmap( ImageSize.Width , ImageSize.Height ))
			{
				// ������֤���ı�
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

				// �������� �ӵ����ռͼƬ����� 30%
				int num = ImageSize.Width * ImageSize.Height * 30 / 100 ;
	
				for( int iCount = 0 ; iCount < num ; iCount ++ )
				{
					// �������λ��ʹ���������ɫ����ͼƬ������
					int x = rand.Next( ImageSize.Width );
					int y = rand.Next( ImageSize.Height );
					int r = rand.Next( 255 );
					int g = rand.Next( 255 );
					int b = rand.Next( 255 );
					Color c = Color.FromArgb( r , g , b );
					bmp.SetPixel( x , y , c );
				}//for

				// ���ͼƬ
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				bmp.Save( ms , System.Drawing.Imaging.ImageFormat.Png );
				this.Response.ContentType = "image/png";
				ms.WriteTo( this.Response.OutputStream );
				ms.Close();

			}//using

			myFont.Dispose();

		}//private void Page_Load(object sender, System.EventArgs e)

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}