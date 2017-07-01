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
using Five.Model.VO;
using Five.Utils;
using Five.View.Components;
using PureMVC.Patterns;

namespace Five.Model
{
    public class PieceboardProxy : Proxy
    {
        public new const string NAME = "PieceboardProxy";

        public Piece[,] Pieces;

        private List<WinningResult> _results;

        /// <summary>
        /// Initializes a new instance of the <see cref="PieceboardProxy"/> class.
        /// </summary>
        public PieceboardProxy()
            : base(NAME)
        {
            Pieces = new Piece[AppConfig.PIECEBOARD_WIDTH, AppConfig.PIECEBOARD_WIDTH];
            _results = new List<WinningResult>();
        }

        /// <summary>
        /// Checks the blank available.
        /// </summary>
        /// <param name="piece">The piece.</param>
        /// <returns></returns>
        public bool CheckBlankAvailable(Piece piece)
        {
            if (Pieces[piece.X, piece.Y] == null)
                return true;

            SendNotification(Notifications.INVALID_POSITION);
            return false;

        }

        /// <summary>
        /// Adds the piece.
        /// </summary>
        /// <param name="piece">The piece.</param>
        public void AddPiece(Piece piece)
        {
            if (CheckBlankAvailable(piece))
            {
                SendNotification(Notifications.PIECE_ADDED_TO_DATA, piece);
            }
        }

        /// <summary>
        /// Checks the winer.
        /// </summary>
        /// <param name="piece">The piece.</param>
        public void CheckWiner(Piece piece)
        {
            _results.Clear();

            Pieces[piece.X, piece.Y] = piece;

            if (CheckAlgorithm(piece.X - 4, piece.Y - 4, 1, 1, 9, piece.Type)
                || CheckAlgorithm(piece.X - 4, piece.Y, 1, 0, 9, piece.Type)
                || CheckAlgorithm(piece.X, piece.Y - 4, 0, 1, 9, piece.Type)
                || CheckAlgorithm(piece.X - 4, piece.Y + 4, 1, -1, 9, piece.Type))
            {
                SendNotification(Notifications.PLAYER_WIN, _results);
            }
            else
            {
                SendNotification(Notifications.CONTINUE);
            }
        }

        /// <summary>
        /// Checks the algorithm.
        /// </summary>
        /// <param name="startX">The start X.</param>
        /// <param name="startY">The start Y.</param>
        /// <param name="stepX">The x step.</param>
        /// <param name="stepY">The y step.</param>
        /// <param name="stepCount">The step count.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private bool CheckAlgorithm(int startX, int startY, int stepX, int stepY, int stepCount, PieceType type)
        {
            WinningResult result=new WinningResult();

            List<Piece> inlinePieces = new List<Piece>();

            for (int i = 0; i < stepCount; i++)
            {
                int x = startX + stepX*i;
                int y = startY + stepY*i;

                if (x >= 0 && y >= 0 && x < AppConfig.PIECEBOARD_WIDTH && y < AppConfig.PIECEBOARD_WIDTH)
                {
                    if (Pieces[startX + stepX * i, startY + stepY * i] != null &&
                        Pieces[startX + stepX * i, startY + stepY * i].Type == type)
                        inlinePieces.Add(Pieces[startX + stepX * i, startY + stepY * i]);
                    else
                       inlinePieces.Clear();

                    if (inlinePieces.Count >= 5)
                    {
                        result.InlinePieces = inlinePieces;
                        result.PieceType = type;

                        if (stepX == 1 && stepY == 1)
                            result.WinningType = WinningType.Forward;
                        if (stepX == 1 && stepY == 0)
                            result.WinningType = WinningType.Horizon;
                        if (stepX == 0 && stepY == 1)
                            result.WinningType = WinningType.Vertical;
                        if (stepX == 1 && stepY == -1)
                            result.WinningType = WinningType.Backward;

                        _results.Add(result);

                        return true;
                    }
                }
            }
            

            return false;
        }

        /// <summary>
        /// Clears the piece.
        /// </summary>
        public void ClearPiece()
        {
            Pieces = new Piece[AppConfig.PIECEBOARD_WIDTH, AppConfig.PIECEBOARD_WIDTH];
        }

    }
}
