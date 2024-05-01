﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
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

        protected void btnLogin_Click( object sender, EventArgs e )
        {
            if (ValidateForm())
            {
                string email = txtEmail.Text;
                string password = txtPass.Text;

                string userId = LoginAdmin(email, password);
                if (userId != null)
                {
                    SetUserInfoCookie(userId.ToString());
                    Response.Redirect("~/Admin/Dashboard/dashboard.aspx");
                }
                else
                {
                    lblLoginMessage.Text = "Invalid email or password.";
                }
            }      
        }

        private string LoginAdmin(string email, string password)
        {// Logs in the user by querying the database with the provided email and password.
            // Returns the user ID if successful, otherwise returns null.
            string query = "SELECT user_id FROM [User] WHERE email = @Email AND password = @Password";

            using (SqlConnection conn = new SqlConnection(connectionString))
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

        private void SetUserInfoCookie(string userId)
        {// Sets a cookie containing the user ID.
            HttpCookie userInfoCookie = new HttpCookie("userInfo");
            userInfoCookie["userId"] = userId.ToString();
            userInfoCookie.Expires = DateTime.Now.AddDays(1);
            Response.Cookies.Add(userInfoCookie);
        }
    }
}