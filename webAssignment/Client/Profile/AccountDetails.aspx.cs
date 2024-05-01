using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Profile
{
    public partial class AccountDetails : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (Request.Cookies["userInfo"] != null)
                {
                    string userId = Request.Cookies["userInfo"]["userID"];
                    LoadUserDetails(userId);
                }
                else
                {
                    // Handle the case where the user is not logged in
                    Response.Redirect("~/Client/LoginSignUp/Login.aspx");
                }
            }
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            try
            {
                // Retrieve user ID from the cookie
                string userId = Request.Cookies["userInfo"]["userID"];
                UpdateUserDetails(userId);
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void LoadUserDetails(string userId)
        {
            // Query the database to retrieve the user's current account details based on the user ID
            string query = "SELECT * FROM [User] WHERE user_id = @UserId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate the form fields with the retrieved account details
                        txtFirstName.Text = reader["first_name"].ToString();
                        txtLastName.Text = reader["last_name"].ToString();
                        txtUsername.Text = reader["username"].ToString();
                        txtEmail.Text = reader["email"].ToString();
                        txtPhoneNo.Text = reader["phone_number"].ToString();

                        // Check if the birthdate field is null
                        if (reader["birth_date"] != DBNull.Value)
                        {
                            // Retrieve the birthdate from the database
                            DateTime birthdate = Convert.ToDateTime(reader["birth_date"]);

                            // Select the corresponding items in the dropdown lists
                            ddlDay.SelectedValue = birthdate.Day.ToString();
                            ddlMonth.SelectedValue = birthdate.ToString("MMMM");
                            ddlYear.SelectedValue = birthdate.Year.ToString();
                        }

                        if (reader["profile_pic_path"] != DBNull.Value)
                        {
                            string profilePicPath = reader["profile_pic_path"].ToString();
                            profilePic.ImageUrl = profilePicPath;
                        }
                    }
                }
            }
        }

        private void UpdateUserDetails(string userId)
        {
            // Retrieve updated account details from the form fields
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string phoneNumber = txtPhoneNo.Text;

            // Retrieve birthdate values from the dropdown lists
            int year = int.Parse(ddlYear.SelectedValue);
            string monthName = ddlMonth.SelectedValue;
            int day = int.Parse(ddlDay.SelectedValue);

            // Parse the month name and create a DateTime object
            DateTime tempDate = DateTime.ParseExact(monthName + " 1, " + year, "MMMM d, yyyy", null);
            int month = tempDate.Month;

            // Create a DateTime object from the selected values
            DateTime birthDate = new DateTime(year, month, day);

            // Get the uploaded file
            HttpPostedFile postedFile = fileUpload.PostedFile;
            string profile_pic_path = "";

            // Check if a file was uploaded
            if (postedFile != null && postedFile.ContentLength > 0)
            {
                // Get the file extension
                string fileExtension = Path.GetExtension(postedFile.FileName);

                // Generate a unique file name
                string fileName = Guid.NewGuid().ToString() + fileExtension;

                string profileImgFolderPath = Server.MapPath("~/ProfilePic/");
                if (!Directory.Exists(profileImgFolderPath))
                {
                    Directory.CreateDirectory(profileImgFolderPath);
                }

                // Define the path to save the file
                string filePath = Server.MapPath("~/ProfilePic/") + fileName;

                // Save the file to the specified path
                postedFile.SaveAs(filePath);

                // Save the file path to the database
                profile_pic_path = "~/ProfilePic/" + fileName;
            }
            else
            {
                // No file was uploaded, leave the existing profile picture
                profile_pic_path = null;
            }

            // Update the corresponding record in the database with the new details
            string query = "UPDATE [User] SET first_name = @FirstName, last_name = @LastName, username = @UserName, email = @Email, phone_number = @PhoneNumber, birth_date = @BirthDate, profile_pic_path = @ProfilePicPath WHERE user_id = @UserId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@BirthDate", birthDate);
                    cmd.Parameters.AddWithValue("@ProfilePicPath", profile_pic_path);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAffected > 0)
                    {
                        // Account details updated successfully
                        lblUpdateDetailsMessage.Text = "Account details updated successfully!";
                    }
                    else
                    {
                        // Failed to update account details
                        lblUpdateDetailsMessage.Text = "Failed to update account details.";
                    }
                }
            }
        }

        private void HandleException(Exception ex)
        {
            // Handle the exception and provide feedback to the user
            lblUpdateDetailsMessage.Text = "An error occurred while updating account details: " + ex.Message;
        }
               
        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            string userId = Request.Cookies["userInfo"]["userID"];
            string query = "SELECT password FROM [User] WHERE user_id = @UserId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        if (txtCurrentPass.Text == reader["password"].ToString())
                        {
                            if (txtNewPass.Text == txtConfirmPass.Text)
                            {
                                reader.Close();
                                string updateQuery = "UPDATE [User] SET password = @Password WHERE user_id = @UserId";

                                using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                {
                                    updateCmd.Parameters.AddWithValue("@Password", txtNewPass.Text);
                                    updateCmd.Parameters.AddWithValue("@UserId", userId);
                                    int rowsAffected = updateCmd.ExecuteNonQuery();
                                    conn.Close();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}