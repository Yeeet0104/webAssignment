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
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDatabaseConnection"].ConnectionString))
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

            // Retrieve the file path of the selected profile picture
       //    string profilePicPath = "";
        //    if (fileUpload.HasFile)
        //    {
         //       string fileName = Path.GetFileName(fileUpload.FileName);
        //        string uploadDirectory = Server.MapPath("~/ProfilePictures/");
        //        if (!Directory.Exists(uploadDirectory))
         //       {
          //          Directory.CreateDirectory(uploadDirectory);
           //     }
         //       string uploadPath = Path.Combine(uploadDirectory, userId + "_" + fileName);
          //      fileUpload.SaveAs(uploadPath);

                // Instead of saving the local file path, generate a URL for accessing the profile picture
           //     string profilePicUrl = "/ProfilePictureHandler.ashx?userId=" + userId + "&fileName=" + fileName;
          //      profilePicPath = profilePicUrl;
         //   }


            // Update the corresponding record in the database with the new details
            string query = "UPDATE [User] SET first_name = @FirstName, last_name = @LastName, username = @UserName, email = @Email, phone_number = @PhoneNumber, birth_date = @BirthDate WHERE user_id = @UserId";

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["MainDatabaseConnection"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@BirthDate", birthDate);
               //     cmd.Parameters.AddWithValue("@ProfilePicPath", profilePicPath);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
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

    }
}