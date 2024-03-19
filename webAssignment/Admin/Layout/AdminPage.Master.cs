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
            assignActiveClass();





        }

        private void assignActiveClass( )
        {
            string currentPage = Path.GetFileName(Request.Path).ToLower();

            // Append 'active' class based on current page
            switch ( currentPage )
            {
                case "customermanagement.aspx":
                    customerLk.Attributes["class"] += " activeNavItem";
                    break;
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