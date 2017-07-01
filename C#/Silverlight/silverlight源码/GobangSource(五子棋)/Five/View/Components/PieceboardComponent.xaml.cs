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

namespace Five.View.Components
{
    public partial class PieceboardComponent : UserControl
    {
        public delegate void PieceboardClickHandler(object sender, Point point);

        public delegate void PieceboardGridMouseEnterHandler(object sender, MouseEventArgs e);

        public delegate void PieceboardGridMouseLeaveHandler(object sender, MouseEventArgs e);

        public delegate void PieceboardGridMouseMoveHandler(object sender, MouseEventArgs e);
        

        public event PieceboardClickHandler PieceboardClick;
        public event PieceboardGridMouseEnterHandler PieceboardGridMouseEnter;
        public event PieceboardGridMouseLeaveHandler PieceboardGridMouseLeave;
        public event PieceboardGridMouseMoveHandler PieceboardGridMouseMove;

        /// <summary>
        /// Initializes a new instance of the <see cref="PieceboardComponent"/> class.
        /// </summary>
        public PieceboardComponent()
        {
            InitializeComponent();
            PieceboardGrid.MouseLeftButtonUp += PieceboardGrid_MouseLeftButtonUp;
            PieceboardGrid.MouseMove += PieceboardGrid_MouseMove;

            PieceboardGrid.MouseEnter += new MouseEventHandler(PieceboardGrid_MouseEnter);
            PieceboardGrid.MouseLeave += new MouseEventHandler(PieceboardGrid_MouseLeave);
            PieceboardGrid.Cursor = Cursors.None;
        }

        /// <summary>
        /// Handles the MouseLeave event of the PieceboardGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        void PieceboardGrid_MouseLeave(object sender, MouseEventArgs e)
        {
            PieceboardGridMouseLeave(sender, e);
        }

        /// <summary>
        /// Handles the MouseEnter event of the PieceboardGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        void PieceboardGrid_MouseEnter(object sender, MouseEventArgs e)
        {
            PieceboardGridMouseEnter(sender, e);
        }

        /// <summary>
        /// Handles the MouseMove event of the PieceboardGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        void PieceboardGrid_MouseMove(object sender, MouseEventArgs e)
        {
            PieceboardGridMouseMove(sender, e);
        }

        /// <summary>
        /// Handles the MouseLeftButtonUp event of the PieceboardGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void PieceboardGrid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Point point = e.GetPosition(sender as Canvas);
            PieceboardClick(this, point);
            PieceboardGrid.CaptureMouse();
        }
    }
}
