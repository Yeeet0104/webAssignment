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

namespace webAssignment.Admin.Voucher
{
    public partial class voucher : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                productListView.DataSource = GetDummyData();

                productListView.DataBind();
            }
        }
        private DataTable GetDummyData( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("voucherCodeID", typeof(int));
            dummyData.Columns.Add("voucherCode", typeof(string));
            dummyData.Columns.Add("discount", typeof(int));
            dummyData.Columns.Add("max", typeof(int));
            dummyData.Columns.Add("min", typeof(int));
            dummyData.Columns.Add("start", typeof(DateTime));
            dummyData.Columns.Add("expiry", typeof(DateTime));
            dummyData.Columns.Add("created", typeof(DateTime));
            dummyData.Columns.Add("status", typeof(string));

            // Add rows with dummy data
            dummyData.Rows.Add(10234, "abcdDexter123", 15, 200, 50, DateTime.Now, DateTime.Now, DateTime.Now, "Active");
            dummyData.Rows.Add(10234, "abcdDexter124", 25, 150, 20, DateTime.Now, DateTime.Now, DateTime.Now, "Expired");
            dummyData.Rows.Add(10234, "abcdDexter125", 10, 250, 250, DateTime.Now, DateTime.Now, DateTime.Now, "Expired");
            dummyData.Rows.Add(10234, "abcdDexter126", 5, 2000, 1050, DateTime.Now, DateTime.Now, DateTime.Now, "Active");
            // Add more rows as needed for testing

            return dummyData;
        }

        protected void productListView_SelectedIndexChanged( object sender, EventArgs e )
        {

        }
        protected void productListView_ItemCommand( object sender, ListViewCommandEventArgs e )
        {
            if ( e.CommandName == "editVoucher" )
            {
                string voucherCodeID = e.CommandArgument.ToString();
                string encryptedStr = EncryptString(voucherCodeID);
                Response.Redirect($"~/Admin/Voucher/editVoucher.aspx?voucherCodeID={voucherCodeID}");
            }
            else if ( e.CommandName == "DeleteProduct" )
            {
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
        public void FilterListView( string searchTerm )
        {
            DataTable dummyData = GetDummyData();
            DataTable filteredData = FilterDataTable(dummyData, searchTerm);

            productListView.DataSource = filteredData;
            productListView.DataBind();
        }

        private DataTable FilterDataTable( DataTable dataTable, string searchTerm )
        {
            // Escape single quotes in the search term which can break the filter expression.
            string safeSearchTerm = searchTerm.Replace("'", "''");

            // Build a filter expression that checks if any of the columns contain the search term.
            string expression = string.Format(
                "Convert(OrderId, 'System.String') LIKE '%{0}%' OR " +
                "ProductName LIKE '%{0}%' OR " +
                "Convert(AdditionalProductsCount, 'System.String') LIKE '%{0}%' OR " +
                "Convert(Date, 'System.String') LIKE '%{0}%' OR " +
                "CustomerName LIKE '%{0}%' OR " +
                "Convert(Total, 'System.String') LIKE '%{0}%' OR " +
                "Convert(PaymentDate, 'System.String') LIKE '%{0}%' OR " +
                "Status LIKE '%{0}%'",
                safeSearchTerm);

            // Use the Select method to find all rows matching the filter expression.
            DataRow[] filteredRows = dataTable.Select(expression);

            // Create a new DataTable to hold the filtered rows.
            DataTable filteredDataTable = dataTable.Clone(); // Clone the structure of the table.

            // Import the filtered rows into the new DataTable.
            foreach ( DataRow row in filteredRows )
            {
                filteredDataTable.ImportRow(row);
            }

            return filteredDataTable;
        }
        protected void closePopUp_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
        }
        protected void btnCancelDelete_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
        }
    }
}