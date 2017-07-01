/*
 
	C#����֮��ϵ�н̳�������ʾ����
	
	���������ѧϰ�Ͳο�ʹ��

	���� Ԭ���� 2008��5��15 
	
	MSN			yyf9989@hotmail.com
	
	QQ			28348092
	
	���߲���	http://xdesigner.cnblogs.com/
	
	ʹ���������ߵ�����֪ʶ��Ȩ��

*/
using System;
using System.Drawing ;
using System.Drawing.Drawing2D ;
using System.Xml ;

namespace cs_discovery
{
	/// <summary>
	/// ������ͼ��״��Ŀ
	/// </summary>
	/// <remarks>��������PieShape�б�ĳ�Ա����</remarks>
	[System.Serializable()]
	public class PieShapeItem
	{
		private double dblValue = 0 ;
		/// <summary>
		/// ��ֵ
		/// </summary>
		public double Value
		{
			get{ return dblValue ;}
			set{ dblValue = value;}
		}

		private string strText = null;
		/// <summary>
		/// �����ı�
		/// </summary>
		public string Text
		{
			get{ return strText ;}
			set{ strText = value;}
		}

		private string strLink = null;
		/// <summary>
		/// ��Ŀ���ӵ�ַ
		/// </summary>
		public string Link
		{
			get{ return strLink ;}
			set{ strLink = value;}
		}

		private Color intColor = Color.Black ;
		/// <summary>
		/// ��Ŀ��ɫ
		/// </summary>
		public Color Color
		{
			get{ return intColor ;}
			set{ intColor = value;}
		}

		/// <summary>
		/// ��ʼ�Ƕ�
		/// </summary>
		internal float StartAngle = 0 ;
		/// <summary>
		/// �����Ƕ�
		/// </summary>
		internal float EndAngle = 0 ;
	}//public class PieShapeItem

	/// <summary>
	/// ƽ���ͼ��״����
	/// </summary>
	/// <remarks>
	/// ��������Ԫ������ΪPieShapeItem���б����ܻ���һ����Բ�ε�ƽ���ͼ
	/// ���� Ԭ���� 2008-1-10
	/// </remarks>
	[System.Serializable()]
	public class PieShape : System.Collections.CollectionBase
	{
		/// <summary>
		/// ��ʼ������
		/// </summary>
		public PieShape( )
		{
		}

		#region �����������λ�� **********************************************

		private int intLeft = 0 ;
		/// <summary>
		/// �������λ��
		/// </summary>
		public int Left
		{
			get{ return intLeft ;}
			set{ intLeft = value;}
		}

		private int intTop = 0 ;
		/// <summary>
		/// ���󶥶�λ��
		/// </summary>
		public int Top
		{
			get{ return intTop ;}
			set{ intTop = value;}
		}
		private int intWidth = 300 ;
		/// <summary>
		/// ������
		/// </summary>
		public int Width
		{
			get{ return intWidth ;}
			set{ intWidth = value;}
		}

		private int intHeight = 300 ;
		/// <summary>
		/// ����߶�
		/// </summary>
		public int Height
		{
			get{ return intHeight ;}
			set{ intHeight = value;}
		}
		/// <summary>
		/// ����߽�
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

		#region �����ͼ��Ŀ�Ĵ���Ⱥ ******************************************

		/// <summary>
		/// ����ָ����Ŵ�����Ŀ
		/// </summary>
		public PieShapeItem this[ int index ]
		{
			get{ return ( PieShapeItem) this.List[ index ] ;}
		}

		/// <summary>
		/// ���һ����Ŀ
		/// </summary>
		/// <param name="item">��Ŀ����</param>
		/// <returns>�¼ӵ���Ŀ���б��е����</returns>
		public int Add( PieShapeItem item )
		{
			return this.List.Add( item );
		}

		/// <summary>
		/// ���һ����Ŀ
		/// </summary>
		/// <param name="Value">��Ŀ��ֵ</param>
		/// <param name="Text">��Ŀ�ı�</param>
		/// <param name="Link">��Ŀ���ӵ�ַ</param>
		/// <param name="Color">��Ŀ��ɫֵ</param>
		/// <returns>������Ŀ���б��е����</returns>
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
		/// ���һ����Ŀ
		/// </summary>
		/// <param name="Value">��Ŀ��ֵ</param>
		/// <param name="Text">��Ŀ�ı�</param>
		/// <param name="Link">��Ŀ���ӵ�ַ</param>
		/// <returns>������Ŀ���б��е����</returns>
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
		/// ɾ��һ����Ŀ
		/// </summary>
		/// <param name="item">Ҫɾ����ָ���ı�ͼ��Ŀ</param>
		public void Remove( PieShapeItem item )
		{
			this.List.Remove( item );
		}

		#endregion

		#region ����ͼ����� **************************************************

		/// <summary>
		/// ˢ�¶���״̬
		/// </summary>
		/// <remarks>
		/// �������з���������еı�ͼ��Ŀ��
		/// ���������ͼ��Ŀ����ʼ����ֹ�Ƕ�</remarks>
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
		/// ����һ����������ͼ��λͼ����
		/// </summary>
		/// <returns>������λͼ����</returns>
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
		/// ���Ʊ�ͼͼ��
		/// </summary>
		/// <param name="g">ͼ�λ��ƶ���</param>
		/// <param name="ClipRectangle">���о���</param>
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
		/// Ϊһ����ͼ��Ŀ����·������
		/// </summary>
		/// <param name="item">��ͼ��Ŀ</param>
		/// <returns>������·������</returns>
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
		/// ����������ʾ��ͼͼƬ�ͳ����ӵ�HTML�����ַ���
		/// </summary>
		/// <param name="imgsrc">ͼƬ��ַ</param>
		/// <returns>������HTML�ַ���</returns>
		/// <remarks>
		/// �˴�û�м�ƴ��HTML�ַ�������������XML��HTML��������
		/// ʹ��һ��XmlTextWriter������HTML�ַ�����
		/// </remarks>
		public string GetHtmlString( string imgsrc )
		{
			if( this.Count == 0 )
				return "";
			// ����Ψһ�ġ�map Ԫ������
			string name = System.Guid.NewGuid().ToString("N");
			// ���ɡ�XmlTextWriter ����
			System.IO.StringWriter myStr = new System.IO.StringWriter();
			System.Xml.XmlTextWriter writer = new XmlTextWriter( myStr );
			writer.IndentChar = ' ' ;
			writer.Indentation = 3 ;
			writer.Formatting = System.Xml.Formatting.Indented ;
			// ��ʼ���HTML
			writer.WriteStartDocument();
			// ���ͼƬԪ��
			writer.WriteRaw("\r\n<img src='" + imgsrc + "' usemap='#" + name + "' border='0'/>");
			// ��� map Ԫ��
			writer.WriteStartElement("map");
			writer.WriteAttributeString("name" , name );
			foreach( PieShapeItem item in this )
			{
				// �������������
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
			// ���������HTML�ַ���
			int index = html.LastIndexOf("?>");
			if( index > 0 )
			{
				html = html.Substring( index + 2 );
			}
			return html ;
		}

		#region �ڲ����� ******************************************************

		/// <summary>
		/// ��ð�Χ��ͼ����ĵ���������
		/// </summary>
		/// <param name="item">��ͼ��Ŀ</param>
		/// <returns>����������</returns>
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
		/// ������Բ��״�����Ƕ�
		/// </summary>
		/// <param name="angle">ԭʼ�Ƕ�</param>
		/// <returns>������ĽǶ�ֵ</returns>
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
		/// ��׼��ɫ�б�
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