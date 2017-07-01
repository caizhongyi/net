using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class WebFormLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void Login_Authenticate(object sender, AuthenticateEventArgs e)
    {
        // Initialize FormsAuthentication
        FormsAuthentication.Initialize();
        // Create a new ticket used for authentication
        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
            1, // Ticket version
            Login.UserName, // Username associated with ticket
            DateTime.Now, // Date/time issued
            DateTime.Now.AddMinutes(30), // Date/time to expire
            true, // "true" for a persistent user cookie
            "admin, privilegeduser", // User-data, in this case the roles
            FormsAuthentication.FormsCookiePath);// Path cookie valid for

        // Encrypt the cookie using the machine key for secure transport
        string hash = FormsAuthentication.Encrypt(ticket);
        HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, // Name of auth cookie
            hash); // Hashed ticket

        // Set the cookie's expiration time to the tickets expiration time
        //if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
        // Add the cookie to the list for outgoing response
        Response.Cookies.Add(cookie);
        Response.Redirect("Default.aspx");
    }
}
