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
	/// WebForm2 ��ժҪ˵����
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

		}
		#endregion
	}
}
