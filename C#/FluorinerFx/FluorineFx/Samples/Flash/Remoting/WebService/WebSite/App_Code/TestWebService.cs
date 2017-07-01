using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using ServiceLibrary.VO;

/// <summary>
/// Summary description for TestWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class TestWebService : System.Web.Services.WebService
{

    public TestWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Hello World";
    }

    [WebMethod]
    public OrderVO PlaceOrder(OrderVO order)
    {
        order.id = 123456;
        return order;
    }

    [WebMethod]
    public DataSet GetDataTable()
    {
        DataSet dataSet = new DataSet("mydataset");
        DataTable dataTable = dataSet.Tables.Add("test");
        dataTable.Columns.Add("Col1");
        dataTable.Columns.Add("Col2");
        dataTable.Rows.Add(new object[] { "cell1", 25 });
        dataTable.Rows.Add(new object[] { "cell1", 35 });
        return dataSet;
    }

}

