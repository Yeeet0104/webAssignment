using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Admin_Management
{
    public partial class addNewAdmin : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAddAdmin_Click(object sender, EventArgs e)
        {
            string adminId = GenerateAdminId();
            InsertAdmin(adminId);
            Response.Redirect("~/Admin/Admin Management/adminManagement.aspx");
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

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string insertQuery = "INSERT INTO [User] (user_id, first_name, last_name, email, password, profile_pic_path, date_created, birth_date, status, phone_number) VALUES (@user_id, @first_name, @last_name, @email, @password, @profile_pic_path, @date_created, @birth_date, @status, @phone_no)";
                SqlCommand cmd = new SqlCommand(insertQuery, conn);
                cmd.Parameters.AddWithValue("@user_id", adminId);
                cmd.Parameters.AddWithValue("@first_name", txtFirstName.Text);
                cmd.Parameters.AddWithValue("@last_name", txtLastName.Text);
                cmd.Parameters.AddWithValue("@email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@password", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@phone_no", txtPhoneNo.Text);
                cmd.Parameters.AddWithValue("@date_created", DateTime.Now.Date.ToString("MM/dd/yyyy"));
                cmd.Parameters.AddWithValue("@profile_pic_path", profile_pic_path);
                cmd.Parameters.AddWithValue("@birth_date", birth_date);
                cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedValue.ToString());


                int rowsAffected = cmd.ExecuteNonQuery();
                conn.Close();

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