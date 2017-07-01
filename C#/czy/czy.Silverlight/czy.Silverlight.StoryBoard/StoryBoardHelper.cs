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
    public class StoryBoardHelper : UserControl
    {
        public static Storyboard GetStoryBoard(DependencyObject obj,Storyboard sb)
        {
            for (int i = 0; i < sb.Children.Count; i++)
            {
                Storyboard.SetTarget(sb.Children[i], obj);
            }
            return sb;
            
        }
    }
}
