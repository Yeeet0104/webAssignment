using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Category
{
    public partial class Category : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {

            if ( !IsPostBack )
            {
                categoryListView.DataSource = GetDummyData();

                categoryListView.DataBind();
            }
        }
        private DataTable GetDummyData( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("CategoryID", typeof(int));
            dummyData.Columns.Add("CategoryBanner", typeof(string));
            dummyData.Columns.Add("CategoryName", typeof(string));
            dummyData.Columns.Add("numberOfProd", typeof(int));
            dummyData.Columns.Add("PaymentDate", typeof(DateTime));
            dummyData.Columns.Add("Sold", typeof(int));
            dummyData.Columns.Add("Stock", typeof(int));

            // Add rows with dummy data
            dummyData.Rows.Add(10001, "~/Admin/Layout/image/DexProfilePic.jpeg", "Phone ", 2, DateTime.Now, 10, 10);
            dummyData.Rows.Add(10002, "~/Admin/Layout/image/DexProfilePic.jpeg", "Pc", 2, DateTime.Now, 10, 10);
            dummyData.Rows.Add(10003, "~/Admin/Layout/image/DexProfilePic.jpeg", "Laptop", 2, DateTime.Now, 10, 10);
            // Add more rows as needed for testing

            return dummyData;
        }

        protected void categoryListView_SelectedIndexChanged( object sender, EventArgs e )
        {

        }
        protected void categoryListView_ItemCommand( object sender, ListViewCommandEventArgs e )
        {
            if ( e.CommandName == "EditCategory" )
            {
                string categoryID = e.CommandArgument.ToString();
                string encryptedStr = EncryptString(categoryID);
                Response.Redirect($"~/Admin/Category/editCategory.aspx?CategoryID={encryptedStr}");
            }
            else if ( e.CommandName == "DeleteCategory" )
            {
                // Show the popup
                popUpDelete.Style.Add("display", "flex");

                // Set the Category Name in the label within the popup
                lblItemInfo.Text = e.CommandArgument.ToString();
            }
        }
        protected string EncryptString( string clearText )
        {
            string EncryptionKey = "ABC123"; // Replace with a more complex key and store securely
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using ( Aes encryptor = Aes.Create() )
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using ( MemoryStream ms = new MemoryStream() )
                {
                    using ( CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write) )
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        protected void categoryListView_ItemCommand1( object sender, ListViewCommandEventArgs e )
        {

        }

        protected void closePopUp_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
        }
        protected void btnCancelDelete_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
        }


        protected void getCategoryInfo( ) { 
           
        }
    }
}