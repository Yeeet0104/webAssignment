using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Client.Product
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }
        }

        private void BindData()
        {
            DataTable dummyData = GetDummyData();
            ListViewProducts.DataSource = dummyData;
            ListViewProducts.DataBind();
        }

        private DataTable GetDummyData()
        {
            DataTable dummyData = new DataTable();

            // Add columns to match ListView's ItemTemplate
            dummyData.Columns.Add("ProductId", typeof(int)); // Add ProductId column
            dummyData.Columns.Add("ProductName", typeof(string));
            dummyData.Columns.Add("Price", typeof(decimal));
            dummyData.Columns.Add("ProductType", typeof(string));
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("Link", typeof(string));
            dummyData.Columns.Add("Sold", typeof(int));

            // Add rows with dummy data
            dummyData.Rows.Add(1, "CORSAIR ONE i160 Compact Gaming PC", 13999.00m, "others", "CORSAIR ONE i160 Compact Gaming PC.png", "ProductDetailsPage.aspx?ProductId=1", 939);
            dummyData.Rows.Add(2, "Logitech G PRO X Gaming Keyboard", 649.00m, "others", "Logitech G PRO X Gaming Keyboard.png", "ProductDetailsPage.aspx?ProductId=2", 1036);
            dummyData.Rows.Add(3, "G502 Hero High Performance Gaming Mouse", 399.00m, "others", "G502 Hero High Performance Gaming Mouse.png", "ProductDetailsPage.aspx?ProductId=3", 3902);
            dummyData.Rows.Add(4, "G560 Lightsync PC Gaming Speakers", 329.00m, "others", "G560 Lightsync PC Gaming Speakers.png", "ProductDetailsPage.aspx?ProductId=4", 919);
            dummyData.Rows.Add(5, "NZXT H710i PC case", 789.99m, "case", "NZXT H710i PC case.png", "ProductDetailsPage.aspx?ProductId=5", 452);
            dummyData.Rows.Add(6, "Gigabyte Z490M Micro-ATX", 599.00m, "motherboard", "Gigabyte Z490M Micro-ATX.png", "ProductDetailsPage.aspx?ProductId=6", 302);
            dummyData.Rows.Add(7, "MSI MPG Z590 ATX", 1299.00m, "motherboard", "MSI MPG Z590 ATX.png", "ProductDetailsPage.aspx?ProductId=7", 220);
            dummyData.Rows.Add(8, "MSI Z490-A PRO", 699.00m, "motherboard", "MSI Z490-A PRO.png", "ProductDetailsPage.aspx?ProductId=8", 192);
            dummyData.Rows.Add(9, "Kingston HyperX Fury 8GB", 159.99m, "ram", "Kingston HyperX FURY 8GB.png", "ProductDetailsPage.aspx?ProductId=9", 262);
            dummyData.Rows.Add(10, "Kingston HyperX Fury Beast RGB 8GB", 188.99m, "ram", "Kingston HyperX Fury Beast RGB 8GB.png", "ProductDetailsPage.aspx?ProductId=10", 279);
            dummyData.Rows.Add(11, "Kingston HyperX Fury Beast RGB 16GB", 349.99m, "ram", "Kingston HyperX Fury Beast RGB 16GB.png", "ProductDetailsPage.aspx?ProductId=11", 239);
            dummyData.Rows.Add(12, "Kingston A400 SSD (480GB)", 225.00m, "storage", "Kingston A400 SSD(480gb).png", "ProductDetailsPage.aspx?ProductId=12", 172);
            dummyData.Rows.Add(13, "WD Blue SSD 1TB", 519.00m, "storage", "WD Blue SSD 1TB.png", "ProductDetailsPage.aspx?ProductId=13", 108);
            dummyData.Rows.Add(14, "WD Blue M.2 1TB", 272.00m, "storage", "WD Blue M.2 1TB.png", "ProductDetailsPage.aspx?ProductId=14", 123);
            dummyData.Rows.Add(15, "Corsair RMx Series™ RM850x", 599.99m, "psu", "Corsair RMx Series RM850x.png", "ProductDetailsPage.aspx?ProductId=15", 712);
            dummyData.Rows.Add(16, "EVGA SuperNOVA 1000w T2", 1449.99m, "psu", "EVGA SuperNOVA 1000w T2.png", "ProductDetailsPage.aspx?ProductId=16", 923);
            dummyData.Rows.Add(17, "Lian Li PC-O11 Dynamic", 600.00m, "case", "Lian Li PC-O11 Dynamic.png", "ProductDetailsPage.aspx?ProductId=17", 490);
            dummyData.Rows.Add(18, "Intel Core i5-11400F", 999.99m, "cpu", "Intel Core i5-11400F.png", "ProductDetailsPage.aspx?ProductId=18", 86);
            dummyData.Rows.Add(19, "Intel Core i7-11700K", 2499.99m, "cpu", "Intel Core i7-11700K.png", "ProductDetailsPage.aspx?ProductId=19", 136);
            dummyData.Rows.Add(20, "Intel Core i9-11900K", 2599.99m, "cpu", "Intel Core i9-11900K.png", "ProductDetailsPage.aspx?ProductId=20", 158);
            dummyData.Rows.Add(21, "NVIDIA GTX 1080 Ti", 2899.00m, "gpu", "NVIDIA GTX 1080 Ti.png", "ProductDetailsPage.aspx?ProductId=21", 569);
            dummyData.Rows.Add(22, "NVIDIA RTX 2080", 3288.00m, "gpu", "NVIDIA_RTX_2080.png", "ProductDetailsPage.aspx?ProductId=22", 305);
            dummyData.Rows.Add(23, "NVIDIA RTX 3080", 4499.00m, "gpu", "NVIDIA RTX 3080 t.png", "ProductDetailsPage.aspx?ProductId=23", 99);

            return dummyData;
        }


        protected void FilterByCategory(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string category = btn.CommandArgument;
            FilterByCategoryLogic(category);
        }

        private void FilterByCategoryLogic(string category)
        {
            DataTable dummyData = GetDummyData();
            DataTable filteredData = dummyData.Clone(); // Create a clone of the schema

            foreach (DataRow row in dummyData.Rows)
            {
                string productType = row["ProductType"].ToString(); // Assuming ProductType column holds the category information
                if (productType.Equals(category, StringComparison.OrdinalIgnoreCase))
                {
                    filteredData.ImportRow(row);
                }
            }

            ListViewProducts.DataSource = filteredData;
            ListViewProducts.DataBind();

            UpdateNoProductsFoundMessageVisibility(filteredData.Rows.Count == 0);
        }

        protected void ApplyPriceFilterButton_Click(object sender, EventArgs e)
        {
            // Ensure that both minprice and maxprice are filled before proceeding
            if (!string.IsNullOrEmpty(minprice.Text) && !string.IsNullOrEmpty(maxprice.Text))
            {
                decimal minPrice = decimal.Parse(minprice.Text);
                decimal maxPrice = decimal.Parse(maxprice.Text);

                FilterByPriceRange(minPrice, maxPrice);
            }
            else
            {
                // Display a simple error message
                ScriptManager.RegisterStartupScript(this, GetType(), "PriceFilterError", "alert('Please fill in both minimum and maximum prices.');", true);

            }

            // Clear the input fields after filtering
            minprice.Text = string.Empty;
            maxprice.Text = string.Empty;
        }

        private void FilterByPriceRange(decimal minPrice, decimal maxPrice)
        {
            DataTable dummyData = GetDummyData();
            DataTable filteredData = dummyData.Clone(); // Create a clone of the schema

            foreach (DataRow row in dummyData.Rows)
            {
                decimal productPrice = Convert.ToDecimal(row["Price"]);
                if (productPrice >= minPrice && productPrice <= maxPrice)
                {
                    filteredData.ImportRow(row);
                }
            }

            ListViewProducts.DataSource = filteredData;
            ListViewProducts.DataBind();

            UpdateNoProductsFoundMessageVisibility(filteredData.Rows.Count == 0);
        }

        protected void SortAllProducts_Click(object sender, EventArgs e)
        {
            DataTable dummyData = GetDummyData();
            DataView dv = dummyData.DefaultView;
            dv.Sort = "ProductName ASC";
            DataTable sortedData = dv.ToTable();

            ListViewProducts.DataSource = sortedData;
            ListViewProducts.DataBind();

            UpdateNoProductsFoundMessageVisibility(sortedData.Rows.Count == 0);
        }


        protected void SortByPopularity_Click(object sender, EventArgs e)
        {
            DataTable dummyData = GetDummyData();
            DataView dv = dummyData.DefaultView;
            dv.Sort = "Sold DESC"; // Sort by number of items sold in descending order
            DataTable sortedData = dv.ToTable();

            ListViewProducts.DataSource = sortedData;
            ListViewProducts.DataBind();

            UpdateNoProductsFoundMessageVisibility(sortedData.Rows.Count == 0);

        }


        protected void SortByPriceLowToHigh_Click(object sender, EventArgs e)
        {
            DataTable dummyData = GetDummyData();
            DataView dv = dummyData.DefaultView;
            dv.Sort = "Price ASC";
            DataTable sortedData = dv.ToTable();

            ListViewProducts.DataSource = sortedData;
            ListViewProducts.DataBind();

            UpdateNoProductsFoundMessageVisibility(sortedData.Rows.Count == 0);
        }

        protected void SortByPriceHighToLow_Click(object sender, EventArgs e)
        {
            DataTable dummyData = GetDummyData();
            DataView dv = dummyData.DefaultView;
            dv.Sort = "Price DESC";
            DataTable sortedData = dv.ToTable();

            ListViewProducts.DataSource = sortedData;
            ListViewProducts.DataBind();

            UpdateNoProductsFoundMessageVisibility(sortedData.Rows.Count == 0);
        }


        protected void ResetButton_Click(object sender, EventArgs e)
        {
            productName.Text = string.Empty; // Reset search input
        }

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            string searchQuery = productName.Text.Trim().ToLower(); // Get the search query and convert to lowercase
            DataTable dummyData = GetDummyData(); // Retrieve dummy data

            // Filter the data based on the search query
            var filteredRows = dummyData.AsEnumerable().Where(row =>
            {
                string productName = row.Field<string>("ProductName").ToLower(); // Get the product name and convert to lowercase
                return productName.Contains(searchQuery); // Check if the product name contains the search query
            });

            // Check if there are any matching rows
            if (filteredRows.Any())
            {
                // Convert the filtered result to DataTable
                var filteredData = filteredRows.CopyToDataTable();

                // Bind the filtered data to the ListView
                ListViewProducts.DataSource = filteredData;
                ListViewProducts.DataBind();

                UpdateNoProductsFoundMessageVisibility(false);
            }
            else
            {
                // Clear the ListView
                ListViewProducts.DataSource = null;
                ListViewProducts.DataBind();

                UpdateNoProductsFoundMessageVisibility(true);
            }

            // Clear the search input
            productName.Text = string.Empty;
        }

        private void UpdateNoProductsFoundMessageVisibility(bool isVisible)
        {
            noProductsFoundMessage.Style["display"] = isVisible ? "block" : "none";
        }


    }
}