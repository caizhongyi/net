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
using Five.Model;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace Five.Controller
{
    public class RestartCommand : SimpleCommand
    {
        /// <summary>
        /// Fulfill the use-case initiated by the given <c>INotification</c>
        /// </summary>
        /// <param name="notification">The <c>INotification</c> to handle</param>
        /// <remarks>
        /// In the Command Pattern, an application use-case typically begins with some user action, which results in an <c>INotification</c> being broadcast, which is handled by business logic in the <c>execute</c> method of an <c>ICommand</c>
        /// </remarks>
        public override void Execute(INotification notification)
        {
            var pieceboardProxy = Facade.RetrieveProxy(PieceboardProxy.NAME) as PieceboardProxy;
            if (pieceboardProxy != null)
                pieceboardProxy.ClearPiece();

        }
    }
}
