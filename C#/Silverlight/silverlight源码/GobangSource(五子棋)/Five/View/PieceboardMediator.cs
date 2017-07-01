using System;
using System.Net;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Five.Model;
using Five.Model.VO;
using Five.Utils;
using Five.View.Components;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace Five.View
{
    public class PieceboardMediator : Mediator
    {
        public new const string NAME = "PieceboardMediator";
        private PieceboardProxy _pieceboardProxy;
        private ApplicationProxy _applicationProxy;
        private bool _isBlack = true;
        private UIElement _currentPiece;
        private bool _clickable;

        /// <summary>
        /// Sets a value indicating whether this <see cref="PieceboardMediator"/> is clickable.
        /// </summary>
        /// <value><c>true</c> if clickable; otherwise, <c>false</c>.</value>
        public bool Clickable
        {
            set
            {
                _clickable = value;
            }
        }

        /// <summary>
        /// Gets the pieceboard component.
        /// </summary>
        /// <value>The pieceboard component.</value>
        public PieceboardComponent PieceboardComponent
        {
            get { return ViewComponent as PieceboardComponent; }
        }

        /// <summary>
        /// List the <c>INotification</c> names this <c>Mediator</c> is interested in being notified of
        /// </summary>
        /// <returns>The list of <c>INotification</c> names</returns>
        public override IList<string> ListNotificationInterests()
        {
            return new List<string>(new[]
                                        {
                                            Notifications.PIECE_ADDED_TO_DATA,
                                            Notifications.CHECK_WINER,
                                            Notifications.PLAYER_WIN,
                                            Notifications.CONTINUE,
                                            Notifications.INVALID_POSITION
                                        });
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PieceboardMediator"/> class.
        /// </summary>
        /// <param name="viewComponent">The view component.</param>
        public PieceboardMediator(object viewComponent)
            : base(NAME, viewComponent)
        {
            _pieceboardProxy = Facade.RetrieveProxy(PieceboardProxy.NAME) as PieceboardProxy;
            _applicationProxy = Facade.RetrieveProxy(ApplicationProxy.NAME) as ApplicationProxy;

            PieceboardComponent.PieceboardClick += Pieceboard_Click;

            var pieceGrid = new Grid();
            pieceGrid.Name = "PieceGrid";
            PieceboardComponent.PieceboardGrid.Children.Add(pieceGrid);

            InitCursor();

            PieceboardComponent.PieceboardGridMouseMove += PieceboardComponent_PieceboardGridMouseMove;
            PieceboardComponent.PieceboardGridMouseEnter += PieceboardComponent_PieceboardGridMouseEnter;
            PieceboardComponent.PieceboardGridMouseLeave += PieceboardComponent_PieceboardGridMouseLeave;

        }

        /// <summary>
        /// Handles the PieceboardGridMouseLeave event of the PieceboardComponent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        void PieceboardComponent_PieceboardGridMouseLeave(object sender, MouseEventArgs e)
        {
            _currentPiece.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Handles the PieceboardGridMouseEnter event of the PieceboardComponent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        void PieceboardComponent_PieceboardGridMouseEnter(object sender, MouseEventArgs e)
        {
            _currentPiece.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the PieceboardGridMouseMove event of the PieceboardComponent control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseEventArgs"/> instance containing the event data.</param>
        void PieceboardComponent_PieceboardGridMouseMove(object sender, MouseEventArgs e)
        {
            if (!PieceboardComponent.CursorContainer.Children.Contains(_currentPiece))
                PieceboardComponent.CursorContainer.Children.Add(_currentPiece);

            _currentPiece.SetValue(Canvas.LeftProperty,
                                   e.GetPosition((UIElement) sender).X - _currentPiece.RenderSize.Width/2);
            _currentPiece.SetValue(Canvas.TopProperty,
                                   e.GetPosition((UIElement) sender).Y - _currentPiece.RenderSize.Height/2);
        }

        /// <summary>
        /// Handle <c>INotification</c>s
        /// </summary>
        /// <param name="notification">The <c>INotification</c> instance to handle</param>
        /// <remarks>
        /// Typically this will be handled in a switch statement, with one 'case' entry per <c>INotification</c> the <c>Mediator</c> is interested in.
        /// </remarks>
        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case Notifications.PIECE_ADDED_TO_DATA:
                    var piece = (Piece) notification.Body;
                    AddPieceToBoard(piece);
                    break;

                case Notifications.CHECK_WINER:
                    var checkPiece = (Piece) notification.Body;
                    _pieceboardProxy.CheckWiner(checkPiece);
                    break;

                case Notifications.PLAYER_WIN:

                    MarkPieces((List<WinningResult>) notification.Body);
                    _clickable = false;
                    break;

                case Notifications.CONTINUE:

                    if (AppConfig.HAS_COMPUTER_PLAYER)
                    {
                        if (_applicationProxy.IsHuman)
                        {
                            _applicationProxy.IsHuman = false;
                            ComputerPlay();
                        }
                        else
                        {
                            _applicationProxy.IsHuman = true;
                            _clickable = true;
                        }
                    }
                    else
                    {
                        _clickable = true;
                    }

                    break;

                case Notifications.INVALID_POSITION:

                    _clickable = true;

                    break;
            }
        }

        /// <summary>
        /// Adds the piece to board.
        /// </summary>
        /// <param name="piece">The piece.</param>
        private void AddPieceToBoard(Piece piece)
        {
            var point = new Point(piece.X*30 - 9, piece.Y*30 - 9);

            piece.Shape.SetValue(Canvas.LeftProperty, point.X);
            piece.Shape.SetValue(Canvas.TopProperty, point.Y);

            PieceboardComponent.PieceContainer.Children.Add(piece.Shape);

            _isBlack = piece.Type != PieceType.Black;

            PieceboardComponent.CursorContainer.Children.Remove(_currentPiece);
            InitCursor();

            SendNotification(Notifications.CHECK_WINER, piece);
        }

        /// <summary>
        /// Marks the pieces.
        /// </summary>
        /// <param name="results">The results.</param>
        private void MarkPieces(List<WinningResult> results)
        {
            foreach (var result in results)
            {
                foreach (var piece in result.InlinePieces)
                {
                    if (piece.Type == PieceType.Black)
                    {
                        ((BlackPieceComponent)piece.Shape).Highlight.RepeatBehavior = RepeatBehavior.Forever;
                        ((BlackPieceComponent)piece.Shape).Highlight.Begin();
                    }
                    else
                    {
                        ((WhitePieceComponent)piece.Shape).Highlight.RepeatBehavior = RepeatBehavior.Forever;
                        ((WhitePieceComponent)piece.Shape).Highlight.Begin();
                    }
                }
            }

            
        }


        /// <summary>
        /// Pieceboard_s the click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="point">The point.</param>
        protected void Pieceboard_Click(object sender, Point point)
        {
            if (_clickable && _applicationProxy.IsHuman)
            {

                _clickable = false;

                var x = (int) Math.Floor(point.X/30);
                var y = (int) Math.Floor(point.Y/30);

                if ((point.X%30 > 10 && point.X%30 < 20) || point.Y%30 > 10 && point.Y%30 < 20)
                {
                    SendNotification(Notifications.INVALID_POSITION);
                    return;
                }

                if (point.Y%30 > 20)
                    y++;
                if (point.X%30 > 20)
                    x++;

                var newPiece = new Piece
                                   {
                                       X = x,
                                       Y = y,
                                       Type = (_isBlack ? PieceType.Black : PieceType.White)
                                   };

                _pieceboardProxy.AddPiece(newPiece);
              
            }
        }

        /// <summary>
        /// Inits the cursor.
        /// </summary>
        public void InitCursor()
        {
            if (_isBlack)
                _currentPiece = new BlackPieceComponent();
            else
                _currentPiece = new WhitePieceComponent();
        }

        /// <summary>
        /// Computers the play.
        /// </summary>
        public void ComputerPlay()
        {
            int m = 0, n = 0;
            int err = 0;

            int[,] board = new int[AppConfig.PIECEBOARD_WIDTH, AppConfig.PIECEBOARD_WIDTH];

            for (int i = 0; i < 15; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    if (_pieceboardProxy.Pieces[i, j] == null)
                        board[i, j] = -1;
                    else
                        board[i, j] = _pieceboardProxy.Pieces[i, j].Type == PieceType.Black ? 0 : 1;
                }
            }


            do
            {
                AI.GetInstance().Down(board);
                m = AI.GetInstance().X;
                n = AI.GetInstance().Y;
                err++;
                if (err > 100)
                {
                    SendNotification(Notifications.GAME_START);
                    return;
                }
            } while (Rules.Exit(m, n, board));

            Piece piece=new Piece();
            piece.X = m;
            piece.Y = n;
            piece.Type = (_isBlack ? PieceType.Black : PieceType.White);

            _pieceboardProxy.AddPiece(piece);
            
        }

        public void InitPieceBoard()
        {
            _isBlack = true;
            if(AppConfig.HUMAN_FIRST || !AppConfig.HAS_COMPUTER_PLAYER)
                _clickable = true;
            
            InitCursor();
            PieceboardComponent.PieceContainer.Children.Clear();
            PieceboardComponent.MaskCanvas.Visibility = Visibility.Collapsed;
        }
    }
}
