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
using System.IO;


namespace czy.Silverlight.Library
{
    public class GetFileDialog
    {
        private  OpenFileDialog GetOpenFileDialog(string filter, bool Multiselect)
        {
          
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = filter;
            op.Multiselect=Multiselect;
            return op;
            
        }

        public string  OpMVFile()
        {
            OpenFileDialog op = GetOpenFileDialog("(*.wmv)|*.wmv|(*.*)|*.*", false);
            if (op.ShowDialog() == true)
            {

                string url = op.File.ToString();
                return url;
               //return op.File.DirectoryName;
                
            }
            return string.Empty;
        }
    }
}
