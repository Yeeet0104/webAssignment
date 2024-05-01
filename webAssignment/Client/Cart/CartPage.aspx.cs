using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Cart
{
    public partial class CartPage : System.Web.UI.Page
    {
        private decimal taxRate = 0.06m;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["UserId"] = "U1001";
                Session["taxRate"] = taxRate;
                Session["Voucher"] = "";

                getData();
                updateCartTotal();
            }
        }



        private void getData()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

            string updateCartQuery =
                "SELECT pv.product_variant_id AS variantID, p.product_name AS productName, pv.variant_name AS variantName, cd.quantity, pv.variant_price AS price, (Select TOP 1 path FROM Image_Path imgp WHERE imgp.product_id = p.product_id ORDER BY imgp.image_path_id ASC) AS imagePath FROM Cart_Details cd INNER JOIN Cart c ON cd.cart_id = c.cart_id INNER JOIN Product_Variant pv ON cd.product_variant_id = pv.product_variant_id INNER JOIN Product p ON pv.product_id = p.product_id WHERE c.user_id = @userId;";


            String userId = GetCurrentUserId(); // Replace with your logic to get the current user ID

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(updateCartQuery, connection))
                    {
                        command.Parameters.AddWithValue("@userId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                // Bind data to your GridView, Repeater, or other control
                                // (Replace with your specific control binding logic)
                                lvCartProduct.DataSource = reader;
                                lvCartProduct.DataBind();

                                cartEmptyMsg.Attributes["class"] = "hidden"; // Hide empty cart message
                                txtCoupon.ReadOnly = false;
                                btnProceedCheckout.Enabled = true;
                            }
                            else
                            {
                                cartEmptyMsg.Attributes["class"] = "block"; // Show empty cart message
                                txtCoupon.ReadOnly = true;
                                btnProceedCheckout.Enabled = false;
                                return;
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

        private String GetCurrentUserId()
        {
            if (Session["UserId"] != null)
            {
                return Convert.ToString(Session["UserId"]);
            }
            else
            {
                return "";
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

            String userId = GetCurrentUserId(); // Replace with your logic to get the current user ID

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
                                decimal discountRate = checkDiscountRate(txtCoupon.Text, command, connection);
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

                                //updateCartTotal(Convert.ToDecimal(reader[0]), command, connection);
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


        protected void btnApplyCoupon_Click(object sender, EventArgs e)
        {
            updateCartTotal();
        }

        protected void btnProceedCheckout_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Client/Checkout/CheckoutPage.aspx");
        }

        protected void cartListView_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            // Check if the user click the reduce btn or the increase btn
            if (e.CommandName == "reduceQty")
            {
                // Get the variant ID
                String productVariantID = e.CommandArgument.ToString();
                String userID = GetCurrentUserId();

                // Connect database
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                string reduceQtyQuery =
                    "SELECT cd.quantity, pv.stock FROM Product_Variant pv INNER JOIN Cart_Details cd ON cd.product_variant_id = pv.product_variant_id INNER JOIN Cart c ON c.cart_id = cd.cart_id WHERE pv.product_variant_id = @pvID AND c.user_id = @userId;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(reduceQtyQuery, connection))
                        {
                            command.Parameters.AddWithValue("@pvID", productVariantID);
                            command.Parameters.AddWithValue("@userId", userID);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    // Get the stock and check if the cart qty > than the stock
                                    if (Convert.ToInt32(reader[0]) > 1)
                                    {
                                        updateCartQuantity(userID, productVariantID, Convert.ToInt32(reader[0]) - 1);
                                        getData();
                                        updateCartTotal();
                                    }


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
            else if (e.CommandName == "increaseQty")
            {
                // Get the variant ID
                String productVariantID = e.CommandArgument.ToString();
                String userID = GetCurrentUserId();

                // Connect database
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                string increaseQtyQuery =
                    "SELECT cd.quantity, pv.stock FROM Product_Variant pv INNER JOIN Cart_Details cd ON cd.product_variant_id = pv.product_variant_id INNER JOIN Cart c ON c.cart_id = cd.cart_id WHERE pv.product_variant_id = @pvID AND c.user_id = @userId;";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(increaseQtyQuery, connection))
                        {
                            command.Parameters.AddWithValue("@pvID", productVariantID);
                            command.Parameters.AddWithValue("@userId", userID);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    // Get the stock and check if the cart qty > than the stock
                                    if (Convert.ToInt32(reader[0]) < Convert.ToInt32(reader[1]))
                                    {
                                        updateCartQuantity(userID, productVariantID, Convert.ToInt32(reader[0]) + 1);
                                        getData();
                                        updateCartTotal();
                                    }


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
            else if (e.CommandName == "deleteItem")
            {
                // Get the variant ID
                String productVariantID = e.CommandArgument.ToString();
                String userID = GetCurrentUserId();

                // Connect database
                string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

                string deleteItemQuery =
                    "DELETE FROM Cart_Details  WHERE product_variant_id = @pvID AND cart_id = (SELECT cart_id FROM cart WHERE user_id = @userId);";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(deleteItemQuery, connection))
                        {
                            command.Parameters.AddWithValue("@pvID", productVariantID);
                            command.Parameters.AddWithValue("@userId", userID);

                            command.ExecuteNonQuery();

                            getData();
                            updateCartTotal();
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
        }

        private void updateCartQuantity(String userID, String pvID, int newQty)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string updateQuery = "UPDATE Cart_Details SET quantity = @newQty WHERE product_variant_id = @pvID AND cart_id = (SELECT cart_id FROM Cart WHERE user_id = @userID);";

                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {

                    command.Parameters.AddWithValue("@newQty", newQty);
                    command.Parameters.AddWithValue("@pvID", pvID);
                    command.Parameters.AddWithValue("userID", userID);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}