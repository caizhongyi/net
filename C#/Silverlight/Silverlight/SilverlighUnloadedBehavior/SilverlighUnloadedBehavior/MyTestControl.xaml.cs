using System.Windows;

namespace SilverlighUnloadedBehavior
{
    public partial class MyTestControl
    {
        public MyTestControl()
        {
            InitializeComponent();
        }

        public void OnUnloaded()
        {
            MessageBox.Show("OnUnloaded is the custom control");
        }
    }
}
