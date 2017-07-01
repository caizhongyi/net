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
using czy.Silverlight.Library;
using czy.Silverlight.StoryBoard;

namespace czy.Silverlight.Controls
{
    public class ShowPictures : Control
    {
        public double MarginLeft
        {
            get { return _marginLeft; }
            set { _marginLeft = value; }
        }
        #region 变量
        Border tempImage;
        int cur = 0;
        int tempCur = 0;
 
        double _offset = 0;        //图片偏移量
        string[] _images;         //图片URL数组
        Size _size;               //图片大小
        double _marginLeft = 50;   //图片间距与
        double _borderRadius = 100;  //边角半径
        List<ObjStoryBoardConfig> configs = new List<ObjStoryBoardConfig>();
        List<Storyboard> moveLeftStoryBoard = new List<Storyboard>();
        List<Storyboard> moveRightStoryBoard = new List<Storyboard>();

        string _imagesRoot = string.Empty;//根目录
        string _classPrefix = string.Empty;//名称共同部分
        int _count = 0;             //图片总数
        string _format = string.Empty;//图片格式
        #endregion

        public ShowPictures()
        {
            this.DefaultStyleKey = typeof(ShowPictures);
        }
      
        public ShowPictures(string[] imagesUrl, Size size)
        {

            this.DefaultStyleKey = typeof(ShowPictures);
            this._images = imagesUrl;
            this._size = size;
            this._count = imagesUrl.Length;
            this._offset = _marginLeft; //间距
        }
        public ShowPictures(string imagesRoot, string classPrefix, int count, string format, Size size)
        {

            this.DefaultStyleKey = typeof(ShowPictures);
            this._imagesRoot = imagesRoot;
            this._classPrefix = classPrefix;
            this._size = size;
            this._count = count;
            this._format = format;
            this._offset = _marginLeft;//间距
        }
        /// <summary>
        /// 载入模版
        /// </summary>
        public override void OnApplyTemplate()
        {
            Init();
       
            MoveInsertObj(moveLeftStoryBoard, moveRightStoryBoard, cur, tempCur, _count);
            ShowStoryBoard(cur, tempCur,configs[0].Obj as Border);
 
        }
        protected void Init()
        {

            if (this._imagesRoot != string.Empty && this._classPrefix != string.Empty)
            { LoadImages(this._imagesRoot, this._classPrefix, this._count, this._format, this._size); }
            else
            {
                this.LoadImages(this._images, this._size);
            }
        }
        /// <summary>
        /// 加载图片
        /// </summary>
        /// <param name="imagesUrl">图片路径集合</param>
        /// <param name="size">图片大小</param>
        protected void LoadImages(string[] imagesUrl, Size size)
        {

            for (int i = 0; i <imagesUrl.Length; i++)
            {
                Image img = new Image();

                img.Load(imagesUrl[i]);

                AddImage(img, i, size, imagesUrl.Length);
     
            }
        }
        /// <summary>
        ///加载图片
        /// </summary>
        /// <param name="imagesRoot"> 图片所在的根目录</param>
        /// <param name="classPrefix">共用名称</param>
        /// <param name="count">数量</param>
        /// <param name="format">格式</param>
        /// <param name="size">大小</param>
        protected void LoadImages(string imagesRoot, string classPrefix, int count, string format, Size size)
        {
            for (int i = 0; i < count ; i++)
            {
                Image img = new Image();

                string url = imagesRoot + "/" + classPrefix + "" + i + "." + format;
                img.Load(url);

                AddImage(img, i, size, count);

            }
        }
        #region 可更改部分
        /// <summary>
        /// 设置位置大小属性
        /// </summary>
        /// <param name="config">属性类</param>
        /// <param name="img">图片</param>
        /// <returns></returns>
        protected ObjStoryBoardConfig SetConfig(ObjStoryBoardConfig config, Border img, int i)
        {
            double x = i * ( _marginLeft);
            double offsetX = x - this._offset;
            

            config.StartOpacity = 0.8;
            config.EndOpacity = 1;
            config.StartPoint = new Point(x,this.Height - this._size.Height);
            config.OffsetStartPoint = new Point(offsetX, this.Height - this._size.Height);
            config.InitStartPoint = new Point(x, this.Height - this._size.Height);
            config.EndPoint = new Point(0, 0);
           
            config.StartSize = this._size;
            config.EndSize = new Size(this.Width, this.Height - this._size.Height);
            config.EndOffsetSize = new Size(this.Width+50, this.Height - this._size.Height+150);
            config.StartProjectionPoint = new ProjectionPoint(-5.225, -61.883, -5.872);
            config.EndProjectionPoint = new ProjectionPoint(0, 0, 0);
            config.Obj = img;
            config.StartBlurEffect = 100;
            config.EndBlurEffect = 0;
            return config;
        }
        /// <summary>
        /// 显示图片
        /// </summary>
        /// <param name="index">当前图片序号</param>
        /// <param name="img">图片对像</param>
        protected void ShowStoryBoard(int index,int tempIndex, Border img)
        {  //放大 
            StoryBoardBuilder.GetZoomAndMoveStoryBoardByCanvas(img, configs[tempIndex].StartPoint, configs[tempIndex].EndPoint, configs[tempIndex].StartSize, configs[index].EndOffsetSize, configs[tempIndex].EndSize).Begin();

            //Storyboard zoom = StoryBoardBuilder.GetMoveStoryBoardByCanvas(img, configs[index].StartPoint, configs[index].EndPoint);
            //zoom.Begin();
            //Storyboard skew = StoryBoardBuilder.GetZoomStoryBoardBySkew(((TransformGroup)img.RenderTransform).Children[1], new Size(1, 1), new Size(20, 20), new Size(21, 21));
            //skew.BeginTime = TimeSpan.FromSeconds(0.5);
            //skew.Begin();
            //Storyboard sb1 = StoryBoardBuilder.GetBlurEffectStoryBoard(img.Effect,  configs[index].StartBlurEffect, configs[index].EndBlurEffect,TimeSpan.FromSeconds(0));
           // sb1.Begin();
            //sb.Completed+=new EventHandler(sb_Completed);

            StoryBoardBuilder.GetOpactiyStoryBoard(img, configs[index].StartOpacity, configs[index].EndOpacity).Begin();
            img.MouseEnter -= new MouseEventHandler(img_MouseEnter);
            img.MouseLeave -= new MouseEventHandler(img_MouseLeave);
            img.MouseLeftButtonUp -= new MouseButtonEventHandler(img_MouseLeftButtonUp);
        }
        
        /// <summary>
        /// 隐藏图片
        /// </summary>
        /// <param name="index">上一个图片序号</param>
        /// <param name="img">上一个图片对像</param>
        protected void HiddenStoryBoard(int tempIndex,int index,Border img)
        {
            //Storyboard zoom = StoryBoardBuilder.GetMoveStoryBoardByCanvas(img, configs[index].EndPoint, configs[index].StartPoint);
            //zoom.Begin();
            //Storyboard skew = StoryBoardBuilder.GetZoomStoryBoardBySkew(((TransformGroup)img.RenderTransform).Children[1], new Size(21, 21), new Size(20, 20), new Size(1, 1));
            //skew.BeginTime = TimeSpan.FromSeconds(0.5);
            //skew.Begin();
            //缩小
            StoryBoardBuilder.GetZoomAndMoveStoryBoardByCanvas(img, configs[tempIndex].EndPoint, configs[tempIndex].StartPoint, configs[tempIndex].EndSize,configs[index].EndOffsetSize, configs[tempIndex].StartSize).Begin();
            StoryBoardBuilder.GetOpactiyStoryBoard(img, configs[tempIndex].EndOpacity, configs[tempIndex].StartOpacity).Begin();
            tempImage.MouseEnter += new MouseEventHandler(img_MouseEnter);
            tempImage.MouseLeave += new MouseEventHandler(img_MouseLeave);
            tempImage.MouseLeftButtonUp += new MouseButtonEventHandler(img_MouseLeftButtonUp);
          
        }
        /// <summary>
        /// 移入效果
        /// </summary>
        /// <param name="index">当前图片序号</param>
        /// <param name="img">图片对像</param>
        protected void MoueEnterStoryBoard(int index,Border img)
        {
            configs[index].CurrentRunStoryBoards.Add(StoryBoardBuilder.GetProjectionStoryBoard(img.Projection, configs[index].StartProjectionPoint, configs[index].EndProjectionPoint));
            configs[index].CurrentRunStoryBoards.Add(StoryBoardBuilder.GetOpactiyStoryBoard(img, configs[index].StartOpacity, 0.5, configs[index].EndOpacity));
            foreach (Storyboard s in configs[index].CurrentRunStoryBoards)
            {
                s.Begin();
            }
        }
        /// <summary>
        /// 移出效果
        /// </summary>
        /// <param name="index">当前图片序号</param>
        /// <param name="img">图片对像</param>
        protected void MoueLeaveStoryBoard(int index, Border img)
        {
            foreach (Storyboard s in configs[index].CurrentRunStoryBoards)
            {
                s.Stop();
            }
            ProjectionPoint p = configs[index].StartProjectionPoint;
            configs[index].CurrentRunStoryBoards.Clear();
            configs[index].CurrentRunStoryBoards.Add(StoryBoardBuilder.GetProjectionStoryBoard(img.Projection, p, configs[index].StartProjectionPoint));
            configs[index].CurrentRunStoryBoards.Add(StoryBoardBuilder.GetOpactiyStoryBoard(img, configs[index].EndOpacity, configs[index].StartOpacity));

            foreach (Storyboard s in configs[index].CurrentRunStoryBoards)
            {
                s.Begin();
            }
        }

        #endregion

        protected void AddImage(Image img, int i, Size size, int count)
        {
            ObjStoryBoardConfig config = new ObjStoryBoardConfig();

            Border b = new Border();
            CornerRadius c = new CornerRadius(_borderRadius);

            TransformGroup tg = new TransformGroup();
            ScaleTransform st = new ScaleTransform();
            SkewTransform skt = new SkewTransform();
            RotateTransform rt = new RotateTransform();
            TranslateTransform tt = new TranslateTransform();
            SetConfig(config, b, i);

            b.CornerRadius = c;

            b.BorderThickness = new Thickness(10);


            System.Windows.Media.Effects.BlurEffect effect = new System.Windows.Media.Effects.BlurEffect();
            effect.Radius = config.EndBlurEffect;
            // b.Effect =effect;
            b.Name = i.ToString();
            b.Cursor = Cursors.Hand;

            PlaneProjection p = new PlaneProjection();
            if (i == 0)
            {
                p.RotationX = 0;
                p.RotationY = 0;
                p.RotationZ = 0;
                tempImage = b;
                tempCur = 0;
            }
            else
            {

                p.RotationX = config.StartProjectionPoint.X;
                p.RotationY = config.StartProjectionPoint.Y;
                p.RotationZ = config.StartProjectionPoint.Z;
            }
            b.Width = config.StartSize.Width;
            b.Height = config.StartSize.Height;
            Canvas.SetLeft(b, config.StartPoint.X);
            Canvas.SetTop(b, config.StartPoint.Y);

         
            b.MouseEnter += new MouseEventHandler(img_MouseEnter);
            b.MouseLeave += new MouseEventHandler(img_MouseLeave);

            b.Projection = p;

            b.MouseLeftButtonUp += new MouseButtonEventHandler(img_MouseLeftButtonUp);

            //}
            tg.Children.Add(st);
            tg.Children.Add(skt);
            tg.Children.Add(rt);
            tg.Children.Add(tt);
            b.RenderTransform = tg;

            configs.Add(config);

            moveLeftStoryBoard.Add(StoryBoardBuilder.GetMoveStoryBoardByCanvas(b, Util.GetCanvasPoint(b), config.OffsetStartPoint));
            moveRightStoryBoard.Add(StoryBoardBuilder.GetMoveStoryBoardByCanvas(b, config.OffsetStartPoint, Util.GetCanvasPoint(b)));

            Canvas canvas = base.GetTemplateChild("ImageCanvas") as Canvas;
            img.Stretch = Stretch.UniformToFill;
            b.Child = img;
            canvas.Children.Add(b);
         

        }
        private void img_MouseEnter(object o, EventArgs e)
        {

            Border img = o as Border;
            int index = Convert.ToInt32(img.Name);
            MoueEnterStoryBoard(index,img);

        }
        private void img_MouseLeave(object o, EventArgs e)
        {
            Border img = o as Border;
            int index = Convert.ToInt32(img.Name);
            MoueLeaveStoryBoard(index, img);
        }
        private void img_MouseLeftButtonUp(object o, MouseEventArgs e)
        {
            Border img = (o as Border);
            cur = Convert.ToInt32(img.Name);
            MoveInsertObj(moveLeftStoryBoard, moveRightStoryBoard, cur, tempCur, _count);
            ShowStoryBoard(cur, tempCur,img);

            //缩小  
            if (tempImage != img)
            {
                HiddenStoryBoard(tempCur, cur, tempImage);
                img_MouseLeave(tempImage, e);
              
            }
           
            tempCur = cur;
            tempImage = img;
        }
        /// <summary>
        /// 一列对像插入与移除一个对像的平移动画
        /// </summary>
        /// <param name="sLeftMove">左移动画</param>
        /// <param name="sRightMove">右移动画</param>
        /// <param name="curIndex">当前index对像</param>
        /// <param name="tempIndex">上一次的index对像</param>
        /// <param name="count">列表总数</param>
        public  void MoveInsertObj(List<Storyboard> sLeftMove, List<Storyboard> sRightMove, int curIndex, int tempIndex, int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (tempIndex < curIndex)
                {
                    if (i >= tempIndex && i < curIndex)
                    {
                        sRightMove[i].Begin();
                        configs[i].StartPoint = configs[i].InitStartPoint;
                    }
                }
                else if ((tempIndex > curIndex))
                {
                    if (i > curIndex && i <= tempIndex)
                    {
                        sLeftMove[i].Begin();
                        configs[i].StartPoint = configs[i].OffsetStartPoint;
                    }
                }
                else
                {
                    if (i > curIndex)
                    {
                        sLeftMove[i].Begin();
                        configs[i].StartPoint = configs[i].OffsetStartPoint;
                    }
                    if (i < curIndex)
                    {
                        sRightMove[i].Begin();
                        configs[i].StartPoint = configs[i].InitStartPoint;
                    }
                }
            }
        }

    }

    public class ObjStoryBoardConfig
    {
        public DependencyObject Obj { get; set; }
        public Point StartPoint { get; set; }
        public Point OffsetStartPoint { get; set; }
        public Point InitStartPoint { get; set; }
        public Point EndPoint { get; set; }
        public Size StartSize { get; set; }
        public Size EndSize { get; set; }
        public Size EndOffsetSize { get; set; }
        public double StartOpacity { get; set; }
        public double EndOpacity { get; set; }
        public ProjectionPoint StartProjectionPoint { get; set; }
        public ProjectionPoint EndProjectionPoint { get; set; }
        public bool State { get; set; }
        public List<Storyboard> CurrentRunStoryBoards { get; set; }
        public double StartBlurEffect { get; set; }
        public double EndBlurEffect { get; set; }
        public ObjStoryBoardConfig()
        {
            if (CurrentRunStoryBoards == null)
            {
                CurrentRunStoryBoards = new List<Storyboard>();
            }
        }
    }

}

