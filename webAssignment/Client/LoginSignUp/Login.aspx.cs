using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace webAssignment.Client.LoginSignUp
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            ValidateTextBox(txtEmail);
            ValidateTextBox(txtPass);

            if (ValidateTextBox(txtEmail) && ValidateTextBox(txtPass))
            {
                // Query the database to retrieve the user record based on the provided email
                string query = "SELECT * FROM [User] WHERE email = @Email";

                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            // User with the provided email found, compare passwords
                            string storedPassword = reader["password"].ToString();
                            if (storedPassword == txtPass.Text)
                            {
                                // Passwords match, login successful
                                // Save user information in a cookie
                                HttpCookie userInfoCookie = new HttpCookie("userInfo");
                                userInfoCookie["userID"] = reader["user_id"].ToString();
                                // You can add more user information here if needed
                                userInfoCookie.Expires = DateTime.Now.AddDays(1); // Cookie expiration time
                                Response.Cookies.Add(userInfoCookie);

                                // Redirect to the home page or dashboard
                                Response.Redirect("~/Client/Home/HomePage.aspx");
                            }
                            else
                            {
                                // Passwords don't match, display error message
                                lblLoginMessage.Text = "Incorrect password.";
                            }
                        }
                        else
                        {
                            // User with the provided email not found, display error message
                            lblLoginMessage.Text = "User with this email does not exist.";
                        }
                    }
                }
            }
        }

        private Boolean ValidateTextBox(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Style["border-color"] = "#EF4444";
                return false;
            }
            else
            {
                textBox.Style.Remove("border-color");
                return true;
            }
        }
    }
}