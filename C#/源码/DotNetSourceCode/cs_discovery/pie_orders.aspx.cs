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
using System.Data.OleDb ;
namespace cs_discovery
{
	/// <summary>
	/// pie_orders ��ժҪ˵����
	/// </summary>
	public class pie_orders : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.Label lblResult;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		 
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			string customerid = this.Request.QueryString["customerid"] ;
			if( customerid == null || customerid.Length == 0 )
				return ;
			// �������ݿ�
			using( OleDbConnection conn = new OleDbConnection())
			{
				conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" 
					+ this.Server.MapPath("demomdb.mdb");
				conn.Open();

				// ��ѯ���ݿ�
				using( OleDbCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
SELECT OrderDate AS ����ʱ��, 
       shipname AS ������, 
        shipaddress AS �ص�, 
        ( select  
			sum( round( unitprice * quantity * ( 1 - discount) , 3 ) ) 
			from orderdetails
			 where orderdetails.orderid = orders.orderid
        ) AS �ܽ��
FROM orders
WHERE customerid ='" + customerid + "'" ;
					OleDbDataReader reader = cmd.ExecuteReader();

					// ������ͼ����
					PieShape pie = new PieShape();
					pie.Width = 400 ;
					pie.Height = 300 ;
					System.IO.StringWriter writer = new System.IO.StringWriter();

					while( reader.Read())
					{
						double Value = Convert.ToDouble( reader.GetValue( 3 ));
						string Text = "ʱ��:" + reader.GetValue( 0 ) 
							+ "\r\n��Ա:" + reader.GetValue( 1 ) 
							+ "\r\n�ص�:" + reader.GetValue( 2 )
							+ "\r\n���:" + reader.GetValue( 3 );
						string Link = "#" ;
						pie.Add( Value , Text , Link );
					}//while
					reader.Close();

					pie.RefreshState();
					this.Session["customerid"] = pie ;
					this.lblResult.Text = pie.GetHtmlString("pieimage.aspx?name=customerid");
					this.DataGrid1.DataSource = pie ;
					this.DataGrid1.DataBind();
				}
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
