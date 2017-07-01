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


    public class CanvasActionInGrid
    {
        /// <summary>
        /// 设置GridColumn(Grid布局动画)
        /// </summary>
        /// <param name="ldf">LinearDoubleKeyFrame</param>
        /// <param name="targetName">Grid</param>
        /// <param name="doubledKeyFrams">doubledKeyFrams</param>
        /// <param name="column">column的值</param>
        public void SetGridColumn(LinearDoubleKeyFrame ldf, Canvas targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams, double  column)
        {
            Storyboard.SetTarget(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(Grid.ColumnSpanProperty));
            ldf.Value = column;
        }
        /// <summary>
        /// 设置GridRow(Grid布局动画)
        /// </summary>
        /// <param name="ldf">LinearDoubleKeyFrame</param>
        /// <param name="targetName">Grid</param>
        /// <param name="doubledKeyFrams">DoubleAnimationUsingKeyFrames</param>
        /// <param name="row">row</param>
        public void SetGridRow(LinearDoubleKeyFrame ldf, Canvas targetName, DoubleAnimationUsingKeyFrames doubledKeyFrams, double  row)
        {
            Storyboard.SetTarget(doubledKeyFrams, targetName);
            Storyboard.SetTargetProperty(doubledKeyFrams, new PropertyPath(Grid.RowSpanProperty));
            ldf.Value = row;
        }
    }

