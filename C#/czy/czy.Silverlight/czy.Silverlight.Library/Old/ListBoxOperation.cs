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

namespace Media
{
    public class ListBoxOperation
    {
        /// <summary>
        /// 创建listBox(横向排列)
        /// </summary>
        /// <returns></returns>
        public ListBox CreateListBoxH()
        {
            Style style = new Style(typeof(ListBoxItem));
            style.Setters.Add(new Setter(ListBoxItem.HorizontalContentAlignmentProperty, HorizontalAlignment.Stretch));
            ListBox listBox = new ListBox();
            return listBox;
        }

        /// <summary>
        /// 创建listBox(纵向排列)
        /// </summary>
        /// <returns></returns>
        public ListBox CreateListBoxV()
        {
            Style style = new Style(typeof(ListBoxItem));
            style.Setters.Add(new Setter(ListBoxItem.VerticalAlignmentProperty,VerticalAlignment.Stretch));
            ListBox listBox = new ListBox();
            return listBox;
        }

        /// <summary>
        /// 图片填充listBoxItem
        /// </summary>
        /// <param name="img">图片</param>
        /// <returns></returns>
        public ListBoxItem ImageFillListBoxItem(Image img)
        {    
            ListBoxItem listboxItem = new ListBoxItem();
            listboxItem.Content = img;
            return listboxItem;
        }

    }
}
