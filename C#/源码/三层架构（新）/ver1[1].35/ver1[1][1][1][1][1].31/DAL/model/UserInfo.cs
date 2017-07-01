using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.model
{
    public class UserInfo
    {
        private int _id;

        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }
        private string _Username;


        public string Username
        {
            get { return _Username; }
            set { _Username = value; }
        }
        private string _Password;


        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }private string _Email;

        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _NatualName;

        public string NatualName
        {
            get { return _NatualName; }
            set { _NatualName = value; }
        }
        private string _Address;

        public string Address
        {
            get { return _Address; }
            set { _Address = value; }
        }
        private string _Address1;

        public string Address1
        {
            get { return _Address1; }
            set { _Address1 = value; }
        }
        private string _Address2;

        public string Address2
        {
            get { return _Address2; }
            set { _Address2 = value; }
        }
        private string _City;

        public string City
        {
            get { return _City; }
            set { _City = value; }
        }
        private string _Provice;

        public string Provice
        {
            get { return _Provice; }
            set { _Provice = value; }
        }
        private string _Zip;

        public string Zip
        {
            get { return _Zip; }
            set { _Zip = value; }
        }
        private string _Country;

        public string Country
        {
            get { return _Country; }
            set { _Country = value; }
        }
        private string _Telphone;

        public string Telphone
        {
            get { return _Telphone; }
            set { _Telphone = value; }
        }
        private string _Mobile;

        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }
        private DateTime RegTime;

        public DateTime RegTime1
        {
            get { return RegTime; }
            set { RegTime = value; }
        }
        private string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }
}