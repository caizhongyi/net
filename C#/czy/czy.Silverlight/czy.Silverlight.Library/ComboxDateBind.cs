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

namespace czy.Silverlight.Library
{
    public class ComboxDateBind
    {
        ComboBox _year;
        ComboBox _day;
        ComboBox _month;
        public ComboxDateBind(ComboBox year ,ComboBox month,ComboBox day)
        {
            _month = month;
            _year = year;
            _day = day;
            InitYear(DateTime.Now.Year - 200, year);
            InitMonth(month);
            InitDay(DateTime.Now.Year - 200, 1, day);
            year.SelectionChanged += new SelectionChangedEventHandler(prov_SelectionChanged);
            month.SelectionChanged += new SelectionChangedEventHandler(city_SelectionChanged);
            day.SelectionChanged += new SelectionChangedEventHandler(town_SelectionChanged);
        }

        void town_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        void city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitDay(Convert.ToInt32(_year.SelectedItem), Convert.ToInt32(_month.SelectedItem), _day);
        }

        void prov_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            InitDay(Convert.ToInt32(_year.SelectedItem), Convert.ToInt32(_month.SelectedItem), _day);
        }

        private void InitYear(int startYear,ComboBox  c)
        {
            for (int i = startYear; i <= DateTime .Now .Year; i++)
            {
                c.Items.Add(i);
            }
        }
        private void InitMonth( ComboBox c)
        {
            for (var i = 1; i <= 12; i++)
            {
                c.Items.Add(i);
            }
        }
        private void InitDay(int year,int month, ComboBox c)
        {
            if (month == 2) //判断是否为2月
            {
                if (year % 400 == 0 || (year % 100 != 0 && year % 4 == 0))
                {
                    for (var i = 1; i <= 28; i++)
                    {
                        c.Items.Add(i);
                    }
                }
                else
                {

                    for (var i = 1; i <= 29; i++)
                    {
                        c.Items.Add(i);
                    }
                }
            }
            else if ((month <= 7 && month != 2 && month % 2 != 0) || (month > 7 && month % 2 == 0))//判断是否为31天
            {
                for (var i = 1; i <= 31; i++)
                {
                    c.Items.Add(i);
                }
            }
            else
            {
                for (var i = 1; i <= 30; i++)
                {
                    c.Items.Add(i);
                }
            }
        }
    }
}
