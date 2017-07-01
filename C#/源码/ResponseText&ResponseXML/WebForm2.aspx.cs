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

namespace WebApplication2
{
	/// <summary>
	/// WebForm2 的摘要说明。
	/// </summary>
	public partial class WebForm2 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
			int a=0;
			int b=0;

			if(Request.QueryString["A"]!=null)
			{
				a=int.Parse(Request.QueryString["A"]);
			}
			if(Request.QueryString["B"]!=null)
			{
				b=int.Parse(Request.QueryString["B"]);
			}


            Response.ContentType = "text/plain";
			Response.Write(a+b);
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

		}
		#endregion
	}
}
