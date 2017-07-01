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
using Five.Model;
using Five.Model.VO;
using Five.Utils;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace Five.View
{
    public class ApplicationMediator : Mediator
    {
        public new const string NAME = "ApplicationMediator";

        private PieceboardProxy _pieceboardProxy;
        private ApplicationProxy _applicationProxy;

        /// <summary>
        /// Gets the application.
        /// </summary>
        /// <value>The application.</value>
        public Page Application
        {
            get
            {
                return ViewComponent as Page;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationMediator"/> class.
        /// </summary>
        /// <param name="viewComponent">The view component.</param>
        public ApplicationMediator(object viewComponent)
            : base(NAME, viewComponent)
        {
            Facade.RegisterMediator(new PieceboardMediator(Application.Pieceboard));
            Facade.RegisterMediator(new RestartPanelMediator(Application.RestartPanel));
            Facade.RegisterMediator(new ModeSelectPanelMediator(Application.ModeSelectPanel));

            _pieceboardProxy = Facade.RetrieveProxy(PieceboardProxy.NAME) as PieceboardProxy;
            _applicationProxy = Facade.RetrieveProxy(ApplicationProxy.NAME) as ApplicationProxy;

            UpdateStastics();

            Application.Shadow.Wind.RepeatBehavior = RepeatBehavior.Forever;
            Application.Shadow.Wind.Begin();

            Application.StartModeSelectClick += StartSelectModel_ButtonClick;

        }

        /// <summary>
        /// Handles the ButtonClick event of the StartSelectModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
         void StartSelectModel_ButtonClick(object sender, RoutedEventArgs e)
         {
             SendNotification(Notifications.START_SELECT_MODE);
         }

        /// <summary>
        /// List the <c>INotification</c> names this <c>Mediator</c> is interested in being notified of
        /// </summary>
        /// <returns>The list of <c>INotification</c> names</returns>
        public override IList<string> ListNotificationInterests()
        {
            return new List<string>(new[]
                                        {
                                            Notifications.ADD_PIECE,
                                            Notifications.PLAYER_WIN,
                                            Notifications.RESTART,
                                            Notifications.MODECHANGE_PVP,
                                            Notifications.MODECHANGE_PVE,
                                            Notifications.MODECHANGE_EVP
                                        });
            
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
                case Notifications.ADD_PIECE:

                    _pieceboardProxy.AddPiece(notification.Body as Piece);

                    break;

                case Notifications.PLAYER_WIN:

                    var type = ((List<WinningResult>) notification.Body)[0].PieceType;

                    _applicationProxy.UpdateStastics(type);
                    Application.RestartPanel.Visibility = Visibility.Visible;
                    Application.RestartPanel.FadeIn.Begin();
                    UpdateStastics();
                    break;

                case Notifications.RESTART:
                    Application.RestartPanel.FadeOut.Begin();
                    SendNotification(Notifications.GAME_START);
                    break;

                case Notifications.MODECHANGE_PVP:
                    _applicationProxy.ResetStastics();
                    UpdateStastics();
                    AppConfig.HUMAN_FIRST = true;
                    AppConfig.HAS_COMPUTER_PLAYER = false;
                    Application.ModeSelectPanel.Visibility = Visibility.Collapsed;

                    SendNotification(Notifications.GAME_START);

                    break;

                case Notifications.MODECHANGE_EVP:
                    _applicationProxy.ResetStastics();
                    UpdateStastics();
                    AppConfig.HUMAN_FIRST = false;
                    AppConfig.HAS_COMPUTER_PLAYER = true;
                    Application.ModeSelectPanel.Visibility = Visibility.Collapsed;

                    SendNotification(Notifications.GAME_START);

                    break;

                case Notifications.MODECHANGE_PVE:
                    _applicationProxy.ResetStastics();
                    UpdateStastics();
                    AppConfig.HUMAN_FIRST = true;
                    AppConfig.HAS_COMPUTER_PLAYER = true;
                    Application.ModeSelectPanel.Visibility = Visibility.Collapsed;

                    SendNotification(Notifications.GAME_START);

                    break;
            }
        }

        /// <summary>
        /// Updates the stastics.
        /// </summary>
        private void UpdateStastics()
        {
            Application.StasticsPanel.WhiteWonTextBlock.Text = _applicationProxy.WhiteWinsCount.ToString();
            Application.StasticsPanel.BlackWonTextBlock.Text = _applicationProxy.BlackWinsCount.ToString();
            
        }

        /// <summary>
        /// Inits the application.
        /// </summary>
        public void InitApplication()
        {
            Application.RestartPanel.Visibility = Visibility.Collapsed;
            Application.ModeSelectPanel.Visibility = Visibility.Collapsed;
        }
    }
}
