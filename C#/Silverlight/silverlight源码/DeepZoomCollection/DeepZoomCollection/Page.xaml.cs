using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;

namespace DeepZoomProject
{
    public partial class Page : UserControl
    {
        //
        // Based on prior work done by Lutz Gerhard, Peter Blois, and Scott Hanselman
        //
        Point lastMousePos = new Point();

        double _zoom = 1;
        bool mouseButtonPressed = false;
        bool mouseIsDragging = false;
        Point dragOffset;
        Point currentPosition;

        public double ZoomFactor
        {
            get { return _zoom; }
            set { _zoom = value; }
        }

        public Page()
        {

            InitializeComponent();
            msi.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                if (mouseButtonPressed)
                {
                    mouseIsDragging = true;
                }
                this.lastMousePos = e.GetPosition(this.msi);
            };

            msi.MouseLeftButtonDown += delegate(object sender, MouseButtonEventArgs e)
            {
                mouseButtonPressed = true;
                mouseIsDragging = false;
                dragOffset = e.GetPosition(this);
                currentPosition = msi.ViewportOrigin;
            };

            msi.MouseLeave += delegate(object sender, MouseEventArgs e)
            {
                mouseIsDragging = false;
            };

            msi.MouseLeftButtonUp += delegate(object sender, MouseButtonEventArgs e)
            {
                mouseButtonPressed = false;
                if (mouseIsDragging == false)
                {
                    bool shiftDown = (Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift;

                    ZoomFactor = 2.0;
                    if (shiftDown) ZoomFactor = 0.5;
                    Zoom(ZoomFactor, this.lastMousePos);
                }
                mouseIsDragging = false;
            };

            msi.MouseMove += delegate(object sender, MouseEventArgs e)
            {
                if (mouseIsDragging)
                {
                    Point newOrigin = new Point();
                    newOrigin.X = currentPosition.X - (((e.GetPosition(msi).X - dragOffset.X) / msi.ActualWidth) * msi.ViewportWidth);
                    newOrigin.Y = currentPosition.Y - (((e.GetPosition(msi).Y - dragOffset.Y) / msi.ActualHeight) * msi.ViewportWidth);
                    msi.ViewportOrigin = newOrigin;
                }
            };

            new MouseWheelHelper(msi).Moved += delegate(object sender, MouseWheelEventArgs e)
            {
                e.Handled = true;
                if (e.Delta > 0)
                    ZoomFactor = 1.2;
                else
                    ZoomFactor = .80;

                Zoom(ZoomFactor, this.lastMousePos);
            };
        }

        public void Zoom(double zoom, Point pointToZoom)
        {
            Point logicalPoint = this.msi.ElementToLogicalPoint(pointToZoom);
            this.msi.ZoomAboutLogicalPoint(zoom, logicalPoint.X, logicalPoint.Y);
        }

        //
        // A small example that arranges all of your images (provided they are the same size) into a grid
        //
        private void ArrangeIntoGrid()
        {
            
            List<MultiScaleSubImage> randomList = RandomizedListOfImages();
            int numberOfImages = randomList.Count();

            int totalImagesAdded = 0;

            int totalColumns = 3;
            int totalRows = numberOfImages / (totalColumns - 1);


            for (int col = 0; col < totalColumns; col++)
            {
                for (int row = 0; row < totalRows; row++)
                {
                    if (numberOfImages != totalImagesAdded)
                    {
                        MultiScaleSubImage currentImage = randomList[totalImagesAdded];

                        Point currentPosition = currentImage.ViewportOrigin;
                        Point futurePosition = new Point(-1.5 * col, -1.8 * row);

                        // Set up the animation to layout in grid
                        Storyboard moveStoryboard = new Storyboard();

                        // Create Animation
                        PointAnimationUsingKeyFrames moveAnimation = new PointAnimationUsingKeyFrames();

                        // Create Keyframe
                        SplinePointKeyFrame startKeyframe = new SplinePointKeyFrame();
                        startKeyframe.Value = currentPosition;
                        startKeyframe.KeyTime = KeyTime.FromTimeSpan(TimeSpan.Zero);

                        startKeyframe = new SplinePointKeyFrame();
                        startKeyframe.Value = futurePosition;
                        startKeyframe.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1));

                        KeySpline ks = new KeySpline();
                        ks.ControlPoint1 = new Point(0, 1);
                        ks.ControlPoint2 = new Point(1, 1);
                        startKeyframe.KeySpline = ks;
                        moveAnimation.KeyFrames.Add(startKeyframe);

                        Storyboard.SetTarget(moveAnimation, currentImage);
                        Storyboard.SetTargetProperty(moveAnimation, new PropertyPath("ViewportOrigin"));

                        moveStoryboard.Children.Add(moveAnimation);
                        msi.Resources.Add("unique_id", moveStoryboard);

                        // Play Storyboard
                        moveStoryboard.Begin();

                        // Now that the Storyboard has done it's work, clear the 
                        // MultiScaleImage resources.
                        msi.Resources.Clear();

                        totalImagesAdded++;
                    }
                    else
                    {
                        break;
                    }
                }
            }


        }

        private List<MultiScaleSubImage> RandomizedListOfImages()
        {
            List<MultiScaleSubImage> imageList = new List<MultiScaleSubImage>();
            Random ranNum = new Random();

            // Store List of Images
            foreach (MultiScaleSubImage subImage in msi.SubImages)
            {
                imageList.Add(subImage);
            }

            int numImages = imageList.Count;

            // Randomize Image List
            for (int i = 0; i < numImages; i++)
            {
                MultiScaleSubImage tempImage = imageList[i];
                imageList.RemoveAt(i);

                int ranNumSelect = ranNum.Next(imageList.Count);

                imageList.Insert(ranNumSelect, tempImage);

            }

            return imageList;
        }

        private void Arrange_Click(object sender, RoutedEventArgs e)
        {
            ArrangeIntoGrid();
        }
    }
}