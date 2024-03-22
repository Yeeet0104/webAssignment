using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
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
        private decimal discountRate = 0.00m;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (isCartEmpty())
                {
                    cartEmptyMsg.Attributes["class"] = "block flex justify-center items-center h-20 text-gray-700 text-xl font-semibold ";
                }
                updateCartDetails();
            }
        }

        private void updateCartDetails()
        {
            lvCartProduct.DataSource = GetDummyData();
            lvCartProduct.DataBind();

            decimal cartSubtotal = calculateCartSubtotal(GetDummyData());
            lblCartSubtotal.Text = cartSubtotal.ToString("C");
            decimal tax = calculateTax(cartSubtotal);
            lblCartTax.Text = tax.ToString("C");
            decimal discount = calculateDiscount(cartSubtotal);
            lblCartDiscount.Text = "(-" + (int)(discountRate * 100) + "%) " + discount.ToString("C");
            lblCartTotal.Text = (cartSubtotal + tax - discount).ToString("C");
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
            // Add more rows as needed for testing

            return dummyData;
        }

        private bool isCartEmpty()
        {
            return GetDummyData().Rows.Count == 0;
        }

        private decimal calculateTax(decimal cartSubtotal)
        {
            return cartSubtotal * taxRate;
        }

        private decimal calculateDiscount(decimal cartSubtotal)
        {
            return cartSubtotal * discountRate;
        }

        private decimal calculateCartSubtotal(DataTable dum)
        {
            if (dum != null)
            {
                decimal cartTotal = 0.0m;

                foreach (DataRow row in dum.Rows)
                {
                    // Retrieve the subtotal for the current row
                    decimal subtotal = Convert.ToDecimal(row["Subtotal"]);
                    // Add the subtotal to the cart total
                    cartTotal += subtotal;
                }
                return cartTotal;
            }
            return 0;
        }

        protected string CalculateSubtotal(object price, object quantity)
        {
            if (price != null && quantity != null)
            {
                int qty = Convert.ToInt32(quantity);
                decimal priceValue = Convert.ToDecimal(price);
                decimal subtotal = qty * priceValue;
                return subtotal.ToString("C");
            }
            return "N/A";
        }

        protected void btnApplyCoupon_Click(object sender, EventArgs e)
        {
            if (txtCoupon.Text == "1234")
            {
                discountRate = 0.5m;
                updateCartDetails();
                txtCoupon.Text = "";
                txtCoupon.Attributes["placeholder"] = "Coupon Applied";
            }
            else
            {
                discountRate = 0.0m;
                updateCartDetails();
                txtCoupon.Text = "";
                txtCoupon.Attributes["placeholder"] = "Invalid Code";
            }

        }

        protected void btnProceedCheckout_Click(object sender, EventArgs e)
        {
            if (isCartEmpty())
            {
                lblMessage.Text = "Please add items before proceeding to checkout.";
            }
            else
            {
                Response.Redirect("~/Client/Checkout/CheckoutPage.aspx");
            }
        }

    }
}