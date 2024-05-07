using DocumentFormat.OpenXml.Drawing.Spreadsheet;
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

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
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

        private void BindWishlistItems( )
        {

            if ( Session["userId"] != null )
            {
             string userId = Session["userId"].ToString();
                try
                {

                    using ( SqlConnection connection = new SqlConnection(connectionString) )
                    {
                        connection.Open();

                        // Query to fetch wishlist items for the specified user
                        string query = @"SELECT DISTINCT p.product_id, p.product_name, pv.variant_price, ip.path, pv.variant_status, pv.variant_name ,pv.product_variant_id
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
                catch ( Exception ex )
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

            }
            else
            {
                Response.Redirect("/Client/LoginSignUp/Login.aspx");

            }
        }

        protected void btnDelete_Click( object sender, EventArgs e )
        {
        }

        protected void lvWishlist_ItemCommand( object sender, ListViewCommandEventArgs e )
        {
            if ( e.CommandName.ToString() == "unwishlist" )
            {
                updateWishlist(e.CommandArgument.ToString());

            }
        }

        private void updateWishlist( string variantID )
        {
            try
            {
                using ( SqlConnection connection = new SqlConnection(connectionString) )
                {
                    connection.Open();
                    string query = "DELETE FROM Wishlist_details WHERE product_variant_id = @productVariantId";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@productVariantId", variantID);

                    int result = command.ExecuteNonQuery();
                    if ( result > 0 )
                    {
                        BindWishlistItems();
                        ShowNotification("Successfully unwishlisted an product!", "success");
                    }
                    else
                    {
                        ShowNotification("No item was removed, check your query and parameters!", "warining");
                    }
                }
            }
            catch ( Exception ex )
            {
                ShowNotification($"Error removing item from wishlist: {ex.Message}", "warining");
            }
        }

        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }
    }
}