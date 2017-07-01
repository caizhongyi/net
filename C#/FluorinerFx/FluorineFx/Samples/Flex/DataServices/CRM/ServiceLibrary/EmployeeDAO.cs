using System;
using System.Collections;
using System.Text;

using System.Data;
using System.Data.OleDb;

using FluorineFx.Data;

namespace samples.crm
{
	/// <summary>
	/// Summary description for EmployeeDAO.
	/// </summary>
	public class EmployeeDAO
	{
		public EmployeeDAO()
		{
		}

		public ArrayList LoadEmployees(string filter, int companyId)
		{
			ArrayList list = new ArrayList();

			string query = "SELECT * FROM employee WHERE companyId = " + companyId;

			using(OleDbConnection connection = new OleDbConnection( CompanyDAO.ConnectionString ))
			{
				Company company = new Company();
				company.companyId = companyId;

				OleDbCommand command = new OleDbCommand( query, connection );
				connection.Open();
				using( OleDbDataReader reader = command.ExecuteReader() )
				{			
					while( reader.Read() )
					{
						Employee employee = new Employee();
						employee.employeeId = reader.GetInt32( reader.GetOrdinal( "employeeId" ) ); 
						employee.company = company;
						employee.firstName = reader.GetString( reader.GetOrdinal( "firstName" ) );
						employee.lastName = reader.GetString( reader.GetOrdinal( "lastName" ) );
						employee.title = reader.GetString( reader.GetOrdinal( "title" ) );
						employee.email = reader.GetString( reader.GetOrdinal( "email" ) );
						employee.phone = reader.GetString( reader.GetOrdinal( "phone" ) );
						list.Add( employee );
					}
				}
			}
			return list;
		}

		public Employee Create(Employee employee)
		{
			Validate( employee );
			using(OleDbConnection connection = new OleDbConnection( CompanyDAO.ConnectionString ))
			{
				connection.Open();
				string query = "INSERT INTO employee (firstName, lastName, title, email, phone, companyId) VALUES (@firstName, @lastName, @title, @email, @phone, @companyId)";
				OleDbCommand command = new OleDbCommand( query, connection );
				command.Parameters.Add ( "@firstName", OleDbType.VarWChar, 50).Value = employee.firstName;
				command.Parameters.Add ( "@lastName", OleDbType.VarWChar, 50).Value = employee.lastName;
				command.Parameters.Add ( "@title", OleDbType.VarWChar, 50).Value = (employee.title == null ? DBNull.Value : (object)employee.title);
				command.Parameters.Add ( "@email", OleDbType.VarWChar, 50).Value = (employee.email == null ? DBNull.Value : (object)employee.email);
				command.Parameters.Add ( "@phone", OleDbType.VarWChar, 50).Value = (employee.phone == null ? DBNull.Value : (object)employee.phone);
				command.Parameters.Add ( "@companyId", OleDbType.Integer).Value = employee.company.companyId;
				command.ExecuteNonQuery();
				command = new OleDbCommand( "select @@identity", connection );
				employee.employeeId = (int) command.ExecuteScalar();
			}
			return employee;
		}

		public void Update(Employee newVersion, Employee previousVersion, IList changes)
		{
			Validate( newVersion );
			using(OleDbConnection connection = new OleDbConnection( CompanyDAO.ConnectionString ))
			{
				connection.Open();
				OleDbCommand command = new OleDbCommand( null, connection );
				command.CommandText = @"UPDATE employee SET companyId = ?, email = ?, firstName = ?, lastName = ?, phone = ?, title = ? WHERE (employeeId = ?) AND (companyId = ? OR ? IS NULL AND companyId IS NULL) AND (email = ? OR ? IS NULL AND email IS NULL) AND (firstName = ? OR ? IS NULL AND firstName IS NULL) AND (lastName = ? OR ? IS NULL AND lastName IS NULL) AND (phone = ? OR ? IS NULL AND phone IS NULL) AND (title = ? OR ? IS NULL AND title IS NULL)";

				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("companyId", System.Data.OleDb.OleDbType.Integer, 0, "companyId")).Value = newVersion.company.companyId;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("email", System.Data.OleDb.OleDbType.VarWChar, 50, "email")).Value = newVersion.email;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("firstName", System.Data.OleDb.OleDbType.VarWChar, 50, "firstName")).Value = newVersion.firstName;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("lastName", System.Data.OleDb.OleDbType.VarWChar, 50, "lastName")).Value = newVersion.lastName;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("phone", System.Data.OleDb.OleDbType.VarWChar, 50, "phone")).Value = newVersion.phone;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("title", System.Data.OleDb.OleDbType.VarWChar, 50, "title")).Value = newVersion.title;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_employeeId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "employeeId", System.Data.DataRowVersion.Original, null)).Value = previousVersion.employeeId;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_companyId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "companyId", System.Data.DataRowVersion.Original, null)).Value = previousVersion.company.companyId;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_companyId1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "companyId", System.Data.DataRowVersion.Original, null)).Value = previousVersion.company.companyId;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_email", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "email", System.Data.DataRowVersion.Original, null)).Value = previousVersion.email;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_email1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "email", System.Data.DataRowVersion.Original, null)).Value = previousVersion.email;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_firstName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "firstName", System.Data.DataRowVersion.Original, null)).Value = previousVersion.firstName;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_firstName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "firstName", System.Data.DataRowVersion.Original, null)).Value = previousVersion.firstName;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_lastName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "lastName", System.Data.DataRowVersion.Original, null)).Value = previousVersion.lastName;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_lastName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "lastName", System.Data.DataRowVersion.Original, null)).Value = previousVersion.lastName;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_phone", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "phone", System.Data.DataRowVersion.Original, null)).Value = previousVersion.phone;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_phone1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "phone", System.Data.DataRowVersion.Original, null)).Value = previousVersion.phone;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_title", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "title", System.Data.DataRowVersion.Original, null)).Value = previousVersion.title;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_title1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "title", System.Data.DataRowVersion.Original, null)).Value = previousVersion.title;

				int rowsAffected = command.ExecuteNonQuery();
				if( rowsAffected == 0 )
					throw new DataSyncException(previousVersion, changes);
			}
		}

		public void Delete(Employee employee)
		{
			using(OleDbConnection connection = new OleDbConnection( CompanyDAO.ConnectionString ))
			{
				connection.Open();
				OleDbCommand command = new OleDbCommand( null, connection );
				command.CommandText = @"DELETE FROM employee WHERE (employeeId = ?) AND (companyId = ? OR ? IS NULL AND companyId IS NULL) AND (email = ? OR ? IS NULL AND email IS NULL) AND (firstName = ? OR ? IS NULL AND firstName IS NULL) AND (lastName = ? OR ? IS NULL AND lastName IS NULL) AND (phone = ? OR ? IS NULL AND phone IS NULL) AND (title = ? OR ? IS NULL AND title IS NULL)";

				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_employeeId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "employeeId", System.Data.DataRowVersion.Original, null)).Value = employee.employeeId;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_companyId", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "companyId", System.Data.DataRowVersion.Original, null)).Value = employee.company.companyId;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_companyId1", System.Data.OleDb.OleDbType.Integer, 0, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "companyId", System.Data.DataRowVersion.Original, null)).Value = employee.company.companyId;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_email", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "email", System.Data.DataRowVersion.Original, null)).Value = employee.email;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_email1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "email", System.Data.DataRowVersion.Original, null)).Value = employee.email;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_firstName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "firstName", System.Data.DataRowVersion.Original, null)).Value = employee.firstName;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_firstName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "firstName", System.Data.DataRowVersion.Original, null)).Value = employee.firstName;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_lastName", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "lastName", System.Data.DataRowVersion.Original, null)).Value = employee.lastName;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_lastName1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "lastName", System.Data.DataRowVersion.Original, null)).Value = employee.lastName;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_phone", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "phone", System.Data.DataRowVersion.Original, null)).Value = employee.phone;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_phone1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "phone", System.Data.DataRowVersion.Original, null)).Value = employee.phone;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_title", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "title", System.Data.DataRowVersion.Original, null)).Value = employee.title;
				command.Parameters.Add(new System.Data.OleDb.OleDbParameter("Original_title1", System.Data.OleDb.OleDbType.VarWChar, 50, System.Data.ParameterDirection.Input, false, ((System.Byte)(0)), ((System.Byte)(0)), "title", System.Data.DataRowVersion.Original, null)).Value = employee.title;

				int rowsAffected = command.ExecuteNonQuery();
				if( rowsAffected == 0 )
					throw new DataSyncException(employee, null);
			}
		}

		private void Validate(Employee employee)
		{
			StringBuilder errors = new StringBuilder();
		
			if (employee.firstName == null || employee.firstName.Equals(""))
			{
				errors.Append( "first name required," );
			}
			if (employee.lastName == null || employee.lastName.Equals(""))
			{
				errors.Append( "last name required" );
			}
			if (errors.Length>0)
				throw new NoNullAllowedException(errors.ToString());
		}
	}
}
