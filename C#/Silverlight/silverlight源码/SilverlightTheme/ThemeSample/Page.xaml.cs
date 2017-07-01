using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
//5-1-aspx
using Microsoft.Windows.Controls.Theming;
using System.Collections;
using Microsoft.Windows.Controls.DataVisualization.Charting;


namespace ThemeSample
{
    public partial class Page : UserControl
    {
        public Page()
        {
            InitializeComponent();
            this.ThemeList.SelectionChanged += new SelectionChangedEventHandler(ThemeList_SelectionChanged);
            this.Loaded += new RoutedEventHandler(Page_Loaded);
        }

        void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ThemeList.Items.Add(new ComboBoxItem() { Name = "ExpressionDark", Content = "ExpressionDark", DataContext = "themes/ExpressionDark.xaml", IsEnabled = true });
            ThemeList.Items.Add(new ComboBoxItem() { Name = "ExpressionLight", Content = "ExpressionLight", DataContext = "themes/ExpressionLight.xaml" });
            ThemeList.Items.Add(new ComboBoxItem() { Name = "RainierOrange", Content = "RainierOrange", DataContext = "themes/RainierOrange.xaml" });
            ThemeList.Items.Add(new ComboBoxItem() { Name = "RainierPurple", Content = "RainierPurple", DataContext = "themes/RainierPurple.xaml" });
            ThemeList.Items.Add(new ComboBoxItem() { Name = "RainierRadialBlue", Content = "RainierRadialBlue", DataContext = "themes/RainierRadialBlue.xaml" });
            ThemeList.Items.Add(new ComboBoxItem() { Name = "ShinyBlue", Content = "ShinyBlue", DataContext = "themes/ShinyBlue.xaml" });
            ThemeList.Items.Add(new ComboBoxItem() { Name = "ShinyDarkGreen", Content = "ShinyDarkGreen", DataContext = "themes/ShinyDarkGreen.xaml" });
            ThemeList.Items.Add(new ComboBoxItem() { Name = "ShinyDarkPurple", Content = "ShinyDarkPurple", DataContext = "themes/ShinyDarkPurple.xaml" });
            ThemeList.Items.Add(new ComboBoxItem() { Name = "ShinyDarkTeal", Content = "ShinyDarkTeal", DataContext = "themes/ShinyDarkTeal.xaml" });
            ThemeList.Items.Add(new ComboBoxItem() { Name = "ShinyRed", Content = "ShinyRed", DataContext = "themes/ShinyRed.xaml" });

            SetTheme(ThemeList.Items[0] as ComboBoxItem);
        }

        void SetTheme(ComboBoxItem comboBoxItem)
        {
            if (comboBoxItem != null)
            {
                ControlPage control = new ControlPage();
                Test.Children.Clear();
                Test.Children.Add(control);

                Uri uri = new Uri(comboBoxItem.DataContext.ToString(), UriKind.Relative);
                ImplicitStyleManager.SetResourceDictionaryUri(control, uri);
                ImplicitStyleManager.SetApplyMode(control, ImplicitStylesApplyMode.Auto);
                ImplicitStyleManager.Apply(control);
            }
        }

        private void ThemeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetTheme(ThemeList.SelectedItem as ComboBoxItem);
        }

    }
}
