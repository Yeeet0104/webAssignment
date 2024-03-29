using System;
using System.Collections.Generic;
using System.Data;
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
        protected void Page_Load( object sender, EventArgs e )
        {
            init();
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
            editLblProdStatus.Text = editDdlProdStatus.SelectedValue;
        }

        protected void UploadButton_Click( object sender, EventArgs e )
        {

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
            int productID = int.Parse(DecryptString(encProdcutID));
            DataTable dummyData = GetDummyData();

            for ( int i = 0 ; i < dummyData.Rows.Count ; i++ )
            {
                DataRow row = dummyData.Rows[i];
                if ( int.Parse(row["productID"].ToString()) == productID )
                {
                    editTbProductName.Text = row["productName"].ToString();
                    editTbProductDes.Text = row["ProductDec"].ToString();
                    editDdlProdStatus.SelectedValue = row["status"].ToString();
                    editLblProdStatus.Text = row["status"].ToString();
                    editDdlCategory.SelectedValue = row["category"].ToString();
                    editTbQuantity.Text = row["Stock"].ToString();


                    string[] variants = row["Variant"] as string[];
                    int[] prices = row["Price"] as int[];

                    if ( variants != null && prices != null )
                    {
                        for ( int v = 0 ; v < variants.Length ; v++ )
                        {
                            int Variantcount = ViewState["VariantCount"] != null ? (int)ViewState["VariantCount"] : 1;
                            // Dynamically create text boxes for each variant
                            TextBox variantTextBox = new TextBox
                            {
                                ID = "variant" + ( v + 1 ) + "Tb",
                                CssClass = "newVariation_input",
                                Text = variants[v], // Initialize with the variant value
                                Attributes = { ["Placeholder"] = "Variant " + ( v + 1 ) }
                            };

                            // Dynamically create text boxes for each price
                            TextBox priceTextBox = new TextBox
                            {
                                ID = "priceVar" + ( v + 1 ) + "Tb",
                                CssClass = "newVariation_input",
                                Text = prices[v].ToString(), // Initialize with the price value
                                Attributes = { ["placeholder"] = "Price for Variant " + ( v + 1 ) }
                            };

                            // Add the text boxes to the panel
                            Literal divStart = new Literal { Text = "<div class='flex gap-4 items-center flex-wrap justify-evenly'>" };
                            Literal divEnd = new Literal { Text = "</div>" };

                            ViewState["VariantCount"] = ( Variantcount + 1 );


                            panelVariantTextBoxes.Controls.Add(divStart);
                            panelVariantTextBoxes.Controls.Add(variantTextBox);
                            panelVariantTextBoxes.Controls.Add(priceTextBox);
                            panelVariantTextBoxes.Controls.Add(divEnd);
                        }
                    }
                }

            }
        }
    }
}