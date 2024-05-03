using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Profile
{
    public partial class AddAddress : System.Web.UI.Page
    {
        string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string passedString = Request.QueryString["action"];

                lblAddress.Text = passedString;

                // Retrieve the address ID from the query string
                string addressId = Request.QueryString["addressId"];
                if(addressId != null)
                {
                    // Load the address details corresponding to the address ID
                    LoadAddressForEdit(addressId);

                    btnAddAddress.Visible = false;
                    editBtn.Visible = true;
                }
                else
                {
                    btnAddAddress.Visible = true;
                    editBtn.Visible = false;
                }
            }
        }
                
        protected void cancelBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/Profile/Address.aspx");
        }

        protected void btnAddAddress_Click(object sender, EventArgs e)
        {
            if (ValidateAddress())
            {
                if (IsValidZipCode(txtZipCode.Text))
                {
                    if (IsValidPhone(txtPhoneNumber.Text))
                    {
                        string addressId = GenerateAddressId();
                        string userId = Request.Cookies["userInfo"]["userID"];
                        string addressLine1 = txtAddressLine1.Text;
                        string addressLine2 = txtAddressLine2.Text;
                        string addressType = Request.QueryString["addressType"];
                        string selectedValue = ddlCountry.SelectedValue;
                        string[] parts = selectedValue.Split('-');
                        string countryCode = parts[1].Trim();

                        string state = txtState.Text;
                        string city = txtCity.Text;
                        int zipCode = int.Parse(txtZipCode.Text);
                        string firstName = txtFirstName.Text;
                        string lastName = txtLastName.Text;
                        string phoneNumber = txtPhoneNumber.Text;

                        InsertAddress(addressId, userId, addressType, addressLine1, addressLine2, countryCode, state, city, zipCode, firstName, lastName, phoneNumber);

                        Response.Redirect("~/Client/Profile/Address.aspx");
                    }
                    else
                    {
                        lblErrorMsg.Text = "Invalid phone number!";
                    }
                }
                else
                {
                    lblErrorMsg.Text = "Invalid zip code!";
                }
            }
            else
            {
                lblErrorMsg.Text = "Input fields cannot be empty!";
            }
        }              

        private void InsertAddress(string addressId, string userId, string addressType, string addressLine1, string addressLine2, string countryCode, string state, string city, int zipCode, string firstName, string lastName, string phoneNumber)
        {
            //SQL query to insert data into the Address table
            string query = "INSERT INTO [Address] (address_id, user_id, address_type, address_line1, address_line2, countryCode, state, city, zip_code, first_name, last_name, phone_number) " +
                           "VALUES (@AddressId, @UserId, @AddressType, @AddressLine1, @AddressLine2, @CountryCode, @State, @City, @ZipCode, @FirstName, @LastName, @PhoneNumber)";

            // Establish connection and command objects
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@AddressId", addressId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@AddressType", addressType);
                    cmd.Parameters.AddWithValue("@AddressLine1", addressLine1);
                    cmd.Parameters.AddWithValue("@AddressLine2", addressLine2);
                    cmd.Parameters.AddWithValue("@CountryCode", countryCode);
                    cmd.Parameters.AddWithValue("@State", state);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@ZipCode", zipCode);
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);

                    // Open connection and execute query
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void LoadAddressForEdit(string addressId)
        {

            if (string.IsNullOrEmpty(addressId))
            {
                // Handle the case where the addressId is not provided
                return;
            }

            string query = "SELECT * FROM [Address] WHERE address_id = @AddressId";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Create a new SqlParameter object and set its properties
                    SqlParameter addressIdParam = new SqlParameter("@AddressId", SqlDbType.NVarChar);
                    addressIdParam.Value = addressId;

                    // Add the parameter to the SqlCommand object
                    cmd.Parameters.Add(addressIdParam);

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtFirstName.Text = reader["first_name"].ToString();
                        txtLastName.Text = reader["last_name"].ToString();
                        txtAddressLine1.Text = reader["address_line1"].ToString();
                        txtAddressLine2.Text = reader["address_line2"].ToString();
                        txtCity.Text = reader["city"].ToString();
                        txtState.Text = reader["state"].ToString();
                        txtZipCode.Text = reader["zip_code"].ToString();
                        txtPhoneNumber.Text = reader["phone_number"].ToString();

                        string countryCode = reader["countryCode"].ToString();

                        // Iterate through each ListItem in the DropDownList
                        foreach (ListItem item in ddlCountry.Items)
                        {
                            // Split the text of the ListItem to extract the country code
                            string[] parts = item.Text.Split('-');
                            string itemCountryCode = parts[1].Trim();

                            // Check if the country code of the current ListItem matches the retrieved countryCode
                            if (itemCountryCode.Equals(countryCode))
                            {
                                // If a match is found, set the Selected property of the ListItem to true
                                item.Selected = true;
                                break; // Exit the loop since the correct item has been selected
                            }
                        }

                    }
                    reader.Close();
                }
            }
        }

        protected void btnEditAddress_Click(object sender, EventArgs e)
        {
            // Retrieve the address ID from the query string
            string addressId = Request.QueryString["addressId"];

            if (!string.IsNullOrEmpty(addressId))
            {
                if (ValidateAddress())
                {
                    if (IsValidZipCode(txtZipCode.Text))
                    {
                        if (IsValidPhone(txtPhoneNumber.Text))
                        {
                            string firstName = txtFirstName.Text;
                            string lastName = txtLastName.Text;
                            string addressLine1 = txtAddressLine1.Text;
                            string addressLine2 = txtAddressLine2.Text;
                            string selectedValue = ddlCountry.SelectedValue;
                            string[] parts = selectedValue.Split('-');
                            string countryCode = parts[1].Trim();
                            string city = txtCity.Text;
                            string state = txtState.Text;
                            int zipCode = int.Parse(txtZipCode.Text);
                            string phoneNumber = txtPhoneNumber.Text;

                            UpdateAddress(addressId, firstName, lastName, addressLine1, addressLine2, countryCode, city, state, zipCode, phoneNumber);

                            Response.Redirect("~/Client/Profile/Address.aspx");
                        }
                        else
                        {
                            lblErrorMsg.Text = "Invalid phone number!";
                        }
                    }
                    else
                    {
                        lblErrorMsg.Text = "Invalid zip code!";
                    }
                }
                else
                {
                    lblErrorMsg.Text = "Input fields cannot be empty!";
                }
            }            
        }

        private void UpdateAddress(string addressId, string firstName, string lastName, string addressLine1, string addressLine2, string countryCode, string city, string state, int zipCode, string phoneNumber)
        {
            //SQL query to update the address details
            string query = "UPDATE [Address] SET first_name = @FirstName, last_name = @LastName, address_line1 = @AddressLine1, address_line2 = @AddressLine2, countryCode = @CountryCode, city = @City, state = @State, zip_code = @ZipCode, phone_number = @PhoneNumber WHERE address_id = @AddressId";

            // Establish connection and command objects
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Add parameters
                    cmd.Parameters.AddWithValue("@FirstName", firstName);
                    cmd.Parameters.AddWithValue("@LastName", lastName);
                    cmd.Parameters.AddWithValue("@AddressLine1", addressLine1);
                    cmd.Parameters.AddWithValue("@AddressLine2", addressLine2);
                    cmd.Parameters.AddWithValue("@CountryCode", countryCode);
                    cmd.Parameters.AddWithValue("@City", city);
                    cmd.Parameters.AddWithValue("@State", state);
                    cmd.Parameters.AddWithValue("@ZipCode", zipCode);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@AddressId", addressId);

                    // Open connection and execute query
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();

                    if (rowsAffected > 0)
                    {
                        // Address details updated successfully
                        // You can redirect the user to a success page or display a message here
                    }
                    else
                    {
                        // Failed to update address details
                        // You can handle this scenario based on your application's requirements
                    }
                }
            }
        }
                
        private string GenerateAddressId()
        {
            // Your SQL query to retrieve the maximum address ID
            string maxIdQuery = "SELECT MAX(address_id) FROM [Address]";

            // Initialize the new address ID
            string newAddressId = "ADDR00001";

            // Establish connection and command objects
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(maxIdQuery, conn))
                {
                    // Open connection and execute query to retrieve the maximum address ID
                    conn.Open();
                    object result = cmd.ExecuteScalar();

                    // Check if the result is not null
                    if (result != DBNull.Value)
                    {
                        // If the result is not null, parse it to get the maximum address ID and increment it by 1
                        int maxAddressId = Convert.ToInt32(result.ToString().Substring(4)); // Extract the numeric part of the address ID
                        int nextAddressId = maxAddressId + 1;
                        newAddressId = "ADDR" + nextAddressId.ToString("D5"); // Format the new address ID with leading zeros
                    }
                }
            }
            return newAddressId;
        }

        private bool ValidateAddress()
        {
            bool isFirstNameValid = !string.IsNullOrEmpty(txtFirstName.Text);
            bool isLastNameValid = !string.IsNullOrEmpty(txtLastName.Text);
            bool isLine1Valid = !string.IsNullOrEmpty(txtAddressLine1.Text);
            bool isLine2Valid = !string.IsNullOrEmpty(txtAddressLine2.Text);
            bool isPhoneNoValid = !string.IsNullOrEmpty(txtPhoneNumber.Text);
            bool isStateValid = !string.IsNullOrEmpty(txtState.Text);
            bool isCityValid = !string.IsNullOrEmpty(txtCity.Text);
            bool isZipCodeValid = !string.IsNullOrEmpty(txtZipCode.Text);

            txtFirstName.Style["border-color"] = isFirstNameValid ? string.Empty : "#EF4444";
            txtLastName.Style["border-color"] = isLastNameValid ? string.Empty : "#EF4444";
            txtAddressLine1.Style["border-color"] = isLine1Valid ? string.Empty : "#EF4444";
            txtAddressLine2.Style["border-color"] = isLine2Valid ? string.Empty : "#EF4444";
            txtPhoneNumber.Style["border-color"] = isPhoneNoValid ? string.Empty : "#EF4444";
            txtState.Style["border-color"] = isStateValid ? string.Empty : "#EF4444";
            txtCity.Style["border-color"] = isCityValid ? string.Empty : "#EF4444";
            txtZipCode.Style["border-color"] = isZipCodeValid ? string.Empty : "#EF4444";

            return isFirstNameValid && isLastNameValid && isLine1Valid && isLine2Valid && isPhoneNoValid && isStateValid && isCityValid && isZipCodeValid;
        }

        private bool IsValidPhone(string phoneNumber)
        {
            string pattern = @"^\d{10,15}$";

            Regex regex = new Regex(pattern);

            return regex.IsMatch(phoneNumber);
        }

        public bool IsValidZipCode(string zipCode)
        {
            // Regular expression pattern for 5-digit zip codes
            string pattern = @"^\d{5}$";

            // Create a Regex object with the pattern
            Regex regex = new Regex(pattern);

            // Use the IsMatch method to check if the zip code matches the pattern
            return regex.IsMatch(zipCode);
        }

    }
}