using ClosedXML;
using DocumentFormat.OpenXml.Spreadsheet;
using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webAssignment.Admin.Product_Management;
using webAssignment.Admin.Voucher;

namespace webAssignment.Client.Checkout
{
    public partial class CheckoutPage : System.Web.UI.Page
    {

        protected Dictionary<string, string[]> Countries { get; set; } = new Dictionary<string, string[]>()
        {
         { "MAL", new string[] { "Kuala Lumpur", "Selangor", "Penang", "Perak"} }, // Malaysia
    { "SGP", new string[] { "Central", "West", "East", "North" } }, // Singapore
         };

        protected Dictionary<string, string[]> State { get; set; } = new Dictionary<string, string[]>()
{
    { "Kuala Lumpur", new string[] { "Sentul", "Ampang", "Cheras", "Setapak" } }, // Malaysia
    { "Selangor", new string[] { "Shah Alam", "Subang Jaya", "Petaling Jaya", "Klang" } }, // Malaysia
    { "Penang", new string[] { "George Town", "Butterworth", "Kepala Batas", "Seberang Perai" } }, // Malaysia
    { "Perak", new string[] { "Ipoh", "Taiping", "Sitiawan", "Teluk Intan" } }, // Malaysia
    { "Central", new string[] { "Marina Bay", "Bishan", "Toa Payoh", "Ang Mo Kio" } }, // Singapore
    { "West", new string[] { "Jurong East", "Clementi", "Bukit Timah", "Buona Vista" } }, // Singapore
    { "East", new string[] { "Pasir Ris", "Tampines", "Bedok", "Changi" } }, // Singapore
    { "North", new string[] { "Yishun", "Sembawang", "Woodlands", "Sengkang" } } // Singapore
};

        decimal taxRate = 0.00m;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["taxRate"] == null)
                Response.Redirect("~/Client/Cart/CartPage.aspx");
            taxRate = Convert.ToDecimal(Session["taxRate"]);
            if (!IsPostBack)
            {
                if (Session["taxRate"] == null)
                    Response.Redirect("~/Client/Cart/CartPage.aspx");
                taxRate = Convert.ToDecimal(Session["taxRate"]);

                getData();
                updateCartTotal();
                autoFillInDetails();
                retrieveShippingAddress();
                retrieveBillingAddress();
            }
        }

        private void retrieveShippingAddress()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string updateProductListQuery =
                "SELECT * FROM dbo.[Address] WHERE user_id = @userId AND address_type = 'SHIPPING'";

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
                                lvShippingAddress.DataSource = reader;
                                lvShippingAddress.DataBind();
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

        private void retrieveBillingAddress()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string updateProductListQuery =
                "SELECT * FROM dbo.[Address] WHERE user_id = @userId AND address_type = 'BILLING'";

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
                                lvBillingAddress.DataSource = reader;
                                lvBillingAddress.DataBind();
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

        protected void AddressListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            // Check if the user click the reduce btn or the increase btn
            if (e.CommandName == "selectShipping")
            {
                String addressId = e.CommandArgument.ToString();
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string reduceQtyQuery =
                   "SELECT address_line1, address_line2, countryCode, state, city, zip_code, first_name, last_name, phone_number, address_id FROM Address WHERE address_id = @addressId;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(reduceQtyQuery, connection))
                        {
                            command.Parameters.AddWithValue("@addressId", addressId);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    txtFirstName.Text = reader.GetString(6);
                                    txtLastName.Text = reader.GetString(7);
                                    txtPhoneNumber.Text = reader.GetString(8);
                                    txtShippingAddressLine1.Text = reader.GetString(0);
                                    txtShippingAddressLine2.Text = reader.GetString(1);
                                    String countryCode = reader.GetString(2);
                                    String state = reader.GetString(3);
                                    String city = reader.GetString(4);

                                    ddlShippingCountry.Items.Clear();
                                    ddlShippingCountry.Items.Add(new ListItem("Malaysia", "MAL"));
                                    ddlShippingCountry.Items.Add(new ListItem("Singapore", "SGP"));

                                    foreach (ListItem item in ddlShippingCountry.Items)
                                    {
                                        if (item.Value == countryCode)
                                        {
                                            item.Selected = true;
                                        }
                                    }


                                    // Update the State DDL
                                    string selectedShippingCountryCode = ddlShippingCountry.SelectedValue;
                                    string[] states = Countries.ContainsKey(selectedShippingCountryCode) ? Countries[selectedShippingCountryCode] : null;
                                    ddlShippingState.Items.Clear();
                                    foreach (string shippingstate in states)
                                    {
                                        ddlShippingState.Items.Add(new ListItem(shippingstate, shippingstate));
                                    }


                                    // Select the State
                                    foreach (ListItem item in ddlShippingState.Items)
                                    {
                                        if (item.Value == state)
                                        {
                                            item.Selected = true;
                                        }
                                    }


                                    // Update City DDL
                                    string selectedShippingState = ddlShippingState.SelectedValue;
                                    string[] shippingCities = State.ContainsKey(selectedShippingState) ? State[selectedShippingState] : null;
                                    ddlShippingCity.Items.Clear();
                                    foreach (string shippingCity in shippingCities)
                                    {
                                        ddlShippingCity.Items.Add(new ListItem(shippingCity, shippingCity));
                                    }

                                    // Select City
                                    foreach (ListItem item in ddlShippingCity.Items)
                                    {
                                        if (item.Value == city)
                                        {
                                            item.Selected = true;
                                        }

                                    }


                                    txtShippingZipCode.Text = reader.GetString(5);

                                    pnlShipping.Visible = false;
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
            else if (e.CommandName == "selectBilling")
            {
                String addressId = e.CommandArgument.ToString();
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
                string reduceQtyQuery =
                   "SELECT address_line1, address_line2, countryCode, state, city, zip_code, first_name, last_name, phone_number FROM Address WHERE address_id = @addressId;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(reduceQtyQuery, connection))
                        {
                            command.Parameters.AddWithValue("@addressId", addressId);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    txtAddressLine1.Text = reader.GetString(0);
                                    txtAddressLine2.Text = reader.GetString(1);
                                    String countryCode = reader.GetString(2);
                                    String state = reader.GetString(3);
                                    String city = reader.GetString(4);

                                    ddlCountry.Items.Clear();
                                    ddlCountry.Items.Add(new ListItem("Malaysia", "MAL"));
                                    ddlCountry.Items.Add(new ListItem("Singapore", "SGP"));
                                    foreach (ListItem item in ddlCountry.Items)
                                    {
                                        if (item.Value == countryCode)
                                        {
                                            item.Selected = true;
                                        }
                                    }


                                    // Update the State DDL
                                    string selectedBillingCountryCode = ddlCountry.SelectedValue;
                                    string[] states = Countries.ContainsKey(selectedBillingCountryCode) ? Countries[selectedBillingCountryCode] : null;
                                    ddlState.Items.Clear();
                                    foreach (string Billingstate in states)
                                    {
                                        ddlState.Items.Add(new ListItem(Billingstate, Billingstate));
                                    }


                                    // Select the State
                                    foreach (ListItem item in ddlState.Items)
                                    {
                                        if (item.Value == state)
                                        {
                                            item.Selected = true;
                                        }
                                        else
                                        {
                                            item.Selected = false;
                                        }
                                    }


                                    // Update City DDL
                                    string selectedBillingState = ddlState.SelectedValue;
                                    string[] shippingCities = State.ContainsKey(selectedBillingState) ? State[selectedBillingState] : null;
                                    ddlCity.Items.Clear();
                                    foreach (string BillingCity in shippingCities)
                                    {
                                        ddlCity.Items.Add(new ListItem(BillingCity, BillingCity));
                                    }

                                    // Select City
                                    foreach (ListItem item in ddlCity.Items)
                                    {
                                        if (item.Value == city)
                                        {
                                            item.Selected = true;
                                        }
                                        else
                                        {
                                            item.Selected = false;
                                        }
                                    }


                                    txtZipCode.Text = reader.GetString(5);
                                    pnlBilling.Visible = false;

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
        }



        private void autoFillInDetails()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string updateProductListQuery =
                "SELECT first_name, last_name, phone_number FROM dbo.[User] WHERE user_id = @userId";

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
                                reader.Read();
                                txtFirstName.Text = Convert.ToString(reader[0]);
                                txtLastName.Text = Convert.ToString(reader[1]);
                                txtPhoneNumber.Text = Convert.ToString(reader[2]);
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

        protected void validateInput(object sender, EventArgs e)
        {
            String firstName = txtFirstName.Text;
            String lastName = txtLastName.Text;
            String phoneNumber = txtPhoneNumber.Text;

            String addressLine1 = txtAddressLine1.Text;
            String addressLine2 = txtAddressLine2.Text;
            String country = ddlCountry.SelectedValue.ToString();
            String state = ddlState.SelectedValue.ToString();
            String city = ddlCity.SelectedValue.ToString();
            String zipcode = txtZipCode.Text;

            String shippingAddressLine1 = txtShippingAddressLine1.Text;
            String shippingAddressLine2 = txtShippingAddressLine2.Text;
            String shippingCountry = ddlShippingCountry.SelectedValue.ToString();
            String shippingState = ddlShippingState.SelectedValue.ToString();
            String shippingCity = ddlShippingCity.SelectedValue.ToString();
            String shippingZipcode = txtShippingZipCode.Text.ToString();

            String orderNote = txtOrderNote.Text.ToString();

            string[] shippingDetails = { "First Name", "Last Name", "Phone Number", "Shipping Address Line 1", "Shipping Address Line 2", "Shipping Zip Code" };
            TextBox[] shippingTextField = { txtFirstName, txtLastName, txtPhoneNumber, txtShippingAddressLine1, txtShippingAddressLine2, txtShippingZipCode };
            for (int i = 0; i < shippingDetails.Length; i++)
            {
                if (string.IsNullOrEmpty(shippingTextField[i].Text))
                {
                    ShowNotification($"{shippingDetails[i]} Cannot Be Empty", "warning");
                    shippingTextField[i].Focus();
                    return; // Exit the loop if a field is empty
                }
            }

            if (firstName.Any(char.IsDigit))
            {
                ShowNotification("First Name cannot contain digit", "warning");
                txtFirstName.Focus();
                return;
            }
            else if (firstName.Length > 50)
            {
                ShowNotification("First Name cannot exceed 50 characters", "warning");
                txtFirstName.Focus();
                return;
            }
            else if (lastName.Any(char.IsDigit))
            {
                ShowNotification("Last Name cannot contain digit", "warning");
                txtLastName.Focus();
                return;
            }
            else if (lastName.Length > 50)
            {
                ShowNotification("Last Name cannot exceed 50 characters", "warning");
                txtLastName.Focus();
                return;
            }
            else if (phoneNumber.Any(char.IsLetter))
            {
                ShowNotification("Phone Number cannot contain letter", "warning");
                txtPhoneNumber.Focus();
                return;
            }
            else if (phoneNumber.Length > 15)
            {
                ShowNotification("Phone Number cannot exceed 15 characters", "warning");
                txtPhoneNumber.Focus();
                return;
            }
            else if (shippingAddressLine1.Length > 50)
            {
                ShowNotification("Shipping Address Line 1 cannot exceed 50 characters", "warning");
                txtShippingAddressLine1.Focus();
                return;
            }
            else if (shippingAddressLine2.Length > 50)
            {
                ShowNotification("Shipping Address Line 2 cannot exceed 50 characters", "warning");
                txtShippingAddressLine2.Focus();
                return;
            }
            else if (shippingZipcode.Any(char.IsLetter))
            {
                ShowNotification("Shipping Zip Code should be numeric only", "warning");
                txtShippingZipCode.Focus();
                return;
            }
            else if (shippingZipcode.Length != 5)
            {
                ShowNotification("Shipping Zip Code should be 5 digit", "warning");
                txtShippingZipCode.Focus();
                return;
            }
            else if (shippingCountry == "null")
            {
                ShowNotification("Please select a shipping country", "warning");
                ddlShippingCountry.Focus();
                return;
            }
            else if (shippingState == "null")
            {
                ShowNotification("Please select a shipping state", "warning");
                ddlShippingState.Focus();
                return;
            }
            else if (shippingCity == "null")
            {
                ShowNotification("Please select a shipping city", "warning");
                ddlShippingCity.Focus();
                return;
            }



            // If the billing address is different
            if (cbxBill.Checked)
            {
                // Check if any input field is empty
                string[] fieldNames = { "Address Line 1", "Address Line 2", "Zip Code" };
                TextBox[] textFields = { txtAddressLine1, txtAddressLine2, txtZipCode };
                for (int i = 0; i < fieldNames.Length; i++)
                {
                    if (string.IsNullOrEmpty(textFields[i].Text))
                    {
                        ShowNotification($"{fieldNames[i]} Cannot Be Empty", "warning");
                        textFields[i].Focus();
                        return;
                    }
                }

                // Validate the input 
                if (addressLine1.Length > 50)
                {
                    ShowNotification("Address Line 1 cannot exceed 50 characters", "warning");
                    txtAddressLine1.Focus();
                    return;
                }
                else if (addressLine2.Length > 50)
                {
                    ShowNotification("Address Line 2 cannot exceed 50 characters", "warning");
                    txtAddressLine2.Focus();
                    return;
                }
                else if (zipcode.Any(char.IsLetter))
                {
                    ShowNotification("Zip Code should be numeric only", "warning");
                    txtZipCode.Focus();
                    return;
                }
                else if (zipcode.Length != 5)
                {
                    ShowNotification("Zip Code should be 5 digit", "warning");
                    txtZipCode.Focus();
                    return;
                }
                else if (country == "null")
                {
                    ShowNotification("Please select a country", "warning");
                    ddlCountry.Focus();
                    return;
                }
                else if (state == "null")
                {
                    ShowNotification("Please select a state", "warning");
                    ddlState.Focus();
                    return;
                }
                else if (city == "null")
                {
                    ShowNotification("Please select a city", "warning");
                    ddlCity.Focus();
                    return;
                }
            }
            else // If ship to the same address
            {
                addressLine1 = shippingAddressLine1;
                addressLine2 = shippingAddressLine2;
                country = shippingCountry;
                state = shippingState;
                city = shippingCity;
                zipcode = shippingZipcode;
            }

            if (orderNote.Length > 100)
            {
                ShowNotification("Order Note cannot exceed 100 character", "warning");
                txtOrderNote.Focus();
                return;
            }

            Address shippingAddress = new Address(firstName, lastName, phoneNumber,
                                      shippingAddressLine1, shippingAddressLine2, shippingCountry,
                                      shippingState, shippingCity, shippingZipcode);
            Session["shippingAddress"] = shippingAddress;

            if (cbxBill.Checked)
            {
                Address billingAddress = new Address(firstName, lastName, phoneNumber, addressLine1, addressLine2, country, state, city, zipcode);
                Session["billingAddress"] = billingAddress;
            }
            else
            {
                Session["billingAddress"] = shippingAddress;
            }

            if (txtOrderNote.Text.Length > 0)
                Session["OrderNote"] = txtOrderNote.Text;
            else
                Session["OrderNote"] = null;

            ShowNotification("Good", "success");

            Response.Redirect("PaymentPage.aspx");

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
                                    decimal shipping = (subtotal > 100.0m) ? 0.0m : 15.0m;
                                    string voucher = Convert.ToString(Session["Voucher"]);
                                    decimal discountRate = 0.0m;
                                    if (voucher != "")
                                        discountRate = checkDiscountRate(voucher, command, connection);
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

        private decimal checkDiscountRate(string voucher, SqlCommand command, SqlConnection connection)
        {
            string getDiscountRate = "SELECT discount_rate FROM Voucher WHERE voucher_id = @id;";
            using (command = new SqlCommand(getDiscountRate, connection))
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
                Response.Redirect("~/Client/LoginSignUo/Login.aspx");
            }
            return "";
        }

        protected void btnSelectShippingAdd_Clicked(object sender, EventArgs e)
        {
            if (pnlShipping.Visible) pnlShipping.Visible = false; else pnlShipping.Visible = true;
        }

        protected void btnSelectBillingAdd_Clicked(object sender, EventArgs e)
        {
            if (pnlBilling.Visible) pnlBilling.Visible = false; else pnlBilling.Visible = true;
        }




        protected void cbxBill_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxBill.Checked)
            {
                shippingAddressDetails.Attributes["class"] = "block";
            }
            else
            {
                shippingAddressDetails.Attributes["class"] = "hidden";
            }
        }

        protected void ShowNotification(string message, string type)
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }


        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCountryCode = ddlCountry.SelectedValue;
            string[] states = Countries.ContainsKey(selectedCountryCode) ? Countries[selectedCountryCode] : null;

            ddlState.Items.Clear(); // Clear existing state options
            ddlCity.Items.Clear(); // Clear existing state options
            ddlCity.Items.Add(new ListItem("-Default-", "null"));
            if (states != null)
            {
                ddlState.Items.Add(new ListItem("-Default-", "null"));
                foreach (string state in states)
                {
                    ddlState.Items.Add(new ListItem(state, state));
                }
                ddlState.SelectedIndex = 0;
            }
            else
            {
                // Handle cases where the country doesn't have states
                ddlState.Items.Add(new ListItem("-Default-", "null"));
            }
        }


        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = ddlState.SelectedValue;
            string[] states = State.ContainsKey(selectedState) ? State[selectedState] : null;

            ddlCity.Items.Clear(); // Clear existing state options
            if (states != null)
            {
                ddlCity.Items.Add(new ListItem("-Default-", "null"));
                foreach (string state in states)
                {
                    ddlCity.Items.Add(new ListItem(state, state));
                }
            }
            else
            {
                // Handle cases where the country doesn't have states
                ddlCity.Items.Add(new ListItem("-Default-", "null"));
            }
        }


        protected void ddlShippingCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedCountryCode = ddlShippingCountry.SelectedValue;
            string[] states = Countries.ContainsKey(selectedCountryCode) ? Countries[selectedCountryCode] : null;

            ddlShippingState.Items.Clear(); // Clear existing state options
            ddlShippingCity.Items.Clear(); // Clear existing state options
            ddlShippingCity.Items.Add(new ListItem("-Default-", "null"));
            if (states != null)
            {
                ddlShippingState.Items.Add(new ListItem("-Default-", "null"));
                foreach (string state in states)
                {
                    ddlShippingState.Items.Add(new ListItem(state, state));
                }
                ddlShippingState.SelectedIndex = 0;
            }
            else
            {
                // Handle cases where the country doesn't have states
                ddlShippingState.Items.Add(new ListItem("-Default-", "null"));
            }
        }

        protected void ddlShippingState_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedState = ddlShippingState.SelectedValue;
            string[] states = State.ContainsKey(selectedState) ? State[selectedState] : null;

            ddlShippingCity.Items.Clear(); // Clear existing state options
            if (states != null)
            {
                ddlShippingCity.Items.Add(new ListItem("-Default-", "null"));
                foreach (string state in states)
                {
                    ddlShippingCity.Items.Add(new ListItem(state, state));
                }
            }
            else
            {
                // Handle cases where the country doesn't have states
                ddlShippingCity.Items.Add(new ListItem("-Default-", "null"));
            }
        }
    }

}


