using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Product
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Check if a category filter is provided in the query string
                string category = Request.QueryString["category"];
                if (!string.IsNullOrEmpty(category))
                {
                    // Filter products based on the selected category and its subcategories
                    DataTable filteredData = GetFilteredProductData(category, null, null, "product_name", SortDirection.Ascending);
                    DisplayFilteredData(filteredData);
                }
                else
                {
                    // If no category filter is provided, load all products
                    BindProductData();
                }
            }
        }

        private DataTable GetFilteredProductData(string category, decimal? minPrice, decimal? maxPrice, string sortBy, SortDirection direction)
        {
            DataTable productData = new DataTable();
            List<string> subcategories = new List<string>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
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
                   INNER JOIN Product_Variant pv ON p.product_id = pv.product_id
                   INNER JOIN Category c ON p.category_id = c.category_id
                   WHERE p.product_status = 'Publish'";

                // Add category filter if provided
                if (!string.IsNullOrEmpty(category))
                {
                    sql += " AND (c.category_name = @Category";

                    // Get all subcategories for the selected category
                    subcategories = GetSubcategories(category);

                    // Include all subcategories in the filter
                    if (subcategories.Any())
                    {
                        sql += " OR c.category_name IN (" + string.Join(",", subcategories.Select(s => "@" + s)) + ")";
                    }

                    sql += ")";
                }

                // Add price filter if provided
                if (minPrice != null && maxPrice != null)
                {
                    sql += " AND pv.variant_price BETWEEN @MinPrice AND @MaxPrice";
                }

                sql += " GROUP BY p.product_id, p.product_name, p.product_description, p.date_added";

                // Sort the data
                sql += " ORDER BY " + sortBy + " " + (direction == SortDirection.Ascending ? "ASC" : "DESC");

                SqlCommand cmd = new SqlCommand(sql, conn);

                // Add parameters for category and subcategories
                if (!string.IsNullOrEmpty(category))
                {
                    cmd.Parameters.AddWithValue("@Category", category);
                    foreach (var subcategory in subcategories)
                    {
                        cmd.Parameters.AddWithValue("@" + subcategory, subcategory);
                    }
                }

                cmd.Parameters.AddWithValue("@MinPrice", minPrice ?? decimal.MinValue);
                cmd.Parameters.AddWithValue("@MaxPrice", maxPrice ?? decimal.MaxValue);
                SqlDataReader reader = cmd.ExecuteReader();
                productData.Load(reader);
                reader.Close();
            }
            return productData;
        }

        // Method to get subcategories for a given category
        private List<string> GetSubcategories(string category)
        {
            List<string> subcategories = new List<string>();

            // Add subcategories based on the provided category
            switch (category)
            {
                case "ComputerAccessories":
                    subcategories.AddRange(new string[] { "Mouse", "Keyboard", "Others" });
                    break;
                case "ComputerParts":
                    subcategories.AddRange(new string[] { "Motherboard", "RAM", "Storage", "GPU", "PSU", "CPU", "Case" });
                    break;
            }

            return subcategories;
        }

        protected void FilterByCategory(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string category = btn.CommandArgument;
            DataTable filteredData = GetFilteredProductData(category, null, null, "product_name", SortDirection.Ascending);
            DisplayFilteredData(filteredData);
        }

        private void BindProductData()
        {
            DataTable productData = GetFilteredProductData(null, null, null, "product_name", SortDirection.Ascending);
            DisplayFilteredData(productData);
        }


        protected void ResetButton_Click(object sender, EventArgs e)
        {
            productName.Text = string.Empty; // Reset search input
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string productName = this.productName.Text.Trim();

            DataTable filteredData = GetFilteredProductData(null, null, null, "product_name", SortDirection.Ascending); // Get all products

            if (!string.IsNullOrEmpty(productName))
            {
                // Filter the data based on the entered product name
                filteredData.DefaultView.RowFilter = $"product_name LIKE '%{productName}%'";
                filteredData = filteredData.DefaultView.ToTable();
            }

            DisplayFilteredData(filteredData);
        }

        protected void ApplyPriceFilterButton_Click(object sender, EventArgs e)
        {
            // Check if both minimum and maximum prices are filled
            if (!string.IsNullOrEmpty(minprice.Text) && !string.IsNullOrEmpty(maxprice.Text))
            {
                decimal minPrice = decimal.Parse(minprice.Text);
                decimal maxPrice = decimal.Parse(maxprice.Text);
                DataTable filteredData = GetFilteredProductData(null, minPrice, maxPrice, "product_name", SortDirection.Ascending);
                DisplayFilteredData(filteredData);
            }
            else
            {
                // Display an error message if both minimum and maximum prices are not filled
                ScriptManager.RegisterStartupScript(this, GetType(), "PriceFilterError",
                    "alert('Please fill in both minimum and maximum prices.');", true);
            }

            // Clear the input fields after filtering
            minprice.Text = string.Empty;
            maxprice.Text = string.Empty;
        }


        protected void SortAllProducts_Click(object sender, EventArgs e)
        {
            DataTable sortedData = GetFilteredProductData(null, null, null, "product_name", SortDirection.Ascending);
            DisplayFilteredData(sortedData);
        }

        protected void SortByArrivals_Click(object sender, EventArgs e)
        {
            DataTable sortedData = GetFilteredProductData(null, null, null, "p.date_added", SortDirection.Descending);
            DisplayFilteredData(sortedData);
        }

        protected void SortByPriceLowToHigh_Click(object sender, EventArgs e)
        {
            DataTable sortedData = GetFilteredProductData(null, null, null, "min_price", SortDirection.Ascending);
            DisplayFilteredData(sortedData);
        }

        protected void SortByPriceHighToLow_Click(object sender, EventArgs e)
        {
            DataTable sortedData = GetFilteredProductData(null, null, null, "max_price", SortDirection.Descending);
            DisplayFilteredData(sortedData);
        }
        private enum SortDirection
        {
            Ascending,
            Descending
        }

        private void DisplayFilteredData(DataTable filteredData)
        {
            ListViewProducts.DataSource = filteredData;
            ListViewProducts.DataBind();
            UpdateNoProductsFoundMessageVisibility(filteredData.Rows.Count == 0);
        }

        private void UpdateNoProductsFoundMessageVisibility(bool isVisible)
        {
            noProductsFoundMessage.Style["display"] = isVisible ? "block" : "none";
        }




    }
}