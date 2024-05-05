using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Profile
{
    public partial class AdminProfile : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
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
               
                            txtDay.Text = birthdate.Day.ToString();
                            txtMonth.Text = birthdate.ToString("MMMM");
                            txtYear.Text = birthdate.Year.ToString();
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
    }
}