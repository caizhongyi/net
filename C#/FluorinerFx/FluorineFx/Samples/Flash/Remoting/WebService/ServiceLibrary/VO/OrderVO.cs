using System;

namespace ServiceLibrary.VO
{
    public class OrderVO
    {
        string _name;
        int _id;

        public OrderVO()
        {
        }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}
