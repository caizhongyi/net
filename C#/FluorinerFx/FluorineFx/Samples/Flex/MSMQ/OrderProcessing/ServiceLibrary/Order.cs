using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceLibrary
{
    [Serializable]
    public class Order
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _product;

        public string Product
        {
            get { return _product; }
            set { _product = value; }
        }

        private int _quantity;

        public int Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }

        private string _user;

        public string User
        {
            get { return _user; }
            set { _user = value; }
        }
    }
}
