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
	/// login 的摘要说明。
	/// </summary>
	public class login : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtUserName;
		protected System.Web.UI.WebControls.TextBox txtPassword;
		protected System.Web.UI.WebControls.TextBox txtCheckCode;
		protected System.Web.UI.WebControls.Label lblResult;
		protected System.Web.UI.WebControls.Button cmdOK;
	
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
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			string UserName = this.txtUserName.Text ;
			string Password = this.txtPassword.Text ;
			string CheckCode = this.txtCheckCode.Text ;
			if( UserName == "张三"
				&& Password == "abc"
				&& checkimage.CheckCode( CheckCode ) )
			{
				this.lblResult.Text = "<b>登录成功</b>";
				this.RegisterStartupScript("a" , "<script>alert('登录成功');</script>");
			}
			else
			{
				this.lblResult.Text = "<font color=red><b>用户登录信息错误，请重新输入</b></font>";
			}
		}
	}
}