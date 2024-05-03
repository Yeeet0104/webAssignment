using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.LoginSignUp
{
    public partial class AdminLogin : System.Web.UI.Page
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

                string result = LoginAdmin(email, hashedPassword);
                if (result != null)
                {
                    string[] resultArray = result.Split(';');
                    string userId = resultArray[0];
                    string role = resultArray[1];

                    if (role == "Admin" || role == "Admin Manager")
                    {                        
                        SetUserInfoCookie(userId);
                        Response.Redirect("~/Admin/Dashboard/Dashboard.aspx");
                    }
                    else
                    {
                        lblLoginMessage.Text = "Only Admins Can Access!";
                    }
                }
                else
                {
                    lblLoginMessage.Text = "Invalid email or password.";
                }
            }
            else
            {
                lblLoginMessage.Text = "Input fields cannot be empty!";
            }
        }

        private string LoginAdmin(string email, string hashedPassword)
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

        private bool ValidateForm()
        {
            bool isEmailValid = !string.IsNullOrEmpty(txtEmail.Text);
            bool isPassValid = !string.IsNullOrEmpty(txtPass.Text);

            txtEmail.Style["border-color"] = isEmailValid ? string.Empty : "#EF4444";
            txtPass.Style["border-color"] = isPassValid ? string.Empty : "#EF4444";

            return isEmailValid && isPassValid;
        }

        private void SetUserInfoCookie(string userId)
        {// Sets a cookie containing the user ID.
            HttpCookie userInfoCookie = new HttpCookie("userInfo");
            userInfoCookie["userId"] = userId.ToString();
            userInfoCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(userInfoCookie);
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
    }
}