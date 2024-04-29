using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Category
{
    public partial class CreateCategory : System.Web.UI.Page
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        protected void Page_Load( object sender, EventArgs e )
        {

        }


        protected void addNewCategory( )
        {
            String newCateName = newCategoryName.Text.ToString();
            String newCateDes = newCategoryDes.Text.ToString();
            String fileSavePath = "";
            string fileName = "";
            if ( newCateName != "" && fileImages.HasFiles )
            {

                if ( fileImages.HasFiles )
                {
                    foreach ( HttpPostedFile postedFile in fileImages.PostedFiles )
                    {
                        fileName = Path.GetFileName(postedFile.FileName);
                        fileSavePath = Server.MapPath("~/CategoryBannerImg/") + fileName;
                        try
                        {
                            // Save the file.
                            postedFile.SaveAs(fileSavePath);
                            Debug.WriteLine("Saving file to: " + fileSavePath);
                            // You can add logic here to add the file details to your database if needed.
                        }
                        catch ( Exception ex )
                        {
                            Debug.WriteLine("Error: " + ex.Message);
                            // Handle the error
                        }
                    }
                    // After handling all files, you can redirect or update the page as needed.
                }

                String newCategoryID = GenerateNextCategoryId();
                ViewState["NewCateName"] = newCateName;
                ViewState["newCategoryID"] = newCategoryID;
                ViewState["FileSavePath"] = ( "~/CategoryBannerImg/" + fileName );

                // Show the popup for confirmation
                popUpPanel.Style.Add("display", "flex");

                // Set the text of the labels in the popup
                categoryID.Text = newCategoryID;
                categoryName.Text = newCateName;
                bannerImage.ImageUrl = ( "~/CategoryBannerImg/" + fileName );
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "notypeName", "showInputNameError();", true);
            }

        }
        private string GenerateNextCategoryId( )
        {
            string newCategoryId = "C0001"; // default if no entries are present
            string query = "SELECT MAX(category_id) as MaxCategoryId FROM Category";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                SqlCommand cmd = new SqlCommand(query, conn);

                try
                {
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    if ( result != DBNull.Value && result != null )
                    {
                        int maxId = int.Parse(result.ToString().Substring(1)); // Assuming IDs are like 'C0001'
                        maxId++; // Increment the numerical part
                        newCategoryId = "C" + maxId.ToString("D4"); // Format as C + four digit number
                    }
                }
                catch ( Exception ex )
                {
                    // Handle exceptions (logging, throw further, etc.)
                    Console.WriteLine("Error in GenerateNextCategoryId: " + ex.Message);
                }
            }

            return newCategoryId;
        }

        private void InsertCategoryIntoDatabase( string categoryId, string categoryName, string imagePath )
        {
            string query = "INSERT INTO Category (category_id, category_name, tumbnail_img_path) VALUES (@CategoryId, @CategoryName, @ImagePath)";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                using ( SqlCommand cmd = new SqlCommand(query, conn) )
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath);

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();

                        // Check Error
                        if ( result < 0 )
                            Debug.WriteLine("Error inserting data into Database!");
                        else
                            Debug.WriteLine("Data inserted successfully!");
                    }
                    catch ( Exception ex )
                    {
                        Debug.WriteLine("Database Error: " + ex.Message);
                        // Handle more specific database errors if needed
                    }
                }
            }
        }

        protected void btnAddNewCust_Click( object sender, EventArgs e )
        {

            addNewCategory();
            newCategoryName.Text = string.Empty;
            newCategoryDes.Text = string.Empty;
        }


        protected void closePopUp_Click( object sender, EventArgs e )
        {
            popUpPanel.Style.Add("display", "none");

            ViewState.Remove("NewCateName");
            ViewState.Remove("NewCateDes");
            ViewState.Remove("FileSavePath");
        }

        protected void btnConfirmAdd_Click( object sender, EventArgs e )
        {

            if ( passwordForConfirm.Text.ToString() != "" )
            {
                if ( Page.IsValid && passwordForConfirm.Text.ToString() == "12345" )
                {
                    // Retrieve the stored values
                    string newCateName = ViewState["NewCateName"]?.ToString();
                    string newCategoryID = ViewState["newCategoryID"]?.ToString();
                    string fileSavePath = ViewState["FileSavePath"]?.ToString();

                    // Perform the insertion using these values
                    InsertCategoryIntoDatabase(newCategoryID, newCateName, fileSavePath);

                    // Optionally, close the popup
                    // popUpPanel.Style.Add("display", "none");

                    popUpPanel.Style.Add("display", "none");

                    // Clear the ViewState
                    ViewState.Remove("NewCateName");
                    ViewState.Remove("NewCateDes");
                    ViewState.Remove("FileSavePath");
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "passwordError", "showPasswordError();", true);
                }

            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "noinputpassword", "showInputPasswordError();", true);
            }
        }

        protected void btnCancelDelete_Click( object sender, EventArgs e )
        {
            popUpPanel.Style.Add("display", "none");

            ViewState.Remove("NewCateName");
            ViewState.Remove("NewCateDes");
            ViewState.Remove("FileSavePath");
        }
    }
}
