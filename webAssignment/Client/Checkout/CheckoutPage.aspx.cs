using ClosedXML;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using webAssignment.Admin.Voucher;

namespace webAssignment.Client.Checkout
{
    public partial class CheckoutPage : System.Web.UI.Page
    {
        decimal discountRate = 0.5m;
        decimal taxRate = 0.06m;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getData();
                updateCartTotal();
                ddlCountry_SelectedIndexChanged(sender, e);
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


            // Check if any input field is empty
            string[] fieldNames = { "First Name", "Last Name", "Phone Number", "Address Line 1", "Address Line 2", "Zip Code" };
            TextBox[] textFields = { txtFirstName, txtLastName, txtPhoneNumber, txtAddressLine1, txtAddressLine2, txtZipCode };
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
            else if (addressLine1.Length > 50)
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

            // If the shipping address is different
            if (cbxShip.Checked)
            {
                string[] shippingDetails = { "Shipping Address Line 1", "Shipping Address Line 2", "Shipping Zip Code" };
                TextBox[] shippingTextField = { txtShippingAddressLine1, txtShippingAddressLine2, txtShippingZipCode };
                for (int i = 0; i < shippingDetails.Length; i++)
                {
                    if (string.IsNullOrEmpty(shippingTextField[i].Text))
                    {
                        ShowNotification($"{shippingDetails[i]} Cannot Be Empty", "warning");
                        shippingTextField[i].Focus();
                        return; // Exit the loop if a field is empty
                    }
                }

                if (shippingAddressLine1.Length > 50)
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
            }
            else // If ship to the same address
            {
                shippingAddressLine1 = addressLine1;
                shippingAddressLine2 = addressLine2;
                shippingCountry = country;
                shippingState = state;
                shippingCity = city;
                shippingZipcode = zipcode;
            }

            if (orderNote.Length > 100)
            {
                ShowNotification("Order Note cannot exceed 100 character", "warning");
                txtOrderNote.Focus();
                return;
            }

            Address billingAddress = new Address(firstName, lastName, phoneNumber,
                                      addressLine1, addressLine2, country,
                                      state, city, zipcode);
            Session["billingAddress"] = billingAddress;

            if (cbxShip.Checked)
            {
                Address shippingAddress = new Address(firstName, lastName, phoneNumber, shippingAddressLine1, shippingAddressLine2, shippingCountry, shippingState, shippingCity, shippingZipcode);
                Session["shippingAddress"] = shippingAddress;
            }
            else
            {
                Session["shippingAddress"] = billingAddress;
            }

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
                    Response.Redirect("~/ErrorPage.aspx"); // Redirect to an error page if needed
                }
            }
        }

        private void LogError(string message)
        {
            // Implement your error logging mechanism (e.g., write to a file, log table, etc.)
            // Replace with your logging implementation
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
                    Response.Redirect("~/ErrorPage.aspx"); // Redirect to an error page if needed
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
                return "U1001";
            }
        }

        private decimal getTaxRate()
        {
            if (Session["taxRate"] != null)
            {
                return Convert.ToDecimal(Session["taxRate"]);
            }
            else
            {
                return 0.06m;
            }
        }



        protected void cbxShip_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxShip.Checked)
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


        protected Dictionary<string, string[]> Countries { get; set; } = new Dictionary<string, string[]>()
        {
         { "MY", new string[] { "Kuala Lumpur", "Selangor", "Penang", "Perak"} }, // Malaysia
    { "SG", new string[] { "Central", "West", "East", "North" } }, // Singapore
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


