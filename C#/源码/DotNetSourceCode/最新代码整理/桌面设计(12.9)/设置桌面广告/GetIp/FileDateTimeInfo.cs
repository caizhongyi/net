using System;
using System.Collections.Generic;
using System.Text;

namespace WbSystem.GetIp
{
    public class FileDateTimeInfo
    {
        private static DateTime _filedatetime;

        public DateTime Filedatetime
        {
            get { return FileDateTimeInfo._filedatetime; }
            set { FileDateTimeInfo._filedatetime = value; }
        }

        private static DateTime _servicetime;

        public DateTime Servicetime
        {
            get { return FileDateTimeInfo._servicetime; }
            set { FileDateTimeInfo._servicetime = value; }
        }

        private static DateTime _lantime;

        public DateTime Lantime
        {
            get { return FileDateTimeInfo._lantime; }
            set { FileDateTimeInfo._lantime = value; }
        }

    }
}
