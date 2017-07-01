using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace SilverlightApplication1
{
    public class UpperCaseBehaviour: Behavior<TextBox>
    {
        List<Key> _skipKeys = new List<Key>(new[] { Key.F1, Key.F2, Key.F3, Key.F4, Key.F5, Key.F6, Key.F7, Key.F8, Key.F9, Key.F10, Key.F11, Key.F12, Key.Tab });

        #region Behavior Initialization
        //The two events required to wire up this behavior to the appropriate control are defined here: OnAttached(), and OnDetaching()
        protected override void OnAttached()
        {

            base.OnAttached();
            AssociatedObject.KeyDown += AssociatedObjectKeyDown;
        }

        void AssociatedObjectKeyDown(object sender, KeyEventArgs e)
        {
            var tb = (TextBox)sender;
            if ((tb.MaxLength == 0) ||(tb.MaxLength > 0 && tb.Text.Length < tb.MaxLength))
            {
                if (_skipKeys.Contains(e.Key)) return;

                //do not hanlde ModifierKeys
                if (Keyboard.Modifiers == ModifierKeys.Shift) return;
                if (Keyboard.Modifiers == ModifierKeys.Control) return;

                //clear the selection 
                if (tb.SelectedText.Length > 0)
                    tb.SelectedText = "";


                var s = new string(new[] { (char)e.PlatformKeyCode });
                var i = tb.SelectionStart;
                tb.Text = tb.Text.Insert(i, s);
                tb.Select(i + 1, 0);
                e.Handled = true;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.KeyDown -= AssociatedObjectKeyDown;
        }
        #endregion
 
    }
}
