using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webAssignment.Admin.Layout;

namespace webAssignment
{
    public partial class adminProducts : System.Web.UI.Page, IFilterable
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
            dummyData.Columns.Add("OrderId", typeof(int));
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("ProductName", typeof(string));
            dummyData.Columns.Add("AdditionalProductsCount", typeof(int));
            dummyData.Columns.Add("Date", typeof(DateTime));
            dummyData.Columns.Add("CustomerName", typeof(string));
            dummyData.Columns.Add("Total", typeof(decimal));
            dummyData.Columns.Add("PaymentDate", typeof(DateTime));
            dummyData.Columns.Add("Status", typeof(string));

            // Add rows with dummy data
            dummyData.Rows.Add(10234, "~/Admin/Layout/image/DexProfilePic.jpeg", "Dexter", 2, DateTime.Now, "John Doe", 150.00m, DateTime.Now, "Processing");
            dummyData.Rows.Add(10235, "~/Admin/Dashboard/Images/iphone11.jpg", "Dex 2", 3, DateTime.Now, "Jane Smith", 200.00m, DateTime.Now, "Shipped");
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

    }
}