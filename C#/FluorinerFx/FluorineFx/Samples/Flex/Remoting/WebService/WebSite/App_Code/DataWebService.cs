using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;

using FluorineFx;
/// <summary>
/// Summary description for DataWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class DataWebService : System.Web.Services.WebService {

    public DataWebService () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }

    [WebMethod]
    public DataSet GetDataSet()
    {
        DataSet dataSet = new DataSet("mydataset");
        DataTable dataTable = dataSet.Tables.Add("test");
        dataTable.Columns.Add("Col1");
        dataTable.Columns.Add("Col2");
        dataTable.Rows.Add(new object[] { "row1", 25 });
        dataTable.Rows.Add(new object[] { "row2", 35 });
        return dataSet;
    }

    [WebMethod]
    public DataTable GetDataTable()
    {
        DataTable dataTable = new DataTable("test");
        dataTable.Columns.Add("Col1");
        dataTable.Columns.Add("Col2");
        dataTable.Rows.Add(new object[] { "row1", 25 });
        dataTable.Rows.Add(new object[] { "row2", 35 });
        return dataTable;
    }
}

