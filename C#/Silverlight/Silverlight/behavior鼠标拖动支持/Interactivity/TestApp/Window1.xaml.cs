using System;
using System.Windows;

namespace TestApp
{
    using Expression.Blend.SampleData.SampleDataSource;

    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SampleDataSource sds = App.Current.Resources["SampleDataSource"] as SampleDataSource;

            Random r=new Random((int) DateTime.Now.Ticks);
           var newitem=new Item{ Name = sds.Collection[r.Next(sds.Collection.Count)].Name,
                                 CompanyName = sds.Collection[r.Next(sds.Collection.Count)].CompanyName,
                                 Image = sds.Collection[r.Next(sds.Collection.Count)].Image
                                   };
            newitem.Children.Add(sds.Collection[r.Next(sds.Collection.Count)].Children[0]);
            sds.Collection.Add(newitem);
        }
    }
}
