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
using PureMVC.Patterns;

namespace Five.Model
{
    public class ApplicationProxy:Proxy
    {
        public new static string NAME = "ApplicationProxy";
        public bool IsHuman;
        public bool VSCom;

        private Dictionary<string, int> _stastics;

        

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationProxy"/> class.
        /// </summary>
        public ApplicationProxy()
            : base(NAME)
        {
            _stastics = new Dictionary<string, int>
                            {
                                {"BlackWins", 0},
                                {"WhiteWins", 0},
                                {"Draw", 0},
                                {"Total", 0}
                            };
        }

        /// <summary>
        /// Loads the config.
        /// </summary>
        public void LoadConfig()
        {
            IsHuman = AppConfig.HUMAN_FIRST;
            VSCom = AppConfig.HAS_COMPUTER_PLAYER;
        }

        /// <summary>
        /// Updates the stastics.
        /// </summary>
        /// <param name="type">The type.</param>
        public void UpdateStastics(PieceType type)
        {
            if (type == PieceType.Black)
                _stastics["BlackWins"]++;

            if (type == PieceType.White)
                _stastics["WhiteWins"]++;

            if (type == PieceType.Draw)
                _stastics["Draw"]++;

            _stastics["Total"]++;
        }

        /// <summary>
        /// Resets the stastics.
        /// </summary>
        public void ResetStastics()
        {
            _stastics["BlackWins"] = 0;
            _stastics["WhiteWins"] = 0;
            _stastics["Draw"] = 0;
            _stastics["Total"] = 0;
        }

        /// <summary>
        /// Gets the black wins count.
        /// </summary>
        /// <value>The black wins count.</value>
        public int BlackWinsCount
        {
            get { return _stastics["BlackWins"]; }
        }

        /// <summary>
        /// Gets the white wins count.
        /// </summary>
        /// <value>The white wins count.</value>
        public int WhiteWinsCount
        {
            get { return _stastics["WhiteWins"]; }
        }

        /// <summary>
        /// Gets the draw count.
        /// </summary>
        /// <value>The draw count.</value>
        public int DrawCount
        {
            get { return _stastics["Draw"]; }
        }

        /// <summary>
        /// Gets the total count.
        /// </summary>
        /// <value>The total count.</value>
        public int TotalCount
        {
            get { return _stastics["Total"]; }
        }



    }
}
