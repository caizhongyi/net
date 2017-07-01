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
using System.Xml ;

namespace cs_discovery
{
	/// <summary>
	/// pieimage ��ժҪ˵����
	/// </summary>
	public class pieimage : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// ��ò���
			string name = this.Request.QueryString["name"] ;
			if( name == null )
			{
				return ;
			}
			// ��ñ�ͼ����
			PieShape pie = this.Session[ name ] as PieShape ;
			if( pie == null )
			{
				return ;
			}
			
			using( Bitmap bmp = pie.CreateBitmap())
			{
				this.Response.ContentType = "image/png";
				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				bmp.Save( ms , System.Drawing.Imaging.ImageFormat.Png );
				ms.WriteTo( this.Response.OutputStream );
				ms.Close();
			}
		}
 
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
