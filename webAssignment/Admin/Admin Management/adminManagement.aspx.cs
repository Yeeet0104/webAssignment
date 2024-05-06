using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Diagrams;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using webAssignment.Admin.Product_Management;

namespace webAssignment.Admin.Admin_Management
{
    public partial class adminManagement : System.Web.UI.Page, IFilterable
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private int pageSize = 5;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] != null)
                {
                    adminListView.ItemDataBound += adminListView_ItemDataBound;
                    ViewState["FilterStatus"] = "";
                    ViewState["PageIndex"] = 0;
                    ViewState["onePageStartDate"] = "";
                    ViewState["onePageEndDate"] = "";
                    BindListView(0, pageSize, "");

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
        protected void adminListView_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                HtmlGenericControl divAction = (HtmlGenericControl)e.Item.FindControl("divAction");
                if (divAction != null)
                {
                    // Get the Admin object associated with the ListViewItem
                    Admin admin = (Admin)e.Item.DataItem;
                    string userId = admin.user_id; 

                    // Get the user ID of the currently logged-in user
                    string currentUserId = Session["userId"].ToString();

                    if (userId == currentUserId)
                    {
                        divAction.Style["display"] = "none"; // Hide the divAction for the current user
                    }
                    else
                    {
                        divAction.Style["display"] = "flex"; // Show the divAction for other users
                    }
                }
            }
        }

        protected void adminEditBtn_Click(object sender, EventArgs e){
            // Get the user ID of the selected user to edit
            LinkButton editButton = (LinkButton)sender;
            string userId = editButton.CommandArgument;

            string query = $"?userId={userId}";

            Response.Redirect("~/Admin/Admin Management/EditAdmin.aspx" + query);
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

        protected void btnAddNewAdmin_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/Admin Management/addNewAdmin.aspx");
        }

        private bool Delete(string adminId)
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

        private DataTable GetAdminData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM [User] WHERE user_id LIKE 'AD%'";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }

        //exporting to excel file
        protected void btnExport_Click(object sender, EventArgs e)
        {
            DataTable dataTable = GetAdminData();

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


        // binding into listview
        private void BindListView(int pageIndex, int pageSize, string status)
        {
            adminListView.DataSource = getAdminData(pageIndex, pageSize, status ?? ViewState["FilterStatus"] as string);
            adminListView.DataBind();
        }

        protected void adminListView_DataBound(object sender, EventArgs e)
        {
            Label pageNumFoot = adminListView.FindControl("pageNumFoot") as Label;
            Label lblCurrPagination = adminListView.FindControl("lblCurrPagination") as Label;

            if (pageNumFoot != null)
            {
                int totalItems = GetTotalAdminCount(ViewState["FilterStatus"].ToString());
                int currentPageIndex = ((int)ViewState["PageIndex"]);
                int startRecord = (currentPageIndex * pageSize) + 1;
                int endRecord = (currentPageIndex + 1) * pageSize;
                endRecord = (endRecord > totalItems) ? totalItems : endRecord;

                lblCurrPagination.Text = (currentPageIndex + 1).ToString();
                pageNumFoot.Text = $"Showing {startRecord}-{endRecord} of {totalItems}";
            }
        }

        private List<Admin> getAdminData(int pageIndex, int pageSize, string status)
        {
            List<Admin> users = new List<Admin>();
            string filter = ViewState["Filter"] as string;
            string sortExpression = ViewState["SortExpression"] as string ?? "user_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";

            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;

            if (ViewState["onePageStartDate"].ToString() != "" && ViewState["onePageEndDate"].ToString() != "")
            {
                sortStartDate = (DateTime)ViewState["onePageStartDate"];
                sortEndDate = (DateTime)ViewState["onePageEndDate"];
            }

            List<string> conditions = new List<string>();
            if (!string.IsNullOrEmpty(status))
            {
                conditions.Add("status = @status");
            }
            if (sortStartDate.HasValue && sortEndDate.HasValue)
            {
                conditions.Add("date_created >= @startDate AND date_created <= @endDate");
            }
            conditions.Add("user_id LIKE 'AD%'");
            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";

            string sql = $@"SELECT 
            user_id,
            first_name,
            last_name,
            phone_number,
            email,
            birth_date,
            profile_pic_path,
            date_created,
            status,
            role,
            ROW_NUMBER() OVER (ORDER BY {sortExpression} {sortDirection}) AS RowNum
        FROM 
            [User]
        {whereClause}
        ORDER BY {sortExpression} {sortDirection}
        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Offset", pageIndex * pageSize);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    if (sortStartDate.HasValue && sortEndDate.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@startDate", sortStartDate.Value);
                        cmd.Parameters.AddWithValue("@endDate", sortEndDate.Value);
                    }
                    if (!string.IsNullOrEmpty(status))
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                    }
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new Admin
                            {
                                user_id = reader.GetString(0),
                                first_name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                last_name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                phone_number = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                email = reader.GetString(4),
                                birth_date = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                                profile_pic_path = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                                date_created = reader.IsDBNull(7) ? DateTime.MinValue : reader.GetDateTime(7),
                                status = reader.GetString(8),
                                role = reader.GetString(9)
                            });
                        }
                    }
                }
            }
            return users;
        }
   

        private List<Admin> getAllAdmin()
        {
            List<Admin> users = new List<Admin>();
            string filter = ViewState["Filter"] as string;
            string sortExpression = ViewState["SortExpression"] as string ?? "user_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";
            string sql = @"SELECT 
            user_id,
            first_name,
            last_name,
            phone_number,
            email,
            birth_date,
            profile_pic_path,
            date_created,
            status,
            role FROM [User]";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new Admin
                            {
                                user_id = reader.GetString(0),
                                first_name = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                                last_name = reader.IsDBNull(2) ? string.Empty : reader.GetString(2),
                                phone_number = reader.IsDBNull(3) ? string.Empty : reader.GetString(3),
                                email = reader.GetString(4),
                                birth_date = reader.IsDBNull(5) ? DateTime.MinValue : reader.GetDateTime(5),
                                profile_pic_path = reader.IsDBNull(6) ? string.Empty : reader.GetString(6),
                                date_created = reader.IsDBNull(7) ? DateTime.MinValue : reader.GetDateTime(7),
                                status = reader.GetString(8),
                                role = reader.GetString(9)
                            });
                        }
                    }
                }
            }
            return users;
        }
        private int GetTotalAdminCount(string status="")
        {
            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;

            if (ViewState["onePageStartDate"].ToString() != "" && ViewState["onePageEndDate"].ToString() != "")
            {
                sortStartDate = (DateTime)ViewState["onePageStartDate"];
                sortEndDate = (DateTime)ViewState["onePageEndDate"];
            }
            var adminList = new List<Admin>();
            string sortExpression = ViewState["SortExpression"] as string ?? "user_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";

            List<string> conditions = new List<string>();
            if (!string.IsNullOrEmpty(status))
            {
                conditions.Add("status = @status");
            }
            if (sortStartDate.HasValue && sortEndDate.HasValue)
            {
                conditions.Add("date_created >= @startDate AND date_created <= @endDate");
            }
            conditions.Add("user_id LIKE 'AD%'");
            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";
            string sql = $@"SELECT COUNT(*) FROM [User] {whereClause}";
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (sortStartDate.HasValue && sortEndDate.HasValue)
                    {
                        cmd.Parameters.AddWithValue("@startDate", sortStartDate.Value);
                        cmd.Parameters.AddWithValue("@endDate", sortEndDate.Value);
                    }
                    if (!string.IsNullOrEmpty(status))
                    {
                        cmd.Parameters.AddWithValue("status", status);
                    }
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        protected void prevPage_Click(object sender, EventArgs e)
        {
            int pageIndex = (int)ViewState["PageIndex"];

            if (pageIndex > 0)
            {
                ViewState["PageIndex"] = pageIndex - 1;
                BindListView((int)ViewState["PageIndex"], pageSize, ViewState["FilterStatus"].ToString());
            }
        }
        protected void nextPage_Click(object sender, EventArgs e)
        {
            int pageIndex = (int)ViewState["PageIndex"];
            int totalAdmin = GetTotalAdminCount(ViewState["FilterStatus"].ToString());

            if ((pageIndex + 1) * pageSize < totalAdmin)
            {
                ViewState["PageIndex"] = pageIndex + 1;
                BindListView((int)ViewState["PageIndex"], pageSize, ViewState["FilterStatus"].ToString());
            }
        }

        //sorting by clicking the table label functions
        protected void adminListView_Sorting(object sender, ListViewSortEventArgs e)
        {
            List<Admin> admin = getAllAdmin();
            string sortDirection = GetSortDirection(e.SortExpression);
            IEnumerable<Admin> sortedAdmins;

            // Sorting the list using LINQ dynamically based on SortExpression and SortDirection
            if (sortDirection == "ASC")
            {
                sortedAdmins = admin.OrderBy(x => GetPropertyValue(x, e.SortExpression));
            }
            else
            {
                sortedAdmins = admin.OrderByDescending(x => GetPropertyValue(x, e.SortExpression));
            }


            adminListView.DataSource = getAdminData((int)ViewState["PageIndex"], pageSize, ViewState["FilterStatus"].ToString());
            adminListView.DataBind();
        }

        private object GetPropertyValue(object obj, string propName)
        {
            return obj.GetType().GetProperty(propName).GetValue(obj, null);
        }

        private string GetSortDirection(string column)
        {
            // By default, set the sort direction to ascending
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if (sortExpression != null && sortExpression == column)
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value is returned.
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }

            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }


        protected void allFilter_click(object sender, EventArgs e)
        {
            ViewState["FilterStatus"] = "";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "");
            changeSelectedtabCss("");
        }

        protected void activeFilter_click(object sender, EventArgs e)
        {
            ViewState["FilterStatus"] = "Active";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Active");
            changeSelectedtabCss("Active");
        }

        protected void blockedFilter_click(object sender, EventArgs e)
        {
            ViewState["FilterStatus"] = "Blocked";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Blocked");
            changeSelectedtabCss("Blocked");
        }

        private void resetfilterTabSttyle()
        {
            RemoveCssClass(activeFilter, "text-blue-600");
            RemoveCssClass(activeFilter, "bg-gray-100");
            RemoveCssClass(blockedFilter, "text-blue-600");
            RemoveCssClass(blockedFilter, "bg-gray-100");
            RemoveCssClass(allFilter, "text-blue-600");
            RemoveCssClass(allFilter, "bg-gray-100");
        }

        private void changeSelectedtabCss(string tabName)
        {
            resetfilterTabSttyle();
            switch (tabName)
            {
                case "Active":
                    activeFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                case "Blocked":
                    blockedFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                default:
                    allFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
            }
        }

        private void RemoveCssClass(WebControl control, string classToRemove)
        {
            List<string> classes = control.CssClass.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            // Remove the specific class
            classes.Remove(classToRemove);

            control.CssClass = String.Join(" ", classes);
        }


        //Search bar filter functions
        public void FilterListView(string searchTerm)
        {
            List<Admin> customerData = getAdminData(0, pageSize, ViewState["FilterStatus"].ToString());
            List<Admin> filteredData = FilterCustomerList(customerData, searchTerm);

            adminListView.DataSource = filteredData;
            adminListView.DataBind();
        }

        // Lists and LINQ
        private List<Admin> FilterCustomerList(List<Admin> customers, string searchTerm)
        {
            searchTerm = searchTerm?.ToLower() ?? string.Empty; // Handle null search term and convert to lowercase

            return customers.Where(c =>
                c.user_id.ToLower().Contains(searchTerm) ||
                c.first_name.ToLower().Contains(searchTerm) ||
                c.last_name.ToLower().Contains(searchTerm) ||
                c.phone_number.ToLower().Contains(searchTerm) ||
                c.email.ToLower().Contains(searchTerm) ||
                c.birth_date.ToString("dd/MM/yyyy").Contains(searchTerm) ||
                c.profile_pic_path.ToLower().Contains(searchTerm) ||
                c.date_created.ToString("dd/MM/yyyy").Contains(searchTerm) ||
                c.status.ToLower().Contains(searchTerm) ||
                c.role.ToLower().Contains(searchTerm)
            ).ToList();
        }

        //Delete Users
        protected void deleteCustomerLink_Click(object sender, EventArgs e)
        {
            // Get the userId of the customer to delete from the command argument of the delete button
            LinkButton deleteAdminLink = (LinkButton)sender;
            string userIdToDelete = deleteAdminLink.CommandArgument;

            // Store the userIdToDelete in a session variable
            Session["userIdToDelete"] = userIdToDelete;

            // Display the confirmation popup or directly call the delete function
            // For example: show confirmation popup
            popUpDelete.Style.Add("display", "flex");
        }

        protected void btnConfirmDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordForDelete.Text))
            {
                if (passwordForDelete.Text == "12345")
                {
                    // Check if Session["userIdToDelete"] is not null before using it
                    if (Session["userIdToDelete"] != null)
                    {
                        // Get the userIdToDelete from the session variable
                        string userIdToDelete = Session["userIdToDelete"].ToString();

                        // Call the delete function with userIdToDelete
                        deleteUser(userIdToDelete);

                        // Redirect to refresh the page
                        Response.Redirect(Request.RawUrl);
                    }
                }
            }
        }

        private void deleteUser(string userId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM [User] WHERE user_id = @userId";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
            // Optionally, hide the delete confirmation popup
            popUpDelete.Style.Add("display", "none");
        }

        //snackbar
        protected void ShowNotification(string message, string type)
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }

        protected void clearDateFilter_Click(object sender, EventArgs e)
        {
            lblDate.Text = "Select Date";
            ViewState["onePageStartDate"] = "";
            ViewState["onePageEndDate"] = "";
            BindListView(0, pageSize, ViewState["FilterStatus"].ToString());
        }

        protected void btnApplyDateFilter_Click(object sender, EventArgs e)
        {
            DateTime startDate;
            DateTime endDate;

            if (!DateTime.TryParse(txtStartDate.Text, out startDate))
            {
                ShowNotification("Missing Inputs", "warning");
                txtStartDate.CssClass += " border-red-800 border-2";
                return;
            }

            // Check if the end date is a valid date
            if (!DateTime.TryParse(txtEndDate.Text, out endDate))
            {
                ShowNotification("Missing Inputs", "warning");
                txtEndDate.CssClass += " border-red-800 border-2";
                return;
            }

            // Optional: Check if the start date is before the end date
            if (startDate > endDate)
            {
                return;
            }
            if (startDate != null && endDate != null)
            {
                lblDate.Text = startDate.ToString("dd/MM/yyyy") + " - " + endDate.ToString("dd/MM/yyyy");
                ViewState["onePageStartDate"] = startDate;
                ViewState["onePageEndDate"] = endDate;
                BindListView(0, pageSize, ViewState["FilterStatus"].ToString());
            }
            else
            {
                txtEndDate.CssClass += "border-red-800";
            }
        }
        protected void cancelDate_click(object sender, EventArgs e)
        {
            pnlDateFilter.Style.Add("display", "none");
        }

        //date filter funtions
        protected void filterDateBtn_click(object sender, EventArgs e)
        {
            pnlDateFilter.Style.Add("display", "flex");
        }
    }
}