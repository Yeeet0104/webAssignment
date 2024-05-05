using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webAssignment.Client.Cart;

namespace webAssignment.Client.ClientMasterPage
{
    public partial class ClientMasterPage : System.Web.UI.MasterPage
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
            Session["UserId"] = "CS1001";
            // Check if the user is logged in and retrieve their cart quantity
            if (Session["UserId"] != null)
            {
                string userId = Session["UserId"].ToString();
                int cartQuantity = GetCartQuantity(userId);

                // Update the cart icon badge
                UpdateCartIconBadge(cartQuantity);
            }
            
        }

        private int GetCartQuantity(string userId)
        {
            int cartQuantity = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT SUM(quantity) FROM Cart_Details WHERE cart_id IN (SELECT cart_id FROM Cart WHERE user_id = @UserId)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        cartQuantity = Convert.ToInt32(result);
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or handle it as needed
            }
            return cartQuantity;
        }

        public void UpdateCartIconBadge(int cartQuantity)
        {
            // Update the HTML element containing the cart icon badge
            if (cartQuantity > 0)
            {
                // Display the badge and update its value
                cartBadge.Visible = true;
                cartBadge.InnerText = cartQuantity.ToString();
            }
            else
            {
                // Hide the badge if there are no items in the cart
                cartBadge.Visible = false;
            }
        }
    }
}