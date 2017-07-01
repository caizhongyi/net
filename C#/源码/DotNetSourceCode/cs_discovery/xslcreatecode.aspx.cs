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
using XDesignerData ;
namespace cs_discovery
{
	/// <summary>
	/// xslcreatecode ��ժҪ˵����
	/// </summary>
	public class xslcreatecode : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DropDownList cboTable;
		protected System.Web.UI.WebControls.DropDownList cboXSLT;
		protected System.Web.UI.WebControls.Label lblResult;
		protected System.Web.UI.WebControls.Button cmdRefresh;
		protected System.Web.UI.WebControls.Label lblDBName;
		protected System.Web.UI.WebControls.Button cmdCreate;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			RefreshSystem();
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
			this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
			this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// ˢ��ϵͳ��Ŧ�¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdRefresh_Click(object sender, System.EventArgs e)
		{
			this.Session["info"] = null;
			this.cboTable.Items.Clear();
			this.cboXSLT.Items.Clear();
			RefreshSystem( );
		}//private void cmdRefresh_Click(object sender, System.EventArgs e)

		/// <summary>
		/// �������밴Ŧ�¼�
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmdCreate_Click(object sender, System.EventArgs e)
		{
			DataBaseInfo info = this.GetInfo();
			string xml = null ;
			if( cboTable.SelectedIndex == 0 )
			{
				xml = GetXMLString( info );
			}
			else
			{
				TableInfo table = info.Tables[ this.cboTable.SelectedValue ] ;
				if( table == null )
				{
					this.lblResult.Text = "��ѡ��һ����";
					return ;
				}
				xml = GetXMLString( table );
			}
			string html = "";
			if( cboXSLT.SelectedIndex <= 0 )
			{
				// û��ʹ���κ�ģ�壬ֱ����ʾXMLԴ����
				html = @"<textarea 
							wrap=off 
							readonly
							style='border:1 solid black;
									overflow=visible;
									background-color:#dddddd'>" 
							+ xml + "</textarea>";
			}
			else
			{
				// ������XSLTģ�壬ִ��XSLTת��
				System.Xml.Xsl.XslTransform transform = new System.Xml.Xsl.XslTransform();
				transform.Load( this.Server.MapPath( this.cboXSLT.SelectedValue ) + ".xslt" );
				System.IO.StringWriter writer = new System.IO.StringWriter();
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml( xml );
				transform.Transform( doc , null , writer , null );
				writer.Close();
				html = writer.ToString();
			}
			this.lblResult.Text = "<b>������ " 
				+ html.Length 
				+ " ���ַ�</b><br />\r\n" + html ;
		}

		/// <summary>
		/// ��ָ���������л���XML�ĵ���Ȼ�󷵻ػ�õ�XML�ַ���
		/// </summary>
		/// <param name="obj">����</param>
		/// <returns>XML�ַ���</returns>
		private string GetXMLString( object obj )
		{
			System.IO.StringWriter myStr = new System.IO.StringWriter();
			System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter( myStr );
			writer.Indentation = 3 ;
			writer.IndentChar = ' ';
			writer.Formatting = System.Xml.Formatting.Indented ;
			System.Xml.Serialization.XmlSerializer sc = 
				new System.Xml.Serialization.XmlSerializer( obj.GetType() );
			sc.Serialize( writer , obj );
			writer.Close();
			string xml = myStr.ToString();
			int index = xml.IndexOf("?>");
			if( index > 0 )
				xml = xml.Substring( index + 2 );
			return xml.Trim() ;
		}

		/// <summary>
		/// ������ݿ�ṹ��Ϣ����
		/// </summary>
		/// <returns>���ݿ�ṹ��Ϣ����</returns>
		private DataBaseInfo GetInfo( )
		{
			DataBaseInfo info = this.Session["info"] as DataBaseInfo ;
			if( info == null )
			{
				info = new DataBaseInfo();
				info.LoadFromAccess2000( this.MapPath("demomdb.mdb"));
				this.Session["info"] = info ;
			}
			return info ;
		}		

		/// <summary>
		/// ˢ��ϵͳ
		/// </summary>
		private void RefreshSystem( )
		{
			DataBaseInfo info = this.GetInfo();
			this.lblDBName.Text = info.Name ;
			if( cboTable.Items.Count == 0 )
			{
				cboTable.Items.Add( new ListItem("���б�" , "���б�" ));
				foreach( TableInfo table in info.Tables )
				{
					cboTable.Items.Add( new ListItem( table.Name , table.Name ));
				}
			}
			if( cboXSLT.Items.Count == 0 )
			{
				cboXSLT.Items.Add("XML����");
				string[] names = System.IO.Directory.GetFiles( this.MapPath(".") , "_*.xslt");
				if( names != null && names.Length > 0 )
				{
					foreach( string name in names )
					{
						string name2 = System.IO.Path.GetFileNameWithoutExtension( name );
						this.cboXSLT.Items.Add( new ListItem( name2 , name2 ));
					}
				}
			}
		}//private void RefreshSystem( )

	}//public class xslcreatecode : System.Web.UI.Page
}