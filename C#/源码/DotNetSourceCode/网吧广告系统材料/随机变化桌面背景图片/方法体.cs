
		
		private void BtnChangeWallpaperClick(object sender, EventArgs e)
		{	
			//Opacity
			//存储图片路径
			//string Picturepath=null;
			
			//读取当前目录名
			string currentDir = Directory.GetCurrentDirectory();
			
			//以此为根目录,读取Wallpaper下面的文件,存储到字符串数组pictureFiles
			string[] pictureFiles=Directory.GetFiles(currentDir+"\\Wallpaper\\".ToString());
			
			//以Wallpaper下面的文件个数为上限,从0开始产生随机数
			//将产生的随机数作为字符串数组pictureFiles下标,
			//通过此下标来指定要显示的文件名
			Random r=new Random ();		
            ActiveDesktop RefreshDesktop=new ActiveDesktop();
            IActiveDesktop iad = RefreshDesktop as IActiveDesktop ;
            iad.SetWallpaper(pictureFiles[r.Next(0,pictureFiles.Length)],0);//设置墙纸
            iad.ApplyChanges(AD_APPLY.ALL );//启用策略,刷新桌面				
        
			
			
		}	
		
	