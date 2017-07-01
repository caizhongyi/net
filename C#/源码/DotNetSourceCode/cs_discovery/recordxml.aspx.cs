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

namespace cs_discovery
{
	/// <summary>
	/// ʹ��XmlTextWriter ���XML�ĵ�����,��������������ͻ���ִ��XSLTת����ASPXҳ��
	/// </summary>
	public class recordxml : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �˴�ʹ�� XmlTextWriter ���������XML�ĵ�����.������XML�ĵ�����ṹ

			this.Response.ContentEncoding = System.Text.Encoding.GetEncoding( 936 );
			this.Response.ContentType = "text/xml";
			// �������ݿ�
			using( System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection())
			{
				conn.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" 
					+ this.Server.MapPath("demomdb.mdb");
				conn.Open();

				// ��ѯ���ݿ�
				using( System.Data.OleDb.OleDbCommand cmd = conn.CreateCommand())
				{
					cmd.CommandText = "Select * From Customers";
					System.Data.OleDb.OleDbDataReader reader = cmd.ExecuteReader();

					// ��������ֶ���
					int FieldCount = reader.FieldCount ;
					string[] FieldNames = new string[ FieldCount ] ;
					for( int iCount = 0 ; iCount < FieldCount ; iCount ++ )
					{
						FieldNames[ iCount ] = reader.GetName( iCount );
					}

					// ����һ��XML�ĵ���д��
					System.Xml.XmlTextWriter xmlwriter = new System.Xml.XmlTextWriter( this.Response.Output );
					xmlwriter.Indentation = 3 ;
					xmlwriter.IndentChar = ' ';
					xmlwriter.Formatting = System.Xml.Formatting.Indented ;

					// ��ʼ���XML�ĵ�
					xmlwriter.WriteStartDocument();
			
					// ���XSLT��ʽ����Ϣͷ
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
						// ���һ����¼
						xmlwriter.WriteStartElement("Record");
						for( int iCount = 0 ; iCount < FieldCount ; iCount ++ )
						{
							// ���һ���ֶ�ֵ
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
