using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
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
using webAssignment.Admin.Product_Management;
using webAssignment.Client.Profile;

namespace webAssignment.Admin.Orders
{
    public partial class EditOrder : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load( object sender, EventArgs e )
        {

            if ( !IsPostBack )
            {
                init();
            }
        }
        private void init() {
            string encorderId =Request.QueryString["orderId"];
            string encuserId = Request.QueryString["userID"];

            if ( string.IsNullOrEmpty(encuserId) || string.IsNullOrEmpty(encorderId) )
            {
                Response.Redirect("~/Admin/Orders/Order.aspx");
                return;
            }
            string orderId = DecryptString(Request.QueryString["orderId"]);
            string userId = DecryptString(Request.QueryString["userID"]);
            InitializeOrder(orderId);
            InitializeCustomer(userId);
            InitializeAddress(orderId);
            InitializeOrderList(orderId);
        }
        public orderInfo GetOrderDetails( string orderId )
        {
            orderInfo order = new orderInfo();
            string sql = $@"SELECT o.order_id, o.date_ordered ,o.status
                    FROM [dbo].[Order] o
                    LEFT JOIN Payment pm ON o.order_id = pm.order_id
                    WHERE o.order_id = @orderId";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        if ( reader.Read() ) // Ensure there is at least one row
                        {
                            order = new orderInfo
                            {
                                order_id = ( "Order ID : " + reader.GetString(0) ),
                                date_ordered = reader.GetDateTime(1),
                                status = reader.GetString(2)
                            };
                        }
                    }
                }
            }
            return order;
        }
        public userInfo GetCustomerDetails( string userId )
        {
            userInfo user = new userInfo();
            string sql = $@"SELECT username, email, phone_number
                    FROM [dbo].[User]
                    WHERE user_id = @userId";
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@userId", userId);
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        if ( reader.Read() ) // Ensure there is at least one row
                        {
                            user = ( new userInfo
                            {
                                username = reader.GetString(0),
                                email = reader.GetString(1),
                                phone_number = reader.GetString(2)
                            } );
                        }

                    }
                }
            }
            return user;
        }
        public List<ordersDetail> GetOrderItems( string orderId )
        {

            List<ordersDetail> items = new List<ordersDetail>();
            string sql = $@"SELECT pd.product_name, od.quantity,pv.variant_name, pv.variant_price , category_name , (pv.variant_price * od.quantity) as totalRowPrice ,(SELECT TOP 1 path FROM Image_Path img WHERE img.product_id = pd.product_id)
                    FROM Order_details od
                    JOIN Product_Variant pv ON od.product_variant_id = pv.product_variant_id
                    JOIN Product pd ON pv.product_id = pd.product_id
                    JOIN Category c ON pd.category_id = c.category_id
                    JOIN [dbo].[Order] o ON o.order_id = od.order_id
                    WHERE od.order_id = @orderId";
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            items.Add(new ordersDetail
                            {
                                product_name = reader.GetString(0),
                                quantity = reader.GetInt32(1),
                                variant_name = reader.GetString(2),
                                variant_price = reader.GetDecimal(3),
                                category_name = reader.GetString(4),
                                totalRowPrice = reader.GetDecimal(5),
                                ProductImageUrl = reader.GetString(6)
                            });
                        }
                    }
                }
            }
          
            return items;

        }
        private void InitializeOrder( string orderId )
        {
            var order = GetOrderDetails(orderId);
            lblorderId.Text = order.order_id;
            lblDateOrded.Text = order.date_ordered.ToString("dd/MM/yyyy");
            lblorderStatus.Text = order.status;
            currStatus.Text = order.status;
            lblorderStatus.CssClass = getColourStatus(order.status) + " p-3 rounded-lg";
            currStatus.CssClass = getColourStatus(order.status) + " p-3 rounded-lg";
        }

        private void InitializeCustomer( string userId )
        {
            var user = GetCustomerDetails(userId);
            customerName.Text = user.username;
            cusEmail.Text = user.email;
            cusPhoneNum.Text = user.phone_number;
        }

        private void InitializeAddress( string orderId )
        {
            string query =
                @"SELECT  
                    ad.address_line1,  
                    ad.address_line2, 
                    CONCAT(ad.zip_code, ', ', ad.city) AS address_line3,
                    CONCAT(ad.state, ', ', ad.countryCode) AS address_line4
                FROM 
                    dbo.[Address] ad 
                INNER JOIN 
                    dbo.[Order] o ON ad.address_id = o.address_id 
                WHERE 
                    o.order_id = @orderID";

            using ( SqlConnection connection = new SqlConnection(connectionString) )
            {
                try
                {
                    connection.Open();
                    using ( SqlCommand command = new SqlCommand(query, connection) )
                    {
                        command.Parameters.AddWithValue("@orderID", orderId);
                        using ( SqlDataReader reader = command.ExecuteReader() )
                        {
                            if ( reader.HasRows )
                            {
                                reader.Read();
                                shippingAddresslbl.Text = reader["address_line1"].ToString() + " " + reader["address_line2"].ToString() + " " + reader["address_line3"].ToString() + " " + reader["address_line4"].ToString();

                            }
                        }
                    }
                }
                catch ( SqlException ex )
                {
                    ShowNotification(ex.Message, "warning");
                }
            }
        }

        private void InitializeOrderList( string orderId )
        {
            var items = GetOrderItems(orderId);
            ordersListView.DataSource = items;
            ordersListView.DataBind();
        }

        protected string FormatPaymentDetails( object paymentDetails )
        {
            string json = Convert.ToString(paymentDetails);
            try
            {
                JObject paymentJson = JObject.Parse(json);
                string paymentMethod = paymentJson["card_number"] != null ? "Card" : ( paymentJson["bank_number"] != null ? "Bank" : ( paymentJson["COD"] != null ? "COD" : "Other" ) );

                string details = "";
                if ( paymentMethod == "Card" )
                {
                    details += "Card Number: " + paymentJson["card_number"];
                }
                else if ( paymentMethod == "Bank" )
                {
                    details += $"Bank ({paymentJson["bank_name"]}): " + paymentJson["bank_number"];
                }
                else if ( paymentMethod == "COD" )
                {
                    details += "COD";
                }

                return details;
            }
            catch ( JsonException ex )
            {
                return "Invalid payment details";
            }
        }


        // Decryption
        protected string DecryptString( string cipherText )
        {
            string EncryptionKey = "ABC123";
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
        public OrderCalculationResults CalculateOrderDetails( string orderId )
        {
            var results = new OrderCalculationResults
            {
                Subtotal = 0m,
                ShippingRate = 5.00m,  
                SSTRate = 0.06m,        
                taxAmount = 0.00m,        
                Items = new List<ordersDetail>()
            };

            string sql = $@"SELECT pd.product_name, od.quantity, pv.variant_name, pv.variant_price, 
                    category_name, (pv.variant_price * od.quantity) as totalRowPrice, 
                    (SELECT TOP 1 path FROM Image_Path img WHERE img.product_id = pd.product_id),
                    v.discount_rate
                    FROM Order_details od
                    JOIN Product_Variant pv ON od.product_variant_id = pv.product_variant_id
                    JOIN Product pd ON pv.product_id = pd.product_id
                    JOIN Category c ON pd.category_id = c.category_id
                    JOIN [dbo].[Order] o ON o.order_id = od.order_id
                    LEFT JOIN Voucher v ON o.voucher_id = v.voucher_id
                    WHERE od.order_id = @orderId";
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            var itemTotal = reader.GetDecimal(5);
                            results.Subtotal += itemTotal;
                            results.Items.Add(new ordersDetail
                            {
                                product_name = reader.GetString(0),
                                quantity = reader.GetInt32(1),
                                variant_name = reader.GetString(2),
                                variant_price = reader.GetDecimal(3),
                                category_name = reader.GetString(4),
                                totalRowPrice = itemTotal,
                                ProductImageUrl = reader.GetString(6)
                                
                            });
                            if ( !reader.IsDBNull(7) )
                            {
                                results.DiscountRate = (float)reader.GetDouble(7);
                            }
                            else
                            {
                                results.DiscountRate = 0.0f; // or some default value
                            }
                        }
                    }
                }
            }
            if ( results.DiscountRate > 0 )
            {
                results.Discount = results.Subtotal * (decimal)results.DiscountRate;
            }
            results.taxAmount =results.Subtotal * results.SSTRate;
            results.Total = results.Subtotal - results.Discount + results.ShippingRate + results.taxAmount;
            return results;
        }
        protected void ordersListView_DataBound( object sender, EventArgs e )
        {
            string orderId = DecryptString(Request.QueryString["orderId"]);
            var orderDetails = CalculateOrderDetails(orderId);
            Label lblSubtotal = ordersListView.FindControl("lblSubtotal") as Label;
            Label lblShippingRate = ordersListView.FindControl("lblShippingRate") as Label;
            Label lblTax = ordersListView.FindControl("lblTax") as Label;
            Label lblTotal = ordersListView.FindControl("lblTotal") as Label;
            Label lblDiscount = ordersListView.FindControl("lblDiscount") as Label;
            Label lblDisRate = ordersListView.FindControl("lblDisRate") as Label;

            
            lblSubtotal.Text = $"RM{orderDetails.Subtotal.ToString("0.00")}";
            lblShippingRate.Text = $"RM{orderDetails.ShippingRate.ToString("0.00")}";
            lblTax.Text = $"RM{orderDetails.taxAmount.ToString("0.00")}";
            lblTotal.Text = $"RM{orderDetails.Total.ToString("0.00")}";
            lblDiscount.Text = $"RM{orderDetails.Discount.ToString("0.00")}";
            lblDisRate.Text = $"{orderDetails.DiscountRate + "%"}";
        }

        protected void editStatus_Click( object sender, EventArgs e )
        {
            popUpPanel.Style.Add("display", "flex");
        }

        protected void cancelChange_Click( object sender, EventArgs e )
        {
            popUpPanel.Style.Add("display", "none");
        }

        protected void changeStatus_Click( object sender, EventArgs e )
        {
            string orderId = DecryptString(Request.QueryString["orderId"]);
            string newStatus = statusDDl.SelectedValue; 
            if ( lblpaymentMethod.Text != statusDDl.SelectedValue.ToString())
            {
                UpdateOrderStatus(orderId, newStatus);
            }
        }
        private void UpdateOrderStatus( string orderId, string newStatus )
        {
            string sql = @"UPDATE [dbo].[Order]
                   SET status = @newStatus
                   WHERE order_id = @orderID";

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@orderID", orderId);
                    cmd.Parameters.AddWithValue("@newStatus", newStatus);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    if ( rowsAffected > 0 )
                    {
                        init();
                        ShowNotification("Successfully Updated the status!" , "success");
                    }
                    else
                    {
                        ShowNotification("status is not updated" , "warning");
                    }
                }
            }
        }

        private string getColourStatus( string status )
        {
            return status == "Pending" ? "bg-gray-200"
                                : status == "On The Road" ? "bg-blue-200"
                                : status == "Packed" ? "bg-yellow-200"
                                : status == "Delivered" ? "bg-green-200"
                                : status == "Cancelled" ? "bg-red-200" : "bg-white-200";
        }

        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }
       
    }
    public class OrderCalculationResults
    {
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public float DiscountRate { get; set; } 
        public decimal ShippingRate { get; set; }
        public decimal SSTRate { get; set; }
        public decimal taxAmount { get; set; }
        public decimal Total { get; set; }
        public List<ordersDetail> Items { get; set; }
    }
}