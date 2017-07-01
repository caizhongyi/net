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

namespace PortalDemo
{
    public partial class AreaHead : UserControl
    {
        public AreaHead()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 所在区域的Id
        /// </summary>
        public string AreaId { get; set; }
    }
}
