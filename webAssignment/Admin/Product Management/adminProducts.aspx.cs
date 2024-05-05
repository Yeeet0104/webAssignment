using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.VariantTypes;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony.Parsing;
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
using System.Web.UI.WebControls;
using System.Xml.Linq;
using webAssignment.Admin.Layout;
using webAssignment.Admin.Product_Management;

namespace webAssignment
{
    public partial class adminProducts : System.Web.UI.Page, IFilterable
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
            productListView.DataSource = getProductData(pageIndex, pageSize, status ?? ViewState["FilterStatus"] as string);
            productListView.DataBind();
        }

        // product list view functions
        // when on bound handle events
        protected void productListView_DataBound( object sender, EventArgs e )
        {
            Label pageNumFoot = productListView.FindControl("pageNumFoot") as Label;
            Label lblCurrPagination = productListView.FindControl("lblCurrPagination") as Label;

            if ( pageNumFoot != null )
            {
                int totalItems = GetTotalProductsCount(ViewState["FilterStatus"].ToString());
                int currentPageIndex = ( (int)ViewState["PageIndex"] );
                int startRecord = ( currentPageIndex * pageSize ) + 1;
                int endRecord = ( currentPageIndex + 1 ) * pageSize;
                endRecord = ( endRecord > totalItems ) ? totalItems : endRecord;

                lblCurrPagination.Text = ( currentPageIndex + 1 ).ToString();
                pageNumFoot.Text = $"Showing {startRecord}-{endRecord} from {totalItems}";
            }

        }
        protected void productListView_SelectedIndexChanged( object sender, EventArgs e )
        {

        }
        protected void productListView_ItemCommand( object sender, ListViewCommandEventArgs e )
        {
            if ( e.CommandName == "EditProduct" )
            {
                string productID = e.CommandArgument.ToString();
                string encryptedStr = EncryptString(productID);
                Response.Redirect($"~/Admin/Product Management/editProduct.aspx?ProdID={encryptedStr}");
            }
            else if ( e.CommandName == "DeleteProduct" )
            {
                string[] arrays = e.CommandArgument.ToString().Split(';');
                popUpDelete.Style.Add("display", "flex");
                lblmodalProdID.Text = arrays[0];
                lblmodalProdName.Text = arrays[1];
                Session["productIdDel"] = arrays[0];
            }
            else if ( e.CommandName == "viewItems" )
            {
                string productID = e.CommandArgument.ToString();
                string encryptedStr = EncryptString(productID);
                Response.Redirect($"~/Admin/Product Management/productVariant.aspx?productID={encryptedStr}");
            }
        }



        // all the init for the nessary product datas
        private List<productsList> getProductData( int pageIndex, int pageSize, string status )
        {
            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;
            // Check Session and assign dates if available
            if ( ViewState["onePageStartDate"].ToString() != "" && ViewState["onePageEndDate"].ToString() != "" )
            {
                sortStartDate = (DateTime)ViewState["onePageStartDate"];
                sortEndDate = (DateTime)ViewState["onePageEndDate"];
            }
            var productsList = new List<productsList>();
            string sortExpression = ViewState["SortExpression"] as string ?? "product_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";

            List<string> conditions = new List<string>();
            if ( !string.IsNullOrEmpty(status) )
            {
                conditions.Add("p.product_status = @product_status");
            }
            if ( sortStartDate.HasValue && sortEndDate.HasValue )
            {
                conditions.Add("p.date_added >= @startDate AND p.date_added <= @endDate");
            }

            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";


            string sql = $@"SELECT 
                                c.category_name,
                                p.product_id, 
                                p.product_name, 
                                p.date_added,
                                p.product_status,
                                SUM(pv.stock) AS total_stock,
                                COUNT(pv.product_variant_id) AS total_variants, 
                                (SELECT TOP 1 ip.path FROM image_path ip WHERE ip.product_id = p.product_id) AS first_image_path
                            FROM 
                                category c 
                            INNER JOIN 
                                product p ON c.category_id = p.category_id 
                            LEFT JOIN 
                                product_variant pv ON p.product_id = pv.product_id
                            {whereClause}
                            GROUP BY 
                                c.category_name,
                                p.product_id, 
                                p.product_name, 
                                p.date_added,
                                p.product_status
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
                        cmd.Parameters.AddWithValue("@product_status", status);
                    }
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            productsList.Add(new productsList
                            {
                                CategoryName = reader.GetString(0),
                                ProductID = reader.GetString(1),
                                ProductName = reader.GetString(2),
                                date_added = reader.GetDateTime(3),
                                ProductVariantID = "",
                                VariantPrice = 0,
                                variantCount = reader.GetInt32(6).ToString(),
                                total_stock = reader.GetInt32(5),
                                ProductStatus = reader.GetString(4),
                                ProductImageUrl = reader.GetString(7)
                            });
                        }
                    }
                }
            }
            return productsList;
        }
        private List<productsList> getTotalProducts( )
        {
            var productsList = new List<productsList>();
            string filter = ViewState["Filter"] as string;
            string sortExpression = ViewState["SortExpression"] as string ?? "product_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";
            string sql = $@"SELECT 
                        c.category_name,
                        p.product_id, 
                        p.product_name, 
                        p.date_added,
                        pv.product_variant_id, 
                        pv.variant_price, 
                        pv.variant_name, 
                        pv.stock, 
                        p.product_status,
                        (SELECT TOP 1 ip.path FROM Image_Path ip WHERE ip.product_id = p.product_id) AS first_image_path
                    FROM 
                        Category c 
                    LEFT JOIN 
                        Product p ON c.category_id = p.category_id 
                    LEFT JOIN 
                        Product_Variant pv ON p.product_id = pv.product_id
                    WHERE 
                        p.product_id IS NOT NULL
                    GROUP BY 
                        c.category_id, c.category_name,
                        p.product_id, p.product_name, p.product_description, p.date_added,
                        pv.product_variant_id, pv.variant_price, pv.variant_name, pv.stock, p.product_status";


            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {

                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {

                        while ( reader.Read() )
                        {
                            productsList.Add(new productsList
                            {
                                CategoryName = reader.GetString(0),
                                ProductID = reader.GetString(1),
                                ProductName = reader.GetString(2),
                                date_added = reader.GetDateTime(3),
                                ProductVariantID = reader.GetString(4),
                                VariantPrice = reader.GetDecimal(5),
                                variantCount = reader.GetString(6),
                                total_stock = reader.GetInt32(7),
                                ProductStatus = reader.GetString(8),
                                ProductImageUrl = reader.GetString(9)
                            });
                        }
                    }
                }
            }
            return productsList;
        }
        private int GetTotalProductsCount( string status = "" )
        {
            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;
            // Check Session and assign dates if available
            if ( ViewState["onePageStartDate"].ToString() != "" && ViewState["onePageEndDate"].ToString() != "" )
            {
                sortStartDate = (DateTime)ViewState["onePageStartDate"];
                sortEndDate = (DateTime)ViewState["onePageEndDate"];
            }
            var productsList = new List<productsList>();
            string sortExpression = ViewState["SortExpression"] as string ?? "product_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";

            List<string> conditions = new List<string>();
            if ( !string.IsNullOrEmpty(status) )
            {
                conditions.Add("p.product_status = @product_status");
            }
            if ( sortStartDate.HasValue && sortEndDate.HasValue )
            {
                conditions.Add("p.date_added >= @startDate AND p.date_added <= @endDate");
            }

            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";


            string countSql = $@"SELECT COUNT(*)
                        FROM Product p 
                        JOIN Category c ON p.category_id = c.category_id
                        {whereClause}";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(countSql, conn) )
                {
                    if ( sortStartDate.HasValue && sortEndDate.HasValue )
                    {
                        cmd.Parameters.AddWithValue("@startDate", sortStartDate.Value);
                        cmd.Parameters.AddWithValue("@endDate", sortEndDate.Value);
                    }
                    if ( !string.IsNullOrEmpty(status) )
                    {
                        cmd.Parameters.AddWithValue("@product_status", status);
                    }
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        //private List<productsList> getProductDataByDate( int pageIndex, int pageSize, DateTime startDate, DateTime endDate )
        //{
        //    var productsList = new List<productsList>();
        //    string filter = ViewState["Filter"] as string;
        //    string sortExpression = ViewState["SortExpression"] as string ?? "product_id";
        //    string sortDirection = ViewState["SortDirection"] as string ?? "ASC";
        //    string sql = $@"SELECT 
        //                    c.category_name,
        //                    p.product_id, 
        //                    p.product_name, 
        //                    p.date_added,
        //                    p.product_status,
	       //                 sum(pv.stock) as total_stock,
        //                    count(pv.product_variant_id) as total_variants, -- total count of variants for each product
        //                    (select top 1 ip.path from image_path ip where ip.product_id = p.product_id) as first_image_path
        //                from 
        //                    category c 
        //                inner join 
        //                    product p on c.category_id = p.category_id 
        //                left join 
        //                    product_variant pv on p.product_id = pv.product_id
        //                WHERE
        //                    p.date_added >= @startDate AND p.date_added <= @endDate
        //                group by 
        //                    c.category_name,
        //                    p.product_id, 
        //                    p.product_name, 
        //                    p.date_added,
        //                    p.product_status
        //                ORDER BY {sortExpression} {sortDirection}
        //                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";



        //    using ( SqlConnection conn = new SqlConnection(connectionString) )
        //    {
        //        conn.Open();
        //        using ( SqlCommand cmd = new SqlCommand(sql, conn) )
        //        {
        //            cmd.Parameters.AddWithValue("@Offset", pageIndex * pageSize);
        //            cmd.Parameters.AddWithValue("@PageSize", pageSize);
        //            cmd.Parameters.AddWithValue("@startDate", startDate);
        //            cmd.Parameters.AddWithValue("@endDate", endDate);


        //            using ( SqlDataReader reader = cmd.ExecuteReader() )
        //            {

        //                while ( reader.Read() )
        //                {
        //                    productsList.Add(new productsList
        //                    {
        //                        CategoryName = reader.GetString(0),
        //                        ProductID = reader.GetString(1),
        //                        ProductName = reader.GetString(2),
        //                        date_added = reader.GetDateTime(3),
        //                        ProductVariantID = "",
        //                        VariantPrice = 0,
        //                        variantCount = reader.GetInt32(6).ToString(),
        //                        total_stock = reader.GetInt32(5),
        //                        ProductStatus = reader.GetString(4),
        //                        ProductImageUrl = reader.GetString(7)
        //                    });
        //                }
        //            }
        //        }
        //    }
        //    return productsList;
        //}

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
            int totalProducts = GetTotalProductsCount(ViewState["FilterStatus"].ToString());

            if ( ( pageIndex + 1 ) * pageSize < totalProducts )
            {
                pageIndex++;
                ViewState["PageIndex"] = pageIndex;
                BindListView((int)ViewState["PageIndex"], pageSize, ViewState["FilterStatus"].ToString());
            }
        }

        //sorting by clicking the table label functions
        protected void productListView_Sorting( object sender, ListViewSortEventArgs e )
        {
            List<productsList> categories = getTotalProducts();
            string sortDirection = GetSortDirection(e.SortExpression);
            IEnumerable<productsList> sortProd;

            // Sorting the list using LINQ dynamically based on SortExpression and SortDirection
            if ( sortDirection == "ASC" )
            {
                sortProd = categories.OrderBy(x => GetPropertyValue(x, e.SortExpression));
            }
            else
            {
                sortProd = categories.OrderByDescending(x => GetPropertyValue(x, e.SortExpression));
            }
            productListView.DataSource = getProductData((int)ViewState["PageIndex"], pageSize, ViewState["FilterStatus"].ToString());
            productListView.DataBind();
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
                    Debug.Write("babakqweqwelastqweeqwe" + sortDirection);
                }
            }

            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;

            return sortDirection;
        }

        // encrpyting to send the product id in the query
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
            List<productsList> categoryData = getProductData(0, pageSize, "");
            List<productsList> filteredData = FilterCategoryList(categoryData, searchTerm);

            productListView.DataSource = filteredData;
            productListView.DataBind();
        }

        private List<productsList> FilterCategoryList( List<productsList> prodlist, string searchTerm )
        {
            string safeSearchTerm = searchTerm.Replace("'", "''").ToLower();

            // Using LINQ to filter the list of categories by name
            var productsLists = prodlist.Where(p =>
                p.CategoryName.ToLower().Contains(searchTerm) ||
                p.total_stock.ToString().Contains(searchTerm) ||
                p.ProductName.ToLower().Contains(searchTerm) ||
                p.ProductStatus.ToLower().Contains(searchTerm) ||
                p.variantCount.ToLower().Contains(searchTerm) ||
                p.VariantPrice.ToString().Contains(searchTerm) ||
                p.date_added.ToString("dd/MM/yyyy").Contains(searchTerm)).ToList(); ;

            return productsLists;
        }


        // filter tab functions
        protected void closePopUp_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
        }
        protected void btnCancelDelete_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
        }
        protected void btnConfirmDelete_Click( object sender, EventArgs e )
        {
            if ( passwordForDelete.Text.ToString() != "" )
            {
                if ( Page.IsValid && passwordForDelete.Text.ToString() == "12345" )
                {
                    deleteProduct(Session["productIdDel"].ToString());
                }
            }
        }
        protected void publishFilter_click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "Publish";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Publish");
            changeSelectedtabCss("Publish");
        }

        protected void draftFilter_click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "Draft";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Draft");
            changeSelectedtabCss("Draft");
        }

        protected void discontinuedFilter_click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "Discontinued";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Discontinued");
            changeSelectedtabCss("Discontinued");
        }

        protected void allProductFilter_click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "");
            changeSelectedtabCss("");
        }
        private void changeSelectedtabCss( string tabName )
        {
            resetfilterTabSttyle();
            switch ( tabName )
            {
                case "Publish":
                    publishFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                case "Draft":
                    draftFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                case "Discontinued":
                    discontinuedFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                default:
                    allProductFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;

            }
        }

        private void resetfilterTabSttyle( )
        {
            RemoveCssClass(publishFilter, "text-blue-600");
            RemoveCssClass(publishFilter, "bg-gray-100");
            RemoveCssClass(draftFilter, "text-blue-600");
            RemoveCssClass(draftFilter, "bg-gray-100");
            RemoveCssClass(discontinuedFilter, "text-blue-600");
            RemoveCssClass(discontinuedFilter, "bg-gray-100");
            RemoveCssClass(allProductFilter, "text-blue-600");
            RemoveCssClass(allProductFilter, "bg-gray-100");
        }
        private void RemoveCssClass( WebControl control, string classToRemove )
        {
            List<string> classes = control.CssClass.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            // Remove the specific class
            classes.Remove(classToRemove);

            control.CssClass = String.Join(" ", classes);
        }

        //date filter funtions
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
        protected void cancelDate_click( object sender, EventArgs e )
        {
            pnlDateFilter.Style.Add("display", "none");
        }



        private void deleteProduct( string productID )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = "UPDATE Product SET product_Status = @status WHERE product_id = @productID";
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@productID", productID);
                    cmd.Parameters.AddWithValue("@status", "Discontinued");
                    cmd.ExecuteNonQuery();
                    popUpDelete.Style.Add("display", "none");
                }
            }
            Response.Redirect(Request.RawUrl);
        }

        //protected void btnExport_Click( object sender, EventArgs e )
        //{
        //    using ( var workbook = new XLWorkbook() )
        //    {
        //        var worksheet = workbook.Worksheets.Add("Categories");
        //        var currentRow = 1;

        //        // Assuming you want to export the headers
        //        worksheet.Cell(currentRow, 1).Value = "Category Name";
        //        worksheet.Cell(currentRow, 2).Value = "Total Sold";
        //        worksheet.Cell(currentRow, 3).Value = "Stock";
        //        worksheet.Cell(currentRow, 4).Value = "Date Added";

        //        List<Category> categories = getProductData(0, GetTotalCategoriesCount() + 1);
        //        // Assuming 'categoryListView' is data-bound to a collection of categories
        //        foreach ( Category item in categories )
        //        {
        //            if ( item != null )
        //            {
        //                currentRow++;
        //                worksheet.Cell(currentRow, 1).Value = item.CategoryName;
        //                worksheet.Cell(currentRow, 2).Value = item.Sold;
        //                worksheet.Cell(currentRow, 3).Value = item.Stock;
        //                worksheet.Cell(currentRow, 4).Value = item.date_added;
        //            }
        //        }

        //        using ( var stream = new MemoryStream() )
        //        {
        //            workbook.SaveAs(stream);
        //            stream.Position = 0;
        //            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //            Response.AddHeader("content-disposition", "attachment;filename=Categories.xlsx");
        //            stream.WriteTo(Response.OutputStream);
        //            Response.Flush();
        //            Response.End();
        //        }
        //    }
        //}


        //snackbar
        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }

        protected void clearDateFilter_Click( object sender, EventArgs e )
        {
            lblDate.Text = "Select Date";
            ViewState["onePageStartDate"] = "";
            ViewState["onePageEndDate"] = "";
            BindListView(0, pageSize, ViewState["FilterStatus"].ToString());
        }
    }
}