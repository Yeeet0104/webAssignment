using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Admin_Management
{
    public partial class EditAdmin : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the user ID from the query string
                string userId = Request.QueryString["userId"];
                if (userId != null)
                {
                    // Load the address details corresponding to the address ID
                    LoadUserForEdit(userId);

                    if (lblStatus.Text == "Active")
                    {
                        lblStatus.CssClass = "rounded-xl font-medium flex items-center px-3 py-1.5 text-sm text-green-700 bg-green-100";
                    }
                    else
                    {
                        lblStatus.CssClass = "rounded-xl font-medium flex items-center px-3 py-1.5 text-sm text-red-700 bg-red-100";
                    }
                }
            }
        }
        private void LoadUserForEdit(string userId)
        {

            if (string.IsNullOrEmpty(userId))
            {
                // Handle the case where the addressId is not provided
                return;
            }

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
                        txtEditFirstName.Text = reader["first_name"].ToString();
                        txtEditLastName.Text = reader["last_name"].ToString();
                        txtEditEmail.Text = reader["email"].ToString();
                        txtEditPhoneNo.Text = reader["phone_number"].ToString();
                        ddlStatus.SelectedValue = (reader["status"].ToString());
                        lblStatus.Text = reader["status"].ToString();
                        ddlRole.SelectedValue = (reader["role"].ToString());
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
                    reader.Close();
                }
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                lblStatus.Text = ddlStatus.SelectedItem.Text;
                if (ddlStatus.SelectedItem.Text == "Blocked")
                {
                    // Add CSS class to change color to red
                    lblStatus.CssClass = "rounded-xl font-medium flex items-center px-3 py-1.5 text-sm text-red-700 bg-red-100";
                }
                else
                {
                    // Reset CSS class for other statuses
                    lblStatus.CssClass = "rounded-xl font-medium flex items-center px-3 py-1.5 text-sm text-green-700 bg-green-100";
                }
            }
        }

        protected void btnSaveDetails_Click(object sender, EventArgs e)
        {
            string userId = Request.QueryString["userId"];
            if (ValidateForm())
            {
                if (IsValidEmail(txtEditEmail.Text))
                {
                    if (!CheckEmail(userId, txtEditEmail.Text))
                    {
                        if (IsValidPhone(txtEditPhoneNo.Text))
                        {
                            UpdateUserDetails(userId);
                            Response.Redirect("~/Admin/Admin Management/adminManagement.aspx");
                        }
                        else
                        {
                            lblErrorMsg.Text = "Invalid phone number!";
                        }
                    }
                    else
                    {
                        lblErrorMsg.Text = "Email already exists! Please try again.";
                    }
                }
                else
                {
                    lblErrorMsg.Text = "Invalid email!";
                }
            }
            else
            {
                lblErrorMsg.Text = "Input fields cannot be empty!";
            }
        }

        private void UpdateUserDetails(string userId)
        {
            string first_name = txtEditFirstName.Text;
            string last_name = txtEditLastName.Text;
            string email = txtEditEmail.Text;
            string phone_no = txtEditPhoneNo.Text;
            string status = ddlStatus.SelectedValue.ToString();
            string role = ddlRole.SelectedValue.ToString();

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
            string query = "UPDATE [User] SET first_name = @FirstName, last_name = @LastName, email = @Email, phone_number = @PhoneNumber, birth_date = @BirthDate, status = @Status, role = @Role";

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
                    cmd.Parameters.AddWithValue("@FirstName", first_name);
                    cmd.Parameters.AddWithValue("@LastName", last_name);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phone_no);
                    cmd.Parameters.AddWithValue("@BirthDate", birthDate);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Role", role);
                    // Conditionally add the @ProfilePicPath parameter
                    if (profile_pic_path != null)
                    {
                        cmd.Parameters.AddWithValue("@ProfilePicPath", profile_pic_path);
                    }

                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
        }

        private bool ValidateForm()
        {
            bool isFirstNameValid = !string.IsNullOrEmpty(txtEditFirstName.Text);
            bool isLastNameValid = !string.IsNullOrEmpty(txtEditLastName.Text);
            bool isEmailValid = !string.IsNullOrEmpty(txtEditEmail.Text);
            bool isPhoneNoValid = !string.IsNullOrEmpty(txtEditPhoneNo.Text);

            txtEditFirstName.Style["border-color"] = isFirstNameValid ? string.Empty : "#EF4444";
            txtEditLastName.Style["border-color"] = isLastNameValid ? string.Empty : "#EF4444";
            txtEditEmail.Style["border-color"] = isEmailValid ? string.Empty : "#EF4444";
            txtEditPhoneNo.Style["border-color"] = isPhoneNoValid ? string.Empty : "#EF4444";

            return isFirstNameValid && isLastNameValid && isEmailValid && isPhoneNoValid;
        }

        public static bool IsValidEmail(string email)
        {
            // Define a regular expression pattern for validating email addresses
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Use the IsMatch method to see if the email matches the pattern
            return regex.IsMatch(email);
        }

        private bool IsValidPhone(string phoneNumber)
        {
            string pattern = @"^\d{10,15}$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(phoneNumber);
        }

        private bool CheckEmail(string userId, string email)
        {
            string query = "SELECT COUNT(*) FROM [User] WHERE email = @Email AND user_Id != @UserId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    conn.Close();
                    return count > 0;
                }
            }
        }
    }
}