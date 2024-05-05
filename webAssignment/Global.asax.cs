using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using webAssignment.Admin;
namespace webAssignment
{
	public class Global : System.Web.HttpApplication
	{
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Application_Start(object sender, EventArgs e) 		
		{

		}
		
		protected void Session_Start(object sender, EventArgs e) 
		{

		}

		protected void Application_BeginRequest(object sender, EventArgs e) 
		{
            string requestUrl = HttpContext.Current.Request.Path.ToLower();

            // Assuming your admin section is under a path that includes "/admin"
            if ( !requestUrl.Contains("/admin") )
            {
                RecordVisit();
            }
        }

		protected void Application_AuthenticateRequest(object sender, EventArgs e) 
		{

		}

		protected void Application_Error(object sender, EventArgs e) 
		{

		}

		protected void Session_End(object sender, EventArgs e) 
		{

		}

		protected void Application_End(object sender, EventArgs e) 
		{

		}

        private void RecordVisit( )
        {
            // Check if the user already has the "VisitorCookie"
            HttpCookie visitorCookie = HttpContext.Current.Request.Cookies["VisitorCookie"];

            if ( visitorCookie == null )
            {
                // This is a new visitor, set a cookie
                HttpCookie newVisitorCookie = new HttpCookie("VisitorCookie");
                newVisitorCookie.Expires = DateTime.Now.AddDays(1); // The cookie expires in 1 day
                HttpContext.Current.Response.Cookies.Add(newVisitorCookie);

                using ( SqlConnection con = new SqlConnection(connectionString) )
                {
                    con.Open();
                    var today = DateTime.Today;

                    // Check if there's already an entry for today
                    string checkQuery = "SELECT VisitCount FROM VisitorLog WHERE VisitDate = @VisitDate";
                    SqlCommand cmd = new SqlCommand(checkQuery, con);
                    cmd.Parameters.AddWithValue("@VisitDate", today);

                    object result = cmd.ExecuteScalar();

                    if ( result != null )
                    {
                        // Update the existing count
                        int currentCount = Convert.ToInt32(result);
                        string updateQuery = "UPDATE VisitorLog SET VisitCount = @VisitCount WHERE VisitDate = @VisitDate";
                        SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                        updateCmd.Parameters.AddWithValue("@VisitCount", currentCount + 1);
                        updateCmd.Parameters.AddWithValue("@VisitDate", today);
                        updateCmd.ExecuteNonQuery();
                    }
                    else
                    {
                        // Create a new entry for today
                        string insertQuery = "INSERT INTO VisitorLog (VisitDate, VisitCount) VALUES (@VisitDate, 1)";
                        SqlCommand insertCmd = new SqlCommand(insertQuery, con);
                        insertCmd.Parameters.AddWithValue("@VisitDate", today);
                        insertCmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}