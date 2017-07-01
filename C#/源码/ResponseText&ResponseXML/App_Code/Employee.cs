using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace AspNetAjaxOverview
{
	/// <summary>
	/// Summary description for Employee
	/// </summary>
	public class Employee
	{
		private string _FirstName;
		private string _LastName;
		private string _Title;

		public Employee() { }

		public Employee(string firstName, string lastName, string title)
		{
			this._FirstName = firstName;
			this._LastName = lastName;
			this._Title = title;
		}

		public string FirstName
		{
			get
			{
				return this._FirstName;
			}
		}

		public string LastName
		{
			get
			{
				return this._LastName;
			}
		}

		public string Title
		{
			get
			{
				return this._Title;
			}
		}
	}
}