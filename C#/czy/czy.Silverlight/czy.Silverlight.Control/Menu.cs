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
using czy.Silverlight.StoryBoard;

namespace czy.Silverlight.Controls
{
    public class NavgationEventArgs : EventArgs
    {
        private int _itemIndex;

        public int ItemIndex
        {
            get { return _itemIndex; }
            set { _itemIndex = value; }
        }
        public NavgationEventArgs(int index)
        {
            _itemIndex = index;
        }
    }
    public class Menu : Control
    {
        #region 私有变量
        public delegate void Event(object o, NavgationEventArgs e);
        public Event ItemClick;
        private Thickness _itemThickness = new Thickness(10, 0, 10, 0);
        private Size _navItemSize = new Size(50, 25);
        private int currentIndex = -1;
        private string currentText = string.Empty;
        #endregion
      
        #region 属性
        /// <summary>
        /// 当前先择项
        /// </summary>
        public int CurrentIndex
        {
            get { return currentIndex; }
            set { currentIndex = value; }
        }
      
        /// <summary>
        /// 当前先择文字
        /// </summary>
        public string CurrentText
        {
            get { return currentText; }
            set { currentText = value; }
        }
        /// <summary>
        /// Menu Item项大小
        /// </summary>
        public Size NavItemSize { get { return _navItemSize; } set { value = _navItemSize; } }
  
        /// <summary>
        /// 左右上下边距
        /// </summary>
        public Thickness ItemThickness { get; set; }
        /// <summary>
        /// Menu数据
        /// </summary>
        public List<MenuItemData> DataList { get; set; }

        #endregion

        public Menu(List<MenuItemData> dataList)
        {
            this.DefaultStyleKey = typeof(Menu);
            DataList = dataList;
      
        }
        /// <summary>
        /// 载入模版
        /// </summary>
        public override void OnApplyTemplate()
        {
            StackPanel sPanel = this.GetTemplateChild("sPanel") as StackPanel;
            LoadData(DataList, sPanel);
        }
        private void LoadData(List<MenuItemData> list, Panel spanel)
        {
         
            for (int i = 0; i < list.Count; i++)
            {
                Border border = new Border();

                TextBlock tb = new TextBlock();
                border.MouseEnter += new MouseEventHandler(tb_MouseEnter);
                border.MouseLeave += new MouseEventHandler(border_MouseLeave);
                border.MouseLeftButtonUp += new MouseButtonEventHandler(border_MouseLeftButtonUp);
                tb.Text = list[i].Name;
                tb.FontSize = 14;
                tb.FontWeight = FontWeights.Bold;
                tb.TextAlignment = TextAlignment.Center;
                tb.VerticalAlignment = VerticalAlignment.Center;
                border.Name = i.ToString();
                SolidColorBrush scb = new SolidColorBrush(Colors.White);
                tb.Foreground = scb;
                border.BorderThickness = new Thickness(1);
                border.Effect = new System.Windows.Media.Effects.DropShadowEffect();
                border.CornerRadius = new CornerRadius(5);
                //border.BorderBrush = scb;
                border.Cursor = Cursors.Hand;
                border.Width = _navItemSize.Width;
                border.Height = _navItemSize.Height;
                tb.Text = list[i].Name;
                border.Projection = new PlaneProjection();
                border.Margin = ItemThickness;
                border.Child = tb;

                if (i != 0)
                {
                    //Image img = new Image();
                    // img.Source = new BitmapImage(new Uri("images/search_line.jpg", UriKind.RelativeOrAbsolute));
                    // (spanel as StackPanel).Children.Add(img);
                }
                (spanel as StackPanel).Children.Add(border);


            }
        }
        void tb_MouseEnter(object sender, MouseEventArgs e)
        {
            Border b = (sender as Border);
            BaseStotyBoard sb = BaseStoryBoardBehaviors.PlaneProjectionStoryBoard(b);
            sb.Begin();
        }
        void border_MouseLeave(object sender, MouseEventArgs e)
        {
            Border b = (sender as Border);
            BaseStotyBoard sb = BaseStoryBoardBehaviors.RePlaneProjectionStoryBoard(b);
            sb.Begin();
        }
        void border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Border border=sender as Border;
            int index = Convert.ToInt32(border.Name);
            currentIndex = index;
            currentText = (border.Child as TextBlock).Text;
            if (ItemClick != null) { ItemClick(sender, new NavgationEventArgs(index)); }
        }
        public class MenuItemData
        {
            public String Name { get; set; }
        }
    }
}
