using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.model
{
    public class Picture
    {
        private int _ID;

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private int _AlbumsID;

        public int AlbumsID
        {
            get { return _AlbumsID; }
            set { _AlbumsID = value; }
        }
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
        private string _Description;

        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        private int _Flag;

        public int Flag
        {
            get { return _Flag; }
            set { _Flag = value; }
        }
        private DateTime _CreateTime;

        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { _CreateTime = value; }
        }
        private int _Hits;

        public int Hits
        {
            get { return _Hits; }
            set { _Hits = value; }
        }
        private string _Path;

        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

    }
}
