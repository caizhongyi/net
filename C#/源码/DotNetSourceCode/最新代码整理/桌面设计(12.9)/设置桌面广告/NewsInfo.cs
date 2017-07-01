using System;
using System.Collections.Generic;
using System.Text;

namespace WbSystem
{
    public class NewsInfo
    {
        private static string _newstext;

        public string Newstext
        {
            get { return NewsInfo._newstext; }
            set { NewsInfo._newstext = value; }
        }

        private static string _wb_Adv_Id;

        public string Wb_Adv_Id
        {
            get { return _wb_Adv_Id; }
            set { _wb_Adv_Id = value; }
        }

        private static string _wb_Adv_Url;

        public string Wb_Adv_Url
        {
            get { return NewsInfo._wb_Adv_Url; }
            set { NewsInfo._wb_Adv_Url = value; }
        }

        private static string _adv_Time;

        public string Adv_Time
        {
            get { return _adv_Time; }
            set { _adv_Time = value; }
        }

        private static string _wb_Id;

        public string Wb_Id
        {
            get { return NewsInfo._wb_Id; }
            set { NewsInfo._wb_Id = value; }
        }

        private static string _wb_Adv_Content;

        public string Wb_Adv_Content
        {
            get { return _wb_Adv_Content; }
            set { _wb_Adv_Content = value; }
        }
    }
}
