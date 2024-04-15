using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Profile
{
    public partial class AccountDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (Request.Cookies["userInfo"] != null)
                {
                    string userId = Request.Cookies["userInfo"]["userID"];

                    // Query the database to retrieve the user's current account details based on the user ID
                    string query = "SELECT * FROM [User] WHERE user_id = @UserId";

                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDatabaseConnection"].ConnectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@UserId", userId);
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                // Populate the form fields with the retrieved account details
                                txtFirstName.Text = reader["first_name"].ToString();
                                txtLastName.Text = reader["last_name"].ToString();
                                txtUsername.Text = reader["username"].ToString();
                                txtEmail.Text = reader["email"].ToString();
                                txtPhoneNo.Text = reader["phone_number"].ToString();
                                // Populate other fields as needed
                            }
                        }
                    }
                }
                else
                {
                    // Handle the case where the user is not logged in
                    Response.Redirect("~/Client/LoginSignUp/Login.aspx");
                }
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            // Retrieve user ID from the cookie
            string userId = Request.Cookies["userInfo"]["userID"];

            // Retrieve updated account details from the form fields
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string phoneNumber = txtPhoneNo.Text;
            // Retrieve other updated fields as needed

            // Update the corresponding record in the database with the new details
            string query = "UPDATE [User] SET first_name = @FirstName, last_name = @LastName, username = @UserName, email = @Email, phone_number = @PhoneNumber WHERE user_id = @UserId";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDatabaseConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        // Account details updated successfully
                        lblUpdateDetailsMessage.Text = "Account details updated successfully!";
                    }
                    else
                    {
                        // Failed to update account details
                        lblUpdateDetailsMessage.Text = "Failed to update account details.";
                    }
                }
            }
        }
    }
}