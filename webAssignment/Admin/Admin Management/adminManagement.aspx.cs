using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Admin_Management
{
    public partial class adminManagement : System.Web.UI.Page//, IFilterable
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] != null)
                {
                    // By default, select the "All" button
                    allAdmins.CssClass = "col-span-1 px-3 py-1 text-blue-600 bg-gray-100 rounded-lg";
                    active.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
                   blocked.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";

                    string adminId = Session["userId"].ToString();
                    if (CheckIsAdminManager(adminId))
                    {
                        divAddNewAdmin.Style["display"] = "block";
                    }
                    else
                    {
                        divAddNewAdmin.Style["display"] = "none";
                    }
                }
                else
                {
                    Response.Redirect("~/Client/LoginSignUp/AdminLogin.aspx");
                }
            }
            
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

        protected void btnAddNewAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Admin Management/addNewAdmin.aspx");
        }

        private bool CheckIsAdminManager(string adminId)
        {
            bool isAdminManager = false;

            string query = "SELECT COUNT(*) FROM [User] WHERE user_id = @adminId AND role = 'Admin Manager'";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@adminId", adminId);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();

                    isAdminManager = count > 0;
                }
            }
            return isAdminManager;
        }

        //exporting to excel file
        protected void btnExport_Click(object sender, EventArgs e)
        {
            // Retrieve the data from the SqlDataSource
            DataView dataView = (DataView)SqlDataSource1.Select(DataSourceSelectArguments.Empty);
            DataTable dataTable = dataView.ToTable();

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Admins");
                var currentRow = 1;

                // Add column headers
                worksheet.Cell(currentRow, 1).Value = "Admin Name";
                worksheet.Cell(currentRow, 2).Value = "Role";
                worksheet.Cell(currentRow, 3).Value = "Email";
                worksheet.Cell(currentRow, 4).Value = "Phone No";
                worksheet.Cell(currentRow, 5).Value = "DOB";
                worksheet.Cell(currentRow, 6).Value = "Status";
                worksheet.Cell(currentRow, 7).Value = "Date Added";

                currentRow++;

                // Iterate over the rows in the DataTable
                foreach (DataRow row in dataTable.Rows)
                {
                    // Add data to Excel sheet for the specified fields
                    worksheet.Cell(currentRow, 1).Value = $"{row["first_name"]} {row["last_name"]}";
                    worksheet.Cell(currentRow, 2).Value = row["role"].ToString();
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

        protected void active_Click(object sender, EventArgs e)
        {
            // Construct the SELECT query for SqlDataSource to filter active customers
            string selectQuery = "SELECT * FROM [User] WHERE user_id LIKE 'AD%' AND status = 'Active'";

            // Set the SelectCommand of SqlDataSource to the updated query
            SqlDataSource1.SelectCommand = selectQuery;

            // Rebind the ListView to reflect the filtered data
            adminListView.DataBind();

            allAdmins.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
            active.CssClass = "col-span-1 px-3 py-1 text-blue-600 bg-gray-100 rounded-lg";
            blocked.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
        }

        protected void blocked_Click(object sender, EventArgs e)
        {
            // Construct the SELECT query for SqlDataSource to filter blocked customers
            string selectQuery = "SELECT * FROM [User] WHERE user_id LIKE 'AD%' AND status = 'Blocked'";

            // Set the SelectCommand of SqlDataSource to the updated query
            SqlDataSource1.SelectCommand = selectQuery;

            // Rebind the ListView to reflect the filtered data
            adminListView.DataBind();

            allAdmins.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
            active.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
            blocked.CssClass = "col-span-1 px-3 py-1 text-blue-600 bg-gray-100 rounded-lg";
        }

        protected void allAdmins_Click(object sender, EventArgs e)
        {
            // Construct the SELECT query for SqlDataSource to show all customers
            string selectQuery = "SELECT * FROM [User] WHERE user_id LIKE 'AD%'";

            // Set the SelectCommand of SqlDataSource to the updated query
            SqlDataSource1.SelectCommand = selectQuery;

            // Rebind the ListView to display all customers
            adminListView.DataBind();

            allAdmins.CssClass = "col-span-1 px-3 py-1 text-blue-600 bg-gray-100 rounded-lg";
            active.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
            blocked.CssClass = "col-span-1 px-3 py-1 hover:text-blue-600 hover:bg-gray-100 rounded-lg";
        }

        //sort dob and date joined ascending and descending   
        protected void SortByDOBDateJoined_Click(object sender, EventArgs e)
        {
            // Determine which column triggered the event
            string column = ((LinkButton)sender).ID == "filterDOB" ? "DOB" : "Added";

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
            string selectQuery = $"SELECT * FROM [User] WHERE user_id LIKE 'AD%' ORDER BY {orderByClause} {sortDirection}";

            // Set the SelectCommand of SqlDataSource to the updated query
            SqlDataSource1.SelectCommand = selectQuery;

            // Rebind the ListView to reflect the sorted data
            adminListView.DataBind();
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
            string selectQuery = "SELECT * FROM [User] WHERE user_id LIKE 'AD%' ORDER BY first_name " + sortDirection + ", last_name " + sortDirection;

            // Set the SelectCommand of SqlDataSource to the updated query
            SqlDataSource1.SelectCommand = selectQuery;

            // Rebind the ListView to reflect the sorted data
            adminListView.DataBind();
        }

    }
}