using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.ComponentModel;
using System.Xml.Linq;

using FluorineFx;

namespace ServiceLibrary
{
    [RemotingService]
    [Description("Customer data service")]
    public class MyDataService
    {
        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MSAccessConnectionString"].ConnectionString;
            return connectionString;
        }

        [DataTableType("Flex.CustomerVO")]
        [Description("Filter customer by phone area code")]
        public DataTable GetCustomers(string areaCode)
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand("SELECT firstname, lastname, phone FROM [customer] WHERE phone LIKE '" + areaCode + "%'", connection);
                connection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataTable result = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }

        [DataTableType("Flex.CustomerVO")]
        public DataTable GetCustomerByLastName(string lastname)
        {
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                OleDbCommand command = new OleDbCommand("SELECT firstname, lastname, phone FROM [customer] WHERE lastname LIKE '" + lastname + "%'", connection);
                connection.Open();
                OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                DataTable result = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }

        public XDocument GetXDocument()
        {
            XDocument srcTree = new XDocument(
                new XComment("This is a comment"),
                new XElement("Root",
                    new XElement("Child1", "data1"),
                    new XElement("Child2", "data2"),
                    new XElement("Child3", "data3"),
                    new XElement("Child2", "data4"),
                    new XElement("Info5", "info5"),
                    new XElement("Info6", "info6"),
                    new XElement("Info7", "info7"),
                    new XElement("Info8", "info8")
                )
            );
            return srcTree;
        }

        public XElement GetXElement()
        {
            XNamespace aw = "http://www.adventure-works.com";
            XElement xmlTree1 = new XElement(aw + "Root",
                new XElement(aw + "Child1", 1),
                new XElement(aw + "Child2", 2),
                new XElement(aw + "Child3", 3),
                new XElement(aw + "Child4", 4),
                new XElement(aw + "Child5", 5),
                new XElement(aw + "Child6", 6)
            );
            return xmlTree1;
        }
    }
}
