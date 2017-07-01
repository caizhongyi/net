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
using System.Text;
using System.Xml;
using System.IO;

namespace PortalDemo
{
    public class Area
    {
        /// <summary>
        /// 区域Id，Guid，惟一标识每个区域
        /// </summary>
        public string AreaId { get; set; }

        /// <summary>
        /// 左坐标
        /// </summary>
        public double Left { get; set; }

        /// <summary>
        /// 右坐标
        /// </summary>
        public double Top { get; set; }

        /// <summary>
        /// 宽度
        /// </summary>
        public double Width { get; set; }

        /// <summary>
        /// 高度
        /// </summary>
        public double Height { get; set; }

        /// <summary>
        /// 区域收起的高度
        /// </summary>
        public double CollapsedHeight
        {
            get
            {
                return 50;
            }
        }

        /// <summary>
        /// 区域在视觉上的高度
        /// </summary>
        public double VisualHeight
        {
            get
            {
                if (this.State == AreaState.Expand)
                {
                    return this.Height;
                }
                if (this.State == AreaState.Collapse)
                {
                    return this.CollapsedHeight;
                }

                return this.Height;
            }
        }

        /// <summary>
        /// 头部的样式键名
        /// </summary>
        public string HeadStyleKey { get; set; }

        /// <summary>
        /// 区域边框的样式键名
        /// </summary>
        public string InBorderStyleKey { get; set; }

        /// <summary>
        /// 所属列Id
        /// </summary>
        public string ColumnId { get; set; }

        /// <summary>
        /// 区域的状态
        /// </summary>
        public AreaState State { get; set; }

        /// <summary>
        /// 内部Border
        /// </summary>
        public Border InBorder { get; set; }

        /// <summary>
        /// 内部StackPanel
        /// </summary>
        public StackPanel InStackPanel { get; set; }

        /// <summary>
        /// 头部
        /// </summary>
        public AreaHead Head { get; set; }

        /// <summary>
        /// 内容区
        /// </summary>
        public UserControl Body { get; set; }

        public Area(string areaId, double left, double top, double width, double height, string headTitle, string headStyleKey, string inBorderStyleKey, string columnId, AreaState state)
        {
            this.AreaId = areaId;
            this.Left = left;
            this.Top = top;
            this.Width = width;
            this.Height = height;
            this.HeadStyleKey = headStyleKey;
            this.InBorderStyleKey = inBorderStyleKey;
            this.ColumnId = columnId;
            this.State = state;

            this.InBorder = new Border();
            this.InBorder.Name = "Border" + AreaId;

            this.Head = new AreaHead();
            this.Head.Name = "AreaTitleBar" + AreaId;
            this.Head.txtTitle.Text = headTitle;
            this.Head.AreaId = this.AreaId;

            this.Body = new AreaBody();
            this.Body.Name = "AreaBody" + AreaId;
            (this.Body as AreaBody).DataServiceUrl = Consts.GetDataServiceUrl(this.Head.txtTitle.Text);

            this.InStackPanel = new StackPanel();
            this.InStackPanel.Name = "StackPanel" + AreaId;
            this.InStackPanel.Children.Add(this.Head);
            this.InStackPanel.Children.Add(this.Body);

            this.InBorder.Child = this.InStackPanel;

            Paint();
        }

        public Area(string xml, double left, double width, string columnId)
        {
            this.Left = left;
            this.Width = width;
            this.ColumnId = columnId;

            byte[] bytes = Encoding.UTF8.GetBytes(xml);
            MemoryStream ms = new MemoryStream(bytes);

            XmlReader reader = XmlReader.Create(ms);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "Area")
                {
                    this.AreaId = reader.GetAttribute("AreaId");
                    this.Top = double.Parse(reader.GetAttribute("Top"));
                    this.Height = double.Parse(reader.GetAttribute("Height"));
                    this.HeadStyleKey = reader.GetAttribute("HeadStyleKey");
                    this.InBorderStyleKey = reader.GetAttribute("InBorderStyleKey");
                    this.State = (AreaState)int.Parse(reader.GetAttribute("State"));

                    this.InBorder = new Border();
                    this.InBorder.Name = "Border" + AreaId;

                    this.Head = new AreaHead();
                    this.Head.Name = "AreaTitleBar" + AreaId;
                    this.Head.AreaId = this.AreaId;

                    this.InStackPanel = new StackPanel();
                    this.InStackPanel.Name = "StackPanel" + AreaId;
                    this.InStackPanel.Children.Add(this.Head);

                    this.InBorder.Child = this.InStackPanel;
                }
                if (reader.NodeType == XmlNodeType.Element && reader.LocalName == "AreaHead")
                {
                    this.Head.txtTitle.Text = reader.GetAttribute("Title");

                    this.Body = new AreaBody();
                    this.Body.Name = "AreaBody" + AreaId;
                    (this.Body as AreaBody).DataServiceUrl = Consts.GetDataServiceUrl(this.Head.txtTitle.Text);
                    this.InStackPanel.Children.Add(this.Body);
                }
            }
            reader.Close();
            ms.Close();

            Paint();
        }

        /// <summary>
        /// 将区域添加到画布上
        /// </summary>
        /// <param name="rootCanvas"></param>
        public void Init(Canvas rootCanvas)
        {
            rootCanvas.Children.Add(this.InBorder);
        }

        /// <summary>
        /// 绘出区域
        /// </summary>
        public void Paint()
        {
            this.InBorder.Width = this.Width - 10;
            this.InBorder.SetValue(Canvas.LeftProperty, this.Left + 5);
            this.InBorder.SetValue(Canvas.TopProperty, this.Top + 5);

            this.Head.Width = this.Width - 10;
            this.Head.Margin = new Thickness(2.0, 2.0, 2.0, 0.0);

            if (this.Body != null)
            {
                this.Body.Width = this.Width - 10;
                this.Body.Margin = new Thickness(2.0, 2.0, 2.0, 0.0);
                AreaBody areaBody = this.Body as AreaBody;
                areaBody.lstInformation.Width = this.Width - 20;
                areaBody.lstInformation.Height = this.Height - this.CollapsedHeight;
                areaBody.lstInformation.Margin = new Thickness(2.0, 0.0, 2.0, 0.0);
            }

            this.InStackPanel.Width = this.Width - 10;
            this.InStackPanel.SetValue(Canvas.LeftProperty, this.Left + 5);
            this.InStackPanel.SetValue(Canvas.TopProperty, this.Top + 5);

            if (this.State == AreaState.Collapse)
            {
                this.InBorder.Height = this.CollapsedHeight - 10;
                this.InStackPanel.Height = this.CollapsedHeight - 10;
                this.Head.btnCollapse.Visibility = Visibility.Collapsed;
                this.Head.btnExpand.Visibility = Visibility.Visible;
            }
            else
            {
                this.InBorder.Height = this.Height - 10;
                this.InStackPanel.Height = this.Height - 10;
                this.Head.btnCollapse.Visibility = Visibility.Visible;
                this.Head.btnExpand.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 将Area对象转化为XML串
        /// </summary>
        /// <returns></returns>
        public string ToXML()
        {
            StringBuilder xml = new StringBuilder();
            xml.AppendFormat("<Area AreaId='{0}' Top='{1}' Height='{2}' HeadStyleKey='{3}' InBorderStyleKey='{4}' State='{5}'>\r\n",
                this.AreaId, this.Top, this.Height, this.HeadStyleKey, this.InBorderStyleKey, (int)this.State);
            xml.AppendFormat("<AreaHead Title='{0}'>\r\n", this.Head.txtTitle.Text);
            xml.Append("</AreaHead>\r\n");
            xml.Append("</Area>\r\n");

            return xml.ToString();
        }
    }

    /// <summary>
    /// 区域的状态
    /// </summary>
    public enum AreaState
    {
        /// <summary>
        /// 收起
        /// </summary>
        Collapse,

        /// <summary>
        /// 展开
        /// </summary>
        Expand
    }
}
