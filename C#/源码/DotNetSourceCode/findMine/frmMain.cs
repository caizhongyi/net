using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Reflection;

namespace findMine
{
	/// <summary>
	/// Form1 的摘要说明。
	/// </summary>
	public class frmMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mineMenu;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem menuItem4;
		private System.Windows.Forms.MenuItem menuItem6;
		private System.Windows.Forms.MenuItem menuItem7;
		private System.Windows.Forms.MenuItem menuItem8;
		private System.Windows.Forms.MenuItem menuItem9;
		private System.Windows.Forms.MenuItem menuItem10;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.Button btnControl;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label lblTime;
		private System.Windows.Forms.Label lblNowMineCount;
		private System.Windows.Forms.Timer myTime;
		private System.Windows.Forms.MenuItem menuItem11;
		private System.Windows.Forms.MenuItem menuItem5;
		private System.Windows.Forms.MenuItem menuItem12;
		private System.Windows.Forms.NotifyIcon notifyMine;
		private System.Windows.Forms.ContextMenu rightMenu;
		private System.Windows.Forms.MenuItem menuItem13;
		private System.Windows.Forms.MenuItem menuItem14;
		private System.Windows.Forms.MenuItem menuItem15;
		private System.Windows.Forms.MenuItem menuItem16;
		private System.Windows.Forms.MenuItem menuItem17;
		private System.Windows.Forms.MenuItem menuItem18;
		private System.Windows.Forms.MenuItem menuItem19;
		private System.Windows.Forms.MenuItem menuItem20;
		private System.Windows.Forms.MenuItem menuItem21;
		private System.Windows.Forms.MenuItem menuItem22;
		private System.ComponentModel.IContainer components;

		public frmMain()
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
				if (components != null) 
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
			this.components = new System.ComponentModel.Container();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmMain));
			this.mineMenu = new System.Windows.Forms.MainMenu();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem4 = new System.Windows.Forms.MenuItem();
			this.menuItem11 = new System.Windows.Forms.MenuItem();
			this.menuItem12 = new System.Windows.Forms.MenuItem();
			this.menuItem5 = new System.Windows.Forms.MenuItem();
			this.menuItem6 = new System.Windows.Forms.MenuItem();
			this.menuItem7 = new System.Windows.Forms.MenuItem();
			this.menuItem8 = new System.Windows.Forms.MenuItem();
			this.menuItem9 = new System.Windows.Forms.MenuItem();
			this.menuItem22 = new System.Windows.Forms.MenuItem();
			this.menuItem21 = new System.Windows.Forms.MenuItem();
			this.menuItem10 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.lblTitle = new System.Windows.Forms.Label();
			this.btnControl = new System.Windows.Forms.Button();
			this.lblTime = new System.Windows.Forms.Label();
			this.lblNowMineCount = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.myTime = new System.Windows.Forms.Timer(this.components);
			this.notifyMine = new System.Windows.Forms.NotifyIcon(this.components);
			this.rightMenu = new System.Windows.Forms.ContextMenu();
			this.menuItem13 = new System.Windows.Forms.MenuItem();
			this.menuItem18 = new System.Windows.Forms.MenuItem();
			this.menuItem14 = new System.Windows.Forms.MenuItem();
			this.menuItem19 = new System.Windows.Forms.MenuItem();
			this.menuItem15 = new System.Windows.Forms.MenuItem();
			this.menuItem16 = new System.Windows.Forms.MenuItem();
			this.menuItem20 = new System.Windows.Forms.MenuItem();
			this.menuItem17 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// mineMenu
			// 
			this.mineMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItem1,
																					 this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem4,
																					  this.menuItem11,
																					  this.menuItem12,
																					  this.menuItem5,
																					  this.menuItem6,
																					  this.menuItem7,
																					  this.menuItem8,
																					  this.menuItem9,
																					  this.menuItem22,
																					  this.menuItem21,
																					  this.menuItem10});
			this.menuItem1.Text = "游戏(&G)";
			this.menuItem1.Click += new System.EventHandler(this.menuItem1_Click);
			// 
			// menuItem4
			// 
			this.menuItem4.Index = 0;
			this.menuItem4.Text = "重新开始(&S)";
			this.menuItem4.Click += new System.EventHandler(this.menuItem4_Click);
			// 
			// menuItem11
			// 
			this.menuItem11.Index = 1;
			this.menuItem11.Text = "暂停(&P)";
			this.menuItem11.Click += new System.EventHandler(this.menuItem11_Click);
			// 
			// menuItem12
			// 
			this.menuItem12.Index = 2;
			this.menuItem12.Text = "继续(&C)";
			this.menuItem12.Click += new System.EventHandler(this.menuItem12_Click);
			// 
			// menuItem5
			// 
			this.menuItem5.Index = 3;
			this.menuItem5.Text = "-";
			// 
			// menuItem6
			// 
			this.menuItem6.Index = 4;
			this.menuItem6.Text = "初级(B)";
			this.menuItem6.Click += new System.EventHandler(this.menuItem6_Click);
			// 
			// menuItem7
			// 
			this.menuItem7.Index = 5;
			this.menuItem7.Text = "中级(&I)";
			this.menuItem7.Click += new System.EventHandler(this.menuItem7_Click);
			// 
			// menuItem8
			// 
			this.menuItem8.Index = 6;
			this.menuItem8.Text = "高级(&E)";
			this.menuItem8.Click += new System.EventHandler(this.menuItem8_Click);
			// 
			// menuItem9
			// 
			this.menuItem9.Index = 7;
			this.menuItem9.Text = "-";
			// 
			// menuItem22
			// 
			this.menuItem22.Index = 8;
			this.menuItem22.Text = "扫雷英雄榜(&T)";
			this.menuItem22.Click += new System.EventHandler(this.menuItem22_Click);
			// 
			// menuItem21
			// 
			this.menuItem21.Index = 9;
			this.menuItem21.Text = "-";
			// 
			// menuItem10
			// 
			this.menuItem10.Index = 10;
			this.menuItem10.Text = "退出(X)";
			this.menuItem10.Click += new System.EventHandler(this.menuItem10_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem3});
			this.menuItem2.Text = "帮助(&H)";
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 0;
			this.menuItem3.Text = "关于扫雷(&A)...";
			this.menuItem3.Click += new System.EventHandler(this.menuItem3_Click);
			// 
			// lblTitle
			// 
			this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblTitle.Location = new System.Drawing.Point(-16, 8);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(592, 48);
			this.lblTitle.TabIndex = 1;
			// 
			// btnControl
			// 
			this.btnControl.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnControl.BackgroundImage")));
			this.btnControl.Location = new System.Drawing.Point(264, 16);
			this.btnControl.Name = "btnControl";
			this.btnControl.Size = new System.Drawing.Size(32, 32);
			this.btnControl.TabIndex = 2;
			this.btnControl.Click += new System.EventHandler(this.btnControl_Click);
			// 
			// lblTime
			// 
			this.lblTime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTime.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblTime.Location = new System.Drawing.Point(488, 16);
			this.lblTime.Name = "lblTime";
			this.lblTime.Size = new System.Drawing.Size(64, 32);
			this.lblTime.TabIndex = 3;
			this.lblTime.Text = "000";
			// 
			// lblNowMineCount
			// 
			this.lblNowMineCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblNowMineCount.Font = new System.Drawing.Font("宋体", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.lblNowMineCount.Location = new System.Drawing.Point(8, 16);
			this.lblNowMineCount.Name = "lblNowMineCount";
			this.lblNowMineCount.Size = new System.Drawing.Size(64, 32);
			this.lblNowMineCount.TabIndex = 4;
			this.lblNowMineCount.Text = "000";
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.groupBox1.ForeColor = System.Drawing.Color.Red;
			this.groupBox1.Location = new System.Drawing.Point(8, 56);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(552, 312);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			// 
			// myTime
			// 
			this.myTime.Tick += new System.EventHandler(this.myTime_Tick);
			// 
			// notifyMine
			// 
			this.notifyMine.ContextMenu = this.rightMenu;
			this.notifyMine.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyMine.Icon")));
			this.notifyMine.Text = "AuthorQQ：26074159";
			this.notifyMine.Visible = true;
			this.notifyMine.DoubleClick += new System.EventHandler(this.notifyMine_DoubleClick);
			// 
			// rightMenu
			// 
			this.rightMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem13,
																					  this.menuItem18,
																					  this.menuItem14,
																					  this.menuItem19,
																					  this.menuItem15,
																					  this.menuItem16,
																					  this.menuItem20,
																					  this.menuItem17});
			// 
			// menuItem13
			// 
			this.menuItem13.DefaultItem = true;
			this.menuItem13.Index = 0;
			this.menuItem13.Text = "打开扫雷(&O)";
			this.menuItem13.Click += new System.EventHandler(this.menuItem13_Click);
			// 
			// menuItem18
			// 
			this.menuItem18.Index = 1;
			this.menuItem18.Text = "-";
			// 
			// menuItem14
			// 
			this.menuItem14.Index = 2;
			this.menuItem14.Text = "在线升级(&U)";
			// 
			// menuItem19
			// 
			this.menuItem19.Index = 3;
			this.menuItem19.Text = "-";
			// 
			// menuItem15
			// 
			this.menuItem15.Enabled = false;
			this.menuItem15.Index = 4;
			this.menuItem15.Text = "最大化(&X)";
			// 
			// menuItem16
			// 
			this.menuItem16.Index = 5;
			this.menuItem16.Text = "最小化(N)";
			this.menuItem16.Click += new System.EventHandler(this.menuItem16_Click);
			// 
			// menuItem20
			// 
			this.menuItem20.Index = 6;
			this.menuItem20.Text = "-";
			// 
			// menuItem17
			// 
			this.menuItem17.Index = 7;
			this.menuItem17.Text = "退出(E)";
			this.menuItem17.Click += new System.EventHandler(this.menuItem17_Click);
			// 
			// frmMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(7, 14);
			this.BackColor = System.Drawing.SystemColors.Control;
			this.ClientSize = new System.Drawing.Size(570, 400);
			this.Controls.Add(this.lblNowMineCount);
			this.Controls.Add(this.lblTime);
			this.Controls.Add(this.btnControl);
			this.Controls.Add(this.lblTitle);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.ForeColor = System.Drawing.Color.Red;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(576, 432);
			this.Menu = this.mineMenu;
			this.MinimumSize = new System.Drawing.Size(576, 432);
			this.Name = "frmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "扫雷";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmMain_Closing);
			this.SizeChanged += new System.EventHandler(this.frmMain_SizeChanged);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.Activated += new System.EventHandler(this.frmMain_Activated);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main() 
		{
			//Application.Run(new frmMain());

			//Get the running instance.
			Process instance = RunningInstance();
			if (instance == null)
			{
				//There isn't another instance, show our form.
				frmMain objFrmMain=new frmMain();
				Application.Run (objFrmMain);
			}
			else
			{
				//There is another instance of this process.
				HandleRunningInstance(instance);
			}
		}
		private Button[] btnMine = new Button[480];
		private Hashtable HTMine=new Hashtable();
		private GameState objGameState=new GameState();
		private void frmMain_Load(object sender, System.EventArgs e)
		{
			int index=0;//Button数组的索引
			int x=5,x1=5;//开始在窗体上排列按纽的初始横坐标
			int y=16;////开始在窗体上排列按纽的初始纵坐标
			int height=18;//按纽高度
			int width=18;//按纽宽度
			int xCount=30;//按纽横排个数
			int yCount=16;//按纽纵排个数
			
			for(int i=0;i<xCount;i++)
				for(int j=0;j<yCount;j++)
				{

					this.btnMine[index] = new Button();
					this.btnMine[index].Size = new System.Drawing.Size(width, height);
					this.btnMine[index].Location = new System.Drawing.Point(x, y);
					this.groupBox1.Controls.Add(btnMine[index]);
					this.btnMine[index].Click+=new EventHandler(btnMineAll_Click);
					this.btnMine[index].MouseDown+=new MouseEventHandler(btnMineAll_MouseDown);
					index++;
					x+=height;
					if(x>xCount*width)
					{
						x=x1;
						y+=width;
					}

				}
			objGameState.GameGrade="高级";
			this.lblNowMineCount.Text="099";
			this.myTime.Interval=1000;
			this.menuItem11.Enabled=false;
			this.menuItem12.Enabled=false;
		}

		private void menuItem3_Click(object sender, System.EventArgs e)
		{
			frmAbout f=new frmAbout();
			f.ShowDialog();
		}

		private void menuItem10_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		private void btnControl_Click(object sender, System.EventArgs e)
		{
			this.btnControl.BackgroundImage=Image.FromFile("image/start.bmp");
			this.HTMine.Clear();
			this.useAllButton();
			this.clear();
			this.objGameState.IsBtnFirstClick=true;
			this.menuItem11.Enabled=true;
			this.menuItem12.Enabled=false;
			//this.objGameState.IsStart=true;
			int gameGrade=99;
			switch(this.objGameState.GameGrade)
			{
				case "中级":
					gameGrade=66;
					break;
				case "初级":
					gameGrade=33;
					break;
			}
			this.lblNowMineCount.Text="0"+gameGrade;
			this.btnControl.Focus();
		}
		private void menuItem4_Click(object sender, System.EventArgs e)
		{
			this.btnControl.PerformClick();
		}
		private void initializeMine(int index)//初始化所有雷
		{
			int gameGrade=99;
			switch(this.objGameState.GameGrade)
			{
				case "中级":
					gameGrade=66;
					break;
				case "初级":
					gameGrade=33;
					break;
			}

			MineState[] objMineState=new MineState[480];
			ArrayList allMine=this.getAllIsMine(gameGrade,index);
			for(int i=0;i<480;i++)
			{
				objMineState[i]=new MineState();
				if(allMine.Contains(i))
				{
					objMineState[i].setIsMine(true);
				}
				this.HTMine.Add(i,objMineState[i]);
			}
			this.objGameState.IsBtnFirstClick=false;
			
		}
		private ArrayList getAllIsMine(int allCount,int isNotMine)
		{
			Random r=new Random();
			int tempR;
			ArrayList allMine=new ArrayList();
			for(int i=0;i<allCount;i++)
			{	
				tempR=r.Next(479);
				if(allMine.Contains(tempR))
				{
					tempR=this.getAMine(allMine);
				}
				allMine.Add(tempR);
			}
			return allMine;
		}
		private int getAMine(ArrayList allMine)
		{
			Random r=new Random();
			int tempR=0;
			for(int j=0;j<480;j++)
			{
				tempR=r.Next(479);
				if(!allMine.Contains(tempR))
				{
					return tempR;
				}
			}
			return tempR;
		}
		private void accountMine(int index)//计算所点击按纽周围雷的具体情况
		{
			if(this.btnMine[index].BackgroundImage!=null)
			{
				if(this.btnMine[index].BackgroundImage.Size.ToString()!="{Width=17, Height=20}")
				return;
			}
			MineState obj=(MineState)HTMine[index];
			if(obj.getIsMine())
			{
				this.showAllMine(index);
				this.unuseAllButton();
				this.myTime.Enabled=false;
				//this.objGameState.IsStart=false;
				return;
			}
			int aroundMineCount=0;  //周围一共有多少颗雷
			int leftUp,up,rightUp; //左上,上,右上
			int left,right;		  //左,右
			int leftDown,down,rightDown;//左下,下,右下
			if(index==0)//最左上角的一个按纽
			{
				right=index+1;
				down=index+30;
				rightDown=index+30+1;
				if(((MineState)HTMine[right]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[down]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[rightDown]).getIsMine()==true)
					aroundMineCount++;
				this.btnMine[index].BackgroundImage=Image.FromFile("image/"+aroundMineCount.ToString()+".bmp");
				this.btnMine[index].FlatStyle=FlatStyle.Popup;
				if(aroundMineCount==0)
				{
					accountMine(down);//
					accountMine(right);//
					accountMine(rightDown);//
					return;
				}
			}
			if(index==29)//最右上角的一个按纽
			{
				left=index-1;
				down=index+30;
				leftDown=index+30-1;
				if(((MineState)HTMine[left]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[down]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[leftDown]).getIsMine()==true)
					aroundMineCount++;
				this.btnMine[index].BackgroundImage=Image.FromFile("image/"+aroundMineCount.ToString()+".bmp");
				this.btnMine[index].FlatStyle=FlatStyle.Popup;
				if(aroundMineCount==0)
				{
					accountMine(left);//
					accountMine(down);//
					accountMine(leftDown);//
					return;
				}
			}
			if(index==450)//最左下角的一个按纽
			{
				right=index+1;
				up=index-30;
				rightUp=index-30+1;
				if(((MineState)HTMine[right]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[up]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[rightUp]).getIsMine()==true)
					aroundMineCount++;
				this.btnMine[index].BackgroundImage=Image.FromFile("image/"+aroundMineCount.ToString()+".bmp");
				this.btnMine[index].FlatStyle=FlatStyle.Popup;
				if(aroundMineCount==0)
				{
					accountMine(up);//
					accountMine(rightUp);//
					accountMine(right);//
					return;
				}
			}
			if(index==479)//最右下角的一个按纽
			{
				left=index-1;
				up=index-30;
				leftUp=index-30-1;
				if(((MineState)HTMine[left]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[up]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[leftUp]).getIsMine()==true)
					aroundMineCount++;
				this.btnMine[index].BackgroundImage=Image.FromFile("image/"+aroundMineCount.ToString()+".bmp");
				this.btnMine[index].FlatStyle=FlatStyle.Popup;
				if(aroundMineCount==0)
				{
					accountMine(leftUp);//
					accountMine(up);//
					accountMine(left);//
					return;
				}
			}
			if(index>0&&index<29)//最上面一排按纽(排除左右两端)
			{
				left=index-1;
				right=index+1;
				leftDown=index+30-1;
				down=index+30;
				rightDown=index+30+1;
				if(((MineState)HTMine[left]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[right]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[leftDown]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[down]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[rightDown]).getIsMine()==true)
					aroundMineCount++;
				this.btnMine[index].BackgroundImage=Image.FromFile("image/"+aroundMineCount.ToString()+".bmp");
				this.btnMine[index].FlatStyle=FlatStyle.Popup;
				if(aroundMineCount==0)
				{
					accountMine(left);//
					accountMine(down);//
					accountMine(right);//
					accountMine(leftDown);//
					accountMine(rightDown);//
					return;
				}
			}
			if(index>450&&index<479)//最下面一排按纽(排除左右两端)
			{
				left=index-1;
				right=index+1;
				leftUp=index-30-1;
				up=index-30;
				rightUp=index-30+1;
				if(((MineState)HTMine[left]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[right]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[leftUp]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[up]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[rightUp]).getIsMine()==true)
					aroundMineCount++;
				this.btnMine[index].BackgroundImage=Image.FromFile("image/"+aroundMineCount.ToString()+".bmp");
				this.btnMine[index].FlatStyle=FlatStyle.Popup;
				if(aroundMineCount==0)
				{
					accountMine(leftUp);//
					accountMine(up);//
					accountMine(rightUp);//
					accountMine(left);//
					accountMine(right);//
					return;
				}
			}
			if(index!=0&&index!=450&&index%30==0)//最左边一排按纽(排除上下两端)
			{
				right=index+1;
				rightUp=index-30+1;
				rightDown=index+30+1;
				up=index-30;
				down=index+30;
				if(((MineState)HTMine[right]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[rightUp]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[rightDown]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[up]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[down]).getIsMine()==true)
					aroundMineCount++;
				this.btnMine[index].BackgroundImage=Image.FromFile("image/"+aroundMineCount.ToString()+".bmp");
				this.btnMine[index].FlatStyle=FlatStyle.Popup;
				if(aroundMineCount==0)
				{
					accountMine(up);//
					accountMine(rightUp);//
					accountMine(down);//
					accountMine(right);//
					accountMine(rightDown);//
					return;
				}
			}
			if(index!=29&&index!=479&&(index+1)%30==0)//最右边一排按纽(排除上下两端)
			{
				left=index-1;
				leftUp=index-30-1;
				leftDown=index+30-1;
				up=index-30;
				down=index+30;
				if(((MineState)HTMine[left]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[leftUp]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[leftDown]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[up]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[down]).getIsMine()==true)
					aroundMineCount++;
				this.btnMine[index].BackgroundImage=Image.FromFile("image/"+aroundMineCount.ToString()+".bmp");
				this.btnMine[index].FlatStyle=FlatStyle.Popup;
				if(aroundMineCount==0)
				{
					accountMine(leftUp);//
					accountMine(up);//
					accountMine(left);//
					accountMine(down);//
					accountMine(leftDown);//
					return;
				}
			}
			if((index>30&&index<59)||(index>60&&index<89)||(index>90&&index<119)||(index>120&&index<149)||
				(index>150&&index<179)||(index>180&&index<209)||(index>210&&index<239)||(index>240&&index<269)||
				(index>270&&index<299)||(index>300&&index<329)||(index>330&&index<359)||(index>360&&index<389)||
				(index>390&&index<419)||(index>420&&index<449))
			{
				leftUp=index-30-1;
				up=index-30;
				rightUp=index-30+1;
				left=index-1;
				right=index+1;
				leftDown=index+30-1;
				down=index+30;
				rightDown=index+30+1;
				if(((MineState)HTMine[leftUp]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[up]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[rightUp]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[left]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[right]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[leftDown]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[down]).getIsMine()==true)
					aroundMineCount++;
				if(((MineState)HTMine[rightDown]).getIsMine()==true)
					aroundMineCount++;
				this.btnMine[index].BackgroundImage=Image.FromFile("image/"+aroundMineCount.ToString()+".bmp");
				this.btnMine[index].FlatStyle=FlatStyle.Popup;
				if(aroundMineCount==0)
				{
					accountMine(leftUp);
					accountMine(up);
					accountMine(rightUp);
					accountMine(left);
					accountMine(down);
					accountMine(right);
					accountMine(leftDown);
					accountMine(rightDown);
					return;
				}
			}
		}
		private void showAllMine(int index)//显示所有的雷
		{
			this.btnControl.BackgroundImage=Image.FromFile("image/cry.bmp");
			foreach(DictionaryEntry obj in this.HTMine)
			{
				if(((MineState)obj.Value).getIsMine())
				{
					if(this.btnMine[Convert.ToInt32(obj.Key)].BackgroundImage==null||this.btnMine[Convert.ToInt32(obj.Key)].BackgroundImage.Size.ToString()=="{Width=17, Height=20}")
					{
						this.btnMine[Convert.ToInt32(obj.Key)].BackgroundImage=Image.FromFile("image/mine.bmp");
						if(Convert.ToInt32(obj.Key)==index)
							this.btnMine[Convert.ToInt32(obj.Key)].BackgroundImage=Image.FromFile("image/mineWrong.bmp");
					}
				}
				if(!((MineState)obj.Value).getIsMine()&&this.btnMine[Convert.ToInt32(obj.Key)].BackgroundImage!=null)
				{
					if(this.btnMine[Convert.ToInt32(obj.Key)].BackgroundImage.Size.ToString()=="{Width=16, Height=16}")
						this.btnMine[Convert.ToInt32(obj.Key)].BackgroundImage=Image.FromFile("image/findWrongMine.bmp");
				}
			}
			this.menuItem11.Enabled=false;
			this.menuItem12.Enabled=false;
			//this.objGameState.IsStart=false;
		}
		private void clear()//清空所有Button的Text值和背景图片
		{
			this.lblNowMineCount.Text="000";
			this.lblTime.Text="000";
			this.myTime.Interval=1000;
			this.myTime.Enabled=false;
			this.lblTime.Text="000";
			this.menuItem11.Text="暂停(&P)";
			for(int index=0;index<480;index++)
			{

				this.btnMine[index].Text="";
				this.btnMine[index].BackgroundImage=null;
				this.btnMine[index].FlatStyle=FlatStyle.Standard;
			}
		}
		private void useAllButton()
		{
			for(int i=0;i<480;i++)
			{
				this.btnMine[i].Enabled=true;
			}
		}
		private void unuseAllButton()
		{
			for(int i=0;i<480;i++)
			{
				this.btnMine[i].Enabled=false;
			}
		}

		private void menuItem8_Click(object sender, System.EventArgs e)
		{
			this.objGameState.GameGrade="高级";
			this.btnControl.PerformClick();
		}
		private void btnMineAll_Click(object sender, System.EventArgs e)
		{
			this.myTime.Enabled=true;
			int index=((Button)sender).TabIndex;
			if(this.btnMine[index].BackgroundImage!=null)
			{
				if(this.btnMine[index].BackgroundImage.Size.ToString()=="{Width=16, Height=16}")
				{
					return;
				}
			}
			if(this.objGameState.IsBtnFirstClick==true)
			{
				this.initializeMine(index);
				this.objGameState.IsBtnFirstClick=false;
				this.menuItem11.Enabled=true;
				this.menuItem12.Enabled=false;
			}
			this.accountMine(index);
			if(this.isPassGame())
			{
				this.menuItem11.Enabled=false;
				this.menuItem12.Enabled=false;
				this.isLeaveName();//调用方法，判断是否写入排行榜
			}
		}

		private void btnMineAll_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int nowMineCount=Convert.ToInt32(this.lblNowMineCount.Text.ToString());
			int index=((Button)sender).TabIndex;
			if (e.Button == MouseButtons.Right)
			{
				if(this.btnMine[index].BackgroundImage==null)
				{
					//isFind.bmp  {Width=16, Height=16}
					//question.bmp   {Width=17, Height=20}
					//0.bmp   {Width=17, Height=15}
					//1.bmp   {Width=16, Height=17}
					//2.bmp   {Width=18, Height=17}
					//3.bmp   {Width=19, Height=15}
					//4.bmp   {Width=15, Height=15}
					//5.bmp   {Width=19, Height=17}
					//6.bmp   {Width=17, Height=16}
					//7.bmp   {Width=18, Height=17}
					//8.bmp   {Width=19, Height=18}

					//findWrongMine.bmp   {Width=15, Height=14}
					//mineWrong.bmp   {Width=17, Height=16}
					this.btnMine[index].BackgroundImage=Image.FromFile("image/isFind.bmp");
					nowMineCount--;
				}
				else if(this.btnMine[index].BackgroundImage.Size.ToString()=="{Width=16, Height=16}")
				{
					this.btnMine[index].BackgroundImage=Image.FromFile("image/question.bmp");
					nowMineCount++;
				}
				else if(this.btnMine[index].BackgroundImage.Size.ToString()=="{Width=17, Height=20}")
				{
					this.btnMine[index].BackgroundImage=null;
				}
				if(nowMineCount>=0&&nowMineCount<=9)
					this.lblNowMineCount.Text="00"+nowMineCount;
				if(nowMineCount>=10&&nowMineCount<=99)
					this.lblNowMineCount.Text="0"+nowMineCount;
				if(nowMineCount>=100)
					this.lblNowMineCount.Text=nowMineCount.ToString();
				if(nowMineCount<0&&nowMineCount>=-9)
					this.lblNowMineCount.Text="-0"+(-nowMineCount);
				if(nowMineCount<-9)
					this.lblNowMineCount.Text="-"+(-nowMineCount);
			}
			if(this.isPassGame())
			{
				this.menuItem11.Enabled=false;
				this.menuItem12.Enabled=false;
				this.isLeaveName();//调用方法，判断是否写入排行榜
			}
		}

		private void menuItem6_Click(object sender, System.EventArgs e)
		{
			this.objGameState.GameGrade="初级";
			this.btnControl.PerformClick();
		}
		private void menuItem7_Click(object sender, System.EventArgs e)
		{
			this.objGameState.GameGrade="中级";
			this.btnControl.PerformClick();
		}
		private bool isPassGame()//每点击一次正键或反键时计算是否已经通过这次游戏
		{
			for(int index=0;index<this.btnMine.Length;index++)//判断是否还有一个按纽未点击
			{
				if(this.btnMine[index].BackgroundImage==null)
					return false;
			}
			for(int index=0;index<this.btnMine.Length;index++)//判断是否还有一个按纽上是?号
			{
				if(this.btnMine[index].BackgroundImage.Size.ToString()=="{Width=17, Height=20}")//当还有一个时返回false
					return false;
			}
			for(int index=0;index<this.btnMine.Length;index++)//判断是否还有一个按纽上是findWrongMine.bmp
			{
				if(this.btnMine[index].BackgroundImage.Size.ToString()=="{Width=15, Height=14}")//当还有一个时返回false
					return false;
			}
			foreach(DictionaryEntry obj in this.HTMine)//判断是否完全正确
			{
				if(((MineState)obj.Value).getIsMine()==true)
				{
					if(this.btnMine[Convert.ToInt32(obj.Key)].BackgroundImage.Size.ToString()!="{Width=16, Height=16}")
						return false;
				}
				if(((MineState)obj.Value).getIsMine()==false)
				{
					if(this.btnMine[Convert.ToInt32(obj.Key)].BackgroundImage.Size.ToString()=="{Width=16, Height=16}")
						return false;
				}
			}
			this.myTime.Enabled=false;
			this.unuseAllButton();
			return true;
		}
		private void myTime_Tick(object sender, System.EventArgs e)
		{
			int mytime=Convert.ToInt32(this.lblTime.Text)+1;
			if(mytime>=0&&mytime<=9)
				this.lblTime.Text="00"+mytime;
			if(mytime>=10&&mytime<=99)
				this.lblTime.Text="0"+mytime;
			if(mytime>=100&&mytime<=999)
				this.lblTime.Text=""+mytime;
			if(mytime>=1000)
			{
				this.myTime.Enabled=false;
				this.lblTime.Text="999";
			}
		}

		private void frmMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
				if(MessageBox.Show("游戏正在进行中,您确定要退出吗？","退出确认",MessageBoxButtons.OKCancel,MessageBoxIcon.Question)==DialogResult.OK)
				{
					Application.Exit();
				}
				else
				{
					e.Cancel=true;
				}
		}

		private void menuItem11_Click(object sender, System.EventArgs e)
		{
			this.myTime.Enabled=false;
			this.unuseAllButton();
			this.menuItem11.Enabled=false;
			this.menuItem12.Enabled=true;
		}

		private void menuItem12_Click(object sender, System.EventArgs e)
		{
			this.myTime.Enabled=true;
			this.useAllButton();
			this.menuItem12.Enabled=false;
			this.menuItem11.Enabled=true;
		}

		private void notifyMine_DoubleClick(object sender, System.EventArgs e)
		{
			this.Show();
			this.WindowState=FormWindowState.Normal;
			this.menuItem16.Enabled=true;//
		}

		private void frmMain_SizeChanged(object sender, System.EventArgs e)
		{
			if(this.WindowState==FormWindowState.Minimized)
			{
				this.Hide();
				this.WindowState=FormWindowState.Normal;
				this.menuItem16.Enabled=false;
			}
		}

		private void menuItem13_Click(object sender, System.EventArgs e)
		{
			this.notifyMine_DoubleClick(sender,e);
		}

		private void menuItem17_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void frmMain_Activated(object sender, System.EventArgs e)
		{
				this.btnControl.Focus();
		}

		private void menuItem16_Click(object sender, System.EventArgs e)
		{
			this.WindowState=FormWindowState.Minimized;//
			this.menuItem16.Enabled=false;//
		}


		public static Process RunningInstance()
		{
			Process current = Process.GetCurrentProcess();
			Process[] processes = Process.GetProcessesByName (current.ProcessName);

			//Loop through the running processes in with the same name
			foreach (Process process in processes)
			{
				//Ignore the current process
				if (process.Id != current.Id)
				{
					//Make sure that the process is running from the exe file.
					if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") ==
						current.MainModule.FileName)
					{
						//Return the other process instance.
						return process;
					}
				}
			}

			//No other instance was found, return null.
			return null;
		}


		public static void HandleRunningInstance(Process instance)
		{
			//Make sure the window is not minimized or maximized
			ShowWindowAsync (instance.MainWindowHandle , WS_SHOWNORMAL);

			//Set the real intance to foreground window
			SetForegroundWindow (instance.MainWindowHandle);
		}

		[DllImport("User32.dll")] 

		private static extern bool ShowWindowAsync(
			IntPtr hWnd, int cmdShow);
		[DllImport("User32.dll")] private static extern bool
			SetForegroundWindow(IntPtr hWnd);
		private const int WS_SHOWNORMAL = 1;

		private void menuItem22_Click(object sender, System.EventArgs e)
		{
			frmHeroList frmherolist=new frmHeroList();
			frmherolist.ShowDialog();
		}
		private void isLeaveName()
		{
			this.btnControl.BackgroundImage=Image.FromFile("image/win.bmp");
			ReadAndSave objRead=new ReadAndSave();
			Hashtable HTTemp=new Hashtable();
			HTTemp=objRead.readAll();
			if(HTTemp==null)
				return;
			string gameGrade=this.objGameState.GameGrade;
			int userOldScore=Convert.ToInt32(((UserInfo)HTTemp[gameGrade]).UserScore);
			int userNewScore=Convert.ToInt32(this.lblTime.Text.ToString());
			if( userNewScore>=userOldScore)
			{
				MessageBox.Show("恭喜过关，不过未能打破最高记录，继续加油！","过关",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
				return;
			}
			frmLeaveName objFrmLeaveName=new frmLeaveName(gameGrade,userNewScore,HTTemp);
			objFrmLeaveName.ShowDialog();
		}

		private void menuItem1_Click(object sender, System.EventArgs e)
		{
		
		}

	}

	public class GameState
	{
		//私有成员变量
		private bool isBtnFirstClick=true;
		private string gameGrade="高级";
		public bool IsBtnFirstClick//后面没有()，采用Pascal命名法
		{
			get//get部分
			{
				return isBtnFirstClick;
			}
			set//set部分
			{
				this.isBtnFirstClick = value;
			}
		}
		public string GameGrade//后面没有()，采用Pascal命名法
		{
			get//get部分
			{
				return gameGrade;
			}
			set//set部分
			{
				this.gameGrade = value;
			}
		}
	}

	public class MineState
	{
		private bool isMine;
		public void setIsMine(bool isMine)
		{
			this.isMine=isMine;
		}
		public bool getIsMine()
		{
			return this.isMine;
		}
	}

}
