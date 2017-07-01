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

namespace czy.Silverlight.Library
{
    /// <summary>
    /// 失量座标
    /// </summary>
    public class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Vector(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector()
            : this(0.0d, 0.0d)
        {
        }

        #region  Vector Operators

        public static Vector operator +(Vector v1, Vector v2)
        {
            return new Vector(v1.X + v2.X, v1.Y + v2.Y);
        }
        public static Vector operator +(Vector v1, Point p)
        {
            return new Vector(v1.X + p.X, v1.Y + p.Y);
        }
        public static Vector operator -(Vector v1, Vector v2)
        {
            return new Vector(v1.X - v2.X, v1.Y - v2.Y);
        }
        public static Vector operator -(Vector v1, Point p)
        {
            return new Vector(v1.X - p.X, v1.Y - p.Y);
        }
        public static Vector operator /(Vector v1, double d)
        {
            return new Vector(v1.X / d, v1.Y / d);
        }
        public static Vector operator *(Vector v1, double m)
        {
            return new Vector(v1.X * m, v1.Y * m);
        }

        #endregion

        public double Length
        {
            get { return Math.Sqrt(this.X * this.X + this.Y * this.Y); }
        }

        public void Normalize()
        {
            double length = this.Length;
            this.X /= length;
            this.Y /= length;
        }
    }
}
