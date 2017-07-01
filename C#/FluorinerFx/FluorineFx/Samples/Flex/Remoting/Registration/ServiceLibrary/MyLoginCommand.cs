using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.Collections;
using System.Security;
using System.Security.Principal;

using FluorineFx.Security;

namespace ServiceLibrary
{
    /// <summary>
    /// Summary description for MyLoginCommand
    /// </summary>
    public class MyLoginCommand : GenericLoginCommand
    {
        public MyLoginCommand()
        {
        }

        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UsersDBConnectionString"].ConnectionString;
            return connectionString;
        }

        public override IPrincipal DoAuthentication(string username, Hashtable credentials)
        {
            string password = credentials["password"] as string;
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(null, connection);
                command.CommandText = "SELECT * FROM [Users] WHERE StrComp(User, '" + username + "', 0) = 0 AND StrComp(Password, '" + password + "', 0) = 0";
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        GenericIdentity identity = new GenericIdentity(username);
                        GenericPrincipal principal = new GenericPrincipal(identity, new string[] { "registereduser" });
                        return principal;
                    }
                }
            }
            throw new SecurityException("Not a valid username or password");
        }
    }
}