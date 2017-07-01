using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Drawing;

namespace czy.Wpf.Library.Control
{
    /// <summary>
    /// PostionBoard.xaml 的交互逻辑
    /// </summary>
    public partial class PostionBoard : UserControl
    {
        private System.Windows.Size _size = new System.Windows.Size(600, 500);

        //public System.Windows.Size Size
        //{
        //    get { return _size; }
        //    set { _size = value; }
        //}

        public PostionBoard()
        {
            InitializeComponent();
            //this.Width = _size.Width;
            //this.Height = _size.Height;
        }
        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="el">控件</param>
        /// <param name="point">位置</param>
        public void Add(UIElement el,System.Windows.Point point)
        {
            Canvas.SetLeft(el, point.X);
            Canvas.SetTop(el, point.Y);
            Board.Children.Add(el);
            el.DragDrop(this.Board);
        }
        /// <summary>
        /// 添加控件
        /// </summary>
        /// <param name="el">控件</param>
        /// <param name="elName">控件名称,名称需与保存的类中的Point属性名一致</param>
        /// <param name="point">位置</param>
        public void Add(UIElement el,string elName, System.Windows.Point point)
        {
            if ((el as System.Windows.Controls.Control) != null)
            {
                (el as System.Windows.Controls.Control).Name = elName;
            }
            else
            {
                (el as System.Windows.Shapes.Shape).Name = elName;
            }
            Canvas.SetLeft(el, point.X);
            Canvas.SetTop(el, point.Y);
            Board.Children.Add(el);
            el.DragDrop(this.Board);
        }
        private List<UIElement> GetElements(Canvas c)
        {    
            List<UIElement> eles = new List<UIElement>();
            for(int i=0;i<c.Children.Count;i++)
            {
                eles.Add(c.Children[i]);
            }
            return eles;
        }
        /// <summary>
        /// 保存 注:控件名称必须与类模型中的属性名称一致才能保存.属性为Point类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="path">路径</param>
        /// <param name="t">类</param>
        public void Save<T>(string path, T t) where T : class,new()
        {
            List<UIElement> eles =GetElements(this.Board);

            for (int i = 0; i < eles.Count; i++)
            {
                string name=(eles[i] as System.Windows.Controls.Control)==null?(eles[i] as System.Windows.Shapes.Shape).Name:(eles[i] as System.Windows.Controls.Control).Name;
                PropertyInfo propertyInfo = t.GetType().GetProperty(name);
                switch ((eles[i]).GetType().Name)
                {
                    case "Label":
                        if (propertyInfo!=null)
                        propertyInfo.SetValue(t, new System.Drawing.Point(Convert.ToInt32(Canvas.GetLeft(eles[i])), Convert.ToInt32(Canvas.GetTop(eles[i]))), null);
                        break;
                    case "Line": Line l = eles[i] as Line;
                        if (propertyInfo != null)
                        propertyInfo.SetValue(t, new System.Drawing.Point[] { 
                            new System.Drawing.Point(Convert .ToInt32(l.X1),Convert.ToInt32(Convert .ToInt32(l.Y1))), 
                            new System.Drawing.Point(Convert .ToInt32(l.X2),Convert.ToInt32(l.Y2)),
                            new System.Drawing.Point(Convert.ToInt32(Canvas.GetLeft(eles[i])), Convert.ToInt32(Canvas.GetTop(eles[i])))
                        }, null);
                      
                        break;
                }
            }
            czy.MyClass.XML.Serializer.ClassToXML(t,path);
        }
        /// <summary>
        /// 读取 注:控件名称必须与类模型中的属性名称一致才能读取.属性为Point类型
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public T Read<T>(string path) where T : class,new()
        {
            T t=czy.MyClass.XML.Serializer.XMLToClass<T>(path);
            List<UIElement> eles = GetElements(this.Board);
            for (int i = 0; i < eles.Count; i++)
            {
                string name = (eles[i] as System.Windows.Controls.Control) == null ? (eles[i] as System.Windows.Shapes.Shape).Name : (eles[i] as System.Windows.Controls.Control).Name;
                PropertyInfo propertyInfo = t.GetType().GetProperty(name);
                switch ((eles[i]).GetType().Name)
                {
                    case "Label":
                        if (propertyInfo != null)
                        {
                            System.Drawing.Point pf = (System.Drawing.Point)propertyInfo.GetValue(t, null);
                            System.Windows.Point p = new System.Windows.Point(pf.X, pf.Y);
                            Canvas.SetLeft(eles[i], p.X);
                            Canvas.SetTop(eles[i], p.Y);
                        }
                        break;
                    case "Line":
                        if (propertyInfo != null)
                        {
                            System.Drawing.Point[] points = (System.Drawing.Point[])propertyInfo.GetValue(t, null);
                            System.Windows.Point start = new System.Windows.Point(points[0].X, points[0].Y);
                            System.Windows.Point end = new System.Windows.Point(points[1].X, points[1].Y);
                            Line l = eles[i] as Line;
                            l.X1 = 0;
                            l.X2 = end.X;
                            l.Y1 = 0;
                            l.Y2 = end.Y;
                            Canvas.SetLeft(l, points[2].X);
                            Canvas.SetTop(l, points[2].Y);
                        }
                        break;
                }
              
            }
            return t;
        }
        /// <summary>
        /// 根据模型创建Label
        /// </summary>
        /// <typeparam name="T">模型类</typeparam>
        /// <param name="t"></param>
        public List<Label> AddLabels<T>(T t) where T : class,new()
        {
            List<Label> labels = new List<Label>();
            foreach (PropertyInfo property in t.GetType().GetProperties())
            {
                Label l=new Label();
                object[] o= property.GetCustomAttributes(false);
                if (o.Length > 0)
                {
                    l.Content = (o[0] as System.ComponentModel.DescriptionAttribute).Description;
                }
                else
                {
                    l.Content = property.Name;
                }
                System.Drawing.Point pf=(System.Drawing.Point)property.GetValue(t, null);
                System.Windows.Point p = new System.Windows.Point(pf.X, pf.Y);
                this.Add(l,property.Name,new System.Windows.Point(p.X,p.Y));
                labels.Add(l);
            }
            return labels;
        }

        public List<Line> AddLines<T>(T t) where T : class,new()
        {
            List<Line> list = new List<Line>();
            foreach (PropertyInfo property in t.GetType().GetProperties())
            {
                
                //DrawingVisual dv = new DrawingVisual();
                System.Drawing.Point[] pf = (System.Drawing.Point[])property.GetValue(t, null);
                System.Windows.Point start = new System.Windows.Point(pf[0].X, pf[0].Y);
                System.Windows.Point end = new System.Windows.Point(pf[1].X, pf[1].Y);
                System.Windows.Point points = new System.Windows.Point(pf[2].X, pf[2].Y);
                Line l = new Line();
              //  System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle();
                l.X1 = 0;
                l.X2 = end.X;
                l.Y1 = 0;
                l.Y2 = end.Y;
                //l.Width = end.X - start.X == 0 ? 10 : end.X - start.X;
                //l.Height = end.Y - start.Y == 0 ? 10 : end.Y - start.Y;
                l.StrokeThickness = 1;
                l.Stroke = System.Windows.Media.Brushes.Black;
                
               // this.Board.Children.Add(l);
                //l.DragDrop(this.Board);
                this.Add(l, property.Name, points);
                //dv.RenderOpen().DrawLine(new System.Windows.Media.Pen(System.Windows.Media.Brushes.Black, 1), start, end);
                list.Add(l);
                //AddVisualChild(dv);
                //AddLogicalChild(dv);
            }
            return list;
        }

    }
}
