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

namespace Silverlight20.Control.Advanced
{
    public partial class DataGrid : UserControl
    {
        public DataGrid()
        {
            InitializeComponent();


            // 写到绑定部分时就可以看看DataContext是怎么用的

            BindData();
        }

        void BindData()
        {
            var source = new Data.SourceData();

            // 设置 DataGrid 的数据源
            dgrd.ItemsSource = source.GetData().Take(10);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
