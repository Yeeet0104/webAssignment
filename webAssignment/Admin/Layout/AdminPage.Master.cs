using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace webAssignment
{
    public partial class AdminPage : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string currentPage = Path.GetFileName(Request.Path).ToLower();
            // Remove 'active' class if it was previously appended

            // Append 'active' class based on current page
            switch (currentPage)
            {
                case "dashboard.aspx":
                    dashboardLk.Attributes["class"] += " activeNavItem";
                    break;
                case "adminproducts.aspx":
                    productLk.Attributes["class"] += " activeNavItem";
                    break;
            }





        }

 

    }



}