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
using czy.Silverlight.StoryBoard;

namespace czy.Silverlight.Demo
{
    public partial class StoryBoardTest : UserControl
    {
        public StoryBoardTest()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
         //   StoryBoard.StoryBoardBuilder.GetZoomAndMoveStoryBoardByCanvas(rec,  new Point(200, 200),new Size( 400, 400)).Begin ();
           // BaseStoryBoardBuilder.ZoomSize(((TransformGroup)rec.RenderTransform).Children[0], 10,9).Begin ();
           //StoryBoardBuilder.GetZoomStoryBoardByScale(((TransformGroup)rec.RenderTransform).Children[0],1, 10).Begin();
        }

        private void rec1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
           // StoryBoard.StoryBoardBuilder.GetOpactiyStoryBoard(rec1, 0).Begin ();
           
        } 

        private void rec2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

             StoryBoard.StoryBoardBuilder.GetSplineStoryBoard(rec2,new Point(100,200)).Begin();
        }

        private void rec3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            StoryBoardConfig config = new StoryBoardConfig();
            config.Obj=rec3;
            config.StartOpacity = 0;
            config.EndOpacity = 1;
            config.StartSize = new Size(400, 0);
            config.EndSize = new Size(400, 100);
        //    StoryBoard.StoryBoardBuilder.GetOpactiySizeStoryBoard(config.Obj,  config.EndOpacity,  config.EndSize).Begin ();
            
        }
    }
    public class StoryBoardConfig
    {
        public DependencyObject Obj { get; set; }
        public Size StartSize { get; set; }
        public Size EndSize { get; set; }
        public double StartOpacity { get; set; }
        public double EndOpacity { get; set; }
    }
}
