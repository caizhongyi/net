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
	/// login ��ժҪ˵����
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
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void cmdOK_Click(object sender, System.EventArgs e)
		{
			string UserName = this.txtUserName.Text ;
			string Password = this.txtPassword.Text ;
			string CheckCode = this.txtCheckCode.Text ;
			if( UserName == "����"
				&& Password == "abc"
				&& checkimage.CheckCode( CheckCode ) )
			{
				this.lblResult.Text = "<b>��¼�ɹ�</b>";
				this.RegisterStartupScript("a" , "<script>alert('��¼�ɹ�');</script>");
			}
			else
			{
				this.lblResult.Text = "<font color=red><b>�û���¼��Ϣ��������������</b></font>";
			}
		}
	}
}