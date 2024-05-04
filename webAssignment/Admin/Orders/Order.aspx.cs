using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
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
using System.Web.UI;
using System.Web.UI.WebControls;
using webAssignment.Admin.Product_Management;

namespace webAssignment.Admin.Orders
{
    public partial class Order : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        private int pageSize = 5;
        protected void Page_Load( object sender, EventArgs e )
        {

            if ( !IsPostBack )
            {
                ViewState["PageIndex"] = 0;
                ViewState["FilterStatus"] = "";
                ViewState["SortDirection"] = "ASC";
                ViewState["cusID"] = "";
                BindListView(0, pageSize, "");
            }
        }


        protected void OrdersListView_ItemCommand( object sender, ListViewCommandEventArgs e )
        {
            if ( e.CommandName == "EditOrder" )
            {
                string orderId = e.CommandArgument.ToString();
                string encryptedStr = EncryptString(orderId);
                string userID = getCusID(orderId);
                string encryptedUserStr = EncryptString(userID);
                Response.Redirect($"~/Admin/orders/EditOrder.aspx?OrderID={encryptedStr}&userID={encryptedUserStr}");
            }
            else if ( e.CommandName == "DeleteOrder" )
            {
                // Show the popup
                popUpDelete.Style.Add("display", "flex");

                // Set the Order ID in the label within the popup
                lblItemInfo.Text = e.CommandArgument.ToString();
            }
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

        private void BindListView( int pageIndex, int pageSize, string status )
        {
            ordersListView.DataSource = getOrders(pageIndex, pageSize, status ?? ViewState["FilterStatus"] as string);
            ordersListView.DataBind();
        }

        public List<orders> getOrders( int pageIndex, int pageSize, string status )
        {
            List<orders> orders = new List<orders>();
            string sortExpression = ViewState["SortExpression"] as string ?? "o.order_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";

            string sql = $@"SELECT 
                            o.order_id, 
                            MIN(i.path) AS ProductImageUrl, 
                            MIN(p.product_name) AS ProductName, 
                            COUNT(DISTINCT od.product_variant_id)-1  AS AdditionalProductsCount,
                            o.date_ordered AS OrderDate,
                            MIN(u.first_name + ' ' + u.last_name) AS CustomerName,
                            o.total_price AS Total,
                            MAX(pm.date_paid) AS PaymentDate,
                            o.status AS Status
                        FROM 
                            [dbo].[Order] o
                            INNER JOIN Order_details od ON o.order_id = od.order_id
                            INNER JOIN Product_Variant pv ON od.product_variant_id = pv.product_variant_id
                            INNER JOIN Product p ON pv.product_id = p.product_id
                            INNER JOIN Image_Path i ON p.product_id = i.product_id
                            INNER JOIN [dbo].[User] u ON o.user_id = u.user_id
                            LEFT JOIN Payment pm ON o.order_id = pm.order_id
                        {( string.IsNullOrEmpty(status) ? "" : "WHERE o.status = @status" )}
                        GROUP BY o.order_id, o.date_ordered, o.total_price, o.status, o.user_id , date_paid
                        ORDER BY {sortExpression} {sortDirection}
                        OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {

                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@Offset", pageIndex * pageSize);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);
                    if ( !string.IsNullOrEmpty(status) )
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                    }

                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            orders.Add(new orders
                            {
                                OrderId = reader["order_id"].ToString(),
                                ProductImageUrl = reader["ProductImageUrl"].ToString(),
                                ProductName = reader["ProductName"].ToString(),
                                AdditionalProductsCount = Convert.ToInt32(reader["AdditionalProductsCount"]),
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                CustomerName = reader["CustomerName"].ToString(),
                                Total = Convert.ToDecimal(reader["Total"]),
                                PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                                Status = reader["Status"].ToString()
                            });
                        }
                    }
                }
            }
            return orders;
        }
        protected void ordersListView_DataBound( object sender, EventArgs e )
        {
            Label pageNumFoot = ordersListView.FindControl("pageNumFoot") as Label;
            Label lblCurrPagination = ordersListView.FindControl("lblCurrPagination") as Label;

            if ( pageNumFoot != null )
            {
                int totalItems = getAllOrdersCount(ViewState["FilterStatus"].ToString());
                int currentPageIndex = ( (int)ViewState["PageIndex"] );
                int startRecord = ( currentPageIndex * pageSize ) + 1;
                int endRecord = ( currentPageIndex + 1 ) * pageSize;
                endRecord = ( endRecord > totalItems ) ? totalItems : endRecord;

                lblCurrPagination.Text = ( currentPageIndex + 1 ).ToString();
                pageNumFoot.Text = $"Showing {startRecord}-{endRecord} from {totalItems}";
            }

        }
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
            int totalProducts = getAllOrdersCount(ViewState["FilterStatus"].ToString());

            if ( ( pageIndex + 1 ) * pageSize < totalProducts )
            {
                pageIndex++;
                ViewState["PageIndex"] = pageIndex;
                BindListView((int)ViewState["PageIndex"], pageSize, ViewState["FilterStatus"].ToString());
            }
        }

        public List<orders> getAllOrders( )
        {
            List<orders> orders = new List<orders>();
            string sortExpression = ViewState["SortExpression"] as string ?? "o.order_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";

            string sql = $@"SELECT 
                            o.order_id, 
                            MIN(i.path) AS ProductImageUrl, 
                            MIN(p.product_name) AS ProductName, 
                            COUNT(DISTINCT od.product_variant_id)-1  AS AdditionalProductsCount,
                            o.date_ordered AS OrderDate,
                            MIN(u.first_name + ' ' + u.last_name) AS CustomerName,
                            o.total_price AS Total,
                            MAX(pm.date_paid) AS PaymentDate,
                            o.status AS Status
                        FROM 
                            [dbo].[Order] o
                            INNER JOIN Order_details od ON o.order_id = od.order_id
                            INNER JOIN Product_Variant pv ON od.product_variant_id = pv.product_variant_id
                            INNER JOIN Product p ON pv.product_id = p.product_id
                            INNER JOIN Image_Path i ON p.product_id = i.product_id
                            INNER JOIN [dbo].[User] u ON o.user_id = u.user_id
                            LEFT JOIN Payment pm ON o.order_id = pm.order_id
                        GROUP BY o.order_id, o.date_ordered, o.total_price, o.status, o.user_id";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {

                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {

                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            orders.Add(new orders
                            {
                                OrderId = reader["order_id"].ToString(),
                                ProductImageUrl = reader["ProductImageUrl"].ToString(),
                                ProductName = reader["ProductName"].ToString(),
                                AdditionalProductsCount = Convert.ToInt32(reader["AdditionalProductsCount"]),
                                OrderDate = Convert.ToDateTime(reader["OrderDate"]),
                                CustomerName = reader["CustomerName"].ToString(),
                                Total = Convert.ToDecimal(reader["Total"]),
                                PaymentDate = Convert.ToDateTime(reader["PaymentDate"]),
                                Status = reader["Status"].ToString()
                            });
                        }
                    }
                }
            }
            return orders;
        }


        //sorting by clicking the table label functions
        protected void ordersListView_Sorting( object sender, ListViewSortEventArgs e )
        {
            GetSortDirection(e.SortExpression);
            ordersListView.DataSource = getOrders((int)ViewState["PageIndex"], pageSize, ViewState["FilterStatus"].ToString());
            ordersListView.DataBind();
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

        public int getAllOrdersCount( string status )
        {
            List<orders> orders = new List<orders>();
            string sortExpression = ViewState["SortExpression"] as string ?? "o.order_id";
            string sortDirection = ViewState["SortDirection"] as string ?? "ASC";
            var total = 0;
            string sql = $@"SELECT COUNT(DISTINCT o.order_id) AS TotalCount
                         FROM 
                            [dbo].[Order] o
                            INNER JOIN Order_details od ON o.order_id = od.order_id
                            INNER JOIN Product_Variant pv ON od.product_variant_id = pv.product_variant_id
                            {( string.IsNullOrEmpty(status) ? "" : "WHERE o.status = @status" )}
";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {

                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    if ( !string.IsNullOrEmpty(status) )
                    {
                        cmd.Parameters.AddWithValue("@status", status);
                    }
                    total = Convert.ToInt32(cmd.ExecuteScalar());
                }
                return total;
            }
        }
        public string getCusID( string order_id )
        {
            string sql = $@"SELECT user_id
                            FROM [dbo].[Order]
                            WHERE order_id = @orderID";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {

                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {

                    cmd.Parameters.AddWithValue("@orderID", order_id);

                    var result = cmd.ExecuteScalar(); 
                    if ( result != null ) 
                    {
                        return result.ToString();
                    }
                    else
                    {
                        return null; 
                    }
                }
            }
        }
        protected void closePopUp_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
        }
        protected void btnCancelDelete_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
        }

        protected void allStatusFilter_click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "");
            changeSelectedtabCss("");
        }

        protected void pendingFilter_click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "Pending";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Pending");
            changeSelectedtabCss("");
        }

        protected void OnTheRoadFilter_click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "On The Road";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "On The Road");
            changeSelectedtabCss("");
        }

        protected void deliveredFilter_click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "Delivered";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Delivered");
            changeSelectedtabCss("");
        }

        protected void cancelFilter_click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "Cancelled";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Cancelled");
            changeSelectedtabCss("");
        }

        protected void packedFilter_click( object sender, EventArgs e )
        {
            ViewState["FilterStatus"] = "Packed";
            ViewState["PageIndex"] = 0;
            BindListView(0, pageSize, "Packed");
            changeSelectedtabCss("");
        }

        private void changeSelectedtabCss( string tabName )
        {
            resetfilterTabSttyle();
            switch ( tabName )
            {
                case "Pending":
                    pendingFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                case "On The Road":
                    otr.CssClass += " text-blue-600 bg-gray-100";
                    break;
                case "Packed":
                    packedFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                case "Delivered":
                    deliveredFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                case "Cancel":
                    cancelFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;
                default:
                    allStatusFilter.CssClass += " text-blue-600 bg-gray-100";
                    break;

            }

        }

        private void resetfilterTabSttyle( )
        {
            RemoveCssClass(pendingFilter, "text-blue-600");
            RemoveCssClass(pendingFilter, "bg-gray-100");
            RemoveCssClass(otr, "text-blue-600");
            RemoveCssClass(otr, "bg-gray-100");
            RemoveCssClass(packedFilter, "text-blue-600");
            RemoveCssClass(packedFilter, "bg-gray-100");
            RemoveCssClass(deliveredFilter, "text-blue-600");
            RemoveCssClass(deliveredFilter, "bg-gray-100");
            RemoveCssClass(cancelFilter, "text-blue-600");
            RemoveCssClass(cancelFilter, "bg-gray-100");
            RemoveCssClass(allStatusFilter, "text-blue-600");
            RemoveCssClass(allStatusFilter, "bg-gray-100");
        }
        private void RemoveCssClass( WebControl control, string classToRemove )
        {
            List<string> classes = control.CssClass.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            // Remove the specific class
            classes.Remove(classToRemove);

            control.CssClass = String.Join(" ", classes);
        }


    }
}