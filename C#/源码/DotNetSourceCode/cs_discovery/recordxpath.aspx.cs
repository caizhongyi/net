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
	/// recordxpath ��ժҪ˵����
	/// </summary>
	public class recordxpath : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtXPath;
		protected System.Web.UI.WebControls.Button cmdQuery;
		protected System.Web.UI.WebControls.TextBox txtXML;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �ڴ˴������û������Գ�ʼ��ҳ��
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
			this.cmdQuery.Click += new System.EventHandler(this.cmdQuery_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// ����һ��������ѯ�����XML�ĵ�����
		/// </summary>
		/// <returns>XML�ĵ�����</returns>
		private System.Xml.XmlDocument CreateRecordXMLDocument()
		{
			System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
			doc.AppendChild( doc.CreateElement("Table") );
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

					while( reader.Read())
					{
						// ���һ����¼
						System.Xml.XmlElement RecordElement = doc.CreateElement("Record");
						doc.DocumentElement.AppendChild( RecordElement );
						for( int iCount = 0 ; iCount < FieldCount ; iCount ++ )
						{
							System.Xml.XmlElement FieldElement = doc.CreateElement( FieldNames[ iCount ] );
							RecordElement.AppendChild( FieldElement );
							// ���һ���ֶ�ֵ
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
		/// ʹ��XPath����XML�ڵ�
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
					this.txtXML.Text = "δ��ѯ�κ�����";
				}
				else
				{
					System.Text.StringBuilder myStr = new System.Text.StringBuilder();
					myStr.Append("����ѯ " + list.Count + " �����");
					for( int iCount = 0 ; iCount < list.Count ; iCount ++ )
					{
						myStr.Append("\r\n���" + iCount + " --------------------" );
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
