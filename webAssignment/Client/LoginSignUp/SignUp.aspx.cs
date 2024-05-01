using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Collections;

namespace webAssignment.Client.LoginSignUp
{
    public partial class SignUp : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                string userId = GenerateUserId();
                if (!string.IsNullOrEmpty(userId))
                {
                    if (InsertUser(userId))
                    {
                        Response.Write("<script>alert('User registered successfully!')</script>");
                    }
                    else
                    {
                        Response.Write("<script>alert('Error occurred while registering user.')</script>");
                    }
                }
                else
                {
                    Response.Write("<script>alert('Error generating user ID.')</script>");
                }
            }
        }

        private bool ValidateForm()
        {
            return ValidateTextBox(txtUsername) && ValidateTextBox(txtEmail) && ValidateTextBox(txtPass) && ValidateTextBox(txtConfirmPass) && ValidatePasswordMatch();
        }

        private bool ValidateTextBox(TextBox textBox)
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

        private bool ValidatePasswordMatch()
        {
            if (txtPass.Text != txtConfirmPass.Text)
            {
                passwordLabel.Text = "Passwords do not match. Please try again!";
                return false;
            }
            else
            {
                passwordLabel.Text = "";
                return true;
            }
        }

        private string GenerateUserId()
        {
            string userId = "";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string readQuery = "SELECT MAX(CAST(RIGHT(user_id, LEN(user_id) - 2) AS BIGINT)) FROM [User]";
                SqlCommand cmd = new SqlCommand(readQuery, conn);
                object result = cmd.ExecuteScalar();

                int lastUserId = result == DBNull.Value ? 0 : Convert.ToInt32(result);
                // Increment the numeric part
                int newNumericPart = lastUserId + 1;

                // Construct the new user ID with leading zeros
                userId = $"CS{newNumericPart:D4}";

                conn.Close();
            }

            return userId;
        }

        private bool InsertUser(string userId)
        {
            string defaultProfilePic = "~/ProfilePic/defaultPic.jpg";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string insertQuery = "INSERT INTO [User] (user_id, username, email, password, profile_pic_path, date_created) VALUES (@user_id, @username, @email, @password, @profile_pic_path, @date_created)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@password", txtPass.Text);
                cmd.Parameters.AddWithValue("@date_created", DateTime.Now.Date.ToString("MM/dd/yyyy"));
                cmd.Parameters.AddWithValue("@profile_pic_path", defaultProfilePic);
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

                return rowsAffected > 0;
            }
        }
    }
}