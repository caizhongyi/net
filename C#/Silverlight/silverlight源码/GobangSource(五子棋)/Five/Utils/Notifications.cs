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

namespace Five.Utils
{
    public sealed class Notifications
    {
        public const string APP_STARTUP = "applicationstartup";

        public const string ADD_PIECE = "AddPiece";
        public const string RESTART = "Restart";
        public const string CHECK_WINER = "CheckWiner";

        public const string INVALID_POSITION = "invalidPosition";

        public const string PIECE_ADDED_TO_DATA = "pieceAddedToData";
        public const string PLAYER_WIN = "playerWin";
        public const string BOARD_FULL = "boardFull";
        public const string PIECE_EXISTED = "pieceExisted";

        public const string CONTINUE = "continue";
        public const string COMPUTER_PLAY = "computerPlay";

        public const string MODECHANGE_PVP = "modeChangePVP";
        public const string MODECHANGE_PVE = "modeChangePVE";
        public const string MODECHANGE_EVP = "modeChangeEVP";

        public const string GAME_START = "GameStart";
        public const string GAME_END = "GameEnd";

        public const string START_SELECT_MODE = "StartSelectMode";
    }
}
