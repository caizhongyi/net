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

namespace czy.Silverlight.StoryBoard
{
    public class BaseStotyBoard
    {

        Storyboard _storyboard;

        public Storyboard Storyboard
        {
            get { return _storyboard; }
            set { _storyboard = value; }
        }
   
        public BaseStotyBoard()
        {
            _storyboard = new Storyboard();
        }
        public void Add(BaseKeyFrames baseKeyFrames)
        {
            _storyboard.Children.Add((Timeline)baseKeyFrames.KeyFrames);
        }
  
        public void Begin()
        {
            _storyboard.Begin();
        }
        public void Stop()
        {
            _storyboard.Stop();
        }
    }
}
