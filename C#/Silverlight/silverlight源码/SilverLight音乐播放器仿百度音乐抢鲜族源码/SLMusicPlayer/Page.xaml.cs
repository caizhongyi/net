using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SLMusicPlayer
{
    public partial class Page : UserControl
    {
        List<Music> MusicDateList;
        int status = 0;//0停止,1播放,2暂停

        public Page()
        {
            InitializeComponent();
            MusicDateList =new List<Music> ();

            this.Loaded += new RoutedEventHandler(Page_Loaded);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            MusicDateList.Add(new Music() { SongUrl = "http://bbs.lehu.shu.edu.cn/Uploads/UserDirs/1/51/20276/%E5%91%A8%E6%9D%B0%E4%BC%A6-%E5%9B%9E%E5%88%B0%E8%BF%87%E5%8E%BB(%E5%85%AB%E5%BA%A6%E7%A9%BA%E9%97%B4).mp3", SongSinger = "周杰伦", SongName = "回到过去" });
            MusicDateList.Add(new Music() { SongUrl = "http://www.skateba.com/music/%E4%B8%80%E8%BE%88%E5%AD%90%E7%9A%84%E5%AD%A4%E5%8D%95.mp3", SongSinger = "刘若英", SongName = "一辈子的孤单" });
            MusicDateList.Add(new Music() { SongUrl = "http://music.hakkaw.com/uploadmtv/20068252144541.wma", SongSinger = "梁静茹", SongName = "如果有一天" });
            MusicDateList.Add(new Music() { SongUrl = "http://wish.wuhan.net.cn/media/d1f582bd-2d45-41b3-be05-5e95ff311305.mp3", SongSinger = "光良", SongName = "第一次" });
            MusicDateList.Add(new Music() { SongUrl = "http://blog.bjradio.com.cn/attachments/2007/10/85009_200710082215501.mp3", SongSinger = "五月天", SongName = "知足" });
            MusicDateList.Add(new Music() { SongUrl = "http://www.zzchildren.com/news/edit/UploadFile/20086318440132.mp3", SongSinger = "群星合唱", SongName = "北京欢迎你" });
            MusicDateList.Add(new Music() { SongUrl = "http://bbsimages.military.china.com/twhb/1011/2009/4/7/1239113605929.mp3", SongSinger = "阿桑", SongName = "一直很安静" });
            MusicDateList.Add(new Music() { SongUrl = "http://cdn1-56.projectplaylist.com/e1/static12/mp3/2777909.mp3", SongSinger = "王力宏", SongName = "心跳" });
            MusicDateList.Add(new Music() { SongUrl = "http://localhost:2024/meirenyu.mp3", SongSinger = "林俊杰", SongName = "美人鱼" });
            MusicDateList.Add(new Music() { SongUrl = "http://a163.com/UploadFiles/11381/%B0%A2%C9%A3-07-%CE%C2%C8%E1%B5%C4%B4%C8%B1%AF.mp3", SongSinger = "阿桑", SongName = "温柔的慈悲" });
            MusicDateList.Add(new Music() { SongUrl = "http://file.m-zone.cn:8080/getfile/http://file.m-zone.cn:8080/getfile/jVwt3tjSEGYvEBbuEBMs3t6weBL-igM.mp3", SongSinger = "刘德华", SongName = "练习" });
            MusicDateList.Add(new Music() { SongUrl = "http://blog.qizu.com/music/2008-8/life.mp3", SongSinger = "汪峰", SongName = "怒放的生命" });
            MusicDateList.Add(new Music() { SongUrl = "http://baobao.qq.com/data/baobao_395135214_video_20080811104257.mp3", SongSinger = "蔡依林", SongName = "日不落" });
            MusicDateList.Add(new Music() { SongUrl = "http://cflc.xmu.edu.cn/linli/linli/student/2006/chenjianjun/music/zhaoziji.mp3", SongSinger = "陶喆", SongName = "找自己" });
            MusicDateList.Add(new Music() { SongUrl = "http://pic.baa.com.cn/temp/music/200852214912570.mp3", SongSinger = "许美静", SongName = "阳光总在风雨后" });

            this.SongListBox.ItemsSource = MusicDateList;
            VolumeSlider.Value = 0.5;
        }

        private void PlayButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MusicMedia.Play();
            ((Storyboard)this.FindName("StartStoryboard")).Resume();
        }

        private void SongListBox_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void PauseButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MusicMedia.Pause();
            ((Storyboard)this.FindName("StartStoryboard")).Pause();
        }

        private void StopButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MusicMedia.Stop();
            ((Storyboard)this.FindName("StartStoryboard")).Stop();
        }

        private void SongListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                MusicName.Text = "";
                int i = SongListBox.SelectedIndex;
                MusicName.Text = "歌曲名称：" + MusicDateList[i].SongName.ToString();

                MusicMedia.AutoPlay = true;
                MusicMedia.Source = new Uri(MusicDateList[i].SongUrl.ToString());
                MusicMedia.Play();

                ((Storyboard)this.FindName("StartStoryboard")).BeginTime = new TimeSpan(0, 0, 0);
                //((SplineDoubleKeyFrame)this.FindName("endTime")).KeyTime
                ((Storyboard)this.FindName("StartStoryboard")).Begin();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VolumeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MusicMedia.Volume = VolumeSlider.Value;
        }
    }
}
