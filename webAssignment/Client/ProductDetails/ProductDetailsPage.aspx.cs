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
using System.Web.UI.WebControls;
using Newtonsoft.Json;

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
                    string productId = Request.QueryString["ProductId"].ToString();
                    PopulateProductDetails(productId);
                    PopulateReviewData(productId);
                    PopulateOverallRatingData(productId);
                }
                else
                {
                    // Handle the case where ProductId is not provided in the query string
                    // You can redirect the user to an error page or display a default product
                }
            }
        }

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
                    imgProduct.ImageUrl = "/Client/Product/Products Images/" + row["product_image"].ToString() + ".png";
                }
            }
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

        protected void VariationButton_Click(object sender, EventArgs e)
        {

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