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
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace PortalDemo
{
    public class Column
    {
        /// <summary>
        /// 列Id，Guid，惟一标识每个列
        /// </summary>
        public string ColumnId { get; set; }

        /// <summary>
        /// 左坐标
        /// </summary>
        public double Left
        {
            get
            {
                return LeftPercentage * Consts.HostWidth;
            }
        }

        /// <summary>
        /// 左坐标占整个宿主区宽度的百分比
        /// </summary>
        public double LeftPercentage { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public double Width
        {
            get
            {
                return WidthPercentage * Consts.HostWidth;
            }
        }

        /// <summary>
        /// 宽度占整个宿主区宽度的百分比
        /// </summary>
        public double WidthPercentage { get; set; }

        /// <summary>
        /// 列中所有区域的高度的总和
        /// </summary>
        public double TotalHeight
        {
            get
            {
                double totalHeight = 0.0;
                foreach (Area area in _areas)
                {
                    totalHeight += area.Height;
                }

                return totalHeight;
            }
        }

        private List<Area> _areas;
        /// <summary>
        /// 所包含的区域
        /// </summary>
        public List<Area> Areas
        {
            get
            {
                return _areas;
            }
        }

        public Column(string columnId, double leftPercentage, double widthPercentage)
        {
            this.ColumnId = columnId;
            this.LeftPercentage = leftPercentage;
            this.WidthPercentage = widthPercentage;
            this._areas = new List<Area>();
        }

        public Column(string xml, Canvas rootCanvas)
        {
            this._areas = new List<Area>();

            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            MemoryStream ms = new MemoryStream(bytes);

            XmlReader reader = XmlReader.Create(ms);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "Column")
                {
                    this.ColumnId = reader.GetAttribute("ColumnId");
                    this.LeftPercentage = double.Parse(reader.GetAttribute("LeftPercentage"));
                    this.WidthPercentage = double.Parse(reader.GetAttribute("WidthPercentage"));
                }
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "Area")
                {
                    Area area = new Area(reader.ReadOuterXml(), this.Left, this.Width, this.ColumnId);
                    this.CreateArea(area, rootCanvas);
                }
            }
            reader.Close();
            ms.Close();
        }

        /// <summary>
        /// 将一个新的区域加到列中
        /// </summary>
        /// <param name="area"></param>
        /// <param name="rootCanvas"></param>
        public void CreateArea(Area area, Canvas rootCanvas)
        {
            this._areas.Add(area);
            area.Init(rootCanvas);
        }

        /// <summary>
        /// 真正的移进一个区域（视觉上）
        /// </summary>
        /// <param name="sourceArea"></param>
        public void MoveInArea(Area sourceArea)
        {
            sourceArea.Paint();
        }

        /// <summary>
        /// 列移进某个区域前的准备工作（在源区域将要放入的位置显示框框）
        /// </summary>
        /// <param name="sourceArea"></param>
        /// <param name="targetArea"></param>
        /// <param name="rootCanvas"></param>
        public void BeforeMoveInArea(Area sourceArea, Area targetArea, Canvas rootCanvas)
        {
            double rectangleHeight = sourceArea.VisualHeight;

            //在源区域将要放入的位置显示框框
            Rectangle rectangle = rootCanvas.FindName("Rectangle0") as Rectangle;
            if (targetArea == null)
            {
                rectangle.SetValue(Canvas.TopProperty, this.TotalHeight);
            }
            else
            {
                rectangle.SetValue(Canvas.TopProperty, targetArea.Top);

                //将目标区域及以下区域下移，留出源区域的位置
                foreach (Area area in _areas)
                {
                    if (area.Top > targetArea.Top)
                    {
                        area.Top += rectangleHeight;
                        area.Paint();
                    }
                }
                targetArea.Top += rectangleHeight;
                targetArea.Paint();
            }

            rectangle.SetValue(Canvas.LeftProperty, this.Left);
            rectangle.Width = this.Width;
            rectangle.Height = rectangleHeight;
            rectangle.Visibility = Visibility.Visible;

            //更新源区域的位置信息并将其加入列（此时视觉上还感觉不到）
            sourceArea.Left = (double)rectangle.GetValue(Canvas.LeftProperty);
            sourceArea.Top = (double)rectangle.GetValue(Canvas.TopProperty);
            sourceArea.Width = rectangle.Width;
            sourceArea.ColumnId = this.ColumnId;
            this._areas.Add(sourceArea);
        }

        /// <summary>
        /// 从列中移出一个区域
        /// </summary>
        /// <param name="area"></param>
        public void MoveOutArea(Area area)
        {
            foreach (Area tempArea in _areas)
            {
                if (tempArea.Top > area.Top)
                {
                    tempArea.Top -= area.VisualHeight;
                    tempArea.Paint();
                }
            }

            this._areas.Remove(area);
        }

        /// <summary>
        /// 将Column对象转化成XML串
        /// </summary>
        /// <returns></returns>
        public string ToXML()
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendFormat("<Column ColumnId='{0}' LeftPercentage='{1}' WidthPercentage='{2}'>\r\n",
                this.ColumnId, this.LeftPercentage, this.WidthPercentage);
            if (this._areas != null && this._areas.Count > 0)
            {
                xml.Append("<Areas>\r\n");
                foreach (Area area in this._areas)
                {
                    xml.Append(area.ToXML());
                }
                xml.Append("</Areas>\r\n");
            }
            xml.Append("</Column>\r\n");

            return xml.ToString();
        }

        /// <summary>
        /// 当列中某个区域的高度发生变化时，重绘列中此区域下面的所有区域
        /// </summary>
        /// <param name="area"></param>
        /// <param name="varHeight"></param>
        public void RePaintArea(Area area, double varHeight)
        {
            area.Paint();
            foreach (Area downArea in this._areas)
            {
                if (downArea.Top > area.Top)
                {
                    downArea.Top = downArea.Top - varHeight;
                    downArea.Paint();
                }
            }
        }

        /// <summary>
        /// 删除列中的某个区域
        /// </summary>
        public void DeleteArea(Area area)
        {
            area.InBorder.Visibility = Visibility.Collapsed;
            this._areas.Remove(area);
            foreach (Area downArea in this._areas)
            {
                if (downArea.Top > area.Top)
                {
                    downArea.Top -= area.VisualHeight;
                    downArea.Paint();
                }
            }
        }

        /// <summary>
        /// 在列的顶部添加一个区域
        /// </summary>
        /// <param name="area"></param>
        /// <param name="rootCanvas"></param>
        public void AddArea(Area area, Canvas rootCanvas)
        {
            foreach (Area inArea in this._areas)
            {
                inArea.Top += area.Height;
                inArea.Paint();
            }

            this._areas.Add(area);
            area.Init(rootCanvas);
        }
    }
}
