using System;
using System.Collections;

using FluorineFx.Data.Assemblers;

namespace samples.crm
{
	/// <summary>
	/// Summary description for EmployeeAssembler.
	/// </summary>
	public class EmployeeAssembler : Assembler
	{
		public EmployeeAssembler()
		{
		}

		#region IAssembler Members

		public override IList Fill(IList fillParameters)
		{
			EmployeeDAO dao = new EmployeeDAO();
			return dao.LoadEmployees(fillParameters[0] as string, (int)fillParameters[1] );
		}

		public override void UpdateItem(object newVersion, object previousVersion, IList changes)
		{
			EmployeeDAO dao = new EmployeeDAO();
			Employee newEmployee = (Employee)newVersion;
			Employee prevEmployee = (Employee)previousVersion;
			dao.Update(newEmployee, prevEmployee, changes);
		}

		public override void DeleteItem(object previousVersion)
		{
			EmployeeDAO dao = new EmployeeDAO();
			dao.Delete((Employee)previousVersion);
		}

		public override void CreateItem(object item)
		{
			Employee newEmployee = (Employee)item;
			EmployeeDAO dao = new EmployeeDAO();
			dao.Create(newEmployee);
		}

		#endregion
	}
}
