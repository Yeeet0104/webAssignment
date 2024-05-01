using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
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
                BindListView(0, pageSize);

            }
        }
        private void BindListView( int pageIndex, int pageSize )
        {
            productListView.DataSource = getProductData(pageIndex, pageSize);
            productListView.DataBind();
        }
        // when on bound handle events
        protected void productListView_DataBound( object sender, EventArgs e )
        {
            Label pageNumFoot = productListView.FindControl("pageNumFoot") as Label;
            Label lblCurrPagination = productListView.FindControl("lblCurrPagination") as Label;

            if ( pageNumFoot != null )
            {
                int totalItems = 100;
                int pageSize = 5;
                int currentPageIndex = ( (int)ViewState["PageIndex"] );
                int startRecord = ( currentPageIndex * pageSize ) + 1;
                int endRecord = ( currentPageIndex + 1 ) * pageSize;
                endRecord = ( endRecord > totalItems ) ? totalItems : endRecord;

                lblCurrPagination.Text = ( currentPageIndex + 1 ).ToString();
                pageNumFoot.Text = $"Showing {startRecord}-{endRecord} from {totalItems}";
            }

        }
        private List<productsList> getProductData( int pageIndex, int pageSize )
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
	                    sum(pv.stock) as total_stock,
                        count(pv.product_variant_id) as total_variants, -- total count of variants for each product
                        (select top 1 ip.path from image_path ip where ip.product_id = p.product_id) as first_image_path
                    from 
                        category c 
                    inner join 
                        product p on c.category_id = p.category_id 
                    left join 
                        product_variant pv on p.product_id = pv.product_id
                    group by 
                        c.category_name,
                        p.product_id, 
                        p.product_name, 
                        p.date_added
                    ORDER BY {sortExpression} {sortDirection}
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";


            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@Offset", pageIndex * pageSize);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {

                        while ( reader.Read() )
                        {
                            productsList.Add(new productsList
                            {
                                CategoryName = reader.GetString(0),
                                ProductID = reader.GetString(1),
                                ProductName = reader.GetString(2),
                                DateAdded = reader.GetDateTime(3),
                                ProductVariantID = "",
                                VariantPrice = 0,
                                variantCount = reader.GetInt32(5).ToString(),
                                Stock = reader.GetInt32(4),
                                ProductStatus = "",
                                ProductImageUrl = reader.GetString(6)
                            });
                        }
                    }
                }
            }
            return productsList;
        }
        private List<productsList> getVariants( string productID )
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
                        pv.product_status,
                        (SELECT TOP 1 ip.path FROM Image_Path ip WHERE ip.product_id = p.product_id) AS first_image_path
                    FROM 
                        Category c 
                    LEFT JOIN 
                        Product p ON c.category_id = p.category_id 
                    LEFT JOIN 
                        Product_Variant pv ON p.product_id = pv.product_id
                    WHERE 
                        p.product_id = @product_id
                    GROUP BY 
                        c.category_id, c.category_name,
                        p.product_id, p.product_name, p.product_description, p.date_added,
                        pv.product_variant_id, pv.variant_price, pv.variant_name, pv.stock, pv.product_status";


            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@product_id", productID);

                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {

                        while ( reader.Read() )
                        {
                            productsList.Add(new productsList
                            {
                                CategoryName = reader.GetString(0),
                                ProductID = reader.GetString(1),
                                ProductName = reader.GetString(2),
                                DateAdded = reader.GetDateTime(3),
                                ProductVariantID = reader.GetString(4),
                                VariantPrice = reader.GetDecimal(5),
                                variantCount = reader.GetString(6),
                                Stock = reader.GetInt32(7),
                                ProductStatus = reader.GetString(8),
                                ProductImageUrl = reader.GetString(9)
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
                        pv.product_status,
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
                        pv.product_variant_id, pv.variant_price, pv.variant_name, pv.stock, pv.product_status";


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
                                DateAdded = reader.GetDateTime(3),
                                ProductVariantID = reader.GetString(4),
                                VariantPrice = reader.GetDecimal(5),
                                variantCount = reader.GetString(6),
                                Stock = reader.GetInt32(7),
                                ProductStatus = reader.GetString(8),
                                ProductImageUrl = reader.GetString(9)
                            });
                        }
                    }
                }
            }
            return productsList;
        }
        private int GetTotalProductsCount( )
        {
            string countSql = $@"SELECT COUNT(*)
                     FROM 
                        Category c 
                     LEFT JOIN 
                        Product p ON c.category_id = p.category_id
                     WHERE 
                        p.product_id IS NOT NULL";

            int totalCount = 0;
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(countSql, conn) )
                {
                    totalCount = (int)cmd.ExecuteScalar();
                }
            }
            return totalCount;
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
                Response.Redirect($"~/Admin/Product Management/editProduct.aspx?OrderID={encryptedStr}");
            }
            else if ( e.CommandName == "DeleteProduct" )
            {
            }
            else if ( e.CommandName == "viewItems" )
            {
                ViewState["SelectedProductId"] = e.CommandArgument.ToString();
                productListView.DataSource = getVariants(e.CommandArgument.ToString());
                productListView.DataBind();
            }
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
            int totalCategories = 100;

            if ( ( pageIndex + 1 ) * pageSize < totalCategories )
            {
                Debug.Write("BABAK1 " + pageIndex);
                ViewState["PageIndex"] = pageIndex + 1;
                BindListView((int)ViewState["PageIndex"], pageSize);
            }
        }
        //sorting by clicking the table label functions
        protected void productListView_Sorting( object sender, ListViewSortEventArgs e )
        {
            List<productsList> categories = getTotalProducts();
            string sortDirection = GetSortDirection(e.SortExpression);
            IEnumerable<productsList> sortedCategories;

            // Sorting the list using LINQ dynamically based on SortExpression and SortDirection
            if ( sortDirection == "ASC" )
            {
                sortedCategories = categories.OrderBy(x => GetPropertyValue(x, e.SortExpression));
            }
            else
            {
                sortedCategories = categories.OrderByDescending(x => GetPropertyValue(x, e.SortExpression));
            }


            productListView.DataSource = getProductData((int)ViewState["PageIndex"], pageSize);
            productListView.DataBind();
        }
        private object GetPropertyValue( object obj, string propName )
        {
            return obj.GetType().GetProperty(propName).GetValue(obj, null);
        }
        private string GetSortDirection( string column )
        {
            // By default, set the sort direction to ascending
            string sortDirection = "ASC";

            // Retrieve the last column that was sorted.
            string sortExpression = ViewState["SortExpression"] as string;

            Debug.Write("babakqweqwe" + sortExpression);
            if ( sortExpression != null && sortExpression == column )
            {
                // Check if the same column is being sorted.
                // Otherwise, the default value is returned.
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
            List<productsList> categoryData = getProductData(0, pageSize);
            List<productsList> filteredData = FilterCategoryList(categoryData, searchTerm);

            productListView.DataSource = filteredData;
            productListView.DataBind();
        }

        private List<productsList> FilterCategoryList( List<productsList> categories, string searchTerm )
        {
            string safeSearchTerm = searchTerm.Replace("'", "''");

            // Using LINQ to filter the list of categories by name
            var filteredCategories = categories.Where(c => c.CategoryName.Contains(safeSearchTerm)).ToList();
            return filteredCategories;
        }

        private DataTable FilterDataTable( DataTable dataTable, string searchTerm )
        {
            // Escape single quotes in the search term which can break the filter expression.
            string safeSearchTerm = searchTerm.Replace("'", "''");

            // Build a filter expression that checks if any of the columns contain the search term.
            string expression = string.Format(
                "Convert(OrderId, 'System.String') LIKE '%{0}%' OR " +
                "ProductName LIKE '%{0}%' OR " +
                "Convert(AdditionalProductsCount, 'System.String') LIKE '%{0}%' OR " +
                "Convert(Date, 'System.String') LIKE '%{0}%' OR " +
                "CustomerName LIKE '%{0}%' OR " +
                "Convert(Total, 'System.String') LIKE '%{0}%' OR " +
                "Convert(PaymentDate, 'System.String') LIKE '%{0}%' OR " +
                "Status LIKE '%{0}%'",
                safeSearchTerm);

            // Use the Select method to find all rows matching the filter expression.
            DataRow[] filteredRows = dataTable.Select(expression);

            // Create a new DataTable to hold the filtered rows.
            DataTable filteredDataTable = dataTable.Clone(); // Clone the structure of the table.

            // Import the filtered rows into the new DataTable.
            foreach ( DataRow row in filteredRows )
            {
                filteredDataTable.ImportRow(row);
            }

            return filteredDataTable;
        }
        protected void closePopUp_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
        }
        protected void btnCancelDelete_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
        }
    }
}