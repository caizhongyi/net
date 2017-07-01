using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;


namespace czy.MyClass.WinForm
{
    public class ChangeDeskBackImage
    {

        
        #region 改变桌面背景图片
        /// <summary>
        /// 改变桌面背景图片
        /// </summary>
        /// <param name="from"></param>
        public void ChageBackImage( Form from,string ImageUrl)
        {
            //Opacity
            //存储图片路径
            string Picturepath = null;

            //读取当前目录名
            string currentDir = Directory.GetCurrentDirectory();

            //以此为根目录,读取Wallpaper下面的文件,存储到字符串数组pictureFiles
            string[] pictureFiles = Directory.GetFiles(currentDir + ImageUrl);

            //以Wallpaper下面的文件个数为上限,从0开始产生随机数
            //将产生的随机数作为字符串数组pictureFiles下标,
            //通过此下标来指定要显示的文件名
            Random r = new Random();
            Picturepath = pictureFiles[r.Next(0, pictureFiles.Length)];

            ActiveDesktop RefreshDesktop = new ActiveDesktop();

            IActiveDesktop iad = RefreshDesktop as IActiveDesktop;
            iad.SetWallpaper(Picturepath, 0);//设置墙纸
            iad.ApplyChanges(AD_APPLY.ALL);//启用策略,刷新桌面

            from.Hide();
            from.ShowInTaskbar = false;
        }
        #endregion
    }
}
