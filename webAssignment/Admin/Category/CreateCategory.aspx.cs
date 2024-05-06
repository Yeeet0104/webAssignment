using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
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
                            postedFile.SaveAs(fileSavePath);
                        }
                        catch ( Exception ex )
                        {
                            Debug.WriteLine("Error: " + ex.Message);
                        }
                    }
                }

                String newCategoryID = GenerateNextCategoryId();
                ViewState["NewCateName"] = newCateName;
                ViewState["newCategoryID"] = newCategoryID;
                ViewState["FileSavePath"] = ( "~/CategoryBannerImg/" + fileName );

                popUpPanel.Style.Add("display", "flex");

                categoryID.Text = newCategoryID;
                categoryName.Text = newCateName;
                bannerImage.ImageUrl = ( "~/CategoryBannerImg/" + fileName );
            }
            else
            {

                ShowNotification("Please dont leave Empty input", "warning");
            }

        }
        private string GenerateNextCategoryId( )
        {
            string newCategoryId = "CAT1001";
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
                        int maxId = int.Parse(result.ToString().Substring(3));
                        maxId++;
                        newCategoryId = "CAT" + maxId.ToString("D3");
                    }
                }
                catch ( Exception ex )
                {
                    ShowNotification($"GenerateNextCategoryId Error:{ex.Message}", "warning");
                }
            }

            return newCategoryId;
        }

        private void InsertCategoryIntoDatabase( string categoryId, string categoryName, string imagePath )
        {
            string query = "INSERT INTO Category (category_id, category_name, tumbnail_img_path, date_added) VALUES (@CategoryId, @CategoryName, @ImagePath , @DateAdded)";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                using ( SqlCommand cmd = new SqlCommand(query, conn) )
                {
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@CategoryName", categoryName);
                    cmd.Parameters.AddWithValue("@ImagePath", imagePath);
                    cmd.Parameters.Add("@DateAdded", SqlDbType.DateTime).Value = DateTime.UtcNow;

                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();

                        if ( result < 0 )
                            ShowNotification("Error inserting data into Database!", "warning");
                    }
                    catch ( Exception ex )
                    {
                        ShowNotification($"Database Error:{ex.Message}", "warning");
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

                    string newCateName = ViewState["NewCateName"]?.ToString();
                    string newCategoryID = ViewState["newCategoryID"]?.ToString();
                    string fileSavePath = ViewState["FileSavePath"]?.ToString();

                    InsertCategoryIntoDatabase(newCategoryID, newCateName, fileSavePath);


                    popUpPanel.Style.Add("display", "none");

                    ViewState.Remove("NewCateName");
                    ViewState.Remove("NewCateDes");
                    ViewState.Remove("FileSavePath");

                    ShowNotification("Succesfully Added New Category", "success");
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


        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }
    }
}
