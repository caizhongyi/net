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
using czy.Silverlight.Library;
using czy.Silverlight.StoryBoard;

namespace czy.Silverlight.Controls
{

    public class ImageRotation3D
    {
        public static   int NUMBER_OF_IMAGE = 6;		    // number of images to be loaded from the folder (make sure it has enough images
        public static String CLASS_PREFIX = "img";	// the class name prefix of the images
        public static double VERTICAL_RADIUS = 90;
        public static double HORIZONTAL_RADIUS = 220;
        public static double SCALE_MIN = 0.5;			// the scale of the image at the back
        public static  double SCALE_MAX = 1.5;			// the scale of the image on the front
        public static double SPEED = 4;				// the speed of the rotation

        private  DispatcherTimer _timer;
        private  int _fps = 25;
        private  double _speedCounter = 0;
        private  List<Image> _images = new List<Image>();
        private List<Canvas> _canvas = new List<Canvas>();

        public static string root = "/Images/";
        public static string format = ".jpg";
        public static double imgWidht = 200;
        public static double imgHeight = 200;

        private Storyboard storyBodrd;
        private Vector  vec;
        private bool flag=false ;
        private Image image;

        #region 3D旋转

        /// <summary>
        /// ImageRotation3D的构造函数
        /// </summary>
        /// <param name="LayoutRoot"></param>
        public ImageRotation3D(Canvas  LayoutRoot)
        {


            addImages(LayoutRoot);
            // start the enter frame event
            _timer = new DispatcherTimer();
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1000 / _fps);
            _timer.Tick += new EventHandler(_timer_Tick);
            _timer.Start();
        }

        /////////////////////////////////////////////////////        
        // Handlers 
        /////////////////////////////////////////////////////	

        void _timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < NUMBER_OF_IMAGE; i++)
            {
                Canvas canvas = _canvas[i];
                // reposition the images
                posImage(canvas, i);
            }
            _speedCounter += SPEED;
        }

        /////////////////////////////////////////////////////        
        // Private Methods 
        /////////////////////////////////////////////////////	
        //void imageUrlClick()
        //{

        //}
        void stopTimer(object sender, MouseEventArgs args)
        {
            UIElement img = (UIElement)sender;
            img.Opacity+=0.5;
           _timer.Stop();
        }
        void starTimer(object sender, MouseEventArgs args)
        {
            //if (storyBodrd != null)
            //{
            //    //storyBodrd.AutoReverse = true;
            //    //storyBodrd.Seek(TimeSpan.FromMilliseconds(59));
            //    //storyBodrd.Begin();
           
            //    //storyBodrd.Completed += new EventHandler(storyBodrd_Completed);
            //}
            UIElement img = (UIElement)sender;
            if (flag)
            {
              
                Point transPoint = new Point(0, 0);
                Point scalePoint = new Point(vec.X, vec.Y);
                //BaseStoryBoard bsb = new BaseStoryBoard(img,1);
              //  storyBodrd = bsb.CreateScaleStoryboard(transPoint, scalePoint, TimeSpan.FromMilliseconds(120), TimeSpan.FromMilliseconds(120));
                img.SetValue(Canvas.ZIndexProperty,1);
                // MessageBox.Show(_images[i]);
                storyBodrd.AutoReverse = false;
                storyBodrd.Begin();
                storyBodrd.Completed += new EventHandler(storyBodrd_Completed);
                
            }
            else
            {
               
                _timer.Start();
            }
            img.Opacity -= 0.5;
        }

        void storyBodrd_Completed(object sender, EventArgs args)
        {
            
            //scaleStoryboard[0].Begin();
           // MessageBox.Show(image.ActualWidth.ToString ());
            _timer.Start();
            flag = false;
        
        }

        void mouseLeftButtonDown(object sender, MouseButtonEventArgs args)
        {
         
            //scaleStoryboard[0].Begin();
         
        }
        void MouseLeftButtonUp(object sender, MouseButtonEventArgs args)
        {
                UIElement img = (UIElement)sender;
                ScaleTransform scaleTransform = new ScaleTransform();
                try
                {
                    
                    scaleTransform = (ScaleTransform)img.RenderTransform;
             
                    vec = new Vector();
                    vec.X = scaleTransform.ScaleX;
                    vec.Y = scaleTransform.ScaleY;
                    Point transPoint = new Point(0, 0);
                    Point scalePoint = new Point(3, 3);
                    //BaseStoryBoard bsb = new BaseStoryBoard(img, 1);
                    //storyBodrd = bsb.CreateScaleStoryboard(transPoint, scalePoint, TimeSpan.FromMilliseconds(120), TimeSpan.FromMilliseconds(120));
                    img.SetValue(Canvas.ZIndexProperty, 100);
               
                    storyBodrd.AutoReverse = false;
                    storyBodrd.Begin();
                    storyBodrd.Completed += new EventHandler(storyBodrd_Completed1);

                    img.Opacity = 1;

                    
                }
                catch {
                    //MessageBox.Show(((TransformGroup)img.RenderTransform).Children[0]);
                }
        }

        void canvas_MouseMove(object sender, MouseEventArgs args)
        {
            UIElement element = (UIElement)sender;
           // element.c

        }

        void storyBodrd_Completed1(object sender, EventArgs args)
        {

            //scaleStoryboard[0].Begin();
            // MessageBox.Show(image.ActualWidth.ToString ());
            flag = true;

        }
        /// <summary>
        /// 增加文件
        /// </summary>
        /// <param name="LayoutRoot">Grid</param>
        private void addImages(Canvas  LayoutRoot)
        {
            for (int i = 0; i < NUMBER_OF_IMAGE; i++)
            {
                // load the images from the Folder
                string url = root + CLASS_PREFIX + i + format;
                Canvas canvas = new Canvas();
                Image image = new Image();
                image.Name = "image" + i;
                image.Source = new BitmapImage(new Uri(url, UriKind.Relative));
                canvas.MouseLeftButtonDown += new MouseButtonEventHandler(mouseLeftButtonDown);
                canvas.MouseLeftButtonUp += new MouseButtonEventHandler(MouseLeftButtonUp);
                canvas.MouseEnter += new MouseEventHandler(stopTimer);
                canvas.MouseLeave += new MouseEventHandler(starTimer);
                canvas.MouseMove+=new MouseEventHandler(canvas_MouseMove);
                image.Height = imgHeight;
                image.Width = imgWidht;


                SolidColorBrush sc = new SolidColorBrush(Color.FromArgb(255, 233, 230, 230));
                Brush br = sc;
         

                Rectangle r = new Rectangle();
                r.Stroke = br;
                r.StrokeThickness = 4;
                r.RadiusX = 5;
                r.RadiusY = 5;
                r.Width = imgWidht;
                r.Height = imgHeight;


                ImageBrush imgbr=new ImageBrush ();
                imgbr.ImageSource=image .Source;
                r.Fill = imgbr;
                canvas.Children.Add(r);

                canvas.Opacity-=0.5;
               // LayoutRoot.Children.Add(image);
                LayoutRoot.Children.Add(canvas);
                _images.Add(image);
                _canvas.Add(canvas);
                
                posImage(canvas, i);
            }
        }


        // reposition the images according to their adding sequence
        /// <summary>
        /// 旋转函数
        /// </summary>
        /// <param name="image">图片</param>
        /// <param name="index">图片的index</param>
        private void posImage(UIElement  image, int index)
        {
            // calculate the angle of the image
            double angle = (_speedCounter / 180 + (double)index / NUMBER_OF_IMAGE * 2) * Math.PI;

            // scale the image
            ScaleTransform scaleTransform = new ScaleTransform();
            scaleTransform.ScaleX = (SCALE_MAX + SCALE_MIN) / 2 + (SCALE_MAX - SCALE_MIN) / 2 * Math.Cos(angle);
            scaleTransform.ScaleY = scaleTransform.ScaleX;
            image.RenderTransform = scaleTransform;


            // position the image
            image.SetValue(Canvas.LeftProperty, Math.Sin(angle) * HORIZONTAL_RADIUS - (double)image.GetValue(FrameworkElement.ActualWidthProperty) / 2 * scaleTransform.ScaleX);
            image.SetValue(Canvas.TopProperty, Math.Cos(angle) * VERTICAL_RADIUS - (double)image.GetValue(FrameworkElement.ActualHeightProperty) / 2 * scaleTransform.ScaleY);

            // sort the children according to their y position
            image.SetValue(Canvas.ZIndexProperty, (int)((double)image.GetValue(Canvas.TopProperty)));

        }

        #endregion
    }

}