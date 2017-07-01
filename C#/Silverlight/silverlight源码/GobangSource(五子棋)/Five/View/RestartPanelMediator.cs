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
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace Five.View
{
    public class RestartPanelMediator : Mediator
    {
        public new const string NAME = "RestartPanelMediator";

        /// <summary>
        /// Gets the restart panel.
        /// </summary>
        /// <value>The restart panel.</value>
        public RestartPanelComponent RestartPanel
        {
            get
            {
                return ViewComponent as RestartPanelComponent;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestartPanelMediator"/> class.
        /// </summary>
        /// <param name="viewComponent">The view component.</param>
        public RestartPanelMediator(object viewComponent)
            : base(NAME, viewComponent)
        {
            RestartPanel.RestartClick += RestartPanel_RestartClick;
        }

        /// <summary>
        /// Handles the RestartClick event of the RestartPanel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        void RestartPanel_RestartClick(object sender, RoutedEventArgs e)
        {
            SendNotification(Notifications.RESTART);
        }

        /// <summary>
        /// List the <c>INotification</c> names this <c>Mediator</c> is interested in being notified of
        /// </summary>
        /// <returns>The list of <c>INotification</c> names</returns>
        public override IList<string> ListNotificationInterests()
        {
            return new List<string>(new[]
                                        {
                                            Notifications.PLAYER_WIN
                                        }
                );
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
                case Notifications.PLAYER_WIN:

                    var type = ((List<WinningResult>)notification.Body)[0].PieceType;
                    RestartPanel.MessageTextBlock.Text = type + " wins";

                    break;
            }
        }


    }
}
