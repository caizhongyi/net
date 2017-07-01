using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
//源码下载及讨论地址：http://www.51aspx.com/CV/SLMusicPlayer
//该源码下载自www.51aspx.com(５１ａsｐｘ．ｃｏｍ)

namespace SLMusicPlayer
{
    public class Music : INotifyPropertyChanged
    {
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        public Music()
        {

        }
        private string _SongUrl;
        private string _SongSinger;
        private string _SongName;
        /// <summary>
        /// 歌曲URL
        /// </summary>
        public string SongUrl
        {
            get { return _SongUrl; }
            set 
            {
                _SongUrl = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SongUrl"));
                }
            }
        }
        /// <summary>
        /// 歌唱者
        /// </summary>
        public string SongSinger
        {
            get { return _SongSinger; }
            set 
            {
                _SongSinger = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SongSinger"));
                }
            }
        }
        /// <summary>
        /// 歌曲名称
        /// </summary>
        public string SongName
        {
            get { return _SongName; }
            set 
            { 
                _SongName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SongName"));
                }

            }
        }

        //public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        //public void NotifyPropertyChanged(string propertyName)
        //{

        //    if (PropertyChanged != null)
        //    {
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        //    }

        //}  
    }
}
