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
using Five.Utils;
using Five.View;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace Five.Controller
{
    public class SelectModeCommand:SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var applicationMediator = Facade.RetrieveMediator(ApplicationMediator.NAME) as ApplicationMediator;
            applicationMediator.Application.ModeSelectPanel.Visibility = Visibility.Visible;
            applicationMediator.Application.RestartPanel.Visibility = Visibility.Collapsed;

            var applicationProxy = Facade.RetrieveProxy(ApplicationProxy.NAME) as ApplicationProxy;
            applicationProxy.ResetStastics();

            var pieceboardMediator = Facade.RetrieveMediator(PieceboardMediator.NAME) as PieceboardMediator;
            pieceboardMediator.Clickable = false;
        }
    }
}
