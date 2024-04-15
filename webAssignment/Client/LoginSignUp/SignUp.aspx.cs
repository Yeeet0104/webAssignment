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

namespace webAssignment.Client.LoginSignUp
{
    public partial class SignUp : System.Web.UI.Page
    {
        private static int lastUserId = 1000;
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnSignUp_Click(object sender, EventArgs e)
        {
            ValidateTextBox(txtUsername);
            ValidateTextBox(txtEmail);
            ValidateTextBox(txtPass);
            ValidateTextBox(txtConfirmPass);

            if (txtPass.Text != txtConfirmPass.Text)
            {
                passwordLabel.Text = "Passwords do not match. Please try again!";
            }
            else
            {
                passwordLabel.Text = ""; 
            }

            if (ValidateTextBox(txtUsername) && ValidateTextBox(txtEmail) && ValidateTextBox(txtPass) && ValidateTextBox(txtConfirmPass))
            {
                string strCon = ConfigurationManager.ConnectionStrings["MainDatabaseConnection"].ConnectionString;
                using (SqlConnection conn = new SqlConnection(strCon))

                {
                    conn.Open();

                    lastUserId++;

                    string userId = "U" + lastUserId.ToString();

                    string insertQuery = "INSERT INTO [User] (user_id, username, email, password) VALUES (@user_id, @username, @email, @password)";
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.Parameters.AddWithValue("@user_id", userId);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@password", txtPass.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Data inserted successfully
                        Response.Write("<script>alert('User registered successfully!')</script>");
                    }
                    else
                    {
                        // Error occurred while inserting data
                        Response.Write("<script>alert('Error occurred while registering user.')</script>");
                    }

                    conn.Close();
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