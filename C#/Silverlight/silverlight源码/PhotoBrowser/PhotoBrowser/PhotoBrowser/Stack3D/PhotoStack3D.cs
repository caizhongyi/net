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
using PhotoBrowser.FlickrApi;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Xml;
using System.IO;
using System.Windows.Markup;
using System.Windows.Data;
using _3DTools;

namespace PhotoBrowser.Stack3D
{
    public class PhotoStack3D : Stack3D
    {
        public PhotoStack3D()
            : base()
        {
            meshGeometry = CreateGeometry(ANGULAR_SWEEP);     
            photos = new List<FlickrPhoto>();
        }
        
        protected virtual MeshGeometry3D CreateGeometry(double angularSweep)
        {
            // The geometry specifes the shape of the 3D plane. In this sample, a flat sheet 
            // is created.
            MeshGeometry3D geometry = new MeshGeometry3D();

            // get the radius for use in point calculations
            double sweepInRadians = (angularSweep / 2.0) * Math.PI / 180;
            double radius = MESH_WIDTH / ( 2 * sweepInRadians);

            _zspacing = radius - radius * Math.Cos(sweepInRadians);
            _zspacing *= -2 * Math.Max(Width / MESH_WIDTH, Height / MESH_HEIGHT);
                
            // create a collection of vertex positions for the MeshGeometry3D
            Point3DCollection planeVertices = new Point3DCollection();
            PointCollection texCoords = new PointCollection();            
            // front face
            for (int i = 0; i <= VERTICAL_SLICES; i++)
            {
                double currRad = -sweepInRadians + i * (2 * sweepInRadians / (VERTICAL_SLICES));
                double z = radius * Math.Cos(currRad) - radius;
                double x = radius * Math.Sin(currRad) + MESH_WIDTH / 2;

                for (int j = 0; j <= HORIZONTAL_SLICES; j++)
                {
                    planeVertices.Add(new Point3D(x,
                                                  j * -MESH_HEIGHT / (HORIZONTAL_SLICES),
                                                  z));
                    texCoords.Add(new Point(i * 0.5 / VERTICAL_SLICES, j * 1.0 / HORIZONTAL_SLICES));
                }
            }

            // back face
            for (int i = 0; i <= VERTICAL_SLICES; i++)
            {
                double currRad = -sweepInRadians + i * (2 * sweepInRadians / (VERTICAL_SLICES));
                double z = radius * Math.Cos(currRad) - radius;
                double x = radius * Math.Sin(currRad) + MESH_WIDTH / 2;

                for (int j = 0; j <= HORIZONTAL_SLICES; j++)
                {
                    planeVertices.Add(new Point3D(x,
                                                  j * -MESH_HEIGHT / (HORIZONTAL_SLICES),
                                                  z));
                    texCoords.Add(new Point(1.0 - i * 0.5 / VERTICAL_SLICES, j * 1.0 / HORIZONTAL_SLICES));
                }
            }
            
            geometry.Positions = planeVertices;
            geometry.TextureCoordinates = texCoords;

            // Create a collection of triangle indices for the MeshGeometry3D.
            Int32Collection planeTriangleIndices = new Int32Collection();            
            for (int i = 0; i < VERTICAL_SLICES; i++)
            {
                for (int j = 0; j < HORIZONTAL_SLICES; j++)
                {
                    int index1 = i * (HORIZONTAL_SLICES + 1) + j;
                    int index2 = i * (HORIZONTAL_SLICES + 1) + j + 1;
                    int index3 = (i + 1) * (HORIZONTAL_SLICES + 1) + j;
                    int index4 = (i + 1) * (HORIZONTAL_SLICES + 1) + j + 1;

                    // front face
                    planeTriangleIndices.Add(index1);
                    planeTriangleIndices.Add(index2);
                    planeTriangleIndices.Add(index3);

                    planeTriangleIndices.Add(index2);
                    planeTriangleIndices.Add(index4);
                    planeTriangleIndices.Add(index3);

                    // back face
                    int offset = (HORIZONTAL_SLICES + 1) * (VERTICAL_SLICES + 1);
                    planeTriangleIndices.Add(index2 + offset);
                    planeTriangleIndices.Add(index1 + offset);
                    planeTriangleIndices.Add(index3 + offset);

                    planeTriangleIndices.Add(index4 + offset);
                    planeTriangleIndices.Add(index2 + offset);
                    planeTriangleIndices.Add(index3 + offset);
                }
            }
            geometry.TriangleIndices = planeTriangleIndices;            

            // Apply the mesh to the geometry model.
            geometry.Freeze();

            return geometry;
        }

        public override void AddItem(object o)
        {
            if (!photos.Contains((FlickrPhoto)o))
            {
                photos.Insert(0, (FlickrPhoto)o);

                base.AddItem(o);
            }
            else
            {
                MoveToFront(o);
            }
        }

        public override void RemoveItem(object o)
        {
            base.RemoveItem(o);
            photos.Remove((FlickrPhoto)o);
        }

        public delegate void GeoLocationSelectedDelegate(double longitude, double latitude, FlickrPhoto photo);
        public event GeoLocationSelectedDelegate GeoLocationSelected;

        public delegate void BlogRequestedDelegate(FlickrPhoto photo);
        public event BlogRequestedDelegate BlogRequested;

        protected override InteractiveVisual3D GetVisual3DRepresentation(object o)
        {
            FlickrPhoto photo = (FlickrPhoto)o;
          
            // create the visual3D to return
            PhotoStackViewVisual3D visual3D = new PhotoStackViewVisual3D(photo);
            
            // set up the material
            MaterialGroup material = new MaterialGroup();
            DiffuseMaterial interactiveMaterial = new DiffuseMaterial();
            interactiveMaterial.SetValue(InteractiveVisual3D.IsInteractiveMaterialProperty, true);
            material.Children.Add(interactiveMaterial);
            material.Children.Add(new SpecularMaterial(Brushes.White, 40));
            visual3D.Material = material;

            visual3D.Geometry = meshGeometry;            
            PictureComment xamlRep = GetXamlRepresentation();
            visual3D.Visual = xamlRep;

            xamlRep.pictureVisual.curvatureSlider.ValueChanged +=
                new RoutedPropertyChangedEventHandler<double>(delegate(object sender, RoutedPropertyChangedEventArgs<double> e)
                                                              {
                                                                  visual3D.Geometry = CreateGeometry(e.NewValue);
                                                              });

            xamlRep.pictureVisual.closeButton.Click += new RoutedEventHandler(
                                                     delegate(object sender, RoutedEventArgs e)
                                                     {
                                                         RemoveItem(photo);
                                                     });

            xamlRep.pictureVisual.geoButton.Click += new RoutedEventHandler(
                                                     delegate(object sender, RoutedEventArgs e)
                                                     {
                                                         XmlDocument doc = new XmlDocument();
                                                         doc.Load(new StringReader(photo.Info));

                                                         XmlNode node = doc.SelectSingleNode("/photo/location");

                                                         GeoLocationSelected(Double.Parse(node.Attributes["longitude"].Value), 
                                                                             Double.Parse(node.Attributes["latitude"].Value),
                                                                             photo);
                                                     });

            xamlRep.pictureVisual.blogButton.Click += new RoutedEventHandler(
                                                      delegate(object sender, RoutedEventArgs e)
                                                      {
                                                          BlogRequested(photo);
                                                      });

           
            // if there is an authorized user - set it up so they can comment on photos
            if (Flickr.CurrAuthorizedUser != null)
            {
                xamlRep.submitCommentButton.IsEnabled = true;
                xamlRep.submitCommentButton.Click += new RoutedEventHandler(
                        delegate(object sender, RoutedEventArgs e)
                        {
                            Flickr.AsynchPostComments(xamlRep.textBox1.Text,
                                                      photo,
                                                      Flickr.CurrAuthorizedUser,
                                                      Dispatcher,
                                                      delegate(object result)
                                                      {
                                                          if ((bool)result)
                                                          {
                                                              Flickr.AsynchGetPhotoComments(photo,
                                                                                            visual3D.Dispatcher,
                                                                                            delegate(object resultData)
                                                                                            {
                                                                                                PhotoCommentsReceived(xamlRep, (string)resultData);
                                                                                            });
                                                          }
                                                      });
                        });
            }
                                                          
            // make an asynch call to flickr to get the photo information we're interested in            
            if (photo != null)
            {
                BitmapImage b = new BitmapImage(new Uri(photo.URL_Medium));
                
                if (b.IsDownloading)
                {
                    // use the small image until the large is ready
                    if (photo.SmallImage != null)
                    {
                        xamlRep.UpdateImage(photo.SmallImage);
                    }

                    b.DownloadCompleted += delegate(object sender, EventArgs e)
                                           {
                                               xamlRep.UpdateImage(b);
                                               ScaleVisual3D(visual3D, xamlRep.Width / (2 * xamlRep.Height));
                                           };                    
                }
                else
                {
                    xamlRep.UpdateImage(b);
                }
            }

            // request all sort of information about the photo
            Flickr.AsynchGetPhotoComments(photo,
                                          visual3D.Dispatcher,
                                          delegate(object resultData)
                                          {
                                              PhotoCommentsReceived(xamlRep, (string)resultData);
                                          });


            if (photo.Info == null)
            {
                Flickr.AsynchGetPhotoInfo(photo,
                                         visual3D.Dispatcher,
                                         delegate(object resultData)
                                         {
                                             PhotoInfoReceived(xamlRep, (string)resultData);
                                         });
            }
            else
            {
                PhotoInfoReceived(xamlRep, photo.Info);
            }

            ScaleVisual3D(visual3D, xamlRep.Width / (2 *  xamlRep.Height));
            // return the visual w/o the bitmap yet
            return visual3D;
        }

        private void PhotoCommentsReceived(PictureComment xamlRep, string comments)
        {
            // bind to the xml data
            xamlRep.commentList.SetComments(comments);
        }

        private void PhotoInfoReceived(PictureComment xamlRep, string info)
        {
            // create the xml document so we can pull from it what we want
            XmlDocument doc = new XmlDocument();
            doc.Load(new StringReader(info));

            // depending on the photo info - display the different buttons
            // Location information
            XmlNode node = doc.SelectSingleNode("/photo/location");
            if (node != null)
            {
                xamlRep.pictureVisual.EnableLocation();
            }
            else
            {
                xamlRep.pictureVisual.DisableLocation();
            }

            // Comment information
            XmlNode commentNode = doc.SelectSingleNode("/photo/comments");
            if (commentNode != null && Int32.Parse(commentNode.InnerText) > 0)
            {
                xamlRep.pictureVisual.EnableComments();
            }
            else
            {
                xamlRep.pictureVisual.DisableComments();
            }            
        }

        private void ScaleVisual3D(PhotoStackViewVisual3D visual3D, double aspectRatio)
        {
            double scaleX, scaleY;

            // adjust scale on mesh
            scaleY = 1;
            scaleX = MESH_HEIGHT * aspectRatio;
        
            // fit within requested box as well
            double scaleFactor = 1;
            if (aspectRatio > 1)
            {
                scaleFactor = Width / (MESH_WIDTH * scaleX);
            }
            else
            {
                scaleFactor = Height / (MESH_HEIGHT * scaleY);
            }

            scaleX *= scaleFactor;
            scaleY *= scaleFactor;

            visual3D.Content.Transform = new ScaleTransform3D(scaleX, scaleY, scaleX);
        }

        private PictureComment GetXamlRepresentation()
        {
            return new PictureComment();
        }

        protected override int GetIndex(object o)
        {
            return photos.IndexOf((FlickrPhoto)o);
        }

        public override void MoveToFront(object o)
        {
            base.MoveToFront(o);

            int index = photos.IndexOf((FlickrPhoto)o);
            photos.RemoveAt(index);
            photos.Insert(0, (FlickrPhoto)o);
        }

        protected override int Count
        {
            get 
            {
                return photos.Count;
            }
        }

        private double _zspacing;
        protected override double ZSpacing
        {
            get
            {
                return _zspacing;
            }
        }

        /// <summary>
        /// The PhotoStackViewVisual3D represents an InteractiveVisual3D
        /// </summary>
        private class PhotoStackViewVisual3D : InteractiveVisual3D
        {
            public PhotoStackViewVisual3D(FlickrPhoto photo)
            {
                _photo = photo;
            }

            private FlickrPhoto _photo;
            public FlickrPhoto Photo
            {
                get { return _photo; }
                set { _photo = value; }
            }
        }

        private double MESH_WIDTH = 1.0;
        private double MESH_HEIGHT = 1.0;

        private int VERTICAL_SLICES = 6;
        private int HORIZONTAL_SLICES = 2;
        private double ANGULAR_SWEEP = 45;

        /// <summary>
        /// The photos that we are displaying
        /// </summary>
        private List<FlickrPhoto> photos;

        /// <summary>
        /// The geometry used to represent a photo
        /// </summary>
        private MeshGeometry3D meshGeometry;        
    }
}
