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
using System.Windows.Data;
using System.Windows.Resources;

namespace czy.Silverlight.Controls
{
    public partial class Loading : UserControl
    {
        GifImageLib.GifImage WaitGif = new GifImageLib.GifImage();
        public Loading()
        {
            InitializeComponent();
            Binding b = new Binding();
            b.Source = this.Width;
            Binding b1 = new Binding();
            b.Source = this.Height;
        
            
            WaitGif.Opacity = 0;
            StreamResourceInfo sr = Application.GetResourceStream(new Uri("czy.Silverlight.Controls;component/images/15250-loading.gif", UriKind.Relative));
            WaitGif.SetSoruce(sr.Stream);
            WaitGif.SetBinding(Control.WidthProperty, b);
            WaitGif.SetBinding(Control.HeightProperty, b1);
            CanvasLoading.Children.Add(WaitGif);
        }
        public void Show()
        {
            czy.Silverlight.StoryBoard.StoryBoardBuilder.GetOpactiyStoryBoard(WaitGif, 0, 1).Begin();
        }
        public void Close()
        {
            czy.Silverlight.StoryBoard.StoryBoardBuilder.GetOpactiyStoryBoard(WaitGif, 1, 0).Begin();
        }
    }
}
