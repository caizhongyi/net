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
using System.Windows.Media.Media3D;
using System.Windows.Media.Animation;
using System.Windows.Media;
using System.Windows.Controls;

using PhotoBrowser.Shapes;
using _3DTools;

namespace PhotoBrowser.CarouselView
{
    public abstract class CarouselView : ModelVisual3D
    {
        /// <summary>
        /// Creates a new carousel view
        /// </summary>
        public CarouselView()
        {
            RowPadding = 0.01;
            ColumnPadding = 0.01;
            
            holderVisual = new ModelVisual3D();
            this.Children.Add(holderVisual);

            viewController = new PartialSphere(220, -80, 89, 2, 10, 10, false);
            
            slider = new Slider();
            slider.Width = 800;
            slider.Background = Brushes.Transparent;
            slider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(slider_ValueChanged);

            viewController.Visual = slider;
            viewController.Transform = new TranslateTransform3D(0, 
                                            0.5 * (finalMeshWidth * Rows + RowPadding * (Rows - 1)) + 3 * RowPadding, 0);

            slider.IsMouseCaptureWithinChanged += new DependencyPropertyChangedEventHandler(slider_IsMouseCaptureWithinChanged);
        }

        void slider_IsMouseCaptureWithinChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue == true)
            {
                double currSliderValue = slider.Value;
                slider.BeginAnimation(Slider.ValueProperty, null);
                slider.Value = currSliderValue;
            }
            else
            {
                // create the animation to go with this data
                currentAnim = new DoubleAnimation(slider.Value, slider.Maximum, new Duration(TimeSpan.FromSeconds((slider.Maximum - slider.Value) * 10)));
                slider.BeginAnimation(Slider.ValueProperty, currentAnim);
            }
        }


        /// <summary>
        /// Now need to move the word so that whatever is at this value is at the right spot
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {            
            DisplayFrontMost(e.NewValue);
        }

        private PartialSphere viewController;
        private Slider slider;

        private ModelVisual3D holderVisual = null;        
        private DoubleAnimation currentAnim = null;                

        #region ABSTRACT_METHODS

        /// <summary>
        /// Gets the number of meshes that are used in this view
        /// </summary>
        /// <returns></returns>
        protected abstract int GetNumMeshes();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract double GetVisualWidth();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected abstract double GetVisualHeight();

        protected abstract void RequestDataRange(int start, int length);

        protected abstract void RequestVisual3D(int i, int row, int col);

        protected abstract ModelVisual3D RequestTemporaryVisual3D();

        #endregion ABSTRACT_METHODS

        #region DEPENDENCY_PROPERTIES

        /// <summary>
        /// The number of rows composing the carousel view
        /// </summary>
        public static DependencyProperty RowsProperty =
            DependencyProperty.Register(
                "Rows",
                typeof(int),
                typeof(CarouselView),
                new PropertyMetadata(4, new PropertyChangedCallback(OnRowsChanged)));

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value); }
        }

        internal static void OnRowsChanged(Object sender, DependencyPropertyChangedEventArgs e)
        {
            CarouselView laundryView = ((CarouselView)sender);

            laundryView.Generate();
        }

        /// <summary>
        /// The number of columns composing the carousel view
        /// </summary>
        public static DependencyProperty ColumnsProperty =
            DependencyProperty.Register(
                "Columns",
                typeof(int),
                typeof(CarouselView),
                new PropertyMetadata(40, new PropertyChangedCallback(OnColumnsChanged)));

        public int Columns
        {
            get { return (int)GetValue(ColumnsProperty); }
            set { SetValue(ColumnsProperty, value); }
        }

        internal static void OnColumnsChanged(Object sender, DependencyPropertyChangedEventArgs e)
        {
            CarouselView laundryView = ((CarouselView)sender);

            laundryView.Generate();
        }

        #endregion DEPENDENCY_PROPERTIES

        // for use when the thing spins
        private class ColIndexMapping
        {
            public ColIndexMapping(int col, int index)
            {
                _col = col;
                _index = index;
            }

            public int _col;
            public int _index;
        }

        protected virtual void ResetData()
        {
            holderVisual.Children.Clear();
            
            slider.BeginAnimation(Slider.ValueProperty, null);            
            currentAnim = null;
            slider.Value = 0;

            _rowcolToItem = new CVItem[Rows, Columns];
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    _rowcolToItem[i, j] = new CVItem();

                    // insert placeholder
                    ModelVisual3D tempVisual3D = RequestTemporaryVisual3D();
                    Transform3D tempTransform = GetObjectTransform(i, j);
                    tempVisual3D.Transform = tempTransform;                    
                    _rowcolToItem[i, j]._placeholder = tempVisual3D;
                }
            }
        }

        protected void DisplayFrontMost(double frontColIndex)
        {            
            // column this item maps to and index in question
            int colIndexRequested = (int)frontColIndex;

            // populate data around the given index
            PopulateAboutIndex(colIndexRequested, colIndexRequested * Rows);
            HideNonFront(colIndexRequested);

            holderVisual.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 
                                                           GetAngleForColumn(frontColIndex)));
        }
        
        protected void DataRangeReady(int start, int end)
        {
            int row = 0;

            for (int i = start; i <= end; i++)
            {
                int actualColumn = i / Rows;
                if (actualColumn < 0) actualColumn = -(Math.Abs(actualColumn) % Columns) + Columns;
                else actualColumn %= Columns;

                // go request the visual
                if (RequestStillValid(row, actualColumn, i))
                {
                    // need some sort of request pending option here
                    if (!_rowcolToItem[row, actualColumn].visual3DRequested)
                    {
                        _rowcolToItem[row, actualColumn].visual3DRequested = true;
                        RequestVisual3D(i, row, actualColumn);                        
                    }
                }

                row++;
                if (row == 4)
                {
                    row = 0;
                }
            }
        }

        private int ColumnRange
        {
            get { return Columns / 6; }
        }

        private void HideNonFront(int column)
        {
            int colRange = ColumnRange;
            int startColumn = (column + colRange + 3) % Columns;
            int dist = Columns - (2 * colRange + 3);

            for (int i = 0; i < dist; i++)
            {
                int colToRemove = (startColumn + i) % Columns;
                if (_rowcolToItem[0, colToRemove].visual3D != null &&
                    !_rowcolToItem[0, colToRemove].visual3DInvisible)
                {
                    for (int j = 0; j < Rows; j++)
                    {
                        if (_rowcolToItem[j, colToRemove].visual3D != null)
                        {
                            VisualHidden(_rowcolToItem[j, colToRemove].visual3D);
                            holderVisual.Children.Remove(_rowcolToItem[j, colToRemove].visual3D);                           
                        }
                        _rowcolToItem[j, colToRemove].visual3DInvisible = true;
                    }
                }
            }
        }

        private void PopulateAboutIndex(int mappedToColumn, int indexRequested)
        {
            //
            // Start by marking territory
            // 
            int colRange = ColumnRange;
            
            int startIndexRequested = -1;
            int endIndexRequested = -1;

            indexRequested = indexRequested - colRange * Rows;
            for (int i = -colRange; i <= colRange + 2; i++)
            {
                int actualColumn = mappedToColumn + i;
                if (actualColumn < 0) actualColumn += Columns;
                else actualColumn %= Columns;

                // first row in column tells us all about the whole thing
                if (!RequestStillValid(0, actualColumn, indexRequested))
                {
                    for (int j = 0; j < Rows; j++)
                    {
                        // remove anything that was already there
                        Point ptIndex = new Point(j, actualColumn);

                        // use the _rowcolToIndex to see if we need to add the thing

                        if (_rowcolToItem[j, actualColumn].visual3D != null)
                        {
                            if (!_rowcolToItem[j, actualColumn].visual3DInvisible)
                            {
                                VisualHidden(_rowcolToItem[j, actualColumn].visual3D);
                            }

                            holderVisual.Children.Remove(_rowcolToItem[j, actualColumn].visual3D);
                            _rowcolToItem[j, actualColumn].visual3D = null;
                        }
                        _rowcolToItem[j, actualColumn].visual3DInvisible = false;
                        _rowcolToItem[j, actualColumn].visual3DRequested = false;

                        // request only if it's something we want
                        if (indexRequested + j >= 0 && indexRequested + j < GetNumMeshes())
                        {
                            _rowcolToItem[j, actualColumn].currDataIndex = indexRequested + j;

                            // add the temporary item if it's not visible
                            if (!_rowcolToItem[j, actualColumn].placeholderVisible)
                            {
                                holderVisual.Children.Add(_rowcolToItem[j, actualColumn]._placeholder);
                                _rowcolToItem[j, actualColumn].placeholderVisible = true;
                            }
                        }
                        else
                        {
                            _rowcolToItem[j, actualColumn].currDataIndex = -1;
                        }                        
                    }

                    if (indexRequested >= 0 && indexRequested < GetNumMeshes())
                    {
                        if (startIndexRequested == -1) startIndexRequested = indexRequested;
                        endIndexRequested = indexRequested + Rows - 1;
                    }
                }
                else
                {
                    if (!_rowcolToItem[0, actualColumn].visual3DRequested)
                    {
                        if (startIndexRequested == -1) startIndexRequested = indexRequested;
                        endIndexRequested = indexRequested + Rows - 1;
                    }
                    else if (_rowcolToItem[0, actualColumn].visual3DInvisible)
                    {
                        for (int j = 0; j < Rows; j++)
                        {
                            if (_rowcolToItem[j, actualColumn].visual3D != null)
                            {
                                VisualVisible(_rowcolToItem[j, actualColumn].visual3D);
                                holderVisual.Children.Add(_rowcolToItem[j, actualColumn].visual3D);
                                _rowcolToItem[j, actualColumn].visual3DInvisible = false;
                            }
                        }
                    }
                }

                indexRequested += Rows;                
            }

            //
            // Now request the actual data
            //
            if (endIndexRequested != -1)
            {
                RequestDataRange(startIndexRequested,
                                 endIndexRequested);
            }
        }

        protected bool RequestStillValid(int row, int col, int index)
        {
            return (_rowcolToItem[row, col].currDataIndex == index &&
                    _rowcolToItem[row, col].currDataIndex != -1);
        }

        /// <summary>
        /// Creates the carousel view 
        /// </summary>
        protected void Generate()
        {
            // start fresh
            ResetData();

            if (!Children.Contains(viewController))
            {
                Children.Add(viewController);
            }
            slider.Maximum = GetNumMeshes() / Rows;
            slider.Minimum = 0;
            slider.Value = 0;

            // show what we got
            DisplayFrontMost(0);

            return;            
        }
       
        protected void Visual3DReady(int row, int col, int index, ModelVisual3D visual3D)
        {
            Point point = new Point(row, col);
            if (RequestStillValid(row, col, index))
            {
                if (currentAnim == null)
                {
                    // create the animation to go with this data
                    currentAnim = new DoubleAnimation(slider.Value, slider.Maximum, 
                                            new Duration(TimeSpan.FromSeconds((slider.Maximum - slider.Value)* 10)));
                    slider.BeginAnimation(Slider.ValueProperty, currentAnim);
                }

                if (_rowcolToItem[row, col].placeholderVisible)
                {
                    holderVisual.Children.Remove(_rowcolToItem[row, col]._placeholder);
                    _rowcolToItem[row, col].placeholderVisible = false;
                }

                Transform3D transform = GetObjectTransform(row, col);
                visual3D.Transform = transform;

                if (!_rowcolToItem[row, col].visual3DInvisible)
                {
                    VisualVisible(visual3D);
                    holderVisual.Children.Add(visual3D);
                }

                // we now have a visual and it's visible
                _rowcolToItem[row, col].visual3D = visual3D;
            }
        }        

        public delegate void ItemClickedOnEvent(object sender, object item);
        public event ItemClickedOnEvent ItemClickedOn;

        protected virtual void OnItemClickedOn(object item)
        {
            ItemClickedOn(this, item);
        }

        public class CVItem
        {
            public CVItem()
            {
                currDataIndex = -1;
                visual3D = null;
                visual3DRequested = false;
                visual3DInvisible = false;

                placeholderVisible = false;
            }

            public int currDataIndex;

            public Visual3D visual3D;
            public bool visual3DInvisible;
            public bool visual3DRequested;

            public ModelVisual3D _placeholder;
            public bool placeholderVisible;
        }

        private CVItem[,] _rowcolToItem;
       
        private double _columnPadding;
        public double ColumnPadding
        {
            get { return _columnPadding; }
            set { _columnPadding = value; }
        }

        private double _rowPadding;
        public double RowPadding
        {
            get { return _rowPadding; }
            set { _rowPadding = value; }
        }

        private double CircleDiameter
        {
            get { return 2 * Math.PI * Radius; }
        }

        private double Radius
        {
            get { return 1; }
        }

        private double finalMeshWidth
        {
            get
            {
                // figure out the scaling to apply
                double meshWidth = GetVisualWidth();
                double visOnViewSize = (CircleDiameter - ColumnPadding * Columns) / Columns;
                double scaleFactor = visOnViewSize / meshWidth;

                double finalMeshWidth = meshWidth * scaleFactor;

                return finalMeshWidth;
            }
        }

        protected virtual void VisualVisible(Visual3D v)
        {
        }

        protected virtual void VisualHidden(Visual3D v)
        {
        }
		
        /// <summary>
        /// Positions the passed in mesh and also lays it out
        /// </summary>
        /// <param name="mesh"></param>
        /// <param name="currRow"></param>
        /// <param name="currCol"></param>
        public Transform3D GetObjectTransform(int currRow, int currCol)
        {
            // the final transform to be applied
            Transform3DGroup transform = new Transform3DGroup();

            // figure out the scaling to apply
            double meshWidth = GetVisualWidth();
            double visOnViewSize = (CircleDiameter - ColumnPadding * Columns) / Columns;
            double scaleFactor = visOnViewSize / meshWidth;

            double finalMeshWidth = meshWidth * scaleFactor;
            double finalMeshHeight = GetVisualHeight() * scaleFactor;

            // height to place the object
            double totalHeight = (Rows - 1) * (finalMeshHeight + RowPadding);
            double y = -1 * (currRow * (finalMeshHeight + RowPadding)) + totalHeight / 2;
            
            // angle to rotate it
            double angle = -1.0 * currCol * (finalMeshWidth + ColumnPadding) / CircleDiameter * 360;

            // create the rotate transform3d
            transform.Children.Add(new ScaleTransform3D(scaleFactor, scaleFactor, 1));
            transform.Children.Add(new TranslateTransform3D(0, y, -Radius));
            transform.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), angle)));            
            
            // apply it to the visual3D
            return transform;
        }

        public double GetAngleForColumn(double column)
        {
            double visOnViewSize = (CircleDiameter - ColumnPadding * Columns) / Columns;
            double angle = 1.0 * column * (visOnViewSize + ColumnPadding) / CircleDiameter * 360;

            return angle;
        }
    }
}
