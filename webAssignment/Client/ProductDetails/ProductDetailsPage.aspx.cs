using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.ProductDetails
{
    public partial class ProductDetailsPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialize product details
                lblProductName.Text = "Corsair RMx Series™ RM850x";
                lblProductPrice.Text = "RM 299.00";
                lblShortProductDesc.Text = "CORSAIR RM850x series fully modular power supplies are built with the highest quality components to deliver 80 PLUS Gold efficient power to your PC, with virtually silent operation.";
                imgProduct.ImageUrl = "/Client/Product/Products Images/Corsair RMx Series RM850x.png";
            }
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            // update cart icon no. & add product to cart
        }

        protected void btnAddToWishlist_Click(object sender, EventArgs e)
        {
            // update wishlist icon no. & add product to wishlist
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Product added to wishlist!');", true);
        }

        protected void StepDown_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(qtyInput.Text);
            if (quantity > 1)
            {
                qtyInput.Text = (quantity - 1).ToString();
            }
        }

        protected void StepUp_Click(object sender, EventArgs e)
        {
            int quantity = int.Parse(qtyInput.Text);
            qtyInput.Text = (quantity + 1).ToString();
        }

    }
}