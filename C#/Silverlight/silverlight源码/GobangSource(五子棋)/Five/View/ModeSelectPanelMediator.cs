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
using PureMVC.Patterns;

using Five.View.Components;

namespace Five.View
{
    public class ModeSelectPanelMediator:Mediator
    {
        public new const string NAME = "ModeSelectPanelMediator";

        /// <summary>
        /// Gets the model select panel.
        /// </summary>
        /// <value>The model select panel.</value>
        public ModeSelectComponent ModeSelectPanel
        {
            get
            {
                return ViewComponent as ModeSelectComponent;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ModeSelectPanelMediator"/> class.
        /// </summary>
        /// <param name="viewComponent">The view component.</param>
        public ModeSelectPanelMediator(object viewComponent)
            : base(NAME, viewComponent)
        {
            ModeSelectPanel.PVPButtonClick += ModeSelectPanel_PVPButtonClick;
            ModeSelectPanel.PVEButtonClick += ModeSelectPanel_PVEButtonClick;
            ModeSelectPanel.EVPButtonClick += ModeSelectPanel_EVPButtonClick;
        }

        void ModeSelectPanel_EVPButtonClick(object sender, RoutedEventArgs e)
        {
            SendNotification(Notifications.MODECHANGE_EVP);
        }

        void ModeSelectPanel_PVEButtonClick(object sender, RoutedEventArgs e)
        {
            SendNotification(Notifications.MODECHANGE_PVE);
        }

        void ModeSelectPanel_PVPButtonClick(object sender, RoutedEventArgs e)
        {
            SendNotification(Notifications.MODECHANGE_PVP);
        }
    }
}
