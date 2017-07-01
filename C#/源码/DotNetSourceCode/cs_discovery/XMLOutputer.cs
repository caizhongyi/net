using System;
using System.Xml ;
using System.Xml.XPath ;
using System.Xml.Xsl ;

namespace cs_discovery
{
	/// <summary>
	/// XML文档输出对象
	/// </summary>
	public sealed class XMLOutputer
	{

		/// <summary>
		/// 获得某个XML节点的带缩进处理的XML字符串
		/// </summary>
		/// <param name="node">XML节点</param>
		/// <returns>生成的XML字符串</returns>
		public static string GetIndentString( object obj )
		{
			System.IO.StringWriter myStrWriter = new System.IO.StringWriter();
			System.Xml.XmlTextWriter myXmlWriter = new XmlTextWriter( myStrWriter );
			myXmlWriter.IndentChar = ' ' ;
			myXmlWriter.Indentation = 3 ;
			myXmlWriter.Formatting = System.Xml.Formatting.Indented ;
			myXmlWriter.WriteStartDocument();
			if( obj is System.Xml.XmlNodeList )
			{
				myXmlWriter.WriteStartElement("Result");
				foreach( System.Xml.XmlNode node in (( System.Xml.XmlNodeList ) obj ) )
				{
					node.WriteTo( myXmlWriter );
				}
				myXmlWriter.WriteEndElement();
			}
			else if( obj is System.Xml.XmlNode )
			{
				(( System.Xml.XmlNode ) obj ).WriteTo( myXmlWriter );
			}
			myXmlWriter.WriteEndDocument();
			myXmlWriter.Close();
			string xml = myStrWriter.ToString();
			int index = xml.IndexOf("?>");
			if( index > 0 )
			{
				xml = xml.Substring( index + 2 );
			}
			return xml ;
		}

		/// <summary>
		/// 创建一个数据库连接对象
		/// </summary>
		/// <returns>创建的的数据库连接对象</returns>
		public static System.Data.IDbConnection CreateConnection()
		{
			System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
			conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" 
				+ System.Web.HttpContext.Current.Request.MapPath("demomdb.mdb");
			conn.Open();
			return conn ;
		}
		
		/// <summary>
		/// 获得包含数据库查询结果的XML文档对象
		/// </summary>
		/// <param name="strSQL">SQL查询语句</param>
		/// <returns>包含查询结果的XML文档对象</returns>
		public static System.Xml.XmlDocument CreateRecordXMLDocument( string strSQL )
		{
			using( System.Data.IDbConnection conn = CreateConnection())
			{
				using( System.Data.IDbCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = strSQL ;
					System.Data.IDataReader reader = cmd.ExecuteReader();
					
					System.IO.StringWriter myStrWriter = new System.IO.StringWriter();
					OutRecordXML( reader , myStrWriter , null );
					reader.Close();
					myStrWriter.Close();

					System.Xml.XmlDocument doc = new XmlDocument();
					doc.LoadXml( myStrWriter.ToString());
					return doc ;
				}
			}
		}

		/// <summary>
		/// 输出记录XML文档
		/// </summary>
		/// <param name="strSQL">查询数据库使用的SQL语句</param>
		/// <param name="writer">输出文本的书写器对象</param>
		/// <param name="strXSLRef">引用的XSLT文件名</param>
		public static void OutRecordXML( 
			string strSQL ,
			System.IO.TextWriter writer ,
			string strXSLRef )
		{
			using( System.Data.IDbConnection conn = CreateConnection())
			{
				using( System.Data.IDbCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = strSQL ;
					System.Data.IDataReader reader = cmd.ExecuteReader();
					OutRecordXML( reader , writer , strXSLRef );
					reader.Close();
				}
			}
		}

		/// <summary>
		/// 输出记录XML文档
		/// </summary>
		/// <param name="strSQL">查询数据库使用的SQL语句</param>
		/// <param name="strXSLRef">引用的XSLT文件名</param>
		public static string OutRecordString( string strSQL , string strXSLRef )
		{
			using( System.Data.IDbConnection conn = CreateConnection())
			{
				using( System.Data.IDbCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = strSQL ;
					System.Data.IDataReader reader = cmd.ExecuteReader();
					System.IO.StringWriter myStrWriter = new System.IO.StringWriter();
					OutRecordXML( reader , myStrWriter , strXSLRef );
					reader.Close();

					if( strXSLRef == null || strXSLRef.Length == 0 )
					{
						return myStrWriter.ToString();
					}

					myStrWriter.Close();
					XslTransform transform = new XslTransform();
					transform.Load(System.Web.HttpContext.Current.Request.MapPath( strXSLRef ) );
					XPathDocument doc = new XPathDocument(
						new System.IO.StringReader( myStrWriter.ToString()));
					myStrWriter = new System.IO.StringWriter();
					transform.Transform( doc , null , myStrWriter , null );
					return myStrWriter.ToString();
				}
			}
		}

		/// <summary>
		/// 将查询数据库所得结果输出到一个XML文档中
		/// </summary>
		/// <param name="reader">查询所得数据的读取器</param>
		/// <param name="writer">保存输出文本的书写器</param>
		/// <param name="strXSLRef">使用的XSLT样式表链接地址，可以为空</param>
		private static void OutRecordXML( 
			System.Data.IDataReader reader ,
			System.IO.TextWriter writer , 
			string strXSLRef )
		{
			// 获得所有字段名
			int FieldCount = reader.FieldCount ;
			string[] FieldNames = new string[ FieldCount ] ;
			for( int iCount = 0 ; iCount < FieldCount ; iCount ++ )
			{
				FieldNames[ iCount ] = reader.GetName( iCount );
			}
			// 生成一个XML文档书写器
			System.Xml.XmlTextWriter xmlwriter = new System.Xml.XmlTextWriter( writer );
			xmlwriter.Indentation = 3 ;
			xmlwriter.IndentChar = ' ';
			xmlwriter.Formatting = System.Xml.Formatting.Indented ;
			xmlwriter.WriteStartDocument();
			// 输出XSLT样式表链接
			if( strXSLRef != null && strXSLRef.Length > 0 )
			{
				xmlwriter.WriteProcessingInstruction(
					"xml-stylesheet" , 
					"type='text/xsl' href='" + strXSLRef + "'");
			}
			xmlwriter.WriteStartElement("Table");
			while( reader.Read())
			{
				xmlwriter.WriteStartElement("Record");
				for( int iCount = 0 ; iCount < FieldCount ; iCount ++ )
				{
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
			}
			xmlwriter.WriteEndElement();
			xmlwriter.WriteEndDocument();
		}

		private XMLOutputer(){}
	}
}