using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
                if (Session["userId"] != null)
                {
                    string userId = Session["userId"].ToString();
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
            string userId = Session["userId"].ToString();
            if (ValidateForm())
            {
                if (IsValidEmail(txtEmail.Text))
                {
                    if (!CheckEmail(userId, txtEmail.Text))
                    {
                        if (IsValidPhone(txtPhoneNo.Text))
                        {                            
                            UpdateAdminDetails(userId);
                            Response.Redirect("~/Admin/Profile/AdminProfile.aspx");
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

        protected void btnChangePass_Click(object sender, EventArgs e)
        {
            if (ValidatePassword())
            {
                string userId = Session["userId"].ToString();
                string query = "SELECT password FROM [User] WHERE user_id = @UserId";

                // Hash the password before querying the database
                string hashedPassword = EncryptPassword(txtCurrentPass.Text);

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {

                        cmd.Parameters.AddWithValue("@UserId", userId);
                        conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            if (hashedPassword == reader["password"].ToString())
                            {
                                if (txtNewPass.Text == txtConfirmPass.Text)
                                {
                                    if (IsStrongPassword(txtConfirmPass.Text))
                                    {
                                        lblChangePass.Text = "Password changed successfuly!";
                                        reader.Close();

                                        string hashedNewPassword = EncryptPassword(txtConfirmPass.Text);
                                        string updateQuery = "UPDATE [User] SET password = @Password WHERE user_id = @UserId";

                                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                                        {
                                            updateCmd.Parameters.AddWithValue("@Password", hashedNewPassword);
                                            updateCmd.Parameters.AddWithValue("@UserId", userId);
                                            int rowsAffected = updateCmd.ExecuteNonQuery();
                                            conn.Close();
                                        }
                                        
                                    }
                                    else
                                    {
                                        lblChangePass.Text = "Please choose a stronger password! Your password should be at least 8 characters long and include a combination of uppercase and lowercase letters, digits, and special characters.";
                                    }
                                }
                                else
                                {
                                    lblChangePass.Text = "New password does not match! Plase try again!";
                                }
                            }
                            else
                            {
                                lblChangePass.Text = "Wrong password!";
                            }
                        }
                    }
                }
            }
            else
            {
                lblChangePass.Text = "Input fields cannot be empty!";
            }

        }

        private bool ValidatePassword()
        {
            bool isCurrentPassValid = !string.IsNullOrEmpty(txtCurrentPass.Text);
            bool isNewPassValid = !string.IsNullOrEmpty(txtNewPass.Text);
            bool isConfirmPassValid = !string.IsNullOrEmpty(txtConfirmPass.Text);

            txtCurrentPass.Style["border-color"] = isCurrentPassValid ? string.Empty : "#EF4444";
            txtNewPass.Style["border-color"] = isNewPassValid ? string.Empty : "#EF4444";
            txtConfirmPass.Style["border-color"] = isConfirmPassValid ? string.Empty : "#EF4444";

            return isCurrentPassValid && isNewPassValid && isConfirmPassValid;
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

        private bool ValidateForm()
        {
            bool isFirstNameValid = !string.IsNullOrEmpty(txtFirstName.Text);
            bool isLastNameValid = !string.IsNullOrEmpty(txtLastName.Text);
            bool isEmailValid = !string.IsNullOrEmpty(txtEmail.Text);
            bool isPhoneNoValid = !string.IsNullOrEmpty(txtPhoneNo.Text);

            txtFirstName.Style["border-color"] = isFirstNameValid ? string.Empty : "#EF4444";
            txtLastName.Style["border-color"] = isLastNameValid ? string.Empty : "#EF4444";
            txtEmail.Style["border-color"] = isEmailValid ? string.Empty : "#EF4444";
            txtPhoneNo.Style["border-color"] = isPhoneNoValid ? string.Empty : "#EF4444";

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
    }
}