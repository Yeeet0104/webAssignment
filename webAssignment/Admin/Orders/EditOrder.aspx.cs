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

namespace webAssignment.Admin.Orders
{
    public partial class EditOrder : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {

            if ( !IsPostBack )
            {
                ordersListView.DataSource = GetDummyData();

                ordersListView.DataBind();
                string orderId = Request.QueryString["OrderID"];
                if ( !string.IsNullOrEmpty(orderId) )
                {
                    // Use this order ID to fetch data from the database
                    //string decryptedOrderID = DecryptString(orderId);
                   // LoadOrderData(decryptedOrderID);


                }
            }
        }

        private void LoadOrderData( string decryptedOrderID )
        {

            testing.Text = decryptedOrderID;
            // Database operations to fetch the order details by order ID
            // Populate the controls on the page with the retrieved data
        }

        // Decryption
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


        private DataTable GetDummyData( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("OrderId", typeof(int));
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("ProductName", typeof(string));
            dummyData.Columns.Add("AdditionalProductsCount", typeof(int));
            dummyData.Columns.Add("SKU", typeof(int));
            dummyData.Columns.Add("quantity", typeof(int));
            dummyData.Columns.Add("price", typeof(decimal));
            dummyData.Columns.Add("total", typeof(decimal));
            dummyData.Columns.Add("subtotal", typeof(decimal));
           
            // Add rows with dummy data
            dummyData.Rows.Add(10234, "~/Admin/Layout/image/DexProfilePic.jpeg","dexter",3 , 302011 , 2 , 123.22 , 123.22, 6999);
            dummyData.Rows.Add(10234, "~/Admin/Layout/image/DexProfilePic.jpeg","dexter",3 , 302011 , 2 , 123.22 , 123.22, 6999);
            dummyData.Rows.Add(10234, "~/Admin/Layout/image/DexProfilePic.jpeg","dexter",3 , 302011 , 2 , 123.22 , 123.22,6999);
            dummyData.Rows.Add(10234, "~/Admin/Layout/image/DexProfilePic.jpeg","dexter",3 , 302011 , 2 , 123.22 , 123.22,6999);
            dummyData.Rows.Add(10234, "~/Admin/Layout/image/DexProfilePic.jpeg","dexter",3 , 302011 , 2 , 123.22 , 123.22,6999);
            // Add more rows as needed for testing

            return dummyData;
        }
    }
}