using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices ;

using System.IO ;
using System.Diagnostics;


namespace 随机变化桌面背景图片
{

	public partial class MainForm : Form
	{
		BindingManagerBase bind;
		private const string FileFilters =
			"所有文件 (*.*)|*.*|"+
			"音频文件 (wav wma  mp3 au aif aifc aiff snd)|*.wav;*.wma;*.mp3;*.au;*.aif;*.aifc;*.aiff;*.snd|" +
			"视频文件 (avi wmv asf)|*.avi;*.wmv;*.asf|"+
			"媒体播放列表 (asx m3u wpl wax wvx wmx)|*.asx;*.m3u;*.wpl;*.wax;*.wvx;*.wmx|"+
			"电影文件 (mpa mp2 mpe mpg mpeg m1v vod ifo)|*.mpa;*.mp2;*.mpe;*.mpg;*.mpeg;*.m1v;*.vod;*.ifo|"+
			"MIDI 文件 (mid midi rmi)|*.mid;*.midi;*.rmi|" +
			"图片文件  (jpg bmp gif tga)|*.jpg;*.bmp;*.gif;*.tga";
		//"All Files (*.*)|*.*";
		
		[STAThread]
		public static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
		public MainForm()
		{
			InitializeComponent();
			
		}
		
		private void BtnChangeWallpaperClick(object sender, EventArgs e)
		{

			
			
		}
		
		void LblCopyRightInfoMouseHover(object sender, EventArgs e)
		{
			for(double d=1d; d>=0d; d-=0.05d)
			{
				System.Threading.Thread.Sleep(30);
				Application.DoEvents();
				this.Opacity=d;
				this.Refresh();
			}
		}
		
		void LblCopyRightInfoMouseLeave(object sender, EventArgs e)
		{
			for(double d=0d; d<=1d; d+=0.05d)
			{
				System.Threading.Thread.Sleep(30);
				Application.DoEvents();
				this.Opacity=d;
				this.Refresh();
			}
		}
		
		void lblmusicBoxClick(object sender, EventArgs e)
		{
			this.Opacity=0.1;
			for(int i=0;i<190;i++)
			{
				this.ClientSize = new System.Drawing.Size(292+(int)(i*2.5-2), 266+i);
				this.Opacity+=i*0.0005;
				this.Refresh();
			}
			this.lstmusicList.Visible=true;
			this.btnUp.Visible=true;
			this.btnDown.Visible=true;
		}
		
		void BtnOpenClick(object sender, EventArgs e)
		{
			
			OpenFileDialog open = new OpenFileDialog();
			open.ShowReadOnly = true;
			open.Multiselect=true; //启用文件多选
			open.Filter=FileFilters; //文件过滤
			
			
			if (open.ShowDialog() == DialogResult.OK)
			{
				bind =
					lstmusicList.BindingContext[lstmusicList.DataSource=open.FileNames];
				/*for(int i=0;i<=lstmusicList.Items.Count-1;i++)
					
					lstmusicList.Items[i].ToString().Replace(lstmusicList.Items[i].ToString(),lstmusicList.Items[i].ToString().Substring(this.lstmusicList.Items[i].ToString().LastIndexOf('\\')+1));*/
				
			}
			
		
		}
		
		
		void btnAttributeClick(object sender, EventArgs e)
		{
			axWindowsMediaPlayer.ShowPropertyPages(axWindowsMediaPlayer);

		}
		
		void BtnCloseMediaPlayerClick(object sender, EventArgs e)
		{
			this.lstmusicList.Visible=false;
			this.btnDown.Visible=false;
			this.btnUp.Visible=false;
			ClientSize = new System.Drawing.Size(292,266);
			
		}
		
		void BtnUpClick(object sender, EventArgs e)
		{
			if(lstmusicList.SelectedItems.Count>0&&lstmusicList.SelectedIndex>0)
			{
				lstmusicList.SelectedIndex=lstmusicList.SelectedIndex-1;
				axWindowsMediaPlayer.URL=lstmusicList.Items[lstmusicList.SelectedIndex].ToString();				
				//MessageBox.Show(this.lstmusicList.SelectedItem.ToString().Substring(this.lstmusicList.SelectedItem.ToString().LastIndexOf('\\')+1));
			}
		}
		
		void BtnDownClick(object sender, EventArgs e)
		{
			if(lstmusicList.SelectedItems.Count>0&&lstmusicList.SelectedIndex<lstmusicList.Items.Count-1)
			{
				lstmusicList.SelectedIndex=lstmusicList.SelectedIndex+1;
				axWindowsMediaPlayer.URL=lstmusicList.Items[lstmusicList.SelectedIndex].ToString();
			}
		}
		
		void LstmusicListMouseDoubleClick(object sender, MouseEventArgs e)
		{
			axWindowsMediaPlayer.URL=lstmusicList.Items[lstmusicList.SelectedIndex].ToString();
			//this.listBox1.Items[this.listBox1.SelectedIndex].ToString()
		}

        private void MainForm_Load(object sender, EventArgs e)
        { 
            //Opacity
            //存储图片路径
            string Picturepath = null;

            //读取当前目录名
            string currentDir = Directory.GetCurrentDirectory();

            //以此为根目录,读取Wallpaper下面的文件,存储到字符串数组pictureFiles
            string[] pictureFiles = Directory.GetFiles(currentDir + "\\Wallpaper\\".ToString());

            //以Wallpaper下面的文件个数为上限,从0开始产生随机数
            //将产生的随机数作为字符串数组pictureFiles下标,
            //通过此下标来指定要显示的文件名
            Random r = new Random();
            Picturepath = pictureFiles[r.Next(0, pictureFiles.Length)];

            ActiveDesktop RefreshDesktop = new ActiveDesktop();

            IActiveDesktop iad = RefreshDesktop as IActiveDesktop;
            iad.SetWallpaper(Picturepath, 0);//设置墙纸
            iad.ApplyChanges(AD_APPLY.ALL);//启用策略,刷新桌面
           
            this.Hide();
            this.ShowInTaskbar = false;
        }

        private void txtMessage_TextChanged(object sender, EventArgs e)
        {

        }
	}
}
