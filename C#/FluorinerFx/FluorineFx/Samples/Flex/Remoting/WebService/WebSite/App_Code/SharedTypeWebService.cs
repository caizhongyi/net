using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using ServiceLibrary;
/// <summary>
/// Summary description for SharedTypeWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SharedTypeWebService : System.Web.Services.WebService
{

    public SharedTypeWebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public SharedClass GetSharedClassInstance()
    {
        SharedClass sc = new SharedClass();
        sc.Field1 = "Hello World";
        sc.Field2 = 1;
        return sc;
    }

}

