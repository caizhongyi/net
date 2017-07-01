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
	/// codexslt 的摘要说明。
	/// </summary>
	public class codexslt : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// 此处代码动态构造 xml 文档对象结构来输出XML文档
			System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
			XmlDoc.AppendChild( XmlDoc.CreateElement("Table"));

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

					while( reader.Read())
					{
						// 输出一条记录
						System.Xml.XmlElement RecordElement = XmlDoc.CreateElement("Record");
						XmlDoc.DocumentElement.AppendChild( RecordElement );
						for( int iCount = 0 ; iCount < FieldCount ; iCount ++ )
						{
							// 输出一个字段值
							System.Xml.XmlElement FieldElement = XmlDoc.CreateElement( FieldNames[ iCount ] );
							RecordElement.AppendChild( FieldElement );
							object v = reader.GetValue( iCount );
							if( v == null || DBNull.Value.Equals( v ))
							{
								FieldElement.SetAttribute("Null" , "1" );
							}
							else
							{
								FieldElement.AppendChild( XmlDoc.CreateTextNode( Convert.ToString( v )));
							}
						}
					}//while( reader.Read())
					reader.Close();
				}//using( System.Data.OleDb.OleDbCommand cmd = conn.CreateCommand())
			}//using( System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection())

			//
			// 开始使用代码模拟实现 XSLT 转换,XSLT代码在 table.xml 中
			// 模拟的XSLT代码为
			/*
			 
<xsl:stylesheet version='1.0' xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output method='html' indent="yes"/>
	<xsl:template match='/'>
		<html>
			<body>
				<table border='1'>
					<xsl:for-each select="Table/Record">
						<tr>
							<xsl:for-each select="*">
								<td>
									<xsl:value-of select="." />
								</td>
							</xsl:for-each>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
			
			 */

			// 保存输出结果的缓冲区
			System.IO.StringWriter myResult = new System.IO.StringWriter();
			
			myResult.WriteLine("<html>");
			myResult.WriteLine("	<body>");
			myResult.WriteLine("		<table border='1'>");
			// 模拟 <xsl:for-each select="Table/Record">
			foreach( System.Xml.XmlNode node in XmlDoc.SelectNodes("Table/Record"))
			{
				myResult.WriteLine("		<tr>");
				// 模拟 <xsl:for-each select="*">
				foreach( System.Xml.XmlNode node2 in node.SelectNodes("*"))
				{
					myResult.Write("			<td>");
					// 模拟 <xsl:value-of select="." />
					myResult.Write( node2.InnerText );
					myResult.WriteLine("</td>");
				}
				myResult.WriteLine("		</tr>");
			}
			myResult.WriteLine("		</table>");
			myResult.WriteLine("	</body>");
			myResult.WriteLine("</html>");
			myResult.Close();

			this.Response.Write( myResult.ToString());
		}//private void Page_Load(object sender, System.EventArgs e)

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
