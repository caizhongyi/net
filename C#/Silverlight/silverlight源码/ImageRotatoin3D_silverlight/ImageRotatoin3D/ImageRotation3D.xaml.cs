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
using System.Windows.Threading;
using System.Windows.Media.Imaging;
using System.Windows.Browser;
using System.Diagnostics;


namespace ImageRotatoin3D
{
    public partial class ImageRotation3D : UserControl
    {
        private static int NUMBER_OF_IMAGE = 6;		    // number of images to be loaded from the folder (make sure it has enough images
        private static String CLASS_PREFIX = "Logo";	// the class name prefix of the images
        private static double VERTICAL_RADIUS = 150;
        private static double HORIZONTAL_RADIUS = 300;
        private static double SCALE_MIN = 0.5;			// the scale of the image at the back
        private static double SCALE_MAX = 1.5;			// the scale of the image on the front
        private static double SPEED = 3;				// the speed of the rotation

        private DispatcherTimer _timer;
        private int _fps = 25;
        private double _speedCounter = 0;
        private List<Image> _images = new List<Image>();

        public ImageRotation3D()
        {
            InitializeComponent();
            //Image img = new Image();
            //img.Source = new BitmapImage(new Uri("images/Logo1.png", UriKind.Relative));
            //img.Height = 1024;
            //img.Width = 1024;
            //LayoutRoot.Children.Add(img);
            //addImages();

            // start the enter frame event
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1000/_fps);
            _timer.Tick +=new EventHandler(_timer_Tick);
            _timer.Start();
        }

        /////////////////////////////////////////////////////        
		// Handlers 
		/////////////////////////////////////////////////////	
		
		void  _timer_Tick(object sender, EventArgs e)
        {
			for(int i = 0; i < NUMBER_OF_IMAGE; i++) {
				Image image = _images[i];
				// reposition the images
				posImage(image, i);
			}
			_speedCounter += SPEED;
		}
		
		/////////////////////////////////////////////////////        
		// Private Methods 
		/////////////////////////////////////////////////////	
        //void imageUrlClick()
        //{
                
        //}
        void stopTimer(object sender,MouseEventArgs args)
        {
            _timer.Stop();
        }
        void starTimer(object sender, MouseEventArgs args)
        {
            _timer.Start();
        }

        void mouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
           
            
        }
		private void addImages(){
			for(int i = 0; i < NUMBER_OF_IMAGE; i++) {
				// load the images from the Folder
	            string url = "images/" + CLASS_PREFIX + i + ".png";

                Image image = new Image();
                image.Source = new BitmapImage(new Uri(url, UriKind.Relative));
                image.MouseLeftButtonDown += new MouseButtonEventHandler(mouseLeftButtonDown);
                image.MouseMove += new MouseEventHandler(stopTimer);
                image.MouseLeave += new  MouseEventHandler(starTimer);
                image.Width = 400;
                image.Height =300;
                
				LayoutRoot.Children.Add(image);



                _images.Add(image);
                posImage(image, i);
			}
		}
		
		// reposition the images according to their adding sequence
		private void posImage(Image image, int index){
			// calculate the angle of the image
			double angle = (_speedCounter/180  + (double) index/NUMBER_OF_IMAGE * 2) * Math.PI;
            
            // scale the image
            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = (SCALE_MAX + SCALE_MIN ) /2  + (SCALE_MAX - SCALE_MIN) / 2 * Math.Cos(angle);
            scaleTransform.ScaleY = scaleTransform.ScaleX;
            image.RenderTransform = scaleTransform;
            

            // position the image
            image.SetValue(Canvas.LeftProperty, Math.Sin(angle) * HORIZONTAL_RADIUS - (double)image.GetValue(FrameworkElement.ActualWidthProperty) / 2 * scaleTransform.ScaleX);
            image.SetValue(Canvas.TopProperty, Math.Cos(angle) * VERTICAL_RADIUS - (double)image.GetValue(FrameworkElement.ActualHeightProperty) / 2 * scaleTransform.ScaleY);

            // sort the children according to their y position
            image.SetValue(Canvas.ZIndexProperty, (int) ((double) image.GetValue(Canvas.TopProperty)));
          
		}
		   
    }
}
