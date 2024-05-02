using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Spreadsheet;
using Irony.Parsing;

namespace webAssignment.Admin.Product_Management
{
    public partial class productVariant : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private int pageSize = 5;

        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                ViewState["PageIndex"] = 0;
                ViewState["productName"] = "";

                init();
            }
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

            Label lblHeaderProductName = productListView.FindControl("lblHeaderProductName") as Label;
            if ( lblHeaderProductName != null )
            {
                lblHeaderProductName.Text = getName();
            }

        }
        private string getName( )
        {
            string prodIenc = Request.QueryString["productID"];
            string prodIDDec = DecryptString(prodIenc);
            var products = getVariants(prodIDDec, 0, pageSize);

            ViewState["productName"] = products[0].ProductName;

            return products[0].ProductName;
        }
        private List<productsList> getVariants( string productID, int pageIndex, int pageSize )
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
                        pv.variant_status,
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
                        pv.product_variant_id, pv.variant_price, pv.variant_name, pv.stock, pv.variant_status
                    ORDER BY {sortExpression} {sortDirection}
                    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";


            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@product_id", productID);
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
        private List<productsList> getTotalVariantProducts( string productID )
        {
            var productsList = new List<productsList>();
            string filter = ViewState["Filter"] as string;
            string sortExpression = ViewState["SortExpression"] as string ?? "product_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";
            string sql = $@"SELECT 
                        Count(p.product_id) as productCount
                    FROM 
                        Category c 
                    LEFT JOIN 
                        Product p ON c.category_id = p.category_id 
                    LEFT JOIN 
                        Product_Variant pv ON p.product_id = pv.product_id
                    WHERE 
                        p.product_id = @p.product_id
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

        // for paginations
        protected void prevPage_Click( object sender, EventArgs e )
        {
            int pageIndex = (int)ViewState["PageIndex"];

            if ( pageIndex > 0 )
            {
                ViewState["PageIndex"] = pageIndex - 1;
                getVariants(Session["productID"].ToString(), (int)ViewState["PageIndex"], pageSize);
            }
        }
        protected void backPage_Click( object sender, EventArgs e )
        {
            int pageIndex = (int)ViewState["PageIndex"];

            if ( pageIndex > 0 )
            {
                ViewState["PageIndex"] = pageIndex - 1;
                getVariants(Session["productID"].ToString(), (int)ViewState["PageIndex"], pageSize);
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
                getVariants(Session["productID"].ToString(), (int)ViewState["PageIndex"], pageSize);
            }
        }
        //sorting by clicking the table label functions
        protected void productListView_Sorting( object sender, ListViewSortEventArgs e )
        {
            List<productsList> categories = getTotalVariantProducts(Session["productID"].ToString());
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


            productListView.DataSource = getVariants(Session["productID"].ToString(), (int)ViewState["PageIndex"], pageSize);
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
            List<productsList> categoryData = getVariants(Session["productID"].ToString(), (int)ViewState["PageIndex"], pageSize);
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

        protected string DecryptString( string cipherText )
        {
            string EncryptionKey = "ABC123"; // Use the same key you used during encryption
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using ( Aes encryptor = Aes.Create() )
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using ( MemoryStream ms = new MemoryStream() )
                {
                    using ( CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write) )
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
        private void init( )
        {
            string prodIenc = Request.QueryString["productID"];
            string prodIDDec = DecryptString(prodIenc);
            Session["productID"] = prodIDDec;
            var products = getVariants(prodIDDec, 0, pageSize);
            productListView.DataSource = products;
            productListView.DataBind();

            ViewState["productName"] = products[0].ProductName;
            Debug.WriteLine("CINai:" + ViewState["productName"]);
            Debug.WriteLine("CINai2:" + products[0].ProductName);
        }

        protected void editProduct_Click( object sender, EventArgs e )
        {

        }

        protected void backBtn_Click( object sender, EventArgs e )
        {
            Response.Redirect($"~/Admin/Product Management/adminProducts.aspx");
        }
    }
}