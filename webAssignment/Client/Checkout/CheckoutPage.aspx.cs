using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                lvCheckoutProduct.DataSource = GetDummyData();
                lvCheckoutProduct.DataBind();
                decimal cartSubtotal = calculateCartSubtotal(GetDummyData());
                lblCartSubtotal.Text = cartSubtotal.ToString("C");
                decimal tax = calculateTax(cartSubtotal);
                lblCartTax.Text = tax.ToString("C");
                decimal discount = calculateDiscount(cartSubtotal);
                lblCartDiscount.Text = "(-" + (int)(discountRate * 100) + "%) " + discount.ToString("C");
                lblCartTotal.Text = (cartSubtotal + tax - discount).ToString("C");
            }
        }

        DataTable GetDummyData()
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

        private void updateCartDetails()
        {
            lvCheckoutProduct.DataSource = GetDummyData();
            lvCheckoutProduct.DataBind();

            decimal cartSubtotal = calculateCartSubtotal(GetDummyData());
            lblCartSubtotal.Text = cartSubtotal.ToString("C");
            decimal tax = calculateTax(cartSubtotal);
            lblCartTax.Text = tax.ToString("C");
            decimal discount = calculateDiscount(cartSubtotal);
            lblCartDiscount.Text = "(-" + (int)(discountRate * 100) + "%) " + discount.ToString("C");
            lblCartTotal.Text = (cartSubtotal + tax - discount).ToString("C");
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
    }
}