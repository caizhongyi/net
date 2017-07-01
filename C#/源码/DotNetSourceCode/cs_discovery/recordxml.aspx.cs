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
	/// 使用XmlTextWriter 输出XML文档内容,并允许在浏览器客户端执行XSLT转换的ASPX页面
	/// </summary>
	public class recordxml : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 此处使用 XmlTextWriter 来快速输出XML文档内容.不构造XML文档对象结构

			this.Response.ContentEncoding = System.Text.Encoding.GetEncoding( 936 );
			this.Response.ContentType = "text/xml";
			// 连接数据库
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

					// 生成一个XML文档书写器
					System.Xml.XmlTextWriter xmlwriter = new System.Xml.XmlTextWriter( this.Response.Output );
					xmlwriter.Indentation = 3 ;
					xmlwriter.IndentChar = ' ';
					xmlwriter.Formatting = System.Xml.Formatting.Indented ;

					// 开始输出XML文档
					xmlwriter.WriteStartDocument();
			
					// 输出XSLT样式表信息头
					string strXSLRef = this.Request.QueryString["xsl"] ;
					if( strXSLRef != null && strXSLRef.Length > 0 )
					{
						xmlwriter.WriteProcessingInstruction(
							"xml-stylesheet" , 
							"type='text/xsl' href='" + strXSLRef + "'");
					}
					xmlwriter.WriteStartElement("Table");
					while( reader.Read())
					{
						// 输出一条记录
						xmlwriter.WriteStartElement("Record");
						for( int iCount = 0 ; iCount < FieldCount ; iCount ++ )
						{
							// 输出一个字段值
							xmlwriter.WriteStartElement( FieldNames[ iCount ] );
							object v = reader.GetValue( iCount );
							if( v == null || DBNull.Value.Equals( v ))
							{
								xmlwriter.WriteAttributeString("Null" , "1");
							}
							else
							{
								xmlwriter.WriteString( Convert.ToString( v ));
							}
							xmlwriter.WriteEndElement();
						}
						xmlwriter.WriteEndElement();
					}//while( reader.Read())
					reader.Close();

					xmlwriter.WriteEndElement();
					xmlwriter.WriteEndDocument();
					xmlwriter.Close();

				}//using( System.Data.OleDb.OleDbCommand cmd = conn.CreateCommand())
			}//using( System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection())
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
