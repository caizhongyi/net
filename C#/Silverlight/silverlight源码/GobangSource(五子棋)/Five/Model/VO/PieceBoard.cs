using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Five.Utils;

namespace Five.Model.VO
{
    public class PieceBoard
    {
        private static PieceBoard _instance;
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static PieceBoard GetInstance()
        {
            if (_instance == null)
                _instance = new PieceBoard();

            return _instance;
        }

        public Piece[,] Pieces;


        /// <summary>
        /// Initializes a new instance of the <see cref="PieceBoard"/> class.
        /// </summary>
        private PieceBoard()
        {
            Init();
        }

        /// <summary>
        /// Inits this pieceboard.
        /// </summary>
        public void Init()
        {
            Pieces = new Piece[AppConfig.PIECEBOARD_WIDTH,AppConfig.PIECEBOARD_WIDTH];
        }
    }
}
