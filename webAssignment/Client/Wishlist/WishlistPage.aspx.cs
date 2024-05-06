using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Wishlist
{
    public partial class WishlistPage : System.Web.UI.Page
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //lvWishlist.DataSource = GetDummyData();
                //lvWishlist.DataBind();
                //Session["UserId"] = "CS1001";

                //if (Session["UserId"] == null)
                //{
                //    BindWishlistItems();
                //}
                BindWishlistItems();

            }
        }

        private void BindWishlistItems()
        {
            //string userId = Session["UserId"].ToString();
            string userId = "CS1001";
            try
            {
                // Establish connection to database
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Query to fetch wishlist items for the specified user
                    string query = @"SELECT p.product_id, p.product_name, pv.variant_price, ip.path, pv.variant_status, pv.variant_name
                             FROM Wishlist w
                             JOIN Wishlist_details wd ON w.wishlist_id = wd.wishlist_id
                             JOIN Product_Variant pv ON wd.product_variant_id = pv.product_variant_id
                             JOIN Product p ON pv.product_id = p.product_id
                             JOIN Image_Path ip ON p.product_id = ip.product_id
                             WHERE w.user_id = @userId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@userId", userId);

                    SqlDataReader reader = command.ExecuteReader();

                    lvWishlist.DataSource = reader;
                    lvWishlist.DataBind();

                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
        }
    }
}