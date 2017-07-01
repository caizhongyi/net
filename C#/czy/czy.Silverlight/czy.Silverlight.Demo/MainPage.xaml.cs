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


namespace czy.Silverlight.Demo
{
    public partial class MainPage : UserControl
    {
       
        public MainPage()
        {
            InitializeComponent();
            this.LayoutRoot.Children.Add(new StoryBoardTest());
           //// this.LayoutRoot.Children.Add(new ShowPicturesDemo());

           // ShowPictures s = new ShowPictures(new string[] {
           //     "/images/img0.jpg",
           //     "/images/img1.jpg", 
           //     "/images/img2.jpg", 
           //     "/images/img3.jpg",
           //     "/images/img4.jpg",
           //     "/images/img5.jpg",
           //     "/images/img6.jpg", 
           //     "/images/img7.jpg" 
           // }, new Size(100, 100));
           // s.Width = 400;
           // s.Height = 400;

           // s.BorderThickness = new Thickness(2);
           // //SolidColorBrush scb = new SolidColorBrush();
           // //scb.Color = Colors.Black;
           // //s.Background = scb;
           // s.Style = this.Resources["sp1"] as Style;
           // this.LayoutRoot.Children.Add(s);


            //this.LayoutRoot.Children.Add( new Demo_DataGrid());

        }
    
    }
}
