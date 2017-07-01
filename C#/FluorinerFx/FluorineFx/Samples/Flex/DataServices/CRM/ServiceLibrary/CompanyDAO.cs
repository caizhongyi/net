using System;
using System.Collections;

using System.Data;
using System.Data.OleDb;

using FluorineFx.Data;

namespace samples.crm
{
	/// <summary>
	/// Summary description for CompanyDAO.
	/// </summary>
	public class CompanyDAO
	{
		public static string ConnectionString = "Jet OLEDB:Global Partial Bulk Ops=2;Jet OLEDB:Registry Path=;Jet OLEDB:Database Locking Mode=1;Data Source=\"" + AppDomain.CurrentDomain.BaseDirectory + "\\crm.mdb\";Jet OLEDB:Engine Type=5;Provider=\"Microsoft.Jet.OLEDB.4.0\";Jet OLEDB:System database=;Jet OLEDB:SFP=False;persist security info=False;Extended Properties=;Jet OLEDB:Encrypt Database=False;Jet OLEDB:Create System Database=False;Jet OLEDB:Don't Copy Locale on Compact=False;Jet OLEDB:Compact Without Replica Repair=False;User ID=Admin;Jet OLEDB:Global Bulk Transactions=1";

		public CompanyDAO()
		{
		}

		public ArrayList FindCompanies(string name, string industry)
		{
			ArrayList list = new ArrayList();

			string query = "SELECT * FROM company";
			if (industry != null && industry != "All")
				query += " WHERE industry = '" + industry + "'";
			if (name != null)
			{
				if (industry == null)
					query += " WHERE company.name LIKE '%"+name+"%'";
				else
					query += " AND company.name LIKE '%"+name+"%'";
			}
			query += " ORDER BY company.name";

			using(OleDbConnection connection = new OleDbConnection( ConnectionString ))
			{
				OleDbCommand command = new OleDbCommand( query, connection );
				connection.Open();
				using( OleDbDataReader reader = command.ExecuteReader() )
				{			
					while( reader.Read() )
					{
						Company company = new Company();
						company.companyId = reader.GetInt32( reader.GetOrdinal( "companyId" ) ); 
						company.name = reader.GetString( reader.GetOrdinal( "name" ) );
						company.address = reader.GetString( reader.GetOrdinal( "address" ) );
						company.city = reader.GetString( reader.GetOrdinal( "city" ) );
						company.state = reader.GetString( reader.GetOrdinal( "state" ) );
						company.industry = reader.GetString( reader.GetOrdinal( "industry" ) );
						company.zip = reader.GetString( reader.GetOrdinal( "zip" ) );
						list.Add( company );
					}
				}
			}
			return list;
		}

		public Company GetCompany(int companyId)
		{
			string query = "SELECT * FROM company WHERE company_id=" + companyId;
			using(OleDbConnection connection = new OleDbConnection( ConnectionString ))
			{
				OleDbCommand command = new OleDbCommand( query, connection );
				connection.Open();
				using( OleDbDataReader reader = command.ExecuteReader() )
				{			
					if( reader.Read() )
					{
						Company company = new Company();
						company.companyId = reader.GetInt32( reader.GetOrdinal( "companyId" ) ); 
						company.name = reader.GetString( reader.GetOrdinal( "name" ) );
						company.address = reader.GetString( reader.GetOrdinal( "address" ) );
						company.city = reader.GetString( reader.GetOrdinal( "city" ) );
						company.state = reader.GetString( reader.GetOrdinal( "state" ) );
						company.industry = reader.GetString( reader.GetOrdinal( "industry" ) );
						company.zip = reader.GetString( reader.GetOrdinal( "zip" ) );
						return company;
					}
				}
			}
			return null;
		}

		public Company Create(Company company)
		{
			using(OleDbConnection connection = new OleDbConnection( CompanyDAO.ConnectionString ))
			{
				connection.Open();
				string query = "INSERT INTO company (name, address, city, state, zip, industry) VALUES (@name, @address, @city, @state, @zip, @industry)";
				OleDbCommand command = new OleDbCommand( query, connection );
				command.Parameters.Add ( "@name", OleDbType.VarWChar, 50).Value = company.name;
				command.Parameters.Add ( "@address", OleDbType.VarWChar, 50).Value = company.address;
				command.Parameters.Add ( "@city", OleDbType.VarWChar, 50).Value = (company.city == null ? DBNull.Value : (object)company.city);
				command.Parameters.Add ( "@state", OleDbType.VarWChar, 50).Value = (company.state == null ? DBNull.Value : (object)company.state);
				command.Parameters.Add ( "@zip", OleDbType.VarWChar, 50).Value = (company.zip == null ? DBNull.Value : (object)company.zip);
				command.Parameters.Add ( "@industry", OleDbType.VarWChar, 50).Value = company.industry;
				command.ExecuteNonQuery();
				command = new OleDbCommand( "select @@identity", connection );
				company.companyId = (int) command.ExecuteScalar();
			}
			return company;
		}

		public void Update(Company newVersion, Company previousVersion, IList changes)
		{
			Validate( newVersion );
			using(OleDbConnection connection = new OleDbConnection( CompanyDAO.ConnectionString ))
			{
				connection.Open();
				OleDbCommand command = new OleDbCommand( null, connection );

				command.CommandText = @"UPDATE company SET address = ?, city = ?, industry = ?, name = ?, state = ?, zip = ? WHERE (companyId = ?) AND (address = ? OR ? IS NULL AND address IS NULL) AND (city = ? OR ? IS NULL AND city IS NULL) AND (industry = ? OR ? IS NULL AND industry IS NULL) AND (name = ? OR ? IS NULL AND name IS NULL) AND (state = ? OR ? IS NULL AND state IS NULL) AND (zip = ? OR ? IS NULL AND zip IS NULL)";
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("address", System.Data.OleDb.OleDbType.VarWChar, 50, "address")).Value = newVersion.address;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("city", System.Data.OleDb.OleDbType.VarWChar, 50, "city")).Value = newVersion.city;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("industry", System.Data.OleDb.OleDbType.VarWChar, 50, "industry")).Value = newVersion.industry;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("name", System.Data.OleDb.OleDbType.VarWChar, 50, "name")).Value = newVersion.name;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("state", System.Data.OleDb.OleDbType.VarWChar, 50, "state")).Value = newVersion.state;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("zip", System.Data.OleDb.OleDbType.VarWChar, 50, "zip")).Value = newVersion.zip;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_companyId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "companyId", System.Data.DataRowVersion.Original, null)).Value = previousVersion.companyId;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_address", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "address", System.Data.DataRowVersion.Original, null)).Value = previousVersion.address;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_address1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "address", System.Data.DataRowVersion.Original, null)).Value = previousVersion.address;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_city", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "city", System.Data.DataRowVersion.Original, null)).Value = previousVersion.city;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_city1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "city", System.Data.DataRowVersion.Original, null)).Value = previousVersion.city;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_industry", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "industry", System.Data.DataRowVersion.Original, null)).Value = previousVersion.industry;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_industry1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "industry", System.Data.DataRowVersion.Original, null)).Value = previousVersion.industry;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_name", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "name", System.Data.DataRowVersion.Original, null)).Value = previousVersion.name;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_name1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "name", System.Data.DataRowVersion.Original, null)).Value = previousVersion.name;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_state", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "state", System.Data.DataRowVersion.Original, null)).Value = previousVersion.state;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_state1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "state", System.Data.DataRowVersion.Original, null)).Value = previousVersion.state;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_zip", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "zip", System.Data.DataRowVersion.Original, null)).Value = previousVersion.zip;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_zip1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "zip", System.Data.DataRowVersion.Original, null)).Value = previousVersion.zip;

				int rowsAffected = command.ExecuteNonQuery();
				if( rowsAffected == 0 )
					throw new DataSyncException(previousVersion, changes);
			}
		}

		public void Delete(Company company)
		{
			using(OleDbConnection connection = new OleDbConnection( CompanyDAO.ConnectionString ))
			{
				connection.Open();
				OleDbCommand command = new OleDbCommand( null, connection );

				command.CommandText = @"DELETE FROM company WHERE (companyId = ?) AND (address = ? OR ? IS NULL AND address IS NULL) AND (city = ? OR ? IS NULL AND city IS NULL) AND (industry = ? OR ? IS NULL AND industry IS NULL) AND (name = ? OR ? IS NULL AND name IS NULL) AND (state = ? OR ? IS NULL AND state IS NULL) AND (zip = ? OR ? IS NULL AND zip IS NULL)";
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_companyId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "companyId", System.Data.DataRowVersion.Original, null)).Value = company.companyId;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_address", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "address", System.Data.DataRowVersion.Original, null)).Value = company.address;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_address1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "address", System.Data.DataRowVersion.Original, null)).Value = company.address;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_city", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "city", System.Data.DataRowVersion.Original, null)).Value = company.city;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_city1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "city", System.Data.DataRowVersion.Original, null)).Value = company.city;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_industry", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "industry", System.Data.DataRowVersion.Original, null)).Value = company.industry;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_industry1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "industry", System.Data.DataRowVersion.Original, null)).Value = company.industry;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_name", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "name", System.Data.DataRowVersion.Original, null)).Value = company.name;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_name1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "name", System.Data.DataRowVersion.Original, null)).Value = company.name;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_state", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "state", System.Data.DataRowVersion.Original, null)).Value = company.state;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_state1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "state", System.Data.DataRowVersion.Original, null)).Value = company.state;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_zip", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "zip", System.Data.DataRowVersion.Original, null)).Value = company.zip;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_zip1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "zip", System.Data.DataRowVersion.Original, null)).Value = company.zip;

				int rowsAffected = command.ExecuteNonQuery();
				if( rowsAffected == 0 )
					throw new DataSyncException(company, null);
			}
		}

		private void Validate(Company company)
		{
			if (company.name == null || company.name.Equals(""))
				throw new NoNullAllowedException("name required");
		}
	}
}
