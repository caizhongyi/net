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
	/// ʹ��XmlDocument���� XML�ĵ�����ṹ��Ȼ�����XML�ĵ�����,���ڷ�������ִ��XSLTת����ASPXҳ��
	/// </summary>
	public class record : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			// �˴����붯̬���� XmlDocument���� �����XML�ĵ�
			System.Xml.XmlDocument XmlDoc = new System.Xml.XmlDocument();
			XmlDoc.AppendChild( XmlDoc.CreateElement("Table"));

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

					while( reader.Read())
					{
						// ���һ����¼
						System.Xml.XmlElement RecordElement = XmlDoc.CreateElement("Record");
						XmlDoc.DocumentElement.AppendChild( RecordElement );
						for( int iCount = 0 ; iCount < FieldCount ; iCount ++ )
						{
							// ���һ���ֶ�ֵ
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

			string strXSLRef = this.Request.QueryString["xsl"] ;
			if( strXSLRef != null && strXSLRef.Length > 0 )
			{
				// ����ҳ�����ָ����XSLT��ʽ������ִ��XSLTת��
				strXSLRef = this.Server.MapPath( strXSLRef );
				System.Xml.Xsl.XslTransform transform = new System.Xml.Xsl.XslTransform();
				transform.Load( strXSLRef );
				transform.Transform( XmlDoc , null , this.Response.Output , null );
			}
			else
			{
				// ֱ��������ɵ�XML�ĵ�
				this.Response.Write( XmlDoc.DocumentElement.OuterXml );
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
