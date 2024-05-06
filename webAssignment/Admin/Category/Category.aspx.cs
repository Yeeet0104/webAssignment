using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;

namespace webAssignment.Admin.Category
{

    public partial class Category : System.Web.UI.Page, IFilterable
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private int pageSize = 5;
        protected void Page_Load( object sender, EventArgs e )
        {

            if ( !IsPostBack )
            {
                ViewState["PageIndex"] = 0;
                ViewState["onePageStartDate"] = "";
                ViewState["onePageEndDate"] = "";
                BindListView(0, pageSize);
            }
        }
        // binding into listview
        private void BindListView( int pageIndex, int pageSize )
        {
            categoryListView.DataSource = getCategoryData(pageIndex, pageSize);
            categoryListView.DataBind();
        }
        // when on bound handle events
        protected void categoryListView_DataBound( object sender, EventArgs e )
        {
            Label pageNumFoot = categoryListView.FindControl("pageNumFoot") as Label;
            Label lblCurrPagination = categoryListView.FindControl("lblCurrPagination") as Label;

            if ( pageNumFoot != null )
            {
                int totalItems = GetTotalCategoriesCount();
                int currentPageIndex = ( (int)ViewState["PageIndex"] );
                int startRecord = ( currentPageIndex * pageSize ) + 1;
                int endRecord = ( currentPageIndex + 1 ) * pageSize;
                endRecord = ( endRecord > totalItems ) ? totalItems : endRecord;

                lblCurrPagination.Text = ( currentPageIndex + 1 ).ToString();
                pageNumFoot.Text = $"Showing {startRecord}-{endRecord} from {totalItems}";
            }

        }
        //accessing to db to get category data for paginations
        private List<Category> getCategoryData( int pageIndex, int pageSize )
        {
            List<Category> categories = new List<Category>();
            string filter = ViewState["Filter"] as string;
            string sortExpression = ViewState["SortExpression"] as string ?? "category_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";

            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;


            if ( ViewState["onePageStartDate"].ToString() != "" && ViewState["onePageEndDate"].ToString() != "" )
            {
                sortStartDate = (DateTime)ViewState["onePageStartDate"];
                sortEndDate = (DateTime)ViewState["onePageEndDate"];
            }

            List<string> conditions = new List<string>();
            if ( sortStartDate.HasValue && sortEndDate.HasValue )
            {
                conditions.Add("c.date_added >= @startDate AND c.date_added <= @endDate");
            }
            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";

            string sql = $@"SELECT 
                        c.category_id,
                        c.category_name,
                        c.tumbnail_img_path,
                        c.date_added,
                        COUNT(DISTINCT p.product_id) AS NumberOfProd,
                        SUM(ISNULL(od.quantity, 0)) AS Sold,
                        SUM(ISNULL(pv.stock, 0)) AS Stock
                        FROM 
                            Category c
                        LEFT JOIN 
                            Product p ON c.category_id = p.category_id
                        LEFT JOIN 
                            Product_Variant pv ON p.product_id = pv.product_id
                        LEFT JOIN 
                            Order_details od ON pv.product_variant_id = od.product_variant_id
                        
                        {whereClause}
                        GROUP BY 
                            c.category_id, c.category_name, c.tumbnail_img_path, c.date_added
                        ORDER BY {sortExpression} {sortDirection}
                        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    //cmd.Parameters.AddWithValue("@Filter", filter ?? string.Empty);
                    cmd.Parameters.AddWithValue("@Offset", pageIndex * pageSize);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    if ( sortStartDate.HasValue && sortEndDate.HasValue )
                    {
                        cmd.Parameters.AddWithValue("@startDate", sortStartDate.Value);
                        cmd.Parameters.AddWithValue("@endDate", sortEndDate.Value);
                    }
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            categories.Add(new Category
                            {
                                CategoryID = reader.GetString(0),
                                CategoryName = reader.GetString(1),
                                CategoryBanner = reader.GetString(2),
                                date_added = reader.GetDateTime(3),
                                NumberOfProd = reader.GetInt32(4),
                                Sold = reader.GetInt32(5),
                                Stock = reader.GetInt32(6)
                            });
                        }
                    }
                }
            }
            return categories;
        }
        // retrieve all items
        private List<Category> getAllCategories( )
        {
            List<Category> categories = new List<Category>();
            string sql = @"SELECT 
                        c.category_id,
                        c.category_name,
                        c.tumbnail_img_path,
                        c.date_added,
                        COUNT(DISTINCT p.product_id) AS NumberOfProd,
                        SUM(ISNULL(od.quantity, 0)) AS Sold,
                        SUM(ISNULL(pv.stock, 0)) AS Stock
                        FROM 
                            Category c
                        LEFT JOIN 
                            Product p ON c.category_id = p.category_id
                        LEFT JOIN 
                            Product_Variant pv ON p.product_id = pv.product_id
                        LEFT JOIN 
                            Order_details od ON pv.product_variant_id = od.product_variant_id
                        GROUP BY 
                            c.category_id, c.category_name, c.tumbnail_img_path, c.date_added
                        ORDER BY category_id";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            categories.Add(new Category
                            {
                                CategoryID = reader.GetString(0),
                                CategoryName = reader.GetString(1),
                                CategoryBanner = reader.GetString(2),
                                date_added = reader.GetDateTime(3),
                                NumberOfProd = reader.GetInt32(4),
                                Sold = reader.GetInt32(5),
                                Stock = reader.GetInt32(6),
                            });
                        }
                    }
                }
            }
            return categories;
        }
        private int GetTotalCategoriesCount( )
        {
            int total = 0;

            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;


            if ( ViewState["onePageStartDate"].ToString() != "" && ViewState["onePageEndDate"].ToString() != "" )
            {
                sortStartDate = (DateTime)ViewState["onePageStartDate"];
                sortEndDate = (DateTime)ViewState["onePageEndDate"];
            }

            List<string> conditions = new List<string>();
            if ( sortStartDate.HasValue && sortEndDate.HasValue )
            {
                conditions.Add("date_added >= @startDate AND date_added <= @endDate");
            }
            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";



            string sql = $@"SELECT COUNT(*) FROM Category {whereClause}";
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();

                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    if ( sortStartDate.HasValue && sortEndDate.HasValue )
                    {
                        cmd.Parameters.AddWithValue("@startDate", sortStartDate.Value);
                        cmd.Parameters.AddWithValue("@endDate", sortEndDate.Value);
                    }
                    total = (int)cmd.ExecuteScalar();
                }
            }
            return total;
        }

        // for paginations
        protected void prevPage_Click( object sender, EventArgs e )
        {
            int pageIndex = (int)ViewState["PageIndex"];

            if ( pageIndex > 0 )
            {
                ViewState["PageIndex"] = pageIndex - 1;
                BindListView((int)ViewState["PageIndex"], pageSize);
            }
        }
        protected void nextPage_Click( object sender, EventArgs e )
        {
            int pageIndex = (int)ViewState["PageIndex"];
            int totalCategories = GetTotalCategoriesCount();

            if ( ( pageIndex + 1 ) * pageSize < totalCategories )
            {
                ViewState["PageIndex"] = pageIndex + 1;
                BindListView((int)ViewState["PageIndex"], pageSize);
            }
        }



        // events handling for the table data
        protected void categoryListView_SelectedIndexChanged( object sender, EventArgs e )
        {

        }
        protected void categoryListView_ItemCommand( object sender, ListViewCommandEventArgs e )
        {
            if ( e.CommandName == "EditCategory" )
            {
                string categoryID = e.CommandArgument.ToString();
                string encryptedStr = EncryptString(categoryID);
                Response.Redirect($"~/Admin/Category/editCategory.aspx?CategoryID={encryptedStr}");
            }
            else if ( e.CommandName == "DeleteCategory" )
            {
                // Show the popup
                popUpDelete.Style.Add("display", "flex");
                lblItemInfo.Text = e.CommandArgument.ToString();
                Session["CategoryIdDel"] = e.CommandArgument.ToString();
            }
        }
        protected void deletedCategory( string categID )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = "DELETE FROM Category WHERE category_id = @categID";

                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@categID", categID);
                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if ( rowsAffected > 0 )
                        {
                            Debug.Write("Delete successful");
                            categoryListView.DataSource = getCategoryData((int)ViewState["PageIndex"], pageSize);
                            categoryListView.DataBind();
                            popUpDelete.Style.Add("display", "none");
                            Session["CategoryIdDel"] = null;
                        }
                        else
                        {
                            ShowNotification("Delete failed: No row found with the specified ID", "warning");
                        }
                    }
                    catch ( SqlException ex )
                    {
                        if ( ex.Number == 547 ) // Check if the exception is a foreign key violation ( stack overflow )
                        {
                            ShowNotification("This category cannot be deleted because it is referenced by one or more products.", "warning");
                        }
                        else
                        {
                            ShowNotification("SQL Error: " + ex.Message, "warning");
                        }
                    }
                    catch ( Exception ex )
                    {
                        ShowNotification("General Error: " + ex.Message, "warning");
                    }
                }
            }

        }

        // for encrypting the Category id then pass it to the edit page
        protected string EncryptString( string clearText )
        {
            string EncryptionKey = "ABC123"; // Replace with a more complex key and store securely
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

        //Search bar filter functions
        public void FilterListView( string searchTerm )
        {
            List<Category> categoryData = getCategoryData(0, pageSize);
            List<Category> filteredData = FilterCategoryList(categoryData, searchTerm);

            categoryListView.DataSource = filteredData;
            categoryListView.DataBind();
        }

        // Lists and LINQ
        private List<Category> FilterCategoryList( List<Category> categories, string searchTerm )
        {
            searchTerm = searchTerm?.ToLower() ?? string.Empty; // Handle null search term and convert to lowercase
            return categories.Where(c =>
                c.CategoryID.ToLower().Contains(searchTerm) ||
                c.CategoryName.ToString().Contains(searchTerm) ||
                c.Sold.ToString().Contains(searchTerm) ||
                c.Stock.ToString().Contains(searchTerm) ||
                ( c.date_added.ToString("dd/MM/yyyy").Contains(searchTerm) )
            ).ToList();
        }

        //sorting by clicking the table label functions
        protected void ListView1_Sorting( object sender, ListViewSortEventArgs e )
        {
            List<Category> categories = getAllCategories();
            string sortDirection = GetSortDirection(e.SortExpression);
            IEnumerable<Category> sortedCategories;

            // Sorting the list using LINQ dynamically based on SortExpression and SortDirection
            if ( sortDirection == "ASC" )
            {
                sortedCategories = categories.OrderBy(x => GetPropertyValue(x, e.SortExpression));
            }
            else
            {
                sortedCategories = categories.OrderByDescending(x => GetPropertyValue(x, e.SortExpression));
            }


            categoryListView.DataSource = getCategoryData((int)ViewState["PageIndex"], pageSize);
            categoryListView.DataBind();
        }
        private object GetPropertyValue( object obj, string propName )
        {
            return obj.GetType().GetProperty(propName).GetValue(obj, null);
        }

        private string GetSortDirection( string column )
        {
            // default is ascending
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted which means that which tab have been selected jn
            string sortExpression = ViewState["SortExpression"] as string;

            Debug.Write("babakqweqwe" + sortExpression);
            if ( sortExpression != null && sortExpression == column )
            {
                string lastDirection = ViewState["SortDirection"] as string;
                Debug.Write("babakqweqwelast" + lastDirection);
                if ( ( lastDirection != null ) && ( lastDirection == "ASC" ) )
                {
                    sortDirection = "DESC";
                    Debug.Write("babakqweqwelastqweeqwe" + sortDirection);
                }
            }

            ViewState["SortDirection"] = sortDirection;
            Debug.Write("babakqweqwe" + sortDirection);
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        //exporting to excel file
        protected void btnExport_Click( object sender, EventArgs e )
        {
            using ( var workbook = new XLWorkbook() )
            {
                var worksheet = workbook.Worksheets.Add("Categories");
                var currentRow = 1;

                worksheet.Cell(currentRow, 1).Value = "Category Name";
                worksheet.Cell(currentRow, 2).Value = "Total Sold";
                worksheet.Cell(currentRow, 3).Value = "Stock";
                worksheet.Cell(currentRow, 4).Value = "Date Added";

                List<Category> categories = getCategoryData(0, GetTotalCategoriesCount() + 1);
                foreach ( Category item in categories )
                {
                    if ( item != null )
                    {
                        currentRow++;
                        worksheet.Cell(currentRow, 1).Value = item.CategoryName;
                        worksheet.Cell(currentRow, 2).Value = item.Sold;
                        worksheet.Cell(currentRow, 3).Value = item.Stock;
                        worksheet.Cell(currentRow, 4).Value = item.date_added;
                    }
                }

                using ( var stream = new MemoryStream() )
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Categories.xlsx");
                    stream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }

        // for Pop Up functions 
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
        protected void btnConfirmDelete_click( object sender, EventArgs e )
        {
            if ( passwordForDelete.Text.ToString() == "12345" )
            {
                string id = Session["CategoryIdDel"].ToString();
                if ( id != null )
                {
                    deletedCategory(id);
                }
            }
        }

        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
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

            //Check if the start date is before the end date
            if ( startDate > endDate )
            {
                ShowNotification("Start Date cannot more than end date!", "warning");
                return;
            }
            if ( startDate != null && endDate != null )
            {
                labelDateRange.Text = startDate.ToString("dd/MM/yyyy") + " - " + endDate.ToString("dd/MM/yyyy");
                ViewState["onePageStartDate"] = startDate;
                ViewState["onePageEndDate"] = endDate;

                BindListView(0, pageSize);
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
        protected void clearDateFilter_Click( object sender, EventArgs e )
        {
            labelDateRange.Text = "Select Date";
            ViewState["onePageStartDate"] = "";
            ViewState["onePageEndDate"] = "";
            BindListView(0, pageSize);
        }
        protected void btnExportToExcel_Click( object sender, EventArgs e )
        {
            var categories = getAllCategories(); 

            using ( var workbook = new XLWorkbook() )
            {
                var worksheet = workbook.Worksheets.Add("Categories");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "Category ID";
                worksheet.Cell(currentRow, 2).Value = "Category Name";
                worksheet.Cell(currentRow, 3).Value = "Thumbnail Image Path";
                worksheet.Cell(currentRow, 4).Value = "Date Added";
                worksheet.Cell(currentRow, 5).Value = "Number Of Products";
                worksheet.Cell(currentRow, 6).Value = "Total Sold";
                worksheet.Cell(currentRow, 7).Value = "Stock";

                foreach ( var category in categories )
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = category.CategoryID;
                    worksheet.Cell(currentRow, 2).Value = category.CategoryName;
                    worksheet.Cell(currentRow, 3).Value = category.CategoryBanner;
                    worksheet.Cell(currentRow, 4).Value = category.date_added.ToString("dd/MM/yyyy");
                    worksheet.Cell(currentRow, 5).Value = category.NumberOfProd;
                    worksheet.Cell(currentRow, 6).Value = category.Sold;
                    worksheet.Cell(currentRow, 7).Value = category.Stock;
                }

                worksheet.Columns().AdjustToContents(); // Adjust column width to content

                using ( var stream = new MemoryStream() )
                {
                    workbook.SaveAs(stream);
                    stream.Position = 0;
                    Response.Clear();
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment;filename=Categories.xlsx");
                    stream.WriteTo(Response.OutputStream);
                    Response.Flush();
                    Response.End();
                }
            }
        }


    }

}