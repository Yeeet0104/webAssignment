using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Collections.Specialized.BitVector32;
using webAssignment.Client.Profile;
using ClosedXML.Excel;
using System.IO;

namespace webAssignment.Admin.Customer
{
    public partial class customerManagement : System.Web.UI.Page//, IFilterable
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if the user is logged in
                if (Session["userId"] == null)
                {
                    // Handle the case where the user is not logged in
                    Response.Redirect("~/Client/LoginSignUp/AdminLogin.aspx");
                }
                else
                {
                    // By default, select the "All" button
                    btnAllCustomers.CssClass = "col-span-1 px-3 py-1 text-blue-600 bg-gray-100 rounded-lg";
                    btnActive.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
                    btnBlocked.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";

                }
            }
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

        protected void allCustomers_Click(object sender, EventArgs e)
        {
            // Construct the SELECT query for SqlDataSource to show all customers
            string selectQuery = "SELECT * FROM [User] WHERE user_id LIKE 'CS%'";

            // Set the SelectCommand of SqlDataSource to the updated query
            SqlDataSource1.SelectCommand = selectQuery;

            // Rebind the ListView to display all customers
            customerListView.DataBind();

            btnAllCustomers.CssClass = "col-span-1 px-3 py-1 text-blue-600 bg-gray-100 rounded-lg";
            btnActive.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
            btnBlocked.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";

        }

        protected void active_Click(object sender, EventArgs e)
        {
            // Construct the SELECT query for SqlDataSource to filter active customers
            string selectQuery = "SELECT * FROM [User] WHERE user_id LIKE 'CS%' AND status = 'Active'";

            // Set the SelectCommand of SqlDataSource to the updated query
            SqlDataSource1.SelectCommand = selectQuery;

            // Rebind the ListView to reflect the filtered data
            customerListView.DataBind();

            btnAllCustomers.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
            btnActive.CssClass = "col-span-1 px-3 py-1 text-blue-600 bg-gray-100 rounded-lg";
            btnBlocked.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";

        }

        protected void blocked_Click(object sender, EventArgs e)
        {
            // Construct the SELECT query for SqlDataSource to filter blocked customers
            string selectQuery = "SELECT * FROM [User] WHERE user_id LIKE 'CS%' AND status = 'Blocked'";

            // Set the SelectCommand of SqlDataSource to the updated query
            SqlDataSource1.SelectCommand = selectQuery;

            // Rebind the ListView to reflect the filtered data
            customerListView.DataBind();

            btnAllCustomers.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
            btnActive.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
            btnBlocked.CssClass = "col-span-1 px-3 py-1 text-blue-600 bg-gray-100 rounded-lg";

        }

        //exporting to excel file
        protected void btnExport_Click(object sender, EventArgs e)
        {
            // Retrieve the data from the SqlDataSource
            DataView dataView = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            DataTable dataTable = dataView.ToTable();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Customers");
                var currentRow = 1;

                // Add column headers
                worksheet.Cell(currentRow, 1).Value = "Customer Name";
                worksheet.Cell(currentRow, 2).Value = "Username";
                worksheet.Cell(currentRow, 3).Value = "Email";
                worksheet.Cell(currentRow, 4).Value = "Phone No";
                worksheet.Cell(currentRow, 5).Value = "DOB";
                worksheet.Cell(currentRow, 6).Value = "Status";
                worksheet.Cell(currentRow, 7).Value = "Date Added";
                worksheet.Cell(currentRow, 8).Value = "Last Login";

                currentRow++;

                // Iterate over the rows in the DataTable
                foreach (DataRow row in dataTable.Rows)
                {
                    // Add data to Excel sheet for the specified fields
                    worksheet.Cell(currentRow, 1).Value = $"{row["first_name"]} {row["last_name"]}";
                    worksheet.Cell(currentRow, 2).Value = row["username"].ToString();
                    worksheet.Cell(currentRow, 3).Value = row["email"].ToString();
                    worksheet.Cell(currentRow, 4).Value = row["phone_number"].ToString();

                    // Handle birth_date column
                    DateTime birthDate;
                    if (row["birth_date"] != DBNull.Value && DateTime.TryParse(row["birth_date"].ToString(), out birthDate))
                    {
                        worksheet.Cell(currentRow, 5).Value = birthDate.ToString("dd MMM yyyy");
                    }
                    else
                    {
                        worksheet.Cell(currentRow, 5).Value = string.Empty; // Or any default value you prefer
                    }
                    worksheet.Cell(currentRow, 6).Value = row["status"].ToString();
                    worksheet.Cell(currentRow, 7).Value = ((DateTime)row["date_created"]).ToString("dd MMM yyyy");
                    worksheet.Cell(currentRow, 8).Value = row["last_login"].ToString();

                    currentRow++;
                }

                // Prepare the Excel file for download
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Customers.xlsx");
                    stream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }
        //sort dob and date joined ascending and descending   
        protected void SortByDOBDateJoined_Click(object sender, EventArgs e)
        {
            // Determine which column triggered the event
            string column = ((LinkButton)sender).ID == "filterDOB" ? "DOB" : "Joined";

            // Retrieve the current sort expression and sort direction from ViewState
            string sortExpression = ViewState["SortExpression"] as string ?? "";
            string sortDirection = ViewState["SortDirection"] as string ?? "";

            // Toggle the sort direction if the user clicks on the button repeatedly
            if (sortExpression.Equals(column, StringComparison.OrdinalIgnoreCase))
            {
                sortDirection = sortDirection.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC";
            }
            else
            {
                // Default to ascending order if sorting by a new column
                sortExpression = column;
                sortDirection = "ASC";
            }

            // Update the sort expression and sort direction in ViewState
            ViewState["SortExpression"] = sortExpression;
            ViewState["SortDirection"] = sortDirection;

            // Reconstruct the SELECT query for SqlDataSource with the appropriate ORDER BY clause
            string orderByClause = column == "DOB" ? "birth_date" : "date_created";
            string selectQuery = $"SELECT * FROM [User] WHERE user_id LIKE 'CS%' ORDER BY {orderByClause} {sortDirection}";

            // Set the SelectCommand of SqlDataSource to the updated query
            SqlDataSource1.SelectCommand = selectQuery;

            // Rebind the ListView to reflect the sorted data
            customerListView.DataBind();
        }
        //sort last login time asc desc 
        protected void filterLastLogin_Click(object sender, EventArgs e)
        {
            // Retrieve the current sort expression and sort direction from ViewState
            string sortExpression = ViewState["SortExpression"] as string ?? "";
            string sortDirection = ViewState["SortDirection"] as string ?? "";

            // Toggle the sort direction if the user clicks on the button repeatedly
            if (sortExpression.Equals("LastLogin", StringComparison.OrdinalIgnoreCase))
            {
                sortDirection = sortDirection.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC";
            }
            else
            {
                // Default to ascending order if sorting by a new column
                sortExpression = "LastLogin";
                sortDirection = "ASC";
            }

            // Update the sort expression and sort direction in ViewState
            ViewState["SortExpression"] = sortExpression;
            ViewState["SortDirection"] = sortDirection;

            // Reconstruct the SELECT query for SqlDataSource with the appropriate ORDER BY clause
            string selectQuery = "SELECT * FROM [User] WHERE user_id LIKE 'CS%' ORDER BY last_login " + sortDirection;

            // Set the SelectCommand of SqlDataSource to the updated query
            SqlDataSource1.SelectCommand = selectQuery;

            // Rebind the ListView to reflect the sorted data
            customerListView.DataBind();
        }
        //sort name asc desc
        protected void filterName_Click(object sender, EventArgs e)
        {
            // Retrieve the current sort expression and sort direction from ViewState
            string sortExpression = ViewState["SortExpression"] as string ?? "";
            string sortDirection = ViewState["SortDirection"] as string ?? "";

            // Toggle the sort direction if the user clicks on the button repeatedly
            if (sortExpression.Equals("Name", StringComparison.OrdinalIgnoreCase))
            {
                sortDirection = sortDirection.Equals("ASC", StringComparison.OrdinalIgnoreCase) ? "DESC" : "ASC";
            }
            else
            {
                // Default to ascending order if sorting by a new column
                sortExpression = "Name";
                sortDirection = "ASC";
            }

            // Update the sort expression and sort direction in ViewState
            ViewState["SortExpression"] = sortExpression;
            ViewState["SortDirection"] = sortDirection;

            // Reconstruct the SELECT query for SqlDataSource with the appropriate ORDER BY clause
            string selectQuery = "SELECT * FROM [User] WHERE user_id LIKE 'CS%' ORDER BY first_name " + sortDirection + ", last_name " + sortDirection;

            // Set the SelectCommand of SqlDataSource to the updated query
            SqlDataSource1.SelectCommand = selectQuery;

            // Rebind the ListView to reflect the sorted data
            customerListView.DataBind();
        }

    }
}