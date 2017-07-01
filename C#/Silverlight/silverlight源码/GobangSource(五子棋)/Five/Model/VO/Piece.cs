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
using Five.Utils;
using Five.View.Components;

namespace Five.Model.VO
{
    public class Piece
    {
        private UserControl _shape;
        private Position _position;

        /// <summary>
        /// Gets or sets the X.
        /// </summary>
        /// <value>The X.</value>
        public int X
        {
            get { return _position.X; }
            set { _position.X = value; }
        }

        /// <summary>
        /// Gets or sets the Y.
        /// </summary>
        /// <value>The Y.</value>
        public int Y
        {
            get { return _position.Y; }
            set { _position.Y = value; }
        }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        /// <value>The position.</value>
        public Position Position
        {
            get { return _position; }
            set { _position = value; }
        }

        public PieceType Type { get; set; }
        public int Index { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Piece"/> class.
        /// </summary>
        public Piece()
        {
            _position = new Position();
        }

        /// <summary>
        /// Gets the shape.
        /// </summary>
        /// <value>The shape.</value>
        public UserControl Shape
        {
            get
            {
                if (_shape == null)
                {
                    if (Type == PieceType.Black)
                        _shape = new BlackPieceComponent();
                    if (Type == PieceType.White)
                        _shape = new WhitePieceComponent();
                }
                return _shape;
            }
        }
    }
}
