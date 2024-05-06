using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
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
                int wishquant = getWishlistQuantity(userId);

                // Update the cart icon badge
                UpdateCartIconBadge(cartQuantity);
                UpdateWishIconBadge(wishquant);
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


        private int getWishlistQuantity( string userId )
        {
            int wishQuant = 0;
            try
            {
                using ( SqlConnection conn = new SqlConnection(connectionString) )
                {
                    conn.Open();
                    string sql = @"SELECT COUNT(DISTINCT wd.product_variant_id) AS NumberOfWishlistedProducts
                                    FROM Wishlist w
                                    JOIN Wishlist_details wd ON w.wishlist_id = wd.wishlist_id
                                    WHERE w.user_id = @UserID";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    object result = cmd.ExecuteScalar();
                    if ( result != null && result != DBNull.Value )
                    {
                        wishQuant = Convert.ToInt32(result);
                    }
                }
            }
            catch ( SqlException ex )
            {
                // Log the exception or handle it as needed
            }
            return wishQuant;
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
        public void UpdateWishIconBadge( int wishQuant )
        {
            // Update the HTML element containing the cart icon badge
            if ( wishQuant > 0 )
            {
                // Display the badge and update its value
                wishListBadge.Visible = true;
                wishListBadge.InnerText = wishQuant.ToString();
            }
            else
            {
                // Hide the badge if there are no items in the cart
                wishListBadge.Visible = false;
            }
        }
        protected void btnSendRequest_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                if (IsValidEmail(txtFeedbackEmail.Text))
                {
                    string receiverEmail = txtFeedbackEmail.Text;
                    string username = txtFeedbackUsername.Text;

                    try
                    {
                        string senderEmail = "gtechpc24@gmail.com";
                        MailMessage verificationMail = new MailMessage(senderEmail, receiverEmail);
                        verificationMail.Subject = "Request a call from our team";

                        verificationMail.Body = $"<h3>Hi {username},</h3>" +
                                                "<p>We have received your request for a call with our team to seek personalised support.</p>" +
                                                "<p>Please wait until we get back to you within 3 business days.</p>" +
                                                "<p>Thank you for contacting us, G-Tech Team.</p>";
                        verificationMail.IsBodyHtml = true;
                        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(senderEmail, "lajd btuc nhuf qryg");

                        smtpClient.Send(verificationMail);
                        lblMsg.Text = "Request Sent!";
                    }
                    catch (Exception ex)
                    {
                        lblMsg.Text = "Email failed to send: " + ex.Message;
                    }
                }
                else
                {
                    lblMsg.Text = "Invalid email format!";
                }
            }
            else
            {
                lblMsg.Text = "Input fields cannot be empty!";
            }
        }
        public static bool IsValidEmail(string email)
        {
            // Define a regular expression pattern for validating email addresses
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Use the IsMatch method to see if the email matches the pattern
            return regex.IsMatch(email);
        }
        private bool ValidateForm()
        {
            bool isEmailValid = !string.IsNullOrEmpty(txtFeedbackEmail.Text);
            txtFeedbackEmail.Style["border-color"] = isEmailValid ? string.Empty : "#EF4444";
            bool isUsernameValid = !string.IsNullOrEmpty(txtFeedbackUsername.Text);
            txtFeedbackUsername.Style["border-color"] = isUsernameValid ? string.Empty : "#EF4444";
            return isEmailValid && isUsernameValid;
        }
    }
}