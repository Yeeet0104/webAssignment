using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Profile
{
    public partial class editProfile : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load( object sender, EventArgs e )
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (Request.Cookies["userInfo"] != null)
                {
                    string userId = Request.Cookies["userInfo"]["userID"];
                    LoadAdminDetails(userId);
                }
                else
                {
                    // Handle the case where the user is not logged in
                    Response.Redirect("~/Client/LoginSignUp/AdminLogin.aspx");
                }
            }
        }

        private void LoadAdminDetails(string adminId)
        {
            // Query the database to retrieve the user's current account details based on the user ID
            string query = "SELECT * FROM [User] WHERE user_id = @UserId";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@UserId", adminId);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        // Populate the form fields with the retrieved account details
                        txtFirstName.Text = reader["first_name"].ToString();
                        txtLastName.Text = reader["last_name"].ToString();
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

        private void UpdateAdminDetails(string adminId)
        {
            // Retrieve updated account details from the form fields
            string firstName = txtFirstName.Text;
            string lastName = txtLastName.Text;
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
            string profile_pic_path = null; // Default to null if no file is uploaded

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

            // Update the corresponding record in the database with the new details
            string query = "UPDATE [User] SET first_name = @FirstName, last_name = @LastName, email = @Email, phone_number = @PhoneNumber, birth_date = @BirthDate";

            // Conditionally add the @ProfilePicPath parameter
            if (profile_pic_path != null)
            {
                query += ", profile_pic_path = @ProfilePicPath";
            }

            query += " WHERE user_id = @UserId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@BirthDate", birthDate);

                    // Conditionally add the @ProfilePicPath parameter
                    if (profile_pic_path != null)
                    {
                        cmd.Parameters.AddWithValue("@ProfilePicPath", profile_pic_path);
                    }

                    cmd.Parameters.AddWithValue("@UserId", adminId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }


        protected void closePopUp_Click( object sender, EventArgs e )
        {
            popUpConfirmation.Style.Add("display", "none");
        }
        protected void btnCancelEdit_Click( object sender, EventArgs e )
        {
            popUpConfirmation.Style.Add("display", "none");
        }

        protected void btnEdit_Click( object sender, EventArgs e )
        {
            popUpConfirmation.Style.Add("display", "flex");
        }

        protected void btnSaveChanges_Click(object sender, EventArgs e)
        {
            string userId = Request.Cookies["userInfo"]["userID"];
            UpdateAdminDetails(userId);
            Response.Redirect("~/Admin/Profile/AdminProfile.aspx");
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