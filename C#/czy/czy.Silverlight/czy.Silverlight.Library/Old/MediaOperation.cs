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

namespace Media
{
    public class MediaOperation
    {

        #region 将Media时间值附到proessBar中
        /// <summary>
        /// 将值附到proessBar中
        /// </summary>
        /// <param name="Media">MediaElement</param>
        /// <param name="proBar">进度条</param>
        /// <param name="txtcurrentTime">文本显示的当前媒体播放时间</param>
        /// <param name="txtallTime">文本显示的媒体播放的总时间</param>
        public void GetMediaValue(MediaElement Media, ProgressBar proBar, TextBlock txtcurrentTime, TextBlock txtallTime)
        {
            if (Media.CurrentState == MediaElementState.Playing)
            {
                txtcurrentTime.Text = GetMediaCurrentTime(Media);
                txtallTime.Text  =GetMediaTotalTime( Media);
                double mediaPercent=GetMediaPercent(Media);
                proBar.Value = mediaPercent;
            }

        }

        /// <summary>
        /// 获取当前Media时间
        /// </summary>
        /// <param name="Media">MediaElement</param>
        /// <returns>返回字符窜类型的Media时间</returns>
        public string GetMediaCurrentTime(MediaElement Media)
        {
            return string.Format("{0}:{1}:{2}", Media.Position.Hours.ToString(), Media.Position.Minutes.ToString(), Media.Position.Seconds.ToString());
        }

        /// <summary>
        /// 获取Media总时间
        /// </summary>
        /// <param name="Media">MediaElement</param>
        /// <returns>返回字符窜类型的Media总时间</returns>
        public string GetMediaTotalTime(MediaElement Media)
        {
            return string.Format("{0}:{1}:{2}", Media.NaturalDuration.TimeSpan.Hours.ToString(), Media.NaturalDuration.TimeSpan.Minutes.ToString(), Media.NaturalDuration.TimeSpan.Seconds.ToString());
        }

        /// <summary>
        ///  获取当前Media时间0-100中的进度值
        /// </summary>
        /// <param name="Media">MediaElement</param>
        /// <returns>Media时间0-100中的进度值</returns>
        public double GetMediaPercent(MediaElement Media)
        {
            int currentTime = Media.Position.Hours * 60 * 60 + Media.Position.Minutes * 60 + Media.Position.Seconds;
            int allTime = Media.NaturalDuration.TimeSpan.Hours * 60 * 60 + Media.NaturalDuration.TimeSpan.Minutes * 60 + Media.NaturalDuration.TimeSpan.Seconds;
            return   Convert.ToDouble(currentTime) /Convert.ToDouble( allTime)*100;
        }
        #endregion


        #region  Media附值
        /// <summary>
        /// Media附值
        /// </summary>
        /// <param name="currentPercent">当前鼠标选定的位置的0-100这间地值</param>
        /// <param name="Media">MediaElement</param>
        /// <param name="allTime">媒体的总时间(秒)</param>
        public void setMediaValue(double currentPercent, MediaElement Media, int  allTime)
        {
            int sec = Convert.ToInt16(currentPercent * allTime % 60);
            int min = Convert.ToInt16(currentPercent * allTime / 60 % 60);
            int hour = Convert.ToInt16(currentPercent * allTime / 60 / 60 % 60);
            TimeSpan time = new TimeSpan(hour, min, sec);
            Media.Position = time;
        }
        #endregion

    }
}
