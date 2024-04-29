using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            if (!IsPostBack)
            {
                init();
            }
        }

        private DataTable GetDummyData( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("CategoryID", typeof(int));
            dummyData.Columns.Add("CategoryBanner", typeof(string));
            dummyData.Columns.Add("CategoryDec", typeof(string));
            dummyData.Columns.Add("CategoryName", typeof(string));
            dummyData.Columns.Add("numberOfProd", typeof(int));
            dummyData.Columns.Add("PaymentDate", typeof(DateTime));
            dummyData.Columns.Add("Sold", typeof(int));
            dummyData.Columns.Add("Stock", typeof(int));

            // Add rows with dummy data
            dummyData.Rows.Add(10001, "~/Admin/Layout/image/DexProfilePic.jpeg","Is a iphone", "Phone ", 2, DateTime.Now, 10, 10);
            dummyData.Rows.Add(10002, "~/Admin/Layout/image/DexProfilePic.jpeg", "Is a PC", "Pc", 2, DateTime.Now, 10, 10);
            dummyData.Rows.Add(10003, "~/Admin/Layout/image/DexProfilePic.jpeg", "Is a Laptop", "Laptop", 2, DateTime.Now, 10, 10);
            // Add more rows as needed for testing

            return dummyData;
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
            string categoryID = DecryptString(encCateID);
            DataTable categoryData = getspecificCategoryData(categoryID);

            if ( categoryData.Rows.Count > 0 )
            {
                DataRow row = categoryData.Rows[0]; // Assuming categoryID is unique and only one row is returned
                editCategoryName.Text = row["CategoryName"].ToString();
                // Make sure "CategoryDec" is the correct column name
                tumbnail.ImageUrl = row["CategoryBanner"].ToString();
                editCategoryDes.Text = row["CategoryBanner"].ToString();
            }
        }
        private DataTable getspecificCategoryData(string categoryID)
        {

            DataTable categoryData = new DataTable();

            // Define the connection using the connection string
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                // Open the connection
                conn.Open();

                // SQL query to select data from the Category table
                string sql = "SELECT category_id, category_name, tumbnail_img_path FROM Category WHERE category_id = @categoryID";

                // Create a SqlCommand object
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    // Define the parameter and its value
                    cmd.Parameters.AddWithValue("@categoryID", categoryID);

                    // Execute the query and obtain a SqlDataReader
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        // Load data directly from the SqlDataReader to the DataTable
                        categoryData.Load(reader);
                    }
                }
            }

            // Rename the columns to match the GridView's DataFields
            categoryData.Columns["category_id"].ColumnName = "CategoryID";
            categoryData.Columns["category_name"].ColumnName = "CategoryName";
            categoryData.Columns["tumbnail_img_path"].ColumnName = "CategoryBanner";

            return categoryData;
        }
    }
}