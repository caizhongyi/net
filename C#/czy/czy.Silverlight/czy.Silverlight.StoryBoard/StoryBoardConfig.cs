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
using czy.Silverlight.Library;
using System.Collections.Generic;

namespace czy.Silverlight.StoryBoard
{
    public class StoryBoardConfig
    {
        public DependencyObject Obj { get; set; }
        public Point StartPoint { get; set; }
        public Point OffsetStartPoint { get; set; }
        public Point InitStartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Size StartSize { get; set; }
        public Size EndSize { get; set; }
        public double StartOpacity { get; set; }
        public double EndOpacity { get; set; }
        public ProjectionPoint StartProjectionPoint { get; set; }
        public ProjectionPoint EndProjectionPoint { get; set; }
        public bool State { get; set; }
        public List<Storyboard> CurrentRunStoryBoards { get; set; }
        public double StartBlurEffect { get; set; }
        public double EndBlurEffect { get; set; }
        public StoryBoardConfig()
        {
            if (CurrentRunStoryBoards == null)
            {
                CurrentRunStoryBoards = new List<Storyboard>();
            }
        }
    }
}
