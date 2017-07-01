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
using Five.Controller;
using Five.Model;
using Five.Utils;
using PureMVC.Interfaces;
using PureMVC.Patterns;

namespace Five
{
    public class ApplicationFacade:Facade
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>The instance.</value>
        public new static ApplicationFacade Instance
        {
            get
            {
                if (m_instance == null)
                    m_instance = new ApplicationFacade();

                return m_instance as ApplicationFacade;
            }
        }

        /// <summary>
        /// Startups the specified app.
        /// </summary>
        /// <param name="app">The app.</param>
        public void Startup(object app)
        {
            NotifyObservers(new Notification(Notifications.APP_STARTUP, app));
        }

        /// <summary>
        /// Initializes the <see cref="ApplicationFacade"/> class.
        /// </summary>
        /// <remarks>
        /// This <c>IFacade</c> implementation is a Singleton, so you should not call the constructor directly, but instead call the static Singleton Factory method <c>Facade.Instance</c>
        /// </remarks>
        static ApplicationFacade(){}

        /// <summary>
        /// Initialize the <c>Controller</c>
        /// </summary>
        /// <remarks>
        /// 	<para>Called by the <c>initializeFacade</c> method. Override this method in your subclass of <c>Facade</c> if one or both of the following are true:</para>
        /// 	<list type="bullet">
        /// 		<item>You wish to initialize a different <c>IController</c></item>
        /// 		<item>You have <c>Commands</c> to register with the <c>Controller</c> at startup</item>
        /// 	</list>
        /// 	<para>If you don't want to initialize a different <c>IController</c>, call <c>base.initializeController()</c> at the beginning of your method, then register <c>Command</c>s</para>
        /// </remarks>
        protected override void InitializeController()
        {
            base.InitializeController();
            RegisterCommand(Notifications.APP_STARTUP, typeof (StartupCommand));
            RegisterCommand(Notifications.RESTART, typeof (RestartCommand));
            RegisterCommand(Notifications.GAME_START, typeof (GameStartCommand));
            RegisterCommand(Notifications.START_SELECT_MODE, typeof (SelectModeCommand));
        }

    }
}
