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
using XDesignerData ;
namespace cs_discovery
{
	/// <summary>
	/// xslcreatecode 的摘要说明。
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
			this.cmdRefresh.Click += new System.EventHandler(this.cmdRefresh_Click);
			this.cmdCreate.Click += new System.EventHandler(this.cmdCreate_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		/// <summary>
		/// 刷新系统按纽事件
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
		/// 创建代码按纽事件
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
					this.lblResult.Text = "请选择一个表";
					return ;
				}
				xml = GetXMLString( table );
			}
			string html = "";
			if( cboXSLT.SelectedIndex <= 0 )
			{
				// 没有使用任何模板，直接显示XML源代码
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
				// 启动了XSLT模板，执行XSLT转换
				System.Xml.Xsl.XslTransform transform = new System.Xml.Xsl.XslTransform();
				transform.Load( this.Server.MapPath( this.cboXSLT.SelectedValue ) + ".xslt" );
				System.IO.StringWriter writer = new System.IO.StringWriter();
				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml( xml );
				transform.Transform( doc , null , writer , null );
				writer.Close();
				html = writer.ToString();
			}
			this.lblResult.Text = "<b>共生成 " 
				+ html.Length 
				+ " 个字符</b><br />\r\n" + html ;
		}

		/// <summary>
		/// 将指定对象序列化成XML文档，然后返回获得的XML字符串
		/// </summary>
		/// <param name="obj">对象</param>
		/// <returns>XML字符串</returns>
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
		/// 获得数据库结构信息对象
		/// </summary>
		/// <returns>数据库结构信息对象</returns>
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
		/// 刷新系统
		/// </summary>
		private void RefreshSystem( )
		{
			DataBaseInfo info = this.GetInfo();
			this.lblDBName.Text = info.Name ;
			if( cboTable.Items.Count == 0 )
			{
				cboTable.Items.Add( new ListItem("所有表" , "所有表" ));
				foreach( TableInfo table in info.Tables )
				{
					cboTable.Items.Add( new ListItem( table.Name , table.Name ));
				}
			}
			if( cboXSLT.Items.Count == 0 )
			{
				cboXSLT.Items.Add("XML代码");
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