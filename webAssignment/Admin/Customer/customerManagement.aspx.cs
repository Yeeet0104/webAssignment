using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Customer
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var testCustomers = GetTestCustomers();
                rptCustomer.DataSource = testCustomers;
                rptCustomer.DataBind();
            }
        }

        private List<testCustomer> GetTestCustomers()
        {
            var customers = new List<testCustomer>
    {
        new testCustomer("John Doe", "johndoe@gmail.com", "+917012345678", new DateTime(2023, 06, 24), "Active", DateTime.Now),
        new testCustomer("Jane Smith", "janesmith@gmail.com", "+917012345679", new DateTime(2023, 06, 24), "Active", DateTime.Now),
        new testCustomer("Alice Johnson", "alicejohnson@gmail.com", "+917012345680", new DateTime(2023, 06, 25), "Active", DateTime.Now),
        new testCustomer("Bob Williams", "bobwilliams@gmail.com", "+917012345681", new DateTime(2023, 06, 26), "Active", DateTime.Now),
        new testCustomer("Emma Davis", "emmadavis@gmail.com", "+917012345682", new DateTime(2023, 06, 27), "Active", DateTime.Now),
        new testCustomer("Michael Brown", "michaelbrown@gmail.com", "+917012345683", new DateTime(2023, 06, 28), "Active", DateTime.Now),
        new testCustomer("Olivia Jones", "oliviajones@gmail.com", "+917012345684", new DateTime(2023, 06, 29), "Active", DateTime.Now),
        new testCustomer("William Wilson", "williamwilson@gmail.com", "+917012345685", new DateTime(2023, 06, 30), "Active", DateTime.Now),
        new testCustomer("Sophia Taylor", "sophiataylor@gmail.com", "+917012345686", new DateTime(2023, 07, 01), "Active", DateTime.Now),
        new testCustomer("James Miller", "jamesmiller@gmail.com", "+917012345687", new DateTime(2023, 07, 02), "Active", DateTime.Now),
        new testCustomer("Ava Moore", "avamoore@gmail.com", "+917012345688", new DateTime(2023, 07, 03), "Active", DateTime.Now)
    };

            return customers;
        }
    }
}