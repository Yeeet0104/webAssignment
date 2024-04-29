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
        {// It validates the form, attempts to log in the user, and redirects to the home page if successful.
            if (ValidateForm())
            {
                string email = txtEmail.Text;
                string password = txtPass.Text;

                string userId = LoginUser(email, password);
                if (userId != null)
                {
                    UpdateLastLoginTime(email);
                    SetUserInfoCookie(userId.ToString());
                    Response.Redirect("~/Client/Home/HomePage.aspx");
                }
                else
                {
                    lblLoginMessage.Text = "Invalid email or password.";
                }
            }
        }

        private bool ValidateForm()
        {
            // Check if both text boxes are empty
            bool isEmailValid = !string.IsNullOrEmpty(txtEmail.Text);
            bool isPassValid = !string.IsNullOrEmpty(txtPass.Text);

            // Set border color for email text box based on validation result
            if (!isEmailValid)
            {
                txtEmail.Style["border-color"] = "#EF4444";
            }
            else
            {
                txtEmail.Style.Remove("border-color");
            }

            // Set border color for password text box based on validation result
            if (!isPassValid)
            {
                txtPass.Style["border-color"] = "#EF4444";
            }
            else
            {
                txtPass.Style.Remove("border-color");
            }

            // Return true if both text boxes are not empty, otherwise false
            return isEmailValid && isPassValid;
        }


        private string LoginUser(string email, string password)
        {// Logs in the user by querying the database with the provided email and password.
            // Returns the user ID if successful, otherwise returns null.
            string query = "SELECT user_id FROM [User] WHERE email = @Email AND password = @Password";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", password);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader.GetString(reader.GetOrdinal("user_id"));
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }


        private void UpdateLastLoginTime(string email)
        {// Updates the last login time for the user in the database.
            string updateQuery = "UPDATE [User] SET last_login = @LastLogin WHERE email = @Email";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@LastLogin", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void SetUserInfoCookie(string userId)
        {// Sets a cookie containing the user ID.
            HttpCookie userInfoCookie = new HttpCookie("userInfo");
            userInfoCookie["userId"] = userId.ToString();
            userInfoCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(userInfoCookie);
        }
    }
}