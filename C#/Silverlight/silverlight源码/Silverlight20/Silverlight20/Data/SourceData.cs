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

using System.Collections.Generic;

namespace Silverlight20.Data
{
    public class SourceData
    {
        public List<SourceDataModel> GetData()
        {
            var source = new List<SourceDataModel>();

            for (int i = 0; i < 100; i++)
            {
                source.Add(new SourceDataModel
                    {
                        Name = "Name" + i.ToString().PadLeft(4, '0'),
                        Age = new Random(i).Next(20, 60),
                        DayOfBirth = DateTime.Now,
                        Male = Convert.ToBoolean(i % 2)
                    });                
            }

            return source;
        }
    }
}
