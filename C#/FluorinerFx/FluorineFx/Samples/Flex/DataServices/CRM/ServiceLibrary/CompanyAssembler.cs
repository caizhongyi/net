using System;
using System.Collections;

using FluorineFx.Data;
using FluorineFx.Data.Assemblers;

namespace samples.crm
{
	/// <summary>
	/// Summary description for CompanyAssembler.
	/// </summary>
	public class CompanyAssembler : Assembler
	{
		public CompanyAssembler()
		{
		}

		#region IAssembler Members

		public override IList Fill(IList fillParameters)
		{
			CompanyDAO dao = new CompanyDAO();
			if (fillParameters.Count == 0)
				return dao.FindCompanies(null, null);
			else if (fillParameters.Count == 1)
				return dao.FindCompanies((string)fillParameters[0], null);
			else if (fillParameters.Count == 2)
				return dao.FindCompanies((string) fillParameters[0], (string) fillParameters[1]);
			return null;
		}

		/**
		 * If your fill methods are auto-refreshed, this method is called
		 * for each item that changes (either created or updated as indicated
		 * by the isCreate parameter).  It can return DO_NOT_EXECUTE_FILL if
		 * you want to leave the contents of the fill unchanged, EXECUTE_FILL if
		 * you want the data service to call your fill method, or APPEND_TO_FILL
		 * if you just want this changed item to be added onto the end of the
		 * list returned by the last fill invocation.
		 *
		 * @param fillParameters the client side parameters to a fill method which
		 * created a managed collection still managed by one or more clients.
		 * @param the item which is being created or updated
		 * @param true if this is a create operation, false if it is an update.
		 * @return DO_NOT_EXECUTE_FILL - do nothing, EXECUTE_FILL - re-run the
		 * fill method to get the new list, APPEND_TO_FILL - just add it to the 
		 * existing list.
		 */
		public override int RefreshFill(IList fillParameters, object item, bool isCreate)
		{
			Company company = item as Company;
			if(fillParameters.Count == 0) // This is the "all query"
				return Assembler.AppendToFill;
			if(fillParameters.Count == 1)
			{
				string name = fillParameters[0] as string;
				if(company.name.IndexOf(name) != -1)
					return Assembler.AppendToFill;
				return Assembler.DoNotExecuteFill;
			}
			else if (fillParameters.Count == 2)
			{
				string name = fillParameters[0] as string;
				string industry = fillParameters[1] as string;
				if((name == null || company.name.IndexOf(name) != -1) && 
					(industry == null || company.industry.IndexOf(industry) != -1))
					return Assembler.AppendToFill;
				return Assembler.DoNotExecuteFill;
			}
			return Assembler.ExecuteFill;
		}


		public override void UpdateItem(object newVersion, object previousVersion, IList changes)
		{
			CompanyDAO dao = new CompanyDAO();
			Company newCompany = (Company)newVersion;
			Company prevCompany = (Company)previousVersion;
			dao.Update(newCompany, prevCompany, changes);
		}

		public override void DeleteItem(object previousVersion)
		{
			CompanyDAO dao = new CompanyDAO();
			dao.Delete((Company)previousVersion);

			// Deleting the company has the side effect of changing the list of 
			// employees for this company.  Need to refresh the fill at this point.
			DataServiceTransaction dtx = DataServiceTransaction.CurrentDataServiceTransaction;
			dtx.RefreshFill("crm.employee", new object[]{"byCompany", (previousVersion as Company).companyId});
		}

		public override void CreateItem(object item)
		{
			Company newCompany = (Company)item;
			CompanyDAO dao = new CompanyDAO();
			dao.Create(newCompany);
		}

        public override object GetItem(IDictionary identity)
        {
            CompanyDAO dao = new CompanyDAO();
            return dao.GetCompany((int)identity["companyId"]);
        }

		#endregion

	}
}
