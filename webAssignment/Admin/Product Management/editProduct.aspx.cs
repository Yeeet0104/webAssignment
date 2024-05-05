using DocumentFormat.OpenXml.VariantTypes;
using Irony.Parsing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace webAssignment.Admin.Product_Management
{
    public partial class editProduct : System.Web.UI.Page
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        protected void Page_Load( object sender, EventArgs e )
        {
            if ( !IsPostBack )
            {
                init();
                initCategory();

            }
            else
            {
                RecreateVariantControls();  // Recreate variant controls based on the count stored in ViewState
            }

        }
        private void init( )
        {
            ViewState["imgChanged"] = false;
            string encProdcutID = Request.QueryString["ProdID"];
            string productID = DecryptString(encProdcutID);
            LoadProductData(productID);
            loadVariantData(productID);
            LoadImagePaths(productID);

        }
        private void LoadImagePaths( string productID )
        {
            List<string> imagePaths = GetImagePaths(productID);
            ViewState["InitialImagePaths"] = imagePaths;
            DisplayImages(imagePaths);
        }
        protected void ddlnewProdStatus_SelectedIndexChanged( object sender, EventArgs e )
        {
            editLblProdStatus.Text = ddlnewProdStatus.SelectedValue;
        }
        // decrypting the encrpyted string from the query string
        protected string DecryptString( string cipherText )
        {
            string EncryptionKey = "ABC123"; // Use the same key you used during encryption
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using ( Aes encryptor = Aes.Create() )
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using ( MemoryStream ms = new MemoryStream() )
                {
                    using ( CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write) )
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        private void prodDec( string descriptionJson )
        {
            List<string> descriptions = JsonConvert.DeserializeObject<List<string>>(descriptionJson);
            StringBuilder formattedDescriptions = new StringBuilder();
            for ( int i = 0 ; i < descriptions.Count ; i++ )
            {
                formattedDescriptions.AppendLine($"DEC{i + 1}: {descriptions[i]}");
            }
            editTbProductDes.Text = formattedDescriptions.ToString();
        }
        // loading data for initialize to the relavent inputs
        private void LoadProductData( string productId )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = "SELECT * FROM Product WHERE product_id = @ProductId";
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        if ( reader.Read() )
                        {
                            // Assume columns: ProductName, Description, CategoryId, Status
                            editTbProductName.Text = reader["product_name"].ToString();
                            prodDec(reader["product_description"].ToString());
                            ViewState["InitialProductDescription"] = reader["product_description"].ToString();
                            editDdlCategory.SelectedValue = reader["category_id"].ToString();
                            ddlnewProdStatus.SelectedValue = reader["product_status"].ToString();
                            editLblProdStatus.Text = reader["product_status"].ToString();
                            ViewState["InitialProductName"] = reader["product_name"].ToString();
                            ViewState["InitialProductDescription"] = reader["product_description"].ToString();
                            ViewState["InitialProductStatus"] = reader["category_id"].ToString();
                            ViewState["InitialCategoryId"] = reader["product_status"].ToString();
                        }
                    }
                }
            }
        }

        private List<Variant> loadVariantData( string productID )
        {
            List<Variant> variants = new List<Variant>();
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = "SELECT product_variant_id, variant_name, variant_price, stock FROM Product_Variant WHERE product_id = @ProductId;";
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@ProductId", productID);
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        int count = 0;
                        while ( reader.Read() )
                        {
                            var variant = new Variant
                            {
                                product_variant_id = reader.GetString(0),
                                variant_name = reader.GetString(1),
                                variant_price = reader.GetDecimal(2),
                                stock = reader.GetInt32(3)
                            };
                            variants.Add(variant);
                            CreateVariantControl(count++, variant.variant_name, variant.variant_price.ToString(), variant.stock.ToString(), variant.product_variant_id);
                        }
                        ViewState["VariantCount"] = count;
                    }
                }
            }
            ViewState["InitialVariants"] = variants;
            return variants;
        }

        // for creating variant rows
        private void CreateVariantControl( int index, string variantName, string price, string stock , string variantId = "" )
        {
            TextBox variantTextBox = new TextBox
            {
                ID = "variant" + index + "Tb",
                CssClass = "newVariation_input",
                Text = variantName,
                Attributes = { ["Placeholder"] = "Variant Name " + index }
            };

            TextBox priceTextBox = new TextBox
            {
                ID = "priceVar" + index + "Tb",
                CssClass = "newVariation_input",
                Text = price,
                Attributes = { ["placeholder"] = "Price for Variant " + index }
            };

            TextBox stockTextBox = new TextBox
            {
                ID = "stockVar" + index + "Tb",
                CssClass = "newVariation_input",
                Text = stock,
                Attributes = { ["placeholder"] = "Stock for Variant " + index }
            };
            HiddenField variantIdField = new HiddenField
            {
                ID = "variantId" + index,
                Value = variantId
            };
            panelVariantTextBoxes.Controls.Add(new Literal { Text = "<div class='grid mb-3 grid-cols-3 gap-4 items-center flex-wrap justify-evenly'>" });
            panelVariantTextBoxes.Controls.Add(variantTextBox);
            panelVariantTextBoxes.Controls.Add(priceTextBox);
            panelVariantTextBoxes.Controls.Add(stockTextBox);
            panelVariantTextBoxes.Controls.Add(variantIdField);
            panelVariantTextBoxes.Controls.Add(new Literal { Text = "</div>" });
        }

        // this is for recreating the variant rows after postback happend
        private void RecreateVariantControls( )
        {
            int variantCount = ViewState["VariantCount"] != null ? (int)ViewState["VariantCount"] : 0;
            for ( int i = 0 ; i < variantCount ; i++ )
            {
                TextBox variantTextBox = (TextBox)panelVariantTextBoxes.FindControl("variant" + i + "Tb");
                TextBox priceTextBox = (TextBox)panelVariantTextBoxes.FindControl("priceVar" + i + "Tb");
                TextBox stockTextBox = (TextBox)panelVariantTextBoxes.FindControl("stockVar" + i + "Tb");
                HiddenField variantIdField = (HiddenField)panelVariantTextBoxes.FindControl("variantId" + i);

                Debug.WriteLine("VariantIDField" + variantIdField);

                if ( variantTextBox == null || priceTextBox == null || stockTextBox == null )
                {
                    // Only recreate if they don't exist
                    CreateVariantControl(i, variantTextBox?.Text, priceTextBox?.Text, Convert.ToInt32(stockTextBox?.Text).ToString());
                }
            }
        }

        // for category ddl init
        private void initCategory( )
        {
            List<Category> categories = getCategories();

            // Clear existing items
            editDdlCategory.Items.Clear();


            // Check if categories were fetched successfully
            if ( categories != null && categories.Count > 0 )
            {
                editDdlCategory.DataSource = categories;
                editDdlCategory.DataTextField = "CategoryName";
                editDdlCategory.DataValueField = "CategoryID";
                editDdlCategory.DataBind();
            }
            editDdlCategory.Items.Insert(0, new ListItem("Select a category", ""));

        }
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

        // for displaying the image
        protected List<string> GetImagePaths( string productId )
        {
            List<string> paths = new List<string>();
            string query = "SELECT path FROM Image_Path WHERE product_id = @ProductId";
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                using ( SqlCommand cmd = new SqlCommand(query, conn) )
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    conn.Open();
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        while ( reader.Read() )
                        {
                            string imagePath = reader["path"].ToString();
                            paths.Add(imagePath);
                        }
                    }
                }
            }
            return paths;
        }
        private void DisplayImages( List<string> imagePaths )
        {
            imageContainer.Controls.Clear(); // Clear existing images similar to innerHTML = ''

            foreach ( string path in imagePaths )
            {
                // Create a new image control
                Image img = new Image
                {
                    ImageUrl = ResolveUrl(path),  // Resolve the URL to ensure it's correct
                    CssClass = "preview-image",
                    Width = 216,
                    Height = 216
                };

                // Add the image control to the panel
                imageContainer.Controls.Add(img);
            }
        }
        protected void UploadImages( object sender, EventArgs e )
        {
            List<string> newImagePaths = new List<string>();
            if ( fileUpload.HasFiles )
            {
                foreach ( HttpPostedFile postedFile in fileUpload.PostedFiles )
                {
                    string filename = Path.GetFileName(postedFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(filename);
                    string filePath = Server.MapPath("~/Admin/Product Management/productImages/") + uniqueFileName;
                    ViewState["imgChanged"] = true;
                    postedFile.SaveAs(filePath);  // Save file
                    newImagePaths.Add("~/Admin/Product Management/productImages/" + uniqueFileName);
                }

                // Optionally, add new paths to ViewState for later processing or directly update the database
                List<string> currentImages = ViewState["UploadedImages"] as List<string> ?? new List<string>();
                currentImages.AddRange(newImagePaths);
                ViewState["UploadedImages"] = currentImages;

                // Refresh the display to include new images
                DisplayImages(currentImages);
            }
        }

        protected void UpdateProductImages( string productID )
        {
            if ( (bool) ViewState["imgChanged"] == true) {
                List<string> initialImages = ViewState["InitialImagePaths"] as List<string>;
                List<string> uploadedImages = ViewState["UploadedImages"] as List<string> ?? new List<string>();

                // Add new images to the database
                foreach ( string imagePath in uploadedImages )
                {
                    if ( !initialImages.Contains(imagePath) )
                    {
                        InsertImageDetails(productID, imagePath);
                    }
                }

                // deleting existing images ( replacing )
                foreach ( string initialPath in initialImages )
                {
                    if ( !uploadedImages.Contains(initialPath) )
                    {
                        // Delete image records and potentially delete files if no longer needed
                        DeleteImageDetails(productID, initialPath);
                    }
                }
            }

        }

        private void InsertImageDetails( string productId, string imagePath )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string imageId = GetNextImageId();
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

        private void DeleteImageDetails( string productId, string imagePath )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {

                conn.Open();
                string sql = "DELETE FROM Image_Path WHERE product_id = @ProductId AND path = @Path";
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    cmd.Parameters.AddWithValue("@Path", imagePath);
                    cmd.ExecuteNonQuery();
                }
            }
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

        protected void UploadButton_Click( object sender, EventArgs e )
        {
            string encProdcutID = Request.QueryString["ProdID"];
            string productID = DecryptString(encProdcutID);
            UpdateProduct();
            UpdateVariants(productID);
            UpdateProductImages(productID);
            LoadImagePaths(productID); // Reload image paths to update the display.
        }

        // for variant tab if the add varian button is clicked then add new rows
        protected void createTextRowBtn_Click( object sender, EventArgs e )
        {
            int variantCount = (int)ViewState["VariantCount"];
            CreateVariantControl(variantCount, "", "", "0");
            ViewState["VariantCount"] = variantCount + 1;

            Debug.WriteLine("CIBAI");
            Debug.WriteLine("CIBAI" + ViewState["VariantCount"]);
        }

        // for variant tab if the reset varian button is clicked then add new rows
        protected void resetVariant_Click( object sender, EventArgs e )
        {
            panelVariantTextBoxes.Controls.Clear();
            string encProdcutID = Request.QueryString["ProdID"];
            string productID = DecryptString(encProdcutID); 
            loadVariantData(productID);
        }

        private string FormatDescriptionsFromTextBox( )
        {
            // Assuming descriptions are separated by new lines and start with "DECx: "
            var lines = editTbProductDes.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            List<string> descriptions = lines.Select(line =>
            {
                int idx = line.IndexOf(": ");
                return idx != -1 ? line.Substring(idx + 2) : string.Empty;
            }).ToList();

            return JsonConvert.SerializeObject(descriptions);
        }
        private bool IsDescriptionChanged( )
        {
            string currentDescription = FormatDescriptionsFromTextBox();
            string initialDescription = ViewState["InitialProductDescription"] as string;

            return !String.Equals(currentDescription, initialDescription, StringComparison.Ordinal);
        }
        // updating the database
        private void UpdateProduct( )
        {
            string currentName = editTbProductName.Text;
            string currentDescription = editTbProductDes.Text;
            string currentStatus = ddlnewProdStatus.SelectedValue;
            string currentCategory = editDdlCategory.SelectedValue;

            bool isUpdated = false;
            SqlCommand cmd = new SqlCommand();
            StringBuilder sql = new StringBuilder("UPDATE Product SET ");
            if ( currentName != (string)ViewState["InitialProductName"] )
            {
                sql.Append("product_name = @Name, ");
                cmd.Parameters.AddWithValue("@Name", currentName);
                isUpdated = true;
            }
            if ( IsDescriptionChanged() )
            {
                sql.Append("product_description = @Description, ");
                cmd.Parameters.AddWithValue("@Description", FormatDescriptionsFromTextBox());
                isUpdated = true;
            }
            if ( currentStatus != (string)ViewState["InitialProductStatus"] )
            {
                sql.Append("product_status = @Status, ");
                cmd.Parameters.AddWithValue("@Status", currentStatus);
                isUpdated = true;
            }
            if ( currentCategory != (string)ViewState["InitialCategoryId"] )
            {
                sql.Append("category_id = @CategoryId, ");
                cmd.Parameters.AddWithValue("@CategoryId", currentCategory);
                isUpdated = true;
            }

            if ( isUpdated )
            {
                string encProdcutID = Request.QueryString["ProdID"];
                string productID = DecryptString(encProdcutID);
                sql.Length -= 2; // Remove the last comma and space
                sql.Append(" WHERE product_id = @ProductId");
                cmd.Parameters.AddWithValue("@ProductId", productID);

                cmd.CommandText = sql.ToString();
                cmd.Connection = new SqlConnection(connectionString);
                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                ShowNotification("Successfully Updated !", "success");
            }
        }
        protected void UpdateVariants( string productID )
        {
            int variantCount = (int)ViewState["VariantCount"];
            for ( int i = 0 ; i < variantCount ; i++ )
            {
                TextBox variantNameBox = (TextBox) panelVariantTextBoxes.FindControl("variant" + i + "Tb");
                TextBox variantPriceBox = (TextBox)panelVariantTextBoxes.FindControl("priceVar" + i + "Tb");
                TextBox variantStockBox = (TextBox)panelVariantTextBoxes.FindControl("stockVar" + i + "Tb");
                HiddenField variantIdField = (HiddenField) panelVariantTextBoxes.FindControl("variantId" + i);
                if ( variantIdField.Value == "")
                {
                    InsertVariant(productID, variantNameBox.Text, decimal.Parse(variantPriceBox.Text), int.Parse(variantStockBox.Text));

                }
                else
                {
                    UpdateVariant(variantIdField.Value, variantNameBox.Text, decimal.Parse(variantPriceBox.Text), int.Parse(variantStockBox.Text));
                }
            }
        }
        private void InsertVariant( string productId, string name, decimal price, int stock )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = "INSERT INTO Product_Variant (product_variant_id,product_id, variant_name, variant_price, stock,variant_status) VALUES (@VariantID,@ProductID, @VariantName, @VariantPrice, @Stock, @status)";
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@VariantID", GetNextProductVariantId(conn));
                    cmd.Parameters.AddWithValue("@ProductID", productId);
                    cmd.Parameters.AddWithValue("@VariantName", name);
                    cmd.Parameters.AddWithValue("@VariantPrice", price);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.Parameters.AddWithValue("@status", "In Stock");
                    cmd.ExecuteNonQuery();
                }
            }
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
        private void UpdateVariant( string variantId, string name, decimal price, int stock )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = "UPDATE Product_Variant SET variant_name = @Name, variant_price = @Price, stock = @Stock WHERE product_variant_id = @VariantId";
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@VariantId", variantId);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Stock", stock);
                    cmd.ExecuteNonQuery();
                }
            }
        }
       
       

        protected void ShowNotification( string message, string type )
        {
            string script = $"window.onload = function() {{ showSnackbar('{message}', '{type}'); }};";
            ClientScript.RegisterStartupScript(this.GetType(), "ShowSnackbar", script, true);
        }
    }
}