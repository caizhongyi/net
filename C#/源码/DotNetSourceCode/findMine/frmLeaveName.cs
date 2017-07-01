using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace findMine
{
	/// <summary>
	/// frmLeaveName 的摘要说明。
	/// </summary>
	public class frmLeaveName : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label label3;
		private string gameGrade;
		private int userScore;
		private Hashtable HTUserInfo;
		private ReadAndSave objSave=new ReadAndSave();
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmLeaveName()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
		public frmLeaveName(string gameGrade,int userScore,Hashtable HTUserInfo)
		{
			InitializeComponent();
			this.gameGrade=gameGrade;
			this.userScore=userScore;
			this.HTUserInfo=HTUserInfo;
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.btnOK = new System.Windows.Forms.Button();
			this.txtName = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(64, 120);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(56, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "确定(&O)";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(16, 88);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(168, 21);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "匿名";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(56, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "已破高级记录，";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(56, 56);
			this.label2.Name = "label2";
			this.label2.TabIndex = 3;
			this.label2.Text = "请留尊姓大名。";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 16);
			this.label3.TabIndex = 4;
			this.label3.Text = "恭喜!!!!!!!!!";
			// 
			// frmLeaveName
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.BackColor = System.Drawing.SystemColors.GrayText;
			this.ClientSize = new System.Drawing.Size(192, 152);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.btnOK);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "frmLeaveName";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "frmLeaveName";
			this.Load += new System.EventHandler(this.frmLeaveName_Load);
			this.Activated += new System.EventHandler(this.frmLeaveName_Activated);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmLeaveName_Activated(object sender, System.EventArgs e)
		{
			this.txtName.Focus();
			this.txtName.SelectAll();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			if(this.txtName.Text=="")
				return;
			UserInfo temp=(UserInfo)this.HTUserInfo[this.gameGrade];
			temp.UserName=this.txtName.Text;
			temp.UserScore=this.userScore.ToString();
			temp.UserDate=System.DateTime.Today.ToShortDateString();
			this.HTUserInfo.Remove(this.gameGrade);
			this.HTUserInfo.Add(this.gameGrade,temp);
			this.objSave.saveAll(this.HTUserInfo);
			this.Close();
		}

		private void frmLeaveName_Load(object sender, System.EventArgs e)
		{
			this.label1.Text="已破"+this.gameGrade+"记录，";
		}

	}
}
