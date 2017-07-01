//---------------------------------------------------------------------------
//
// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Limited Permissive License.
// See http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedpermissivelicense.mspx
// All other rights reserved.
//
// This file is part of the 3D Tools for Windows Presentation Foundation
// project.  For more information, see:
// 
// http://CodePlex.com/Wiki/View.aspx?ProjectName=3DTools
//
//---------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;

using PhotoBrowser.CarouselView;
using PhotoBrowser.FlickrApi;
using PhotoBrowser.Stack3D;
using PhotoBrowser.Shapes;

using _3DTools;

namespace PhotoBrowser
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : System.Windows.Window
    {
        public Window1()
        {
            InitializeComponent();

            Flickr.Initialize();

            cv = new PhotoCarouselView();
            ps = new PhotoStack3D();

            Transform3DGroup cvTG = new Transform3DGroup();
            cvTG.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), 5)));
            cvTG.Children.Add(new TranslateTransform3D(0, 0, -1));
            cv.Transform = cvTG;
            
            Transform3DGroup tg = new Transform3DGroup();
            tg.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), -13)));
            tg.Children.Add(new TranslateTransform3D(0, ps.Height / 2, -1.2));
            ps.Transform = tg; 

            MainViewport.Children.Add(cv);
            MainViewport.Children.Add(ps);

            cv.ItemClickedOn += new PhotoBrowser.CarouselView.CarouselView.ItemClickedOnEvent(cv_ItemClickedOn);
            cv.GeoLocationAvailable += new PhotoCarouselView.GeoEventHandler(cv_GeoLocationAvailable);
            cv.GeoLocationHidden += new PhotoCarouselView.GeoEventHandler(cv_GeoLocationHidden);

            ps.GeoLocationSelected += new PhotoStack3D.GeoLocationSelectedDelegate(ps_GeoLocationSelected);
            ps.SelectionChanged += new PhotoBrowser.Stack3D.Stack3D.StackSelectionChangedEvent(ps_SelectionChanged);
            ps.BlogRequested += new PhotoStack3D.BlogRequestedDelegate(ps_BlogRequested);

            // create the globe we'll use
            earth = new InteractiveSphere();
            mapVisual = new MapVisual();
            mapVisual.searchMenuItem.Click += new RoutedEventHandler(searchMenuItem_Click);
            earth.Visual = mapVisual;
            
            // create the transformation applied to the globe
            earthTransform = new Transform3DGroup();
            Point3D earthPos = new Point3D(-0.2, 0.05, -1.1);
            Vector3D earthToCam = new Vector3D(0 - earthPos.X, 
                                               0 - earthPos.Y, 
                                               0 - earthPos.Z);
            earthToCam.Normalize();

            earthTransform.Children.Add(new ScaleTransform3D(0.0, 0.0, 0.0));
            _myQuaternionRotLong = new QuaternionRotation3D(new Quaternion(new Vector3D(0, 1, 0), 0));
            _myQuaternionRotLat = new QuaternionRotation3D(new Quaternion(new Vector3D(1, 0, 0), 0));

            earthTransform.Children.Add(new RotateTransform3D(_myQuaternionRotLong));            
            earthTransform.Children.Add(new RotateTransform3D(_myQuaternionRotLat));
            earthTransform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0),
                                        Math.Acos(Vector3D.DotProduct(new Vector3D(0, 0, 1), earthToCam)) / Math.PI * 180)));
            
            earthTransform.Children.Add(new TranslateTransform3D((Vector3D)earthPos));
            earth.Transform = earthTransform;

            // add in the earth
            HookUpEarthEvents();
        }
        
        /// <summary>
        /// Adds input event logic to the earth control
        /// </summary>
        private void HookUpEarthEvents()
        {
            mapVisual.MouseDown += 
                delegate(object sender, System.Windows.Input.MouseButtonEventArgs e)
                {
                    if (e.RightButton == MouseButtonState.Pressed)
                    {
                        mapVisPreviousPoint = e.GetPosition(MainViewport);
                        mapVisual.CaptureMouse();
                    }
                };

            mapVisual.MouseMove += 
                delegate(object sender, System.Windows.Input.MouseEventArgs e)
                {
                    if (mapVisual.IsMouseCaptured)
                    {
                        Vector diff = e.GetPosition(MainViewport) - mapVisPreviousPoint;

                        _myQuaternionRotLong.Quaternion *= new Quaternion(new Vector3D(0, 1, 0), diff.X);
                        _myQuaternionRotLat.Quaternion *= new Quaternion(new Vector3D(1, 0, 0), diff.Y);

                        mapVisPreviousPoint = e.GetPosition(MainViewport);
                    }
                };

            mapVisual.MouseUp +=
                delegate(object sender, MouseButtonEventArgs e)
                {
                    if (mapVisual.IsMouseCaptured)
                    {
                        mapVisual.ReleaseMouseCapture();
                    }
                };
        }

        private void ps_BlogRequested(FlickrPhoto photo)
        {
            blogVisual3D = new InteractiveVisual3D();

            MeshGeometry3D blogMesh = (MeshGeometry3D)Resources["PlaneMesh"];
            UIElement blogMeshVisual = new BlogVisual();

            blogVisual3D.Geometry = blogMesh;
            blogVisual3D.Visual = blogMeshVisual;

            Transform3DGroup transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new ScaleTransform3D(0.17, 0.17, 0.17));
            transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 15)));
            transformGroup.Children.Add(new TranslateTransform3D(-0.20, 0, -1));
            blogVisual3D.Transform = transformGroup;

            MainViewport.Children.Add(blogVisual3D);
        }

        private void searchMenuItem_Click(object sender, RoutedEventArgs e)
        {
            Rect latLongRect = mapVisual.GetSearchRect();

            Flickr.AsynchSearchForPhotos(latLongRect,
                                         cv.Dispatcher,
                                         delegate(object o)
                                         {
                                             earth.Children.Clear();
                                             cv.Data = (FlickrPhotos)o;
                                         });
        }

        private void ps_SelectionChanged(object selected)
        {
            if (blogVisual3D != null)
            {
                MainViewport.Children.Remove(blogVisual3D);
            }

            if (!earthButtonClicked)
            {
                MakeEarthInVisible();
            }            
        }
               
        private void MakeEarthVisible()
        {
            if (!MainViewport.Children.Contains(earth))
            {
                MainViewport.Children.Add(earth);
            }

            // transform in the globe
            DoubleAnimation scaleAnim = new DoubleAnimation(0.15, TimeSpan.FromMilliseconds(500));
            ScaleTransform3D scaleXForm = (ScaleTransform3D)(((Transform3DGroup)earth.Transform).Children[0]);

            scaleXForm.BeginAnimation(ScaleTransform3D.ScaleXProperty, scaleAnim);
            scaleXForm.BeginAnimation(ScaleTransform3D.ScaleYProperty, scaleAnim);
            scaleXForm.BeginAnimation(ScaleTransform3D.ScaleZProperty, scaleAnim);
        }

        private void MakeEarthInVisible()
        {
            if (MainViewport.Children.Contains(earth))
            {
                // transform in the globe
                DoubleAnimation scaleAnim = new DoubleAnimation(0.0, TimeSpan.FromMilliseconds(500));
                ScaleTransform3D scaleXForm = (ScaleTransform3D)(((Transform3DGroup)earth.Transform).Children[0]);

                scaleXForm.BeginAnimation(ScaleTransform3D.ScaleXProperty, scaleAnim);
                scaleXForm.BeginAnimation(ScaleTransform3D.ScaleYProperty, scaleAnim);
                scaleXForm.BeginAnimation(ScaleTransform3D.ScaleZProperty, scaleAnim);

                scaleAnim.Completed += delegate(object sender, EventArgs e)
                {
                    MainViewport.Children.Remove(earth);
                };
            }
        }

        void ps_GeoLocationSelected(double longitude, double latitude, FlickrPhoto photo)
        {
            MakeEarthVisible();            

            // convert relative to our object
            longitude = 180 + longitude;

            if (earthLocationVisual != null)
            {
                earthLocationVisual.SetPinFill(Brushes.White);
                ((Transform3DGroup)earthLocationVisual.Transform).Children.RemoveAt(0);
            }

            earthLocationVisual = _photoToPushpin[photo];
            earthLocationVisual.SetPinFill(Brushes.Red);

            // if it had any children, clear the now           
            Transform3DGroup transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new ScaleTransform3D(1, 1, 1.1));
            transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), -latitude)));                        
            transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), longitude)));
            earthLocationVisual.Transform = transformGroup;

            if (!earth.Children.Contains(earthLocationVisual))
            {
                earth.Children.Add(earthLocationVisual);
            }

            QuaternionAnimation quatAnimLong = new QuaternionAnimation(new Quaternion(new Vector3D(0, 1, 0), -longitude),
                                                                       new Duration(TimeSpan.FromMilliseconds(500)));
            QuaternionAnimation quatAnimLat = new QuaternionAnimation(new Quaternion(new Vector3D(1, 0, 0), latitude), 
                                                                      new Duration(TimeSpan.FromMilliseconds(500)));

            quatAnimLat.Completed += new EventHandler(quatAnimLat_Completed);

            _myQuaternionRotLong.BeginAnimation(QuaternionRotation3D.QuaternionProperty, quatAnimLong);
            _myQuaternionRotLat.BeginAnimation(QuaternionRotation3D.QuaternionProperty, quatAnimLat);
        }

        void cv_GeoLocationHidden(FlickrPhoto photo, Point latLong)
        {
            earth.Children.Remove(_photoToPushpin[photo]);
            _photoToPushpin.Remove(photo);
        }

        public class GeoLocationPushpin : InteractiveVisual3D
        {
            public GeoLocationPushpin(FlickrPhoto photo, Point loc, PhotoStack3D stack, Window1 win1)
            {
                _photo = photo;
                _pstack = stack;
                _window = win1;
                _loc = loc;

                // set the geometry for the pushpin
                Geometry = _geometry;

                // we do this to make the push pin interactive
                _visualRep = new Rectangle();
                _visualRep.Width = 10;
                _visualRep.Height = 10;
                _visualRep.Fill = Brushes.White;
                _visualRep.MouseLeftButtonDown += new MouseButtonEventHandler(MouseLeftButtonDown);
                Visual = _visualRep;
            }

            static GeoLocationPushpin()
            {
                CreatePushpinGeometry();
            }

            private static void CreatePushpinGeometry()
            {
                MeshGeometry3D geometry = new MeshGeometry3D();

                // Create a collection of vertex positions for the MeshGeometry3D. 
                Point3DCollection ptrVertices = new Point3DCollection();
                ptrVertices.Add(new Point3D(0, 0, 0));
                ptrVertices.Add(new Point3D(-0.01, -0.01, 1.2));
                ptrVertices.Add(new Point3D(0.01, -0.01, 1.2));
                ptrVertices.Add(new Point3D(0, 0.01, 1.2));
                geometry.Positions = ptrVertices;

                // Create a collection of triangle indices for the MeshGeometry3D.
                Int32Collection ptrTriangleIndices = new Int32Collection();
                ptrTriangleIndices.Add(1);
                ptrTriangleIndices.Add(2);
                ptrTriangleIndices.Add(3);

                ptrTriangleIndices.Add(0);
                ptrTriangleIndices.Add(1);
                ptrTriangleIndices.Add(3);

                ptrTriangleIndices.Add(0);
                ptrTriangleIndices.Add(3);
                ptrTriangleIndices.Add(2);

                ptrTriangleIndices.Add(0);
                ptrTriangleIndices.Add(2);
                ptrTriangleIndices.Add(1);
                geometry.TriangleIndices = ptrTriangleIndices;

                // create a collection of texture coordinates for the MeshGeometry3D
                PointCollection ptrTexCoordindates = new PointCollection();
                ptrTexCoordindates.Add(new Point(0.5, 0.5));
                ptrTexCoordindates.Add(new Point(0.5, 0.5));
                ptrTexCoordindates.Add(new Point(0.5, 0.5));

                ptrTexCoordindates.Add(new Point(0.5, 0.5));
                ptrTexCoordindates.Add(new Point(0.5, 0.5));
                ptrTexCoordindates.Add(new Point(0.5, 0.5));

                ptrTexCoordindates.Add(new Point(0.5, 0.5));
                ptrTexCoordindates.Add(new Point(0.5, 0.5));
                ptrTexCoordindates.Add(new Point(0.5, 0.5));

                ptrTexCoordindates.Add(new Point(0.5, 0.5));
                ptrTexCoordindates.Add(new Point(0.5, 0.5));
                ptrTexCoordindates.Add(new Point(0.5, 0.5));
                geometry.TextureCoordinates = ptrTexCoordindates;

                // Apply the mesh to the geometry model.
                geometry.Freeze();

                _geometry = geometry;
            }

            void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
            {
                _pstack.AddItem(_photo);
                _window.ps_GeoLocationSelected(_loc.Y, _loc.X, _photo);
            }

            public void SetPinFill(Brush b)
            {
                _visualRep.Fill = b;
            }

            // the geometry
            private static MeshGeometry3D _geometry;

            public FlickrPhoto _photo;
            public PhotoStack3D _pstack;
            public Window1 _window;
            public Point _loc;

            public Rectangle _visualRep;
        }

        void cv_GeoLocationAvailable(FlickrPhoto photo, Point latLong)
        {
            GeoLocationPushpin model = new GeoLocationPushpin(photo, latLong, ps, this);
            

            Transform3DGroup transformGroup = new Transform3DGroup();
            transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), -latLong.X)));
            transformGroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), latLong.Y + 180)));
            model.Transform = transformGroup;

            earth.Children.Add(model);
            _photoToPushpin[photo] = model;
        }


        void quatAnimLat_Completed(object sender, EventArgs e)
        {
            Quaternion currQuatLat = _myQuaternionRotLat.Quaternion;
            _myQuaternionRotLat.BeginAnimation(QuaternionRotation3D.QuaternionProperty, null);
            _myQuaternionRotLat.Quaternion = currQuatLat;

            Quaternion currQuatLong = _myQuaternionRotLong.Quaternion;
            _myQuaternionRotLong.BeginAnimation(QuaternionRotation3D.QuaternionProperty, null);
            _myQuaternionRotLong.Quaternion = currQuatLong;
        }

        private Vector3D SphericalToCartesian(double theta, double phi)
        {
            // degrees to radians
            theta = theta / 180.0 * Math.PI;
            phi = phi / 180.0 * Math.PI;

            double x = 1 * Math.Sin(theta) * Math.Sin(phi);
            double y = 1 * Math.Cos(phi);
            double z = 1 * Math.Cos(theta) * Math.Sin(phi);

            return new Vector3D(x, y, z);
        }

        void cv_ItemClickedOn(object sender, object item)
        {
            ps.AddItem((FlickrPhoto)item);
        }

        public void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string searchTerm = SearchTextBox.Text;

            if (searchTerm != "")
            {
                Flickr.AsynchSearchForPhotos(searchTerm,                    
                                             cv.Dispatcher,
                                             delegate(object o)
                                             {
                                                 cv.Data = (FlickrPhotos)o;
                                             });
            }
        }

        public void OnGeoButtonClicked(object sender, EventArgs e)
        {
            if (earthButtonClicked)
            {
                MakeEarthInVisible();
            }
            else
            {
                MakeEarthVisible();                
            }

            earthButtonClicked = !earthButtonClicked;
        }

        public void TextBoxKeyPressed(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                OnSearchButtonClicked(sender, e);
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Flickr.Shutdown();
        }

        /// <summary>
        /// Handles authenticating the user once the authenicated button is clicked on
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void OnAuthenticate(object sender, EventArgs e)
        {
            AuthenticationGrid.Visibility = Visibility.Visible;
            confirmAuthentButton.IsEnabled = false;

            Flickr.AsynchGetFrob(Dispatcher,
                                 delegate(object o)
                                 {
                                     frob = (string)o;
                                     string loginUrl = Flickr.GetLoginUrl(frob, "write");

                                     System.Diagnostics.Process.Start(loginUrl);
                                     confirmAuthentButton.IsEnabled = true;
                                     authenticateButton.IsEnabled = false;
                                 });
        }

        public void AuthenticateUser(object sender, EventArgs e)
        {
            AuthenticationGrid.Visibility = Visibility.Hidden;

            Flickr.AsynchGetAuthenticatedUser(frob,
                                              Dispatcher,
                                              delegate(object o)
                                              {
                                                  user = (AuthorizedFlickrUser)o;

                                                  AuthenticationGrid.Visibility = Visibility.Hidden;
                                                  confirmAuthentButton.IsEnabled = false;

                                                  authenticateButton.Visibility = Visibility.Hidden;

                                                  
                                                  logInLabel.Visibility = Visibility.Visible;

                                                  Flickr.AsynchGetRecentlyUpdated(user,
                                                                               cv.Dispatcher,
                                                                               delegate(object photos)
                                                                               {
                                                                                   cv.Data = (FlickrPhotos)photos;
                                                                               });
                                              });
        }
        
        public void outerGridKeyDown(Object sender, KeyEventArgs e)
        {
            InteractiveVisual3D newVis = null;

            // figure out what mesh is requested
            if (e.Key == Key.F2)
            {
                newVis = reallyFakeIV3D;
            }
            else if (e.Key == Key.F3)
            {
                newVis = new PartialSphere(0, 360, 60, 60, 30, 30, false);
            }
            else if (e.Key == Key.F4)
            {
                newVis = new InteractiveSphere();
            }
            else if (e.Key == Key.F5)
            {
                newVis = new InteractiveCylinder();
            }
            else if (e.Key == Key.F6)
            {
                newVis = new InteractiveCone();
            }

            // add it to the scene
            if (newVis != null)
            {
                Visual oldVisual;
                if (cachedNewVis == null) { oldVisual = reallyFakeIV3D.Visual; reallyFakeIV3D.Visual = null; }
                else { oldVisual = cachedNewVis.Visual; cachedNewVis.Visual = null; }

                newVis.Visual = oldVisual;
                
                if (cachedNewVis != null)
                {
                    MainViewportOuter.Children.Remove(cachedNewVis);
                }
                else
                {
                    MainViewportOuter.Children.Remove(reallyFakeIV3D);
                    oldCameraTransform = MainViewportOuter.Camera.Transform;
                    twoAxisValuator.Enabled = true;
                }

                MainViewportOuter.Children.Add(newVis);

                //transform
                if (newVis == reallyFakeIV3D)
                {
                    MainViewportOuter.Camera.Transform = oldCameraTransform;
                    twoAxisValuator.Enabled = false;
                }
                else
                {
                    Transform3DGroup tgroup = new Transform3DGroup();
                    tgroup.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 15)));
                    tgroup.Children.Add(new ScaleTransform3D(0.68, 0.68, 0.68));
                    twoAxisValuator.Up = new Vector3D(0, Math.Cos(15 * Math.PI / 180), Math.Sin(15 * Math.PI / 180));
                    newVis.Transform = tgroup;
                }

                if (newVis == reallyFakeIV3D)
                {
                    cachedNewVis = null;
                }
                else
                {
                    cachedNewVis = newVis;
                }

                e.Handled = true;
            }
        }

        //////////////////////////////////////////
        // Private Data
        //////////////////////////////////////////
        private string frob;
        private AuthorizedFlickrUser user = null;

        private PhotoCarouselView cv = null;
        private PhotoStack3D ps = null;

        private Point mapVisPreviousPoint;
        private InteractiveVisual3D blogVisual3D = null;

        private QuaternionRotation3D _myQuaternionRotLong;
        private QuaternionRotation3D _myQuaternionRotLat;

        private Transform3DGroup earthTransform;
        private InteractiveSphere earth;
        private MapVisual mapVisual;
        private GeoLocationPushpin earthLocationVisual = null;

        private bool earthButtonClicked = false;        

        private Transform3D oldCameraTransform = null;
        private InteractiveVisual3D cachedNewVis = null;

        private Dictionary<FlickrPhoto, GeoLocationPushpin> _photoToPushpin = new Dictionary<FlickrPhoto, GeoLocationPushpin>();        
    }
}