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
using System.Data.OleDb ;
namespace cs_discovery
{
	/// <summary>
	/// pie_orders 的摘要说明。
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
			// 连接数据库
			using( OleDbConnection conn = new OleDbConnection())
			{
				conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" 
					+ this.Server.MapPath("demomdb.mdb");
				conn.Open();

				// 查询数据库
				using( OleDbCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = @"
SELECT OrderDate AS 订购时间, 
       shipname AS 运输人, 
        shipaddress AS 地点, 
        ( select  
			sum( round( unitprice * quantity * ( 1 - discount) , 3 ) ) 
			from orderdetails
			 where orderdetails.orderid = orders.orderid
        ) AS 总金额
FROM orders
WHERE customerid ='" + customerid + "'" ;
					OleDbDataReader reader = cmd.ExecuteReader();

					// 创建饼图对象
					PieShape pie = new PieShape();
					pie.Width = 400 ;
					pie.Height = 300 ;
					System.IO.StringWriter writer = new System.IO.StringWriter();

					while( reader.Read())
					{
						double Value = Convert.ToDouble( reader.GetValue( 3 ));
						string Text = "时间:" + reader.GetValue( 0 ) 
							+ "\r\n人员:" + reader.GetValue( 1 ) 
							+ "\r\n地点:" + reader.GetValue( 2 )
							+ "\r\n金额:" + reader.GetValue( 3 );
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
