using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            init();
            initCategory();
            if ( IsPostBack )
            {
                // the whole section here is to ensure that the text box is retain else it will disapear after refresh due to posback
                int Variantcount = ViewState["VariantCount"] != null ? (int)ViewState["VariantCount"] : 1;
                System.Diagnostics.Debug.WriteLine("huh: " + Variantcount);
                for ( int i = 1 ; i < ( Variantcount + 1 ) ; i++ )
                {
                    //Literal divStart = new Literal { Text = "<div class='flex gap-4 items-center flex-wrap justify-evenly'>" };
                    //Literal divEnd = new Literal { Text = "</div>" };

                    ////creating new asp textbox
                    //TextBox textBox = new TextBox();
                    //textBox.ID = "variant" + ( i + 1 ) + "Tb";
                    //textBox.CssClass = "newVariation_input";
                    //textBox.Attributes["Placeholder"] = "Variant " + ( i + 1 );

                    ////creating new asp textbox
                    //TextBox newPrice = new TextBox();
                    //newPrice.ID = "priceVar" + ( i + 1 ) + "Tb";
                    //newPrice.CssClass = "newVariation_input";
                    //newPrice.Attributes["placeholder"] = "Price for Variant " + ( i + 1 );

                    //// adding into the panel ( using updatepanel because of avoiding anoying refresh
                    //panelVariantTextBoxes.Controls.Add(divStart);
                    //panelVariantTextBoxes.Controls.Add(textBox);
                    //panelVariantTextBoxes.Controls.Add(newPrice);
                    //panelVariantTextBoxes.Controls.Add(divEnd);


                }
            }
        }



        protected void ddlnewProdStatus_SelectedIndexChanged( object sender, EventArgs e )
        {
            editLblProdStatus.Text = ddlnewProdStatus.SelectedValue;
        }

        protected void createTextRowBtn_Click( object sender, EventArgs e )
        {
            //int Variantcount = ViewState["VariantCount"] != null ? (int)ViewState["VariantCount"] : 1;
            //System.Diagnostics.Debug.WriteLine("1: " + Variantcount);
            //System.Diagnostics.Debug.WriteLine("2: " + ( Variantcount + 1 ));
            //// Create the container div for the new row
            //Literal divStart = new Literal { Text = "<div class='flex gap-4 items-center flex-wrap justify-evenly>" };
            //int newId = Variantcount + 1;
            //// Create the Variant TextBox
            //TextBox newVariant = new TextBox();
            //newVariant.ID = "variant" + newId + "Tb";

            //newVariant.Attributes["Placeholder"] = "Variant " + newId;
            //newVariant.CssClass = "newVariation_input";

            //// Create the Price TextBox
            //TextBox newPrice = new TextBox();
            //newPrice.ID = "priceVar" + newId + "Tb"; // Notice that we use Variantcount for both to keep them paired
            //newPrice.Attributes["placeholder"] = "Price for Variant " + newId;
            //newPrice.CssClass = "newVariation_input";

            //// Create the container div for the new row
            //Literal divEnd = new Literal { Text = "</div>" };

            //// Add the opening div tag, the variant TextBox, the price TextBox, and the closing div tag to the panel
            //panelVariantTextBoxes.Controls.Add(divStart);
            //panelVariantTextBoxes.Controls.Add(newVariant);
            //panelVariantTextBoxes.Controls.Add(newPrice);
            //panelVariantTextBoxes.Controls.Add(divEnd);

            //ViewState["VariantCount"] = ( Variantcount + 1 );

            //// For debugging (visible in output window during debugging)
            //System.Diagnostics.Debug.WriteLine("Variant TextBox created with ID: " + newVariant.ID);
            //System.Diagnostics.Debug.WriteLine("Price TextBox created with ID: " + newPrice.ID);
        }

        private DataTable GetDummyData( )
        {
            DataTable dummyData = new DataTable();

            // Add columns to match your GridView's DataFields
            dummyData.Columns.Add("productID", typeof(int));
            dummyData.Columns.Add("ProductImageUrl", typeof(string));
            dummyData.Columns.Add("ProductName", typeof(string));
            dummyData.Columns.Add("ProductDec", typeof(string));
            dummyData.Columns.Add("Variant", typeof(string[]));
            dummyData.Columns.Add("Price", typeof(int[]));
            dummyData.Columns.Add("Stock", typeof(int));
            dummyData.Columns.Add("category", typeof(string));
            dummyData.Columns.Add("Status", typeof(string));

            // Define your arrays
            string[] variants = { "256GB", "512GB", "1TB" };
            int[] prices = { 999, 1199, 1399 };
            int stock = 50; // Example stock value
            string category = "Phone";
            string status = "Published";

            // Add rows with dummy data
            dummyData.Rows.Add(10234, "~/Admin/Layout/image/DexProfilePic.jpeg", "Iphone 30", "Is an iPhone", variants, prices, stock, category, status);
            // Add more rows as needed for testing

            return dummyData;
        }
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
        private void init( )
        {
            string encProdcutID = Request.QueryString["OrderID"];
            string productID = DecryptString(encProdcutID);
            LoadProductData(productID);
            List<string> imagePaths = GetImagePaths(productID);
            DisplayImages(imagePaths);

        }

        private void LoadProductData( string productId )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = "SELECT * FROM Product WHERE product_id = @ProductId; SELECT * FROM Product_Variant WHERE product_id = @ProductId;";
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@ProductId", productId);
                    using ( SqlDataReader reader = cmd.ExecuteReader() )
                    {
                        if ( reader.Read() )
                        {
                            // Assume columns: ProductName, Description, CategoryId, Status
                            editTbProductName.Text = reader["product_name"].ToString();
                            editTbProductDes.Text = reader["product_description"].ToString();
                            editDdlCategory.SelectedValue = reader["category_id"].ToString();
                            ddlnewProdStatus.SelectedValue = reader["product_status"].ToString();
                        }

                        if ( reader.NextResult() )
                        {
                            while ( reader.Read() )
                            {
                                CreateVariantControl(reader["variant_name"].ToString(), reader["variant_price"].ToString(),(int) reader["stock"]);
                            }
                        }
                    }
                }
            }
        }

        private void CreateVariantControl( string variantName, string price , int stock )
        {
            int variantCount = Convert.ToInt32(ViewState["VariantCount"]);
            TextBox variantTextBox = new TextBox
            {
                ID = "variant" + variantCount + "Tb",
                CssClass = "newVariation_input",
                Text = variantName
            };

            TextBox priceTextBox = new TextBox
            {
                ID = "priceVar" + variantCount + "Tb",
                CssClass = "newVariation_input",
                Text = price
            };
            TextBox stockTextBox = new TextBox
            {
                ID = "stockVar" + variantCount + "Tb",
                CssClass = "newVariation_input",
                Text = stock.ToString()
            };

            Literal divStart = new Literal { Text = "<div class='flex gap-4 items-center flex-wrap justify-evenly'>" };
            Literal divEnd = new Literal { Text = "</div>" };

            panelVariantTextBoxes.Controls.Add(divStart);
            panelVariantTextBoxes.Controls.Add(variantTextBox);
            panelVariantTextBoxes.Controls.Add(priceTextBox);
            panelVariantTextBoxes.Controls.Add(stockTextBox);
            panelVariantTextBoxes.Controls.Add(divEnd);

            ViewState["VariantCount"] = variantCount + 1;
        }

        protected void UploadButton_Click( object sender, EventArgs e )
        {
            if ( Page.IsValid )
            {
                UpdateProduct(editTbProductName.Text, editTbProductDes.Text, editDdlCategory.SelectedValue, ddlnewProdStatus.SelectedValue);
            }
        }

        private void UpdateProduct( string name, string description, string categoryId, string status )
        {
            using ( SqlConnection conn = new SqlConnection(connectionString) )
            {
                conn.Open();
                string sql = "UPDATE Products SET ProductName = @Name, Description = @Description, CategoryId = @CategoryId, Status = @Status WHERE ProductId = @ProductId;";
                using ( SqlCommand cmd = new SqlCommand(sql, conn) )
                {
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Description", description);
                    cmd.Parameters.AddWithValue("@CategoryId", categoryId);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@ProductId", Request.QueryString["ProductId"]);
                    cmd.ExecuteNonQuery();
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
        //private void DisplayImages( List<string> imagePaths )
        //{
        //    Literal imagesLiteral = new Literal();
        //    foreach ( string path in imagePaths )
        //    {
        //        string resolvedUrl = ResolveUrl(path);
        //        imagesLiteral.Text += $"<img src='{resolvedUrl}' class='preview-image' alt='Product Image'Height='216' Width='216' onclick='document.getElementById('<%= fileUploadClientID %>').click()'/>";
        //    }
        //    PanelBackground.Controls.Add(imagesLiteral);  // Assuming `imageContainer` is a Panel or similar container
        //}
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
            if ( fileUpload.HasFiles )
            {
                List<string> imagePaths = new List<string>();
                foreach ( HttpPostedFile postedFile in fileUpload.PostedFiles )
                {
                    // Generate a unique file name; could use GUID or a similar method
                    string filename = Path.GetFileName(postedFile.FileName);
                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(filename);
                    string filePath = Server.MapPath("~/Admin/Product Management/productImages/") + uniqueFileName;

                    // Save the uploaded file to the server
                    postedFile.SaveAs(filePath);
                    imagePaths.Add("~/Admin/Product Management/productImages/" + uniqueFileName);
                    // Assuming you have a method to update or insert a new image path into the database
                    //UpdateImagePathInDatabase(uniqueFileName, "productId");  // Example productId
                }

                // Optionally, refresh the image display
                DisplayImages(imagePaths);
            }
        }

    }
}