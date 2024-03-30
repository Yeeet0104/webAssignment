using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Transaction
{
    public partial class transaction : System.Web.UI.Page
    {
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                transactionListView.DataSource = GetDummyData();

                transactionListView.DataBind();
            }
        }
        private DataTable GetDummyData( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("transactionID", typeof(int));
            dummyData.Columns.Add("CustomerID", typeof(int));
            dummyData.Columns.Add("UserName", typeof(string));
            dummyData.Columns.Add("Amount", typeof(decimal));
            dummyData.Columns.Add("paymentMethod", typeof(string));
            dummyData.Columns.Add("Date", typeof(DateTime));
            dummyData.Columns.Add("ProductName", typeof(string[]));
            dummyData.Columns.Add("Variant", typeof(string[]));
            dummyData.Columns.Add("Price", typeof(int[]));

            string[] productNames = { "Iphone 11" ,"Msi laptop" , "Dell desktop" };
            string[] variants = { "256GB", "512GB", "1TB" };
            int[] prices = { 999, 1199, 1399 };
            double total = 0;
            for (int i = 0;i<prices.Length ; i++ )
            {
                total  += prices[i];    
            }

            // Add rows with dummy data
            dummyData.Rows.Add(3001,2001, "Dexter", total, "credit", DateTime.Now,productNames,variants,prices);
            dummyData.Rows.Add(3001,2001, "Dexter", total, "credit", DateTime.Now,productNames,variants,prices);
            dummyData.Rows.Add(3001,2001, "Dexter", total, "credit", DateTime.Now,productNames,variants,prices);
            dummyData.Rows.Add(3001,2001, "Dexter", total, "credit", DateTime.Now,productNames,variants,prices);



            return dummyData;
        }

        protected void transactionListView_SelectedIndexChanged( object sender, EventArgs e )
        {

        }
        protected void transactionListView_ItemCommand( object sender, ListViewCommandEventArgs e )
        {
            if ( e.CommandName == "deleteTransaction" )
            {
                // Show the popup
                popUpDelete.Style.Add("display", "flex");

                string commandArgument = e.CommandArgument.ToString();
                string[] arguments = commandArgument.Split(',');
                if ( arguments.Length == 2 )
                {
                    lbltransIDInfo.Text = arguments[0];
                    lblcusIDInfo.Text = arguments[1];
                    // Now, you can use transactionID and userName as needed
                }

                // Set the Order ID in the label within the popup
            }
        }
        public void FilterListView( string searchTerm )
        {
            DataTable dummyData = GetDummyData();
            DataTable filteredData = FilterDataTable(dummyData, searchTerm);

            transactionListView.DataSource = filteredData;
            transactionListView.DataBind();
        }

        private DataTable FilterDataTable( DataTable dataTable, string searchTerm )
        {
            // Escape single quotes in the search term which can break the filter expression.
            string safeSearchTerm = searchTerm.Replace("'", "''");

            // Build a filter expression that checks if any of the columns contain the search term.
            string expression = string.Format(
                "Convert(CustomerID, 'System.String') LIKE '%{0}%' OR " +
                "UserName LIKE '%{0}%' OR " +
                "Convert(Date, 'System.String') LIKE '%{0}%' OR " +
                "Description LIKE '%{0}%' OR " +
                "Convert(Amount, 'System.String') LIKE '%{0}%' OR " +
                "paymentMethod LIKE '%{0}%'",
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