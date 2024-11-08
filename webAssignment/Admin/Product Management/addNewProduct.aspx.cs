﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace webAssignment.Admin.Product_Management
{
    public partial class addNewProduct : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                initCategory();
                ViewState["VariantCount"] = 0;
                ViewState["addedOnce"] = 0;
            }
            else
            {
                if ( ViewState["addedOnce"] != null && (int)ViewState["addedOnce"] == 1 )
                {
                    recreateVariantTb();
                }
            }

        }

        protected void recreateVariantTb( )
        {
            // the whole section here is to ensure that the text box is retain else it will disapear after refresh due to posback
            panelVariantTextBoxes.Controls.Clear();
            int variantCount = ViewState["VariantCount"] != null ? (int)ViewState["VariantCount"] : 1;
            for ( int i = 1 ; i <= variantCount ; i++ )
            {
                Literal divStart = new Literal { Text = "<div class='grid grid-cols-3 gap-4 items-center flex-wrap justify-evenly'>" };
                Literal divEnd = new Literal { Text = "</div>" };

                //creating new asp textbox
                TextBox textBox = new TextBox();
                System.Diagnostics.Debug.WriteLine("huh2: " + ( i + 1 ));
                textBox.ID = "variant" + ( i + 1 ) + "Tb";
                textBox.CssClass = "newVariation_input";
                textBox.Attributes["Placeholder"] = "Variant Name " + ( i + 1 );

                //creating new asp textbox
                TextBox newPrice = new TextBox();
                newPrice.ID = "priceVar" + ( i + 1 ) + "Tb";
                newPrice.CssClass = "newVariation_input";
                newPrice.Attributes["placeholder"] = "Price for Variant " + ( i + 1 );

                TextBox newStock = new TextBox();
                newStock.ID = "stockVar" + ( i + 1 ) + "Tb";
                newStock.CssClass = "newVariation_input";
                newStock.Attributes["placeholder"] = "Stock for Variant " + ( i + 1 );
                // adding into the panel ( using updatepanel because of avoiding anoying refresh
                panelVariantTextBoxes.Controls.Add(divStart);
                panelVariantTextBoxes.Controls.Add(textBox);
                panelVariantTextBoxes.Controls.Add(newPrice);
                panelVariantTextBoxes.Controls.Add(newStock);
                panelVariantTextBoxes.Controls.Add(divEnd);
            }
        }
        protected void ddlnewProdStatus_SelectedIndexChanged( object sender, EventArgs e )
        {
            lblnewProdStatus.Text = ddlnewProdStatus.SelectedValue;
        }

        protected void createTextRowBtn_Click( object sender, EventArgs e )
        {
            ViewState["VariantCount"] = ( (int)ViewState["VariantCount"] + 1 );
            ViewState["addedOnce"] = 1;
            recreateVariantTb(); 
            //int Variantcount = (int)ViewState["VariantCount"];

            //// Create the container div for the new row
            //Literal divStart = new Literal { Text = "<div class='grid grid-cols-3 gap-4 items-center flex-wrap justify-evenly>" };
            //int newId = Variantcount;

            //// Create the Variant TextBox
            //TextBox newVariant = new TextBox();
            //newVariant.ID = "variant" + newId + "Tb";
            //newVariant.Attributes["Placeholder"] = "Variant Name " + newId;
            //newVariant.CssClass = "newVariation_input";

            //// Create the Price TextBox
            //TextBox newPrice = new TextBox();
            //newPrice.ID = "priceVar" + newId + "Tb";
            //newPrice.Attributes["placeholder"] = "Price for Variant " + newId;
            //newPrice.CssClass = "newVariation_input";

            //// Create the Price TextBox
            //TextBox newStock = new TextBox();
            //newStock.ID = "stockVar" + newId + "Tb";
            //newStock.Attributes["placeholder"] = "Stock for Variant " + newId;
            //newStock.CssClass = "newVariation_input";
            //// Create the container div for the new row
            //Literal divEnd = new Literal { Text = "</div>" };

            //panelVariantTextBoxes.Controls.Add(divStart);
            //panelVariantTextBoxes.Controls.Add(newVariant);
            //panelVariantTextBoxes.Controls.Add(newPrice);
            //panelVariantTextBoxes.Controls.Add(newStock);
            //panelVariantTextBoxes.Controls.Add(divEnd);

            //Debug.WriteLine("WUI" + panelVariantTextBoxes.Controls);
            //Debug.WriteLine("WUI" + Variantcount);
        }

        protected void UploadButton_Click( object sender, EventArgs e )
        {
            String prodName = newProductName.Text.Trim();
            String prodDesc = newProductDes.Text.Trim();
            String category = ddlCategory.SelectedValue;
            String status = ddlnewProdStatus.SelectedValue;

            if ( checkInputs() == false )
            {
                ShowNotification("Please correct the highlighted errors and try again.", "warning");
                return;
            }
            if ( checkProdVariants() == false )
            {
                return;
            }
            if ( Page.IsValid )
            {
                string productId = InsertProduct(prodName, prodDesc, category, status);

                if ( productId != "" )
                {
                    SaveProductVariants(productId);
                    SaveProductImages(productId);
                }
                else
                {
                    ShowNotification("Input are not complete!.", "warning");
                }
                ShowNotification("Successfully added new product", "success");
            }
        }

        private bool checkInputs( )
        {
            String prodName = newProductName.Text.Trim();
            String category = ddlCategory.SelectedValue;
            if ( prodName == "" )
            {
                ShowNotification("Please type product name!.", "warning");
                return false;
            }
            if ( category == "" )
            {
                ShowNotification("Please choose a category!.", "warning");
                return false;
            }
            if ( !fileImages.HasFile )
            {
                ShowNotification("Please insert Atleast a Image and try again.", "warning");
                return false;
            }
            List<string> descriptions = productDec();
            Debug.WriteLine("qwqcqwwq" + descriptions);
            Debug.WriteLine("qwqcqwwq" + descriptions.Any());
            if ( descriptions.Any() == false )
            {
                ShowNotification("Please include at least one valid DEC label with a description.", "warning");
                return false;
            }
            return true;
        }

        private bool checkProdVariants( )
        {
            int variantCount = ViewState["VariantCount"] != null ? (int)ViewState["VariantCount"] : 1;
            for ( int i = 1 ; i <= variantCount ; i++ )
            {
                TextBox textBoxVariant = (TextBox)panelVariantTextBoxes.FindControl("variant" + i + "Tb");
                TextBox textBoxPrice = (TextBox)panelVariantTextBoxes.FindControl("priceVar" + i + "Tb");
                TextBox textBoxStock = (TextBox)panelVariantTextBoxes.FindControl("stockVar" + i + "Tb");

                if ( textBoxVariant == null || textBoxPrice == null || textBoxStock == null )
                {
                    ShowNotification("A variant textbox was not found, please try again.", "warning");
                    return false;
                }

                if ( string.IsNullOrWhiteSpace(textBoxVariant.Text) || string.IsNullOrWhiteSpace(textBoxPrice.Text) || string.IsNullOrWhiteSpace(textBoxStock.Text) )
                {
                    ShowNotification("Please make sure no field for the product variant is empty", "warning");
                    return false;
                }

                // Check if price is an integer
                if ( !int.TryParse(textBoxPrice.Text, out int price) )
                {
                    ShowNotification("Please enter a valid integer for price.", "warning");
                    return false;
                }

                // Check if stock is an integer
                if ( !int.TryParse(textBoxStock.Text, out int stock) )
                {
                    ShowNotification("Please enter a valid integer for stock.", "warning");
                    return false;
                }
            }
            return true;
        }
        public List<string> GetImagePaths( )
        {
            List<string> imagePaths = new List<string>();

            string folderPath = HttpContext.Current.Server.MapPath("~/ProductImage/");

            string[] files = Directory.GetFiles(folderPath);

            foreach ( string file in files )
            {
                if ( IsImage(file) )
                {
                    string relativePath = "ProductImage/" + Path.GetFileName(file);
                    imagePaths.Add(relativePath);
                }
            }

            return imagePaths;
        }



        // getting all the categories for display at the category ddl
        private List<Category> getCategories( )
        {
            List<Category> categories = new List<Category>();

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                // Open the connection
                conn.Open();

                // SQL query to select data from the Category table
                string sql = "SELECT category_id, category_name FROM Category;";

                // Create a SqlCommand object
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {

                    // Execute the query and obtain a SqlDataReader
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {

                        while ( reader.Read() )
                        {
                            categories.Add(new Category
                            {
                                CategoryID = reader.GetString(0),
                                CategoryName = reader.GetString(1)
                            });
                        }
                    }
                }
            }


            return categories;
        }

        // for category ddl init
        private void initCategory( )
        {
            List<Category> categories = getCategories();

            // Clear existing items
            ddlCategory.Items.Clear();


            // Check if categories were fetched successfully
            if ( categories != null && categories.Count > 0 )
            {
                ddlCategory.DataSource = categories;
                ddlCategory.DataTextField = "CategoryName";
                ddlCategory.DataValueField = "CategoryID";
                ddlCategory.DataBind();
            }
            ddlCategory.Items.Insert(0, new ListItem("Select a category", ""));

        }
        //private string productDec( )
        //{
        //    var lines = newProductDes.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
        //    var descriptions = new List<string>();

        //    foreach ( var line in lines )
        //    {
        //        var splitIndex = line.IndexOf(":");
        //        if ( splitIndex != -1 && line.StartsWith("DEC") ) 
        //        {
        //            descriptions.Add(line.Substring(splitIndex + 1).Trim()); 
        //        }
        //    }

        //    return JsonConvert.SerializeObject(descriptions);
        //}
        private List<string> productDec( )
        {
            var lines = newProductDes.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            var descriptions = new List<string>();
            bool hasValidDec = false;

            foreach ( var line in lines )
            {
                if ( line.Trim().StartsWith("DEC") && line.Contains(":") )
                {
                    var splitIndex = line.IndexOf(":");
                    if ( splitIndex != -1 && splitIndex + 1 < line.Length )
                    {
                        string descriptionContent = line.Substring(splitIndex + 1).Trim();
                        if ( !string.IsNullOrWhiteSpace(descriptionContent) )
                        {
                            descriptions.Add(descriptionContent);
                            hasValidDec = true;
                        }
                        else
                        {
                            return new List<string>();
                        }
                    }
                }
                else
                {
                    return new List<string>();
                }
            }

            if ( !hasValidDec )
            {
                return new List<string>();
            }

            return descriptions;
        }
        private string InsertProduct( string prodName, string prodDesc, string categoryId, string status )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = "INSERT INTO Product (product_id,category_id, product_name, product_description, product_status , date_added) OUTPUT INSERTED.product_id VALUES (@productID,@CategoryID, @ProductName, @ProductDescription, @Status,@date_Added)";


                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@productID", GetNextProductId(conn));
                    cmd.Parameters.AddWithValue("@CategoryID", categoryId);
                    cmd.Parameters.AddWithValue("@ProductName", prodName);
                    cmd.Parameters.AddWithValue("@ProductDescription", JsonConvert.SerializeObject(productDec()));
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.Add("@date_Added", SqlDbType.DateTime).Value = DateTime.UtcNow;
                    string productId = cmd.ExecuteScalar().ToString();
                    return productId;
                }
            }
        }
        private void SaveProductVariants( string productId )
        {
            int variantCount = ViewState["VariantCount"] != null ? (int)ViewState["VariantCount"] : 0;
            if ( variantCount == 0)
            {
                variantCount = 1;
            }
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                for ( int i = 1 ; i <= variantCount ; i++ )
                {
                    TextBox textBoxVariant = (TextBox)panelVariantTextBoxes.FindControl("variant" + i + "Tb");
                    TextBox textBoxPrice = (TextBox)panelVariantTextBoxes.FindControl("priceVar" + i + "Tb");
                    TextBox textBoxStock = (TextBox)panelVariantTextBoxes.FindControl("stockVar" + i + "Tb");

                    if ( textBoxVariant != null && textBoxPrice != null && textBoxStock != null &&
                        !string.IsNullOrWhiteSpace(textBoxVariant.Text) && !string.IsNullOrWhiteSpace(textBoxPrice.Text) )
                    {
                        string variantName = textBoxVariant.Text.Trim();
                        decimal price = decimal.Parse(textBoxPrice.Text);
                        int stock = int.Parse(textBoxStock.Text);
                        string variantId = GetNextProductVariantId(conn);

                        Debug.WriteLine("TEstingCheck" + variantId);
                        Debug.WriteLine("TEstingCheck" + productId);
                        Debug.WriteLine("TEstingCheck" + variantName);
                        Debug.WriteLine("TEstingCheck" + price);
                        Debug.WriteLine("TEstingCheck" + stock);


                        string sql = "INSERT INTO Product_Variant (product_variant_id,product_id, variant_name, variant_price, stock,variant_status) VALUES (@VariantID,@ProductID, @VariantName, @VariantPrice, @Stock, @status)";

                        using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                        {
                            cmd.Parameters.AddWithValue("@VariantID", variantId);
                            cmd.Parameters.AddWithValue("@ProductID", productId);
                            cmd.Parameters.AddWithValue("@VariantName", variantName);
                            cmd.Parameters.AddWithValue("@VariantPrice", price);
                            cmd.Parameters.AddWithValue("@Stock", stock);
                            cmd.Parameters.AddWithValue("@status", "In Stock");
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        private bool SaveProductImages( string productId )
        {
            if ( fileImages.HasFiles )
            {
                foreach ( HttpPostedFile postedFile in fileImages.PostedFiles )
                {
                    string fileName = Path.GetFileName(postedFile.FileName);
                    string fileExtension = Path.GetExtension(fileName);
                    string fileSavePath = Server.MapPath("~/Admin/Product Management/productImages/") + fileName;

                    try
                    {
                        // Save the file to the server
                        postedFile.SaveAs(fileSavePath);

                        // Generate a unique image ID
                        string imageId = GetNextImageId();

                        // Save image details in the database
                        InsertImageDetails(imageId, productId, ( "~/Admin/Product Management/productImages/" + fileName ));
                        Debug.WriteLine("woi : " + imageId);
                        Debug.WriteLine("woi : " + productId);
                    }
                    catch ( Exception ex )
                    {
                        // Log error (consider logging to a file or database)
                        Debug.WriteLine("Error: " + ex.Message);
                    }
                }
            }
            return true;
        }

        private void InsertImageDetails( string imageId, string productId, string imagePath )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();

                string sql = "INSERT INTO Image_Path (image_path_id, product_id, path) VALUES (@ImageId, @ProductId, @Path)";
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@ImageId", imageId);
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@Path", imagePath);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // getting all the nessary ids
        private string GetNextProductId( SqlConnection conn )
        {
            string prefix = "P";
            int maxNumber = 1000;

            string sql = "SELECT MAX(product_id) FROM Product WHERE product_id LIKE @Prefix + '%'";
            using ( SqlCommand cmd = new SqlCommand(sql, conn) )
            {
                cmd.Parameters.AddWithValue("@Prefix", prefix);
                object result = cmd.ExecuteScalar();
                if ( result != null && !DBNull.Value.Equals(result) )
                {
                    string maxIdStr = result.ToString().Substring(prefix.Length);
                    if ( int.TryParse(maxIdStr, out maxNumber) )
                    {
                        maxNumber++;
                    }
                }
                else
                {
                    maxNumber = 1;
                }
            }

            return prefix + maxNumber.ToString("D3");
        }
        private string GetNextImageId( )
        {
            string prefix = "IMGP";
            int maxId = 1000; // Default 0 

            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = "SELECT MAX(image_path_id) FROM Image_Path WHERE image_path_id LIKE @Prefix + '%'";
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@Prefix", prefix);
                    object result = cmd.ExecuteScalar();

                    if ( result != null && !DBNull.Value.Equals(result) )
                    {
                        string maxIdStr = result.ToString().Substring(prefix.Length);
                        if ( int.TryParse(maxIdStr, out maxId) )
                        {
                            maxId++;
                        }
                    }
                }
            }

            return prefix + maxId.ToString("D3");
        }

        private string GetNextProductVariantId( SqlConnection conn )
        {
            string prefix = "PV"; // Assuming IDs are like PV1001, PV1002, ...
            int maxNumber = 1000; // Start from 0 if no entries exist

            string sql = "SELECT MAX(product_variant_id) FROM Product_Variant WHERE product_variant_id LIKE @Prefix + '%'";
            using ( SqlCommand cmd = new SqlCommand(sql, conn) )
            {
                cmd.Parameters.AddWithValue("@Prefix", prefix);
                object result = cmd.ExecuteScalar();
                if ( result != null && !DBNull.Value.Equals(result) && result.ToString().Length > prefix.Length )
                {
                    string maxIdStr = result.ToString().Substring(prefix.Length);
                    if ( int.TryParse(maxIdStr, out maxNumber) )
                    {
                        maxNumber++; // Increment the numeric part
                    }
                }
                else
                {
                    maxNumber = 1; // Start with 1 if no IDs are found
                }
            }

            return prefix + maxNumber.ToString("D4"); // Ensure it is padded to four digits (e.g., PV1001)
        }


        // double check if the inputed file is an ikmage
        private bool IsImage( string file )
        {
            string extension = Path.GetExtension(file).ToLowerInvariant();
            return extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".gif";
        }
        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }

        private void clearAllFields( )
        {
            // Clear text boxes 
            newProductName.Text = String.Empty;
            newProductDes.Text = String.Empty;

            // Reset to default
            if ( ddlCategory.Items.Count > 0 )
                ddlCategory.SelectedIndex = 0;

            // Reset to default
            if ( ddlnewProdStatus.Items.Count > 0 )
                ddlnewProdStatus.SelectedIndex = 0;

            // Clear all variant text boxes
            panelVariantTextBoxes.Controls.Clear();
            ViewState["VariantCount"] = 1;

            // Clear the file upload control
            fileImages.Attributes.Clear();

            // Reset added once flag
            ViewState["addedOnce"] = 0;

        }
        private bool IsValidImageExtension( string fileName )
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();
            var validExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png" };
            return validExtensions.Contains(extension);
        }
    }
}