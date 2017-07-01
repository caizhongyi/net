using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.model
{
    public class Albums
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private string _UserName;

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Status;

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
        private string _Cover;

        public string Cover
        {
            get { return _Cover; }
            set { _Cover = value; }
        }
        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private DateTime _CreateTime;

        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private string _Path;

        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
        private int _Hits;

        public int Hits
        {
            get { return _Hits; }
            set { _Hits = value; }
        }
        private int _Power;

        public int Power
        {
            get { return _Power; }
            set { _Power = value; }
        }
    }
}
