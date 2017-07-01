using System;

namespace JuSNS.Model
{

    public class UserBaseInfo
    {
        private Int32 _UserID;
        private DateTime _Birthday;
        private Int32 _BirthidayDisplay;
        private Int32 _Constellation;
        private String _MSN;
        private String _QQ;
        private String _Tel;
        private String _Addr;
        private String _WebSite;
        private String _Favourite;
        private String _FavMusics;
        private String _FavMovies;
        private String _FavCartoons;
        private String _FavGames;
        private String _FavSports;
        private String _FavBooks;
        private String _FavAdages;
        private String _AppreciateMen;
        private String _Presentation;
        private Int32 _Vocation;
        private Int32 _HomeCity;
        private Byte _MobilePrivacy;
        private Byte _EmailPrivacy;
        private Int32 _MSNPrivacy;
        private Int32 _QQPrivacy;
        private Int32 _TelPrivacy;
        private Int32 _AddrPrivacy;
        private Int32 _WebSitePrivacy;

        public UserBaseInfo()
        { }

        public UserBaseInfo(Int32 userID, DateTime birthday, Int32 birthidayDisplay, Int32 constellation, String mSN, String qQ, String tel, String addr, String webSite, String favourite, String favMusics, String favMovies, String favCartoons, String favGames, String favSports, String favBooks, String favAdages, String appreciateMen, String presentation, Int32 vocation, Int32 homeCity, Byte mobilePrivacy, Byte emailPrivacy, Int32 mSNPrivacy, Int32 qQPrivacy, Int32 telPrivacy, Int32 addrPrivacy, Int32 webSitePrivacy)
        {
            this._UserID = userID;
            this._Birthday = birthday;
            this._BirthidayDisplay = birthidayDisplay;
            this._Constellation = constellation;
            this._MSN = mSN;
            this._QQ = qQ;
            this._Tel = tel;
            this._Addr = addr;
            this._WebSite = webSite;
            this._Favourite = favourite;
            this._FavMusics = favMusics;
            this._FavMovies = favMovies;
            this._FavCartoons = favCartoons;
            this._FavGames = favGames;
            this._FavSports = favSports;
            this._FavBooks = favBooks;
            this._FavAdages = favAdages;
            this._AppreciateMen = appreciateMen;
            this._Presentation = presentation;
            this._Vocation = vocation;
            this._HomeCity = homeCity;
            this._MobilePrivacy = mobilePrivacy;
            this._EmailPrivacy = emailPrivacy;
            this._MSNPrivacy = mSNPrivacy;
            this._QQPrivacy = qQPrivacy;
            this._TelPrivacy = telPrivacy;
            this._AddrPrivacy = addrPrivacy;
            this._WebSitePrivacy = webSitePrivacy;
        }


        public Int32 UserID
        {
            get { return this._UserID; }
            set { this._UserID = value; }
        }

        public DateTime Birthday
        {
            get { return this._Birthday; }
            set { this._Birthday = value; }
        }

        public Int32 BirthidayDisplay
        {
            get { return this._BirthidayDisplay; }
            set { this._BirthidayDisplay = value; }
        }

        public Int32 Constellation
        {
            get { return this._Constellation; }
            set { this._Constellation = value; }
        }

        public String MSN
        {
            get { return this._MSN; }
            set { this._MSN = value; }
        }

        public String QQ
        {
            get { return this._QQ; }
            set { this._QQ = value; }
        }

        public String Tel
        {
            get { return this._Tel; }
            set { this._Tel = value; }
        }

        public String Addr
        {
            get { return this._Addr; }
            set { this._Addr = value; }
        }

        public String WebSite
        {
            get { return this._WebSite; }
            set { this._WebSite = value; }
        }

        public String Favourite
        {
            get { return this._Favourite; }
            set { this._Favourite = value; }
        }

        public String FavMusics
        {
            get { return this._FavMusics; }
            set { this._FavMusics = value; }
        }

        public String FavMovies
        {
            get { return this._FavMovies; }
            set { this._FavMovies = value; }
        }

        public String FavCartoons
        {
            get { return this._FavCartoons; }
            set { this._FavCartoons = value; }
        }

        public String FavGames
        {
            get { return this._FavGames; }
            set { this._FavGames = value; }
        }

        public String FavSports
        {
            get { return this._FavSports; }
            set { this._FavSports = value; }
        }

        public String FavBooks
        {
            get { return this._FavBooks; }
            set { this._FavBooks = value; }
        }

        public String FavAdages
        {
            get { return this._FavAdages; }
            set { this._FavAdages = value; }
        }

        public String AppreciateMen
        {
            get { return this._AppreciateMen; }
            set { this._AppreciateMen = value; }
        }

        public String Presentation
        {
            get { return this._Presentation; }
            set { this._Presentation = value; }
        }

        public Int32 Vocation
        {
            get { return this._Vocation; }
            set { this._Vocation = value; }
        }

        public Int32 HomeCity
        {
            get { return this._HomeCity; }
            set { this._HomeCity = value; }
        }

        public Byte MobilePrivacy
        {
            get { return this._MobilePrivacy; }
            set { this._MobilePrivacy = value; }
        }

        public Byte EmailPrivacy
        {
            get { return this._EmailPrivacy; }
            set { this._EmailPrivacy = value; }
        }

        public Int32 MSNPrivacy
        {
            get { return this._MSNPrivacy; }
            set { this._MSNPrivacy = value; }
        }

        public Int32 QQPrivacy
        {
            get { return this._QQPrivacy; }
            set { this._QQPrivacy = value; }
        }

        public Int32 TelPrivacy
        {
            get { return this._TelPrivacy; }
            set { this._TelPrivacy = value; }
        }

        public Int32 AddrPrivacy
        {
            get { return this._AddrPrivacy; }
            set { this._AddrPrivacy = value; }
        }

        public Int32 WebSitePrivacy
        {
            get { return this._WebSitePrivacy; }
            set { this._WebSitePrivacy = value; }
        }
    }
}