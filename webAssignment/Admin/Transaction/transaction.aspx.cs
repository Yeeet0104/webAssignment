using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using webAssignment.Admin.Orders;

namespace webAssignment.Admin.Transaction
{
    public partial class transaction : System.Web.UI.Page , IFilterable
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
        private void BindListView( int pageIndex, int pageSize)
        {
            transactionListView.DataSource = transactionData(pageIndex, pageSize);
            transactionListView.DataBind();
        }

        // when on bound handle events
        protected void transactionListView_DataBound( object sender, EventArgs e )
        {
            Label pageNumFoot = transactionListView.FindControl("pageNumFoot") as Label;
            Label lblCurrPagination = transactionListView.FindControl("lblCurrPagination") as Label;

            if ( pageNumFoot != null )
            {
                int totalItems = getTotalTransactionCount();
                int currentPageIndex = ( (int)ViewState["PageIndex"] );
                int startRecord = ( currentPageIndex * pageSize ) + 1;
                int endRecord = ( currentPageIndex + 1 ) * pageSize;
                endRecord = ( endRecord > totalItems ) ? totalItems : endRecord;

                lblCurrPagination.Text = ( currentPageIndex + 1 ).ToString();
                pageNumFoot.Text = $"Showing {startRecord}-{endRecord} from {totalItems}";
            }

        }


        private List<transactionClass> transactionData( int pageIndex, int pageSize )
        {
            List<transactionClass> transactionData = new List<transactionClass>();

            string sortExpression = ViewState["SortExpression"] as string ?? "pay.date_paid";
            string sortDirection = ViewState["SortDirection"] as string ?? "DESC";

            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;

            List<string> conditions = new List<string>();
            // Check Session and assign dates if available
            if ( Session["StartDate"] != null && Session["EndDate"] != null )
            {
                sortStartDate = (DateTime)Session["StartDate"];
                sortEndDate = (DateTime)Session["EndDate"];
            }
            else
            {
                sortStartDate = DateTime.Today;
                sortEndDate = DateTime.Today;
            }
            if ( sortStartDate.HasValue && sortEndDate.HasValue )
            {
                conditions.Add("pay.date_paid >= @startDate AND pay.date_paid <= @endDate");
            }
            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";


            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                string sql = $@"SELECT 
                                o.order_id,
                                o.user_id, 
                                u.username, 
                                o.total_price,
                                pay.payment_details,
                                pay.date_paid,
                                STRING_AGG(p.product_name + ' - ' + CAST(pv.variant_name AS NVARCHAR(100)) + ' - ' + 'RM' + CAST(pv.variant_price AS NVARCHAR(100)), '; ') AS product_details
                            FROM 
                                [dbo].[Order] o
                                JOIN [dbo].[User] u ON o.user_id = u.user_id
                                JOIN Order_details od ON o.order_id = od.order_id
                                JOIN Product_Variant pv ON od.product_variant_id = pv.product_variant_id
                                JOIN Product p ON pv.product_id = p.product_id
                                JOIN Payment pay ON o.order_id = pay.order_id
                            {whereClause}
                            GROUP BY 
                                o.order_id, o.user_id, u.username, o.total_price, pay.payment_details, pay.date_paid
                ORDER BY {sortExpression} {sortDirection}
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

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
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {

                        while ( reader.Read() )
                        {
                            transactionData.Add(new transactionClass
                            {
                                order_id = reader.GetString(0),
                                user_id = reader.GetString(1),
                                username = reader.GetString(2),
                                total_price = reader.GetDecimal(3),
                                payment_details = reader.GetString(4),
                                date_paid = reader.GetDateTime(5),
                                product_details = reader.GetString(6)
                            });
                        }
                    }
                }
            }
            return transactionData;
        }
        private List<transactionClass> getAlltransactionData()
        {
            List<transactionClass> transactionData = new List<transactionClass>();
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                string sql = $@"SELECT 
                                o.order_id,
                                o.user_id, 
                                u.username, 
                                o.total_price,
                                pay.payment_details,
                                pay.date_paid,
                                STRING_AGG(p.product_name + ' - ' + CAST(pv.variant_name AS NVARCHAR(100)) + ' - ' + 'RM' + CAST(pv.variant_price AS NVARCHAR(100)), '; ') AS product_details
                            FROM 
                                [dbo].[Order] o
                                JOIN [dbo].[User] u ON o.user_id = u.user_id
                                JOIN Order_details od ON o.order_id = od.order_id
                                JOIN Product_Variant pv ON od.product_variant_id = pv.product_variant_id
                                JOIN Product p ON pv.product_id = p.product_id
                                JOIN Payment pay ON o.order_id = pay.order_id
                            GROUP BY 
                                o.order_id, o.user_id, u.username, o.total_price, pay.payment_details, pay.date_paid
                            ORDER BY 
                                o.order_id;";
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {

                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            transactionData.Add(new transactionClass
                            {
                                order_id = reader.GetString(0),
                                user_id = reader.GetString(1),
                                username = reader.GetString(2),
                                total_price = reader.GetDecimal(3),
                                payment_details = reader.GetString(4),
                                date_paid = reader.GetDateTime(5),
                                product_details = reader.GetString(6)
                            });
                        }
                    }
                }
            }
            return transactionData;
        }
        private int getTotalTransactionCount( )
        {

            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;

            List<string> conditions = new List<string>();
            // Check Session and assign dates if available
            if ( Session["StartDate"] != null && Session["EndDate"] != null )
            {
                sortStartDate = (DateTime)Session["StartDate"];
                sortEndDate = (DateTime)Session["EndDate"];
            }
            else
            {
                sortStartDate = DateTime.Today;
                sortEndDate = DateTime.Today;
            }
            if ( sortStartDate.HasValue && sortEndDate.HasValue )
            {
                conditions.Add("pay.date_paid >= @startDate AND pay.date_paid <= @endDate");
            }
            string whereClause = conditions.Count > 0 ? "WHERE " + string.Join(" AND ", conditions) : "";

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                string sql = $@"
                                SELECT COUNT(DISTINCT o.order_id) AS totalTransactionCount
                                FROM [dbo].[Order] o
                                JOIN [dbo].[User] u ON o.user_id = u.user_id
                                JOIN Order_details od ON o.order_id = od.order_id
                                JOIN Product_Variant pv ON od.product_variant_id = pv.product_variant_id
                                JOIN Product p ON pv.product_id = p.product_id
                                JOIN Payment pay ON o.order_id = pay.order_id
                                {whereClause}       

                    ;";

                connection.Open();
                using ( SqlCommand command = new SqlCommand(sql, connection) )
                {
                    if ( sortStartDate.HasValue && sortEndDate.HasValue )
                    {
                        command.Parameters.AddWithValue("@startDate", sortStartDate.Value);
                        command.Parameters.AddWithValue("@endDate", sortEndDate.Value);
                    }
                    return (int)command.ExecuteScalar();
                }
            }
        }
        protected string FormatPaymentDetails( object paymentDetails )
        {
            string json = Convert.ToString(paymentDetails);
            try
            {
                JObject paymentJson = JObject.Parse(json);
                string paymentMethod = paymentJson["card_number"] != null ? "Card" : ( paymentJson["bank_number"] != null ? "Bank" : "Other" );

                string details = "";
                if ( paymentMethod == "Card" )
                {
                    details += "Card Number: " + paymentJson["card_number"];
                }
                else if ( paymentMethod == "Bank" )
                {
                    details += $"Bank ({paymentJson["bank_name"]}): " + paymentJson["bank_number"];
                }

                return details;
            }
            catch ( JsonException ex )
            {
                return "Invalid payment details";
            }
        }
        protected string FormatProducts( object productDetails )
        {
            var details = productDetails.ToString();
            var products = details.Split(';');
            var formattedHtml = new StringBuilder();
            foreach ( var product in products )
            {
                formattedHtml.Append("<div>" + product + "</div>");
            }
            return formattedHtml.ToString();
        }
        protected List<string> ParseVariants( object variantsData )
        {
            var variants = variantsData.ToString();
            return variants.Split(',').ToList();
        }
        protected void transactionListView_ItemCommand( object sender, ListViewCommandEventArgs e )
        {
            if ( e.CommandName == "deleteTransaction" )
            {
                // Show the popup
                popUpDelete.Style.Add("display", "flex");

                string commandArgument = e.CommandArgument.ToString();
                string[] arguments = commandArgument.Split(',');
                if ( arguments.Length == 2 )
                {
                    lbltransIDInfo.Text = arguments[0];
                    lblcusIDInfo.Text = arguments[1];
                    // Now, you can use transactionID and userName as needed
                }

                // Set the Order ID in the label within the popup
            }
        }
        //sorting by clicking the table label functions
        protected void transactionListView_Sorting( object sender, ListViewSortEventArgs e )
        {
            List<transactionClass> voucherList = getAlltransactionData();
            string sortDirection = GetSortDirection(e.SortExpression);
            IEnumerable<transactionClass> sortVoucher;

            // Sorting the list using LINQ dynamically based on SortExpression and SortDirection
            if ( sortDirection == "ASC" )
            {
                sortVoucher = voucherList.OrderBy(x => GetPropertyValue(x, e.SortExpression));
            }
            else
            {
                sortVoucher = voucherList.OrderByDescending(x => GetPropertyValue(x, e.SortExpression));
            }
            transactionListView.DataSource = transactionData((int)ViewState["PageIndex"], pageSize);
            transactionListView.DataBind();
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
        protected void closePopUp_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
        }
        protected void btnCancelDelete_Click( object sender, EventArgs e )
        {
            popUpDelete.Style.Add("display", "none");
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
            int totalVoucher = getTotalTransactionCount();

            if ( ( pageIndex + 1 ) * pageSize < totalVoucher )
            {
                pageIndex++;
                ViewState["PageIndex"] = pageIndex;
                BindListView((int)ViewState["PageIndex"], pageSize);
            }
        }
        public void FilterListView( string searchTerm )
        {
            List<transactionClass> transData = transactionData(0, pageSize);
            List<transactionClass> filteredData = FilterTransList(transData, searchTerm);
            transactionListView.DataSource = filteredData;
            transactionListView.DataBind();
        }

        // Lists and LINQ
        private List<transactionClass> FilterTransList( List<transactionClass> trans, string searchTerm )
        {
            searchTerm = searchTerm?.ToLower() ?? string.Empty; // Handle null search term and convert to lowercase

            return trans.Where(t =>
                t.order_id.ToLower().Contains(searchTerm) ||
                t.user_id.ToLower().Contains(searchTerm) ||
                t.username.ToLower().Contains(searchTerm) ||
                t.total_price.ToString().Contains(searchTerm) ||
                t.payment_details.ToLower().Contains(searchTerm) ||
                ( t.date_paid.ToString("dd/MM/yyyy").Contains(searchTerm) ) ||
                t.product_details.ToLower().Contains(searchTerm) 
            ).ToList();
        }
    }
}