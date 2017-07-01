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
	/// pie_customers ��ժҪ˵����
	/// </summary>
	public class pie_customers : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblResult;
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
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
select top 10
	customers.customerid ,
    customers.companyname as �ͻ�����, 
    ( select sum( round(	orderdetails.unitprice 
							* orderdetails.quantity 
							* ( 1.0 - orderdetails.discount) , 2 ) ) 
      from orderdetails , orders 
      where orderdetails.orderid = orders.orderid 
            and orders.customerid = customers.customerid
	) as �����ܽ��
from customers";
					OleDbDataReader reader = cmd.ExecuteReader();
					// ������ͼ����
					PieShape pie = new PieShape();
					pie.Width = 400 ;
					pie.Height = 300 ;
					System.IO.StringWriter writer = new System.IO.StringWriter();

					while( reader.Read())
					{
						string id = Convert.ToString( reader.GetValue( 0 ));
						double Value = Convert.ToDouble( reader.GetValue( 2 ));
						string Text = "�ͻ�����:" + Convert.ToString( reader.GetValue( 1 )) 
									+ "\r\n�������:" + Convert.ToString( reader.GetValue( 2 ))
									+ "\r\n����쿴�ÿͻ���������ϸ���" ;
						string Link = "pie_orders.aspx?customerid=" + id ;
						pie.Add( Value , Text , Link );
					}//while

					reader.Close();
					pie.RefreshState();
					this.Session["pie_customers"] = pie ;
					this.lblResult.Text = pie.GetHtmlString("pieimage.aspx?name=pie_customers");
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