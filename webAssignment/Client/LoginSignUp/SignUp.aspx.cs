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
using System.Web.Security;
using System.Text;
using System.Security.Cryptography;

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
                if(!CheckEmail(txtEmail.Text))
                {                    
                    if (ValidatePasswordMatch())
                    {
                        if (IsStrongPassword(txtConfirmPass.Text))
                        {
                            string userId = GenerateUserId();
                            if (!string.IsNullOrEmpty(userId))
                            {
                                if (InsertUser(userId))
                                {                             
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "showPopup", "showPopUp();", true);
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
                        else
                        {
                            passwordLabel.Text = "Please choose a stronger password! Your password should be at least 8 characters long and include a combination of uppercase and lowercase letters, digits, and special characters.";
                        }
                    }
                    else
                    {
                        passwordLabel.Text = "Password does not match!";
                    }
                }
                else
                {                    
                    passwordLabel.Text = "Email already exists! Please try again.";
                }                
            }
            else
            {
                passwordLabel.Text = "Input fields cannot be empty!";
            }
        }

        private bool ValidateForm()
        {
            bool isUsernameValid = !string.IsNullOrEmpty(txtUsername.Text);
            bool isEmailValid = !string.IsNullOrEmpty(txtEmail.Text);
            bool isPassValid = !string.IsNullOrEmpty(txtPass.Text);
            bool isConfirmPassValid = !string.IsNullOrEmpty(txtConfirmPass.Text);

            txtUsername.Style["border-color"] = isUsernameValid ? string.Empty : "#EF4444";
            txtEmail.Style["border-color"] = isEmailValid ? string.Empty : "#EF4444";
            txtPass.Style["border-color"] = isPassValid ? string.Empty : "#EF4444";
            txtConfirmPass.Style["border-color"] = isConfirmPassValid ? string.Empty : "#EF4444";

            return isUsernameValid && isEmailValid && isPassValid && isConfirmPassValid;
        }
               
        private bool ValidatePasswordMatch()
        {
            return txtPass.Text == txtConfirmPass.Text;
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

        private bool CheckEmail(string email)
        {
            string query = "SELECT COUNT(*) FROM [User] WHERE email = @Email";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();
                    return count > 0;
                }
            }
        }

        private bool InsertUser(string userId)
        {
            string defaultProfilePic = "~/ProfilePic/defaultPic.jpg";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string password = EncryptPassword(txtPass.Text); // Encrypt the password

                string insertQuery = "INSERT INTO [User] (user_id, username, email, password, profile_pic_path, date_created, status, role) VALUES (@user_id, @username, @email, @password, @profile_pic_path, @date_created, @status, @role)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@user_id", userId);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@password", password);
                cmd.Parameters.AddWithValue("@date_created", DateTime.Now.Date.ToString("MM/dd/yyyy"));
                cmd.Parameters.AddWithValue("@profile_pic_path", defaultProfilePic);
                cmd.Parameters.AddWithValue("@status", "Active");
                cmd.Parameters.AddWithValue("@role", "User");
                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
                               
                return rowsAffected > 0;
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