// Deep Earth is a community project available under the Microsoft Public License (Ms-PL)
// Code is provided as is and with no warrenty – Use at your own risk
// View the project and the latest code at http://codeplex.com/deepearth/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using DeepEarth.Events;
using DeepEarth.Layers;

namespace DeepEarth.Geometry
{

    /// <summary>
    /// <para>
    /// An ILayer instance which acts as the controller for a set of child geometry items
    /// </para>
    /// <example>
    /// 
    /// <code title="Add a GeometryLayer with TransformUpdate">
    ///    Map map = new Map();
    ///    map.BaseLayer.Source = new VeTileSource(VeMapModes.VeHybrid);
    ///    GeometryLayer transformLayer = new GeometryLayer(map) { UpdateMode = GeometryLayer.UpdateModes.TransformUpdate };
    ///    transformLayer.Opacity = 0.5;
    ///    map.Layers.Add(transformLayer);
    ///    Polygon polygon = new Polygon();
    ///    transformLayer.Add(polygon);
    ///    polygon.Points = new ObservableCollection&lt;Point&gt; { new Point(-80.195, 25.775), new Point(-64.75, 32.303), new Point(-66.073, 18.44) };
    ///    polygon.FillColor = Color.FromArgb(0x7F, 0xFF, 0x00, 0x00);
    ///    System.Windows.Controls.ToolTipService.SetToolTip(polygon, "Bermuda Triangle");
    /// </code>
    /// </example>
    /// </summary>
    public class GeometryLayer : Layer, ICollection<GeometryBase>
    {
        private const double UpdatesPerSecond = 0;
        private static int TopZ = 1000;
        private static int LowZ = -1000;

        private readonly Dictionary<Guid, GeometryBase> _Shapes;
        
        protected bool _IsReadOnly;

        /// <summary>
        /// Sets or gets the Sytle property to be applied to all geometry items in the layer
        /// </summary>
        public Style ItemsStyle { get; set; }

        ///<summary>
        /// Enumeration of the currently supported update modes for plotting geometries on the canvas.
        ///</summary>
        public enum UpdateModes
        {
            ///<summary>
            /// Iterates through all GeometryBase objects on the GeometryLayer Canvas and performs 
            /// and explicit update of each Geographic Point to a Pixel.  
            /// Best option for lots of Points because no scaling is involved and you get updates during zooms.
            ///</summary>
            ElementUpdate,
            ///<summary>
            /// Offsets the Canvas in synch with teh the map as the viewport changes and hides the shapes during zooms.  
            /// Shapes locations are only updated on the map when zoom is complete.
            ///</summary>
            PanOnlyUpdate,
            ///<summary>
            /// Pans and scales the GeometryLayer Canvas in synch with the full map extent.  
            /// It sets the items on the Canvas once and the scaling and offsetting of the Canvas do the rest. 
            ///</summary>
            TransformUpdate
        }
        private UpdateModes _UpdateType = UpdateModes.ElementUpdate;

        /// <summary>
        /// Represents the technique used to update the location of child geometry items.
        /// </summary>
        public UpdateModes UpdateMode
        {
            get { return _UpdateType; }
            set 
            { 
                _UpdateType = value;

                switch(UpdateMode)
                {
                    case UpdateModes.PanOnlyUpdate:
                    {
                        InitLayerTransforms();
                        UpdateTranslateTransform();
                        break;
                    }
                    case UpdateModes.TransformUpdate:
                    {
                        InitLayerTransforms();
                        UpdateLayerTranforms();
                        break;
                    }
                }
            }
        }

        ///<summary>
        /// Primary constructor for the layer, requires a map instance.
        ///</summary>
        ///<param name="map"></param>
        public GeometryLayer(Map map)
        {
            _Map = map;
            _Shapes = new Dictionary<Guid, GeometryBase>();


            _Map.Events.MapViewChanged += ViewChanged;
            _Map.Events.MapZoomStarted += ZoomStarted;
            _Map.Events.MapZoomEnded += ZoomEnded;
            _Map.Events.MapLoaded += MapLoaded;

            if (sbAnimation == null)
            {
                sbAnimation = new Storyboard { Duration = new Duration(TimeSpan.FromSeconds(UpdatesPerSecond)) };
                sbAnimation.Completed += sbAnimation_Completed;
            }

        }

        /// <summary>
        /// Since geometry items may have been added before map wad loaded, this event handler will
        /// iterate its children and assure all geometries are loaded onto the Canvas
        /// </summary>
        /// <param name="map">Map Instance</param>
        /// <param name="args">Map Event Args</param>
        private void MapLoaded(Map map, MapEventArgs args)
        {
            //Make sure that all shapes are on the canvas not that the map is ready
            UpdateView();
            foreach (GeometryBase shape in _Shapes.Values)
            {
                if (Children.Contains(shape) == false)
                {
                    Children.Add(shape);
                    UpdateChildLocation(shape);
                }
            }
        }


        /// <summary>
        /// Indexer for accessing child geometry items by index location.
        /// </summary>
        /// <param name="id">Unique identifier for the shape</param>
        public GeometryBase this[Guid id]
        {
            get
            {
                foreach (GeometryBase shape in _Shapes.Values)
                {
                    if (shape.ID == id) return shape;
                }
                return null;
            }
        }

        /// <summary>
        /// The enumerable collection of geometries for the GeometryLayer
        /// </summary>
        public IEnumerable<GeometryBase> Geometry
        {
            get
            {
                foreach (GeometryBase shape in _Shapes.Values)
                {
                    yield return shape;
                }
            }
        }

        /// <summary>
        /// The enumerable collection of GeometryBase objects that are within the geographic bounds of the Map
        /// </summary>
        public IEnumerable<GeometryBase> GeometryInView
        {
            get
            {
                Rect bounds = _Map.GeoBounds;
                foreach (GeometryBase shape in _Shapes.Values)
                {
                    if (shape.Intersects(bounds)) yield return shape;
                }
            }
        }

        /// <summary>
        /// Get the specified shape by its ID
        /// </summary>
        /// <param name="id">The GUID ID of the shape</param>
        /// <returns>GeometryBase object representing the shape, null if nothing is found</returns>
        public GeometryBase GetShapeByID(Guid id)
        {
            GeometryBase shape;
            _Shapes.TryGetValue(id, out shape);
            return shape;
        }

        /// <summary>
        /// Move the specified geometry to the top (max Z index) of the canvas
        /// </summary>
        /// <param name="geometry">The geometry to move</param>
        public void SendToTop(GeometryBase geometry)
        {

            SetZIndex(geometry, TopZ++);
        }

        /// <summary>
        /// Move the specified geometry to the bottom (min Z index) of the canvas
        /// </summary>
        /// <param name="geometry">The geometry to move</param>
        public void SendToBack(GeometryBase geometry)
        {
            SetZIndex(geometry, LowZ--);
        }

        /// <summary>
        /// Handles the visibility change event
        /// </summary>
        protected override void OnIsVisibleChanged()
        {
            base.OnIsVisibleChanged();
            UpdateAllShapes();
        }


        #region ICollection<GeometryBase> Members

        public IEnumerator<GeometryBase> GetEnumerator()
        {
            foreach (GeometryBase shape in _Shapes.Values)
            {
                yield return shape;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)new GeometryLayer(_Map);
        }

        public virtual void Add(GeometryBase shape)
        {
            shape.Layer = this;
            _Shapes.Add(shape.ID, shape);

            //Apply a System.Windows.Style if one is set on the layer
            if (ItemsStyle != null) shape.Style = ItemsStyle;

            if (MapInstance.IsMapLoaded)
            {
                Children.Add(shape);

                //If we are in TransformMode update the shape whether it is in view or not.
                switch (UpdateMode)
                {
                    case UpdateModes.PanOnlyUpdate:
                    case UpdateModes.TransformUpdate:
                        {
                            UpdateChildLocation(shape);
                            break;
                        }
                    case UpdateModes.ElementUpdate:
                        {
                            UpdateShapeView(shape, _Map.GeoBounds);
                            break;
                        }
                }
            }

        }

        public virtual bool Remove(GeometryBase shape)
        {
            bool result = false;
            if (_Shapes.ContainsValue(shape))
            {
                result = _Shapes.Remove(shape.ID);
                RemoveShape(shape);
            }

            return result;
        }


        public void Clear()
        {
            GeometryBase[] shapes = new GeometryBase[_Shapes.Count];
            CopyTo(shapes, 0);

            foreach (GeometryBase shape in shapes)
            {
                RemoveShape(shape);
            }

            _Shapes.Clear();
        }

        private void RemoveShape(GeometryBase shape)
        {
            _Shapes.Remove(shape.ID);
            if (Children.Contains(shape)) Children.Remove(shape);
        }

        public bool Contains(GeometryBase shape)
        {
            return _Shapes.ContainsValue(shape);
        }

        public void CopyTo(GeometryBase[] shapes, int index)
        {
            _Shapes.Values.CopyTo(shapes, index);
        }

        public virtual int Count
        {
            get { return _Shapes.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return _IsReadOnly; }
        }

        #endregion


        #region Animation Update Loop Operations

        private const double ChangeSignificanceThreshold = 0.05;
        private bool _InAnimation;
        private bool _InMotion;
        private Point _PriorOrigin;


        // make the animation last 1500 ms, which is the length of the animation in Deep Zoom
        private TimeSpan animationDuration = new TimeSpan(0, 0, 0, 0, 1500);
        private Storyboard sbAnimation;
        private DateTime _UpdateStartTime;


        private void ViewChanged(Map map, MapEventArgs args)
        {
            UpdateView();
        }

        private void UpdateView()
        {
            switch (UpdateMode)
            {
                case UpdateModes.PanOnlyUpdate:
                    UpdateTranslateTransform();
                    break;

                case UpdateModes.TransformUpdate:
                    UpdateLayerTranforms();
                    break;

                case UpdateModes.ElementUpdate:
                    _InMotion = true;

                    //Ignore if we are already in process of an update.
                    if (_InAnimation == false)
                    {
                        _UpdateStartTime = DateTime.Now;
                        if (sbAnimation == null)
                        {
                            sbAnimation = new Storyboard { Duration = new Duration(TimeSpan.FromSeconds(UpdatesPerSecond)) };
                            sbAnimation.Completed += sbAnimation_Completed;
                        }
                        sbAnimation.Begin();
                    }
                    break;
            }
        }

        private void sbAnimation_Completed(object sender, EventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(string.Format("UpdateCounter: {0}", counter++));

            double progress = (DateTime.Now - _UpdateStartTime).TotalSeconds/animationDuration.TotalSeconds;
            if (progress >= 1)
            {
                //Clean Up. Final UpdateShapes moved to MotionComplete event handler
                sbAnimation = null;
                _InAnimation = false;
            }
            else
            {
                //Uses the sum of squares to determine distance moved.
                //The woble effect seeems to occur at ~ a .03% of the logical display.  
                //So seeting the change tolerance at .05 eliminates much of the woble effect especially post zoom.
                double distLogChange = Math.Sqrt(Math.Pow(_Map.LogicalOrigin.X - _PriorOrigin.X, 2) + Math.Pow(_Map.LogicalOrigin.Y - _PriorOrigin.Y, 2));
                double changeSignificance = 100*distLogChange/_Map.MapViewLogicalSize.Width;
                if (changeSignificance > ChangeSignificanceThreshold)
                {
                    _PriorOrigin = _Map.LogicalOrigin;
                    UpdateAllShapes();
                }

                //Provide a short circuit if MSI.MotionEnded has been called.
                if (_InMotion)
                {
                    //Loop until time is up.
                    _InAnimation = true;
                    sbAnimation.Begin();
                }
                else
                {
                    sbAnimation = null;
                    _InAnimation = false;
                }
            }
        }

        private bool _InPanOnlyZoom = false;
        private Visibility _PriorToZoomVisibility = Visibility.Visible;
        private void ZoomStarted(Map map, MapEventArgs args)
        {
            if (UpdateMode == UpdateModes.PanOnlyUpdate)
            {
                if (_InPanOnlyZoom == false)
                {
                    _InPanOnlyZoom = true;
                    _PriorToZoomVisibility = Visibility;
                    Visibility = Visibility.Collapsed;
                }
            }
        }

        private void ZoomEnded(Map map, MapEventArgs args)
        {
            _InMotion = false;

            switch (UpdateMode)
            {
                case UpdateModes.ElementUpdate:
                {
                    UpdateAllShapes();
                    break;
                }
                case UpdateModes.PanOnlyUpdate:
                {
                    _InPanOnlyZoom = false;
                    Visibility = _PriorToZoomVisibility;
                    UpdateAllShapes();
                    UpdateTranslateTransform();
                    break;
                }
                case UpdateModes.TransformUpdate:
                {
                    //No update needed
                    break;
                }
            }

        }

        #endregion


        #region Canvas Transformation Operations

        private void InitLayerTransforms()
        {
            TransformGroup transformGroup = new TransformGroup();
            TranslateTransform translateTransform = new TranslateTransform { X = 1, Y = 1 };
            ScaleTransform scaleTransform = new ScaleTransform{ ScaleX = 1.0, ScaleY = 1.0 };

            switch (UpdateMode)
            {
                case UpdateModes.TransformUpdate:
                {
                    //This Size is arbitrary, but we'll set it to Max Map Size as this becomes the basis for scaling child elements.
                    Width = 67108864;
                    Height = 67108864;
                    break;
                }
            }

            transformGroup.Children.Add(scaleTransform);
            transformGroup.Children.Add(translateTransform);
            RenderTransform = transformGroup;   
        }

        private void UpdateLayerTranforms()
        {
            if (RenderTransform != null)
            {
                UpdateScaleTransform();
                UpdateTranslateTransform();
            }

        }

        private void UpdateTranslateTransform()
        {
            if (RenderTransform != null)
            {
                Point offset = ViewOffset;
                TranslateTransform translate = (TranslateTransform)((TransformGroup)RenderTransform).Children[1];
                translate.X = -offset.X;
                translate.Y = -offset.Y;
            }
        }

        private void UpdateScaleTransform()
        {
            if (RenderTransform != null)
            {
                Point scaleFactor = MapExtentToFullSizeScale;
                ScaleTransform scale = (ScaleTransform)((TransformGroup)RenderTransform).Children[0];
                scale.ScaleX = scaleFactor.X;
                scale.ScaleY = scaleFactor.Y;

                //Update scale of children on canvas
                double scaleAdjustment = 1 / scaleFactor.X;
                foreach (UIElement element in Children)
                {
                    if (element is GeometryBase) 
                    {
                        GeometryBase geometry = element as GeometryBase;
                        geometry.ScaleAdjustment = scaleAdjustment;
                    }
                }
            }
        }

        private void UpdateScale(GeometryBase geometry)
        {
            if (UpdateMode == UpdateModes.TransformUpdate)
            {
                if (MapInstance.IsMapLoaded)
                {
                    Point scaleFactor = MapExtentToFullSizeScale;
                    double scaleAdjust = 1 / scaleFactor.X;
                    geometry.ScaleAdjustment = scaleAdjust;
                }
            }
        }

        internal Point MapExtentToFullSizeScale
        {
            get
            {
                Size mapExtent = _Map.MapExtentPixelSize;
                double xScale = mapExtent.Width / Width;
                double yScale = mapExtent.Height / Height;
                return new Point(xScale, yScale);
            }
        }

        private Point ViewOffset
        {
            get
            {
                Point point;
                if (_Map.IsMapLoaded)
                {
                    switch (UpdateMode)
                    {
                        case UpdateModes.PanOnlyUpdate:
                        case UpdateModes.TransformUpdate:
                        {
                            Size mapExtent = _Map.MapExtentPixelSize;
                            point = new Point(_Map.LogicalOrigin.X * mapExtent.Width, _Map.LogicalOrigin.Y * mapExtent.Height);
                            break;
                        }
                        default:
                        {
                            point = new Point();
                            break;
                        }
                    }
                }
                else
                {
                    point = new Point();
                }
                return point;
            }
        }

        #endregion


        #region Canvas Update Operations

        /// <summary>
        /// Update the location of all child shapes.
        /// </summary>
        internal void UpdateAllShapes()
        {
            if (_Shapes.Count > 0)
            {
                Rect bounds = _Map.GeoBounds;
                var shapesCopy = new GeometryBase[_Shapes.Count];
                _Shapes.Values.CopyTo(shapesCopy, 0);

                foreach (GeometryBase geometry in shapesCopy)
                {
                    UpdateShapeView(geometry, bounds);
                }
            }
        }

        /// <summary>
        /// Update the location of a particular shape.
        /// </summary>
        /// <param name="shape"></param>
        internal void UpdateShape(GeometryBase shape)
        {
            UpdateShapeView(shape, _Map.GeoBounds);
        }

        /// <summary>
        /// Update the location of the shape if it is within the specified extent
        /// </summary>
        /// <param name="shape">The geometry to update</param>
        /// <param name="bounds">The extent to intersect</param>
        private void UpdateShapeView(GeometryBase shape, Rect bounds)
        {
            if (_Map.IsMapLoaded)
            {
                if (shape.Layer.IsVisible)
                {
                    if (shape.IsVisible)
                    {
                        if (shape.Intersects(bounds))
                        {
                            ShowShape(shape);
                        }
                        else
                        {
                            HideShape(shape);
                        }
                    }
                }
                else
                {
                    HideShape(shape);
                }
            }
        }

        ///<summary>
        /// Updates the location of the shape on the canvas with respect to configured update mode.
        ///</summary>
        ///<param name="shape">Shape which needs its canvas location updated.</param>
        public void UpdateChildLocation(GeometryBase shape)
        {
            if (shape.IsLoaded)
            {
                if (UpdateMode == UpdateModes.TransformUpdate) UpdateScale(shape);


                if (shape is PointBase)
                {
                    UpdatePoint((PointBase)shape);
                }

                if (shape is PathBase)
                {
                    UpdatePathData((PathBase)shape);
                }

            }
        }


        private void UpdatePoint(PointBase dp)
        {
            switch (UpdateMode)
            {
                case UpdateModes.PanOnlyUpdate:
                {
                    Size mapExtent = MapInstance.MapExtentPixelSize;
                    Point logicalPoint = dp.LogicalCoordinate;
                    Point pixel = new Point(logicalPoint.X * mapExtent.Width, logicalPoint.Y * mapExtent.Height);
                    SetLeft(dp, pixel.X - dp.Anchor.X);
                    SetTop(dp, pixel.Y - dp.Anchor.Y);
                    break;
                }
                case UpdateModes.ElementUpdate:
                {
                    Point pixel = Map.DefaultInstance.CoordHelper.LogicalToPixel(dp.LogicalCoordinate);
                    SetLeft(dp, pixel.X - dp.Anchor.X);
                    SetTop(dp, pixel.Y - dp.Anchor.Y);
                    break;
                }

                case UpdateModes.TransformUpdate:
                {
                    Size mapExtent = MapInstance.MapExtentPixelSize;
                    double xMaxToCurrRatio = Width / mapExtent.Width;
                    double yMaxToCurrRatio = Height / mapExtent.Height;
                    Point logicalPoint = dp.LogicalCoordinate;

                    Point pixel = new Point(logicalPoint.X * xMaxToCurrRatio * mapExtent.Width, logicalPoint.Y * yMaxToCurrRatio * mapExtent.Height);

                    SetLeft(dp, pixel.X - dp.Anchor.X);
                    SetTop(dp, pixel.Y - dp.Anchor.Y);

                    break;
                }
            }
        }

        private void UpdatePathData(PathBase pathGeometry)
        {
            var pathData = new PathGeometry();
            var pf = new PathFigure();
            int idx = 0;

            //Create an optimized calculation for the points in the loop.
            if (pathGeometry.Points != null)
            {
                Size mapExtent = MapInstance.MapExtentPixelSize;
                double xMaxToCurrRatio = Width / mapExtent.Width;
                double yMaxToCurrRatio = Height / mapExtent.Height;

                foreach (Point logicalPoint in pathGeometry.LogicalCoordinates)
                {
                    //Add pixels relative to Layer size.
                    Point pixel;
                    switch (UpdateMode)
                    {
                        case UpdateModes.TransformUpdate:
                        {
                            pixel = new Point(logicalPoint.X * xMaxToCurrRatio * mapExtent.Width, logicalPoint.Y * yMaxToCurrRatio * mapExtent.Height);
                            break;
                        }
                        case UpdateModes.PanOnlyUpdate:
                        {
                            pixel = new Point(logicalPoint.X * mapExtent.Width, logicalPoint.Y * mapExtent.Height);
                            break;
                        }
                        case UpdateModes.ElementUpdate:
                        {
                            pixel = MapInstance.CoordHelper.LogicalToPixel(logicalPoint);
                            break;
                        }
                        default:
                        {
                            pixel = new Point();
                            break;
                        }
                    }

                    if (idx == 0)
                    {
                        pf.StartPoint = pixel;
                    }
                    else
                    {
                        var ls = new LineSegment { Point = pixel };
                        pf.Segments.Add(ls);
                    }
                    idx++;
                }
            }
            pf.IsClosed = pathGeometry.IsClosed;
            pathData.Figures.Add(pf);

            pathGeometry.PathData = pathData;
        }

        protected static void HideShape(GeometryBase shape)
        {
            shape.InView = false;
            if (shape is PointBase)
            {
                SetLeft(shape, -1000);
                SetTop(shape, -1000);
            }

            if (shape is PathBase)
            {
                shape.Visibility = Visibility.Collapsed;
            }
        }

        protected void ShowShape(GeometryBase shape)
        {
            shape.InView = true;
            if (shape is PointBase)
            {
                UpdateChildLocation(shape);
            }

            if (shape is PathBase)
            {
                if (shape.IsVisible) shape.Visibility = Visibility.Visible;
                UpdateChildLocation(shape);
            }
        }

        #endregion

    }
}

