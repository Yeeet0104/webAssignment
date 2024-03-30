using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Customer
{
    public partial class customerManagement : System.Web.UI.Page, IFilterable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Retrieve the updated customer data from the Query String
            DataRow updatedRow = null;
            if (!IsPostBack)

            if (Request.QueryString["updatedData"] != null)
            {
                string encodedUpdatedData = Request.QueryString["updatedData"];
                updatedRow = GetDataRowFromEncodedString(encodedUpdatedData);
            }

            // Update the customerListView with the updated data or bind with initial data
            if (updatedRow != null)
            {
                UpdateCustomerListView(updatedRow);
            }
            else
            {
                customerListView.DataSource = GetDummyData();
                customerListView.DataBind();
            }
        }

        private DataRow GetDataRowFromEncodedString(string encodedString)
        {
            string decodedString = HttpUtility.UrlDecode(encodedString);
            DataTable dt = new DataTable();
            dt.Columns.Add("CustomerName", typeof(string));
            dt.Columns.Add("CustomerEmail", typeof(string));
            dt.Columns.Add("PhoneNo", typeof(string));
            dt.Columns.Add("CustomerImageUrl", typeof(string));

            DataRow updatedRow = dt.NewRow();
            string[] values = decodedString.Split(',');
            // Check if the values array has the expected number of elements
            if (values.Length == 4)
            {
                updatedRow["CustomerName"] = values[0];
                updatedRow["CustomerEmail"] = values[1];
                updatedRow["PhoneNo"] = values[2];
                updatedRow["CustomerImageUrl"] = values[3];
            }

            return updatedRow;
        }

        private DataTable GetDummyData()
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields

            dummyData.Columns.Add("CustomerImageUrl", typeof(string));
            dummyData.Columns.Add("CustomerName", typeof(string));
            dummyData.Columns.Add("CustomerEmail", typeof(string));
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

        private void UpdateCustomerListView(DataRow updatedRow)
        {
            // Retrieve the data source for the customerListView
            DataTable customerData = GetDummyData();

            // Find the row to update based on the customer name or any unique identifier
            DataRow rowToUpdate = customerData.Rows.Cast<DataRow>()
                                               .FirstOrDefault(row => row["CustomerName"].ToString() == updatedRow["CustomerName"].ToString());

            // Update the row with the new data
            if (rowToUpdate != null)
            {
                rowToUpdate["CustomerName"] = updatedRow["CustomerName"];
                rowToUpdate["CustomerEmail"] = updatedRow["CustomerEmail"];
                rowToUpdate["PhoneNo"] = updatedRow["PhoneNo"];
                rowToUpdate["CustomerImageUrl"] = updatedRow["CustomerImageUrl"];
            }

            // Rebind the customerListView with the updated data source
            customerListView.DataSource = customerData;
            customerListView.DataBind();
        }

        protected void customerListView_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Add your event handling logic here
        }

        public void FilterListView(string searchTerm)
        {
            DataTable dummyData = GetDummyData();
            DataTable filteredData = FilterDataTable(dummyData, searchTerm);

            customerListView.DataSource = filteredData;
            customerListView.DataBind();
        }

        private DataTable FilterDataTable(DataTable dataTable, string searchTerm)
        {
            // Escape single quotes in the search term which can break the filter expression.
            string safeSearchTerm = searchTerm.Replace("'", "''");

            // Build a filter expression that checks if any of the columns contain the search term.
            string expression = string.Format(
                "CustomerName LIKE '%{0}%' OR " +
                "CustomerEmail LIKE '%{0}%' OR " +
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
        protected void showPopUp_Click(object sender, EventArgs e)
        {
            popUpDelete.Style.Add("display", "flex");
        }

        protected void closePopUp_Click(object sender, EventArgs e)
        {
            popUpDelete.Style.Add("display", "none");
        }
        protected void btnCancelDelete_Click(object sender, EventArgs e)
        {
            popUpDelete.Style.Add("display", "none");
        }
    }
}