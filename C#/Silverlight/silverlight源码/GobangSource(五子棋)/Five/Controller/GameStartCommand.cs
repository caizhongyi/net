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
    public class GameStartCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var applicationProxy = Facade.RetrieveProxy(ApplicationProxy.NAME) as ApplicationProxy;
            var pieceboardProxy = Facade.RetrieveProxy(PieceboardProxy.NAME) as PieceboardProxy;
            var pieceboardMediator = Facade.RetrieveMediator(PieceboardMediator.NAME) as PieceboardMediator;
            var applicationMediator = Facade.RetrieveMediator(ApplicationMediator.NAME) as ApplicationMediator;

            applicationProxy.LoadConfig();
            pieceboardProxy.ClearPiece();
            pieceboardMediator.InitPieceBoard();
            applicationMediator.InitApplication();
            


            if(!AppConfig.HUMAN_FIRST && AppConfig.HAS_COMPUTER_PLAYER)
                pieceboardMediator.ComputerPlay();
        }
    }
}
