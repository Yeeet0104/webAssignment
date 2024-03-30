using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;

namespace webAssignment
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                bestSellingItemLv.DataSource = GetDummyDataBestSeller();
                bestSellingItemLv.DataBind();
                ordersListView.DataSource = GetDummyData();

                ordersListView.DataBind();
            }
        }
        private DataTable GetDummyData( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("OrderId", typeof(int));
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("ProductName", typeof(string));
            dummyData.Columns.Add("AdditionalProductsCount", typeof(int));
            dummyData.Columns.Add("Order Date", typeof(DateTime));
            dummyData.Columns.Add("CustomerName", typeof(string));
            dummyData.Columns.Add("Total", typeof(decimal));
            dummyData.Columns.Add("PaymentDate", typeof(DateTime));
            dummyData.Columns.Add("Status", typeof(string));

            // Add rows with dummy data
            dummyData.Rows.Add(10234, "~/Admin/Layout/image/DexProfilePic.jpeg", "Product 1", 2, DateTime.Now, "John Doe", 150.00m, DateTime.Now, "Processing");
            dummyData.Rows.Add(10235, "~/Admin/Dashboard/Images/iphone11.jpg", "Product 2", 3, DateTime.Now, "Jane Smith", 200.00m, DateTime.Now, "Shipped");
            dummyData.Rows.Add(10235, "~/Admin/Dashboard/Images/iphone11.jpg", "Product 2", 3, DateTime.Now, "Jane Smith", 200.00m, DateTime.Now, "Shipped");
            dummyData.Rows.Add(10235, "~/Admin/Dashboard/Images/iphone11.jpg", "Product 2", 3, DateTime.Now, "Jane Smith", 200.00m, DateTime.Now, "Shipped");
            dummyData.Rows.Add(10235, "~/Admin/Dashboard/Images/iphone11.jpg", "Product 2", 3, DateTime.Now, "Jane Smith", 200.00m, DateTime.Now, "Shipped");
            // Add more rows as needed for testing

            return dummyData;
        }

        protected void ordersListView_SelectedIndexChanged( object sender, EventArgs e )
        {

        }
        protected void OrdersListView_ItemCommand( object sender, ListViewCommandEventArgs e )
        {
            if ( e.CommandName == "EditOrder" )
            {
                string orderId = e.CommandArgument.ToString();
                string encryptedStr = EncryptString(orderId);
                Response.Redirect($"~/Admin/orders/EditOrder.aspx?OrderID={encryptedStr}");
            }
            else if ( e.CommandName == "DeleteOrder" )
            {
                // Show the popup
                //popUpDelete.Style.Add("display", "flex");

                //// Set the Order ID in the label within the popup
                //lblItemInfo.Text = e.CommandArgument.ToString();
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

        private DataTable GetDummyDataBestSeller( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("productName", typeof(string));
            dummyData.Columns.Add("productVariant", typeof(string));
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("sold", typeof(int));
            dummyData.Columns.Add("price", typeof(int));
            dummyData.Columns.Add("profit", typeof(int));

            // Add rows with dummy data
            dummyData.Rows.Add("Iphone 30 ", " pro max gao", "~/Admin/Layout/image/DexProfilePic.jpeg", 350 , 10 , (350*10));
            dummyData.Rows.Add("Iphone 30 ", " max", "~/Admin/Layout/image/DexProfilePic.jpeg", 350 , 10 , (350*10));
            dummyData.Rows.Add("Iphone 30 ", " pro max", "~/Admin/Layout/image/DexProfilePic.jpeg", 350 , 10 , (350*10));


            // Add more rows as needed for testing

            return dummyData;
        }
    }
}