using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using webAssignment.Client.Cart;
using webAssignment.Client.ClientMasterPage;

namespace webAssignment.Client.ProductDetails
{
    public partial class ProductDetailsPage : System.Web.UI.Page
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["ProductId"] != null)
                {
                    Session["UserId"] = "CS1001";
                    string productId = Request.QueryString["ProductId"].ToString();
                    PopulateProductDetails(productId);
                    PopulateReviewData(productId);
                    PopulateOverallRatingData(productId);
                    PopulateVariantDetails(productId);
                }
                else
                {
                    ShowNotification("ProductId is missing in the query string.", "warning");
                }
            }

            // Check if there is a success message in the session
            if (Session["reviewLikeDislike"] != null)
            {
                string successMessage = Session["reviewLikeDislike"].ToString();
                ShowNotification(successMessage, "success");

                // Clear the session variable after displaying the notification
                Session.Remove("reviewLikeDislike");
            }
        }

        //Display Product Details
        private DataTable GetProductData(string productId)
        {
            DataTable productData = new DataTable();
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            string sql = @"SELECT p.product_id, p.product_name, p.product_description,
                          MIN(pv.variant_price) AS min_price,
                          MAX(pv.variant_price) AS max_price,
                          (
                              SELECT TOP 1 path
                              FROM Image_Path ip
                              WHERE ip.product_id = p.product_id
                              ORDER BY image_path_id
                          ) AS product_image
                   FROM Product p
                   LEFT JOIN Product_Variant pv ON p.product_id = pv.product_id
                   WHERE p.product_id = @ProductId
                   GROUP BY p.product_id, p.product_name, p.product_description";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@ProductId", productId);
            SqlDataReader reader = cmd.ExecuteReader();
            productData.Load(reader);
            reader.Close();
            conn.Close();
            return productData;
        }
        private void PopulateProductDetails(string productId)
        {
            ddlProdVariant.Items.Clear();
            DataTable productData = GetProductData(productId);
            if (productData.Rows.Count > 0)
            {
                DataRow row = productData.Rows[0];
                lblProductName.Text = row["product_name"].ToString();
                lblProductPrice.Text = string.Format("RM {0:N2} - {1:N2}", row["min_price"], row["max_price"]);

                string descriptionJson = row["product_description"].ToString();
                string[] descriptionsArray = JsonConvert.DeserializeObject<string[]>(descriptionJson);
                if (descriptionsArray != null && descriptionsArray.Length > 0)
                {

                    string firstDescription = descriptionsArray[0];
                    lblShortProductDesc1.Text = firstDescription;

                    if (descriptionsArray.Length >= 2)
                    {
                        lblShortProductDesc2.Text = descriptionsArray[1]; // Second description
                    }

                    if (descriptionsArray.Length >= 3)
                    {
                        lblShortProductDesc3.Text = descriptionsArray[2]; // Third description
                    }
                }

                if (!string.IsNullOrEmpty(row["product_image"].ToString()))
                {
                    imgProduct.ImageUrl = row["product_image"].ToString();
                }

            }
        }
        private void initProductVariant( string productId )
        {
            DataTable prodVar = GetProductVariantData(productId);

            // Clear existing items
            ddlProdVariant.Items.Clear();


            // Check if categories were fetched successfully
            if ( prodVar != null )
            {
                ddlProdVariant.DataSource = prodVar;
                ddlProdVariant.DataTextField = "variant_name";
                ddlProdVariant.DataValueField = "product_variant_id";
                ddlProdVariant.DataBind();
            }
            ddlProdVariant.Items.Insert(0, new ListItem("Select a variant", ""));

        }
        private DataTable GetProductVariantData(string productId)
        {
            DataTable variantData = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT product_variant_id, variant_name
                       FROM Product_Variant
                       WHERE product_id = @ProductId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@ProductId", productId);
                SqlDataReader reader = cmd.ExecuteReader();
                variantData.Load(reader);
            }
            return variantData;
        }
        private void PopulateVariantDetails(string productId)
        {
            DataTable variantData = GetProductVariantData(productId);
            initProductVariant(productId);
            //if (variantData.Rows.Count > 0)
            //{
            //    Button[] buttons = { btnVariation1, btnVariation2, btnVariation3 }; // Add more buttons as needed

            //    for (int i = 0; i < variantData.Rows.Count && i < buttons.Length; i++)
            //    {
            //        buttons[i].Text = variantData.Rows[i]["variant_name"].ToString();
            //        buttons[i].Attributes["data-variant-id"] = variantData.Rows[i]["product_variant_id"].ToString();
            //    }
            //}
        }
        private DataTable GetReviewData(string productId)
        {
            DataTable reviewData = new DataTable();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"SELECT r.review_id AS ReviewId, u.username AS ReviewerName, r.date_reviewed AS ReviewDate, 
                    r.rating AS Rating, r.comment AS Comment, r.post_like AS Likes, r.post_dislike AS Dislikes
               FROM Review r
               INNER JOIN [User] u ON r.user_id = u.user_id
               INNER JOIN Product_Variant pv ON r.product_variant_id = pv.product_variant_id
               WHERE pv.product_id = @ProductId";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    reviewData.Load(reader);
                }
            }
            return reviewData;
        }
        private void PopulateReviewData(string productId)
        {
            DataTable reviewData = GetReviewData(productId);
            rptReviews.DataSource = reviewData;
            rptReviews.DataBind();
        }
        protected string GetStarRating(int rating)
        {
            StringBuilder starsHtml = new StringBuilder();

            // Loop to generate stars based on rating
            for (int i = 0; i < 5; i++)
            {
                // Check if current star should be filled or empty based on the rating
                if (i < rating)
                {
                    // Add filled star icon
                    starsHtml.Append("<i class=\"fas fa-star\"></i>");
                }
                else
                {
                    // Add empty star icon
                    starsHtml.Append("<i class=\"far fa-star\"></i>");
                }
            }

            return starsHtml.ToString();
        }
        protected void PopulateOverallRatingData(string productId)
        {
            DataTable reviewData = GetReviewData(productId);

            if (reviewData.Rows.Count > 0)
            {
                int totalRatings = reviewData.Rows.Count;
                int totalRatingSum = 0;
                int[] starCounts = new int[5];

                foreach (DataRow row in reviewData.Rows)
                {
                    int rating = Convert.ToInt32(row["Rating"]);
                    totalRatingSum += rating;

                    // Count each star rating
                    starCounts[rating - 1]++;
                }

                // Calculate overall rating
                double overallRating = (double)totalRatingSum / totalRatings;

                // Calculate percentage for each star rating
                double[] starPercentages = new double[5];
                for (int i = 0; i < 5; i++)
                {
                    starPercentages[i] = (double)starCounts[i] / totalRatings * 100;
                    Debug.WriteLine($"Star {i + 1} Count: {starCounts[i]}, Percentage: {starPercentages[i]}%");
                    starlbl1.Style.Add("width", starPercentages[4].ToString() + "%");
                    starlbl2.Style.Add("width", starPercentages[3].ToString() + "%");
                    starlbl3.Style.Add("width", starPercentages[2].ToString() + "%");
                    starlbl4.Style.Add("width", starPercentages[1].ToString() + "%");
                    starlbl5.Style.Add("width", starPercentages[0].ToString() + "%");
                }

                // Set overall rating label
                lblOverallRating.Text = overallRating.ToString("0.0");

                // Set total ratings label
                lblTotalRatings.Text = totalRatings.ToString();
                divStarRating.InnerHtml = GetStarRating((int)overallRating);

                Label[] countLabels = {
                    Label1StarCount,
                    Label2StarCount,
                    Label3StarCount,
                    Label4StarCount,
                    Label5StarCount
                };

                Label[] percentageLabels = {
                    Label1StarPercentage,
                    Label2StarPercentage,
                    Label3StarPercentage,
                    Label4StarPercentage,
                    Label5StarPercentage
                };

                for (int i = 0; i < 5; i++)
                {
                    // Set text for count and percentage labels
                    countLabels[i].Text = starCounts[i].ToString();
                    percentageLabels[i].Text = starPercentages[i].ToString("0.0") + "%";

                }
            }
        }

        //Update Product Price when Variation is selected
        protected void UpdatePrice(object sender, EventArgs e)
        {
            Button variationButton = (Button)sender;
            string variantId = variationButton.Attributes["data-variant-id"];
            decimal price = GetPriceForVariant(variantId);
            lblProductPrice.Text = string.Format("RM {0:N2}", price);

            // Update the hidden field value with the selected variant ID
            selectedVariation.Value = variantId;

            // Remove the selected class from all variation buttons
            //btnVariation1.CssClass = btnVariation1.CssClass.Replace("selected", "").Trim();
            //btnVariation2.CssClass = btnVariation2.CssClass.Replace("selected", "").Trim();
            //btnVariation3.CssClass = btnVariation3.CssClass.Replace("selected", "").Trim();

            // Add the selected class to the clicked button
            variationButton.CssClass += " selected";
        }
        private decimal GetPriceForVariant(string variantId)
        {
            decimal price = 0.00m;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Add debug statement for SQL query
                string sql = @"SELECT variant_price FROM Product_Variant WHERE product_variant_id = @VariantId";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@VariantId", variantId);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    price = Convert.ToDecimal(result);
                }
            }

            return price;
        }
        protected void ddlProdVariant_SelectedIndexChanged(object sender, EventArgs e)
        {
            decimal price = GetPriceForVariant(ddlProdVariant.SelectedValue.ToString());
            lblProductPrice.Text = string.Format("RM {0:N2}", price);
        }

        //Add to Cart Function
        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            // Check if the user is logged in
            if (Session["UserId"] == null)
            {
                // Redirect the user to the login page or show a message to log in
                Response.Redirect("/Client/LoginSignUp/SignUp.aspx");
                return;
            }

            // Check if a variation is selected
            if (string.IsNullOrEmpty(ddlProdVariant.SelectedValue))
            {
                // Show a notification to select a variation
                ShowNotification("Please select a variation before adding to cart.", "warning");
                return;
            }

            // Get user ID, variation, qty
            string userId = Session["UserId"].ToString();
            string productVariantId = ddlProdVariant.SelectedItem.Value.ToString();
            int quantity = int.Parse(qtyInput.Text);

            // Generate cart ID 
            string cartId = GenerateCartId();

            // Add the cart to the database
            bool cartAdded = AddToCart(cartId, userId);

            // Add the product details to the cart details table if cart addition is successful
            if (cartAdded)
            {
                bool detailsAdded = AddToCartDetails(cartId, productVariantId, quantity);
                if (detailsAdded)
                {
                    // Show success message
                    ShowNotification("Product added to cart successfully!", "success");
                    Response.Redirect(Request.RawUrl);
                }
                else
                {
                    // Show error message
                    ShowNotification("Failed to add product to cart details.", "warning");
                }
            }
            else
            {
                // Show error message
                ShowNotification("Failed to add product to cart.", "warning");
            }
        }
        private string GenerateCartId()
        {
            string cartIdPrefix = "C";
            int nextId = 1;

            // Get the latest cart ID from the database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT TOP 1 cart_id FROM Cart ORDER BY cart_id DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    // Extract the numeric part of the latest cart ID and increment it
                    string lastCartId = result.ToString();
                    string numericPart = lastCartId.Substring(1); // Remove the prefix
                    nextId = int.Parse(numericPart) + 1;
                }
            }

            // Format the next cart ID with leading zeros
            string formattedId = cartIdPrefix + nextId.ToString("D4");
            return formattedId;
        }
        private bool AddToCart(string cartId, string userId)
        {
            bool success = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"INSERT INTO Cart (cart_id, user_id) VALUES (@CartId, @UserId)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@CartId", cartId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    success = (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or handle it as needed
                success = false;
            }
            return success;
        }
        private bool AddToCartDetails(string cartId, string productVariantId, int quantity)
        {
            bool success = false;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = @"INSERT INTO Cart_Details (cart_id, product_variant_id, quantity) 
                  VALUES (@CartId, @ProductVariantId, @Quantity)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@CartId", cartId);
                    cmd.Parameters.AddWithValue("@ProductVariantId", productVariantId);
                    cmd.Parameters.AddWithValue("@Quantity", quantity);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    success = (rowsAffected > 0);
                }
            }
            catch (SqlException ex)
            {

                success = false;
            }
            return success;
        }

        //Like & Dislikes Review Function
        protected void btnLike_Click(object sender, EventArgs e)
        {
            try
            {
                string reviewId = ((System.Web.UI.WebControls.LinkButton)sender).CommandArgument;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sqlQuery = "UPDATE Review SET post_like = post_like + 1 WHERE review_id = @ReviewId";
                    SqlCommand command = new SqlCommand(sqlQuery, conn);
                    command.Parameters.AddWithValue("@ReviewId", reviewId);

                    conn.Open();
                    command.ExecuteNonQuery();
                }

                Session["reviewLikeDislike"] = "Like customer review successfully.";
                // Redirect to the raw URL
                Response.Redirect(Request.RawUrl);

            }
            catch (Exception ex)
            {
                // Show error notification
                ShowNotification("Unable to like the customer review.", "warning");
                // Log the exception or handle it accordingly
                Console.WriteLine(ex.Message);
            }
        }
        protected void btnDislike_Click(object sender, EventArgs e)
        {
            try
            {
                string reviewId = ((System.Web.UI.WebControls.LinkButton)sender).CommandArgument;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string sqlQuery = "UPDATE Review SET post_dislike = post_dislike + 1 WHERE review_id = @ReviewId";
                    SqlCommand command = new SqlCommand(sqlQuery, conn);
                    command.Parameters.AddWithValue("@ReviewId", reviewId);

                    conn.Open();
                    command.ExecuteNonQuery();
                }

                Session["reviewLikeDislike"] = "Dislike customer review successfully.";
                // Redirect to the raw URL
                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                // Show error notification
                ShowNotification("Unable to dislike the customer review..", "warningy");
                // Log the exception or handle it accordingly
                Console.WriteLine(ex.Message);
            }
        }


        // Add to Wishlist Function
        protected void btnAddToWishlist_Click(object sender, EventArgs e)
        {
            // Check if the user is logged in, if no then redirect the user to the login page
            if (Session["UserId"] == null)
            {
                Response.Redirect("/Client/LoginSignUp/SignUp.aspx");
                return;
            }

            // Check if a variation is selected, if no show notification and return
            if (string.IsNullOrEmpty(ddlProdVariant.SelectedValue))
            {
                ShowNotification("Please select a variation before adding to wishlist.", "warning");
                return;
            }

            // Get user ID, product variant ID
            string userId = Session["UserId"].ToString();
            string productVariantId = ddlProdVariant.SelectedItem.Value.ToString();

            // Get/create wishlist ID for the user
            string wishlistId = GetOrCreateWishlistId(userId);

            // Add the product variant to the wishlist details
            bool addedToWishlist = AddToWishlistDetails(wishlistId, productVariantId);

            // Show notification based on whether the product was successfully added to the wishlist
            if (addedToWishlist)
            {
                ShowNotification("Product added to wishlist successfully!", "success");
            }
        }
        private string GetOrCreateWishlistId(string userId)
        {
            string wishlistId = "";

            try
            {
                // Check if the user already has a wishlist
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT wishlist_id FROM Wishlist WHERE user_id = @UserId";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    object result = cmd.ExecuteScalar();

                    // If a wishlist exists, retrieve the wishlist ID
                    if (result != null && result != DBNull.Value)
                    {
                        wishlistId = result.ToString();
                    }
                    else
                    {
                        // If no wishlist exists, create a new wishlist for the user
                        wishlistId = CreateNewWishlist(userId);
                    }
                }
            }
            catch (SqlException ex)
            {
                ShowNotification(ex.Message, "warning");
            }

            return wishlistId;
        }
        private string CreateNewWishlist(string userId)
        {
            string wishlistId = "";

            try
            {
                // Generate a new wishlist ID
                wishlistId = GenerateWishlistId();

                // Insert the new wishlist ID into the database for the user
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "INSERT INTO Wishlist (wishlist_id, user_id) VALUES (@WishlistId, @UserId)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@WishlistId", wishlistId);
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or handle it as needed
            }

            return wishlistId;
        }
        private string GenerateWishlistId()
        {
            string wishlistIdPrefix = "WISH";
            int nextId = 1;

            try
            {
                // Get the latest wishlist ID from the database
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sql = "SELECT TOP 1 wishlist_id FROM Wishlist ORDER BY wishlist_id DESC";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    object result = cmd.ExecuteScalar();

                    // If a wishlist ID exists, extract the numeric part and increment it
                    if (result != null && result != DBNull.Value)
                    {
                        string lastWishlistId = result.ToString();
                        string numericPart = lastWishlistId.Substring(wishlistIdPrefix.Length); // Remove the prefix
                        nextId = int.Parse(numericPart) + 1;
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the exception or handle it as needed
            }

            // Format the next wishlist ID with leading zeros
            string formattedId = wishlistIdPrefix + nextId.ToString("D4");
            return formattedId;
        }
        private bool AddToWishlistDetails(string wishlistId, string productVariantId)
        {
            bool success = false;

            try
            {
                // Check if the product variant already exists in the wishlist details table
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string checkExistingSql = "SELECT COUNT(*) FROM Wishlist_details WHERE wishlist_id = @WishlistId AND product_variant_id = @ProductVariantId";
                    SqlCommand checkCmd = new SqlCommand(checkExistingSql, conn);
                    checkCmd.Parameters.AddWithValue("@WishlistId", wishlistId);
                    checkCmd.Parameters.AddWithValue("@ProductVariantId", productVariantId);
                    int existingCount = (int)checkCmd.ExecuteScalar();

                    if (existingCount > 0)
                    {
                        // Product variant already exists in the wishlist
                        ShowNotification("Product already exists in the wishlist.", "warning");
                    }
                    else
                    {
                        // Add the product variant to the wishlist details table
                        string insertSql = "INSERT INTO Wishlist_details (wishlist_id, product_variant_id, date_added) VALUES (@WishlistId, @ProductVariantId, GETDATE())";
                        SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                        insertCmd.Parameters.AddWithValue("@WishlistId", wishlistId);
                        insertCmd.Parameters.AddWithValue("@ProductVariantId", productVariantId);
                        int rowsAffected = insertCmd.ExecuteNonQuery();
                        success = (rowsAffected > 0);
                    }
                }
            }
            catch (SqlException ex)
            {
                ShowNotification(ex.Message, "warning");
                Debug.WriteLine(ex.Message);
            }

            return success;
        }


        //Product Quantity Step Up & Down
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

        //Show Snakbar Function
        protected void ShowNotification(string message, string type)
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }


    }
}