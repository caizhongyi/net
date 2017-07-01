using System;
using System.Xml ;
using System.Xml.XPath ;
using System.Xml.Xsl ;

namespace cs_discovery
{
	/// <summary>
	/// XML�ĵ��������
	/// </summary>
	public sealed class XMLOutputer
	{

		/// <summary>
		/// ���ĳ��XML�ڵ�Ĵ����������XML�ַ���
		/// </summary>
		/// <param name="node">XML�ڵ�</param>
		/// <returns>���ɵ�XML�ַ���</returns>
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
		/// ����һ�����ݿ����Ӷ���
		/// </summary>
		/// <returns>�����ĵ����ݿ����Ӷ���</returns>
		public static System.Data.IDbConnection CreateConnection()
		{
			System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
			conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" 
				+ System.Web.HttpContext.Current.Request.MapPath("demomdb.mdb");
			conn.Open();
			return conn ;
		}
		
		/// <summary>
		/// ��ð������ݿ��ѯ�����XML�ĵ�����
		/// </summary>
		/// <param name="strSQL">SQL��ѯ���</param>
		/// <returns>������ѯ�����XML�ĵ�����</returns>
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
		/// �����¼XML�ĵ�
		/// </summary>
		/// <param name="strSQL">��ѯ���ݿ�ʹ�õ�SQL���</param>
		/// <param name="writer">����ı�����д������</param>
		/// <param name="strXSLRef">���õ�XSLT�ļ���</param>
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
		/// �����¼XML�ĵ�
		/// </summary>
		/// <param name="strSQL">��ѯ���ݿ�ʹ�õ�SQL���</param>
		/// <param name="strXSLRef">���õ�XSLT�ļ���</param>
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
		/// ����ѯ���ݿ����ý�������һ��XML�ĵ���
		/// </summary>
		/// <param name="reader">��ѯ�������ݵĶ�ȡ��</param>
		/// <param name="writer">��������ı�����д��</param>
		/// <param name="strXSLRef">ʹ�õ�XSLT��ʽ�����ӵ�ַ������Ϊ��</param>
		private static void OutRecordXML( 
			System.Data.IDataReader reader ,
			System.IO.TextWriter writer , 
			string strXSLRef )
		{
			// ��������ֶ���
			int FieldCount = reader.FieldCount ;
			string[] FieldNames = new string[ FieldCount ] ;
			for( int iCount = 0 ; iCount < FieldCount ; iCount ++ )
			{
				FieldNames[ iCount ] = reader.GetName( iCount );
			}
			// ����һ��XML�ĵ���д��
			System.Xml.XmlTextWriter xmlwriter = new System.Xml.XmlTextWriter( writer );
			xmlwriter.Indentation = 3 ;
			xmlwriter.IndentChar = ' ';
			xmlwriter.Formatting = System.Xml.Formatting.Indented ;
			xmlwriter.WriteStartDocument();
			// ���XSLT��ʽ������
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