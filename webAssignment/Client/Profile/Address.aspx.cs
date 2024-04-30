using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Profile
{
    public partial class Address : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void addNewShippingAddress_Click(object sender, EventArgs e)
        {
            string action = "Add New Address";
            string addressType = "shipping";

            // Construct the query string with both parameters
            string query = $"?action={action}&addressType={addressType}";

            // Redirect to AddAddress.aspx with the query string
            Response.Redirect("~/Client/Profile/AddAddress.aspx" + query);

        }

        protected void addNewBillingAddress_Click(object sender, EventArgs e)
        {
            string action = "Add New Address";
            string addressType = "billing";

            // Construct the query string with both parameters
            string query = $"?action={action}&addressType={addressType}";

            // Redirect to AddAddress.aspx with the query string
            Response.Redirect("~/Client/Profile/AddAddress.aspx" + query);

        }
        protected void editButton_Click(object sender, EventArgs e)
        {
            // Get the address ID of the selected address to edit
            LinkButton editButton = (LinkButton)sender;
            string addressId = editButton.CommandArgument;

            // Construct the query string with both parameters
            string action = "Edit Address";
            string query = $"?action={action}&addressId={addressId}";

            // Redirect to AddAddress.aspx with the combined query string
            Response.Redirect("~/Client/Profile/AddAddress.aspx" + query);
        }

        protected void removeButton_Click(object sender, EventArgs e)
        {
            LinkButton removeButton = (LinkButton)sender;
            string addressId = removeButton.CommandArgument;

            if (!string.IsNullOrEmpty(addressId))
            {
                // SQL query to delete the address from the database
                string query = "DELETE FROM [Address] WHERE address_id = @AddressId";

                // Establish connection and command objects
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@AddressId", addressId);

                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        conn.Close();

                        if (rowsAffected > 0)
                        {
                            // Address successfully deleted
                            // You can redirect the user to a success page or display a message here
                            billingAddressList.DataBind();
                        }
                        else
                        {
                            // Failed to delete address
                            // You can handle this scenario based on your application's requirements
                        }
                    }
                }
            }
        }

    }
}