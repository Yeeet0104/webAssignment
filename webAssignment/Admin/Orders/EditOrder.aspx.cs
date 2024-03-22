using System;
using System.Collections.Generic;
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
                string orderId = Request.QueryString["OrderID"];
                if ( !string.IsNullOrEmpty(orderId) )
                {
                    // Use this order ID to fetch data from the database
                    string decryptedOrderID = DecryptString(orderId);
                    LoadOrderData(decryptedOrderID);
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
    }
}