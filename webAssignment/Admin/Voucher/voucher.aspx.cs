using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;
using webAssignment.Admin.Product_Management;

namespace webAssignment.Admin.Voucher
{
    public partial class voucher : System.Web.UI.Page, IFilterable
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private int pageSize = 5;
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                ViewState["PageIndex"] = 0;
                ViewState["FilterStatus"] = "";
                ViewState["onePageStartDate"] = "";
                ViewState["onePageEndDate"] = "";
                BindListView(0, pageSize, "");
            }
        }
        private void BindListView( int pageIndex, int pageSize, string status )
        {
            voucherListView.DataSource = GetVoucherData(pageIndex, pageSize, status ?? ViewState["FilterStatus"] as string);
            voucherListView.DataBind();
        }
        private List<Voucher> GetVoucherData( int pageIndex, int pageSize, string status )
        {

            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;


            if ( ViewState["onePageStartDate"].ToString() != "" && ViewState["onePageEndDate"].ToString() != "" )
            {
                sortStartDate = (DateTime)ViewState["onePageStartDate"];
                sortEndDate = (DateTime)ViewState["onePageEndDate"];
            }
            List<Voucher> vouchers = new List<Voucher>();

            string filter = ViewState["Filter"] as string;
            string sortExpression = ViewState["SortExpression"] as string ?? "voucher_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";

            List<string> conditions = new List<string>();
            if ( !string.IsNullOrEmpty(status) )
            {
                conditions.Add("voucher_status = @status");
            }
            if ( sortStartDate.HasValue && sortEndDate.HasValue )
            {
                conditions.Add("added_date >= @startDate AND added_date <= @endDate");
            }
            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";

            string sql = $@"SELECT * FROM Voucher {( whereClause )}
                ORDER BY {sortExpression} {sortDirection}
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();

                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@Offset", pageIndex * pageSize);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    if ( sortStartDate.HasValue && sortEndDate.HasValue )
                    {
                        cmd.Parameters.AddWithValue("@startDate", sortStartDate.Value);
                        cmd.Parameters.AddWithValue("@endDate", sortEndDate.Value);
                    }
                    if ( !string.IsNullOrEmpty(status) )
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                    }

                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            vouchers.Add(new Voucher
                            {
                                voucher_id = reader.GetString(0),
                                quantity = reader.GetInt32(1),
                                discount_rate = (reader.GetDouble(2) * 100),
                                cap_at = reader.GetDecimal(3),
                                min_spend = reader.GetDecimal(4),
                                added_date = reader.GetDateTime(5),
                                started_date = reader.GetDateTime(6),
                                expiry_date = reader.GetDateTime(7),
                                voucher_status = reader.GetString(8)
                            });
                        }
                    }
                }
            }

            return vouchers;
        }
        private int GetTotalVouchers( string status )
        {
            int totalCount = 0;

            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;


            if ( ViewState["onePageStartDate"].ToString() != "" && ViewState["onePageEndDate"].ToString() != "" )
            {
                sortStartDate = (DateTime)ViewState["onePageStartDate"];
                sortEndDate = (DateTime)ViewState["onePageEndDate"];
            }
            List<string> conditions = new List<string>();
            if ( !string.IsNullOrEmpty(status) )
            {
                conditions.Add("voucher_status = @status");
            }
            if ( sortStartDate.HasValue && sortEndDate.HasValue )
            {
                conditions.Add("added_date >= @startDate AND added_date <= @endDate");
            }

            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                // Initialize the SQL command
                string sql = $@"SELECT COUNT(*) FROM Voucher {( whereClause )}";

                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    if ( sortStartDate.HasValue && sortEndDate.HasValue )
                    {
                        cmd.Parameters.AddWithValue("@startDate", sortStartDate.Value);
                        cmd.Parameters.AddWithValue("@endDate", sortEndDate.Value);
                    }
                    if ( !string.IsNullOrEmpty(status) )
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                    }

                    // Execute the command and retrieve the count of vouchers
                    totalCount = (int)cmd.ExecuteScalar();
                }
            }

            return totalCount;
        }
        private List<Voucher> GetAllVoucher( )
        {
            List<Voucher> vouchers = new List<Voucher>();

            string sql = "SELECT * FROM Voucher";
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            vouchers.Add(new Voucher
                            {
                                voucher_id = reader.GetString(0),
                                quantity = reader.GetInt32(1),
                                discount_rate = reader.GetDouble(2),
                                cap_at = reader.GetDecimal(3),
                                min_spend = reader.GetDecimal(4),
                                added_date = reader.GetDateTime(5),
                                started_date = reader.GetDateTime(6),
                                expiry_date = reader.GetDateTime(7),
                                voucher_status = reader.GetString(8)
                            });
                        }
                    }
                }
            }


            return vouchers;
        }
        protected void voucherListView_ItemCommand( object sender, ListViewCommandEventArgs e )
        {
            if ( e.CommandName == "editVoucher" )
            {
                string voucherCodeID = e.CommandArgument.ToString();
                string encryptedStr = EncryptString(voucherCodeID);
                Response.Redirect($"~/Admin/Voucher/editVoucher.aspx?voucherCodeID={encryptedStr}");
            }
            else if ( e.CommandName == "deleteVoucher" )
            {
                popUpDelete.Style.Add("display", "flex");
                Session["CategoryIdDel"] = e.CommandArgument.ToString();
            }
        }

        private void deleteVoucher( string voucher_id )
        {

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {

                string query = "UPDATE Voucher SET voucher_status = 'Expired' WHERE voucher_id = @VoucherID";

                using ( SqlCommand command = new SqlCommand(query, connection) )
                {
                    command.Parameters.AddWithValue("@VoucherID", voucher_id);
                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                    Session["CategoryIdDel"] = null;
                    BindListView((int)ViewState["PageIndex"], pageSize, ViewState["FilterStatus"].ToString());
                }
            }

            ShowNotification("Voucher status updated to expired.", "success");
        }

        // when on bound handle events
        protected void voucherListView_DataBound( object sender, EventArgs e )
        {
            Label pageNumFoot = voucherListView.FindControl("pageNumFoot") as Label;
            Label lblCurrPagination = voucherListView.FindControl("lblCurrPagination") as Label;

            if ( pageNumFoot != null )
            {
                int totalItems = GetTotalVouchers(ViewState["FilterStatus"].ToString());
                int currentPageIndex = ( (int)ViewState["PageIndex"] );
                int startRecord = ( currentPageIndex * pageSize ) + 1;
                int endRecord = ( currentPageIndex + 1 ) * pageSize;
                endRecord = ( endRecord > totalItems ) ? totalItems : endRecord;

                lblCurrPagination.Text = ( currentPageIndex + 1 ).ToString();
                pageNumFoot.Text = $"Showing {startRecord}-{endRecord} from {totalItems}";
            }

        }
        // for paginations
        protected void prevPage_Click( object sender, EventArgs e )
        {
            int pageIndex = (int)ViewState["PageIndex"];

            if ( pageIndex > 0 )
            {
                ViewState["PageIndex"] = pageIndex - 1;
                BindListView((int)ViewState["PageIndex"], pageSize, ViewState["FilterStatus"].ToString());
            }
        }
        protected void nextPage_Click( object sender, EventArgs e )
        {
            int pageIndex = (int)ViewState["PageIndex"];
            int totalVoucher = GetTotalVouchers(ViewState["FilterStatus"].ToString());

            if ( ( pageIndex + 1 ) * pageSize < totalVoucher )
            {
                pageIndex++;
                ViewState["PageIndex"] = pageIndex;
                BindListView((int)ViewState["PageIndex"], pageSize, ViewState["FilterStatus"].ToString());
            }
        }
        //sorting by clicking the table label functions
        protected void voucherListView_Sorting( object sender, ListViewSortEventArgs e )
        {
            List<Voucher> voucherList = GetAllVoucher();
            string sortDirection = GetSortDirection(e.SortExpression);
            IEnumerable<Voucher> sortVoucher;

            // Sorting the list using LINQ dynamically based on SortExpression and SortDirection
            if ( sortDirection == "ASC" )
            {
                sortVoucher = voucherList.OrderBy(x => GetPropertyValue(x, e.SortExpression));
            }
            else
            {
                sortVoucher = voucherList.OrderByDescending(x => GetPropertyValue(x, e.SortExpression));
            }
            voucherListView.DataSource = GetVoucherData((int)ViewState["PageIndex"], pageSize, ViewState["FilterStatus"].ToString());
            voucherListView.DataBind();
        }
        private object GetPropertyValue( object obj, string propName )
        {
            return obj.GetType().GetProperty(propName).GetValue(obj, null);
        }
        private string GetSortDirection( string column )
        {
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            if ( sortExpression != null && sortExpression == column )
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value is returned.
                string lastDirection = ViewState["SortDirection"] as string;
                if ( ( lastDirection != null ) && ( lastDirection == "ASC" ) )
                {
                    sortDirection = "DESC";
                }
            }

            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }
        protected string DetermineStatus( object expiryDate, object quantity )
        {
            if ( expiryDate == DBNull.Value || quantity == DBNull.Value )
                return "Data missing";

            DateTime expiry = Convert.ToDateTime(expiryDate);
            int qty = Convert.ToInt32(quantity);

            if ( DateTime.Now > expiry )
                return "Ended";
            else if ( qty <= 0 )
                return "Fully Claimed";
            else
                return "Ongoing";
        }
        protected string EncryptString( string clearText )
        {
            string EncryptionKey = "ABC123";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using ( Aes encryptor = Aes.Create() )
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using ( MemoryStream ms = new MemoryStream() )
                {
                    using ( CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write) )
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


        private void changeSelectedtabCss( string tabName )
        {
            resetfilterTabSttyle();
            switch ( tabName )
            {
                case "OnGoing":
                    ogFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                case "Expired":
                    expiredFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                case "Fully Claimed":
                    fullyClaimFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                case "Pending":
                    pendingFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                default:
                    allVoucherFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
            }
        }
        // filter tab functions
        private void resetfilterTabSttyle( )
        {
            RemoveCssClass(ogFilter, "text-blue-600");
            RemoveCssClass(ogFilter, "bg-gray-100");
            RemoveCssClass(expiredFilter, "text-blue-600");
            RemoveCssClass(expiredFilter, "bg-gray-100");
            RemoveCssClass(fullyClaimFilter, "text-blue-600");
            RemoveCssClass(fullyClaimFilter, "bg-gray-100");
            RemoveCssClass(allVoucherFilter, "text-blue-600");
            RemoveCssClass(allVoucherFilter, "bg-gray-100");
            RemoveCssClass(pendingFilter, "text-blue-600");
            RemoveCssClass(pendingFilter, "bg-gray-100");
        }
        private void RemoveCssClass( WebControl control, string classToRemove )
        {
            List<string> classes = control.CssClass.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            // Remove the specific class
            classes.Remove(classToRemove);

            control.CssClass = String.Join(" ", classes);
        }
        protected void allVoucherFilter_Click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "");
            changeSelectedtabCss("");
        }
        protected void pendingFilter_Click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "Pending";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Pending");
            changeSelectedtabCss("Pending");
        }
        protected void ogFilter_Click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "OnGoing";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "OnGoing");
            changeSelectedtabCss("OnGoing");
        }

        protected void expiredFilter_Click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "Expired";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Expired");
            changeSelectedtabCss("Expired");
        }

        protected void fullyClaimFilter_Click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "Fully Claimed";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Fully Claimed");
            changeSelectedtabCss("Fully Claimed");
        }


        // search bar filtering
        public void FilterListView( string searchTerm )
        {
            List<Voucher> dummyData = GetVoucherData(0, pageSize, ViewState["FilterStatus"].ToString());
            List<Voucher> filteredData = FilterDataTable(dummyData, searchTerm);

            voucherListView.DataSource = filteredData;
            voucherListView.DataBind();
        }

        private List<Voucher> FilterDataTable( List<Voucher> vouchers, string searchTerm )
        {
            searchTerm = searchTerm?.ToLower() ?? string.Empty; // Handle null search term and convert to lowercase

            return vouchers.Where(v =>
                v.voucher_id.ToLower().Contains(searchTerm) ||
                v.quantity.ToString().Contains(searchTerm) ||
                v.discount_rate.ToString().Contains(searchTerm) ||
                v.cap_at.ToString().Contains(searchTerm) ||
                v.min_spend.ToString().Contains(searchTerm) ||
                ( v.started_date.ToString("dd/MM/yyyy").Contains(searchTerm) ) ||
                ( v.expiry_date.ToString("dd/MM/yyyy").Contains(searchTerm) ) ||
                v.added_date.ToString("dd/MM/yyyy").Contains(searchTerm)
            ).ToList();
        }

        // pop up functions
        protected void closePopUp_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
            Session["CategoryIdDel"] = null;
        }
        protected void btnCancelDelete_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
            Session["CategoryIdDel"] = null;
        }

        protected void btnExport_Click( object sender, EventArgs e )
        {
            using ( var workbook = new XLWorkbook() )
            {
                var worksheet = workbook.Worksheets.Add("Vouchers");
                var currentRow = 1;

                // Assuming you want to export the headers
                worksheet.Cell(currentRow, 1).Value = "Voucher Code";
                worksheet.Cell(currentRow, 2).Value = "Quantity";
                worksheet.Cell(currentRow, 3).Value = "Discount Rate (%)";
                worksheet.Cell(currentRow, 4).Value = "Cap At";
                worksheet.Cell(currentRow, 5).Value = "Min Spend";
                worksheet.Cell(currentRow, 6).Value = "Added Date";
                worksheet.Cell(currentRow, 7).Value = "Started Date";
                worksheet.Cell(currentRow, 8).Value = "Expiry Date";
                worksheet.Cell(currentRow, 9).Value = "Voucher Status";

                List<Voucher> vouchers = GetAllVoucher();
                // Assuming 'categoryListView' is data-bound to a collection of categories
                foreach (  Voucher v in vouchers )
                {
                    if ( v != null )
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = v.voucher_id;
                        worksheet.Cell(currentRow, 2).Value = v.quantity;
                        worksheet.Cell(currentRow, 3).Value = v.discount_rate;
                        worksheet.Cell(currentRow, 4).Value = v.cap_at;
                        worksheet.Cell(currentRow, 5).Value = v.min_spend;
                        worksheet.Cell(currentRow, 6).Value = v.added_date;
                        worksheet.Cell(currentRow, 7).Value = v.started_date;
                        worksheet.Cell(currentRow, 8).Value = v.expiry_date;
                        worksheet.Cell(currentRow, 9).Value = v.voucher_status;

                    }
                }

                using ( var stream = new MemoryStream() )
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Vouchers.xlsx");
                    stream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }


        protected void filterDateBtn_click( object sender, EventArgs e )
        {
            pnlDateFilter.Style.Add("display", "flex");
        }

        protected void btnApplyDateFilter_Click( object sender, EventArgs e )
        {
            DateTime startDate;
            DateTime endDate;

            if ( !DateTime.TryParse(txtStartDate.Text, out startDate) )
            {
                ShowNotification("Missing Inputs", "warning");
                txtStartDate.CssClass += " border-red-800 border-2";
                return;
            }

            // Check if the end date is a valid date
            if ( !DateTime.TryParse(txtEndDate.Text, out endDate) )
            {
                ShowNotification("Missing Inputs", "warning");
                txtEndDate.CssClass += " border-red-800 border-2";
                return;
            }

            // Optional: Check if the start date is before the end date
            if ( startDate > endDate )
            {
                return;
            }
            if ( startDate != null && endDate != null )
            {
                labelDateRange.Text = startDate.ToString("dd/MM/yyyy") + " - " + endDate.ToString("dd/MM/yyyy");
                ViewState["onePageStartDate"] = startDate;
                ViewState["onePageEndDate"] = endDate;
                BindListView(0, pageSize, ViewState["FilterStatus"].ToString());
                pnlDateFilter.Style.Add("display", "none");
            }
            else
            {
                txtEndDate.CssClass += "border-red-800";
            }
        }
        protected void cancelDate_click( object sender, EventArgs e )
        {
            pnlDateFilter.Style.Add("display", "none");
        }


        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }

        protected void clearDateFilter_Click( object sender, EventArgs e )
        {
            labelDateRange.Text = "Select Date";
            ViewState["onePageStartDate"] = "";
            ViewState["onePageEndDate"] = "";
            BindListView(0, pageSize, ViewState["FilterStatus"].ToString());
        }

        protected void btnExportToExcel_Click( object sender, EventArgs e )
        {
            var vouchers = GetAllVoucher();  

            using ( var workbook = new XLWorkbook() )
            {
                var worksheet = workbook.Worksheets.Add("Vouchers");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Voucher ID";
                worksheet.Cell(currentRow, 2).Value = "Quantity";
                worksheet.Cell(currentRow, 3).Value = "Discount Rate (%)";
                worksheet.Cell(currentRow, 4).Value = "Cap At (RM)";
                worksheet.Cell(currentRow, 5).Value = "Min Spend (RM)";
                worksheet.Cell(currentRow, 6).Value = "Added Date";
                worksheet.Cell(currentRow, 7).Value = "Started Date";
                worksheet.Cell(currentRow, 8).Value = "Expiry Date";
                worksheet.Cell(currentRow, 9).Value = "Status";

                foreach ( var voucher in vouchers )
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = voucher.voucher_id;
                    worksheet.Cell(currentRow, 2).Value = voucher.quantity;
                    worksheet.Cell(currentRow, 3).Value = voucher.discount_rate;
                    worksheet.Cell(currentRow, 4).Value = voucher.cap_at;
                    worksheet.Cell(currentRow, 5).Value = voucher.min_spend;
                    worksheet.Cell(currentRow, 6).Value = voucher.added_date.ToString("dd/MM/yyyy");
                    worksheet.Cell(currentRow, 7).Value = voucher.started_date.ToString("dd/MM/yyyy");
                    worksheet.Cell(currentRow, 8).Value = voucher.expiry_date.ToString("dd/MM/yyyy");
                    worksheet.Cell(currentRow, 9).Value = voucher.voucher_status;
                }

                worksheet.Columns().AdjustToContents(); // Adjust column width to content

                using ( var stream = new MemoryStream() )
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Vouchers.xlsx");
                    stream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        protected void btnConfirmDelete_Click( object sender, EventArgs e )
        {
            if ( passwordForDelete.Text.ToString() == "12345" )
            {
                string id = Session["CategoryIdDel"].ToString();
                if ( id != null )
                {
                    deleteVoucher(id);
                    popUpDelete.Style.Add("display", "none");
                }
            }
            else
            {
                ShowNotification("Invalid Password", "warning");
            }

        }
    }
}