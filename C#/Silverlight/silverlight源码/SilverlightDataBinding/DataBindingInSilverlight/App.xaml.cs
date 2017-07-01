using System;
using System.Windows;

namespace DataBindingInSilverlight
{
	public partial class App : Application
	{




		public App()
		{
			InitializeComponent();
         this.Startup += OnStartup;
         this.Exit += OnExit;
		}

		private void OnStartup(object o, EventArgs e)
		{
			//Set initial page here
			this.RootVisual = new Page();
		}

		private void OnExit(object o, EventArgs e)
		{
		}

	}

} //end of root namespace