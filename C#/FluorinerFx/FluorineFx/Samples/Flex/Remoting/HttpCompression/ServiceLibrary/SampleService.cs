using System;
using System.Data;

using FluorineFx;

namespace ServiceLibrary
{
	/// <summary>
	/// FluorineFx sample service.
	/// </summary>
    [RemotingService("FluorineFx SampleService")]
	public class SampleService
	{
        public SampleService()
		{
		}

        public DataSet GetDataSet()
        {
            DataSet dataSet = new DataSet("mydataset");
            DataTable dataTable = dataSet.Tables.Add("test");
            dataTable.Columns.Add("Col1");
            dataTable.Columns.Add("Col2");
            for (int i = 0; i < 500; i++)
            {
                dataTable.Rows.Add(new object[] { "cell"+i, i });
            }
            return dataSet;
        }
	}
}
