using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Spreadsheet;
using DocumentFormat.OpenXml.Office.Word;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using webAssignment.Admin.Voucher;
using webAssignment.Client.Profile;

namespace webAssignment.Client.Checkout
{
    public partial class PaymentPage : System.Web.UI.Page
    {
        Address shippingAddress;
        Address billingAddress;

        decimal discountRate = 0.5m;
        decimal taxRate = 0.00m;
        protected void Page_Load(object sender, EventArgs e)
        {
            Address add = (Address)Session["shippingAddress"];

            if (!IsPostBack)
            {
                if (Session["taxRate"] == null)
                    Response.Redirect("~/Client/Cart/CartPage.aspx");
                taxRate = Convert.ToDecimal(Session["taxRate"]);
                getData();
                updateCartTotal();
                updateCardExpiryDDL();

                shippingAddress = (Address)Session["shippingAddress"];
                billingAddress = (Address)Session["billingAddress"];

            }
        }
        private void updateCardExpiryDDL()
        {
            int currentYear = DateTime.Now.Year;

            // Populate expiration year dropdown
            for (int year = currentYear; year <= currentYear + 4; year++)
            {
                ddlExpirationYear.Items.Add(new ListItem(year.ToString(), year.ToString()));
            }

            int currentMonth = DateTime.Now.Month;

            // Populate expiration month dropdown
            for (int month = currentMonth; month <= 12; month++)
            {
                string monthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);
                ddlExpirationMonth.Items.Add(new ListItem(monthName, month.ToString()));
            }
        }


        private void getData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string updateProductListQuery =
                "SELECT p.product_name AS productName, pv.variant_name AS variantName, cd.quantity, pv.variant_price AS price, (Select TOP 1 path FROM Image_Path imgp WHERE imgp.product_id = p.product_id ORDER BY imgp.image_path_id ASC) AS imagePath FROM Cart_Details cd INNER JOIN Cart c ON cd.cart_id = c.cart_id INNER JOIN Product_Variant pv ON cd.product_variant_id = pv.product_variant_id INNER JOIN Product p ON pv.product_id = p.product_id WHERE c.user_id = @userId;";

            String userId = getCurrentUserId();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateProductListQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                lvCheckoutProduct.DataSource = reader;
                                lvCheckoutProduct.DataBind();
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

        private void LogError(string message)
        {
            Console.WriteLine("Error: " + message);
        }

        private void updateCartTotal()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string getCartTotalQuery =
                "SELECT cd.quantity, pv.variant_price AS price FROM Cart c INNER JOIN Cart_Details cd ON c.cart_id = cd.cart_id INNER JOIN Product_Variant pv ON cd.product_variant_id = pv.product_variant_id WHERE c.user_id = @userId;";

            String userId = getCurrentUserId(); // Replace with your logic to get the current user ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(getCartTotalQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                decimal subtotal = 0.0m;
                                while (reader.Read())
                                {

                                    int qty = Convert.ToInt32(reader[0]);
                                    decimal price = Convert.ToDecimal(reader[1]);
                                    subtotal += qty * price;
                                }
                                calTotal(subtotal);


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

        private void calTotal(decimal subtotal)
        {
            decimal shipping = (subtotal > 100.0m) ? 0.0m : 15.0m;
            string voucher = getVoucher();
            decimal discountRate = 0.0m;
            if (voucher != null)
                discountRate = checkDiscountRate(voucher);
            decimal discount = subtotal * discountRate;
            decimal tax = (subtotal - discount) * taxRate;
            decimal total = subtotal - discount + tax + shipping;

            lblCartSubtotal.Text = "RM " + $"{subtotal:F2}";
            if (shipping == 0.0m)
                lblCartShipping.Text = "Free";
            else
                lblCartShipping.Text = "RM " + $"{shipping:F2}";
            lblCartDiscount.Text = "[-" + $"{discountRate * 100:F0}" + "%] RM " + $"{discount:F2}";
            lblCartTax.Text = "[" + $"{taxRate * 100:F0}" + "%] RM " + $"{tax:F2}";
            lblCartTotal.Text = "RM " + $"{total:F2}";

            Session["orderTotal"] = total.ToString();
        }

        private decimal checkDiscountRate(string voucher)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            string getDiscountRate = "SELECT discount_rate FROM Voucher WHERE voucher_id = @id;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                try
                {
                    using (SqlCommand command = new SqlCommand(getDiscountRate, connection))
                    {
                        command.Parameters.AddWithValue("@id", voucher);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                Session["Voucher"] = voucher;
                                return Convert.ToDecimal(reader[0]);
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


            Session["Voucher"] = "";
            return 0.0m;
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

        protected void btnCard_Click(object sender, EventArgs e)
        {
            pnlCard.Visible = true;
            pnlBank.Visible = false;
            pnlCOD.Visible = false;
        }

        protected void btnBank_Click(object sender, EventArgs e)
        {
            pnlCard.Visible = false;
            pnlBank.Visible = true;
            pnlCOD.Visible = false;

        }

        protected void btnCOD_Click(object sender, EventArgs e)
        {
            pnlCard.Visible = false;
            pnlBank.Visible = false;
            pnlCOD.Visible = true;

        }

        protected void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            // Select Card
            if (pnlCard.Visible)
            {
                String cardNumber = txtCardNumber.Text.ToString();
                String expMonth = ddlExpirationMonth.SelectedValue.ToString();
                String expYear = ddlExpirationYear.SelectedValue.ToString();
                String cvv = txtCVV.Text.ToString();

                if (cardNumber == "")
                {
                    ShowNotification("Card number cannot be empty", "warning");
                    txtCardNumber.Focus();
                    return;
                }
                else if (cardNumber.Any(char.IsLetter) || cardNumber.Any(char.IsSymbol))
                {
                    ShowNotification("Card number should contain number only", "warning");
                    txtCardNumber.Focus();
                    return;
                }
                else if (cardNumber.Length != 16)
                {
                    ShowNotification("Card number should be 16 digit", "warning");
                    txtCardNumber.Focus();
                    return;
                }
                else if (cvv == "")
                {
                    ShowNotification("CVV cannot be empty", "warning");
                    txtCVV.Focus();
                    return;
                }

                else if (cvv.Any(char.IsLetter) || cvv.Any(char.IsSymbol))
                {
                    ShowNotification("CVV should contain number only", "warning");
                    txtCVV.Focus();
                    return;
                }
                else if (cvv.Length != 3)
                {
                    ShowNotification("CVV should be 3 digit", "warning");
                    txtCVV.Focus();
                    return;
                }

                // Update Address Table
                shippingAddress = (Address)Session["shippingAddress"];
                string addressID = checkAddressExist(shippingAddress);
                if (addressID == "")
                {
                    addressID = createAddress(shippingAddress, "SHIPPING");
                }

                // Update Order Table
                string orderID = createOrder(addressID);


                // Payment Details
                Dictionary<string, string> paymentDetails = new Dictionary<string, string>()
                {
                    {"bank_name", cardNumber},
                    {"expiry_month", expMonth},
                    {"expiry_year", expYear},
                    {"cvv", cvv}
                };
                string paymentDetailsJson = JsonConvert.SerializeObject(paymentDetails);

                decimal total = Convert.ToDecimal(Session["orderTotal"]);

                // Update Order Details Table
                createOrderDetails(orderID);

                // Update Payment Table
                createPayment(paymentDetailsJson, orderID, total);


                // Get And Update Stock
                getStock();

                // Clear cart details
                clearCartDetails();


                // Decrease Voucher Quantity
                if (getVoucher() != null)
                {
                    updateVoucherQuantity();
                }
                clearSession();

                sendEmail();

                popUpOrderPlaced.Style.Add("display", "flex");

            }
            else if (pnlBank.Visible)
            {
                string bankNumber = txtBankNumber.Text;

                if (bankNumber == "")
                {
                    ShowNotification("Bank number cannot be empty", "warning");
                    txtBankNumber.Focus();
                    return;
                }
                else if (bankNumber.Any(char.IsLetter) || bankNumber.Any(char.IsSymbol))
                {
                    ShowNotification("Bank number should contain number only", "warning");
                    txtBankNumber.Focus();
                    return;
                }
                else if (bankNumber.Length != 16)
                {
                    ShowNotification("Bank number should be 16 digit", "warning");
                    txtBankNumber.Focus();
                    return;
                }


                // Update Address Table
                shippingAddress = (Address)Session["shippingAddress"];
                string addressID = checkAddressExist(shippingAddress);
                if (addressID == "")
                {
                    addressID = createAddress(shippingAddress, "SHIPPING");
                }

                // Update Order Table
                string orderID = createOrder(addressID);


                // Payment Details
                string bank = ddlBank.SelectedValue.ToString();
                Dictionary<string, string> paymentDetails = new Dictionary<string, string>()
                {
                    {"bank_name", bank},
                    {"bank_number", bankNumber}
                };
                string paymentDetailsJson = JsonConvert.SerializeObject(paymentDetails);

                decimal total = Convert.ToDecimal(Session["orderTotal"]);

                // Update Order Details Table
                createOrderDetails(orderID);

                // Update Payment Table
                createPayment(paymentDetailsJson, orderID, total);


                // Get And Update Stock
                getStock();

                // Clear cart details
                clearCartDetails();


                // Decrease Voucher Quantity
                if (getVoucher() != null)
                {
                    updateVoucherQuantity();
                }
                sendEmail();


                clearSession();

                popUpOrderPlaced.Style.Add("display", "flex");


            }
            else if (pnlCOD.Visible)
            {
                // Update Address Table
                shippingAddress = (Address)Session["shippingAddress"];
                string addressID = checkAddressExist(shippingAddress);
                if (addressID == "")
                {
                    addressID = createAddress(shippingAddress, "SHIPPING");
                }

                // Update Order Table
                string orderID = createOrder(addressID);


                // Payment Details
                Dictionary<string, string> paymentDetails = new Dictionary<string, string>()
                {
                    {"COD", "Cash On Delivery"}
                };
                string paymentDetailsJson = JsonConvert.SerializeObject(paymentDetails);

                decimal total = Convert.ToDecimal(Session["orderTotal"]);

                // Update Order Details Table
                createOrderDetails(orderID);

                // Update Payment Table
                createPayment(paymentDetailsJson, orderID, total);


                // Get And Update Stock
                getStock();

                // Clear cart details
                clearCartDetails();


                // Decrease Voucher Quantity
                if (getVoucher() != null)
                {
                    updateVoucherQuantity();
                }

                sendEmail();

                // Remove Session 
                clearSession();


                popUpOrderPlaced.Style.Add("display", "flex");
            }



        }

        protected void closePopUp_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/Home/HomePage.aspx");
        }

        private void clearSession()
        {
            Session.Remove("orderTotal");
            Session.Remove("Voucher");
            Session.Remove("taxRate");
            Session.Remove("billingAddress");
            Session.Remove("shippingAddress");
        }
        private void updateVoucherQuantity()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string updateStockQuery = @"
                UPDATE Voucher
                SET quantity = quantity - 1
                WHERE voucher_id= @voucherID;
            ";

            string voucherID = getVoucher();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateStockQuery, connection))
                    {
                        command.Parameters.AddWithValue("@voucherID", voucherID);

                        command.ExecuteNonQuery();
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
    

        private void getStock()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string getStock =
                @"SELECT pv.product_variant_id, pv.stock, cd.quantity FROM Product_Variant pv INNER JOIN Cart_Details cd ON pv.product_variant_id = cd.product_variant_id INNER JOIN Cart c ON c.cart_id = cd.cart_id WHERE user_id = @userid";

            String userId = getCurrentUserId();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(getStock, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string prodID = Convert.ToString(reader[0]);
                                int stock = Convert.ToInt32(reader[1]);
                                int qty = Convert.ToInt32(reader[2]);

                                updateStock(prodID, stock - qty);
                            }
                        }
                    }

                }
                catch (SqlException ex)
                {
                    // Handle database errors gracefully (e.g., log the error, display a user-friendly message)
                    LogError(ex.Message);
                    ShowNotification(ex.Message, "warning");
                    ShowNotification(ex.Message, "warning");
                }
            }
        }

        private void updateStock( string prodID, int newStock)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string updateStockQuery = @"
                UPDATE Product_Variant 
                SET stock = @newStock
                WHERE product_variant_id = @prodID;
            ";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateStockQuery, connection))
                    {
                        command.Parameters.AddWithValue("@prodID", prodID);
                        command.Parameters.AddWithValue("@newStock", newStock);

                        command.ExecuteNonQuery();
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

        private void clearCartDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string retriveUserCartDetails =
                "DELETE FROM Cart_Details WHERE cart_id IN (SELECT cart_id FROM Cart WHERE user_id = @userId)";

            String userId = getCurrentUserId();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(retriveUserCartDetails, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        command.ExecuteNonQuery();
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
        private void createOrderDetails(string orderID)
        {
            // Get the user cart details
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string retriveUserCartDetails =
                "SELECT cd.product_variant_id AS variantID, cd.quantity, pv.variant_price AS price FROM Cart_Details cd INNER JOIN Cart c ON cd.cart_id = c.cart_id INNER JOIN Product_Variant pv ON cd.product_variant_id = pv.product_variant_id WHERE c.user_id = @userId;";

            String userId = getCurrentUserId();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(retriveUserCartDetails, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            // Transfer To Order Details
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string variantID = Convert.ToString(reader[0]);
                                    int quantity = Convert.ToInt32(reader[1]);
                                    decimal price = Convert.ToDecimal(reader[2]);
                                    insertOrderDetails(orderID, variantID, quantity, price);

                                }

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

        private void insertOrderDetails(string orderID, string variantID, int qty, decimal total)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string insertOrderDetailsQuery = "INSERT INTO dbo.[Order_details] ([order_id], [product_variant_id], [quantity], [price]) VALUES (@orderID, @variantID, @quantity, @total);";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(insertOrderDetailsQuery, connection))
                {
                    // Add parameters
                    command.Parameters.AddWithValue("@orderID", orderID);
                    command.Parameters.AddWithValue("@variantID", variantID);
                    command.Parameters.AddWithValue("@quantity", qty);
                    command.Parameters.AddWithValue("@total", total);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        
                    }
                    catch (SqlException ex)
                    {
                        LogError(ex.Message);
                        ShowNotification(ex.Message, "warning"); // Redirect to an error page if needed
                    }
                }
            }
        }

        private string createOrder(String addressID)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


            string getNextNum = "INSERT INTO dbo.[Order] ([order_id], [user_id], [voucher_id], [total_price], [date_ordered], [status], [address_id], [note]) VALUES (@orderID, @userID, @voucherID, @total, @date, @status, @addressID, @note)";

            string newOrderID = getNextOrderID();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(getNextNum, connection))
                    {
                        
                        string userID = getCurrentUserId();
                        string voucherID = getVoucher();
                        decimal total = Convert.ToDecimal(Session["orderTotal"]);
                        DateTime dateOrdered = DateTime.Now.Date;
                        string formattedDate = dateOrdered.ToString("MM-dd-yyyy");
                        string status = "PENDING";
                        string note = Convert.ToString(Session["OrderNote"]);

                        command.Parameters.AddWithValue("@orderID", newOrderID);
                        command.Parameters.AddWithValue("@userID", getCurrentUserId());
                        command.Parameters.AddWithValue("@total", total);
                        if(voucherID == null)
                            command.Parameters.AddWithValue("@voucherID", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@voucherID", voucherID);
                        command.Parameters.AddWithValue("@date", formattedDate);
                        command.Parameters.AddWithValue("@status", status);
                        command.Parameters.AddWithValue("@addressID", addressID);
                        if(note == null)
                            command.Parameters.AddWithValue("@note", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@note", note);


                        command.ExecuteNonQuery();
                    }

                }
                catch (SqlException ex)
                {
                    // Handle database errors gracefully (e.g., log the error, display a user-friendly message)
                    LogError(ex.Message);
                    ShowNotification(ex.Message, "warning");
                }
            }
            return newOrderID;
        }

        private string getNextOrderID()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string getLatestNum = "SELECT MAX(order_id) FROM dbo.[Order];";
            string newOrderID = "O1001";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(getLatestNum, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            int currentOrderID = Convert.ToInt32(result.ToString().Substring(1));
                            int nextOrderID = currentOrderID + 1;
                            newOrderID = "O" + nextOrderID;
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
            return newOrderID;
        }

        private string getNextPaymentID()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string getLatestNum = "SELECT MAX(payment_id) FROM dbo.[Payment];";
            string newPaymentID = "PAY1001";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(getLatestNum, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            int currentPaymentID = Convert.ToInt32(result.ToString().Substring(3));
                            int nextPaymentID = currentPaymentID + 1;
                            newPaymentID = "PAY" + nextPaymentID;
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
            return newPaymentID;
        }

        private void createPayment(string paymentDetails, String orderID, decimal amountPaid)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string createPaymentQuery =
                "INSERT INTO dbo.[Payment] ([payment_id], [order_id], [payment_details], [status], [date_paid], [amount_paid]) VALUES (@paymentID, @orderID, @paymentDetails, @status, @date, @amount)";

            String userId = getCurrentUserId(); // Replace with your logic to get the current user ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(createPaymentQuery, connection))
                    {
                        command.Parameters.AddWithValue("@paymentID", getNextPaymentID());
                        command.Parameters.AddWithValue("@orderID", orderID);
                        command.Parameters.AddWithValue("@paymentDetails", paymentDetails);
                        command.Parameters.AddWithValue("@status", "PENDING");
                        command.Parameters.AddWithValue("@date", DateTime.Now.Date.ToString("MM-dd-yyyy"));
                        command.Parameters.AddWithValue("@amount", amountPaid);

                        command.ExecuteNonQuery();
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

        

        private string createAddress(Address address, string type)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


            string getNextNum = "INSERT INTO dbo.[Address] ([address_id], [user_id], [address_type], [address_line1], [address_line2], [countryCode], [state], [city], [zip_code], [first_name], [last_name], [phone_number]) VALUES (@addID, @userID, @type, @address1, @address2, @country, @state, @city, @zip, @fn, @ln, @pn)";
            string newAddID = getNextAddressID();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(getNextNum, connection))
                    {
                        command.Parameters.AddWithValue("@addID", newAddID);
                        command.Parameters.AddWithValue("@userID", getCurrentUserId());
                        command.Parameters.AddWithValue("@type", type);
                        command.Parameters.AddWithValue("@address1", address.addressLine1);
                        command.Parameters.AddWithValue("@address2", address.addressLine2);
                        command.Parameters.AddWithValue("@country", address.country);
                        command.Parameters.AddWithValue("@state", address.state);
                        command.Parameters.AddWithValue("@city", address.city);
                        command.Parameters.AddWithValue("@zip", address.zipcode);
                        command.Parameters.AddWithValue("@fn", address.firstName);
                        command.Parameters.AddWithValue("@ln", address.lastName);
                        command.Parameters.AddWithValue("@pn", address.phoneNumber);

                        command.ExecuteNonQuery();
                    }

                }
                catch (SqlException ex)
                {
                    // Handle database errors gracefully (e.g., log the error, display a user-friendly message)
                    LogError(ex.Message);
                    ShowNotification(ex.Message, "warning"); // Redirect to an error page if needed
                }
            }
            return newAddID;
        }

        private string checkAddressExist(Address address)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


            string getNextNum = "SELECT address_id FROM dbo.[Address] WHERE address_line1 = @address1 AND address_line2 = @address2 AND countryCode = @country AND state = @state AND city = @city AND zip_code = @zip AND first_name = @fn AND last_name = @ln AND phone_number = @pn AND user_id = @userid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(getNextNum, connection))
                    {
                        command.Parameters.AddWithValue("@address1", address.addressLine1);
                        command.Parameters.AddWithValue("@address2", address.addressLine2);
                        command.Parameters.AddWithValue("@country", address.country);
                        command.Parameters.AddWithValue("@state", address.state);
                        command.Parameters.AddWithValue("@city", address.city);
                        command.Parameters.AddWithValue("@zip", address.zipcode);
                        command.Parameters.AddWithValue("@fn", address.firstName);
                        command.Parameters.AddWithValue("@ln", address.lastName);
                        command.Parameters.AddWithValue("@pn", address.phoneNumber);
                        command.Parameters.AddWithValue("@userid", getCurrentUserId());


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return reader[0].ToString();
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
            return "";
        }

        private string getNextAddressID()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string getLatestNum = "SELECT MAX(address_id) FROM dbo.[Address];";
            string newAddID = "ADDR1001";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(getLatestNum, connection))
                    {
                        object result = command.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            int currentAddID = Convert.ToInt32(result.ToString().Substring(4));
                            int nextAddID = currentAddID + 1;
                            newAddID = "ADDR" + nextAddID;
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
            return newAddID;
        }


        private string getVoucher()
        {
            if (Convert.ToString(Session["Voucher"]) != "")
                return Convert.ToString(Session["Voucher"]);
            else
                return null;
        }

        protected void ShowNotification(string message, string type)
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }


        private void sendEmail()
        {
            try
            {
                string senderEmail = "gtechpc24@gmail.com";
                MailMessage verificationMail = new MailMessage(senderEmail, getUserEmail());
                verificationMail.Subject = "Order Pending";

                verificationMail.Body = $"<h3>Hi User {getCurrentUserId()},</h3>" +
                                        "<p>We have received your order. Your order status is currently pending</p>" +
                                        "<p>Your order will be packed and shipped within 3 business days.</p>" +
                                        "<p>Thank you for choosing us, G-Tech Team.</p>";
                verificationMail.IsBodyHtml = true;
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(senderEmail, "lajd btuc nhuf qryg");

                smtpClient.Send(verificationMail);
            }
            catch (Exception ex)
            {
                ShowNotification(ex.Message, "warning");
            }
        }

        private string getUserEmail()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


            string getNextNum = "SELECT email FROM User WHERE user_id = @userid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(getNextNum, connection))
                    {
                        command.Parameters.AddWithValue("@userid", getCurrentUserId());


                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return reader.GetString(0);
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
            return "";
        }
    }
}
