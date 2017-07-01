using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;


    public class GridOperation
    {
        public Grid CreateGrid(int columnCount, int rowsCount, double width, double height)
        {
            Grid grid = new Grid();
           
            for(int i=0;i<columnCount;i++)
            {
               ColumnDefinition c = new ColumnDefinition();
               c.Width = new GridLength( width);
               grid.ColumnDefinitions.Add(c);
              
            }
            for (int i = 0; i < rowsCount; i++)
            {
                RowDefinition r = new RowDefinition();
                r.Height = new GridLength(height);
                grid.RowDefinitions.Add(r);
            }
            return grid;
        }
    }

