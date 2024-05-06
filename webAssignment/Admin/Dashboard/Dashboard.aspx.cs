using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.DataVisualization.Charting;
using System.Web.UI.WebControls;
using webAssignment.Admin.Orders;

namespace webAssignment
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                init();
            }

            DateTime? startDate = Session["StartDate"] as DateTime?;
            DateTime? endDate = Session["EndDate"] as DateTime?;
            if ( startDate != null && endDate != null )
            {
                init();
            }
        }

        private void init( )
        {
            itemSolded.Text = GetTotalNumberOfOrders().ToString() + " Items Solded Today";
            lblOrders.Text = GetTotalNumberOfOrders().ToString() + " Orders";
            subLblOrders.Text = GetTotalNumberOfOrders().ToString() + " Total Orders on Today";
            lblVisitCount.Text = getNumberOfVisitor().ToString() + " Users";
            lblWishlist.Text = GetTotalWishlistedItems().ToString();
            bestSellingItemLv.DataSource = GetBestSellingProductVariants();
            bestSellingItemLv.DataBind();
            chartBoxInit();

        }
        private void chartBoxInit( )
        {
            GetSalesJsonData();
            decimal salesData = ( getTotalSale() );

            if ( salesData > 0 && salesData != 0 )
            {
                chartSales.Text = "RM " + salesData.ToString();
                todaySales.Text = "RM " + salesData.ToString();
            }
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


        public int GetTotalNumberOfOrders( )
        {
            string dateFilter = "";
            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;

            // Check Session and assign dates if got
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
            dateFilter = " WHERE date_ordered >= @startDate AND date_ordered <= @endDate ";

            using ( SqlConnection con = new SqlConnection(connectionString) )
            {
                string sql = $@"
                        SELECT
                            COUNT(*) AS TotalOrders
                        FROM
                            [dbo].[Order]
                            {dateFilter}";

                using ( SqlCommand cmd = new SqlCommand(sql, con) )
                {
                    if ( sortStartDate.HasValue && sortEndDate.HasValue )
                    {
                        cmd.Parameters.AddWithValue("@startDate", sortStartDate.Value);
                        cmd.Parameters.AddWithValue("@endDate", sortEndDate.Value);
                    }
                    con.Open();
                    object result = cmd.ExecuteScalar(); 
                    if ( result != DBNull.Value && result != null )
                    {

                        return (int)result; 
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }


        private int getNumberOfVisitor( )
        {
            string dateFilter = "";
            DataTable dt = new DataTable();
            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;

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
            dateFilter = " VisitDate >= @startDate AND VisitDate <= @endDate ";


            using ( SqlConnection con = new SqlConnection(connectionString) )
            {
                string sql = $@"
                        SELECT
                            VisitCount
                        FROM
                            [dbo].[VisitorLog]
                        WHERE
                            {dateFilter}";

                using ( SqlCommand cmd = new SqlCommand(sql, con) )
                {
                    if ( sortStartDate.HasValue && sortEndDate.HasValue )
                    {
                        cmd.Parameters.AddWithValue("@startDate", sortStartDate.Value);
                        cmd.Parameters.AddWithValue("@endDate", sortEndDate.Value);
                    }
                    con.Open();
                    object result = cmd.ExecuteScalar(); 
                    if ( result != DBNull.Value && result != null )
                    {

                        return (int)result;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        public int GetTotalWishlistedItems( )
        {
            DataTable dt = new DataTable();
            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;

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

            using ( SqlConnection con = new SqlConnection(connectionString) )
            {
                string sql = @"
            SELECT COUNT(*) AS TotalWishlistedItems
            FROM wishlist_details
            WHERE date_added >= @startDate AND date_added <= @endDate";

                using ( SqlCommand cmd = new SqlCommand(sql, con) )
                {
                    cmd.Parameters.AddWithValue("@startDate", sortStartDate);
                    cmd.Parameters.AddWithValue("@endDate", sortEndDate);

                    con.Open();
                    object totalItems = cmd.ExecuteScalar();
                    if ( totalItems != DBNull.Value && totalItems != null )
                    {

                        return (int)totalItems;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        public decimal getTotalSale( )
        {
            string dateFilter = "";
            DataTable dt = new DataTable();
            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;

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
            dateFilter = "WHERE date_ordered >= @startDate AND date_ordered <= @endDate ";

            using ( SqlConnection con = new SqlConnection(connectionString) )
            {
                string sql = $@"
                        SELECT
                                SUM(total_price) AS TotalSales
                                FROM
                                [dbo].[Order]
                            {dateFilter}";

                using ( SqlCommand cmd = new SqlCommand(sql, con) )
                {
                    if ( sortStartDate != null && sortEndDate != null )
                    {
                        cmd.Parameters.AddWithValue("@startDate", sortStartDate);
                        cmd.Parameters.AddWithValue("@endDate", sortEndDate);
                    }
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if ( result != DBNull.Value && result != null )
                    {

                        return (decimal)result;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        // for chart
        public DataTable GetMonthlySalesData( )
        {
            string dateFilter = "";
            DataTable dt = new DataTable();
            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;

            if ( Session["StartDate"] != null && Session["EndDate"] != null )
            {
                sortStartDate = (DateTime)Session["StartDate"];
                sortEndDate = (DateTime)Session["EndDate"];
            lblDateRange.Text = ((DateTime)Session["StartDate"]).ToString("dd/MM/yyyy") + " - " + ( (DateTime)Session["EndDate"] ).ToString("dd/MM/yyyy");

            }
            else
            {
                sortStartDate = DateTime.Today;
                sortEndDate = DateTime.Today;
                lblDateRange.Text = "Today";
            }

            dateFilter = " AND date_ordered >= @startDate AND date_ordered <= @endDate ";

            using ( SqlConnection con = new SqlConnection(connectionString) )
            {
                string sql = $@"
                                SELECT
                                    CONVERT(VARCHAR(10), date_ordered, 120) AS Date,
                                    SUM(total_price) AS TotalSales
                                FROM
                                    [dbo].[Order]
                                WHERE
                                    status = 'Delivered'
                                    {dateFilter}
                                GROUP BY
                                    CONVERT(VARCHAR(10), date_ordered, 120)
                                ORDER BY
                                    Date";

                using ( SqlCommand cmd = new SqlCommand(sql, con) )
                {
                    if ( sortStartDate != null && sortEndDate != null )
                    {
                        cmd.Parameters.AddWithValue("@startDate", sortStartDate);
                        cmd.Parameters.AddWithValue("@endDate", sortEndDate);
                    }
                    con.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    adapter.Fill(dt);
                }
            }
            return dt;
        }
        public string GetSalesJsonData( DateTime? startDate = null, DateTime? endDate = null )
        {
            DataTable dt = GetMonthlySalesData();
            string json = JsonConvert.SerializeObject(dt, Formatting.None);
            return json;
        }



        public List<ProductVariantDetail> GetBestSellingProductVariants( )
        {
            DateTime? sortStartDate = null;
            DateTime? sortEndDate = null;

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

            List<ProductVariantDetail> productVariants = new List<ProductVariantDetail>();

            using ( SqlConnection con = new SqlConnection(connectionString) )
            {
                string sql = @"
                    SELECT TOP 4
                        pv.product_variant_id,
                        pv.variant_name,
                        pv.variant_price,
                        p.product_name,
                        SUM(od.quantity) AS TotalUnitsSold,
                        SUM(od.price * od.quantity) AS TotalRevenue,
                        ip.path AS ProductImagePath
                    FROM 
                        [dbo].[Order_details] od
                    JOIN 
                        [dbo].[Product_Variant] pv ON od.product_variant_id = pv.product_variant_id
                    JOIN 
                        [dbo].[Product] p ON pv.product_id = p.product_id
                    JOIN 
                        [dbo].[Order] o ON od.order_id = o.order_id
                    OUTER APPLY (
                        SELECT TOP 1 ip.path
                        FROM [dbo].[Image_Path] ip
                        WHERE ip.product_id = p.product_id
                        ORDER BY ip.image_path_id ASC
                    ) ip
                    WHERE  o.date_ordered BETWEEN @startDate AND @endDate AND o.status = 'Delivered'
                    GROUP BY 
                        pv.product_variant_id, pv.variant_name, p.product_name, pv.variant_price, ip.path
                    ORDER BY 
                        TotalRevenue DESC, TotalUnitsSold DESC
                    ";

                using ( SqlCommand cmd = new SqlCommand(sql, con) )
                {
                    cmd.Parameters.AddWithValue("@startDate", sortStartDate);
                    cmd.Parameters.AddWithValue("@endDate", sortEndDate);
                    con.Open();
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            productVariants.Add(new ProductVariantDetail
                            {
                                product_variant_id = reader.GetString(reader.GetOrdinal("product_variant_id")),
                                variant_name = reader.GetString(reader.GetOrdinal("variant_name")),
                                variant_price = reader.GetDecimal(reader.GetOrdinal("variant_price")),
                                product_name = reader.GetString(reader.GetOrdinal("product_name")),
                                TotalUnitsSold = reader.GetInt32(reader.GetOrdinal("TotalUnitsSold")),
                                TotalRevenue = reader.GetDecimal(reader.GetOrdinal("TotalRevenue")),
                                ProductImagePath = reader.IsDBNull(reader.GetOrdinal("ProductImagePath")) ? null : reader.GetString(reader.GetOrdinal("ProductImagePath"))
                            });
                        }
                    }
                }
            }

            return productVariants;
        }

        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }

    }
    public class ProductVariantDetail
    {
        public string product_variant_id { get; set; }
        public string variant_name { get; set; }
        public decimal variant_price { get; set; }
        public string product_name { get; set; }
        public int TotalUnitsSold { get; set; }
        public decimal TotalRevenue { get; set; }
        public string ProductImagePath { get; set; }
    }
}