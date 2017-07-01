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
using System.Xml ;

namespace cs_discovery
{
	/// <summary>
	/// pieimage 的摘要说明。
	/// </summary>
	public class pieimage : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 获得参数
			string name = this.Request.QueryString["name"] ;
			if( name == null )
			{
				return ;
			}
			// 获得饼图对象
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
