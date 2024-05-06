using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.ProfileMaster
{
    public partial class ProfileMasterPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkLogout_Click(object sender, EventArgs e)
        {
            
            if(Session["userId"] != null)
            {
                Session.Remove("userId");
            }

            // Redirect the user to the login page
            Response.Redirect("~/Client/LoginSignUp/Login.aspx");
        }
    }
}