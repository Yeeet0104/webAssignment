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
                productListView.DataSource = GetDummyData();

                productListView.DataBind();
            }
        }
        private DataTable GetDummyData( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("CustomerID", typeof(int));
            dummyData.Columns.Add("UserName", typeof(string));
            dummyData.Columns.Add("Description", typeof(string));
            dummyData.Columns.Add("Amount", typeof(decimal));
            dummyData.Columns.Add("paymentMethod", typeof(string));
            dummyData.Columns.Add("Date", typeof(DateTime));

            // Add rows with dummy data
            dummyData.Rows.Add(2001,"Dexter", "Product Purchase-Iphone 25 Pro Max,Iphone 25 Pro Max,Iphone 25 Pro Max", 25000.99,"credit",DateTime.Now);
            dummyData.Rows.Add(2001,"Dexter","Product Purchase-Iphone 25 Pro Max",25000.99,"credit",DateTime.Now);
            dummyData.Rows.Add(2001,"Dexter","Product Purchase-Iphone 25 Pro Max",25000.99,"credit",DateTime.Now);
            // Add more rows as needed for testing

            return dummyData;
        }

        protected void productListView_SelectedIndexChanged( object sender, EventArgs e )
        {

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
    }
}