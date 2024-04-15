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
                return;
            }
            else
            {
                passwordLabel.Text = "";
                if (ValidateTextBox(txtUsername) && ValidateTextBox(txtEmail) && ValidateTextBox(txtPass) && ValidateTextBox(txtConfirmPass))
                {
                    string strCon = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                    using (SqlConnection conn = new SqlConnection(strCon))

                    {
                        conn.Open();

                        // Query to get the maximum user ID from the database
                        string readQuery = "SELECT MAX(CAST(RIGHT(user_id, LEN(user_id) - 1) AS INT)) FROM [User]";
                        SqlCommand cmd = new SqlCommand(readQuery, conn);
                        object result = cmd.ExecuteScalar();

                        int lastUserId = result == DBNull.Value ? 0 : Convert.ToInt32(result);
                        string userId = "U" + (lastUserId + 1).ToString();

                        string insertQuery = "INSERT INTO [User] (user_id, username, email, password) VALUES (@user_id, @username, @email, @password)";
                        cmd = new SqlCommand(insertQuery, conn);
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