using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;
using webAssignment.Client.Profile;

namespace webAssignment.Admin.Customer
{
    public partial class customerManagement : System.Web.UI.Page//, IFilterable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void customerEditBtn_Click(object sender, EventArgs e)
        {
            // Get the user ID of the selected user to edit
            LinkButton editButton = (LinkButton)sender;
            string userId = editButton.CommandArgument;

            string query = $"?userId={userId}";

            Response.Redirect("~/Admin/Customer Management/editCustomer.aspx" + query);
        }


        //public void FilterListView(string searchTerm)
        //{
        //    DataTable dummyData = GetDummyData();
        //    DataTable filteredData = FilterDataTable(dummyData, searchTerm);

        //    customerListView.DataSource = filteredData;
        //    customerListView.DataBind();
        //}

        //private DataTable FilterDataTable(DataTable dataTable, string searchTerm)
        //{
        //    // Escape single quotes in the search term which can break the filter expression.
        //    string safeSearchTerm = searchTerm.Replace("'", "''");

        //    // Build a filter expression that checks if any of the columns contain the search term.
        //    string expression = string.Format(
        //        "CustomerName LIKE '%{0}%' OR " +
        //        "CustomerEmail LIKE '%{0}%' OR " +
        //        "PhoneNo LIKE '%{0}%' OR " +
        //        "Convert(DOB, 'System.String') LIKE '%{0}%' OR " +
        //        "Status LIKE '%{0}%' OR " +
        //        "Convert(Added, 'System.String') LIKE '%{0}%'",
        //        safeSearchTerm);

        //    // Use the Select method to find all rows matching the filter expression.
        //    DataRow[] filteredRows = dataTable.Select(expression);

        //    // Create a new DataTable to hold the filtered rows.
        //    DataTable filteredDataTable = dataTable.Clone(); // Clone the structure of the table.

        //    // Import the filtered rows into the new DataTable.
        //    foreach (DataRow row in filteredRows)
        //    {
        //        filteredDataTable.ImportRow(row);
        //    }

        //    return filteredDataTable;
        //}


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