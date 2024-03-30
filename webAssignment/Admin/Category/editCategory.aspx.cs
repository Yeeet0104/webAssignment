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
    public partial class editCategory : System.Web.UI.Page
    {
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
            int categoryID = int.Parse(DecryptString(encCateID));
            DataTable dummyData = GetDummyData();

            for ( int i = 0 ; i < dummyData.Rows.Count ; i++ )
            {
                DataRow row = dummyData.Rows[i];
                if ( int.Parse(row["CategoryID"].ToString()) == categoryID )
                {
                    editCategoryName.Text = row["CategoryName"].ToString();
                    editCategoryDes.Text = row["CategoryDec"].ToString();



                }

            }
        }
    }
}