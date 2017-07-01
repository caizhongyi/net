using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace findMine
{
	/// <summary>
	/// frmHeroList 的摘要说明。
	/// </summary>
	public class frmHeroList : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lblScore1;
		private System.Windows.Forms.Label lblScore2;
		private System.Windows.Forms.Label lblName1;
		private System.Windows.Forms.Label lblName2;
		private System.Windows.Forms.Label lblName3;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Label lblDate1;
		private System.Windows.Forms.Label lblDate2;
		private System.Windows.Forms.Label lblDate3;
		private System.Windows.Forms.Label lblScore3;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
		private Hashtable HTUserInfo=new Hashtable();
		private ReadAndSave objReadAndSave=new ReadAndSave();
		public frmHeroList()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmHeroList));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblScore1 = new System.Windows.Forms.Label();
			this.lblScore2 = new System.Windows.Forms.Label();
			this.lblScore3 = new System.Windows.Forms.Label();
			this.lblName1 = new System.Windows.Forms.Label();
			this.lblName2 = new System.Windows.Forms.Label();
			this.lblName3 = new System.Windows.Forms.Label();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.lblDate1 = new System.Windows.Forms.Label();
			this.lblDate2 = new System.Windows.Forms.Label();
			this.lblDate3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 23);
			this.label1.TabIndex = 0;
			this.label1.Text = "初级:";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 23);
			this.label2.TabIndex = 1;
			this.label2.Text = "中级:";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(16, 56);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 23);
			this.label3.TabIndex = 2;
			this.label3.Text = "高级:";
			// 
			// lblScore1
			// 
			this.lblScore1.Location = new System.Drawing.Point(56, 8);
			this.lblScore1.Name = "lblScore1";
			this.lblScore1.Size = new System.Drawing.Size(40, 23);
			this.lblScore1.TabIndex = 3;
			this.lblScore1.Text = "999秒";
			// 
			// lblScore2
			// 
			this.lblScore2.Location = new System.Drawing.Point(56, 32);
			this.lblScore2.Name = "lblScore2";
			this.lblScore2.Size = new System.Drawing.Size(40, 23);
			this.lblScore2.TabIndex = 4;
			this.lblScore2.Text = "999秒";
			// 
			// lblScore3
			// 
			this.lblScore3.Location = new System.Drawing.Point(56, 56);
			this.lblScore3.Name = "lblScore3";
			this.lblScore3.Size = new System.Drawing.Size(40, 23);
			this.lblScore3.TabIndex = 5;
			this.lblScore3.Text = "999秒";
			// 
			// lblName1
			// 
			this.lblName1.Location = new System.Drawing.Point(104, 8);
			this.lblName1.Name = "lblName1";
			this.lblName1.Size = new System.Drawing.Size(32, 23);
			this.lblName1.TabIndex = 6;
			this.lblName1.Text = "匿名";
			// 
			// lblName2
			// 
			this.lblName2.Location = new System.Drawing.Point(104, 32);
			this.lblName2.Name = "lblName2";
			this.lblName2.Size = new System.Drawing.Size(32, 23);
			this.lblName2.TabIndex = 7;
			this.lblName2.Text = "匿名";
			// 
			// lblName3
			// 
			this.lblName3.Location = new System.Drawing.Point(104, 56);
			this.lblName3.Name = "lblName3";
			this.lblName3.Size = new System.Drawing.Size(32, 23);
			this.lblName3.TabIndex = 8;
			this.lblName3.Text = "匿名";
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(56, 80);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(80, 23);
			this.btnReset.TabIndex = 9;
			this.btnReset.Text = "重新记分(&R)";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnOK
			// 
			this.btnOK.Location = new System.Drawing.Point(168, 80);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(56, 23);
			this.btnOK.TabIndex = 10;
			this.btnOK.Text = "确定(&O)";
			this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
			// 
			// lblDate1
			// 
			this.lblDate1.Location = new System.Drawing.Point(144, 8);
			this.lblDate1.Name = "lblDate1";
			this.lblDate1.Size = new System.Drawing.Size(72, 23);
			this.lblDate1.TabIndex = 11;
			this.lblDate1.Text = "0000-00-00";
			// 
			// lblDate2
			// 
			this.lblDate2.Location = new System.Drawing.Point(144, 32);
			this.lblDate2.Name = "lblDate2";
			this.lblDate2.Size = new System.Drawing.Size(72, 23);
			this.lblDate2.TabIndex = 12;
			this.lblDate2.Text = "0000-00-00";
			// 
			// lblDate3
			// 
			this.lblDate3.Location = new System.Drawing.Point(144, 56);
			this.lblDate3.Name = "lblDate3";
			this.lblDate3.Size = new System.Drawing.Size(72, 23);
			this.lblDate3.TabIndex = 13;
			this.lblDate3.Text = "0000-00-00";
			// 
			// frmHeroList
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(232, 110);
			this.Controls.Add(this.lblDate3);
			this.Controls.Add(this.lblDate2);
			this.Controls.Add(this.lblDate1);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.lblName3);
			this.Controls.Add(this.lblName2);
			this.Controls.Add(this.lblName1);
			this.Controls.Add(this.lblScore3);
			this.Controls.Add(this.lblScore2);
			this.Controls.Add(this.lblScore1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.HelpButton = true;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmHeroList";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "扫雷英雄榜";
			this.Load += new System.EventHandler(this.frmHeroList_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmHeroList_Load(object sender, System.EventArgs e)
		{
			this.HTUserInfo=this.objReadAndSave.readAll();
			this.showHeroList();
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			this.HTUserInfo.Clear();
			UserInfo obj=new UserInfo();
			obj.UserScore="999";
			obj.UserName="匿名";
			obj.UserDate="0000-00-00";
			this.HTUserInfo.Add("初级",obj);
			this.HTUserInfo.Add("中级",obj);
			this.HTUserInfo.Add("高级",obj);
			this.objReadAndSave.saveAll(this.HTUserInfo);

			this.lblName1.Text="匿名";
			this.lblScore1.Text="999秒";
			this.lblDate1.Text="0000-00-00";

			this.lblName2.Text="匿名";
			this.lblScore2.Text="999秒";
			this.lblDate2.Text="0000-00-00";

			this.lblName3.Text="匿名";
			this.lblScore3.Text="999秒";
			this.lblDate3.Text="0000-00-00";
		}
		private void showHeroList()
		{
			UserInfo obj1=(UserInfo)this.HTUserInfo["初级"];
			UserInfo obj2=(UserInfo)this.HTUserInfo["中级"];
			UserInfo obj3=(UserInfo)this.HTUserInfo["高级"];
			this.lblName1.Text=obj1.UserName;
			this.lblScore1.Text=obj1.UserScore+"秒";
			this.lblDate1.Text=obj1.UserDate;
			this.lblName2.Text=obj2.UserName;
			this.lblScore2.Text=obj2.UserScore+"秒";
			this.lblDate2.Text=obj2.UserDate;
			this.lblName3.Text=obj3.UserName;
			this.lblScore3.Text=obj3.UserScore+"秒";
			this.lblDate3.Text=obj3.UserDate;
		}
	}
}
