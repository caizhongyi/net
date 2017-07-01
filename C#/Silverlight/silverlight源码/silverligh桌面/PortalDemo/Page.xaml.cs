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
using System.Windows.Browser;
using System.Xml;
using System.IO.IsolatedStorage;
using System.IO;
using System.Text;

namespace PortalDemo
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 所包含的列
        /// </summary>
        private List<Column> Columns = new List<Column>();

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string xml = GetAllAreaLayout();
                XmlReader reader = null;
                MemoryStream ms = null;
                if (!string.IsNullOrEmpty(xml))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(xml);
                    ms = new MemoryStream(bytes);
                    reader = XmlReader.Create(ms);
                }
                else
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(Consts.InitAreaLayout);
                    ms = new MemoryStream(bytes);
                    reader = XmlReader.Create(ms);
                }

                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "Column")
                    {
                        Column column = new Column(reader.ReadOuterXml(), LayoutRoot);
                        foreach (Area area in column.Areas)
                        {
                            area.InBorder.Style = UCRoot.Resources[area.InBorderStyleKey] as Style;
                            area.Head.LayoutRoot.Style = UCRoot.Resources[area.HeadStyleKey] as Style;

                            area.InBorder.MouseLeftButtonDown += new MouseButtonEventHandler(Handle_MouseDown);
                            area.InBorder.MouseMove += new MouseEventHandler(Handle_MouseMove);
                            area.InBorder.MouseLeftButtonUp += new MouseButtonEventHandler(Handle_MouseUp);
                            area.Head.btnCollapse.Click += new RoutedEventHandler(btnCollapse_Click);
                            area.Head.btnExpand.Click += new RoutedEventHandler(btnExpand_Click);
                            area.Head.btnClose.Click += new RoutedEventHandler(btnClose_Click);
                            area.Head.btnRefresh.Click += new RoutedEventHandler(btnRefresh_Click);
                        }
                        Columns.Add(column);
                    }
                }

                reader.Close();
                if (ms != null)
                {
                    ms.Close();
                }

                BorderToolBar.Width = Consts.HostWidth;
                RefreshHostHeight();

                AddAreaView.btnOK.Click += new RoutedEventHandler(btnOK_Click);
                AddAreaView.btnCancel.Click += new RoutedEventHandler(btnCancel_Click);
            }
            catch
            {
            }
        }

        #region 处理区域移动的方法

        /// <summary>
        /// 是否鼠标按下并捕获到了区域
        /// </summary>
        bool isMouseCaptured;

        /// <summary>
        /// 鼠标处在的纵坐标
        /// </summary>
        double mouseVerticalPosition;

        /// <summary>
        /// 鼠标处在的横坐标
        /// </summary>
        double mouseHorizontalPosition;

        /// <summary>
        /// 跟随鼠标移动的源区域
        /// </summary>
        Area sourceArea;

        /// <summary>
        /// 鼠标到达的目标区域
        /// </summary>
        Area targetArea;

        /// <summary>
        /// 源列
        /// </summary>
        Column sourceColumn;

        /// <summary>
        /// 鼠标到达的目标列
        /// </summary>
        Column targetColumn;

        /// <summary>
        /// 鼠标左键按下事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Handle_MouseDown(object sender, MouseEventArgs args)
        {
            try
            {
                mouseVerticalPosition = args.GetPosition(null).Y;
                mouseHorizontalPosition = args.GetPosition(null).X;
                sourceArea = GetArea(mouseHorizontalPosition, mouseVerticalPosition);
                sourceColumn = GetColumn(mouseHorizontalPosition);

                sourceArea.InBorder.CaptureMouse();
                isMouseCaptured = true;

                sourceArea.InBorder.SetValue(Canvas.ZIndexProperty, 2);
                //sourceArea.InStackPanel.Background = new SolidColorBrush(Colors.Magenta);
                sourceArea.InStackPanel.Opacity = 0.3;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 鼠标移动事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Handle_MouseMove(object sender, MouseEventArgs args)
        {
            try
            {
                if (isMouseCaptured)
                {
                    //计算出鼠标纵、横坐标的变化
                    double deltaV = args.GetPosition(null).Y - mouseVerticalPosition;
                    double deltaH = args.GetPosition(null).X - mouseHorizontalPosition;

                    //让源区域随着鼠标移动
                    double newTop = deltaV + (double)sourceArea.InBorder.GetValue(Canvas.TopProperty);
                    double newLeft = deltaH + (double)sourceArea.InBorder.GetValue(Canvas.LeftProperty);
                    sourceArea.InBorder.SetValue(Canvas.TopProperty, newTop);
                    sourceArea.InBorder.SetValue(Canvas.LeftProperty, newLeft);

                    //更新鼠标纵、横坐标
                    mouseVerticalPosition = args.GetPosition(null).Y;
                    mouseHorizontalPosition = args.GetPosition(null).X;

                    //移动过程中激活目标区域
                    ActiveTargetArea(mouseHorizontalPosition, mouseVerticalPosition);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 鼠标左键放开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void Handle_MouseUp(object sender, MouseEventArgs args)
        {
            try
            {
                mouseVerticalPosition = args.GetPosition(null).Y;
                mouseHorizontalPosition = args.GetPosition(null).X;
                targetArea = GetArea(mouseHorizontalPosition, mouseVerticalPosition);
                targetColumn = GetColumn(mouseHorizontalPosition);

                targetColumn.MoveInArea(sourceArea);
                RefreshHostHeight();

                //sourceArea.InStackPanel.Background = new SolidColorBrush(Colors.White);
                sourceArea.InStackPanel.Opacity = 1.0;
                sourceArea.InBorder.SetValue(Canvas.ZIndexProperty, 1);

                isMouseCaptured = false;
                sourceArea.InBorder.ReleaseMouseCapture();

                mouseVerticalPosition = -1;
                mouseHorizontalPosition = -1;

                Rectangle0.Visibility = Visibility.Collapsed;

                SaveAllAreaLayout();
            }
            catch
            {
            }
        }

        #endregion

        #region 处理区域移动的辅助方法

        /// <summary>
        /// 得到鼠标处在的区域
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private Area GetArea(double x, double y)
        {
            foreach (Column column in Columns)
            {
                foreach (Area area in column.Areas)
                {
                    if (x > area.Left && x < area.Left + area.Width)
                    {
                        if (y > area.Top && y < area.Top + area.Height)
                        {
                            return area;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 得到鼠标处在的列
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private Column GetColumn(double x)
        {
            foreach (Column column in Columns)
            {
                if (x > column.Left && x < column.Left + column.Width)
                {
                    return column;
                }
            }

            return null;
        }

        /// <summary>
        /// 移动过程中激活目标区域
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void ActiveTargetArea(double x, double y)
        {
            targetColumn = GetColumn(x);
            targetArea = GetArea(x, y);

            if (sourceColumn.ColumnId == targetColumn.ColumnId && targetArea != null && sourceArea.AreaId == targetArea.AreaId)
            {
                Rectangle0.SetValue(Canvas.TopProperty, sourceArea.Top);
                Rectangle0.SetValue(Canvas.LeftProperty, sourceArea.Left);
                Rectangle0.Width = sourceArea.Width;
                Rectangle0.Height = sourceArea.VisualHeight;
                Rectangle0.Visibility = Visibility.Visible;
            }
            else
            {
                sourceColumn.MoveOutArea(sourceArea);
                targetColumn.BeforeMoveInArea(sourceArea, targetArea, LayoutRoot);
                sourceColumn = targetColumn;
            }
        }

        /// <summary>
        /// 根据所有列的最大高度刷新宿主区的高度
        /// </summary>
        private void RefreshHostHeight()
        {
            double maxColumnHeight = 0.0;
            foreach (Column column in Columns)
            {
                if (column.TotalHeight > maxColumnHeight)
                {
                    maxColumnHeight = column.TotalHeight;
                }
            }

            double maxAreaHeight = 0.0;
            foreach (Column column in Columns)
            {
                foreach (Area area in column.Areas)
                {
                    if (area.Height > maxAreaHeight)
                    {
                        maxAreaHeight = area.Height;
                    }
                }
            }

            double lastHeight = (maxColumnHeight + maxAreaHeight) > 1000.0 ? (maxColumnHeight + maxAreaHeight) : 1000.0;

            HtmlDocument doc = HtmlPage.Document;
            HtmlElement ele1=doc.GetElementById("Xaml1");
            if(ele1!=null)
            {
                ele1.SetAttribute("height", lastHeight + "px");
            } 
        }

        /// <summary>
        /// 根据宿主区的宽度重绘所有的区域
        /// </summary>
        public void PaintAllArea()
        {
            foreach (Column column in Columns)
            {
                foreach (Area area in column.Areas)
                {
                    area.Left = column.Left;
                    area.Width = column.Width;
                    area.Paint();
                }
            }
        }

        /// <summary>
        /// 保存所有区域的布局
        /// </summary>
        public void SaveAllAreaLayout()
        {
            StringBuilder xml = new StringBuilder();
            xml.Append("<Columns>\r\n");
            foreach (Column column in Columns)
            {
                xml.Append(column.ToXML());
            }
            xml.Append("</Columns>\r\n");

            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForSite())
            {
                string directoryName = "AreaLayout";
                if (!store.DirectoryExists(directoryName))
                {
                    store.CreateDirectory(directoryName);
                }

                string filePath = directoryName + "/" + "AreaLayout.xml";
                if (store.FileExists(filePath))
                {
                    store.DeleteFile(filePath);
                }
                IsolatedStorageFileStream fileStream = store.CreateFile(filePath);
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    sw.Write(xml);
                }
                fileStream.Close();
            }
        }

        /// <summary>
        /// 得到所有区域的布局
        /// </summary>
        /// <returns></returns>
        public string GetAllAreaLayout()
        {
            string xml = string.Empty;
            using (IsolatedStorageFile store = IsolatedStorageFile.GetUserStoreForSite())
            {
                String filePath = "AreaLayout/AreaLayout.xml";
                if (store.FileExists(filePath))
                {
                    IsolatedStorageFileStream fileStream = store.OpenFile(filePath, FileMode.Open, FileAccess.Read);
                    StreamReader reader = new StreamReader(fileStream);
                    xml = reader.ReadToEnd();
                    fileStream.Close();
                }
            }

            return xml;
        }

        #endregion

        #region 事件响应

        /// <summary>
        /// 收起区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCollapse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                AreaHead head = ((Grid)btn.Parent).Parent as AreaHead;
                Area area = GetArea(head.AreaId);
                Column column = GetColumn(area.ColumnId);
                area.State = AreaState.Collapse;
                double varHeight = area.Height - area.CollapsedHeight;
                column.RePaintArea(area, varHeight);

                head.btnCollapse.Visibility = Visibility.Collapsed;
                head.btnExpand.Visibility = Visibility.Visible;

                SaveAllAreaLayout();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 展开区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExpand_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                AreaHead head = ((Grid)btn.Parent).Parent as AreaHead;
                Area area = GetArea(head.AreaId);
                Column column = GetColumn(area.ColumnId);
                area.State = AreaState.Expand;
                double varHeight = area.CollapsedHeight - area.Height;
                column.RePaintArea(area, varHeight);

                head.btnCollapse.Visibility = Visibility.Visible;
                head.btnExpand.Visibility = Visibility.Collapsed;

                SaveAllAreaLayout();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 关闭区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                AreaHead head = ((Grid)btn.Parent).Parent as AreaHead;
                Area area = GetArea(head.AreaId);
                Column column = GetColumn(area.ColumnId);
                column.DeleteArea(area);

                SaveAllAreaLayout();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 显示添加区域视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddArea_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RectangleOut.SetValue(Canvas.ZIndexProperty, 10);
                RectangleOut.Visibility = Visibility.Visible;

                AddAreaView.SetValue(Canvas.ZIndexProperty, 11);
                AddAreaView.SetValue(Canvas.LeftProperty, 0.5 * (Consts.HostWidth - AddAreaView.Width));
                AddAreaView.SetValue(Canvas.TopProperty, 100.0);
                AddAreaView.Visibility = Visibility.Visible;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 添加区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double height=100.0;
                try
                {
                    height = double.Parse(AddAreaView.txtHeight.Text);
                    if (height < 100.0 || height > 500.0)
                    {
                        HtmlPage.Window.Alert("高度值要在100与500之间！");
                        return;
                    }
                }
                catch
                {
                    HtmlPage.Window.Alert("请输入正确的高度值！");
                    return;
                }

                ComboBoxItem cbTitleSelectedItem = AddAreaView.cbTitle.SelectedItem as ComboBoxItem;
                if (cbTitleSelectedItem == null)
                {
                    HtmlPage.Window.Alert("请选择标题！");
                    return;
                }
                string title = cbTitleSelectedItem.Content.ToString();
                foreach (Column tempColumn in Columns)
                {
                    foreach (Area tempArea in tempColumn.Areas)
                    {
                        if (tempArea.Head.txtTitle.Text == title)
                        {
                            HtmlPage.Window.Alert(string.Format("标题为“{0}”的区域已存在于页面上，请选择其他的标题！",title));
                            return;
                        }
                    }
                }

                ComboBoxItem cbTitleBGSelectedItem = AddAreaView.cbTitleBG.SelectedItem as ComboBoxItem;
                if (cbTitleBGSelectedItem == null)
                {
                    HtmlPage.Window.Alert("请选择标题背景！");
                    return;
                }
                string titleBG = cbTitleBGSelectedItem.Content.ToString();

                RectangleOut.Visibility = Visibility.Collapsed;
                AddAreaView.Visibility = Visibility.Collapsed;

                Column column = Columns[0];
                Area area = new Area(Guid.NewGuid().ToString(), column.Left, 0.0, column.Width, height, title,
                    titleBG, "InBorder", column.ColumnId, AreaState.Expand);
                area.InBorder.Style = UCRoot.Resources[area.InBorderStyleKey] as Style;
                area.Head.LayoutRoot.Style = UCRoot.Resources[area.HeadStyleKey] as Style;

                area.InBorder.MouseLeftButtonDown += new MouseButtonEventHandler(Handle_MouseDown);
                area.InBorder.MouseMove += new MouseEventHandler(Handle_MouseMove);
                area.InBorder.MouseLeftButtonUp += new MouseButtonEventHandler(Handle_MouseUp);
                area.Head.btnCollapse.Click += new RoutedEventHandler(btnCollapse_Click);
                area.Head.btnExpand.Click += new RoutedEventHandler(btnExpand_Click);
                area.Head.btnClose.Click += new RoutedEventHandler(btnClose_Click);
                column.AddArea(area, LayoutRoot);

                SaveAllAreaLayout();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 取消添加区域
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RectangleOut.Visibility = Visibility.Collapsed;
                AddAreaView.Visibility = Visibility.Collapsed;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 刷新AreaBody
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = sender as Button;
                AreaHead head = ((Grid)btn.Parent).Parent as AreaHead;
                Area area = GetArea(head.AreaId);

                if (area.Body != null)
                {
                    AreaBody areaBody = area.Body as AreaBody;
                    areaBody.DownloadData();
                }
            }
            catch
            {
            }
        }

        #endregion

        #region 事件响应辅助方法

        /// <summary>
        /// 根据AreaId得到某个区域
        /// </summary>
        /// <param name="areaId"></param>
        /// <returns></returns>
        private Area GetArea(string areaId)
        {
            foreach (Column column in Columns)
            {
                foreach (Area area in column.Areas)
                {
                    if (area.AreaId == areaId)
                    {
                        return area;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 根据列Id得到某个列
        /// </summary>
        /// <param name="columnId"></param>
        /// <returns></returns>
        private Column GetColumn(string columnId)
        {
            foreach (Column column in Columns)
            {
                if (column.ColumnId == columnId)
                {
                    return column;
                }
            }

            return null;
        }

        #endregion
    }
}
