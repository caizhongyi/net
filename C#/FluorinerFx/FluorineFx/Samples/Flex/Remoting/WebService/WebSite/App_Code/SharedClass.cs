using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ServiceLibrary
{
    public class SharedClass
    {
        private string _field1;

        public string Field1
        {
            get { return _field1; }
            set { _field1 = value; }
        }

        private int _field2;

        public int Field2
        {
            get { return _field2; }
            set { _field2 = value; }
        }

        public SharedClass()
        {
        }
    }
}