﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Profile
{
    public partial class OrderHistoryDetailsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                init();
            }
        }

        private void init()
        {
            string encOrderID = Request.QueryString["OrderID"];
            string orderID = DecryptString(encOrderID);


            retrieveOrderDetails(orderID);
            updateOrderStatusBar(getOrderStatus(orderID));
            updateOrderInfo(orderID);
            updatePaymentInfo(orderID);
            updateAddressInfo(orderID);
        }
        private void updateOrderStatusBar(string status)
        {
                   
            switch (status.ToUpper())
            {
                case "PENDING":
                    checkPending.Style["display"] = "block";
                    pending.Style["background-color"] = "blue";
                    cancelBox.Style["display"] = "block";
                    break;
                case "PACKED":
                    checkPending.Style["display"] = "block";
                    pending.Style["background-color"] = "blue";
                    line1.Style["background-color"] = "blue";
                    checkPacked.Style["display"] = "block";
                    packed.Style["background-color"] = "blue";
                    break;
                case "ON THE ROAD":
                    checkPending.Style["display"] = "block";
                    pending.Style["background-color"] = "blue";
                    line1.Style["background-color"] = "blue";
                    checkPacked.Style["display"] = "block";
                    packed.Style["background-color"] = "blue";
                    line2.Style["background-color"] = "blue";
                    checkOTR.Style["display"] = "block";
                    onTheRoad.Style["background-color"] = "blue";
                    break;
                case "DELIVERED":
                    checkPending.Style["display"] = "block";
                    pending.Style["background-color"] = "blue";
                    line1.Style["background-color"] = "blue";
                    checkPacked.Style["display"] = "block";
                    packed.Style["background-color"] = "blue";
                    line2.Style["background-color"] = "blue";
                    checkOTR.Style["display"] = "block";
                    onTheRoad.Style["background-color"] = "blue";
                    line3.Style["background-color"] = "blue";
                    checkDelivered.Style["display"] = "block";
                    delivered.Style["background-color"] = "blue";

                    reviewBox.Style["display"] = "block";
                    break;
                case "CANCELLED":
                    cancelled.Style["display"] = "block";
                    crossDelivered.Style["display"] = "block";
                    crossOTR.Style["display"] = "block";
                    crossPacked.Style["display"] = "block";
                    crossPending.Style["display"] = "block";
                    pending.Style["background-color"] = "red";
                    line1.Style["background-color"] = "red";
                    packed.Style["background-color"] = "red";
                    line2.Style["background-color"] = "red";
                    onTheRoad.Style["background-color"] = "red";
                    line3.Style["background-color"] = "red";
                    delivered.Style["background-color"] = "red";

                    break;
                default:
                    break;
            }
        }

        private string getOrderStatus(string orderID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query =
                @"SELECT status FROM dbo.[Order] WHERE order_id = @orderID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@orderID", orderID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return Convert.ToString(reader[0]);
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    LogError(ex.Message);
                    ShowNotification(ex.Message, "warning");
                }
            }
            return "";
        }

        private void updateAddressInfo(string orderID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

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

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@orderID", orderID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                lblShippingAddress1.Text = reader["address_line1"].ToString();
                                lblShippingAddress2.Text = reader["address_line2"].ToString();
                                lblShippingAddress3.Text = reader["address_line3"].ToString();
                                lblShippingAddress4.Text = reader["address_line4"].ToString();
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    LogError(ex.Message);
                    ShowNotification(ex.Message, "warning");
                }
            }
        }

        private void updatePaymentInfo(string orderID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string query =
                @"SELECT payment_id, date_paid, amount_paid FROM dbo.[Payment] WHERE order_id = @orderID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@orderID", orderID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                lblPaymentID.Text = Convert.ToString(reader[0]);
                                DateTime orderDate = (DateTime)reader[1];
                                lblPaymentDate.Text = orderDate.ToString("MM/dd/yyyy");
                                string amount = "RM " + Convert.ToString(reader[2]);
                                lblPaymentAmount.Text = amount.ToUpper();
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    LogError(ex.Message);
                    ShowNotification(ex.Message, "warning");
                }
            }
        }

        private void updateOrderInfo(string orderID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string getOrderHistoryQuery =
                @"SELECT date_ordered AS dateOrder, status FROM dbo.[Order] WHERE order_id = @orderID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(getOrderHistoryQuery, connection))
                    {
                        command.Parameters.AddWithValue("@orderID", orderID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                lblOrderID.Text = orderID;
                                DateTime orderDate = (DateTime)reader[0];
                                lblOrderDate.Text = orderDate.ToString("MM/dd/yyyy");       
                                string status = Convert.ToString(reader[1]);
                                lblOrderStatus.Text = status.ToUpper();
                            }
                        }
                    }
                }
                catch (SqlException ex)
                {
                    LogError(ex.Message);
                    ShowNotification(ex.Message, "warning");
                }
            }
        }

        private void retrieveOrderDetails(string orderID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string getOrderHistoryQuery =
                @"SELECT pv.product_variant_id AS variantID, p.product_name AS productName, pv.variant_name AS variantName, od.quantity, od.price, (od.quantity * od.price) AS amount, (Select TOP 1 path FROM Image_Path imgp WHERE imgp.product_id = p.product_id ORDER BY imgp.image_path_id ASC) AS imagePath
                FROM Order_Details od 
                INNER JOIN dbo.[Order] o ON od.order_id = o.order_id 
                INNER JOIN Product_Variant pv ON od.product_variant_id = pv.product_variant_id 
                INNER JOIN Product p ON pv.product_id = p.product_id 
                WHERE o.order_id = @orderID";

            String userId = getCurrentUserId();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(getOrderHistoryQuery, connection))
                    {
                        command.Parameters.AddWithValue("@orderID", orderID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                lvOrderList.DataSource = reader;
                                lvOrderList.DataBind();
                            }
                        }

                    }

                }
                catch (SqlException ex)
                {
                    // Handle database errors gracefully (e.g., log the error, display a user-friendly message)
                    LogError(ex.Message);
                    ShowNotification(ex.Message, "warning"); // Redirect to an error page if needed
                }
            }
        }
        protected string DecryptString(string cipherText)
        {
            string EncryptionKey = "ABC123";  
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        private DataTable GetDummyData()
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("ProductName", typeof(string));
            dummyData.Columns.Add("Price", typeof(decimal));
            dummyData.Columns.Add("Quantity", typeof(int));
            dummyData.Columns.Add("Subtotal", typeof(decimal));

            // Add rows with dummy data
            dummyData.Rows.Add("~/Client/Cart/images/i7.png", "Iphone 11", 1500.00m, 2, 3000.00m);
            dummyData.Rows.Add("~/Admin/Layout/image/DexProfilePic.jpeg", "DTX 4090", 10.00m, 1, 10.00m);
            dummyData.Rows.Add("~/Client/Cart/images/ssd.jpg", "Iphone 11", 1500.00m, 2, 3000.00m);
            dummyData.Rows.Add("~/Client/Cart/images/cryingKermit.png", "Kermit", 10.00m, 1, 10.00m);
            // Add more rows as needed for testing

            return dummyData;
        }

        private String getCurrentUserId()
        {
            if (Session["UserId"] != null)
            {
                return Convert.ToString(Session["UserId"]);
            }
            else
            {
                return "CS1001";
            }
        }


        private void LogError(string message)
        {
            Console.WriteLine("Error: " + message);
        }

        protected void ShowNotification(string message, string type)
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }

        protected string EncryptString(string clearText)
        {
            string EncryptionKey = "ABC123"; // Replace with a more complex key and store securely
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        protected void lbReview_Click(object sender, EventArgs e)
        {

        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {

        }
    }
}