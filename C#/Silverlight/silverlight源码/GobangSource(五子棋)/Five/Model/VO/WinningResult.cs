using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Five.Model.VO
{
    public enum WinningType
    {
        Forward, Backward, Horizon, Vertical
    }

    public class WinningResult
    {
        public List<Piece> InlinePieces;
        public WinningType WinningType;
        public PieceType PieceType;

        public WinningResult()
        {
            InlinePieces = new List<Piece>();
        }
    }
}
