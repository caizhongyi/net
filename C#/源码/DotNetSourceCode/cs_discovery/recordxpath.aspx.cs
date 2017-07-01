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
	/// recordxpath 的摘要说明。
	/// </summary>
	public class recordxpath : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtXPath;
		protected System.Web.UI.WebControls.Button cmdQuery;
		protected System.Web.UI.WebControls.TextBox txtXML;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 在此处放置用户代码以初始化页面
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
			this.cmdQuery.Click += new System.EventHandler(this.cmdQuery_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 创建一个包含查询结果的XML文档对象
		/// </summary>
		/// <returns>XML文档对象</returns>
		private System.Xml.XmlDocument CreateRecordXMLDocument()
		{
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.AppendChild( doc.CreateElement("Table") );
			using( System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection())
			{
				conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" 
					+ this.Server.MapPath("demomdb.mdb");
				conn.Open();

				// 查询数据库
				using( System.Data.OleDb.OleDbCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = "Select * From Customers";
					System.Data.OleDb.OleDbDataReader reader = cmd.ExecuteReader();

					// 获得所有字段名
					int FieldCount = reader.FieldCount ;
					string[] FieldNames = new string[ FieldCount ] ;
					for( int iCount = 0 ; iCount < FieldCount ; iCount ++ )
					{
						FieldNames[ iCount ] = reader.GetName( iCount );
					}

					while( reader.Read())
					{
						// 输出一条记录
						System.Xml.XmlElement RecordElement = doc.CreateElement("Record");
						doc.DocumentElement.AppendChild( RecordElement );
						for( int iCount = 0 ; iCount < FieldCount ; iCount ++ )
						{
							System.Xml.XmlElement FieldElement = doc.CreateElement( FieldNames[ iCount ] );
							RecordElement.AppendChild( FieldElement );
							// 输出一个字段值
							object v = reader.GetValue( iCount );
							if( v == null || DBNull.Value.Equals( v ))
							{
								FieldElement.SetAttribute("Null" , "1");
							}
							else
							{
								FieldElement.AppendChild( doc.CreateTextNode( Convert.ToString( v )));
							}
						}
					}//while( reader.Read())
					reader.Close();
				}//using( System.Data.OleDb.OleDbCommand cmd = conn.CreateCommand())
			}//using( System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection())
					
			return doc ;
		}//private System.Xml.XmlDocument CreateRecordXMLDocument()

		/// <summary>
		/// 使用XPath检索XML节点
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdQuery_Click(object sender, System.EventArgs e)
		{
			System.Xml.XmlDocument doc = CreateRecordXMLDocument( );
			string xpath = this.txtXPath.Text ;
			if( xpath != null )
			{
				xpath = xpath.Trim();
			}
			if( xpath != null && xpath.Length > 0 )
			{
				System.Xml.XmlNodeList list = doc.SelectNodes( xpath );
				if( list == null || list.Count == 0 )
				{
					this.txtXML.Text = "未查询任何数据";
				}
				else
				{
					System.Text.StringBuilder myStr = new System.Text.StringBuilder();
					myStr.Append("共查询 " + list.Count + " 个结果");
					for( int iCount = 0 ; iCount < list.Count ; iCount ++ )
					{
						myStr.Append("\r\n结果" + iCount + " --------------------" );
						myStr.Append( "\r\n" + GetXMLString( list[ iCount ] ));
					}
					this.txtXML.Text = myStr.ToString();
				}
			}
			else
			{
				this.txtXML.Text = GetXMLString( doc.DocumentElement );
			}
		}

		private string GetXMLString( System.Xml.XmlNode node )
		{
			System.IO.StringWriter myStr = new System.IO.StringWriter();
			System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter( myStr );
			writer.Indentation = 3 ;
			writer.IndentChar = ' ';
			writer.Formatting = System.Xml.Formatting.Indented ;
			writer.WriteStartDocument();
			node.WriteTo( writer );
			writer.WriteEndDocument();
			writer.Close();
			string xml = myStr.ToString();
			int index = xml.IndexOf("?>");
			if( index > 0 )
				xml = xml.Substring( index + 2 );
			return xml.Trim() ;
		}
	}
}
