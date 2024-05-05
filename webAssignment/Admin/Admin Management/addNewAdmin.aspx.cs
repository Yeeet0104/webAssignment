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
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Admin_Management
{
    public partial class addNewAdmin : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] == null)
                {
                    Response.Redirect("~/Client/LoginSignUp/AdminLogin.aspx");
                }
            }
        }

        protected void btnAddAdmin_Click(object sender, EventArgs e)
        {            
            if (ValidateForm())
            {
                if (IsValidEmail(txtEmail.Text))
                {
                    if (!CheckEmail(txtEmail.Text))
                    {
                        if (IsValidPhone(txtPhoneNo.Text))
                        {
                            string adminId = GenerateAdminId();
                            InsertAdmin(adminId);
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
                    lblErrorMsg.Text = "Invalid email format!";
                }
            }
            else
            {
                lblErrorMsg.Text = "Input fields cannot be empty!";
            }

        }

        private bool InsertAdmin(string adminId)
        {
            // Retrieve birthdate values from the dropdown lists
            int year = int.Parse(ddlYear.SelectedValue);
            string monthName = ddlMonth.SelectedValue;
            int day = int.Parse(ddlDay.SelectedValue);

            // Parse the month name and create a DateTime object
            DateTime tempDate = DateTime.ParseExact(monthName + " 1, " + year, "MMMM d, yyyy", null);
            int month = tempDate.Month;

            // Create a DateTime object from the selected values
            DateTime birth_date = new DateTime(year, month, day);

            // Get the uploaded file
            HttpPostedFile postedFile = fileUpload.PostedFile;
            string profile_pic_path = null;
            bool picAdded = false;

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
                picAdded = true;
            }

            // Hash the password before querying the database
            string hashedPassword = EncryptPassword(txtPhoneNo.Text);

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string insertQuery = "INSERT INTO [User] (user_id, first_name, last_name, email, password, profile_pic_path, date_created, birth_date, status, phone_number, role) VALUES (@user_id, @first_name, @last_name, @email, @password, @profile_pic_path, @date_created, @birth_date, @status, @phone_no, @role)";
                string defaultProfilePic = "~/ProfilePic/defaultPic.jpg";

                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@user_id", adminId);
                cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@last_name", txtLastName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@password", hashedPassword);
                cmd.Parameters.AddWithValue("@phone_no", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@date_created", DateTime.Now.Date.ToString("MM/dd/yyyy"));
                cmd.Parameters.AddWithValue("@birth_date", birth_date);
                cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue.ToString());

                if (adminManagerChk.Checked)
                {
                    cmd.Parameters.AddWithValue("@role", "Admin Manager");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@role", "Admin");
                }
                // Conditionally add the profile_pic_path parameter
                if (picAdded)
                {
                    cmd.Parameters.AddWithValue("@profile_pic_path", profile_pic_path);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@profile_pic_path", defaultProfilePic);
                }                              

                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();
                if (rowsAffected > 0)
                {
                    if (picAdded)
                    {
                        profilePic.ImageUrl = profile_pic_path + "?timestamp=" + DateTime.Now.Ticks;
                    }
                }               
                return rowsAffected > 0;
            }
        }

        private string GenerateAdminId()
        {
            string adminId = "";

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
                adminId = $"AD{newNumericPart:D4}";

                conn.Close();
            }

            return adminId;
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

    }
}