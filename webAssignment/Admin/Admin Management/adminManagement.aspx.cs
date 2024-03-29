using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Admin_Management
{
    public partial class adminManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                adminListView.DataSource = GetDummyData();

                adminListView.DataBind();
            }

        }

        private DataTable GetDummyData()
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields

            dummyData.Columns.Add("AdminImageUrl", typeof(string));
            dummyData.Columns.Add("AdminName", typeof(string));
            dummyData.Columns.Add("AdminEmail", typeof(string));
            dummyData.Columns.Add("PhoneNo", typeof(string));
            dummyData.Columns.Add("DOB", typeof(DateTime));
            dummyData.Columns.Add("Status", typeof(string));
            dummyData.Columns.Add("Added", typeof(DateTime));


            // Add rows with dummy data
            dummyData.Rows.Add("~/Admin/Layout/image/DexProfilePic.jpeg", "John Doe", "johndoe@gmail.com", "0123136742", DateTime.Now, "Active", DateTime.Now);
            dummyData.Rows.Add("~/Admin/Layout/image/DexProfilePic.jpeg", "Jane Smith", "janesmith@gmail.com", "0123136742", DateTime.Now, "Active", DateTime.Now);
            dummyData.Rows.Add("~/Admin/Layout/image/DexProfilePic.jpeg", "Alice Johnson", "alicejohnson@gmail.com", "01233136742", DateTime.Now, "Active", DateTime.Now);
            dummyData.Rows.Add("~/Admin/Layout/image/DexProfilePic.jpeg", "Bob Williams", "bobwilliams@gmail.com", "0123136742", DateTime.Now, "Active", DateTime.Now);
            dummyData.Rows.Add("~/Admin/Layout/image/DexProfilePic.jpeg", "Emma Davis", "emmadavis@gmail.com", "01231363742", DateTime.Now, "Active", DateTime.Now);
            dummyData.Rows.Add("~/Admin/Layout/image/DexProfilePic.jpeg", "Michael Brown", "michaelbrown@gmail.com", "0123136742", DateTime.Now, "Active", DateTime.Now);
            dummyData.Rows.Add("~/Admin/Dashboard/Images/iphone11.jpg", "Olivia Jones", "oliviajones@gmail.com", "0123136742", DateTime.Now, "Active", DateTime.Now);
            dummyData.Rows.Add("~/Admin/Layout/image/DexProfilePic.jpeg", "Sophia Taylor", "sophiataylor@gmail.com", "0123136742", DateTime.Now, "Active", DateTime.Now);
            dummyData.Rows.Add("~/Admin/Layout/image/DexProfilePic.jpeg", "James Miller", "jamesmiller@gmail.com", "0123136742", DateTime.Now, "Active", DateTime.Now);
            dummyData.Rows.Add("~/Admin/Layout/image/DexProfilePic.jpeg", "Ava Moore", "avamoore@gmail.com", "01231367423", DateTime.Now, "Active", DateTime.Now);


            return dummyData;
        }

        protected void adminListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Add your event handling logic here
        }

        public void FilterListView(string searchTerm)
        {
            DataTable dummyData = GetDummyData();
            DataTable filteredData = FilterDataTable(dummyData, searchTerm);

            adminListView.DataSource = filteredData;
            adminListView.DataBind();
        }

        private DataTable FilterDataTable(DataTable dataTable, string searchTerm)
        {
            // Escape single quotes in the search term which can break the filter expression.
            string safeSearchTerm = searchTerm.Replace("'", "''");

            // Build a filter expression that checks if any of the columns contain the search term.
            string expression = string.Format(
                "AdminName LIKE '%{0}%' OR " +
                "AdminEmail LIKE '%{0}%' OR " +
                "PhoneNo LIKE '%{0}%' OR " +
                "Convert(DOB, 'System.String') LIKE '%{0}%' OR " +
                "Status LIKE '%{0}%' OR " +
                "Convert(Added, 'System.String') LIKE '%{0}%'",
                safeSearchTerm);

            // Use the Select method to find all rows matching the filter expression.
            DataRow[] filteredRows = dataTable.Select(expression);

            // Create a new DataTable to hold the filtered rows.
            DataTable filteredDataTable = dataTable.Clone(); // Clone the structure of the table.

            // Import the filtered rows into the new DataTable.
            foreach (DataRow row in filteredRows)
            {
                filteredDataTable.ImportRow(row);
            }

            return filteredDataTable;
        }
    }
}