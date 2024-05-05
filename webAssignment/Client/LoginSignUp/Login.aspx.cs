using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace webAssignment.Client.LoginSignUp
{
    public partial class Login : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                string email = txtEmail.Text;
                string password = txtPass.Text;

                // Hash the password before querying the database
                string hashedPassword = EncryptPassword(password);
                if (CheckStatus(email, hashedPassword) != "Blocked")
                {
                    string result = LoginUser(email, hashedPassword);
                    if (result != null)
                    {
                        string[] resultArray = result.Split(';');
                        string userId = resultArray[0];
                        string role = resultArray[1];

                        if (role == "User")
                        {
                            UpdateLastLoginTime(email);
                            SetUserSession(userId);
                            Response.Redirect("~/Client/Home/HomePage.aspx");

                        }
                        else
                        {
                            lblLoginMessage.Text = "Admin or other roles cannot access this page.";
                        }
                    }
                    else
                    {
                        lblLoginMessage.Text = "Invalid email or password.";
                    }
                }
                else
                {
                    lblLoginMessage.Text = "Your account has been blocked! Please contact customer support.";
                }
            }
            else
            {
                lblLoginMessage.Text = "Input fields cannot be empty!";
            }
        }
        private bool ValidateForm()
        {
            bool isEmailValid = !string.IsNullOrEmpty(txtEmail.Text);
            bool isPassValid = !string.IsNullOrEmpty(txtPass.Text);

            txtEmail.Style["border-color"] = isEmailValid ? string.Empty : "#EF4444";
            txtPass.Style["border-color"] = isPassValid ? string.Empty : "#EF4444";

            return isEmailValid && isPassValid;
        }

        private string LoginUser(string email, string hashedPassword)
        {
            string query = @"SELECT user_id, role FROM [User] WHERE email = @Email AND password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Return both user_id and role
                        return reader["user_id"].ToString() + ";" + reader["role"].ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        private string CheckStatus(string email, string hashedPassword)
        {
            string query = @"SELECT status FROM [User] WHERE email = @Email AND password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        return reader["status"].ToString();
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

            using (SqlConnection conn = new SqlConnection(connectionString))
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

        private void SetUserSession(string userId)
        {// Set a session variable containing the user ID.
            Session["userId"] = userId.ToString();
        }

        private string EncryptPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        protected void btnAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/LoginSignUp/AdminLogin.aspx");
        }
    }
}