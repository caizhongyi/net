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
using System.Windows.Media.Media3D;

using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PhotoBrowser.FlickrApi;
using _3DTools;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.IO;

namespace PhotoBrowser.CarouselView
{
    public class PhotoCarouselView : CarouselView
    {
        private const double PLANE_WIDTH = 1.0;
        private const double PLANE_HEIGHT = 1.0;

        private MeshGeometry3D meshGeometry;

        public PhotoCarouselView() : base()
        {
            meshGeometry = CreateGeometry();
            _bitmapToGeometry = new Dictionary<BitmapImage, CarouselInteractiveVisual3D>();         
        }

        protected virtual MeshGeometry3D CreateGeometry()
        {
            MeshGeometry3D geometry = new MeshGeometry3D();

            // Create a collection of vertex positions for the MeshGeometry3D. 
            Point3DCollection planeVertices = new Point3DCollection();
            planeVertices.Add(new Point3D(-PLANE_WIDTH / 2, -PLANE_HEIGHT / 2, 0));
            planeVertices.Add(new Point3D(-PLANE_WIDTH / 2, PLANE_HEIGHT / 2, 0));
            planeVertices.Add(new Point3D(PLANE_WIDTH / 2, PLANE_HEIGHT / 2, 0));
            planeVertices.Add(new Point3D(PLANE_WIDTH / 2, -PLANE_HEIGHT / 2, 0));
            geometry.Positions = planeVertices;

            // Create a collection of triangle indices for the MeshGeometry3D.
            Int32Collection planeTriangleIndices = new Int32Collection();
            // first face of plane
            planeTriangleIndices.Add(0);
            planeTriangleIndices.Add(2);
            planeTriangleIndices.Add(1);

            // second face of plane
            planeTriangleIndices.Add(0);
            planeTriangleIndices.Add(3);
            planeTriangleIndices.Add(2);
            geometry.TriangleIndices = planeTriangleIndices;

            // Create the texture coordinates
            PointCollection texCoords = new PointCollection();
            texCoords.Add(new Point(0, 1));
            texCoords.Add(new Point(0, 0));
            texCoords.Add(new Point(1, 0));
            texCoords.Add(new Point(1, 1));
            geometry.TextureCoordinates = texCoords;

            // Apply the mesh to the geometry model.
            geometry.Freeze();

            return geometry;
        }

        private class CarouselInteractiveVisual3D : InteractiveVisual3D
        {
            public CarouselInteractiveVisual3D(PhotoCarouselView containingPLV, int row, int col, int index)
            {
                Row = row;
                Column = col;
                Index = index;
                ContainingPLV = containingPLV;
            }

            private PhotoCarouselView _cointainingPLV;
            public PhotoCarouselView ContainingPLV
            {
                get { return _cointainingPLV; }
                set { _cointainingPLV = value; }
            }
	

            private int _row;
            public int Row
            {
                get { return _row; }
                set { _row = value; }
            }

            private int _col;
            public int Column
            {
                get { return _col; }
                set { _col = value; }
            }

            private int _index;
            public int Index
            {
                get { return _index; }
                set { _index = value; }
            }
	

            private FlickrPhoto _photo;
            public FlickrPhoto Photo
            {
                get { return _photo; }
                set { _photo = value; }
            }            
        }

        protected override void RequestVisual3D(int index, int row, int col)
        {
            // create the visual3D to return
            CarouselInteractiveVisual3D visual3D = new CarouselInteractiveVisual3D(this, row, col, index);            
            visual3D.Geometry = meshGeometry;
            
            // make an asynch call to flickr to get the photo information we're interested in
            Flickr.AsynchGetPhoto(Data, index, visual3D.Dispatcher,
                                  delegate(object o)
                                  {
                                      PhotoReadyHandler((FlickrPhoto)o, visual3D);
                                  });            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="col"></param>
        /// <param name="start"></param>
        /// <param name="length"></param>
        protected override void RequestDataRange(int start, int end)
        {
            Flickr.AsynchLoadPhotoRange(start, end, Data, Dispatcher,
            delegate(object o)
            {
                DataRangeReady(start, end);
            });
        }

        /// <summary>
        /// Called once Flickr has the photo metadata available for us
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="o"></param>
        private void PhotoReadyHandler(FlickrPhoto photo, CarouselInteractiveVisual3D visual3D)
        {
            if (photo != null  && RequestStillValid(visual3D.Row, visual3D.Column, visual3D.Index))
            {
                visual3D.Photo = photo;

                BitmapImage b = new BitmapImage(new Uri(photo.URL_Small));
                _bitmapToGeometry[b] = visual3D;

                if (b.IsDownloading)
                {
                    b.DownloadCompleted += new EventHandler(ImageLoadCompleted);
                }
                else
                {
                    ImageLoadCompleted(b, null);
                }
            }
        }

        /// <summary>
        /// Lets us go from the bitmap image in question to the geometry that uses it
        /// </summary>
        private Dictionary<BitmapImage, CarouselInteractiveVisual3D> _bitmapToGeometry;

        /// <summary>
        /// Called once the image is competely loaded - at this point we hook everything up
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ImageLoadCompleted(object sender, EventArgs e)
        {
            BitmapImage b = (BitmapImage)sender;

            // get and then remove the bitmap to geometry lookup
            if (_bitmapToGeometry.ContainsKey(b))
            {
                CarouselInteractiveVisual3D interactiveMV3D = _bitmapToGeometry[b];
                _bitmapToGeometry.Remove(b);

                interactiveMV3D.Photo.SmallImage = b;
                System.Windows.Controls.Image i = new System.Windows.Controls.Image();
                i.Source = b;

                i.MouseLeftButtonDown += new MouseButtonEventHandler(
                                delegate(object o, MouseButtonEventArgs mouseEventArgs)
                                {
                                    OnItemClickedOn(interactiveMV3D.Photo);
                                    mouseEventArgs.Handled = true;
                                });
                interactiveMV3D.Visual = i;

                Visual3DReady(interactiveMV3D.Row, 
                              interactiveMV3D.Column,
                              interactiveMV3D.Index,
                              interactiveMV3D);
            }
        }

        protected override ModelVisual3D RequestTemporaryVisual3D()
        {
            ModelVisual3D visual3D = new ModelVisual3D();
            GeometryModel3D geomModel = new GeometryModel3D();
            geomModel.Geometry = meshGeometry;

            LinearGradientBrush myLinearGradientBrush = new LinearGradientBrush();
            myLinearGradientBrush.StartPoint = new Point(0, 0);
            myLinearGradientBrush.EndPoint = new Point(1, 1);
            myLinearGradientBrush.GradientStops.Add(new GradientStop(Colors.Blue, 0.0));
            myLinearGradientBrush.GradientStops.Add(new GradientStop(Colors.DarkBlue, 1.0));
            geomModel.Material = new DiffuseMaterial(myLinearGradientBrush);

            visual3D.Content = geomModel;

            return visual3D;
        }

        public delegate void GeoEventHandler(FlickrPhoto photo, Point latLong);

        public event GeoEventHandler GeoLocationAvailable;
        public event GeoEventHandler GeoLocationHidden;

        protected override void VisualVisible(Visual3D v)
        {
            base.VisualVisible(v);

            CarouselInteractiveVisual3D plv = (CarouselInteractiveVisual3D)v;

            if (plv.Photo.Info != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(new StringReader(plv.Photo.Info));

                XmlNode node = doc.SelectSingleNode("/photo/location");

                if (node != null)
                {
                    GeoLocationAvailable(plv.Photo,
                                         new Point(Double.Parse(node.Attributes["latitude"].Value),
                                                   Double.Parse(node.Attributes["longitude"].Value)));
                }
            }
        }

        protected override void VisualHidden(Visual3D v)
        {
            base.VisualHidden(v);

            CarouselInteractiveVisual3D plv = (CarouselInteractiveVisual3D)v;

            if (plv.Photo.Info != null)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(new StringReader(plv.Photo.Info));

                XmlNode node = doc.SelectSingleNode("/photo/location");

                if (node != null)
                {
                    GeoLocationHidden(plv.Photo,
                                         new Point(Double.Parse(node.Attributes["latitude"].Value),
                                                   Double.Parse(node.Attributes["longitude"].Value)));
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override double GetVisualWidth()
        {
            return PLANE_WIDTH;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override double GetVisualHeight()
        {
            return PLANE_HEIGHT;
        }

        /// <summary>
        /// Gets the number of meshes that are used in this view
        /// </summary>
        /// <returns></returns>
        protected override int GetNumMeshes()
        {
            return Data.Count;
        }

        protected override void ResetData()
        {
            base.ResetData();

            _bitmapToGeometry.Clear();
        }

        /// <summary>
        /// The underlying data that is being displayed
        /// </summary>
        private FlickrPhotos _data;
        public FlickrPhotos Data
        {
            get { return _data; }
            set
            {
                _data = value;

                Generate();
            }
        }
    }
}
