using System;

namespace ServiceLibrary
{
    class Stock
    {
        public Stock()
        {
        }

        public Stock(string symbol, string name, DateTime date, double change, double open, double low, double high, double last, double volume)
        {
            _symbol = symbol;
            _name = name;
            _date = date;
            _change = change;
            _open = open;
            _low = low;
            _high = high;
            _last = last;
            _volume = volume;
        }

        private string _symbol;

        public string symbol
        {
            get { return _symbol; }
            set { _symbol = value; }
        }
        private string _name;

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        private double _low;

        public double low
        {
            get { return _low; }
            set { _low = value; }
        }
        private double _high;

        public double high
        {
            get { return _high; }
            set { _high = value; }
        }
        private double _open;

        public double open
        {
            get { return _open; }
            set { _open = value; }
        }
        private double _last;

        public double last
        {
            get { return _last; }
            set { _last = value; }
        }
        private double _change;

        public double change
        {
            get { return _change; }
            set { _change = value; }
        }

        private double _volume;

        public double volume
        {
            get { return _volume; }
            set { _volume = value; }
        }

        private DateTime _date;

        public DateTime date
        {
            get { return _date; }
            set { _date = value; }
        }

    }
}
