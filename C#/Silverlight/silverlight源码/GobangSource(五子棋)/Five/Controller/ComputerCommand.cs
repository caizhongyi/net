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
    public class ComputerCommand:SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            var applicationProxy = Facade.RetrieveProxy(ApplicationProxy.NAME) as ApplicationProxy;

        }
    }
}
