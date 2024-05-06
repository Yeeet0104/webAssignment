using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Category
{
    public partial class editCategory : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                init();
            }
        }

        protected string DecryptString( string cipherText )
        {
            string EncryptionKey = "ABC123"; // Use the same key you used during encryption
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using ( Aes encryptor = Aes.Create() )
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using ( MemoryStream ms = new MemoryStream() )
                {
                    using ( CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write) )
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        private void init( )
        {
            string encCateID = Request.QueryString["CategoryID"];

            if ( string.IsNullOrEmpty(encCateID) )
            {
                Response.Redirect("~/Admin/Category/Category.aspx"); 
                return; 
            }

            string categoryID = DecryptString(encCateID);
            DataTable categoryData = getspecificCategoryData(categoryID);

            if ( categoryData.Rows.Count > 0 )
            {
                // categoryID is unique and only one row is returned
                DataRow row = categoryData.Rows[0]; 
                editCategoryName.Text = row["CategoryName"].ToString();
                tumbnail.ImageUrl = row["tumbnail_img_path"].ToString();
                editCategoryDes.Text = row["descriptions"].ToString();
            }
        }

        private DataTable getspecificCategoryData( string categoryID )
        {

            DataTable categoryData = new DataTable();

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {

                conn.Open();

                string sql = "SELECT category_id, category_name, tumbnail_img_path , descriptions FROM Category WHERE category_id = @categoryID";

                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {

                    cmd.Parameters.AddWithValue("@categoryID", categoryID);


                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {

                        categoryData.Load(reader);
                    }
                }
            }


            categoryData.Columns["category_id"].ColumnName = "CategoryID";
            categoryData.Columns["category_name"].ColumnName = "CategoryName";
            categoryData.Columns["tumbnail_img_path"].ColumnName = "tumbnail_img_path";
            categoryData.Columns["descriptions"].ColumnName = "descriptions";

            return categoryData;
        }

        protected void saveChanges_Click( object sender, EventArgs e )
        {
            string encCateID = Request.QueryString["CategoryID"];
            string categoryID = DecryptString(encCateID);
            DataTable categoryData = getspecificCategoryData(categoryID);

            if ( categoryData.Rows.Count > 0 )
            {
                DataRow row = categoryData.Rows[0];
                bool changesDetected = false;

                if ( editCategoryName.Text != row["CategoryName"].ToString() )
                {
                    changesDetected = true;
                    row["CategoryName"] = editCategoryName.Text;
                }
                if ( editCategoryDes.Text != row["descriptions"].ToString() )
                {
                    changesDetected = true;
                    row["descriptions"] = editCategoryDes.Text;
                }

                if ( getFileSavePath(false) != "No File" )  
                {
                    changesDetected = true;
                    row["tumbnail_img_path"] = getFileSavePath(true);
                }

                if ( changesDetected )
                {
                    UpdateCategoryData(row);
                    init();
                }
            }
        }
        
        private string getFileSavePath( bool saveToLocal ) {

            string fileName = "";

            if ( fileImages.HasFiles )
            {
                foreach ( HttpPostedFile postedFile in fileImages.PostedFiles )
                {
                    fileName = Path.GetFileName(postedFile.FileName);
                    string fileSavePath = Server.MapPath("~/CategoryBannerImg/") + fileName;
                    try
                    {
                        if ( saveToLocal ) { 
                            // Save the file.
                            postedFile.SaveAs(fileSavePath);
                            Debug.WriteLine("Saving file to: " + fileSavePath);
                        }
                    }
                    catch ( Exception ex )
                    {
                        Debug.WriteLine("Error: " + ex.Message);
                    }

                }
            }
            else
            {
                return "No File";
            }

            return "~/CategoryBannerImg/" + fileName;
        }
        private void UpdateCategoryData( DataRow row )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = @"
                            UPDATE Category SET 
                            category_name = @CategoryName,
                            descriptions = @Descriptions,
                            tumbnail_img_path = @tumbnail_img_path
                            WHERE category_id = @categoryID";

                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@categoryID", row["CategoryID"]);
                    cmd.Parameters.AddWithValue("@CategoryName", row["CategoryName"]);
                    cmd.Parameters.AddWithValue("@tumbnail_img_path", row["tumbnail_img_path"]);
                    cmd.Parameters.AddWithValue("@Descriptions", row["descriptions"]);
                    int result = cmd.ExecuteNonQuery();
                    if ( result > 0 )
                    {
                        ShowNotification("Succesfully Update Category", "success");
                    }
                    else
                    {
                        ShowNotification("Something when wrong ", "warning");

                    }
                }
            }
        }
        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ScriptManager.RegisterStartupScript(this, GetType(), "showSnackbar", $"showSnackbar('{message}', '{type}');", true);
        }
    }

}