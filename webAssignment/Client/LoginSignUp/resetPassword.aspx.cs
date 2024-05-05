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
    public partial class WebForm2 : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        private void UpdatePassword(string userId, string newPassword)
        {
            string query = "UPDATE [User] SET password = @Password WHERE user_id = @UserId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Password", newPassword);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            if (ValidatePassword())
            {
                if (IsStrongPassword(txtConfirmPass.Text))
                {
                    string email = HttpUtility.UrlDecode(Request.QueryString["email"]);

                    string query = "SELECT user_id FROM [User] WHERE email = @email";

  
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@email", email );
                            conn.Open();
                            SqlDataReader reader = cmd.ExecuteReader();
                            if (reader.Read())
                            {
                                if (txtNewPass.Text == txtConfirmPass.Text)
                                {

                                    reader.Close();

                                    string hashedNewPassword = EncryptPassword(txtConfirmPass.Text);
                                    string updateQuery = "UPDATE [User] SET password = @Password WHERE email = @email";

                                    using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                    {
                                        updateCmd.Parameters.AddWithValue("@Password", hashedNewPassword);
                                        updateCmd.Parameters.AddWithValue("@email", email);
                                        int rowsAffected = updateCmd.ExecuteNonQuery();
                                        conn.Close();
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "showPopup", "showPopUp();", true);
                                    }
                                }
                                else
                                {
                                    lblChangePass.Text = "New password does not match! Plase try again!";
                                }
                            }
                        }
                    }
                }
                else
                {
                    lblChangePass.Text = "Please choose a stronger password! Your password should be at least 8 characters long and include a combination of uppercase and lowercase letters, digits, and special characters.";
                }
            }
            else
            {
                lblChangePass.Text = "Input fields cannot be empty!";
            }
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
        private bool ValidatePassword()
        {
            bool isNewPassValid = !string.IsNullOrEmpty(txtNewPass.Text);
            bool isConfirmPassValid = !string.IsNullOrEmpty(txtConfirmPass.Text);
            txtNewPass.Style["border-color"] = isNewPassValid ? string.Empty : "#EF4444";
            txtConfirmPass.Style["border-color"] = isConfirmPassValid ? string.Empty : "#EF4444";

            return isNewPassValid && isConfirmPassValid;
        }
        private bool IsStrongPassword(string password)
        {
            // Check minimum length
            if (password.Length < 8)
            {
                return false;
            }

            // Check for presence of uppercase letters
            if (!password.Any(char.IsUpper))
            {
                return false;
            }

            // Check for presence of lowercase letters
            if (!password.Any(char.IsLower))
            {
                return false;
            }

            // Check for presence of digits
            if (!password.Any(char.IsDigit))
            {
                return false;
            }

            // Check for presence of special characters
            if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            {
                return false;
            }

            // If all checks pass, return true
            return true;
        }
        protected void showPopUp_Click(object sender, EventArgs e)
        {
            popUpText.Style.Add("display", "flex");
        }

        protected void closePopUp_Click(object sender, EventArgs e)
        {
            popUpText.Style.Add("display", "none");
        }
        protected void btnContinue_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/LoginSignUp/Login.aspx");
        }
    }
}