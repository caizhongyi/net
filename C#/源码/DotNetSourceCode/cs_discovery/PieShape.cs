/*
 
	C#发现之旅系列教程配套演示代码
	
	本代码仅供学习和参考使用

	编制 袁永福 2008－5－15 
	
	MSN			yyf9989@hotmail.com
	
	QQ			28348092
	
	作者博客	http://xdesigner.cnblogs.com/
	
	使用者请作者的尊重知识产权。

*/
using System;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.Xml ;

namespace cs_discovery
{
	/// <summary>
	/// 单个饼图形状项目
	/// </summary>
	/// <remarks>该类型是PieShape列表的成员类型</remarks>
	[System.Serializable()]
	public class PieShapeItem
	{
		private double dblValue = 0 ;
		/// <summary>
		/// 数值
		/// </summary>
		public double Value
		{
			get{ return dblValue ;}
			set{ dblValue = value;}
		}

		private string strText = null;
		/// <summary>
		/// 对象文本
		/// </summary>
		public string Text
		{
			get{ return strText ;}
			set{ strText = value;}
		}

		private string strLink = null;
		/// <summary>
		/// 项目链接地址
		/// </summary>
		public string Link
		{
			get{ return strLink ;}
			set{ strLink = value;}
		}

		private Color intColor = Color.Black ;
		/// <summary>
		/// 项目颜色
		/// </summary>
		public Color Color
		{
			get{ return intColor ;}
			set{ intColor = value;}
		}

		/// <summary>
		/// 开始角度
		/// </summary>
		internal float StartAngle = 0 ;
		/// <summary>
		/// 结束角度
		/// </summary>
		internal float EndAngle = 0 ;
	}//public class PieShapeItem

	/// <summary>
	/// 平面饼图形状对象
	/// </summary>
	/// <remarks>
	/// 本对象是元素类型为PieShapeItem的列表，并能绘制一个椭圆形的平面饼图
	/// 编制 袁永福 2008-1-10
	/// </remarks>
	[System.Serializable()]
	public class PieShape : System.Collections.CollectionBase
	{
		/// <summary>
		/// 初始化对象
		/// </summary>
		public PieShape( )
		{
		}

		#region 定义对象坐标位置 **********************************************

		private int intLeft = 0 ;
		/// <summary>
		/// 对象左端位置
		/// </summary>
		public int Left
		{
			get{ return intLeft ;}
			set{ intLeft = value;}
		}

		private int intTop = 0 ;
		/// <summary>
		/// 对象顶端位置
		/// </summary>
		public int Top
		{
			get{ return intTop ;}
			set{ intTop = value;}
		}
		private int intWidth = 300 ;
		/// <summary>
		/// 对象宽度
		/// </summary>
		public int Width
		{
			get{ return intWidth ;}
			set{ intWidth = value;}
		}

		private int intHeight = 300 ;
		/// <summary>
		/// 对象高度
		/// </summary>
		public int Height
		{
			get{ return intHeight ;}
			set{ intHeight = value;}
		}
		/// <summary>
		/// 对象边界
		/// </summary>
		public Rectangle Bounds
		{
			get
			{
				return new Rectangle(
					intLeft ,
					intTop , 
					intWidth , 
					intHeight );
			}
			set
			{
				intLeft = value.Left ;
				intTop = value.Top ;
				intWidth = value.Width ;
				intHeight = value.Height ;
			}
		}

		#endregion

		#region 管理饼图项目的代码群 ******************************************

		/// <summary>
		/// 返回指定序号处的项目
		/// </summary>
		public PieShapeItem this[ int index ]
		{
			get{ return ( PieShapeItem) this.List[ index ] ;}
		}

		/// <summary>
		/// 添加一个项目
		/// </summary>
		/// <param name="item">项目对象</param>
		/// <returns>新加的项目在列表中的序号</returns>
		public int Add( PieShapeItem item )
		{
			return this.List.Add( item );
		}

		/// <summary>
		/// 添加一个项目
		/// </summary>
		/// <param name="Value">项目数值</param>
		/// <param name="Text">项目文本</param>
		/// <param name="Link">项目链接地址</param>
		/// <param name="Color">项目颜色值</param>
		/// <returns>新增项目在列表中的序号</returns>
		public int Add( double Value , string Text , string Link , Color Color )
		{
			PieShapeItem item = new PieShapeItem();
			item.Value = Value ;
			item.Text = Text ;
			item.Link = Link ;
			item.Color = Color ;
			return this.List.Add( item );
		}

		/// <summary>
		/// 添加一个项目
		/// </summary>
		/// <param name="Value">项目数值</param>
		/// <param name="Text">项目文本</param>
		/// <param name="Link">项目链接地址</param>
		/// <returns>新增项目在列表中的序号</returns>
		public int Add( double Value , string Text , string Link )
		{
			PieShapeItem item = new PieShapeItem();
			item.Value = Value ;
			item.Text = Text ;
			item.Link = Link ;
			item.Color = StdColors[ this.Count % StdColors.Length ] ;
			return this.List.Add( item );
		}
 
		/// <summary>
		/// 删除一个项目
		/// </summary>
		/// <param name="item">要删除的指定的饼图项目</param>
		public void Remove( PieShapeItem item )
		{
			this.List.Remove( item );
		}

		#endregion

		#region 绘制图形相关 **************************************************

		/// <summary>
		/// 刷新对象状态
		/// </summary>
		/// <remarks>
		/// 本函数中反向遍历所有的饼图项目，
		/// 计算各个饼图项目的起始和终止角度</remarks>
		public void RefreshState()
		{
			double TotalValue = 0 ;
			foreach( PieShapeItem item in this )
			{
				TotalValue += item.Value ;
			}
			float AngleCount = 0 ;
			for( int iCount = this.Count - 1 ; iCount >= 0 ; iCount -- )
			{
				PieShapeItem item = this[ iCount ] ;
				float angle = ( float ) ( 360.0 * item.Value / TotalValue ) ;
				item.StartAngle = ( float ) Math.Round( AngleCount , 3 ) ;
				item.EndAngle = ( float ) Math.Round( AngleCount + angle , 3 ) ;
				AngleCount += angle ;
				item.StartAngle = ( float ) Math.Round( FixAngle( item.StartAngle ) , 3 );
				item.EndAngle = ( float ) Math.Round( FixAngle( item.EndAngle ) , 3 ) ;
			}
		}

		/// <summary>
		/// 创建一个包含对象图形位图对象
		/// </summary>
		/// <returns>创建的位图对象</returns>
		public Bitmap CreateBitmap( )
		{
			Bitmap bmp = new Bitmap( intWidth + 1 , intHeight + 1 ) ;
			using( Graphics g = Graphics.FromImage( bmp ))
			{
				g.Clear( Color.White );
				g.TranslateTransform( intLeft , intTop );
				g.SmoothingMode = SmoothingMode.HighQuality ;
				Draw( g , this.Bounds );
			}
			return bmp ;
		}

		/// <summary>
		/// 绘制饼图图形
		/// </summary>
		/// <param name="g">图形绘制对象</param>
		/// <param name="ClipRectangle">剪切矩形</param>
		public void Draw( Graphics g , Rectangle ClipRectangle )
		{
			foreach( PieShapeItem item in this )
			{
				using( GraphicsPath path = CreatePath( item ))
				{
					using( SolidBrush b = new SolidBrush( item.Color ))
					{
						g.FillPath( b , path );
						g.DrawPath( Pens.Black , path );
					}
				}
			}
		}
		 
		/// <summary>
		/// 为一个饼图项目创建路径对象
		/// </summary>
		/// <param name="item">饼图项目</param>
		/// <returns>创建的路径对象</returns>
		public GraphicsPath CreatePath( PieShapeItem item )
		{
			GraphicsPath path = new GraphicsPath();
			path.AddPie( 
				intLeft ,
				intTop , 
				intWidth ,
				intHeight , 
				item.StartAngle , 
				item.EndAngle - item.StartAngle );
			return path ;
		}

		#endregion

		/// <summary>
		/// 创建用于显示饼图图片和超链接的HTML代码字符串
		/// </summary>
		/// <param name="imgsrc">图片地址</param>
		/// <returns>创建的HTML字符串</returns>
		/// <remarks>
		/// 此处没有简单拼凑HTML字符串，而是利用XML和HTML的相似性
		/// 使用一个XmlTextWriter来生成HTML字符串。
		/// </remarks>
		public string GetHtmlString( string imgsrc )
		{
			if( this.Count == 0 )
				return "";
			// 生成唯一的　map 元素名称
			string name = System.Guid.NewGuid().ToString("N");
			// 生成　XmlTextWriter 对象
			System.IO.StringWriter myStr = new System.IO.StringWriter();
			System.Xml.XmlTextWriter writer = new XmlTextWriter( myStr );
			writer.IndentChar = ' ' ;
			writer.Indentation = 3 ;
			writer.Formatting = System.Xml.Formatting.Indented ;
			// 开始输出HTML
			writer.WriteStartDocument();
			// 输出图片元素
			writer.WriteRaw("\r\n<img src='" + imgsrc + "' usemap='#" + name + "' border='0'/>");
			// 输出 map 元素
			writer.WriteStartElement("map");
			writer.WriteAttributeString("name" , name );
			foreach( PieShapeItem item in this )
			{
				// 输出超链接区域
				Point[] ps = this.GetPoints( item );
				writer.WriteStartElement("area");
				writer.WriteAttributeString("shape" , "poly");
				writer.WriteStartAttribute("coords" , null );
				for( int iCount = 0 ; iCount < ps.Length ; iCount ++ )
				{
					writer.WriteString( ps[ iCount ].X.ToString() );
					writer.WriteString("," );
					writer.WriteString( ps[ iCount ].Y.ToString() );
					writer.WriteString("," );
				}
				writer.WriteEndAttribute();
				if( item.Link != null && item.Link.Length > 0 )
				{
					writer.WriteAttributeString("href" , item.Link );
				}
				writer.WriteAttributeString("title" , item.Text );
				writer.WriteEndElement();
			}
			writer.WriteEndElement();
			writer.WriteEndDocument();
			writer.Close();
			string html = myStr.ToString();
			// 修正输出的HTML字符串
			int index = html.LastIndexOf("?>");
			if( index > 0 )
			{
				html = html.Substring( index + 2 );
			}
			return html ;
		}

		#region 内部代码 ******************************************************

		/// <summary>
		/// 获得包围饼图区域的点坐标数组
		/// </summary>
		/// <param name="item">饼图项目</param>
		/// <returns>点坐标数组</returns>
		private Point[] GetPoints( PieShapeItem item )
		{
			GraphicsPath path = CreatePath( item );
			path.Flatten();
			PointF[] ps = path.PathPoints ;
			path.Dispose();
			Point[] ps2 = new Point[ ps.Length ] ;
			for( int iCount = 0 ; iCount < ps.Length ; iCount ++ )
			{
				ps2[ iCount ].X = ( int ) ( ps[ iCount ].X );
				ps2[ iCount ].Y = ( int ) ( ps[ iCount ].Y );
			}
			return ps2 ;
		}

		/// <summary>
		/// 根据椭圆形状修正角度
		/// </summary>
		/// <param name="angle">原始角度</param>
		/// <returns>修正后的角度值</returns>
		private float FixAngle( float angle )
		{
			if( ( angle % 90.0 ) == 0 )
				return angle ;
			if( intWidth == intHeight )
				return angle ;
			double x = intWidth * Math.Cos( angle * Math.PI / 180 );
			double y = intHeight * Math.Sin( angle * Math.PI / 180 );
			float result = ( float ) ( Math.Atan2( y , x ) * 180 / Math.PI );
			if( result < 0 )
				result += 360 ;
			return result ;
		}

		/// <summary>
		/// 标准颜色列表
		/// </summary>
		private static Color[] StdColors = new Color[]{
														  Color.Purple,
														  Color.Red,
														  Color.Green,
														  Color.Blue,
														  Color.Yellow,
														  Color.Olive,
														  Color.Navy,
														  Color.Aqua,
														  Color.Lime,
														  Color.Maroon,
														  Color.Teal,
														  Color.Fuchsia
													  };

		#endregion

	}//public class PieShape : System.Collections.CollectionBase
}