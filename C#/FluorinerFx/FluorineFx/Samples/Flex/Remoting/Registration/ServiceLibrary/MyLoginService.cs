using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;
using System.IO;

using FluorineFx;
using FluorineFx.AMF3;
using FluorineFx.Context;

namespace ServiceLibrary
{
    /// <summary>
    /// MyLoginService is used to force setCredentials sending out credentials
    /// For Flash this is also used to log out.
    /// </summary>
    [FluorineFx.RemotingService]
    public class MyLoginService
    {
        public MyLoginService()
        {
        }

        private string GetConnectionString()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["UsersDBConnectionString"].ConnectionString;
            return connectionString;
        }

        public bool Login()
        {
            System.Security.Principal.IPrincipal p = FluorineContext.Current.User;
            return true;
        }

        public bool Logout()
        {
            //FormsAuthentication.SignOut();
            new MyLoginCommand().Logout(null);
            return true;
        }

        public ByteArray GetCaptcha()
        {
            // Create a random code and store it in the Session object.
            FluorineContext.Current.Session["CaptchaImageText"] = GenerateRandomCode();
            CaptchaImage ci = new CaptchaImage(FluorineContext.Current.Session["CaptchaImageText"].ToString(), 200, 50);
            // Write the image in JPEG format.
            MemoryStream tempStream = new MemoryStream();
            ci.Image.Save(tempStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            // Dispose of the CAPTCHA image object.
            ci.Dispose();
            ByteArray result = new ByteArray(tempStream);
            return result;
            //throw new Exception("Failed to process request. Please contact your vendor for further information.");
        }

        //
        // Returns a string of six random digits.
        //
        private string GenerateRandomCode()
        {
		    // For generating random numbers.
		    Random random = new Random();

            string s = "";
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        public void Register(string user, string password, string email, string captcha)
        {
            if (FluorineContext.Current.Session["CaptchaImageText"] == null ||
                FluorineContext.Current.Session["CaptchaImageText"].ToString() != captcha)
            {
                throw new Exception("Incorrect code, please try again");
            }
            //Now go and create the user
            using (OleDbConnection connection = new OleDbConnection(GetConnectionString()))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand(null, connection);
                command.CommandText = "SELECT count(*) FROM [Users] WHERE StrComp(User, '" + user + "', 0) = 0 OR StrComp(Email, '" + email + "', 0) = 0";
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    reader.Read();
                    int count = reader.GetInt32(0);
                    if (count != 0)
                        throw new Exception("Failed to process registration. The specified Username or Email already exists.");
                }

                string query = "INSERT INTO [Users] ([User], [Password], Email) VALUES (@User, @Password, @Email)";
                command = new OleDbCommand(query, connection);
                command.Parameters.Add("@User", OleDbType.VarWChar, 50).Value = user;
                command.Parameters.Add("@Password", OleDbType.VarWChar, 50).Value = password;
                command.Parameters.Add("@Email", OleDbType.VarWChar, 255).Value = email;
                command.ExecuteNonQuery();
            }

        }
    }
}