using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Admin_Management
{
    public partial class adminManagement : System.Web.UI.Page//, IFilterable
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        
        protected void adminEditBtn_Click(object sender, EventArgs e)
        {

        }
        //{
        //   public void FilterListView(string searchTerm)
        //   {
        //       DataTable dummyData = GetDummyData();
        //       DataTable filteredData = FilterDataTable(dummyData, searchTerm);

        //       adminListView.DataSource = filteredData;
        //       adminListView.DataBind();
        //    }

        //   private DataTable FilterDataTable(DataTable dataTable, string searchTerm)
        //    {
        // Escape single quotes in the search term which can break the filter expression.
        //      string safeSearchTerm = searchTerm.Replace("'", "''");

        // Build a filter expression that checks if any of the columns contain the search term.
        //      string expression = string.Format(
        //          "AdminName LIKE '%{0}%' OR " +
        //         "AdminEmail LIKE '%{0}%' OR " +
        //         "PhoneNo LIKE '%{0}%' OR " +
        //          "Convert(DOB, 'System.String') LIKE '%{0}%' OR " +
        //          "Status LIKE '%{0}%' OR " +
        //          "Convert(Added, 'System.String') LIKE '%{0}%'",
        //       safeSearchTerm);

        // Use the Select method to find all rows matching the filter expression.
        //       DataRow[] filteredRows = dataTable.Select(expression);

        // Create a new DataTable to hold the filtered rows.
        //          DataTable filteredDataTable = dataTable.Clone(); // Clone the structure of the table.

        // Import the filtered rows into the new DataTable.
        //       foreach (DataRow row in filteredRows)
        //     {
        //          filteredDataTable.ImportRow(row);
        //      }

        //     return filteredDataTable;
        //  }
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